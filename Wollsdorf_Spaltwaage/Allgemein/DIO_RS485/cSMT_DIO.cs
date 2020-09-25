using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;

namespace Allgemein.DIO
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
            bValue= (byte)this.BitFld_ARM_Box1.Mask;
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
            if (this.Get_RelaisBox_für_Ausgang(iBitNr)==1)
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
                    s = cGlobalScale.objCIND890APIClient.DiscreteIO.ReadDIOInput(port, slave_ARM_Box1);
                    iReturn = Convert.ToInt32(s);
                    this.BitFld_ARM_Box1_Eingänge.Mask = (byte) iReturn;
                }
                else if (RelaisBox == 2)
                {
                    s = cGlobalScale.objCIND890APIClient.DiscreteIO.ReadDIOInput(port, slave_ARM_Box2);
                    iReturn = Convert.ToInt32(s);
                    this.BitFld_ARM_Box2_Eingänge.Mask = (byte) iReturn;
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
                MessageBox.Show("IO Fehler \r\n" + ex.Message);
                throw;
            }
            
            return bRet;
        }
        private void WriteDIO(byte value, int RelaisBox)
        {
            try
            {
                if (RelaisBox == 1)
                {
                    cGlobalScale.objCIND890APIClient.DiscreteIO.WriteToDIO(slave_ARM_Box1, port, value);
                }
                else if (RelaisBox == 2)
                {
                    cGlobalScale.objCIND890APIClient.DiscreteIO.WriteToDIO(slave_ARM_Box2, port, value);
                }
                else
                {
                    Debug.Assert(false);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("IO Fehler \r\n" + ex.Message);
                throw;
            }
        }        
    }
}
