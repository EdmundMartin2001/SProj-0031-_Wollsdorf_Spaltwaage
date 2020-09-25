using System;
using System.Diagnostics;
using System.Windows.Forms;
using Microsoft.Win32;
using Allgemein.Helper; 

namespace SMT_SQL_2V.SMT_Standards
{
	internal class AppStandards
	{
		private static string		 _sAppName					= "unknown";
		private static string		 _sDebugProgramFolder		= @"c:\";
		private static string		 _sKonfigurationsFile		= @"c:\config.ini";
        private static string        _sKonfigurationsFilePassw  = "";
		private static string		 _sDatabaseName				= "unknown";		
		private static string		 _sLastErrorText			= "unknown";
		private static IntPtr		 _ptr_Main					= IntPtr.Zero;
		private static string		 _sStationsName				= null;
		private static string		 []sEntwicklerPC			= null;
		private static string		 _sActiveUserGUIDE			= "{00000000-0000-0000-0000-000000000000}";
		private static string		 _sSystemPassword			= "13";
		private static string		 _sLizenzName				= "unbekannt";		
        
		public AppStandards()
		{
		}

        public static string MachineName
        {
            get
            {
                string s1 = string.Empty;
                System.Net.Dns.Resolve(s1);
                return s1;
            }
        }

		public static string ActiveUserGUIDE
	   {
			get { return _sActiveUserGUIDE; } set { _sActiveUserGUIDE = value; }
	   }

		public static string SystemPassword
	   {
			get { return _sSystemPassword; } set { _sSystemPassword = value; }
	   }
		
	   
		public static void Set_EntwicklungsPCs ( string []EntwicklerPC)
		{
			sEntwicklerPC = EntwicklerPC;
		}

		public static IntPtr Main_IntPtr   
		{
			get { return _ptr_Main; } set { _ptr_Main = value; }
		}

		public static string DatabaseName
		{
			get { return _sDatabaseName; } set { _sDatabaseName = value;	}
		}
		public static string LizenzName
		{
			get { return _sLizenzName; } set { _sLizenzName = value;	}
		}
		public static string AppName
		{
			get { return _sAppName; } set { _sAppName = value; }
		}

        /// <summary>
        /// Verwalte den Dateinamen mit den System Settings, zb. eine INI Datei,
        /// oder eine Access oder TurboDB Datei
        /// </summary>
		public static string KonfigurationsFile
		{
		  get { return _sKonfigurationsFile; } set { _sKonfigurationsFile = value; }
		}

        /// <summary>
        /// Verwalte bei einer Datenbank mit System Settings das optionale 
        /// Passwort der Datenbank bei zb. Access oder TurboDB 
        /// </summary>
        public static string KonfigurationsFilePasswort
		{
		  get { return _sKonfigurationsFilePassw; } set { _sKonfigurationsFilePassw = value; }
		}

        

		public static string DebugProgramFolder
		{
			get { return _sDebugProgramFolder;	} set
			{
				_sDebugProgramFolder = value;
				if (! _sDebugProgramFolder.EndsWith(@"\") ) { _sDebugProgramFolder += @"\"; }
			}
		}

		public static string Get_ApplicationPath
		{
			get
			{
				if ( ISEntwicklungsPC )
					return _sDebugProgramFolder;
				else
				{
                    return System.IO.Path.GetDirectoryName(SystemHelper.ExecutablePath) + @"\";
				}  
			}
		
		}

		public static string Get_Path_LayoutTemp
		{
			get
			{
				if ( ISEntwicklungsPC )
					return _sDebugProgramFolder + @"LayoutTemp\";
				else
				{
                    return System.IO.Path.GetDirectoryName(SystemHelper.ExecutablePath) + @"\LayoutTemp\";
				}  
			}
		}

		public static string Get_Logfilepath_Standard
		{
			get
			{
				if ( ISEntwicklungsPC )
					return _sDebugProgramFolder + @"Logfile\";
				else
				{
                    return System.IO.Path.GetDirectoryName(SystemHelper.ExecutablePath) + @"\Logfile\";
				}  
			}
		}
	
		public static string Get_Logfilepath_ErrorLog
		{
			get
			{
				if ( ISEntwicklungsPC )
					return _sDebugProgramFolder + @"ErrorLog\";
				else
				{
                    return System.IO.Path.GetDirectoryName(SystemHelper.ExecutablePath) + @"\ErrorLog\";
				}  
			}
		}

		public static string Get_Logfilepath_ImportLog
		{
			get
			{
				if ( ISEntwicklungsPC )
					return _sDebugProgramFolder + @"ImportLog\";
				else
				{
                    return System.IO.Path.GetDirectoryName(SystemHelper.ExecutablePath) + @"\ImportLog\";
				}  
			}
		}

		public static string LastErrorText
		{
			get { return _sLastErrorText;	} set {_sLastErrorText = value;}
		}

 

		public static bool ISEntwicklungsPC
		{
			get
			{
				bool ret = false;
				if ( sEntwicklerPC == null) return false;

				foreach (string s in sEntwicklerPC )
				{
					if ( SMT_Standards.AppStandards.MachineName.ToUpper().Equals(s) ) 
					{
						ret = true;
						break;
					}
				}

				return ret;
			}
		}

		 

	}
}
