#region Usings

using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Wollsdorf_Spaltwaage.Allgemein.Button;

#endregion

namespace Wollsdorf_Spaltwaage.Allgemein.ButtonBar
{
    public partial class ctrlButtonBar : UserControl
    {
	    public delegate void _EventButtonClick(object sender, string fTaste, int iTastenCode, string fTag);
	    public event         _EventButtonClick EventButtonClick;

        public delegate void _EventAfterButtonClick();
        public event         _EventAfterButtonClick EventAfterButtonClick;
 
	    private int     iPage;                          // Aktive Seite

	    private string []Text_Row1  = new string[8];     // Die Beschriftung der Tasten
	    private string []ButtonTag  = new string[8];     // Wird über das Event weitergereicht
    
        public ctrlButtonBar()
        {
            try
		    {
			    InitializeComponent();

			    this.Dock = System.Windows.Forms.DockStyle.Bottom;
		    }
		    catch(Exception ex)
		    {
			    MessageBox.Show(ex.Message , "FEHLER");
		    }
			
		      #region Setze die Defaults für die Buttons
		      this.Set_Button_Defaults(Button1);
              this.Set_Button_Defaults(Button2);
              this.Set_Button_Defaults(Button3);
              this.Set_Button_Defaults(Button4);
              this.Set_Button_Defaults(Button5);
              this.Set_Button_Defaults(Button6);
              this.Set_Button_Defaults(Button7);
              this.Set_Button_Defaults(Button8);
		      #endregion
        }
     
        /// <summary>
        /// Die nachfolgenden Parameter müssen gesetzt werden damit eine eigene
        /// Hintergrundfarbe Möglich ist
        /// </summary>
        /// <param name="b"></param>
        #region Set_Button_Defaults
        private void Set_Button_Defaults(ctrlButton b)
        {
            b.TabStop = false;
            b.StartColor = Color.DimGray;            
            b.Invalidate();            
        }
        #endregion

        public void BUTTON_DEAKTIVIEREN(int iButton, bool bISEnabled)
        {
            ctrlButton b = null;

            switch (iButton)
            {
                case 1: b = this.Button_F1; break;
                case 2: b = this.Button_F2; break;
                case 3: b = this.Button_F3; break;
                case 4: b = this.Button_F4; break;
                case 5: b = this.Button_F5; break;
                case 6: b = this.Button_F6; break;
                case 7: b = this.Button_F7; break;
                case 8: b = this.Button_F8; break;
            }

            if (b != null)
            {
                if (bISEnabled)
                {
                    b.Enabled = true;
                    this.Set_Button_Defaults(b);
                }
                else
                {
                    //b.Text = "";
                    //b.ImageIcon = null;
                    b.Enabled = false;
                    //b.Invalidate();  
                }
            }
        }

        private void ctrlButtonBar_Load(object sender, EventArgs e)
        {
		  Init_Label_Caption();
		  DISP_PAGE(1);	

		  this.Height = 69;
        }
        
        #region Properties
        
		public System.Drawing.Color Button1BackColor
		{
		    set { Button1.BackColor = value;}
		}
		public System.Drawing.Color Button2BackColor
		{
		    set { Button2.BackColor = value;}
		}
		public System.Drawing.Color Button3BackColor
		{
		    set { Button3.BackColor = value;}
		}
		public System.Drawing.Color Button4BackColor
		{
		    set { Button4.BackColor = value;}
		}
		public System.Drawing.Color Button5BackColor
		{
		    set { Button5.BackColor = value;}
		}
		public System.Drawing.Color Button6BackColor
		{
		    set { Button6.BackColor = value;}
		}
		public System.Drawing.Color Button7BackColor
		{
		    set { Button7.BackColor = value;}
		}
		public System.Drawing.Color Button8BackColor
		{
		    set { Button8.BackColor = value;}
		}                                                        

        internal ctrlButton Button_F1
        {
            get { return Button1;} set { Button1 = value; }
        }
        internal ctrlButton Button_F2
        {
            get { return Button2;} set { Button2 = value; }
        }
        internal ctrlButton Button_F3
        {
            get { return Button3;} set { Button3 = value; }
        }
        internal ctrlButton Button_F4
        {
            get { return Button4;} set { Button4 = value; }
        }
        internal ctrlButton Button_F5
        {
            get { return Button5;} set { Button5 = value; }
        }
        internal ctrlButton Button_F6
        {
            get { return Button6;} set { Button6 = value; }
        }
        internal ctrlButton Button_F7
        {
            get { return Button7;} set { Button7 = value; }
        }
        internal ctrlButton Button_F8
        {
            get { return Button8;} set { Button8 = value; }
        }     
        
