/*-------------------------------------------------------------------------------
Module                  : frmAPICalls_NonIPC.CS
Version                 : 
-------------------------------------------------------------------------------
Created                 : x.x.x, dd.mm.yy, Author

Modification History:
-------------------------------------------------------------------------------
TTP No.                 : 5875
Version                 : V1.3.6A 
Author                  : MTTS-HARI                    Date : 2/8/2017
Verified by             : MTTS-JRK                     Date : 4/8/2017
Description             : Equip IND890 API with a 4th channel for 'high speed' serial communication
Affected functions      : frmSamples_Load, btnInterfaceCmdWrite_Click, btnInterfaceCmdWriteByte_Click, 
 * btnSetDataMode_Click, btnGetDataMode_Click, frmAPICalls_Closing
 -------------------------------------------------------------------------------  
 TTP No.                 : 5873
Version                 : V1.3.7a 
Author                  : MTTS-RAD                     Date : 22/8/2017
Verified by             : MTTS-JRK                     Date : 23/8/2017
Description             : Extend API read OS / Image version of CE.
Affected functions      :  
 * Added methods - GetTerminalBtn_Click,ReadRegistryKey,IsWEC7Version
           .
 -------------------------------------------------------------------------------  
TTP No.                 : 5795
Version                 : V1.3.7c 
Author                  : MTTS-HARI                    Date : 27/9/2017
Verified by             : MTTS-JRK                     Date : 
Description             : Equip API with a function to trigger Scale FACT 2/2. Created 5th Client for GetGNT.
Affected functions      : Changed Scale Client to Weighing client in methods : frmSamples_Load, GetWeight, frmAPICalls_Closing, button7_RB_All_Click, 
 *                          timer1_Parallel_Scale_Mode_Tick
 * StopScale and StartScale not needed in Scale functions: btnZero_Click, btnTare_Click, btnPreTare_Click, m_APIScaleClient_OnScaleTared, btnSwitchUnit_Click,
 *                          btnFact_Click, btnSendFact_Click
 -------------------------------------------------------------------------------  
 TTP No.                : 5724
Version                 : V1.3.7b 
Author                  : MTTS-RAD                     Date : 27/9/2017
Verified by             : MTTS-JRK                     Date : 28/9/2017
Description             : Complete APIClientTool with SQL test functions for test.
Affected functions      :  
 * Added methods - btnView_Click,btnSearch_Click,btnAdd_Click,btnConnect_Click,btnSelect_Click
                    btnDisconnect_Click,btnUpdate_Click,btnDelete_Click,IsNumeric.
 ------------------------------------------------------------------------------- 
 */
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MTTS.IND890.CE;
using System.Threading;
using System.Diagnostics;
using Microsoft.Win32;

namespace IND890APIClientTool_NonIPC
{
    public partial class frmAPICalls_NonIPC : Form
    {
        #region Constatnt
        private const string constAPPLICATION = "API Client Tool 1.29";
        private const string constBuildDate = "06.11.2017";
        private const int constGETGNTCALLRATE = 100;

        private enum enumKeypadType
        {
            NONE = -1,
            ALPHA_NUMERIC = 0,
            PRETARE = 1,
            SET_UNIT = 2,
            QUIT_MESSAGEBOX = 3,
            NUMERIC = 4,
            //@MTTS-CTA ,21-05-2013 #4072
            BROWSE_DIALOG = 5
        }
        ////@MTTS-CTA ,21-05-2013 #4072 Browse Dialog open  Mode
        //public enum enumDialogType
        //{
        //    OpenDialog = 0,
        //    SaveAsDialog = 1,
        //    FoderDialog =2 
        //};
        private enum enumDockStyle
        {
            NONE = 0,
            TOP = 1,
            BOTTOM = 2
        }

        #endregion
        private static CIND890APIScaleClient m_APIScaleClient;
        private static CIND890APITerminalClient m_APITerminalClient;
        private static CIND890APIDIOClient m_APIDIOClient;
        //MTTS-HARI - v1.3.6A - 2/8/2017 - #5875 - Equip IND890 API with a 4th channel for 'high speed' serial communication
        private static CIND890APISerialClient m_APISerialClient;
        //MTTS-HARI - v1.3.7c - 27/9/2017 - #5795 - Equip API with a function to trigger Scale FACT 2/2
        private static CIND890APIWeighingClient m_APIWeighingClient;
        private CScaleInfo m_ScaleInfo;
        private CScaleWeight m_ScaleWeight;
        private System.Threading.Timer m_ThreadTimer;
        private System.Threading.Timer m_BacklightTimer;
        private enumKeypadType m_eKeypadType = enumKeypadType.NONE;
        private string m_AlphaNumericInput = "Test Data";
        private string m_BrowseDialogInput = string.Empty;
        private bool IsTimerEnabled = true;
        private bool m_ShutDownSystem;
        private CTerminal.enumApplicationExitType m_ShutDownType;
        
        #region Constructor
        public frmAPICalls_NonIPC()
        {
            InitializeComponent();
            m_APIScaleClient = new CIND890APIScaleClient();
            m_ScaleInfo = new CScaleInfo();
            m_ScaleWeight = new CScaleWeight();
            m_ThreadTimer = new System.Threading.Timer(eventOnTimer, null, Timeout.Infinite, Timeout.Infinite);
            m_BacklightTimer = new System.Threading.Timer(eventOnBacklightTimer, null, Timeout.Infinite, Timeout.Infinite);
            if (m_BacklightTimer != null)
                m_BacklightTimer.Change(Timeout.Infinite, Timeout.Infinite);

            CreateAlibiDatatable();
            //MTTS_RAD - 22/8/2017 -TTP5873 Added,Check Neo terminal.
            IsNeoTerminal = false; IsWEC7Version();
            //MTTS-RAD - 25/9/2017 - TTP5724 Created,productDetailform object.
            productObj = new CProductDetail();
            m_DBFunction = CDBFunctions.GetInstance();
            frmProductDetailObj = new frmProductDetail();
        }
        #endregion

        #region Events
        private void eventOnTimer(object StateInfo)
        {
            if (m_ThreadTimer != null)
                m_ThreadTimer.Change(Timeout.Infinite, Timeout.Infinite);
            if (IsTimerEnabled == false) return;
            GetWeight();
            if (m_ThreadTimer != null)
                //Mtts-Hari - 13/2/2015 - Timer called immediately after the previous call
                //m_ThreadTimer.Change(0, constGETGNTCALLRATE);
                m_ThreadTimer.Change(constGETGNTCALLRATE, constGETGNTCALLRATE);
            
        }
        private void eventOnBacklightTimer(object StateInfo)
        {
            if (m_BacklightTimer != null)
                m_BacklightTimer.Change(Timeout.Infinite, Timeout.Infinite);
            m_APITerminalClient.Terminal.TurnOnBackLight();
        }

        private void frmSamples_Load(object sender, EventArgs e)
        {
            this.Text = string.Format("{0} {1}", constAPPLICATION, constBuildDate);
            int x = (Screen.PrimaryScreen.WorkingArea.Width / 2) - (Width / 2);
            int y = (Screen.PrimaryScreen.WorkingArea.Height / 2) - (Height / 2);
            Location = new Point(x, y);
            bool bConnectToAPI = true;

            if (m_APIScaleClient.ConnectToAPIServer("API") == false)
            {
                MessageBox.Show("Connection to IND890 API Server1 failed.", "IND890APIClient") ;
                m_APIScaleClient.Dispose();
                this.Close();
                bConnectToAPI = false;
            }
            if (bConnectToAPI)
            {
                //m_APIScaleClient.OnScaleZeroed += m_APIScaleClient_OnScaleZeroed;
                m_APIScaleClient.OnScaleTared += m_APIScaleClient_OnScaleTared; // uncommented for 5891 - pretare with invalid weight value
                //m_APIScaleClient.OnSwitchScale += m_APIScaleClient_OnSwitchScale;
                //MTTS-HARI 14/10/2015 v1.3.3A #5480 Notify API if High Resolution is switched off automatically in Approved mode.
                m_APIScaleClient.OnNotifyHighResOff += new CIND890APIClient.delegateOnNotifyHighResOff(m_APIScaleClient_OnNotifyHighResOff);
                m_APIScaleClient.OnSendFactResult += new CIND890APIClient.delegateOnGetSendFactResult(m_APIScaleClient_OnSendFactResult);
            }
            bool bStatus = m_APIScaleClient.RunSecondClient(true);
            bool bDIOClientStatus = m_APIScaleClient.RunThirdClient(true);
            //MTTS-HARI - v1.3.6A - 2/8/2017 - #5875 - Equip IND890 API with a 4th channel for 'high speed' serial communication
            bool bSerialClientStatus = m_APIScaleClient.RunFourthClient(true);
            //MTTS-HARI - v1.3.7c - 27/9/2017 - #5795 - Equip API with a function to trigger Scale FACT 2/2
            bool bWeighingClientStatus = m_APIScaleClient.RunFifthClient(true);
            m_APITerminalClient = new CIND890APITerminalClient();
            m_APIDIOClient = new CIND890APIDIOClient();
            //MTTS-HARI - v1.3.6A - 2/8/2017 - #5875 - Equip IND890 API with a 4th channel for 'high speed' serial communication
            m_APISerialClient = new CIND890APISerialClient();
            //MTTS-HARI - v1.3.7c - 27/9/2017 - #5795 - Equip API with a function to trigger Scale FACT 2/2
            m_APIWeighingClient = new CIND890APIWeighingClient();
            bConnectToAPI = true;
            if (m_APITerminalClient.ConnectToAPIServer("API") == false)
            {
                MessageBox.Show("Connection to IND890 API Server2 failed.", "IND890APIClient");
                m_APITerminalClient.Dispose();
                this.Close();
                bConnectToAPI = false;
            }
            if (m_APIDIOClient.ConnectToAPIServer("API") == false)
            {
                MessageBox.Show("Connection to IND890 API Server3 failed.", "IND890APIClient");
                m_APIDIOClient.Dispose();
                this.Close();
                bConnectToAPI = false; 
            }
            //MTTS-HARI - v1.3.6A - 2/8/2017 - #5875 - Equip IND890 API with a 4th channel for 'high speed' serial communication
            if (m_APISerialClient.ConnectToAPIServer("API") == false)
            {
                MessageBox.Show("Connection to IND890 API Server4 failed.", "IND890APIClient");
                m_APISerialClient.Dispose();
                this.Close();
                bConnectToAPI = false;
            }
            //MTTS-HARI - v1.3.7c - 27/9/2017 - #5795 - Equip API with a function to trigger Scale FACT 2/2
            if (m_APIWeighingClient.ConnectToAPIServer("API") == false)
            {
                MessageBox.Show("Connection to IND890 API Server5 failed.", "IND890APIClient");
                m_APIWeighingClient.Dispose();
                this.Close();
                bConnectToAPI = false;
            }
            if (bConnectToAPI)
            {

                m_APITerminalClient.OnGetKeypadResult += m_APITerminalClient_OnGetKeypadResult;
                //m_APITerminalClient.DiscreteIO.OnDIOInput += DiscreteIO_OnDIOInput;
                m_APITerminalClient.OnBackLightTurnedOn += new CIND890APIClient.delegateOnBackLightTurnedOn(m_APIScaleClient_OnBackLightTurnedOn);
                //MTTS-HARI 13/10/2015 v1.3.3A #5481 Notify API about automatic User logout.
                m_APITerminalClient.OnNotifyUserLogout += new CIND890APIClient.delegateOnNotifyUserLogout(m_APIScaleClient_OnNotifyUserLogout);
                m_APITerminalClient.OnSetupExited += new CIND890APIClient.delegateOnSetupExited(m_APITerminalClient_OnSetupExited);

                //m_APITerminalClient.OnNotifyHighResOff += new CIND890APIClient.delegateOnNotifyHighResOff(m_APITerminalClient_OnNotifyHighResOff);
                //@MTTS-CTA, 09-09-2014
                m_APIDIOClient.DiscreteIO.OnDIOInput += DiscreteIO_OnDIOInput;

                InitiateInterface();
                dataGridAlibi.DataSource = m_dtAlibi;

                cmbIntefaceNo.SelectedIndex = 0;
                cmbTemplateNo.SelectedIndex = 0;
                cmbValue.SelectedIndex = 0;
                cmbWndElemnts.SelectedIndex = 0;
                cmbWndElmnt.SelectedIndex = 0;
                cmbDockStle.SelectedIndex = 0;
                cmbDockStyle.SelectedIndex = 0;
                cmbLocation.SelectedIndex = 0;
                cmbPortNo.SelectedIndex = 0;
                cmbClearHistory.SelectedIndex = 0;
                cmbAppMode.SelectedIndex = 0;
                cmbShutdownType.SelectedIndex = 1;
                m_ThreadTimer.Change(0, constGETGNTCALLRATE);
            }
        }

