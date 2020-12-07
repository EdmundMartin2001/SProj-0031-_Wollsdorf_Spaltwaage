using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MTTS.IND890.CE;
using System.Data;

namespace IND890APIClientTool_NonIPC
{
    public partial class frmAPICalls_NonIPC
    {
        private CInterface m_Interface1;
        private CInterface m_Interface2;
        private CInterface m_Interface3;
        private CInterface m_Interface4;
        private CInterface m_Interface5;
        private CInterface m_Interface6;
        private CInterface m_Ethernet;

        #region Interface
        public void InitiateInterface()
        {
            //MTTS-HARI - v1.3.6A - 2/8/2017 - #5875 - Equip IND890 API with a 4th channel for 'high speed' serial communication.
            //Change the Interface communications from Terminal client to Serial client
            m_Interface1 = m_APISerialClient.Interface[1];
            m_Interface1.DataMode = CInterface.enumDataMode.MODE_STRING;
            m_Interface1.OnRxString += m_Interface1_OnRxString;
            //Handled OnRxByte event in binary mode for #4289, V1.3.2a Iteration II
            m_Interface1.OnRxByte += new CInterface.RxByte(m_Interface1_OnRxByte);
            //
            m_Interface2 = m_APISerialClient.Interface[2];
            m_Interface2.DataMode = CInterface.enumDataMode.MODE_STRING;
            m_Interface2.OnRxString += new CInterface.RxString(m_Interface2_OnRxString);
            m_Interface2.OnRxByte += new CInterface.RxByte(m_Interface2_OnRxByte);
            //
            m_Interface3 = m_APISerialClient.Interface[3];
            m_Interface3.DataMode = CInterface.enumDataMode.MODE_STRING;
            m_Interface3.OnRxString += new CInterface.RxString(m_Interface3_OnRxString);
            m_Interface3.OnRxByte += new CInterface.RxByte(m_Interface3_OnRxByte);
            //
            m_Interface4 = m_APISerialClient.Interface[4];
            m_Interface4.DataMode = CInterface.enumDataMode.MODE_STRING;
            m_Interface4.OnRxString += new CInterface.RxString(m_Interface4_OnRxString);
            m_Interface4.OnRxByte += new CInterface.RxByte(m_Interface4_OnRxByte);
            //
            m_Interface5 = m_APISerialClient.Interface[5];
            m_Interface5.DataMode = CInterface.enumDataMode.MODE_STRING;
            m_Interface5.OnRxString += new CInterface.RxString(m_Interface5_OnRxString);
            m_Interface5.OnRxByte += new CInterface.RxByte(m_Interface5_OnRxByte);
            //
            m_Interface6 = m_APISerialClient.Interface[6];
            m_Interface6.DataMode = CInterface.enumDataMode.MODE_STRING;
            m_Interface6.OnRxString += new CInterface.RxString(m_Interface6_OnRxString);
            m_Interface6.OnRxByte += new CInterface.RxByte(m_Interface6_OnRxByte);

            m_Ethernet = m_APISerialClient.Interface[7];
            m_Ethernet.DataMode = CInterface.enumDataMode.MODE_STRING;
            m_Ethernet.OnRxString += new CInterface.RxString(m_Ethernet_OnRxString);
            m_Ethernet.OnRxByte += new CInterface.RxByte(m_Ethernet_OnRxByte);

            EnableInterfaceContols();
        }

        private void EnableInterfaceContols()
        {
            tb_Enet.Enabled = true;
            tb_RX1.Enabled = true;
            tb_RX2.Enabled = true;
            tb_RX3.Enabled = true;
            tb_RX4.Enabled = true;
            tb_RX5.Enabled = true;
            tb_RX6.Enabled = true;
            txtDatToSend.Enabled = true;

            cmbIntefaceNo.Enabled = true;
            cmbTemplateNo.Enabled = true;

            btnInterfaceCmdWrite.Enabled = true;
        }

