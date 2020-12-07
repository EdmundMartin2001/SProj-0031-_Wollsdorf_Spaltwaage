using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace Wollsdorf_Spaltwaage.Allgemein.ScaleEngine
{
    internal class cScale
    {
        private bool bISConnection;

        public cScale()
        {
            this.bISConnection = false;
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
                if (cGlobalScale.objRS232_X5 != null)
                {                    
                    cGlobalScale.objRS232_X5 = null;
                }

                if (cGlobalScale.objCIND890APIClient != null)
                {
                    cGlobalScale.objCIND890APIClient.DisconnectFromAPIServer();
                    cGlobalScale.objCIND890APIClient = null;
                }

                if (cGlobalScale.objCIND890APIClient_DigitalIO != null)
                {
                    cGlobalScale.objCIND890APIClient_DigitalIO.DisconnectFromAPIServer();
                    cGlobalScale.objCIND890APIClient_DigitalIO = null;
                }

                if (cGlobalScale.objCIND890APITerminalClient != null)
                {
                    cGlobalScale.objCIND890APITerminalClient.DisconnectFromAPIServer();
                    cGlobalScale.objCIND890APITerminalClient = null;
                }
                if (cGlobalScale.m_APIWeighingClient != null)
                {
                    cGlobalScale.m_APIWeighingClient.DisconnectFromAPIServer();
                    cGlobalScale.m_APIWeighingClient = null;
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
            const string apiPassword = "API";

            try
            {
                // Erstelle neue Client Objekte
                cGlobalScale.create_client_objects();

                this.bISConnection = cGlobalScale.objCIND890APIClient.ConnectToAPIServer(apiPassword);

                if (this.bISConnection)
                {
                    bool bStatus = cGlobalScale.objCIND890APIClient.RunSecondClient(true);

                    if (!bStatus)
                    {
                        MessageBox.Show("Connection to IND890 API Server failed.", "Phase#1.IND890APIClient.RunSecondClient");
                        return false;
                    }
                    else
                    {
                        this.bISConnection = cGlobalScale.objCIND890APITerminalClient.ConnectToAPIServer("API");

                        if (!this.bISConnection)
                        {
                            MessageBox.Show("Connection to IND890 API Server failed.", "Phase#3.IND890APIClient.CIND890APITerminalClient");
                            return false;
                        }
                        else
                        {
                            bStatus = cGlobalScale.objCIND890APIClient.RunThirdClient(true);

                            if (!bStatus)
                            {
                                MessageBox.Show("Connection to IND890 API Server failed.", "Phase#4.IND890APIClient.RunThirdClient");
                                return false;
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Connection to IND890 API Server2 failed.", "Phase#1.IND890APIClient.RunSecondClient");
                    return false;
                }


                if (this.bISConnection)
                {
                    bool bIS4IO_Connection = cGlobalScale.objCIND890APIClient_DigitalIO.ConnectToAPIServer(apiPassword);

                    if (!bIS4IO_Connection)
                    {
                        MessageBox.Show("Achtung!\r\n\r\nKeine Verbindung zur ARM100", "Phase#5.IND890APIClient.CIND890APIClient_DigitalIO");
                    }
                    else
                    {
                        //MessageBox.Show("Die Verbindung zur ARM100\r\nwurde erfolgreich aufgebaut", "Digital IO");
                    }

                    
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