        #endregion
                                                 
        /// <summary>
        /// Beschrifte die kleinen Labels mit den F Tasten
        /// </summary>
        private void Init_Label_Caption()
		{
		    Button_Label_F1.Text = "F1"; Button_Label_F1.Size = new Size ( (int) 25, (int) 18);
		    Button_Label_F2.Text = "F2"; Button_Label_F2.Size = new Size ( (int) 25, (int) 18);
			Button_Label_F3.Text = "F3"; Button_Label_F3.Size = new Size ( (int) 25, (int) 18);
			Button_Label_F4.Text = "F4"; Button_Label_F4.Size = new Size ( (int) 25, (int) 18);
			Button_Label_F5.Text = "F5"; Button_Label_F5.Size = new Size ( (int) 25, (int) 18);
			Button_Label_F6.Text = "F6"; Button_Label_F6.Size = new Size ( (int) 25, (int) 18);
			Button_Label_F7.Text = "F7"; Button_Label_F7.Size = new Size ( (int) 25, (int) 18);
			Button_Label_F8.Text = "F8"; Button_Label_F8.Size = new Size ( (int) 25, (int) 18);
		}
        		 
		public void DISP_PAGE(int i)
		{
			iPage = i;
			DISP_PAGE();
		}

		public void BUTTON_ENABLE(bool bEnabled)
		{
			Button1.Enabled = bEnabled;
			Button2.Enabled = bEnabled;
			Button3.Enabled = bEnabled;
			Button4.Enabled = bEnabled;
			Button5.Enabled = bEnabled;
			Button6.Enabled = bEnabled;
			Button7.Enabled = bEnabled;
			Button8.Enabled = bEnabled;
		}

		public void DISP_PAGE()
		{
			Button1.Text = Text_Row1[0]; 
			Button2.Text = Text_Row1[1];
			Button3.Text = Text_Row1[2];
			Button4.Text = Text_Row1[3];
			Button5.Text = Text_Row1[4];
			Button6.Text = Text_Row1[5];
			Button7.Text = Text_Row1[6];
			Button8.Text = Text_Row1[7];
		}

		/// <summary>
		/// button caption for each button
		/// </summary>
		public void SET_BUTTON_TEXT(int ButtonID, string Text, string Tag)
		{
			switch (ButtonID)
			{
				case (1): Text_Row1[0] = Text; if ( iPage == 1) {Button1.Text = Text_Row1[0]; ButtonTag[0] = Tag; } break;
				case (2): Text_Row1[1] = Text; if ( iPage == 1) {Button2.Text = Text_Row1[1]; ButtonTag[1] = Tag; } break;
				case (3): Text_Row1[2] = Text; if ( iPage == 1) {Button3.Text = Text_Row1[2]; ButtonTag[2] = Tag; } break;
				case (4): Text_Row1[3] = Text; if ( iPage == 1) {Button4.Text = Text_Row1[3]; ButtonTag[3] = Tag; } break;
				case (5): Text_Row1[4] = Text; if ( iPage == 1) {Button5.Text = Text_Row1[4]; ButtonTag[4] = Tag; } break;
				case (6): Text_Row1[5] = Text; if ( iPage == 1) {Button6.Text = Text_Row1[5]; ButtonTag[5] = Tag; } break;
				case (7): Text_Row1[6] = Text; if ( iPage == 1) {Button7.Text = Text_Row1[6]; ButtonTag[6] = Tag; } break;
				case (8): Text_Row1[7] = Text; if ( iPage == 1) {Button8.Text = Text_Row1[7]; ButtonTag[7] = Tag; } break;				
			}
		}
		public void RaiseExternButtonEvent(object sender, string sButtonName, int iButtonID)
		{
            ctrlButton b = null;
		
		    switch (iButtonID )
		    {
		        case 1: b = Button_F1; break;
		        case 2: b = Button_F2; break;
		        case 3: b = Button_F3; break;
		        case 4: b = Button_F4; break;
		        case 5: b = Button_F5; break;
		        case 6: b = Button_F6; break;
		        case 7: b = Button_F7; break;
		        case 8: b = Button_F8; break;
		    }
		
		    if ( b != null)
		    {
		    }
		    else 
		    {
		        EventButtonClick(this , sButtonName , iButtonID,  ButtonTag[ iButtonID -1] );
				  if (EventAfterButtonClick != null) { EventAfterButtonClick(); }
		    }
		}
		