        void m_Interface1_OnRxString(string data)
        {
            SetText(tb_RX1, data);
            SetText(statusAPITest, string.Format("Data From Inetface 1 : {0}", data));
        }
        void m_Interface2_OnRxString(string data)
        {
            SetText(tb_RX2, data);
            SetText(statusAPITest, string.Format("Data From Inetface 2 : {0}", data));
        }
        void m_Interface3_OnRxString(string data)
        {
            SetText(tb_RX3, data);
            SetText(statusAPITest, string.Format("Data From Inetface 3 : {0}", data));
        }
        void m_Interface4_OnRxString(string data)
        {
            SetText(tb_RX4, data);
            SetText(statusAPITest, string.Format("Data From Inetface 4 : {0}", data));
        }
        void m_Interface5_OnRxString(string data)
        {
            SetText(tb_RX5, data);
            SetText(statusAPITest, string.Format("Data From Inetface 5 : {0}", data));
        }
        void m_Interface6_OnRxString(string data)
        {
            SetText(tb_RX6, data);
            SetText(statusAPITest, string.Format("Data From Inetface 6 : {0}", data));
        }
        void m_Ethernet_OnRxString(string data)
        {
            SetText(tb_Enet, data);
            SetText(statusAPITest, string.Format("Data From Ethernet : {0}", data));
        }
        void m_Interface1_OnRxByte(byte[] bydata, int iLen)
        {
            string sData = Encoding.ASCII.GetString(bydata, 0, iLen);
            SetText(tb_RX1, sData);
            SetText(statusAPITest, sData);
        }
        void m_Interface2_OnRxByte(byte[] bydata, int iLen)
        {
            string sData = Encoding.ASCII.GetString(bydata, 0, iLen);
            SetText(tb_RX2, sData);
            SetText(statusAPITest, sData);
        }
        void m_Interface3_OnRxByte(byte[] bydata, int iLen)
        {
            string sData = Encoding.ASCII.GetString(bydata, 0, iLen);
            SetText(tb_RX3, sData);
            SetText(statusAPITest, sData);
        }
        void m_Interface4_OnRxByte(byte[] bydata, int iLen)
        {
            string sData = Encoding.ASCII.GetString(bydata, 0, iLen);
            SetText(tb_RX4, sData);
            SetText(statusAPITest, sData);
        }
        void m_Interface5_OnRxByte(byte[] bydata, int iLen)
        {
            string sData = Encoding.ASCII.GetString(bydata, 0, iLen);
            SetText(tb_RX5, sData);
            SetText(statusAPITest, sData);
        }

        void m_Interface6_OnRxByte(byte[] bydata, int iLen)
        {
            string sData = Encoding.ASCII.GetString(bydata, 0, iLen);
            SetText(tb_RX6, sData);
            SetText(statusAPITest, string.Format("Data From Interface 6 : {0}", sData));
        }
        //Handled OnRxByte event in binary mode for #4289, V1.3.2a Iteration II
        void m_Ethernet_OnRxByte(byte[] bydata, int iLen)
        {
            string sData = Encoding.ASCII.GetString(bydata, 0, iLen);
            SetText(tb_Enet, sData);
            SetText(statusAPITest, string.Format("Data From Ethernet : {0}", sData));
        }

        
        #endregion

        #region Alibi Memory
        DataTable m_dtAlibi;

