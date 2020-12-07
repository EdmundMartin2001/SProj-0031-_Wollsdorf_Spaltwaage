using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace MTTS.IND890.CE
{
   public interface IDatabase
    {
        bool ConnectToDatabase();
        void DisconnectToDatabase();
        bool AddRecord(string sTableName, DataRow row);
        DataTable SearchRecord(string sTableName, int startRecNo, int endRecNo, string sColumnName);
        DataTable SelectRecord(string sTableName, int nRecordNo, string sColumnName);
        bool DeleteRecord(string sTableName, int nRecordNo, string sColumnName);
        DataTable ViewRecord(string sTableName,string sColumnName);
        int GetLastRowCount(string sTableName, string sColumnName);
        int GetRowCount(string sTableName);
        DataRow SelectParticularRecord(string sTableName, int nRecordNo, string sColumnName);
    }
}
