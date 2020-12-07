using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlServerCe;
using System.Data.SqlClient;
using System.IO;

namespace MTTS.IND890.CE
{
    public class CLocalDatabase : IDatabase
    {
        #region constant
        private const string constDataDirectoryPath = "ProductDB.sdf";
        private const string constDatabasePassword = "tspl";

        public const string constProductDetailTableName = "ProductDetail";
        #endregion
        private DataTable m_ProductDetailDataTable;
        private string SqlDatabaseConnectionString;
        private SqlCeConnection m_SqlConnection;
        private string m_DatabaseSourcePath;

        #region Constructor
        public CLocalDatabase(string sDirectoryFolder)
        {
            CreateDatabase(sDirectoryFolder);
        }
        #endregion

        #region public methods
        /// <summary>
        /// Create connectionstring for connect to database.
        /// </summary>
        public void CreateDatabase(string sDirectoryFolder)
        {
            m_DatabaseSourcePath = string.Format("{0}\\{1}", sDirectoryFolder, constDataDirectoryPath);
            if (SqlDatabaseConnectionString == null)
                SqlDatabaseConnectionString = string.Format("Data Source = '{0}'; LCID=1033; Password = '{1}'; Encrypt = FALSE; Max Buffer Size = 1024", m_DatabaseSourcePath, constDatabasePassword);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool ConnectToDatabase()
        {
            try
            {
                if (!File.Exists(m_DatabaseSourcePath))
                {
                    return false;
                }
                if (m_SqlConnection == null)
                {
                    m_SqlConnection = new SqlCeConnection(SqlDatabaseConnectionString);
                }
                if (m_SqlConnection.State != ConnectionState.Open)
                    m_SqlConnection.Open();

                if (m_SqlConnection.State == ConnectionState.Broken || m_SqlConnection.State == ConnectionState.Closed)
                    return false;
            }
            catch (InvalidOperationException ex)
            {
                string a = ex.Message;
            }
            catch (SqlException ex1)
            {
                string a = ex1.Message;
            }
            catch (Exception ex2)
            {
                string a = ex2.Message;
            }

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        public void CreateInternalDataTable()
        {
            CreateProductDetailTable();
        }

        /// <summary>
        /// 
        /// </summary>
        public void LoadInternalDatatable()
        {
            bool status = FillDataTable(m_ProductDetailDataTable, constProductDetailTableName);
        }

        #region Add Record
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sTableName"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        //SQL Query to Add
        //"INSERT into ProductDetail (RecordNo,ProductName,ProductWeight,ActualProductWeight,Scale,DateTime) Values (1,'XYZ','150 kg','140 kg',2,'12/9/2017 1:23 PM'); "
        public bool AddRecord(string sTableName, DataRow row)
        {
            SqlCeDataAdapter adapter = null;
            StringBuilder sbInsertColumns;
            StringBuilder sbInsertValues;
            try
            {
                sbInsertColumns = new StringBuilder(1024);
                sbInsertValues = new StringBuilder(1024);
                string sInsertQuery = string.Empty;
                foreach (DataColumn objColumn in row.Table.Columns)
                {
                    sbInsertColumns.Append(objColumn.ToString());
                    switch (objColumn.DataType.ToString())
                    {
                        case "System.Long":
                        case "System.Double":
                        case "System.Int16":
                        case "System.Int32":
                        case "System.Boolean":
                        case "System.Byte":
                        case "System.DateTime":
                        case "System.Guid":
                        case "System.String":
                            sbInsertValues.Append(string.Format("'{0}'", row[objColumn].ToString()));
                            break;
                        default:
                            sbInsertValues.Append(string.Format("{0}", row[objColumn].ToString()));
                            break;
                    }
                    sbInsertValues.Append(",");
                    sbInsertColumns.Append(",");
                }
                sbInsertValues.Remove(sbInsertValues.Length - 1, 1);
                sbInsertColumns.Remove(sbInsertColumns.Length - 1, 1);
                sInsertQuery = string.Format("Insert into {0}({1}) values ({2})", sTableName, sbInsertColumns, sbInsertValues);

                return ExecuteQuery(sInsertQuery);
            }
            catch
            {
#if DEBUG
                System.Diagnostics.Debug.WriteLine("Exception in CDatabase.AddRecord() ");
                System.Windows.Forms.MessageBox.Show("Exception in CDatabase.AddRecord() ");
#endif
                return false;
            }

        }
        #endregion

        #region Search Record
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sTableName"></param>
        /// <param name="startRecNo"></param>
        /// <param name="endRecNo"></param>
        /// <param name="sColumnName"></param>
        /// <returns></returns>
        // SQL Query to search
        // "SELECT * FROM ProductDetail WHERE RecordNo BETWEEN 4 AND 8;"
        public DataTable SearchRecord(string sTableName, int startRecNo, int endRecNo, string sColumnName)
        {
            string query = string.Format("SELECT * FROM {0} WHERE {1} BETWEEN {2} AND {3}", sTableName, sColumnName, startRecNo, endRecNo);
            using (DataTable dt = new DataTable())
            {
                DataRow row = null;
                try
                {
                    if (m_SqlConnection == null)
                    {
                        m_SqlConnection = new SqlCeConnection(GetConnectionString(sTableName));
                    }

                    if (m_SqlConnection.State == ConnectionState.Broken || m_SqlConnection.State == ConnectionState.Closed)
                    {
                        m_SqlConnection.Open();
                    }

                    using (SqlCeDataAdapter adpater = new SqlCeDataAdapter(query, m_SqlConnection))
                    {
                        adpater.Fill(dt);
                    }
                }
                catch (Exception ex)
                {
#if DEBUG
                    System.Diagnostics.Debug.WriteLine(ex.StackTrace);
#endif
                }
                return dt;
            }
        }
        #endregion

        #region Select Record
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sTableName"></param>
        /// <param name="nRecordNo"></param>
        /// <param name="sColumnName"></param>
        /// <returns></returns>
        // SQL Query to Select
        // "SELECT * FROM ProductDetail WHERE RecordNo = 3;"
        public DataTable SelectRecord(string sTableName, int nRecordNo, string sColumnName)
        {
            string query = string.Format("SELECT * FROM {0} WHERE {1} = {2}", sTableName, sColumnName, nRecordNo);
            using (DataTable dt = new DataTable())
            {
                DataRow row = null;
                try
                {
                    if (m_SqlConnection == null)
                    {
                        m_SqlConnection = new SqlCeConnection(GetConnectionString(sTableName));
                    }

                    if (m_SqlConnection.State == ConnectionState.Broken || m_SqlConnection.State == ConnectionState.Closed)
                    {
                        m_SqlConnection.Open();
                    }

                    using (SqlCeDataAdapter adpater = new SqlCeDataAdapter(query, m_SqlConnection))
                    {
                        adpater.Fill(dt);
                    }
                }
                catch (Exception ex)
                {
#if DEBUG
                    System.Diagnostics.Debug.WriteLine(ex.StackTrace);
#endif
                }
                return dt;
            }
        }
        #endregion
        #region Delete Record
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sTableName"></param>
        /// <param name="nRecordNo"></param>
        /// <param name="sColumnName"></param>
        /// <returns></returns>
        // SQL Query to delete
        // "DELETE FROM ProductDetail WHERE RecordNo = 4;"
        public bool DeleteRecord(string sTableName, int nRecordNo, string sColumnName)
        {
            string query = string.Format("DELETE FROM {0} WHERE {1}={2}", sTableName, sColumnName, nRecordNo);
            using (DataTable dt = new DataTable())
            {
                try
                {
                    if (m_SqlConnection == null)
                    {
                        m_SqlConnection = new SqlCeConnection(GetConnectionString(sTableName));
                    }

                    if (m_SqlConnection.State == ConnectionState.Broken || m_SqlConnection.State == ConnectionState.Closed)
                    {
                        m_SqlConnection.Open();
                    }
                }
                catch (Exception ex)
                {
#if DEBUG
                    System.Diagnostics.Debug.WriteLine(ex.StackTrace);
                    return false;
#endif
                }
                return ExecuteQuery(query);
            }
        }
        #endregion
        #region View Records
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sTableName"></param>
        /// <returns></returns>
        // SQL Query to Select all records
        // "SELECT * FROM ProductDetail;"
        public DataTable ViewRecord(string sTableName, string sColumnName)
        {
            string query = string.Format("SELECT * FROM {0} ORDER BY {1}", sTableName,sColumnName);
            using (DataTable dt = new DataTable())
            {
                DataRow row = null;
                try
                {
                    if (m_SqlConnection == null)
                    {
                        m_SqlConnection = new SqlCeConnection(GetConnectionString(sTableName));
                    }

                    if (m_SqlConnection.State == ConnectionState.Broken || m_SqlConnection.State == ConnectionState.Closed)
                    {
                        m_SqlConnection.Open();
                    }

                    using (SqlCeDataAdapter adpater = new SqlCeDataAdapter(query, m_SqlConnection))
                    {
                        adpater.Fill(dt);
                    }
                }
                catch (Exception ex)
                {
#if DEBUG
                    System.Diagnostics.Debug.WriteLine(ex.StackTrace);
#endif
                }
                return dt;
            }
        }
        #endregion
        #region LastRowCount
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sTableName"></param>
        /// <returns></returns>
        // SQL Query to select Last record Count
        // "SELECT MAX(RecordNo) FROM ProductDetail;"
        public int GetLastRowCount(string sTableName, string sColumnName)
        {
            SqlCeCommand cmd = null;
            SqlCeDataReader reader = null;
            int value = 0;
            string query = string.Empty;
            SqlCeConnection SqlConnection = null;
            try
            {
                if (SqlConnection == null)
                {
                    string connectionString = GetConnectionString(sTableName);
                    SqlConnection = new SqlCeConnection(connectionString);
                }
                if (SqlConnection.State == ConnectionState.Broken || SqlConnection.State == ConnectionState.Closed)
                {
                    SqlConnection.Open();
                }
                query = string.Format("select MAX({0}) from {1}", sColumnName, sTableName);

                cmd = new SqlCeCommand(query, SqlConnection);
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    value = reader.GetInt32(0);
                }
            }
            catch (Exception ex)
            {
#if DEBUG
                System.Diagnostics.Debug.WriteLine(String.Format(" LoadDataTable failed while loading - the datatable. StackTrace = {0} Message {1}\nQuery = {2}", ex.StackTrace, ex.Message, query));
#endif
                value = -1;
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                    reader = null;
                }
                if (cmd != null)
                {
                    cmd.Dispose();
                    cmd = null;
                }
                if (SqlConnection != null)
                {
                    SqlConnection.Close();
                    SqlConnection = null;
                }
            }
            return value;
        }
        #endregion
        
