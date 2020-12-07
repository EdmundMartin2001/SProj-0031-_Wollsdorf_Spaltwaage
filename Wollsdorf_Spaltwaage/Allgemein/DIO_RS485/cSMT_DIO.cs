using System;
using System.Diagnostics;
using System.Windows.Forms;
using Allgemein;
using Wollsdorf_Spaltwaage.Allgemein.ScaleEngine;
using Wollsdorf_Spaltwaage.Allgemein.SQL;
using Wollsdorf_Spaltwaage.Kundenspezifisch;

namespace Wollsdorf_Spaltwaage.Allgemein.DIO_RS485
{
    internal class cSMT_DIO
    {
        private HELPER_BIT_FIELD BitFld_ARM_Box1; //ARM Relaisbox mit 1 bis 7 Ausgänge
        private HELPER_BIT_FIELD BitFld_ARM_Box2; //ARM Relaisbox mit 1 bis 7 Ausgänge

        private HELPER_BIT_FIELD BitFld_ARM_Box1_Eingänge;
        private HELPER_BIT_FIELD BitFld_ARM_Box2_Eingänge;

        private const byte slave_ARM_Box1 = 1;       //select ARM100 with slave address 2
        private const byte slave_ARM_Box2 = 2;       //select ARM100 with slave address 2

        private const byte port = 3;        //X3 der Port der RS485 Karte, ist für beide Relaisboxen gleich

        public cSMT_DIO()
        {
            this.BitFld_ARM_Box1 = new HELPER_BIT_FIELD();
            this.BitFld_ARM_Box1.Mask = 0;
            this.BitFld_ARM_Box2 = new HELPER_BIT_FIELD();
            this.BitFld_ARM_Box2.Mask = 0;

            this.BitFld_ARM_Box1_Eingänge = new HELPER_BIT_FIELD();
            this.BitFld_ARM_Box1_Eingänge.Mask = 0;
            this.BitFld_ARM_Box2_Eingänge = new HELPER_BIT_FIELD();
            this.BitFld_ARM_Box2_Eingänge.Mask = 0;
        }

        public void DIO_ResetAll()
        {
            this.BitFld_ARM_Box1.ClearField();
            this.BitFld_ARM_Box2.ClearField();
            this.BitFld_ARM_Box1_Eingänge.ClearField();
            this.BitFld_ARM_Box2_Eingänge.ClearField();

            byte bValue = 0;
            bValue = (byte)this.BitFld_ARM_Box1.Mask;
            this.WriteDIO(bValue, /*Relaisbox*/ 1);
            bValue = (byte)this.BitFld_ARM_Box2.Mask;
            this.WriteDIO(bValue, /*Relaisbox*/ 2);
        }

