using System;
using System.Windows.Forms;
using MTTS.IND890.CE;
using Wollsdorf_Spaltwaage.Allgemein.SQL;

namespace Wollsdorf_Spaltwaage.Allgemein.ScaleEngine
{
    internal class cGlobalScale
    {
        private static cScale objSCALE;

        public static CIND890APIDIOClient objCIND890APIClient_DigitalIO;
        public static CIND890APITerminalClient objCIND890APITerminalClient;
        public static CIND890APIScaleClient objCIND890APIClient;
        public static CIND890APIWeighingClient m_APIWeighingClient;
        public static CInterface objRS232_X5;

        public static void create_client_objects()
        {
            try
            {
                objCIND890APIClient = new CIND890APIScaleClient();                
                objCIND890APITerminalClient = new CIND890APITerminalClient();
                objCIND890APIClient_DigitalIO = new CIND890APIDIOClient();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "GlobalScale.create_client_objects");
            }
        }
        public static bool STARTE_SCALE()
        {
            bool bRet = false;

            try
            {
                objSCALE = new cScale();
                bRet= objSCALE.Start();
                
                objRS232_X5 = objCIND890APIClient.Interface[5];
                objRS232_X5.DataMode = MTTS.IND890.CE.CInterface.enumDataMode.MODE_STRING;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "GlobalScale (a)");
                throw;
            }

            return bRet;
        }
        public static void BEENDE_SCALE()
        {
            if ( objSCALE != null)
            {
                objSCALE.Close();
                objSCALE = null;
            }
        }

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
