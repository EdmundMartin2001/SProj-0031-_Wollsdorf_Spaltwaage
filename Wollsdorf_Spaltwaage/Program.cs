using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Wollsdorf
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [MTAThread]
        static void Main()
        {
            //Beispiel
            string sAppPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);

            //string m_customDBPath = @"\Storage Card\CustomNET\Data\smt_ind890_data.sdf";
            string m_customDBPath = sAppPath + @"\Data\smt_ind890_data.sdf";

            SMT_SQL_2V.DB.cDB_Settings.CE_ConnectionString =
                m_customDBPath;

            //    string.Format("DataSource={0}", m_customDBPath);

            // Test to see if custom DB exists
            if (!System.IO.File.Exists(m_customDBPath))
            {
                MessageBox.Show("FEHLER 1221: Datenbank nicht installiert");
                MessageBox.Show(m_customDBPath);
                return;
            }

            Allgemein.SiAuto.WriteErrorLog("StartUp", "", null);

            //   Allgemein.FormHelper.cFullScreen f = new Allgemein.FormHelper.cFullScreen();
            //  f.SetFullScreen(true);

            try
            {
                Application.Run(new frmMain());
            }
            catch (Exception ex)
            {
            }
            finally
            {
                //    Allgemein.FormHelper.cFullScreen f1 = new Allgemein.FormHelper.cFullScreen();
                //  f1.SetFullScreen(false);
            }
        }
    }
}