        /// <summary>
        /// Liefere die ID für Ausgänge da bei mehreren RelaisBoxen in Serie möglich sein.
        /// Auf jeder Relaisbox sind 6 Ausgänge.
        /// Also 1,2,3,4,5,6 sind auf RelaisBox1
        /// dann 7,8,9,10,11,12 auf ReleasBox2
        /// </summary>
        /// <param name="iBitNr"></param>
        /// <returns></returns>
        private int Get_RelaisBox_für_Ausgang(int iBitNr)
        {
            if (iBitNr > 0 && iBitNr < 7)
            {
                return 1;
            }
            else if (iBitNr > 6 && iBitNr < 13)
            {
                return 2;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Liefere die ID der RelaisBox für Eingänge.
        /// Auf jeer Relaisbox sind 4 Eingänge.
        /// Also 1,2,3,4 auf Relaisbox1
        /// dann 5,6,7,8 auf Relaisbox2
        /// </summary>
        /// <param name="iBitNr"></param>
        /// <returns></returns>
        private int Get_RelaisBox_für_Eingang(int iBitNr)
        {
            if (iBitNr > 0 && iBitNr < 5)
            {
                return 1;
            }
            else if (iBitNr > 4 && iBitNr < 9)
            {
                return 2;
            }
            else
            {
                return 0;
            }
        }
        public bool Get_OUT_Bit(int iBitNr)
        {
            if (this.Get_RelaisBox_für_Ausgang(iBitNr) == 1)
            {
                return BitFld_ARM_Box1.AnyOn(HELPER_BIT_FIELD.DecimalToFlag(iBitNr));
            }
            if (this.Get_RelaisBox_für_Ausgang(iBitNr) == 2)
            {
                // d
                iBitNr = iBitNr - 6;
                return BitFld_ARM_Box2.AnyOn(HELPER_BIT_FIELD.DecimalToFlag(iBitNr));
            }
            else
            {
                return false;
            }
        }
        public void Set_OUT_Bit(int iBitNr, bool bISOn)
        {
            byte bValue = 0;
            int iRelaisBox = 0;

            iRelaisBox = this.Get_RelaisBox_für_Ausgang(iBitNr);

            if (iRelaisBox == 1)
            {
                HELPER_BIT_FIELD.Flag currFlag = HELPER_BIT_FIELD.DecimalToFlag(iBitNr);

                if (bISOn)
                {
                    this.BitFld_ARM_Box1.SetOn(currFlag);
                }
                else
                {
                    this.BitFld_ARM_Box1.SetOff(currFlag);
                }

                bValue = (byte)this.BitFld_ARM_Box1.Mask;
            }
            else if (iRelaisBox == 2)
            {
                iBitNr = iBitNr - 6;
                HELPER_BIT_FIELD.Flag currFlag = HELPER_BIT_FIELD.DecimalToFlag(iBitNr);

                if (bISOn)
                {
                    this.BitFld_ARM_Box2.SetOn(currFlag);
                }
                else
                {
                    this.BitFld_ARM_Box2.SetOff(currFlag);
                }

                bValue = (byte)this.BitFld_ARM_Box2.Mask;
            }
            else
            {
                Debug.Assert(false);
                return;
            }

            this.WriteDIO(bValue, /*Relaisbox*/ iRelaisBox);
        }
        public bool Get_IN_Bit(int iBitNr)
        {
            return this.ReadDIO(iBitNr);
        }

        private bool ReadDIO(int iBitNr)
        {
            string s = "x";
            bool bRet = false;
            int iReturn = 0;

            int RelaisBox = this.Get_RelaisBox_für_Eingang(iBitNr);

            try
            {
                if (RelaisBox == 1)
                {
                    try
                    {
                        s = cGlobalScale.objCIND890APIClient_DigitalIO.DiscreteIO.ReadDIOInput(/*ist immer 3 für X3*/ port, /*ist immer 1*/ slave_ARM_Box1);
                    }
                    catch (Exception ex)
                    {
                        SiAuto.LogException("ReadDIO(a)", ex);

                        cGlobalHandling.MessageBox(
                            "ARM100 Lesen Box1: Das Scaleinterface meldet an Port X3, unter der Slave Adresse " + slave_ARM_Box1.ToString() +
                            " keine Verbindung", "ARM 100 Lesefunktion (Read DIO)");
                        return false;
                    }


                    iReturn = Convert.ToInt32(s);
                    this.BitFld_ARM_Box1_Eingänge.Mask = (byte)iReturn;
                }
                else if (RelaisBox == 2)
                {
                    try
                    {
                        s = cGlobalScale.objCIND890APIClient_DigitalIO.DiscreteIO.ReadDIOInput(/*ist immer 3 für X3*/ port, /*ist immer 1*/ slave_ARM_Box2);
                    }
                    catch (Exception ex)
                    {
                        SiAuto.LogException("ReadDIO(b)", ex);

                        cGlobalHandling.MessageBox(
                            "ARM100 Lesen Box2: Das Scaleinterface meldet an Port X3, unter der Slave Adresse " + slave_ARM_Box2.ToString() +
                            " keine Verbindung", "ARM 100 Lesefunktion (Read DIO)");
                        return false;
                    }





                    iReturn = Convert.ToInt32(s);
                    this.BitFld_ARM_Box2_Eingänge.Mask = (byte)iReturn;
                }
                else
                {
                }

                if (RelaisBox == 1)
                {
                    bRet = this.BitFld_ARM_Box1_Eingänge.AnyOn(HELPER_BIT_FIELD.DecimalToFlag(iBitNr));
                }
                else if (RelaisBox == 2)
                {
                    iBitNr = iBitNr - 4;
                    bRet = this.BitFld_ARM_Box2_Eingänge.AnyOn(HELPER_BIT_FIELD.DecimalToFlag(iBitNr));
                }
                else
                {
                    bRet = false;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Read DIO IO Fehler \r\n" + ex.Message);
                throw;
            }

            return bRet;
        }
        private void WriteDIO(byte value, int RelaisBox)
        {
            try
            {
                //bool bStatus = m_APIDIOClient.DiscreteIO.WriteToDIO(bLocation, bPort, bValue);

                if (RelaisBox == 1)
                {
                    cGlobalScale.objCIND890APIClient_DigitalIO.DiscreteIO.WriteToDIO(1, port, value);
                }
                else if (RelaisBox == 2)
                {
                    cGlobalScale.objCIND890APIClient_DigitalIO.DiscreteIO.WriteToDIO(2, port, value);
                }
                else
                {
                    Debug.Assert(false);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Write DIO IO Fehler \r\n" + ex.Message);
                throw;
            }
        }
    }
}


//using System;
//using System.Diagnostics;
//using System.Windows.Forms;

//namespace Allgemein.DIO
//{
//    /// <summary>
//    /// Wollsdorf verwendet 2x 4io Karten an Port X3 und an Port X4
//    /// </summary>
//    internal class cSMT_DIO
//    {
//        private readonly HELPER_BIT_FIELD BitFld_ARM_Box1; //ARM Relaisbox mit 1 bis 7 Ausgänge
//        private readonly HELPER_BIT_FIELD BitFld_ARM_Box2; //ARM Relaisbox mit 1 bis 7 Ausgänge

//        private readonly HELPER_BIT_FIELD BitFld_ARM_Box1_Eingänge;
//        private readonly HELPER_BIT_FIELD BitFld_ARM_Box2_Eingänge;

//        private const byte slave_ARM_Box1 = 1;       //select ARM100 with slave address 1
//        private const byte slave_ARM_Box2 = 2;       //select ARM100 with slave address 2

//        // Die ARM 100 wird über X3, das ist eine RS485, angesprochen
//        // Beide Relaisboxen sind am selben Port
//        private const byte port_A_X5 = 3;        //X3
//        private const byte port_B_X6 = 3;        //X6

//        public cSMT_DIO()
//        {
//            this.BitFld_ARM_Box1 = new HELPER_BIT_FIELD();
//            this.BitFld_ARM_Box1.Mask = 0;
//            this.BitFld_ARM_Box2 = new HELPER_BIT_FIELD();
//            this.BitFld_ARM_Box2.Mask = 0;

//            this.BitFld_ARM_Box1_Eingänge = new HELPER_BIT_FIELD();
//            this.BitFld_ARM_Box1_Eingänge.Mask = 0;
//            this.BitFld_ARM_Box2_Eingänge = new HELPER_BIT_FIELD();
//            this.BitFld_ARM_Box2_Eingänge.Mask = 0;
//        }

//        public void DIO_ResetAll()
//        {
//            this.BitFld_ARM_Box1.ClearField();
//            this.BitFld_ARM_Box2.ClearField();
//            this.BitFld_ARM_Box1_Eingänge.ClearField();
//            this.BitFld_ARM_Box2_Eingänge.ClearField();

//            byte bValue = 0;
//            bValue= (byte)this.BitFld_ARM_Box1.Mask;
//            this.WriteDIO(bValue, /*Relaisbox*/ 1);
//            bValue = (byte)this.BitFld_ARM_Box2.Mask;
//            this.WriteDIO(bValue, /*Relaisbox*/ 2);
//        }

//        /// <summary>
//        /// Liefere die ID für Ausgänge da bei mehreren RelaisBoxen in Serie möglich sein.
//        /// Auf jeder Relaisbox sind 6 Ausgänge.
//        /// Also 1,2,3,4,5,6 sind auf RelaisBox1
//        /// dann 7,8,9,10,11,12 auf ReleasBox2
//        /// </summary>
//        /// <param name="iBitNr"></param>
//        /// <returns></returns>
//        private int Get_RelaisBox_für_Ausgang(int iBitNr)
//        {
//            if (iBitNr > 0 && iBitNr < 7)
//            {
//                return 1;
//            }
//            else if (iBitNr > 6 && iBitNr < 13)
//            {
//                return 2;
//            }
//            else
//            {
//                return 0;
//            }
//        }

//        /// <summary>
//        /// Liefere die ID der RelaisBox für Eingänge.
//        /// Auf jeer Relaisbox sind 4 Eingänge.
//        /// Also 1,2,3,4 auf Relaisbox1
//        /// dann 5,6,7,8 auf Relaisbox2
//        /// </summary>
//        /// <param name="iBitNr"></param>
//        /// <returns></returns>
//        private int Get_RelaisBox_für_Eingang(int iBitNr)
//        {
//            if (iBitNr > 0 && iBitNr < 5)
//            {
//                return 1;
//            }
//            else if (iBitNr > 4 && iBitNr < 9)
//            {
//                return 2;
//            }
//            else
//            {
//                return 0;
//            }
//        }

//        public bool Get_OUT_Bit(int iBitNr)
//        {
//            if (this.Get_RelaisBox_für_Ausgang(iBitNr)==1)
//            {
//                return BitFld_ARM_Box1.AnyOn(HELPER_BIT_FIELD.DecimalToFlag(iBitNr));
//            }
//            if (this.Get_RelaisBox_für_Ausgang(iBitNr) == 2)
//            {
//                // d
//                iBitNr = iBitNr - 6;
//                return BitFld_ARM_Box2.AnyOn(HELPER_BIT_FIELD.DecimalToFlag(iBitNr));
//            }
//            else
//            {
//                return false;
//            }
//        }
//        public void Set_OUT_Bit(int iBitNr, bool bISOn)
//        {                        
//            byte bValue = 0;
//            int iRelaisBox = 0;

//            iRelaisBox = this.Get_RelaisBox_für_Ausgang(iBitNr);

//            if (iRelaisBox == 1)
//            {
//                HELPER_BIT_FIELD.Flag currFlag = HELPER_BIT_FIELD.DecimalToFlag(iBitNr);

//                if (bISOn)
//                {
//                    this.BitFld_ARM_Box1.SetOn(currFlag);
//                }
//                else
//                {
//                    this.BitFld_ARM_Box1.SetOff(currFlag);
//                }

//                bValue = (byte)this.BitFld_ARM_Box1.Mask;                
//            }
//            else if (iRelaisBox == 2)
//            {
//                iBitNr = iBitNr - 6;
//                HELPER_BIT_FIELD.Flag currFlag = HELPER_BIT_FIELD.DecimalToFlag(iBitNr);

//                if (bISOn)
//                {
//                    this.BitFld_ARM_Box2.SetOn(currFlag);
//                }
//                else
//                {
//                    this.BitFld_ARM_Box2.SetOff(currFlag);
//                }

//                bValue = (byte)this.BitFld_ARM_Box2.Mask;     
//            }
//            else
//            {
//                Debug.Assert(false);
//                return;
//            }

//            this.WriteDIO(bValue, /*Relaisbox*/ iRelaisBox);
//        }

//        public bool Get_IN_Bit(int iBitNr)
//        {
//            return this.ReadDIO(iBitNr);
//        }

//        private bool ReadDIO(int iBitNr)
//        {
//            string s = "x";
//            bool bRet = false;
//            int iReturn = 0;

//            int RelaisBox = this.Get_RelaisBox_für_Eingang(iBitNr);
            
//            try
//            {
//                if (RelaisBox == 1)
//                {
//                    s = cGlobalScale.objCIND890APIClient_DigitalIO.DiscreteIO.ReadDIOInput(port_A_X5, slave_ARM_Box1);
//                    iReturn = Convert.ToInt32(s);
//                    this.BitFld_ARM_Box1_Eingänge.Mask = (byte) iReturn;
//                }
//                else if (RelaisBox == 2)
//                {
//                    s = cGlobalScale.objCIND890APIClient_DigitalIO.DiscreteIO.ReadDIOInput(port_B_X6, slave_ARM_Box2);
//                    iReturn = Convert.ToInt32(s);
//                    this.BitFld_ARM_Box2_Eingänge.Mask = (byte) iReturn;
//                }

//                if (RelaisBox == 1)
//                {
//                    bRet = this.BitFld_ARM_Box1_Eingänge.AnyOn(HELPER_BIT_FIELD.DecimalToFlag(iBitNr));
//                }
//                else if (RelaisBox == 2)
//                {
//                    iBitNr = iBitNr - 4;
//                    bRet = this.BitFld_ARM_Box2_Eingänge.AnyOn(HELPER_BIT_FIELD.DecimalToFlag(iBitNr));
//                }
//                else
//                {
//                    bRet = false;
//                }

//            }
//            catch (Exception ex)
//            {             
//                MessageBox.Show("Read IO Fehler \r\n" + ex.Message);
//                throw;
//            }
            
//            return bRet;
//        }
//        private void WriteDIO(byte value, int RelaisBox)
//        {
//            try
//            {
//                if (cGlobalScale.objCIND890APIClient_DigitalIO == null)
//                {
//                    MessageBox.Show("WriteDIO API ist NULL", "Phase1");
//                    return;
//                }
//                else if (cGlobalScale.objCIND890APIClient_DigitalIO.DiscreteIO == null)
//                {
//                    MessageBox.Show("WriteDIO Discrete IO liefert NULL", "Phase1");
//                    return;
//                }

//                if (RelaisBox == 1)
//                {                    
//                    cGlobalScale.objCIND890APIClient_DigitalIO.DiscreteIO.WriteToDIO( /*Location*/ (byte)1, (byte)port_A_X5, (byte)value);                    
//                }
//                else if (RelaisBox == 2)
//                {
//                    cGlobalScale.objCIND890APIClient_DigitalIO.DiscreteIO.WriteToDIO( /*Location*/ (byte)2, (byte)port_B_X6, (byte)value);
//                }
//                else
//                {
//                    Debug.Assert(false);
//                }
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show("IO Fehler \r\n" + ex.Message);
//                throw;
//            }
//        }        
//    }
//}