        void m_APITerminalClient_OnSetupExited()
        {
            //@@MTTS-JRK2 v1.3.6 RC9 06/Jul/2017 5852- start the scale only if Scale tab
            //@@MTTS-JRK2 v1.25 5778 - Start the GetGNT calls After returning from Setup screen
            //StartScale();
            if (tabControl1.SelectedIndex == 0) 
                StartScale();
        }

        void m_APIScaleClient_OnSendFactResult(byte factResult)
        {
            MessageBox.Show("Fact Result : " + ((CScale.enumSendFactResult)factResult));
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            //@@MTTS-JRK2 v1.3.2a 07/May/2015, API V1.15 - If Base pac is already shutdown, do not display messagebox. Simply close the application
            if (!m_ShutDownSystem)
            {
                m_eKeypadType = enumKeypadType.QUIT_MESSAGEBOX;
                m_APITerminalClient.InvokeMessageBox(constAPPLICATION, "Do you want to quit the application?", CIND890APIClient.enumMsgBoxButton.YESNO, CIND890APIClient.enumMsgBoxIcon.QUESTION);
                //Handle the result inside onKeyPadresult event
            }
            else
            {
                CloseTestApplication();
            }
        }
        #endregion

        #region Interface
        private void btnInterfaceCmdWrite_Click(object sender, EventArgs e)
        {
            if (cmbIntefaceNo.SelectedIndex <= -1)
            {
                MessageBox.Show("Please Select Interface Number !");
                return;
            }
            //MTTS-HARI - v1.3.6A - 2/8/2017 - #5875 - Equip IND890 API with a 4th channel for 'high speed' serial communication.
            //Change the Interface communications from Terminal client to Serial client
            //MTTS-HARI v1.3.3a 19/11/2015 #5072 Send String to GA46 from API
            m_APISerialClient.Interface[(cmbIntefaceNo.SelectedIndex + 1)].SendString(txtDatToSend.Text.ToString());

            //@MTTS-CTA ,v1.2.3e ,05-03-2014 ,Interface data exchange from API must be encoded / decoded with current language dependent codepages 
            //byte[] bData = Encoding.ASCII.GetBytes(txtDatToSend.Text.ToString());
            //m_APITerminalClient.Interface[(cmbIntefaceNo.SelectedIndex + 1)].SendBytes(bData, txtDatToSend.Text.ToString().Length);            
            //byte[] bData = m_APITerminalClient.CurrentAPIEncoding.GetBytes(txtDatToSend.Text.ToString());
            //m_APITerminalClient.Interface[(cmbIntefaceNo.SelectedIndex + 1)].SendBytes(bData, bData.Length);
        }

        //MTTS-HARI v1.3.3a 19/11/2015 #5072 Send Bytes to GA46 from API
        private void btnInterfaceCmdWriteByte_Click(object sender, EventArgs e)
        {
            if (cmbIntefaceNo.SelectedIndex <= -1)
            {
                MessageBox.Show("Please Select Interface Number !");
                return;
            }
            //MTTS-HARI - v1.3.6A - 2/8/2017 - #5875 - Equip IND890 API with a 4th channel for 'high speed' serial communication.
            //Change the Interface communications from Terminal client to Serial client
            byte[] bData = m_APISerialClient.CurrentAPIEncoding.GetBytes(txtDatToSend.Text.ToString());
            m_APISerialClient.Interface[(cmbIntefaceNo.SelectedIndex + 1)].SendBytes(bData, bData.Length);
        }

        private void btnSetDataMode_Click(object sender, EventArgs e)
        {
            if (cmbIntefaceNo.SelectedIndex <= -1)
            {
                MessageBox.Show("Please Select Interface Number !");
                return;
            }
            //MTTS-HARI - v1.3.6A - 2/8/2017 - #5875 - Equip IND890 API with a 4th channel for 'high speed' serial communication.
            //Change the Interface communications from Terminal client to Serial client
            if (m_APISerialClient.Interface[(cmbIntefaceNo.SelectedIndex + 1)].DataMode == CInterface.enumDataMode.MODE_STRING)
                m_APISerialClient.Interface[(cmbIntefaceNo.SelectedIndex + 1)].DataMode = CInterface.enumDataMode.MODE_BINARY;
            else
                m_APISerialClient.Interface[(cmbIntefaceNo.SelectedIndex + 1)].DataMode = CInterface.enumDataMode.MODE_STRING;
        }

        private void btnGetDataMode_Click(object sender, EventArgs e)
        {
            if (cmbIntefaceNo.SelectedIndex <= -1)
            {
                MessageBox.Show("Please Select Interface Number !");
                return;
            }
            //MTTS-HARI - v1.3.6A - 2/8/2017 - #5875 - Equip IND890 API with a 4th channel for 'high speed' serial communication.
            //Change the Interface communications from Terminal client to Serial client
            SetText(statusAPITest, m_APISerialClient.Interface[(cmbIntefaceNo.SelectedIndex + 1)].DataMode.ToString());
        }
        #endregion

        #region Scale
        private void GetWeight()
        {
            //MTTS-HARI - v1.3.7c - 27/9/2017 - #5795 - Equip API with a function to trigger Scale FACT 2/2.
            //m_APIScaleClient.CurrentScale.GetGNTInPrimaryUnit(ref m_ScaleWeight);
            m_APIWeighingClient.CurrentScale.GetGNTInPrimaryUnit(ref m_ScaleWeight);
            SetText(lblGross, m_ScaleWeight.GrossWeight.ToString());
            SetText(lblNet, m_ScaleWeight.NetWeight.ToString());
            SetText(lblTare, m_ScaleWeight.TareWeight.ToString());
        }

        private void StopScale()
        {
            m_ThreadTimer.Change(Timeout.Infinite, Timeout.Infinite);
            IsTimerEnabled = false;
        }

        private void StartScale()
        {
            IsTimerEnabled = true;
            m_ThreadTimer.Change(0, constGETGNTCALLRATE);
        }

        delegate void SetTextDelegate(Control senderControl, string strText);
        private void SetText(Control sender, string str)
        {
            try
            {
                    if (this.InvokeRequired)
                    {
                        SetTextDelegate SetTextEvent = new SetTextDelegate(SetText);
                        this.Invoke(SetTextEvent, new object[] { sender, str });
                    }
                    else
                    {
                        sender.Text = str;
                    }
            }
            catch { }
        }

        private void butStart_Click(object sender, EventArgs e)
        {
            if (!IsTimerEnabled)
            {
                butStart.Text = "Stop Scale";
                StartScale();

            }
            else
            {
                StopScale();
                butStart.Text = "Start Scale";
            }
        }

        private void btnGross_Click(object sender, EventArgs e)
        {
            SetText(lbl2Gross, m_APIScaleClient.CurrentScale.GrossWeight.Weight.ToString());
            SetText(statusAPITest, "Gross Weight");
        }

        private void btnNet_Click(object sender, EventArgs e)
        {
            SetText(lbl2Net, m_APIScaleClient.CurrentScale.NetWeight.Weight.ToString());
            SetText(statusAPITest, "Net Weight");
        }

        private void btn2Tare_Click(object sender, EventArgs e)
        {
            SetText(lbl2Tare, m_APIScaleClient.CurrentScale.TareWeight.Weight.ToString());
            SetText(statusAPITest, "Tare Weight");
        }

        private void btnNetMode_Click(object sender, EventArgs e)
        {
            SetText(lblInNetMode, m_APIScaleClient.CurrentScale.InNetMode.ToString());
            SetText(statusAPITest, "Net Mode");
        }

        private void btnAtZero_Click(object sender, EventArgs e)
        {
            SetText(lblAtZer, m_APIScaleClient.CurrentScale.AtZero.ToString());
            SetText(statusAPITest, "At Zero");
        }

        private void btnTareRslt_Click(object sender, EventArgs e)
        {
            SetText(lblTareRslt, m_APIScaleClient.CurrentScale.TareResult.ToString());
            SetText(statusAPITest, "Tare Result");
        }

        private void btnZeroRslt_Click(object sender, EventArgs e)
        {
            SetText(lblZeroRslt, m_APIScaleClient.CurrentScale.ZeroResult.ToString());
            SetText(statusAPITest, "Zero Result");
        }

