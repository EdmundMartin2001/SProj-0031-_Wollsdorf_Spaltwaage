using System;
using SMT_SQL_2V;

namespace SMT_SQL_2V.DB
{
	internal class cDB_Settings
	{
		private static string		_sDatabaseFileName;	
		private static string		_sDatabaseUserName;
		private static string		_sDatabaseUserPassw;
		private static string		_sCE_ConnectionString;
        private static string       _sEXTERN_ConnectionString;
		private static string		_sServerName;
		private static string		_sInfoConnString;
		
		public cDB_Settings()
		{
		}

		public static string DataBaseFileName
		{
			get { return _sDatabaseFileName; } set { _sDatabaseFileName = value; }
		}

		public static string DataBaseUserName
		{
			get { return _sDatabaseUserName; } set { _sDatabaseUserName = value; }
		}

		public static string DataBaseUserPassword
		{
			get { return _sDatabaseUserPassw; } set { _sDatabaseUserPassw = value; }
		}

		public static string CE_ConnectionString
		{
			get { return _sCE_ConnectionString; }
            set { _sCE_ConnectionString = value; }
		}

        public static string ConnectionString_EXTERN
        {
            get { return _sEXTERN_ConnectionString; }
            set { _sEXTERN_ConnectionString = value; }
        }
		
		public static string Display_ServerName
		{
			get { return _sServerName; } set { _sServerName = value; }
		}
		public static string Display_InfoConnString
		{
			get { return _sInfoConnString; } set { _sInfoConnString = value; }
		}		

	}
}