        private void Button_Click_Event(object sender, System.EventArgs e)
		{
			try
			{
			    string    sName = "";
				int       iTaste = 0;

                if (sender.GetType() != typeof(ctrlButton)) return;

                sName = ((ctrlButton)sender).Name;

                //((ctrlButton)sender).Enabled = false;
				
				if ( EventButtonClick != null )
				{
                    switch (((ctrlButton)sender).Name)
				    {
					case ("Button1"): iTaste = 1;  EventButtonClick(sender , sName , iTaste,  ButtonTag[0] ); break;
					case ("Button2"):	iTaste = 2;  EventButtonClick(sender , sName , iTaste,  ButtonTag[1] ); break;
					case ("Button3"):	iTaste = 3;  EventButtonClick(sender , sName , iTaste,  ButtonTag[2] ); break;
					case ("Button4"):	iTaste = 4;  EventButtonClick(sender , sName , iTaste,  ButtonTag[3] ); break;
					case ("Button5"):	iTaste = 5;  EventButtonClick(sender , sName , iTaste,  ButtonTag[4] ); break;
					case ("Button6"):	iTaste = 6;  EventButtonClick(sender , sName , iTaste,  ButtonTag[5] ); break;
					case ("Button7"):	iTaste = 7;  EventButtonClick(sender , sName , iTaste,  ButtonTag[6] ); break;
					case ("Button8"):	iTaste = 8;  EventButtonClick(sender , sName , iTaste,  ButtonTag[7] ); break;
					default:				iTaste = 99; EventButtonClick(sender , sName , iTaste,  ButtonTag[8] ); break;
				    }				    
				}			
			}
			catch(Exception ex)
			{
					Trace.WriteLine ( "Exception in 'ctrlButtonBar'");
					Trace.WriteLine ( ex.Message );					 
			}
            //if (!this.IsDisposed)
            //{
            //    ((ctrlButton)sender).Enabled = true;
            //}

			if (EventAfterButtonClick != null) { EventAfterButtonClick(); }
	    }

        private void ctrlButtonBar_Resize(object sender, EventArgs e)
        {
            this.Update_Size(); 
        }
        private void Update_Size()
        {
            #region Setze die Breite der 8 Buttons
            int W = this.Width/8;            
			Button1.Width  = W;			
			Button2.Width  = W;
            Button3.Width  = W;
            Button4.Width  = W;
            Button5.Width  = W;
            Button6.Width  = W;
            Button7.Width  = W;
            Button8.Width  = W;
            #endregion
            
            Button1.Height  = this.Height - Button1.Top;
            Button2.Height  = Button1.Height;
            Button3.Height  = Button1.Height;
            Button4.Height  = Button1.Height;
            Button5.Height  = Button1.Height;
            Button6.Height  = Button1.Height;
            Button7.Height  = Button1.Height;
            Button8.Height  = Button1.Height;
            
            Button1.Top     = 1;
            Button2.Top     = Button1.Top;
            Button3.Top     = Button1.Top;
            Button4.Top     = Button1.Top;
            Button5.Top     = Button1.Top;
            Button6.Top     = Button1.Top;
            Button7.Top     = Button1.Top;
            Button8.Top     = Button1.Top;
	
	        #region Ordne die Buttons neu an
			Button1.Left = 0;
			Button2.Left = Button1.Left + Button1.Width +1;
			Button3.Left = Button2.Left + Button2.Width +1;
            Button4.Left = Button3.Left + Button3.Width +1;
            Button5.Left = Button4.Left + Button4.Width +1;
            Button6.Left = Button5.Left + Button5.Width +1;
            Button7.Left = Button6.Left + Button6.Width +1;
            Button8.Left = Button7.Left + Button7.Width +1;
            #endregion
        }     					
    }
}