        private void btnZero_Click(object sender, EventArgs e)
        {
            //MTTS-HARI - v1.3.7c - 27/9/2017 - #5795 - Equip API with a function to trigger Scale FACT 2/2. GNT always runs in 5th client.
            //StopScale();
            CScale.enumZeroResult zeroRslt = m_APIScaleClient.CurrentScale.PerformZero();
            SetText(statusAPITest, zeroRslt.ToString());
            //MTTS-HARI - v1.3.7c - 27/9/2017 - #5795 - Equip API with a function to trigger Scale FACT 2/2. GNT always runs in 5th client.
            //StartScale();
        }
        //void m_APIScaleClient_OnScaleZeroed(byte zeroResult)
        //{
        //    StartScale();
        //    //MessageBox.Show("Zero Result : " + ((CScale.enumZeroResult)zeroResult));
        //    SetText(statusAPITest, ((CScale.enumZeroResult)zeroResult).ToString());
        //}
        private void btnTare_Click(object sender, EventArgs e)
        {
            //MTTS-HARI - v1.3.7c - 27/9/2017 - #5795 - Equip API with a function to trigger Scale FACT 2/2. GNT always runs in 5th client.
            //StopScale();
            CScale.enumTareResult tareResult = m_APIScaleClient.CurrentScale.PerformTare();
            SetText(statusAPITest, tareResult.ToString());
            //MTTS-HARI - v1.3.7c - 27/9/2017 - #5795 - Equip API with a function to trigger Scale FACT 2/2. GNT always runs in 5th client.
            //StartScale();
        }
        private void btnPreTare_Click(object sender, EventArgs e)
        {
            //MTTS-HARI - v1.3.7c - 27/9/2017 - #5795 - Equip API with a function to trigger Scale FACT 2/2. GNT always runs in 5th client.
            //StopScale();
            m_eKeypadType = enumKeypadType.PRETARE;
            m_APITerminalClient.InvokeNumericKeypad(CIND890APIScaleClient.enumKeypadType.WEIGHT, "0 Kg");
            SetText(statusAPITest, "PreTare Function()");
        }
        void m_APIScaleClient_OnScaleTared(byte tareResult, byte tareType)
        {
            //MTTS-HARI - v1.3.7c - 27/9/2017 - #5795 - Equip API with a function to trigger Scale FACT 2/2. GNT always runs in 5th client.
            //StartScale();
             MessageBox.Show("Tare Result : " + ((CScale.enumTareResult)tareResult));
            //SetText(statusAPITest, ((CScale.enumTareResult)tareResult).ToString());
            //MessageBox.Show(string.Format("Tare Result : {0}; Type : {1}", ((CScale.enumTareResult)tareResult).ToString(), tareType.ToString()));
        }

        void m_APITerminalClient_OnGetKeypadResult(byte returnCode, string value)
        {
            //Set Second Unit
            switch (m_eKeypadType)
            {
                case enumKeypadType.NONE:
                    break;
                case enumKeypadType.ALPHA_NUMERIC:
                case enumKeypadType.NUMERIC:
                    if ((DialogResult)(enumDialogResult)returnCode == DialogResult.OK)
                    {
                        AddItemToList(value);
                        m_AlphaNumericInput = value;
                    }
                    break;
                case enumKeypadType.PRETARE:
                    if ((DialogResult)(enumDialogResult)returnCode == DialogResult.OK)
                    {
                        CScale.enumTareResult tareResult = m_APIScaleClient.CurrentScale.PerformTare(new CWeight(value));
                        SetText(statusAPITest, tareResult.ToString());
                    }
                    StartScale();
                    break;
                //@MTTS-CTA ,22-05-2013 #4072
                case enumKeypadType.BROWSE_DIALOG:
                    if ((DialogResult)(enumDialogResult)returnCode == DialogResult.OK)
                    {
                        AddItemToList(value);
                        m_BrowseDialogInput = value;
                    }
                    break;
                case enumKeypadType.SET_UNIT:
                    if ((DialogResult)(enumDialogResult)returnCode == DialogResult.OK)
                    {
                        SetText(statusAPITest, String.Format("Set Second Unit Result : {0} ", m_APIScaleClient.CurrentScale.SetSecondUnit((byte)Int32.Parse(value))));
                    }
                    break;
                case enumKeypadType.QUIT_MESSAGEBOX:
                    if (m_ShutDownSystem)
                    {
                        bool boolSD = (DialogResult)(enumDialogResult)returnCode == DialogResult.Yes;
                        bool result = false;
                        if (boolSD)
                        {
                            //@@MTTS-JRK2 v1.3.2a 07/May/2015 #5387 - allow user to select shutdown type
                            //result = m_APITerminalClient.Terminal.ShutDownSystem(CTerminal.enumApplicationExitType.Shutdown);
                            result = m_APITerminalClient.Terminal.ShutDownSystem((CTerminal.enumApplicationExitType)m_ShutDownType);
                        }
                        else
                        {
                            m_ShutDownSystem = false;
                        }
                        SetText(statusAPITest, (string.Format("System ShutDown : {0} , Result : {1}", boolSD.ToString(), result.ToString())));
                    }
                    else if ((DialogResult)(enumDialogResult)returnCode == DialogResult.Yes)
                        CloseTestApplication();
                    break;
                default:
                    break;
            }
            m_eKeypadType = enumKeypadType.NONE;
        }
        delegate void delegateFormClose();
        private void CloseTestApplication()
        {
            if (this.InvokeRequired)
            {
                delegateFormClose del = new delegateFormClose(CloseTestApplication);
                this.Invoke(del);
            }
            else
                this.Close();
        }

        delegate void delegateAddItemToList(string value);
        private void AddItemToList(string value)
        {
            if (lst1.InvokeRequired)
            {
                delegateAddItemToList del = new delegateAddItemToList(AddItemToList);
                lst1.Invoke(del, new object[] { value });
            }
            else

                lst1.Items.Add(value);
        }

        delegate void delegateClearListItems();
        private void ClearListItem()
        {
            if (lst1.InvokeRequired)
            {
                delegateClearListItems del = new delegateClearListItems(ClearListItem);
                lst1.Invoke(del);
            }
            else
                lst1.Items.Clear();
        }



        private void btnSwitchToScale_Click(object sender, EventArgs e)
        {
            int iScaleID = 0;
            CScale.enumScale ScaleID;
            if (cmbBoxScaleNr.SelectedIndex < 0 && string.IsNullOrEmpty(cmbBoxScaleNr.Text))
                {
                    MessageBox.Show("Enter a valid scale number");
                    return;
                }
            else
                ScaleID = (CScale.enumScale)cmbBoxScaleNr.SelectedIndex; 
            if (!string.IsNullOrEmpty(cmbBoxScaleNr.Text))
            {
                try
                {
                    iScaleID = Convert.ToInt32(cmbBoxScaleNr.Text);
                    if (iScaleID > 0 && iScaleID < 6)
                        ScaleID = (CScale.enumScale)iScaleID;
                    else
                    {
                        MessageBox.Show("Enter a valid scale number");
                        return;
                    }
                }
                catch
                {
                    MessageBox.Show("Enter a valid scale number");
                    return;
                }
            }
            StopScale();
            CScale.enumSwitchScaleResult switchScaleResult = m_APIScaleClient.CurrentScale.PerformSwitchScale(ScaleID);
            SetText(statusAPITest, switchScaleResult.ToString());
            StartScale();
        }
        
        //Mtts-Hari - 7/7/2015 - v1.3.2b #5360 Activate High Resolution from API
        private void btnActivateHighRes_Click(object sender, EventArgs e)
        {
            bool highresResult = m_APIScaleClient.CurrentScale.ActivateHighRes(true);
            SetText(statusAPITest, highresResult.ToString());
        }

        //Mtts-Hari - 7/7/2015 - v1.3.2b #5360 Activate High Resolution from API
        private void btnDeActivateHighRes_Click(object sender, EventArgs e)
        {
            bool highresResult = m_APIScaleClient.CurrentScale.ActivateHighRes(false);
            SetText(statusAPITest, highresResult.ToString());
        }

        private void btnSwitchScale_Click(object sender, EventArgs e)
        {
            StopScale();

            CScale.enumSwitchScaleResult switchScaleResult = m_APIScaleClient.CurrentScale.PerformSwitchScale();
            //CScale.enumSwitchScaleResult switchScaleResult = m_APIScaleClient.CurrentScale.PerformSwitchScale();
            //m_APITerminalClient.InvokeNumericKeypad(CIND890APIScaleClient.enumKeypadType.NUMERIC,"Scale Id");            
            //m_APIScaleClient.Scale[1].PerformSwitchScale();
            SetText(statusAPITest, switchScaleResult.ToString());
            StartScale();
            //SetText(statusAPITest, String.Format("{0} : Prev Scale : {1} CrntScale : {2}", ((CScale.enumSwitchScaleResult)result).ToString(), oldScaleNumber, newScaleNumber));
        }
        void m_APIScaleClient_OnSwitchScale(byte result, byte oldScaleNumber, byte newScaleNumber)
        {
            StartScale();
            SetText(statusAPITest, String.Format("{0} : Prev Scale : {1} CrntScale : {2}", ((CScale.enumSwitchScaleResult)result).ToString(), oldScaleNumber, newScaleNumber));
        }

        private void btnSwitchUnit_Click(object sender, EventArgs e)
        {
            //MTTS-HARI - v1.3.7c - 27/9/2017 - #5795 - Equip API with a function to trigger Scale FACT 2/2. GNT always runs in 5th client.
            //StopScale();
            SetText(statusAPITest, String.Format("Switch Unit Result : {0} ", m_APIScaleClient.CurrentScale.PerformSwitchUnit()));
            //MTTS-HARI - v1.3.7c - 27/9/2017 - #5795 - Equip API with a function to trigger Scale FACT 2/2. GNT always runs in 5th client.
            //StartScale();
        }
        private void btnGet2UUnit_Click(object sender, EventArgs e)
        {
            SetText(statusAPITest, String.Format("Get Second Unit Result : {0} ", m_APIScaleClient.CurrentScale.GetSecondUnit()));
        }

        private void btnSet2UUnit_Click(object sender, EventArgs e)
        {
            m_eKeypadType = enumKeypadType.SET_UNIT;
            m_APITerminalClient.InvokeNumericKeypad(CIND890APIClient.enumKeypadType.NUMERIC, "Unit Code");
        }
        #endregion

        #region Terminal
        private void btnTerminalId1_Click(object sender, EventArgs e)
        {
            string TerminalID = m_APITerminalClient.Terminal.TerminalID1;
            AddItemToList(string.Format("Terminal ID1 : {0}", TerminalID));
            SetText(statusAPITest, "TerminalID1");
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearListItem();
            SetText(statusAPITest, "List Cleared");
        }

        private void btnTerminalId2_Click(object sender, EventArgs e)
        {
            string TerminalID = m_APITerminalClient.Terminal.TerminalID2;
            AddItemToList(string.Format("Terminal ID2 : {0}", TerminalID));
            SetText(statusAPITest, "TerminalID2");
        }

        private void btnTerminalId3_Click(object sender, EventArgs e)
        {
            string TerminalID = m_APITerminalClient.Terminal.TerminalID3;
            AddItemToList(string.Format("Terminal ID3 : {0}", TerminalID));
            SetText(statusAPITest, "TerminalID3");
        }
        private void btnSerialNo_Click(object sender, EventArgs e)
        {
            string serialNo = m_APITerminalClient.Terminal.SerialNumber;
            AddItemToList(string.Format("Serial Number : {0}", serialNo));
            SetText(statusAPITest, "SerialNumber");
        }

