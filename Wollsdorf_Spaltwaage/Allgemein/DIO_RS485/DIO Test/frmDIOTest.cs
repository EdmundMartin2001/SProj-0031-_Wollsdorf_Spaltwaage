using System;
using System.Drawing;
using System.Windows.Forms;
using Wollsdorf_Spaltwaage.Kundenspezifisch;

namespace Wollsdorf_Spaltwaage.Allgemein.DIO_RS485
{
    internal partial class frmDIOTest : Form
    {
        private global::Wollsdorf_Spaltwaage.Allgemein.DIO_RS485.cSMT_DIO objDIO;
        private bool bStackCheck;

        public frmDIOTest()
        {
            InitializeComponent();

            this.bStackCheck = false;
            this.timer1.Enabled = false;
        }
        private bool Erzeuge_Objekt()
        {
            bool bRet = false;

            try
            {
                this.objDIO = new global::Wollsdorf_Spaltwaage.Allgemein.DIO_RS485.cSMT_DIO();

                if (this.objDIO != null)
                {
                    this.objDIO.DIO_ResetAll();
                    
                    this.objDIO.Set_OUT_Bit(/*Bit*/ 1, true);
                    bRet = true;
                }
                else
                {
                    MessageBox.Show("KEINE DIGITAL IO", "Hardware Fehler bei IO");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Phase1");
            }

            return bRet;
        }
        private void frmDIOTest_Load(object sender, EventArgs e)
        {
            cGlobalHandling.CenterForm(this, 80);

            if (!this.Erzeuge_Objekt())
            {
                this.Close();
            }
            else
            {
                this.timer1.Interval = 5000;
                this.timer1.Enabled = true;
            }
        }
        private void cmdIO1_Click(object sender, EventArgs e)
        {
            this.Set_Button(/*Bit*/ 1, this.cmdIO1);
        }
        private void cmdIO2_Click(object sender, EventArgs e)
        {
            this.Set_Button(/*Bit*/ 2, this.cmdIO2);
        }
        private void cmdIO3_Click(object sender, EventArgs e)
        {
            this.Set_Button(/*Bit*/ 3, this.cmdIO3);
        }
        private void cmdIO4_Click(object sender, EventArgs e)
        {
            this.Set_Button(/*Bit*/ 4, this.cmdIO4);
        }
        private void cmdIO5_Click(object sender, EventArgs e)
        {
            this.Set_Button(/*Bit*/ 5, this.cmdIO5);
        }
        private void cmdIO6_Click(object sender, EventArgs e)
        {
            this.Set_Button(/*Bit*/ 6, this.cmdIO6);
        }
        private void cmdIO7_Click(object sender, EventArgs e)
        {
            this.Set_Button(/*Bit*/ 7, this.cmdIO7);
        }
        private void cmdIO8_Click(object sender, EventArgs e)
        {
            this.Set_Button(/*Bit*/ 8, this.cmdIO8);
        }
        private void cmdIO9_Click(object sender, EventArgs e)
        {
            this.Set_Button(/*Bit*/ 9, this.cmdIO9);
        }
        private void cmdIO10_Click(object sender, EventArgs e)
        {
            this.Set_Button(/*Bit*/ 10, this.cmdIO10);
        }
        private void cmdIO11_Click(object sender, EventArgs e)
        {
            this.Set_Button(/*Bit*/ 11, this.cmdIO11);
        }
        private void cmdIO12_Click(object sender, EventArgs e)
        {
            this.Set_Button(/*Bit*/ 12, this.cmdIO12);
        }
        private void Set_Button(int iBit, System.Windows.Forms.Button c)
        {
            bool bInvert = this.objDIO.Get_OUT_Bit(/*Bit*/ iBit);
            this.objDIO.Set_OUT_Bit(/*Bit*/ iBit, !bInvert);

            bInvert = this.objDIO.Get_OUT_Bit(/*Bit*/ iBit);
            c.Text = iBit.ToString() + (bInvert ? "on" : "off");
        }
        private void cmdIOReset_Click(object sender, EventArgs e)
        {
            this.objDIO.DIO_ResetAll();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Text = DateTime.Now.ToLongTimeString();

            if (this.bStackCheck)
            {
                return;
            }

            if (this.objDIO == null)
            {
                return;
            }

            bStackCheck = true;

            try
            {
                this.Get_InputState(1, this.cmdIOInp1);
                this.Get_InputState(2, this.cmdIOInp2);
                this.Get_InputState(3, this.cmdIOInp3);
                this.Get_InputState(4, this.cmdIOInp4);
                this.Get_InputState(5, this.cmdIOInp5);
                this.Get_InputState(6, this.cmdIOInp6);
                this.Get_InputState(7, this.cmdIOInp7);
                this.Get_InputState(8, this.cmdIOInp8);
            }
            catch (Exception)
            {
                this.timer1.Enabled = false;
            }
            finally
            {
                this.bStackCheck = false;
            }
        }
        private void Get_InputState(int iBit, global::Wollsdorf_Spaltwaage.Allgemein.Button.ctrlButton c)
        {
            bool b = this.objDIO.Get_IN_Bit(/*Bit*/ iBit);
            
            c.Text = b ? "on" : "off";

            if (b)
            {
                c.StartColor = Color.Red;
            }
            else
            {
                c.StartColor = Color.DarkGray;
            }
        }       
        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.timer1.Enabled = false;

            this.Close();
        }
    }
}