        #region RowCount
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sTableName"></param>
        /// <returns></returns>
        // SQL Query to select Last record Count
        // "SELECT * FROM ProductDetail;"
        public int GetRowCount(string sTableName)
        {
            SqlCeCommand cmd = null;
            SqlCeDataReader reader = null;
            int value = 0;
            string query = string.Empty;
            SqlCeConnection SqlConnection = null;
            try
            {
                if (SqlConnection == null)
                {
                    string connectionString = GetConnectionString(sTableName);
                    SqlConnection = new SqlCeConnection(connectionString);
                }
                if (SqlConnection.State == ConnectionState.Broken || SqlConnection.State == ConnectionState.Closed)
                {
                    SqlConnection.Open();
                }
                query = string.Format("select * from {0}", sTableName);

                cmd = new SqlCeCommand(query, SqlConnection);
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    value = reader.GetInt32(0);
                }
            }
            catch (Exception ex)
            {
#if DEBUG
                System.Diagnostics.Debug.WriteLine(String.Format(" LoadDataTable failed while loading - the datatable. StackTrace = {0} Message {1}\nQuery = {2}", ex.StackTrace, ex.Message, query));
#endif
                value = -1;
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                    reader = null;
                }
                if (cmd != null)
                {
                    cmd.Dispose();
                    cmd = null;
                }
                if (SqlConnection != null)
                {
                    SqlConnection.Close();
                    SqlConnection = null;
                }
            }
            return value;
        }
        #endregion
        #region Select Particular Record
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sTableName"></param>
        /// <param name="nRecordNo"></param>
        /// <param name="sColumnName"></param>
        /// <returns></returns>
        // SQL Query to Select particular Record
        // "SELECT * FROM ProductDetail WHERE RecordNo = 4;"
        public DataRow SelectParticularRecord(string sTableName, int nRecordNo, string sColumnName)
        {
            string query = string.Format("SELECT * FROM {0} WHERE {1} = {2}", sTableName, sColumnName, nRecordNo);
            using (DataTable dt = new DataTable())
            {
                DataRow row = null;
                try
                {
                    if (m_SqlConnection == null)
                    {
                        m_SqlConnection = new SqlCeConnection(GetConnectionString(sTableName));
                    }

                    if (m_SqlConnection.State == ConnectionState.Broken || m_SqlConnection.State == ConnectionState.Closed)
                    {
                        m_SqlConnection.Open();
                    }

                    using (SqlCeDataAdapter adpater = new SqlCeDataAdapter(query, m_SqlConnection))
                    {
                        adpater.Fill(dt);
                    }
                    if (dt.Rows.Count > 0) row = dt.Rows[0];
                }
                catch (Exception ex)
                {
#if DEBUG
                    System.Diagnostics.Debug.WriteLine(ex.StackTrace);
#endif
                }
                return row;
            }
        }
        #endregion

        /// <summary>
        /// Disconnect from database.
        /// </summary>
        public void DisconnectToDatabase()
        {
            if (m_SqlConnection != null)
            {
                if (m_SqlConnection.State == ConnectionState.Open)
                {
                    m_SqlConnection.Close();
                    m_SqlConnection.Dispose();
                    m_SqlConnection = null;
                }
            }
        }

        public bool ExecuteQuery(string sQuery)
        {
            SqlCeCommand cmd = null;

            try
            {
                if (m_SqlConnection != null)
                {
                    if (m_SqlConnection.State == ConnectionState.Open)
                    {
                        cmd = new SqlCeCommand(sQuery, m_SqlConnection);
                        cmd.ExecuteNonQuery();
                    }

                }
            }
            catch (Exception e)
            {
#if DEBUG
                System.Windows.Forms.MessageBox.Show(string.Format("Exception has raised in - Updatetable {0}- Query {1}", e.StackTrace,sQuery));
#endif
                return false;
            }
            finally
            {
                if (cmd != null)
                {
                    cmd.Dispose();
                    cmd = null;
                }
            }
            return true;
        }
        #region Update Record
        /// <summary>
        ///  Update record to database.
        /// </summary>
        /// <param name="prodObj"></param>
        /// <returns></returns>
        // SQL Query to Update
        // "UPDATE ProductDetail SET ProductName = 'XYZ',ProductWeight = '150 kg',ActualProductWeight = '120 kg',Scale = 2,DateTime ='11/9/2017 1:23 PM' WHERE RecordNo =1"
        public bool UpdateProducttable(CProductDetail prodObj)
        {
            string query = string.Format("UPDATE {0} SET ProductName = '{1}',ProductWeight = '{2}',ActualProductWeight = '{3}',Scale = {4} WHERE RecordNo ={5}", constProductDetailTableName, prodObj.ProductName, prodObj.ProductWeight, prodObj.ActualProductWeight, prodObj.Scale, prodObj.ProductNumber);
            try
            {
                if (m_SqlConnection == null)
                {
                    m_SqlConnection = new SqlCeConnection(GetConnectionString(constProductDetailTableName));
                }

                if (m_SqlConnection.State == ConnectionState.Broken || m_SqlConnection.State == ConnectionState.Closed)
                {
                    m_SqlConnection.Open();
                }
            }
            catch (Exception ex)
            {
#if DEBUG
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
                return false;
#endif
            }
            return ExecuteQuery(query);
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prodObj"></param>
        /// <returns></returns>
        public DataRow AddRow(CProductDetail prodObj)
        {
            DataRow dr = m_ProductDetailDataTable.NewRow();
            dr["RecordNo"] = prodObj.ProductNumber;
            dr["ProductName"] = prodObj.ProductName;
            dr["ProductWeight"] = prodObj.ProductWeight;
            dr["ActualProductWeight"] = prodObj.ActualProductWeight;
            dr["Scale"] = prodObj.Scale;
            m_ProductDetailDataTable.Rows.Add(dr);
            return dr;
        }
        #endregion

        #region Private methods
        private string GetConnectionString(string tableName)
        {
            string connectionString = null;
            if (string.Compare(tableName, constProductDetailTableName) == 0)
            {
                connectionString = SqlDatabaseConnectionString;
            }
            return connectionString;
        }
        //Create Internal product detail table.
        /// <summary>
        /// 
        /// </summary>
        private void CreateProductDetailTable()
        {
            m_ProductDetailDataTable = new DataTable(constProductDetailTableName);
            m_ProductDetailDataTable.Columns.Add("RecordNo", typeof(int));
            m_ProductDetailDataTable.Columns.Add("ProductName", typeof(string));
            m_ProductDetailDataTable.Columns.Add("ProductWeight", typeof(string));
            m_ProductDetailDataTable.Columns.Add("ActualProductWeight", typeof(string));
            m_ProductDetailDataTable.Columns.Add("Scale", typeof(byte));
            m_ProductDetailDataTable.PrimaryKey = new DataColumn[] { m_ProductDetailDataTable.Columns["RecordNo"] };
        }

        private bool FillDataTable(DataTable dt, string DBTableName)
        {
            SqlCeConnection connection = null;
            SqlCeDataAdapter da = null;
            string Query = string.Empty;
            string connectionString = string.Empty;

            if (DBTableName.Equals(constProductDetailTableName))
            {
                Query = string.Format("SELECT * FROM {0}", DBTableName);
            }
            try
            {
                connectionString = GetConnectionString(DBTableName);
                if (connectionString.Equals(SqlDatabaseConnectionString))
                {
                    connection = new SqlCeConnection(connectionString);
                    connection.Open();

                    da = new SqlCeDataAdapter(Query, connection);
                }
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(String.Format(" LoadDataTable failed while loading - {0}StackTrace = {1} Message {2}\nQuery = {3}", DBTableName, ex.StackTrace, ex.Message, Query));
                return false;
            }
            finally
            {
                if (da != null) da.Dispose();
                da = null;
                if (connectionString.Equals(SqlDatabaseConnectionString))
                {
                    if (connection != null)
                    {
                        connection.Close();
                    }
                    connection = null;
                }
            }
            return true;
        }
        #endregion
    }
}