        private void btnUserName_Click(object sender, EventArgs e)
        {
            string sUsrName = m_APITerminalClient.Terminal.UserName;
            AddItemToList(string.Format("User Name : {0}", sUsrName));
        }

        private void btnUsrType_Click(object sender, EventArgs e)
        {
            string sUsrType = m_APITerminalClient.Terminal.UserType;
            AddItemToList(string.Format("User Type : {0}", sUsrType));
            SetText(statusAPITest, "UserType");
        }

        private void btnTotalScale_Click(object sender, EventArgs e)
        {
            string sTotalScales = m_APITerminalClient.Terminal.TotalScales();
            AddItemToList(string.Format("Total Scale(s) : {0}", sTotalScales));
            SetText(statusAPITest, "TotalScales()");
        }

        private void btnAmtScale_Click(object sender, EventArgs e)
        {
            int sTotalScales;
            string sAmtScales;
            m_APITerminalClient.Terminal.AmountScale(out sTotalScales, out sAmtScales);

            AddItemToList((string.Format("Total Scales(s) : {0}Scale Numbers : {1}", sTotalScales, sAmtScales)));
            SetText(statusAPITest, "AmountScale()");
        }
        private void btnAlphaNumKeypad_Click(object sender, EventArgs e)
        {
            m_APITerminalClient.InvokeAlphaNumericKeypad(m_AlphaNumericInput, constAPPLICATION);
            m_eKeypadType = enumKeypadType.ALPHA_NUMERIC;
        }
        #endregion

        #region Info
        private void btnTerminalInfo_Click(object sender, EventArgs e)
        {
            CTerminalInfo terminalinfo = new CTerminalInfo();
            terminalinfo = m_APITerminalClient.Terminal.TerminalData();

            AddItemToList(string.Format("APIServer Version : {0}", terminalinfo.APIServerVersion));
            AddItemToList(string.Format("APIClient Version : {0}", CTerminalInfo.APIClientVersion));
            AddItemToList(string.Format("Application Version : {0}", terminalinfo.ApplicationVersion));
            AddItemToList(string.Format("BIOS Version : {0}", terminalinfo.BiosVersion));
            AddItemToList(string.Format("CommServer Version :  {0}", terminalinfo.CommServerVersion));
            AddItemToList(string.Format("ScaleServer Version :  {0}", terminalinfo.ScaleServerVersion));
            AddItemToList(string.Format("Serial Number :  {0}", terminalinfo.SerialNumber));
            //MTTS-HARI - 28/6/2016 - v1.3.4d #5606 Show Bootservice and System Service version in Terminal Info
            AddItemToList(string.Format("BootService Version : {0}", terminalinfo.BootServiceVersion));
            AddItemToList(string.Format("SystemServices Version : {0}", terminalinfo.SystemServiceVersion));

            SetText(statusAPITest, "TerminalInfo()");
        }

        private void btnScaleInfo_Click(object sender, EventArgs e)
        {
            CScaleInfo scaleInfo = new CScaleInfo();
            m_APIScaleClient.GetScaleInfo(ref scaleInfo, 0);
            AddItemToList(string.Format("UpdateRateAdjValues : {0}", scaleInfo.UpdateRateAdjValues));
            AddItemToList(string.Format("UpdateRate : {0}", scaleInfo.UpdateRate));
            AddItemToList(string.Format("AppOfWeighingplatform : {0}", scaleInfo.AppOfWeighingplatform));
            AddItemToList(string.Format("PlaceOfUseCountry : {0}", scaleInfo.PlaceOfUseCountry));
            AddItemToList(string.Format("Approval : {0}", scaleInfo.Approval));
            AddItemToList(string.Format("CurrentLanguage : {0}", scaleInfo.CurrentLanguage));
            AddItemToList(string.Format("AvailableLanguage : {0}", scaleInfo.AvailableLanguage));
            AddItemToList(string.Format("Unit : {0}", scaleInfo.Unit));
            AddItemToList(string.Format("ConversionAllowed : {0}", scaleInfo.ConversionAllowed));
            AddItemToList(string.Format("ScaleType : {0}", scaleInfo.ScaleType));
            AddItemToList(string.Format("MaxCapacity : {0}", scaleInfo.MaxCapacity));
            AddItemToList(string.Format("MaxCapacityUnit : {0}", scaleInfo.MaxCapacityUnit));
            AddItemToList(string.Format("MinCapacity : {0}", scaleInfo.MinCapacity));
            AddItemToList(string.Format("MinCapacityUnit : {0}", scaleInfo.MinCapacityUnit));
            AddItemToList(string.Format("MaxTareCompensation : {0}", scaleInfo.MaxTareCompensation));
            AddItemToList(string.Format("MaxTareCompUnit : {0}", scaleInfo.MaxTareCompUnit));
            AddItemToList(string.Format("MaxPreSetTare : {0}", scaleInfo.MaxPreSetTare));
            AddItemToList(string.Format("MaxPreSetTareUnit : {0}", scaleInfo.MaxPreSetTareUnit));
            AddItemToList(string.Format("Range0 : {0}", scaleInfo.Range0));
            AddItemToList(string.Format("Range0Unit : {0}", scaleInfo.Range0Unit));
            AddItemToList(string.Format("Range0Res : {0}", scaleInfo.Range0Res));
            AddItemToList(string.Format("Range0ResUnit : {0}", scaleInfo.Range0ResUnit));
            AddItemToList(string.Format("Range1 : {0}", scaleInfo.Range1));
            AddItemToList(string.Format("Range1Unit : {0}", scaleInfo.Range1Unit));
            AddItemToList(string.Format("Range1Res : {0}", scaleInfo.Range1Res));
            AddItemToList(string.Format("Range1ResUnit : {0}", scaleInfo.Range1ResUnit));
            AddItemToList(string.Format("Range2 : {0}", scaleInfo.Range2));
            AddItemToList(string.Format("Range2Unit : {0}", scaleInfo.Range2Unit));
            AddItemToList(string.Format("Range2Res : {0}", scaleInfo.Range2Res));
            AddItemToList(string.Format("Range2ResUnit : {0}", scaleInfo.Range2ResUnit));
            AddItemToList(string.Format("Range3 : {0}", scaleInfo.Range3));
            AddItemToList(string.Format("Range3Unit : {0}", scaleInfo.Range3Unit));
            AddItemToList(string.Format("Range3Res : {0}", scaleInfo.Range3Res));
            AddItemToList(string.Format("Range3ResUnit : {0}", scaleInfo.Range3ResUnit));
            AddItemToList(string.Format("Verification : {0}", scaleInfo.Verification));
            AddItemToList(string.Format("MinReprod : {0}", scaleInfo.MinReprod));
            AddItemToList(string.Format("MinReprodUnit : {0}", scaleInfo.MinReprodUnit));
            AddItemToList(string.Format("ZeroValueAtPowerUp : {0}", scaleInfo.ZeroValueAtPowerUp));
            AddItemToList(string.Format("VA : {0}", scaleInfo.VA));
            AddItemToList(string.Format("VAAdjustableValues : {0}", scaleInfo.VAAdjustableValues));
            AddItemToList(string.Format("WPA : {0}", scaleInfo.WPA));
            AddItemToList(string.Format("WPAAdjustableValues : {0}", scaleInfo.WPAAdjustableValues));
            AddItemToList(string.Format("ASD : {0}", scaleInfo.ASD));
            AddItemToList(string.Format("ASDAdjustableValues : {0}", scaleInfo.ASDAdjustableValues));
            AddItemToList(string.Format("AutoZeroPossible : {0}", scaleInfo.AutoZeroPossible));
            AddItemToList(string.Format("AutoZeroOnOff : {0}", scaleInfo.AutoZeroOn));
            AddItemToList(string.Format("RestartPossible : {0}", scaleInfo.RestartPossible));
            AddItemToList(string.Format("RestartOnOff : {0}", scaleInfo.RestartOn));
            AddItemToList(string.Format("SoftwareNr : {0}", scaleInfo.SoftwareNr));
            AddItemToList(string.Format("IdentCode : {0}", scaleInfo.IdentCode));
            AddItemToList(string.Format("AutoTarePossible : {0}", scaleInfo.AutoTarePossible));
            //AddItemToList(string.Format("AutoZeroPossible : {0}", scaleInfo.AutoZeroPossible)); //MTTS-JRK2 already added
            AddItemToList(string.Format("AutoTareOnOff : {0}", scaleInfo.AutoTareOn));
            AddItemToList(string.Format("SecondUnit : {0}", scaleInfo.SecondUnit));
            AddItemToList(string.Format("AvailableSecondUnit : {0}", scaleInfo.AvailableSecondUnit));
            //@MTTS -CTA   19 April 2012   V1.1.7, Adding Serial Number info to the ScaleInfo details #3300 . 
            AddItemToList(string.Format("SerialNumber : {0}", scaleInfo.SerialNumber));

            SetText(statusAPITest, "ScaleInfo() of Current scale");
        }

        private void btnInterfaceInfo_Click(object sender, EventArgs e)
        {
            CInterfaceInfo interfaceInfo = new CInterfaceInfo();
            //Mtts-Hari - 12/8/2015 - v1.3.2d Interface Info is alwayes displayed for X1.
            for (int i = 1; i <= 6; i++)
            {
                interfaceInfo = m_APITerminalClient.Interface[i].InterfaceData();
                AddItemToList("X" + i.ToString() + " : ");  
                AddItemToList(string.Format("Interface Type : {0}", interfaceInfo.InterfaceType.ToString()));
                //@MTTS -CTA   19 April 2012   V1.1.7, Removinf Serial Number info to the InterfaceInfo details #3300 . 
                //AddItemToList(string.Format("Serial Number : {0}", interfaceInfo.SerialNumber.ToString()));
                AddItemToList(string.Format("Software Version : {0}", interfaceInfo.SoftwareVersion.ToString()));
            }
            SetText(statusAPITest, "InterfaceInfo() of EtherNet");

        }
        #endregion

        #region Terminal-Events
        private void btnBeep_Click(object sender, EventArgs e)
        {
            m_APITerminalClient.Terminal.Beep(1000);
            SetText(statusAPITest, "Beep()");
        }

        bool bHide = true;
        private void btnHide_Click(object sender, EventArgs e)
        {
            m_APITerminalClient.Terminal.HideWeightWindow = bHide;
            bHide = !bHide;
            SetText(statusAPITest, "HideWeightWindow");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Current Scale Index :" + m_APITerminalClient.Terminal.CurrentScaleIndex.ToString());
            SetText(statusAPITest, "Current Scale Index");
        }

        private void btnWindowStatus_Click(object sender, EventArgs e)
        {
            SetText(statusAPITest, "Window Display Status");
            MessageBox.Show("Weightwindow Display status :" + m_APITerminalClient.Terminal.WeightWindowStatus.ToString());
        }

        private void btmWMStatus_Click(object sender, EventArgs e)
        {
            AddItemToList(string.Format("WM Approval Status : {0}", m_APITerminalClient.Terminal.WMApprovalMode.ToString()));
            SetText(statusAPITest, "WM Status");
        }
        #endregion