        private void CreateAlibiDatatable()
        {
            m_dtAlibi = new DataTable("AlibiMemory");
            m_dtAlibi.Columns.Add("RecordNumber", typeof(string));
            m_dtAlibi.Columns.Add("Date", typeof(string));
            m_dtAlibi.Columns.Add("Time", typeof(string));
            m_dtAlibi.Columns.Add("TransactionCounter", typeof(string));
            m_dtAlibi.Columns.Add("ScaleNumber", typeof(string));
            m_dtAlibi.Columns.Add("ScaleWeight", typeof(string));
            m_dtAlibi.Columns.Add("NetWeight", typeof(string));
            m_dtAlibi.Columns.Add("TareWeight", typeof(string));
            m_dtAlibi.Columns.Add("TareType", typeof(string));
            m_dtAlibi.Columns.Add("MinWeigh", typeof(string));
            m_dtAlibi.Columns.Add("IdentA", typeof(string));
            m_dtAlibi.Columns.Add("IdentB", typeof(string));
            m_dtAlibi.Columns.Add("IdentC", typeof(string));
            m_dtAlibi.Columns.Add("IdentD", typeof(string));
            m_dtAlibi.Columns.Add("IdentE", typeof(string));
            m_dtAlibi.Columns.Add("IdentF", typeof(string));
            m_dtAlibi.Columns.Add("CheckSum", typeof(string));
            m_dtAlibi.Columns.Add("UserData", typeof(string));
            //5852 V1.3.6 RC9- Added column for ValidData
            m_dtAlibi.Columns.Add("ValidData", typeof(short)); 
        }
        private void btnAlibiGen_Click(object sender, EventArgs e)
        {
            string sUserData = "";
            if (!string.IsNullOrEmpty(txtUsrData.Text.ToString()))
                sUserData = txtUsrData.Text.ToString();
            //MTTS-HARI v1.3.3a 2/11/2015 #5108 GenerateAlibiRecord always returns false. Scale Client runs GetGNT thread which alters the LastCommandSent to the Server.
            //Scale Client modified to Terminal Client.
            //bool alibigen = m_APIScaleClient.AlibiMemory.GenerateAlibiRecord(sUserData);
            bool alibigen = m_APITerminalClient.AlibiMemory.GenerateAlibiRecord(sUserData);
            SetText(statusAPITest, string.Format("Generate Alibi : {0}", alibigen.ToString()));
        }
        private void btnAlibiRead_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtAlibiRecNo.Text.ToString()))
            {
                MessageBox.Show("Please Enter Record Number !");
                return;
            }
            MTTS.IND890.CE.ScaleServer.AlibiInfo alibiInfo = new MTTS.IND890.CE.ScaleServer.AlibiInfo();

            int iRecNo = Int32.Parse(txtAlibiRecNo.Text.ToString());
            bool bStatus = m_APIScaleClient.AlibiMemory.ReadAlibiRecord(iRecNo, ref alibiInfo);
            SetText(statusAPITest, string.Format("Read Alibi : {0}", bStatus.ToString()));
            m_dtAlibi.Rows.Clear();
            if (bStatus)
            {
                DataRow dr = m_dtAlibi.NewRow();
                dr["RecordNumber"] = alibiInfo.RecordNumber;
                dr["Date"] = alibiInfo.Date;
                dr["Time"] = alibiInfo.Time;
                dr["TransactionCounter"] = alibiInfo.TransactionCounter;
                dr["ScaleNumber"] = alibiInfo.ScaleNumber;
                dr["ScaleWeight"] = alibiInfo.ScaleWeight;
                dr["NetWeight"] = alibiInfo.NetWeight;
                dr["TareWeight"] = alibiInfo.TareWeight;
                dr["TareType"] = alibiInfo.TareType;
                dr["MinWeigh"] = alibiInfo.MinWeigh;
                dr["IdentA"] = alibiInfo.IdentA;
                dr["IdentB"] = alibiInfo.IdentB;
                dr["IdentC"] = alibiInfo.IdentC;
                dr["IdentD"] = alibiInfo.IdentD;
                dr["IdentE"] = alibiInfo.IdentE;
                dr["IdentF"] = alibiInfo.IdentF;
                dr["CheckSum"] = alibiInfo.Checksum;
                dr["UserData"] = alibiInfo.UserData;
                //5852 V1.3.6 RC9- Added column for ValidData
                dr["ValidData"] = alibiInfo.ValidData;
                m_dtAlibi.Rows.Add(dr);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLastRec_Click(object sender, EventArgs e)
        {
            MTTS.IND890.CE.ScaleServer.AlibiInfo alibiInfo = new MTTS.IND890.CE.ScaleServer.AlibiInfo();

            bool bStatus = m_APIScaleClient.AlibiMemory.GetLatestAlibi(ref alibiInfo);
            SetText(statusAPITest, string.Format("Read Alibi : {0}", bStatus.ToString()));
            m_dtAlibi.Rows.Clear();
            if (bStatus)
            {
                DataRow dr = m_dtAlibi.NewRow();
                dr["RecordNumber"] = alibiInfo.RecordNumber;
                dr["Date"] = alibiInfo.Date;
                dr["Time"] = alibiInfo.Time;
                dr["TransactionCounter"] = alibiInfo.TransactionCounter;
                dr["ScaleNumber"] = alibiInfo.ScaleNumber;
                dr["ScaleWeight"] = alibiInfo.ScaleWeight;
                dr["NetWeight"] = alibiInfo.NetWeight;
                dr["TareWeight"] = alibiInfo.TareWeight;
                dr["TareType"] = alibiInfo.TareType;
                dr["MinWeigh"] = alibiInfo.MinWeigh;
                dr["IdentA"] = alibiInfo.IdentA;
                dr["IdentB"] = alibiInfo.IdentB;
                dr["IdentC"] = alibiInfo.IdentC;
                dr["IdentD"] = alibiInfo.IdentD;
                dr["IdentE"] = alibiInfo.IdentE;
                dr["IdentF"] = alibiInfo.IdentF;
                dr["CheckSum"] = alibiInfo.Checksum;
                dr["UserData"] = alibiInfo.UserData;
                //5852 V1.3.6 RC9- Added column for ValidData
                dr["ValidData"] = alibiInfo.ValidData;
                m_dtAlibi.Rows.Add(dr);
            }
        }
        #endregion
    }
}
