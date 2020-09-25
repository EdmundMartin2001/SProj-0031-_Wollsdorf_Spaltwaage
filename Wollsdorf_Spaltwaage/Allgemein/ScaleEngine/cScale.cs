namespace Allgemein
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Text;
    using MTTS.IND890.CE;
    using System.Diagnostics;
    using System.Windows.Forms;

    internal class cScale
    {
        private CIND890APIScaleClient objIND890APIScaleClient;
        private CIND890APITerminalClient objIND890APITerminalClient;

        private bool bISConnection;

        public cScale()
        {
            this.bISConnection = false;
          //  this.objIND890APIClient = null;
            this.objIND890APIScaleClient = null;
            this.objIND890APITerminalClient = null;
        
        
        }

        ~cScale()
        {
            this.Close();
        }

        public void Run_Setup()
        {
            if (cGlobalScale.objCIND890APIClient != null)
            {
                bool bloging = cGlobalScale.objCIND890APIClient.Terminal.LoginStatus;
                cGlobalScale.objCIND890APIClient.Terminal.LoadSetupScreen();
            }
        }

        public void Close()
        {
            try
            {
                if (cGlobalScale.objCIND890APIClient != null)
                {
                    cGlobalScale.objCIND890APIClient.DisconnectFromAPIServer();
                    cGlobalScale.objCIND890APIClient = null;
                }

                if (this.objIND890APIScaleClient != null)
                {
                    this.objIND890APIScaleClient.DisconnectFromAPIServer();
                    this.objIND890APIScaleClient = null;
                }

                if (this.objIND890APITerminalClient != null)
                {
                    this.objIND890APITerminalClient.DisconnectFromAPIServer();
                    this.objIND890APITerminalClient = null;
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
                throw;
            }
        }

        public bool Start()
        {
            bool bRet = false;

            try
            {
                bRet = cGlobalScale.STARTE_CLIENT();
                if (!bRet)
                {
                    MessageBox.Show("Keine Verbindung zu Terminal Client");
                    return false;
                }

                this.objIND890APIScaleClient = new CIND890APIScaleClient();
                this.objIND890APITerminalClient = new CIND890APITerminalClient();
                
                string apiPassword = "API";
                //To enable IPC Communication , available only for WinCE
                this.bISConnection = cGlobalScale.objCIND890APIClient.ConnectToAPIServer(apiPassword);
                if (this.bISConnection)
                {
                    cGlobalScale.objCIND890APIClient.Terminal.Top = 19;
                    cGlobalScale.objCIND890APIClient.Terminal.Left = 669;

                    // Schalte die Waagenanzeige auf Sichbar, nur wenn diese unsichtbar ist.
                    if (!cGlobalScale.objCIND890APIClient.Terminal.WeightWindowStatus)
                    {                        
                        cGlobalScale.objCIND890APIClient.Terminal.HideWeightWindow = false;
                    }
                }
                else
                {
                    MessageBox.Show("Connect To API Server nicht möglich [#Fehler 298]");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
                MessageBox.Show(ex.Message, "Scale Start");
            }
            finally
            {
            }

            return this.bISConnection;
        }        
    }
}