        #region APPBlock
        private void btnAppRead_Click(object sender, EventArgs e)
        {
            int iAppBlock = -1;
            try
            {
                iAppBlock = Int32.Parse(txtAppBlockNo.Text.ToString());
            }
            catch
            {
                MessageBox.Show("Please enter valid block number");
                return;
            }
            int iSubBlock = 0;
            int iExtBlock = 0;
            if (!string.IsNullOrEmpty(txtSubBlckNo.Text))
                iSubBlock = Int32.Parse(txtSubBlckNo.Text.ToString());

            if (!string.IsNullOrEmpty(txtExtBlckNo.Text))
                iExtBlock = Int32.Parse(txtExtBlckNo.Text.ToString());


            txtRslt.Text = m_APITerminalClient.Terminal.ReadAPPBlockEx(iAppBlock, iSubBlock, iExtBlock);
            SetText(statusAPITest, "ReadAPPBlock()");
        }
        private void btnAppWrite_Click(object sender, EventArgs e)
        {
            int iAppBlock = -1;
            try
            {
                iAppBlock = Int32.Parse(txtAppBlockNo.Text.ToString());
            }
            catch
            {
                MessageBox.Show("Please enter valid block number");
                return;
            }
            int iSubBlock = 0;
            int iExtBlock = 0;
            if (!string.IsNullOrEmpty(txtSubBlckNo.Text))
                iSubBlock = Int32.Parse(txtSubBlckNo.Text.ToString());
            if (!string.IsNullOrEmpty(txtExtBlckNo.Text))
                iExtBlock = Int32.Parse(txtExtBlckNo.Text.ToString());
            string sData = "";
            if (!string.IsNullOrEmpty(txtData.Text))
                sData = txtData.Text.ToString();

            txtRslt.Text = m_APITerminalClient.Terminal.WriteAPPBlockEx(iAppBlock, iSubBlock, iExtBlock, sData);
            SetText(statusAPITest, "WriteAPPBlock()");
        }
        #endregion

        #region DeltaTrac
        System.Windows.Forms.Timer m_Timer;
        double value;
        double incrVal;
        UIDeltaTrac.enumDeltaResult enumCurrStatus;

        void m_Timer_Tick(object sender, EventArgs e)
        {
            uiDeltaTrac1.CurrentValue = value;
            enumCurrStatus = uiDeltaTrac1.DeltaResult;
            SetText(statusAPITest, string.Format("DeltaResult ( {0} )                          Current Value ( {1} )", (uiDeltaTrac1.DeltaResult).ToString(), value.ToString()));
            value += incrVal;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                m_Timer.Enabled = true;
                SetText(statusAPITest, "Started...");
                uiDeltaTrac1.TargetValue = double.Parse(txtTarget.Text);
                if (chkMinTol.Checked)
                    uiDeltaTrac1.MinToleranceRate = int.Parse(txtTarget.Text);
                else
                    uiDeltaTrac1.MinToleranceValue = double.Parse(txtTarget.Text);

                if (chkMaxTol.Checked)
                    uiDeltaTrac1.MaxToleranceRate = int.Parse(txtMinTol.Text);
                else
                    uiDeltaTrac1.MaxToleranceValue = double.Parse(txtMinTol.Text);

                value = double.Parse(txtInitialVal.Text);
                incrVal = double.Parse(txtIncrement.Text);
                uiDeltaTrac1.DeltatracType = (MTTS.IND890.CE.UIDeltaTrac.enumDeltatracType)cmbDeltaType.SelectedIndex;
                uiDeltaTrac1.StartDeltaTrac();
            }
            catch
            {
                MessageBox.Show("Please enter valid input for all the input box.");
                return;
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            cmbDeltaType.SelectedIndex = 0;
            m_Timer.Enabled = false;
            SetText(statusAPITest, "Ready...");

            uiDeltaTrac1.TargetValue = 0;
            uiDeltaTrac1.MinToleranceValue = 0;
            uiDeltaTrac1.MaxToleranceValue = 0;
            uiDeltaTrac1.CurrentValue = 0;
            uiDeltaTrac1.MaxToleranceRate = 0;
            uiDeltaTrac1.MinToleranceRate = 0;
            value = 0;

            txtTarget.Text = "";
            txtMinTol.Text = "";
            txtMax.Text = "";
            cmbDeltaType.SelectedIndex = 0;
            txtInitialVal.Text = "";
            txtIncrement.Text = "";
        }
        #endregion

        #region GridView
        DataTable m_GridDataTable;
        private void CreateDataTable()
        {
            m_GridDataTable.Columns.Add("Name", typeof(string));
            m_GridDataTable.Columns.Add("AccessLevel", typeof(string));
            m_GridDataTable.Columns.Add("DefaultUser", typeof(string));
            m_GridDataTable.Columns.Add("LogOffTime", typeof(string));

            DataRow row1 = m_GridDataTable.NewRow();
            row1[0] = "Admin1";
            row1[1] = "Administrator";
            row1[2] = "Yes";
            row1[3] = "10 Mints";

            DataRow row2 = m_GridDataTable.NewRow();
            row2[0] = "Service";
            row2[1] = "Service";
            row2[2] = "No";
            row2[3] = "6 Mints";

            DataRow row3 = m_GridDataTable.NewRow();
            row3[0] = "Operator";
            row3[1] = "Operator";
            row3[2] = "No";
            row3[3] = "1 Mints";

            DataRow row4 = m_GridDataTable.NewRow();
            row4[0] = "Admin4";
            row4[1] = "Administrator";
            row4[2] = "No";
            row4[3] = "10 Mints";

            DataRow row5 = m_GridDataTable.NewRow();
            row5[0] = "Admin5";
            row5[1] = "Service";
            row5[2] = "Yes";
            row5[3] = "3 Mints";

            DataRow row6 = m_GridDataTable.NewRow();
            row6[0] = "Admin6";
            row6[1] = "Administrator";
            row6[2] = "No";
            row6[3] = "2 Mints";

            DataRow row7 = m_GridDataTable.NewRow();
            row7[0] = "Admin7";
            row7[1] = "Operator";
            row7[2] = "NO";
            row7[3] = "5 Mints";

            DataRow row8 = m_GridDataTable.NewRow();
            row8[0] = "Admin9";
            row8[1] = "Admin";
            row8[2] = "Yes";
            row8[3] = "5 Mints";

            DataRow row9 = m_GridDataTable.NewRow();
            row9[0] = "Admin10";
            row9[1] = "Administrator";
            row9[2] = "Yes";
            row9[3] = "10 Mints";

            DataRow row10 = m_GridDataTable.NewRow();
            row10[0] = "Admin11";
            row10[1] = "Administrator";
            row10[2] = "No";
            row10[3] = "1 Mints";

            DataRow row11 = m_GridDataTable.NewRow();
            row11[0] = "Operator";
            row11[1] = "Operator";
            row11[2] = "Yes";
            row11[3] = "10 Mints";

            DataRow row12 = m_GridDataTable.NewRow();
            row12[0] = "Admin13";
            row12[1] = "Administrator";
            row12[2] = "No";
            row12[3] = "5 Mints";

            DataRow row13 = m_GridDataTable.NewRow();
            row13[0] = "Service";
            row13[1] = "Service";
            row13[2] = "No";
            row13[3] = "20 Mints";

            m_GridDataTable.Rows.Add(row1);
            m_GridDataTable.Rows.Add(row2);
            m_GridDataTable.Rows.Add(row3);
            m_GridDataTable.Rows.Add(row4);
            m_GridDataTable.Rows.Add(row5);
            m_GridDataTable.Rows.Add(row6);
            m_GridDataTable.Rows.Add(row7);
            m_GridDataTable.Rows.Add(row8);
            m_GridDataTable.Rows.Add(row9);
            m_GridDataTable.Rows.Add(row10);
            m_GridDataTable.Rows.Add(row11);
            m_GridDataTable.Rows.Add(row12);
            m_GridDataTable.Rows.Add(row13);
        }
        #endregion

        #region DIO
        private void btnReadDIOOut_Click(object sender, EventArgs e)
        {
            //@MTTS-CTA ,V121f, Added portnumber to read DIOout 
            byte bLocation = (byte)1;
            byte bPort = (byte)0;
            bLocation = byte.Parse(cmbLocation.SelectedItem.ToString());
            bPort = byte.Parse(cmbPortNo.SelectedItem.ToString());

            //SetText(statusAPITest, string.Format("Read DIOOut Status : {0}", m_APITerminalClient.DiscreteIO.ReadDIOOutput(bPort, bLocation)));
            //@MTTS-CTA , 25-09-2014 ,#DIOCOmmunications 
            SetText(statusAPITest, string.Format("Read DIOOut Status : {0}", m_APIDIOClient.DiscreteIO.ReadDIOOutput(bPort, bLocation)));
        }

        private void btnReadDIOIn_Click(object sender, EventArgs e)
        {
            //@MTTS-CTA ,V121f, Added portnumber to read DIOin 
            byte bLocation = (byte)1;
            byte bPort = (byte)0;
            bLocation = byte.Parse(cmbLocation.SelectedItem.ToString());
            bPort = byte.Parse(cmbPortNo.SelectedItem.ToString());
            
            //SetText(statusAPITest, string.Format("Read DIOIn Status : {0}", m_APITerminalClient.DiscreteIO.ReadDIOInput(bPort, bLocation)));
            //@MTTS-CTA , 25-09-2014 ,#DIOCOmmunications 
            SetText(statusAPITest, string.Format("Read DIOIn Status : {0}", m_APIDIOClient.DiscreteIO.ReadDIOInput(bPort, bLocation)));

        }
        private void btnWrite_Click(object sender, EventArgs e)
        {
            byte bLocation = (byte)1;
            byte bPort = (byte)0;
            byte bValue = (byte)1;
            bLocation = byte.Parse(cmbLocation.SelectedItem.ToString());
            bPort = byte.Parse(cmbPortNo.SelectedItem.ToString());
            bValue = byte.Parse(cmbValue.SelectedItem.ToString());

            //bool bStatus = m_APITerminalClient.DiscreteIO.WriteToDIO(bLocation, bPort, bValue);
            //@MTTS-CTA , 25-09-2014 ,#DIOCOmmunications 
            bool bStatus = m_APIDIOClient.DiscreteIO.WriteToDIO(bLocation, bPort, bValue);

            SetText(statusAPITest, string.Format("Write To DIO Status : {0}", bStatus.ToString()));
        }  
          

