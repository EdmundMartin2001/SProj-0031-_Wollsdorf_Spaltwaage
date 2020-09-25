namespace Allgemein
{
    using System;
    using System.Windows.Forms;
    using MTTS.IND890.CE;
    using System.Diagnostics;

    internal class cGlobalScale
    {
        public static cScale objSCALE;
        public static MTTS.IND890.CE.CIND890APIClient objCIND890APIClient;
        public static MTTS.IND890.CE.CInterface objRS232_X5;

        //public static Allgemein.DIO.cSMT_DIO objDIO;

        public static bool STARTE_CLIENT()
        {
            bool bRet = false;

            try
            {
                objCIND890APIClient = new MTTS.IND890.CE.CIND890APIClient();
                
                bRet = objCIND890APIClient != null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return bRet;
        }
        public static bool STARTE_SCALE()
        {
            bool bRet = false;

            try
            {
                objSCALE = new cScale();
                bRet= objSCALE.Start();
                

                objRS232_X5 = objCIND890APIClient.Interface[5]; //x5
                objRS232_X5.DataMode = MTTS.IND890.CE.CInterface.enumDataMode.MODE_STRING;

 
            }
            catch (Exception ex)
            {
                SiAuto.LogException(ex);
                throw;
            }

            return bRet;
        }
        public static void BEENDE_SCALE()
        {
            try
            {
                if ( objSCALE != null)
                {
                    objSCALE.Close();
                    objSCALE = null;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        //public static void INI_DIO()
        //{
        //    //if (objDIO == null)
        //    //{
        //    //    objDIO = new Allgemein.DIO.cSMT_DIO();
        //    //}
        //}

        public static void Hide_Scale()
        {
            if (cGlobalScale.objCIND890APIClient != null)
            {
                // Schalte die Waagenanzeige auf Unsichtbar, wenn diese Sichtbar ist
                if (cGlobalScale.objCIND890APIClient.Terminal.WeightWindowStatus)
                {
                    cGlobalScale.objCIND890APIClient.Terminal.HideWeightWindow = true;
                }
            }
        }
        public static void Show_Scale()
        {
            if (cGlobalScale.objCIND890APIClient != null)
            {
                // Schalte die Waagenanzeige auf Sichbar, nur wenn diese unsichtbar ist.
                if (!cGlobalScale.objCIND890APIClient.Terminal.WeightWindowStatus)
                {
                    cGlobalScale.objCIND890APIClient.Terminal.Top = 19;
                    cGlobalScale.objCIND890APIClient.Terminal.Left = 669;
                    cGlobalScale.objCIND890APIClient.Terminal.HideWeightWindow = false;
                }
            }
        }

        public static CScaleWeight GetStabile()
        {
            CWeight w = null;
            CScaleWeight s = new CScaleWeight();

            try
            {
                long lClock = Environment.TickCount;

                do
                {                    
                    cGlobalScale.objCIND890APIClient.CurrentScale.GetGNTInPrimaryUnit(ref s);
                    
                    if (s.WeightStable == true&& s.Status == CScaleWeight.enumScaleStatus.STATUS_WEIGHTOK)
                    {
                        //w = s.NetWeight;
                        //Trace.WriteLine(w.Weight.ToString() + " " + w.Unit + " ==>" + w.Status.ToString() + " =>STABIL");
                        break;
                    }
                    else if (s.WeightStable == false && s.Status == CScaleWeight.enumScaleStatus.STATUS_WEIGHTOK)
                    {
                        //w = s.NetWeight;
                        //Trace.WriteLine(w.Weight.ToString() + " " + w.Unit + " ==>" + w.Status.ToString() + " =>DYNAMISCH");
                    }
                    else
                    {
                        //Trace.WriteLine("ERROR");
                    }

                    if (Environment.TickCount - lClock > 4000)
                    {
                        break;
                    }
                }
                while (true);
            }
            catch (Exception ex)
            {
                SiAuto.LogException(ex);
                throw;
            }

            if (s.WeightStable == true && s.Status == CScaleWeight.enumScaleStatus.STATUS_WEIGHTOK)
            {
                return s;
            }
            else
            {
                return null;
            }
        }
        public static CScaleWeight GetDynamic()
        {
            CScaleWeight s = new CScaleWeight();

            try
            {
                long lClock = Environment.TickCount;

                do
                {
                    cGlobalScale.objCIND890APIClient.CurrentScale.GetGNTInPrimaryUnit(ref s);

                    if (s.Status == CScaleWeight.enumScaleStatus.STATUS_WEIGHTOK)
                    {
                        break;
                    }

                    if (Environment.TickCount - lClock > 4000)
                    {
                        break;
                    }
                }
                while (true);
            }
            catch (Exception ex)
            {
                SiAuto.LogException(ex);
                throw;
            }

            if (s.Status == CScaleWeight.enumScaleStatus.STATUS_WEIGHTOK)
            {
                return s;
            }
            else
            {
                return null;
            }
        }

        public static MTTS.IND890.CE.CScale.enumTareResult TareScale()
        {            
            if (cGlobalScale.objCIND890APIClient != null)
            {
                return cGlobalScale.objCIND890APIClient.CurrentScale.PerformTare();
            }
            else
            {
                return MTTS.IND890.CE.CScale.enumTareResult.TARE_NOTSUPPORTED;
            }
        }
        public static MTTS.IND890.CE.CScale.enumTareResult setTare(double dGewicht)
        {
            if (cGlobalScale.objCIND890APIClient != null)
            {
                CWeight w = new CWeight(dGewicht ,1, cGlobalScale.objCIND890APIClient.CurrentScale.GrossWeight.Unit);

                MTTS.IND890.CE.CScale.enumTareResult tareResult;                
                tareResult = cGlobalScale.objCIND890APIClient.CurrentScale.PerformTare(w);

                //MessageBox.Show(tareResult.ToString());

                return tareResult;
            }
            else
            {
                return MTTS.IND890.CE.CScale.enumTareResult.TARE_NOTSUPPORTED;
            }
        }
        public static MTTS.IND890.CE.CScale.enumZeroResult ZeroScale()
        {
            if (cGlobalScale.objCIND890APIClient != null)
            {
                return cGlobalScale.objCIND890APIClient.CurrentScale.PerformZero();
            }
            else
            {
                return MTTS.IND890.CE.CScale.enumZeroResult.ZERO_NOTPERFORMED;
            }
        }      
    }
}
