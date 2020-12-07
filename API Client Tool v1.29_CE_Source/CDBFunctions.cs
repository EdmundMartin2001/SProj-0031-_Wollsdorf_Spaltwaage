using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Diagnostics;
using Microsoft.Win32;

namespace MTTS.IND890.CE
{
    public class CDBFunctions
    {
        private const string constproductTableName = "ProductDetail";
        private const string constproductColumnName = "RecordNo";
        private IDatabase m_Database;
        private CLocalDatabase m_LocalDatabase;
        private static CDBFunctions m_DBFunctions;
        private const string REGKEY_IND890 = @"HKEY_LOCAL_MACHINE\SOFTWARE\IND890";
        private const string KEY_IMAGE_VERSION_CE7 = "ImageCE700";
        private string m_DirectoryFolder;


        public bool IsNeoTerminal { get; set; }

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        public CDBFunctions()
        {            
        }
        #endregion

        /// <summary>
        /// Only function where CDBFunction instance is created.
        /// </summary>
        /// <returns></returns>
        public static CDBFunctions GetInstance()
        {
            lock (typeof(CDBFunctions))
            {
                if (m_DBFunctions == null)
                {
                    m_DBFunctions = new CDBFunctions();
                    m_DBFunctions.IsNeoTerminal = m_DBFunctions.IsWEC7Version();
#if (WindowsCE)
                        m_DBFunctions.m_DirectoryFolder = string.Format("{0}",System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase));
#else
                    m_DBFunctions.m_DirectoryFolder = System.Windows.Forms.Application.StartupPath;
#endif

                        m_DBFunctions.m_Database = new CLocalDatabase(m_DBFunctions.m_DirectoryFolder);
                    m_DBFunctions.InitializeData();
                }
                return m_DBFunctions;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void InitializeData()
        {
            m_LocalDatabase = (CLocalDatabase)m_Database;
            m_LocalDatabase.CreateInternalDataTable();
            m_LocalDatabase.LoadInternalDatatable();
        }
        /// <summary>
        /// Method is used for connect to database.
        /// </summary>
        /// <returns></returns>
        public bool ConnectDatabase()
        {
            bool result = m_Database.ConnectToDatabase();
            return result;
        }
        /// <summary>
        /// Method is used for Disconnect to database.
        /// </summary>
        public void DisconnectDatabase()
        {
            m_Database.DisconnectToDatabase();
        }
        /// <summary>
        /// Add record to database using object.
        /// </summary>
        /// <param name="prodObj"></param>
        /// <returns></returns>
        public bool AddRecord(CProductDetail prodObj)
        {
            DataRow row = m_LocalDatabase.AddRow(prodObj);
            bool result = m_Database.AddRecord(constproductTableName, row);
            return result;
        }
        /// <summary>
        ///  update record to database.
        /// </summary>
        /// <param name="prodObj"></param>
        /// <returns></returns>
        public bool UpdateRecord(CProductDetail prodObj)
        {
            bool result =m_LocalDatabase.UpdateProducttable(prodObj);
            return result;
        }
        /// <summary>
        /// This method is used to get max of row count from database.
        /// </summary>
        /// <returns></returns>
        public int GetLastRowCount()
        {
            int count = m_Database.GetLastRowCount(constproductTableName, constproductColumnName);
            return count;
        }

        /// <summary>
        /// This method is used to get Total records count from database.
        /// </summary>
        /// <returns></returns>
        public int GetRowCount()
        {
            int count = m_Database.GetRowCount(constproductTableName);
            return count;
        }
        /// <summary>
        /// Method is used for select records for particular range from database.
        /// </summary>
        /// <param name="rangeFrom"></param>
        /// <param name="rangeTo"></param>
        /// <returns></returns>
        public DataTable SearchRecord(int rangeFrom, int rangeTo)
        {
            DataTable dt = new DataTable();
           dt = m_Database.SearchRecord(constproductTableName, rangeFrom, rangeTo, constproductColumnName);
           return dt;
        }
        /// <summary>
        /// Method is used for select particular record from database.
        /// </summary>
        /// <param name="nSelectNo"></param>
        /// <returns></returns>
        public DataTable SelectRecord(int nSelectNo)
        {
            DataTable dt = new DataTable();
            dt = m_Database.SelectRecord(constproductTableName, nSelectNo, constproductColumnName);
            return dt;
        }
        /// <summary>
        /// Method is used for get all records from ddatabase.
        /// </summary>
        /// <returns></returns>
        public DataTable ViewRecord()
        {
            DataTable dt = new DataTable();
            dt = m_Database.ViewRecord(constproductTableName,constproductColumnName);
            return dt;
        }
        /// <summary>
        /// Method is used for delete record from database.
        /// </summary>
        /// <param name="nRecordNo"></param>
        /// <returns></returns>
        public bool DeleteRecord(int nRecordNo)
        {
            bool result = m_Database.DeleteRecord(constproductTableName, nRecordNo, constproductColumnName);
            return result;
        }
        /// <summary>
        /// Method is used for select row using RecordNo.
        /// </summary>
        /// <param name="nSelectNo"></param>
        /// <returns></returns>
        public CProductDetail SelectRow(int nSelectNo)
        {
            DataRow row = m_Database.SelectParticularRecord(constproductTableName, nSelectNo, constproductColumnName);
            return ReadValue(row);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool IsWEC7Version()
        {
            string imageVersion = ReadRegistryKey(REGKEY_IND890, KEY_IMAGE_VERSION_CE7, "0");
            return (imageVersion == "0") ? false : true; // if imageVersion is "0" then it is a CE6.0 version
        }
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

        private CProductDetail ReadValue(DataRow row)
        {
            CProductDetail prodObj = new CProductDetail();
            if (row != null)
            {
                prodObj.ProductNumber = Convert.ToInt32(row["RecordNo"].ToString());
                prodObj.ProductName = row["ProductName"].ToString();
                prodObj.ProductWeight = row["ProductWeight"].ToString();
                prodObj.ActualProductWeight = row["ActualProductWeight"].ToString();
                prodObj.Scale = Convert.ToByte(row["Scale"].ToString());
            }
            return prodObj;
        }
    }
}