        void DiscreteIO_OnDIOInput(byte location, byte port, byte value)
        {
            SetText(statusAPITest, string.Format("DIO Input Received - Location : {0} Port : {1} Value : {2}", location, port, value));
        }
        void m_APIScaleClient_OnBackLightTurnedOn()
        {
            MessageBox.Show("Backlight Turned On");
        }
        //MTTS-HARI 13/10/2015 v1.3.3A #5481 Notify API about automatic User logout.
        void m_APIScaleClient_OnNotifyUserLogout()
        {
            MessageBox.Show("User Logged Out");
        }
        //MTTS-HARI 14/10/2015 v1.3.3A #5480 Notify API if High Resolution is switched off automatically in Approved mode.
        void m_APIScaleClient_OnNotifyHighResOff()
        {
            MessageBox.Show("High Resolution Switched off");
        }
        void m_APITerminalClient_OnNotifyHighResOff()
        {
            MessageBox.Show("HIGH RESOLUTION SWITCHED OFF");
        }
        #endregion

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
   try
            {
                if (m_Timer != null) m_Timer.Enabled = false;
                StopScale();
                switch (tabControl1.SelectedIndex)
                {
                    //Scale
                    case 0:
                        //if (btnStart.Text.Equals("Stop Scale")) StartScale();
                        StartScale();
                        break;
                    //About
                    case 1:
                        break;
                    //AppBlock & Alibi
                    case 2:
                        break;
                    //DeltaTrac
                    case 3:
                        m_Timer = new System.Windows.Forms.Timer();
                        m_Timer.Interval = 1000;
                        m_Timer.Tick += new EventHandler(m_Timer_Tick);
                        enumCurrStatus = MTTS.IND890.CE.UIDeltaTrac.enumDeltaResult.ZERO;
                        cmbDeltaType.SelectedIndex = 0;
                        break;
                    //Grid Control
                    case 4:
                        m_GridDataTable = new DataTable("SampleTable");
                        CreateDataTable();
                        string formatXmlFile = "UserSettingsList.xml";
                        uiGridControl1.GridView.GridDataTable = m_GridDataTable;
                        //uiGridControl1.GridView.GridFormatXml = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase), formatXmlFile);
                        //uiGridControl1.GridView.GridFormatXml = Path.Combine(Application.StartupPath, formatXmlFile);
                        uiGridControl1.ShowGrid();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception :" + ex);
            }
        }

        private void btnPrintStr_Click(object sender, EventArgs e)
        {
            byte bConnPort;
            string sData = "";
            if (cmbIntefaceNo.SelectedIndex < 0)
            {
                MessageBox.Show("Please Enter Valid Interface No ");
                return;
            }
            if (!string.IsNullOrEmpty(txtDatToSend.Text.ToString()))
                sData = txtDatToSend.Text.ToString();

            bConnPort = (byte)(cmbIntefaceNo.SelectedIndex + 1);
            SetText(statusAPITest, string.Format("PrintString() Result : {0}", m_APITerminalClient.PrintString(sData, bConnPort)));
        }

        private void btnPrintTemp_Click(object sender, EventArgs e)
        {
            byte bTemplateNo;
            byte bConnPort;

            if (cmbIntefaceNo.SelectedIndex < 0 || cmbTemplateNo.SelectedIndex < 0)
            {
                MessageBox.Show("Please Enter Valid Interface No (or) Template No");
                return;
            }
            bConnPort = (byte)(cmbIntefaceNo.SelectedIndex + 1);
            bTemplateNo = (byte)(cmbTemplateNo.SelectedIndex + 1);
            SetText(statusAPITest, string.Format("PrintTemplate() Result : {0}", m_APITerminalClient.PrintTemplate(bTemplateNo, bConnPort)));
        }

        private void frmAPICalls_Closing(object sender, CancelEventArgs e)
        {
            //Mtts-Hari - 2/2/2015 - Object Disposed Exception on Closing
            IsTimerEnabled = false;
            if (m_ThreadTimer != null)
            {
                m_ThreadTimer.Change(Timeout.Infinite, Timeout.Infinite);
                m_ThreadTimer.Dispose();
                m_ThreadTimer = null;
            }
            //
            if (m_BacklightTimer != null)
            {
                m_BacklightTimer.Change(Timeout.Infinite, Timeout.Infinite);
                m_BacklightTimer.Dispose();
                m_BacklightTimer = null;
            }
            if (m_APIScaleClient != null) m_APIScaleClient.DisconnectFromAPIServer();
            if (m_APITerminalClient != null) m_APITerminalClient.DisconnectFromAPIServer();
            //@MTTS-CTA ,07-10-2014, Disconnecting DIOClient from the APIserver .
            if (m_APIDIOClient != null) m_APIDIOClient.DisconnectFromAPIServer();
            //MTTS-HARI - v1.3.6A - 2/8/2017 - #5875 - Equip IND890 API with a 4th channel for 'high speed' serial communication
            if (m_APISerialClient != null) m_APISerialClient.DisconnectFromAPIServer();
            //MTTS-HARI - v1.3.7c - 27/9/2017 - #5795 - Equip API with a function to trigger Scale FACT 2/2
            if (m_APIWeighingClient != null) m_APIWeighingClient.DisconnectFromAPIServer();
        }

        #region Weight Window Size
        private void cmbWndElmnt_SelectedIndexChanged(object sender, EventArgs e)
        {
            //string wnDElemnt = "";
            //if (!string.IsNullOrEmpty(cmbWndElemnts.SelectedItem.ToString()))
            //    wnDElemnt = cmbWndElemnts.SelectedItem.ToString();
            //lblWndElement.Text = wnDElemnt;
        }
        private void btnGetDockSytle_Click(object sender, EventArgs e)
        {
            enumDockStyle eDockStyle = (enumDockStyle)m_APITerminalClient.Terminal.DockingStyle;
            //AddItemToList(string.Format("Dock Style : {0}", eDockStyle.ToString()));
            SetText(statusAPITest, string.Format("Dock Style : {0}", eDockStyle.ToString()));
        }

        private void btnSetDock_Click(object sender, EventArgs e)
        {
            if (cmbDockStle.SelectedIndex < 0)
            {
                MessageBox.Show("Please Select Dockstyle");
                return;
            }
            m_APITerminalClient.Terminal.DockingStyle = cmbDockStle.SelectedIndex;
            SetText(statusAPITest, "Set DockStyle");
        }
        private void btnGetWndElmnt_Click(object sender, EventArgs e)
        {
            if (cmbWndElemnts.SelectedIndex < 0)
            {
                MessageBox.Show("Please Select WindowSize Elements");
                return;
            }
            int iValue = 0;
            switch (cmbWndElmnt.SelectedIndex)
            {
                //Top
                case 0:
                    iValue = m_APITerminalClient.Terminal.Top;
                    break;
                //Left
                case 1:
                    iValue = m_APITerminalClient.Terminal.Left;
                    break;
                //Width
                case 2:
                    iValue = m_APITerminalClient.Terminal.Width;
                    break;
                //Height
                case 3:
                    iValue = m_APITerminalClient.Terminal.Height;
                    break;
            }
            SetText(statusAPITest, (string.Format("{0} : {1}", cmbWndElmnt.SelectedItem.ToString(), iValue.ToString())));
        }

        private void btnSetWndElmnt_Click(object sender, EventArgs e)
        {
            if (cmbWndElemnts.SelectedIndex < 0)
            {
                MessageBox.Show("Please Select WindowSize Elements");
                return;
            }
            if (string.IsNullOrEmpty(txtWndSizeValue.Text.ToString()))
            {
                MessageBox.Show("Please Enter Value");
                return;
            }
            int iValue = Int32.Parse(txtWndSizeValue.Text.ToString());

            switch (cmbWndElmnt.SelectedIndex)
            {
                //Top
                case 0:
                    m_APITerminalClient.Terminal.Top = iValue;
                    break;
                //Left
                case 1:
                    m_APITerminalClient.Terminal.Left = iValue;
                    break;
                //Width
                case 2:
                    m_APITerminalClient.Terminal.Width = iValue;
                    break;
                //Height
                case 3:
                    m_APITerminalClient.Terminal.Height = iValue;
                    break;
            }
            SetText(statusAPITest, (string.Format("{0} : {1}", cmbWndElmnt.SelectedItem.ToString(), iValue.ToString())));
        }
        #endregion

        private void btnInvokeNumKeypad_Click(object sender, EventArgs e)
        {
            m_eKeypadType = enumKeypadType.NUMERIC;
            m_APITerminalClient.InvokeNumericKeypad(CIND890APIClient.enumKeypadType.NUMERIC, "Test Numeric Dialog");
        }

        private void btnInvokeBrowseDialog_Click(object sender, EventArgs e)
        {
            //@MTTS-CTA ,27-08-2013 , Default file name added for save dialog
            m_eKeypadType = enumKeypadType.BROWSE_DIALOG;
            string defaultPath = @"\Hard Disk2";
            string defaultFileName = "default";
            m_APITerminalClient.InvokeBrowseDialog("Folder Dialog", "", CIND890APIClient.enumDialogType.FolderDialog, defaultPath, defaultFileName);
        }

