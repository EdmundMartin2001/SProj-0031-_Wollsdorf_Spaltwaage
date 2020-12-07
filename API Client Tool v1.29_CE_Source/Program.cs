using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;

namespace IND890APIClientTool_NonIPC
//namespace IND890APIClientTool_IPCEnabled    
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [MTAThread]
        static void Main()
        {
            Application.Run(new frmAPICalls_NonIPC());
  //          Application.Run(new frmAPICalls_IPCEnabled());            
        }
    }
}