        private void btnSetClearHistory_Click(object sender, EventArgs e)
        {
            if (cmbClearHistory.SelectedIndex < 0)
            {
                MessageBox.Show("Please Select a Clear_History Option !");
                return;
            }
            m_APITerminalClient.Terminal.ClearHistoryList = (CTerminal.enumClearInputDialogHistory)(cmbClearHistory.SelectedIndex);
            SetText(statusAPITest, "Set ClearHistory ");
            AddItemToList(string.Format("Set ClearHistory for {0}", cmbClearHistory.SelectedItem));
        }
        /// <summary>
        /// Set password
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetPassword_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtIndAppPass.Text))
            {
                m_APITerminalClient.Terminal.DongleIndividualAppPassword = txtIndAppPass.Text;
                SetText(statusAPITest, (string.Format("Individual App Password : {0}", txtIndAppPass.Text)));
            }
        }

        private void btnGetIndAppPass_Click(object sender, EventArgs e)
        {
            MessageBox.Show(string.Format("Individual Application Password:{0}", m_APITerminalClient.Terminal.DongleIndividualAppPassword), "IND890APIClient");
        }

        private void btnAppModeGet_Click(object sender, EventArgs e)
        {
            SetText(statusAPITest, m_APITerminalClient.Terminal.ApplicationMode.ToString());
        }

        private void btnAppModeSet_Click(object sender, EventArgs e)
        {
            m_APITerminalClient.Terminal.ApplicationMode = (CTerminal.enumDisplayMode)cmbAppMode.SelectedIndex;
        }

        private void btnShutDown_Click(object sender, EventArgs e)
        {
            //07/May/2015 Base v1.3.2a API V1.15 changed for TTP#5387 - allow user to select shutdown type
            string msg = string.Empty;
            m_ShutDownType = (CTerminal.enumApplicationExitType)cmbShutdownType.SelectedIndex;
            switch (m_ShutDownType)
            {
                case CTerminal.enumApplicationExitType.Default:
                    msg = "You are about to close the IND890 Weighing applicaiton.Do you want to continue?";
                    break;
                case CTerminal.enumApplicationExitType.Shutdown:
                    msg = "You are about to shutdown the operating system.Do you want to continue?";
                    break;
                case CTerminal.enumApplicationExitType.Restart:
                    msg = "You are about to restart the operating system.Do you want to continue?";
                    break;
            }
            m_eKeypadType = enumKeypadType.QUIT_MESSAGEBOX;
            m_APITerminalClient.InvokeMessageBox(constAPPLICATION, msg, CIND890APIClient.enumMsgBoxButton.YESNO, CIND890APIClient.enumMsgBoxIcon.QUESTION);
            m_ShutDownSystem = true;
        }
        /// <summary>
        /// Validates the user Credentials 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCheckCredential_Click(object sender, EventArgs e)
        {
            string OperatorType = (cmbOperatorType.SelectedItem != null) ? cmbOperatorType.SelectedItem.ToString().ToLower() : "";
            //bool boolSD = m_APITerminalClient.Terminal.ValidateUser(txtUname.Text.ToLower(), txtPassword.Text, ref OperatorType);
            //Testing API without ToLower option from API
            bool boolSD = m_APITerminalClient.Terminal.ValidateUser(txtUname.Text, txtPassword.Text, ref OperatorType);
            if (boolSD)
                MessageBox.Show(string.Format("Crediential is valid - Operator Type : {0} , ", OperatorType));
            else
                MessageBox.Show("Invalid Credentials");
        }
        /// <summary>
        /// Loads Application Base  screen from API 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetup_Click(object sender, EventArgs e)
        {
            //@@MTTS-JRK2 v1.25 5778 - stop the GetGNT calls before entering Setup screen
            StopScale();
            m_APITerminalClient.Terminal.LoadSetupScreen();
        }

        /// <summary>
        /// Login Basepac
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLogin_Click(object sender, System.EventArgs e)
        {
            //Mtts-Hari - 10/8/2015 - v1.3.2d #5449 API call to Login Basepac.
            bool userLogged = m_APITerminalClient.Terminal.Login(txtUname.Text, txtPassword.Text);

             if (userLogged)
                 MessageBox.Show(string.Format("{0} Logged in.", txtUname.Text));
             else
                 MessageBox.Show("User Validation Failed.");
        }

        /// <summary>
        /// Logout Basepac
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLogout_Click(object sender, System.EventArgs e)
        {
            //Mtts-Hari - 19/8/2015 - v1.3.2d #5449 API call to validate User credentials before entering Setup.
            bool userLoggedOut = m_APITerminalClient.Terminal.Logout();

            if (userLoggedOut)
                MessageBox.Show("User Logged Out.");
            else
                MessageBox.Show("User Logout Failed.");
        }

        /// <summary>
        /// clears Credential Panel Values 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCredClear_Click(object sender, EventArgs e)
        {
            txtUname.Text = string.Empty;
            txtPassword.Text = string.Empty;
            cmbOperatorType.SelectedIndex = 0;
        }
        /// <summary>
        ///Turns the backlight off 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTurnOffBackLight_Click(object sender, EventArgs e)
        {
            m_APITerminalClient.Terminal.TurnOffBackLight();
        }

        private void btnKeypadLang_Click(object sender, EventArgs e)
        {
            CTerminal.enumKeypadSelectionLanguage lang = m_APITerminalClient.Terminal.KeypadSelectionLanguage;
            AddItemToList(string.Format("Keypad Lang : {0}", lang.ToString()));
            SetText(statusAPITest, "Keypad Selection Language");
        }

        private void btnDisplayLanguage_Click(object sender, EventArgs e)
        {
            CTerminal.enumDisplayMessageLanguage lang = m_APITerminalClient.Terminal.DisplayMessageLanguage;
            AddItemToList(string.Format("Display Lang : {0}", lang.ToString()));
            SetText(statusAPITest, "Display Message Language");
        }

        //MTTS-Hari : 10/12/2014 - FACT mode enable/disable
        bool bfact = true;
        private void btnFact_Click(object sender, EventArgs e)
        {
            //MTTS-HARI - v1.3.7c - 27/9/2017 - #5795 - Equip API with a function to trigger Scale FACT 2/2. GNT always runs in 5th client.
            //StopScale();
            CScale.enumFactModeResult factmodeRslt = m_APIScaleClient.CurrentScale.PerformFactModeEnableDisable(bfact);
            bfact = !bfact;
            SetText(statusAPITest, factmodeRslt.ToString());
            //MTTS-HARI - v1.3.7c - 27/9/2017 - #5795 - Equip API with a function to trigger Scale FACT 2/2. GNT always runs in 5th client.
            //StartScale();
            
        }

        private void button7_RB_All_Click(object sender, EventArgs e)  //@@@RB_27012915
        {
            //MTTS-HARI - v1.3.7c - 27/9/2017 - #5795 - Equip API with a function to trigger Scale FACT 2/2
            //m_APIScaleClient.Scale[1].GetGNTInPrimaryUnit(ref m_ScaleWeight);
            m_APIWeighingClient.Scale[1].GetGNTInPrimaryUnit(ref m_ScaleWeight);
            SetText(textBox1_RB_Scale1, m_ScaleWeight.GrossWeight.ToString());
            //m_APIScaleClient.Scale[2].GetGNTInPrimaryUnit(ref m_ScaleWeight);
            m_APIWeighingClient.Scale[2].GetGNTInPrimaryUnit(ref m_ScaleWeight);
            SetText(textBox1_RB_Scale2, m_ScaleWeight.GrossWeight.ToString());
            //m_APIScaleClient.Scale[3].GetGNTInPrimaryUnit(ref m_ScaleWeight);
            m_APIWeighingClient.Scale[3].GetGNTInPrimaryUnit(ref m_ScaleWeight);
            SetText(textBox1_RB_Scale3, m_ScaleWeight.GrossWeight.ToString());
            //m_APIScaleClient.Scale[4].GetGNTInPrimaryUnit(ref m_ScaleWeight);
            m_APIWeighingClient.Scale[4].GetGNTInPrimaryUnit(ref m_ScaleWeight);
            SetText(textBox1_RB_Scale4, m_ScaleWeight.GrossWeight.ToString());
        }

        private void button7_Click(object sender, EventArgs e)  //@@@RB_27012915
        {
            timer1_Parallel_Scale_Mode.Enabled = !timer1_Parallel_Scale_Mode.Enabled;
            checkBox1_Parallel_Automatic.Checked = !checkBox1_Parallel_Automatic.Checked;
        }

        private void timer1_Parallel_Scale_Mode_Tick(object sender, EventArgs e)
        {
            //MTTS-HARI - v1.3.7c - 27/9/2017 - #5795 - Equip API with a function to trigger Scale FACT 2/2
            //m_APIScaleClient.Scale[1].GetGNTInPrimaryUnit(ref m_ScaleWeight);
            m_APIWeighingClient.Scale[1].GetGNTInPrimaryUnit(ref m_ScaleWeight);
            SetText(textBox1_RB_Scale1, m_ScaleWeight.GrossWeight.ToString());
            //m_APIScaleClient.Scale[2].GetGNTInPrimaryUnit(ref m_ScaleWeight);
            m_APIWeighingClient.Scale[2].GetGNTInPrimaryUnit(ref m_ScaleWeight);
            SetText(textBox1_RB_Scale2, m_ScaleWeight.GrossWeight.ToString());
            //m_APIScaleClient.Scale[3].GetGNTInPrimaryUnit(ref m_ScaleWeight);
            m_APIWeighingClient.Scale[3].GetGNTInPrimaryUnit(ref m_ScaleWeight);
            SetText(textBox1_RB_Scale3, m_ScaleWeight.GrossWeight.ToString());
            //m_APIScaleClient.Scale[4].GetGNTInPrimaryUnit(ref m_ScaleWeight);
            m_APIWeighingClient.Scale[4].GetGNTInPrimaryUnit(ref m_ScaleWeight);
            SetText(textBox1_RB_Scale4, m_ScaleWeight.GrossWeight.ToString());
        }
        //MTTS-HARI 14/10/2015 v1.3.3A #5480 Create IsHighResAvailable property for API
        private void btnIsHighResAvailable_Click(object sender, EventArgs e)
        {
            bool flag = m_APIScaleClient.CurrentScale.IsHighResAvailable();
            AddItemToList(string.Format("High Resolution : {0}", (flag)?"Yes":"No"));
            SetText(statusAPITest, "High Resolution");
        }
        //MTTS_HARI 2/11/2015 v1.3.3a #5355 Allow API to update WW update timer.
        private void btnGetWWtimer_Click(object sender, EventArgs e)
        {
            SetText(txtWWtimer, m_APITerminalClient.Terminal.WeightWindowUpdateTimer.ToString());
            SetText(statusAPITest, "Get WW Timer");
        }
        //MTTS_HARI 2/11/2015 v1.3.3a #5355 Allow API to update WW update timer.
        private void btnSetWWtimer_Click(object sender, EventArgs e)
        {
            try
            {
                m_APITerminalClient.Terminal.WeightWindowUpdateTimer = Int32.Parse(txtWWtimer.Text);
                SetText(statusAPITest, "Set WW Timer");
            }
            catch
            {
                SetText(statusAPITest, "Set WW Timer Failed, Invalid Value");
            }
        }

        //private void btnTurnOnBackLight_Click(object sender, EventArgs e)
        //{
        //    //m_APITerminalClient.Terminal.TurnOnBackLight();
        //}

        private void btnStandby_Click(object sender, EventArgs e)
        {
            
            m_APITerminalClient.Terminal.ActivateStandBy();
        }
        //first turn the backlight Off and then after 15 seconds, invoke TurnOnBacklight()
        private void btnTurnOnBackLight_Click(object sender, EventArgs e)
        {
            m_APITerminalClient.Terminal.TurnOffBackLight();
            m_BacklightTimer.Change(15000, 0);
        }

        private void btnSendFact_Click(object sender, EventArgs e)
        {
            //MTTS-HARI - v1.3.7c - 27/9/2017 - #5795 - Equip API with a function to trigger Scale FACT 2/2. GNT always runs in 5th client.
            //StopScale();
            CScale.enumSendFactResult sendFactRslt = m_APIScaleClient.CurrentScale.SendFact();
            SetText(statusAPITest, sendFactRslt.ToString());
            //MTTS-HARI - v1.3.7c - 27/9/2017 - #5795 - Equip API with a function to trigger Scale FACT 2/2. GNT always runs in 5th client.
            //StartScale();
        }

        private void btnFactResult_Click(object sender, EventArgs e)
        {
            SetText(lblFactResult, m_APIScaleClient.CurrentScale.SendFactResult.ToString());
            SetText(statusAPITest, "FACT Result");
        }

        //MTTS-HARI v1.3.6 RC9 12/7/2017 5825 - API communication is disturbed when automatic brightness reduction message is shown
        private void btnReduceBacklight_Click(object sender, EventArgs e)
        {
            m_APITerminalClient.Terminal.ReduceBackLight();
        }
        //MTTS-HARI v1.3.6 RC9 12/7/2017 5825 - API communication is disturbed when automatic brightness reduction message is shown
        private void btnResetBacklight_Click(object sender, EventArgs e)
        {
            m_APITerminalClient.Terminal.ResetBackLight();
        }
        //@@MTTS-JRk2 v1.3.6 RC9 13/Jul/2017 - #5743 - support for API applications to reset logout timer
        private void btnResetLogoutTimer_Click(object sender, EventArgs e)
        {
            bool result = m_APITerminalClient.Terminal.ResetLogoutTimer();
            SetText(statusAPITest, string.Format("Reset = {0}", result));
        }

        //MTTS-HARI v1.3.6 RC9 12/7/2017 5825 - API communication is disturbed when automatic brightness reduction message is shown
        System.Threading.Timer autoBLTh;
        private void btnAutoBL_Click(object sender, EventArgs e)
        {
#if(WindowsCE)
            try
            {
                int DueTime = Convert.ToInt32(((String.IsNullOrEmpty(txtMinutes.Text)) ? "1" : txtMinutes.Text)) * 60 * 1000;
                
                if (autoBLTh != null)
                    autoBLTh.Change(DueTime, Timeout.Infinite);
                else
                    autoBLTh = new System.Threading.Timer(eventAutoResetBL, null, DueTime, Timeout.Infinite);
            }
            catch
            {
                MessageBox.Show("Invalid Input");
            }
#endif
        }

        private void eventAutoResetBL(object StateInfo)
        {
            m_APITerminalClient.Terminal.ReduceBackLight();
            MessageBox.Show("Backlight Reduced. Press OK to reset");
            m_APITerminalClient.Terminal.ResetBackLight();
        }
        //MTTS-RAD - 22/8/2017 - TTP5873 - added "Get OS Terminal" button and read OS and Image verion of CE.
        #region Get Terminal OS
        //MTTS-RAD -22/8/2017 -TTP5873 - added constant variable.
        private const string REGKEY_IND890 = @"HKEY_LOCAL_MACHINE\SOFTWARE\IND890";
        private const string KEY_IMAGE_VERSION_CE7 = "ImageCE700";
        private const string KEY_IMAGE_VERSION_CE6 = "ImageCE600";

        //MTTS-RAD -22/8/2017 -TTP5873 - added properties for find neo terminal.
        #region properties
        public bool IsNeoTerminal { get; set; }
        #endregion
        //MTTS-RAD - 22/8/2017 -TTP5873 -Read OS/Image version of CE.
        private void GetTerminalBtn_Click(object sender, EventArgs e)
        {
#if(WindowsCE)
            if(IsNeoTerminal)
            {
                AddItemToList(string.Format("OS version : {0}",KEY_IMAGE_VERSION_CE7));
                AddItemToList(string.Format("CE Image Version : {0}",ReadRegistryKey(REGKEY_IND890, KEY_IMAGE_VERSION_CE7, "").ToLower()));
            }
            else
            {
                AddItemToList(string.Format("OS version : {0}",KEY_IMAGE_VERSION_CE6));
                AddItemToList(string.Format("CE Image Version : {0}",ReadRegistryKey(REGKEY_IND890, KEY_IMAGE_VERSION_CE6, "").ToLower()));
            }
#endif
        }
        //MTTS-RAD - 22/8/2017 - TTP#5873 -Read registry
        #region Read-Registry
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fullPath"></param>
        /// <param name="key"></param>
        /// <param name="defaultVal"></param>
        /// <returns></returns>
        public static string ReadRegistryKey(string fullPath, string key, string defaultVal)
        {
            string result = defaultVal;

            object retobject = null;
            try
            {
                retobject = Registry.GetValue(fullPath, key, null);
            }
            catch (Exception ex)
            {
                Debug.Assert(false, ex.Message);
            }

            if (retobject != null)
            {
                if (retobject.GetType() == typeof(string[]))
                    result = string.Join("; ", (string[])retobject);
                else
                    result = retobject.ToString();
            }

            return result;
        }
        public bool IsWEC7Version()
        {
            string imageVersion = ReadRegistryKey(REGKEY_IND890, KEY_IMAGE_VERSION_CE7, "0");
            return (imageVersion == "0") ? false : true; // if imageVersion is "0" then it is a CE6.0 version
        }
      
        #endregion
        #endregion
        //MTTS-RAD - 25/9/2017 - TTP#5724- added "DB" tab and DB functions.
        #region DB
        private CDBFunctions m_DBFunction;
        private CProductDetail productObj;
        private bool connectionResult = false;
        private frmProductDetail frmProductDetailObj;

        private void btnView_Click(object sender, EventArgs e)
        {
            DataTable dt = m_DBFunction.ViewRecord();
            dataGridProducts.DataSource = dt;
            int rowCount = m_DBFunction.GetRowCount();
            if (rowCount > 0)
            {
                btnUpdate.Enabled = true;
                btnDelete.Enabled = true;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            bool fromRangeResult = IsNumeric(txtRangeFrom.Text);
            bool toRangeResult = IsNumeric(txtRangeTo.Text);
            //MTTS-RAD - TTP5724 -6/11/2017 To check txtRangeFrom.Text is not null and Empty.
            //if(txtRangeFrom.Text!= null && fromRangeResult)
            if (fromRangeResult && !string.IsNullOrEmpty(txtRangeFrom.Text))
            {
                int fromRange = int.Parse(txtRangeFrom.Text);
                //MTTS-RAD - TTP5724 -6/11/2017 To check txtRangeFrom.Text is not null and Empty.
                //if(txtRangeTo.Text!= null && toRangeResult)
                if (toRangeResult && !string.IsNullOrEmpty(txtRangeTo.Text) )
                {
                    int toRange = int.Parse(txtRangeTo.Text);
                    int rowCount = m_DBFunction.GetLastRowCount();
                    if (rowCount < fromRange)
                    {
                        MessageBox.Show("Doesnot meet Range");
                        return;
                    }
                    else
                    {
                        DataTable dt = m_DBFunction.SearchRecord(fromRange, toRange);
                        if (dt.Rows.Count == 0)
                        {
                            dataGridProducts.DataSource = dt;
                            btnUpdate.Enabled = false;
                            btnDelete.Enabled = false;
                        }
                        else
                        {
                            dataGridProducts.DataSource = dt;
                            btnUpdate.Enabled = true;
                            btnDelete.Enabled = true;
                        }
                    }
                }
                else
                    MessageBox.Show("Enter Numeric Value");
            }
            else
                MessageBox.Show("Enter Numeric Value");

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmProductDetailObj.btnAddProduct.Enabled = true;
            frmProductDetailObj.ShowDialog();
            DataTable dt = m_DBFunction.ViewRecord();
            dataGridProducts.DataSource = dt;
            dataGridProducts.Refresh();
            int rowCount = m_DBFunction.GetRowCount();
            if (rowCount > 0)
            {
                btnUpdate.Enabled = true;
                btnDelete.Enabled = true;
            }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            connectionResult = m_DBFunction.ConnectDatabase();

            if (connectionResult)
            {
                btnConnect.Enabled = false;
                btnDisconnect.Enabled = true;
                btnAdd.Enabled = true;
                btnSearch.Enabled = true;
                btnSelect.Enabled = true;
                btnView.Enabled = true;
                dataGridProducts.Enabled = true;
            }
            else
                MessageBox.Show("SDF File Not Found /Connection  Error");
        }
        private void btnSelect_Click(object sender, EventArgs e)
        {
            bool selectRangeResult = IsNumeric(txtProductNo.Text);
            //MTTS-RAD - TTP5724 -6/11/2017 To check txtProductNo.Text is not null and Empty.
            //if(txtProductNo.Text!= null && selectRangeResult)
            if (!string.IsNullOrEmpty(txtProductNo.Text) && selectRangeResult)
            {
                int productno = Convert.ToInt32(txtProductNo.Text);
                int rowCount = m_DBFunction.GetLastRowCount();
                if (rowCount >= productno)
                {
                    DataTable dt = m_DBFunction.SelectRecord(productno);
                    if (dt.Rows.Count == 0)
                    {
                        dataGridProducts.DataSource = dt;
                        btnUpdate.Enabled = false;
                        btnDelete.Enabled = false;
                    }
                    else
                    {
                        dataGridProducts.DataSource = dt;
                        btnUpdate.Enabled = true;
                        btnDelete.Enabled = true;
                    }
                }
                else
                {
                    MessageBox.Show("Selected Number not in Range");
                }
            }
            else
            {
                MessageBox.Show("Enter Numeric value");
            }
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            m_DBFunction.DisconnectDatabase();
            btnDisconnect.Enabled = false;
            btnAdd.Enabled = false;
            btnDelete.Enabled = false;
            btnUpdate.Enabled = false;
            btnSearch.Enabled = false;
            btnSelect.Enabled = false;
            dataGridProducts.Enabled = false;
            btnView.Enabled = false;
            btnConnect.Enabled = true;
            dataGridProducts.DataSource = null;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int selectRowCount = Convert.ToInt32(((DataTable)(dataGridProducts.DataSource)).Rows[dataGridProducts.CurrentRowIndex]["RecordNo"]);
            productObj = m_DBFunction.SelectRow(selectRowCount);
            frmProductDetailObj.btnAddProduct.Enabled = false;
            frmProductDetailObj.UpdateValue(productObj);
            frmProductDetailObj.ShowDialog();
            DataTable dt = m_DBFunction.ViewRecord();
            dataGridProducts.DataSource = dt;
            dataGridProducts.Refresh();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

            int rowCount = Convert.ToInt32(((DataTable)(dataGridProducts.DataSource)).Rows[dataGridProducts.CurrentRowIndex]["RecordNo"]);
            bool status = m_DBFunction.DeleteRecord(rowCount);
            int rowCnt = m_DBFunction.GetRowCount();
            if (rowCnt <= 0)
            {
                btnUpdate.Enabled = false;
                btnDelete.Enabled = false;
            }
            DataTable dt = m_DBFunction.ViewRecord();
            dataGridProducts.DataSource = dt;
        }

        private bool IsNumeric(string sRange)
        {
            foreach (char c in sRange)
            {
                if (!char.IsNumber(c))
                {
                    return false;
                }
            }
            return true;
        }
        #endregion
        //Reverted for 5825 - API communication is disturbed when automatic brightness reduction message is shown
        //MTTS-HARI v1.3.6 RC9 26/6/2017 5825 - Reduce backlight level for WW & legacy mode
        //private void btnReduceBacklightOn_Click(object sender, EventArgs e)
        //{
        //    m_APITerminalClient.Terminal.TurnOnReduceBackLight();
        //}
        //MTTS-HARI v1.3.6 RC9 26/6/2017 5825 - Reduce backlight level for WW & legacy mode
        //private void btnReduceBacklightOff_Click(object sender, EventArgs e)
        //{
        //    m_APITerminalClient.Terminal.TurnOffReduceBackLight();
        //}

               
        //
    }
}
