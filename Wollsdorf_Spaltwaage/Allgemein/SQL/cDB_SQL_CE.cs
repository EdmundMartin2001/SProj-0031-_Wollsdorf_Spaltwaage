namespace SMT_SQL_2V.DB.Private
{
    using System;
    using System.Collections;
    using System.Data;
    using System.Data.SqlServerCe;
    using System.Diagnostics;
    using System.IO;
    using System.Windows.Forms;
    using Allgemein;
    
    public enum eSQLFieldeType
    {
        Bit = 0,
        VarChar = 1,
        Float = 2,
        SmallInt = 3,
        DateTim = 4,
        BigInt = 5,
        UniqueIDNT = 6
    };
    public enum eSQLNullType
    {
        NotNull = 0,
        NuLl = 1
    };
    public enum enumDeleteError
    {        
        Delete_Okay = 0,        
        InVerwendung = 429,        
        Unbekannt = 9999,
    };

    internal class cDB_SQL_CE
    {
        private SqlCeConnection CONN	= null;
        private SqlCeDataReader DR	    = null;
        private DataSet data		    = null;
        private int iAnzahl			= 0;
        private int iMom			= 0;
        public bool bOk			    = true;
        private ArrayList           _FieldName	= new ArrayList(0);
        private ArrayList           _FieldValue	= new ArrayList(0);
        private string		        _sFunctName = "";	// wir für mögliche Errormeldungen benötigt
        private int			        _iFunctID   = 0;
        
        /*Für Static wird ein eigener Connectionstring gepuffert */
        private string	_sConnStr			   = "";				
        private static string _sConnStaticStr  = "";

        private System.Globalization.CultureInfo ci;
                        
        public cDB_SQL_CE(bool bTempTest)
        {
            this.ci = System.Globalization.CultureInfo.InvariantCulture;
            _sConnStr = cDB_Settings.CE_ConnectionString; 		
            bool b = SQL_OPEN();            
        }
         
        public cDB_SQL_CE(string ConnectionString)
        {
            _sConnStr = ConnectionString;
        
            bool b = SQL_OPEN();
        }		

        public cDB_SQL_CE(string ConnectionString, string Funktionsname, int FunctID)
        {
            _sConnStr	= ConnectionString;
            _sFunctName = Funktionsname;
            _iFunctID	= FunctID;
        
            bool b = SQL_OPEN();
        }		

        #region SQL_OPEN (Prüfe SQL Server Verfügbarkeit und gebe Fehlermeldung aus)
        /// <summary>
        /// Versuche eine SQL Server Connection zu erhalten. Ist dieser Vorgang nicht erfolgreich,
        /// dann erfolgt sofort eine Detailierte Fehlermeldung
        /// </summary>
        /// <returns></returns>
        private bool SQL_OPEN()
        {
            try
            {	
                if ( _sConnStr.Length <=0)
                {
                    Trace.WriteLine("SMT_SQL =>{Connectionstring ist leer}"); 
                    MessageBox.Show ("{Connectionstring ist leer}", SETTINGS.ApplikationName);
                    return false;
                }

                string sx = string.Format("DataSource={0}", _sConnStr); 
                
                CONN = new SqlCeConnection(sx);
                if(CONN.State != ConnectionState.Open) { CONN.Open(); }

                SETTINGS.SQLTestCounter++;
                Trace.WriteLine("SQL " + SETTINGS.SQLTestCounter);
            }
            catch (SqlCeException ex) 
            {
                Trace.WriteLine("SQL Exception");
                Trace.WriteLine(ex.Message);

                if (ex.NativeError == 25017)
                {
                    MessageBox.Show(
                        "Folgender Fehler ist bei der Datenbankverbindung aufgetreten:\r\n\r\n ["
                        + ex.Message.ToString()
                        + "\r\n\r\n"
                        + "=========================================\r\n"
                        + "(siehe Fehlercode 25017)",
                        "Fehler 25017");
                    return false;
                }
                else
                {
                    MessageBox.Show(
                        "Folgender Fehler ist bei der Datenbankverbindung aufgetreten:\r\n\r\n ["
                        + ex.Message.ToString()
                        + "\r\n\r\n"
                        + "=========================================\r\n"
                        + "Die 'Data/*.sdf' Datei im Projekt muss ausgechecket sein, oder \r\n"
                        + "aus dem Deploy ausgenommen werden (siehe Fehlercode 25039)",
                        "Fehler 25039");
                }
                return false;

            }
            catch (Exception ex)
            {
                Trace.WriteLine("Exception in SQL OPEN");
                Trace.WriteLine(ex.Message);
                MessageBox.Show (ex.ToString() );
                return false;
            }

            return true;			
        }
        #endregion

        public bool Exec(string s)
        {
            return saveExec(s,CONN);
        }
        public bool Exec(string s, string sFunktionsName, int iFunktionsID)
        {
            return saveExec(s , CONN, sFunktionsName , iFunktionsID );
        }
        
        #region ADD (Überladungen)
  
        public void ADD(string sFieldName, string sFieldValue)
        {
            _FieldName.Add	 (sFieldName);
            _FieldValue.Add (sFieldValue==null ? "null":"'" + sFieldValue.Replace('\'', '~') + "'");
        }

        public void ADD(string sFieldName, TextBox Tb)
        {
            _FieldName.Add	 (sFieldName);
            _FieldValue.Add (Tb.Text==null || Tb.Text.Equals("") ? "null":"'" + Tb.Text + "'");
        }

        public void ADDNOW(string sFieldName)
        {
            _FieldName.Add	 (sFieldName);
            _FieldValue.Add ("getdate()");
        }

        public void ADD(string sFieldName, int iFieldValue)
        {
            _FieldName.Add	 (sFieldName);
            _FieldValue.Add ("" + iFieldValue);
        }

        public void ADD(string sFieldName, bool bFieldValue)
        {
            _FieldName.Add	 (sFieldName);
            _FieldValue.Add ("" + (bFieldValue ? 1:0));
        }

        public void ADD(string sFieldName, Double dFieldValue)
        {
            _FieldName.Add			(sFieldName);
            _FieldValue.Add			( "" + dFieldValue.ToString().Replace(",","."));	
        }

        public void ADD(string sFieldName, DateTime dtFieldValue)
        {
            _FieldName.Add			(sFieldName);
            _FieldValue.Add			(dtFieldValue == DateTime.MinValue ? "null":" " + DATE_TIME_TO_DB(dtFieldValue) + " ");			
        }	
        
        //		public void ADD(string sFieldName, CTRL_COMBO_BUTTON.cmb_ComboButton cmb)
        //		{
        //			_FieldName.Add (sFieldName);
        //			if ( cmb.Combo.SelectedItem == null) 
        //				_FieldValue.Add ("null");
        //			else 
        //				_FieldValue.Add ( " " + ((ComboBoxObject)cmb.Combo.SelectedItem).Get_Id.ToString() + " " ); 
        //		}

        #endregion

        #region INSERT - Create Insert Command like INSERT INTO.....

        public int INSERT(string sTable,bool bAic)
        {
            string s = "INSERT INTO " + sTable + "(" + _FieldName[0];
    
            for(int i=1; i<_FieldName.Count; i++)
                s = s + "," + _FieldName[i];

            s = s + ") VALUES (" + _FieldValue[0];

            for(int i=1; i<_FieldValue.Count; i++)
                s = s + "," + _FieldValue[i];

            s = s + ")";
            
            RESET();

            if (Exec(s))
            {
                if (bAic)
                {
                    //OPEN("SELECT SCOPE_IDENTITY () x");
                    //int i1 = getI("x");

                    OPEN("SELECT @@identity x");
                    int i2 = getI("x");

                    //if (i1 != i2) { SiAuto.LogError("ERROR IDENTITIER NICHT GLEICH"); }
                    return i2;
                }
                else
                    return -1;
            }
            else
                return -2;

        }
        #endregion

        #region INSERT THROW - Create Insert Command like INSERT INTO.....

        public int INSERT_THROW(string sTable,bool bAic, string sFunktionsName, int iFunktionsID)
        {
            string s = "INSERT INTO " + sTable + "(" + _FieldName[0];
    
            for(int i=1; i<_FieldName.Count; i++)
                s = s + "," + _FieldName[i];

            s = s + ") VALUES (" + _FieldValue[0];

            for(int i=1; i<_FieldValue.Count; i++)
                s = s + "," + _FieldValue[i];

            s = s + ")";

            //SiAuto.LogSql("{==>Helper_SQL.InsertThrow}", s);
            
            RESET();

            if (ThrowExec(s, CONN, sFunktionsName, iFunktionsID))
            {
                if (bAic)
                {
                    OPEN("SELECT @@identity x");
                    int i2 = getI("x");
                    //SiAuto.LogInt("{==>Helper_SQL.Insert} identity = ", i2);                    
                    return i2;
                }
                else
                    return -1;
            }
            else
                return -2;

        }
        #endregion

        #region UPDATE - Create Update Command

        #region Reguläres Update
        public bool UPDATE(string sTable, string sAIC /*AIC Feldname*/ ,int iAic /*AIC Wert*/)
        {
            string s = "UPDATE "+sTable+" SET "+_FieldName[0]+"="+_FieldValue[0];

            for(int i=1; i<_FieldName.Count; i++)
                s = s + "," + _FieldName[i]+"="+_FieldValue[i];

            s = s + " WHERE " + sAIC + "=" + iAic;
             
            RESET();
            return Exec(s);
        }

        public bool UPDATE(string sTable,string sAIC /*AIC Feldname*/ ,string iAic /*AIC Wert*/)
        {
            string s = "UPDATE "+sTable+" SET "+_FieldName[0]+"="+_FieldValue[0];

            for(int i=1; i<_FieldName.Count; i++)
                s = s + "," + _FieldName[i]+"="+_FieldValue[i];

            s = s + " WHERE " + sAIC + "= '" + iAic  + "'" ;
             
            RESET();
            return Exec(s);
        }
        #endregion

        public bool UPDATE_THROW(string sTable, string sAIC /*AIC Feldname*/ , int iAic /*AIC Wert*/, string sFunktionsName, int iFunktionsID)
        {
            string s = "UPDATE " + sTable + " SET " + _FieldName[0] + "=" + _FieldValue[0];

            for (int i = 1; i < _FieldName.Count; i++)
                s = s + "," + _FieldName[i] + "=" + _FieldValue[i];

            s = s + " WHERE " + sAIC + "= '" + iAic + "'";

            //SiAuto.LogSql("{==>Helper_SQL.UpdateThrow}", s);
            
            RESET();

            if (ThrowExec(s, CONN, sFunktionsName, iFunktionsID))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool UPDATE_THROW(string sTable, 
                                 string sAIC /*AIC Feldname*/ , 
                                 string sAic /*AIC Wert*/, 
                                 string sFunktionsName, int iFunktionsID )
        {
         
            string s = "UPDATE " + sTable + " SET " + _FieldName[0] + "=" + _FieldValue[0];

            for (int i = 1; i < _FieldName.Count; i++)
                s = s + "," + _FieldName[i] + "=" + _FieldValue[i];

            s = s + " WHERE " + sAIC + "= '" + sAic + "'";

            //SiAuto.LogSql("{==>Helper_SQL.UpdateThrow}", s);

            RESET();
 

            if (ThrowExec(s, CONN, sFunktionsName, iFunktionsID))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region Zugriff auf Column Auflistung (GetColCount, GetColName, GetColTypeName)

        public int Column_GetColCount()
        {
            return data.Tables[0].Columns.Count; 
        }

        public string Column_GetColName(int ColIndex)
        {
            return data.Tables[0].Columns[ColIndex].ToString();
        }

        public string Column_GetColTypeName(int ColIndex)
        {
            return data.Tables[0].Columns[ColIndex].DataType.Name;
        }

        #endregion


        public int Anzahl
        {
            get
            {
                return iAnzahl;
            }
        }
        public int Index
        {
            get
            {
                return iMom;
            }
        }
        public void RESET()
        {
            _FieldName.Clear();
            _FieldValue.Clear();
        }
        public void FREE()
        {
            try
            {
                if (data != null)
                    data.Dispose();

                if (DR != null)
                    DR.Close();

                CONN.Dispose();
            }
            catch (Exception ex)
            {
                SiAuto.LogError("Fehler bei FREE");
                SiAuto.LogException(ex);
            }

            SETTINGS.SQLTestCounter--;
            //SiAuto.LogInt("SQL", SETTINGS.SQLTestCounter);
        }
        public void moveFirst()
        {
            iMom=0;
        }
        public void moveNext()
        {
            iMom++;
        }
        public void moveLast()
        {
            iMom = iAnzahl;
        }
        public void movePrev()
        {
            if (iMom>0)
                iMom--;
        }
        public bool eof()
        {
            return iMom>=iAnzahl;
        }
        public bool OPENTHROW(string s)
        {
            if(CONN.State != ConnectionState.Open) return false;
            
            data = new DataSet();
            SqlCeDataAdapter DA = new SqlCeDataAdapter(s, CONN);
            try
            {
                DA.Fill (data);
                bOk=true;
            }
            catch (SqlCeException e)
            {
                PrintError(e, s, _sFunctName, _iFunctID);
                bOk=false;
                throw;
            }

            DA.Dispose();
            if (bOk)
                iAnzahl=data.Tables[0].Rows.Count;
            else
                iAnzahl=-1;
            iMom=0;
            return iAnzahl>0;
        }

        public bool OPEN(string s)
        {
            if(CONN.State != ConnectionState.Open) return false;
            
            data = new DataSet();
            SqlCeDataAdapter DA = new SqlCeDataAdapter(s, CONN);
            try
            {
                DA.Fill (data);
                bOk=true;
            }
            catch (SqlCeException e)
            {
                PrintError(e, s, _sFunctName, _iFunctID);
                bOk=false;
            }
            DA.Dispose();
            if (bOk)
                iAnzahl=data.Tables[0].Rows.Count;
            else
                iAnzahl=-1;
            iMom=0;
            return iAnzahl>0;
        }
        public string OPEN2(System.Data.DataTable DT,string s)
        {
            SqlCeDataAdapter DA = new SqlCeDataAdapter(s, CONN);
            string sStatus="";
            try
            {
                DA.Fill(DT);
            }
            catch (SqlCeException e)
            {
                sStatus=e.Message;
            }
            DA.Dispose();
            return sStatus;	
        } 
        public string EXEC2(string s)
        {
            SqlCeCommand sqlCommand = new SqlCeCommand(s, CONN);
            try
            {
                sqlCommand.ExecuteNonQuery();
                return "ok";
            }
            catch (SqlCeException e)
            {
                return e.Message;
            }
        }
        
        #region GetXXX Funktionen - GetS, GetI, GetB, GetD .....
        public object getO(string s)
        {
            if (eof())
                return null;

            try
            {
                return data.Tables[0].Rows[iMom][s];
            }
            catch (Exception ex)
            {
                SiAuto.LogException("Exeption in {cDB_SQL} Funktion getO", ex);
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        public string getS(string s)
        {
            if (eof())
                return "";

            try
            {
                object o = data.Tables[0].Rows[iMom][s];
                return o == Convert.DBNull ? "":o.ToString();
            }
            catch (Exception Ex)
            {
                SiAuto.LogError("Exeption in {cDB_SQL} Funktion GetS");
                SiAuto.LogException(Ex);

                MessageBox.Show(Ex.Message );
                return "";
            }			
        }

        /// <summary>
        /// Liefert bei NULL auch Tasächlich NULL zurück und nicht
        /// wie GetS einen Leerstring
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public string getS_Null(string s)
        {
            if (eof())
                return "";

            try
            {
                object o = data.Tables[0].Rows[iMom][s];
                return o == Convert.DBNull ? null:o.ToString();
            }
            catch (Exception Ex)
            {
                SiAuto.LogError("Exeption in {cDB_SQL} Funktion GetS");
                SiAuto.LogException(Ex);

                MessageBox.Show(Ex.Message );
                return "";
            }			
        }
        public object getObj_Null(string s)
        {
            object o = null;

            if (eof())
                return 0;
                            
            try
            {
                o = data.Tables[0].Rows[iMom][s];            
                return o;
                    
            }
            catch(Exception e)
            {
                SiAuto.LogError("Exeption in {cDB_SQL} Funktion getObj_Null");
                SiAuto.LogException(e);
                return 0;
            }            
        }
        public int getI(string s)
        {
            if (eof())
                return 0;
                
            object o = data.Tables[0].Rows[iMom][s];
            if (o == Convert.DBNull ||  o.ToString().Equals(""))
                return 0;
            else
            {
                try
                {
                    return Convert.ToInt32(o.ToString());
                }
                catch(Exception e)
                {
                    SiAuto.LogError("Exeption in {cDB_SQL} Funktion GetI");
                    SiAuto.LogException(e);
                    return 0;
                }
            }
        }   
        public DateTime getD(string s)
        {
            object obj = null;

            try
            {
                if ( eof() )
                {  
                    return DateTime.MinValue;
                }
                
                obj = data.Tables[0].Rows[iMom][s];

                if ( obj == Convert.DBNull || obj.ToString().Equals("") )
                {
                    return DateTime.MinValue;
                }
                else
                { 
                    return Convert.ToDateTime(obj.ToString() , this.ci);
                }
            }
            catch ( Exception ex )
            {
                SiAuto.LogException("getD", ex);
                throw;
            }

            //if (eof())
            //    return DateTime.MinValue;
            //object o = data.Tables[0].Rows[iMom][s];
            //if (o == Convert.DBNull ||  o.ToString().Equals(""))
            //    return DateTime.MinValue;
            //else
            //    return Convert.ToDateTime(o.ToString());
        }
        public double getF(string s)
        {
            if (eof())
                return 0;
            object o = data.Tables[0].Rows[iMom][s];
            if (o == Convert.DBNull ||  o.ToString().Equals(""))
                return 0;
            else
                return Convert.ToDouble(o.ToString());
        }
        
        public byte[] GetByteArray(string s)
        {
            if (eof())
                return new byte[0];
            object o = data.Tables[0].Rows[iMom][s];
            if (o == Convert.DBNull ||  o.ToString().Equals(""))
                return new byte[0];
            else
            {
                try
                {
                    return (byte[]) o;
                }
                catch(Exception e)
                {
                    Debug.Assert (false);
                    SiAuto.LogError("Exeption in {cDB_SQL} Funktion []GetByteArray");
                    SiAuto.LogException(e);
                    return new byte[0];
                }
            } 
        }

        public bool getB(string s)
        {
            if (eof())
                return false;
            object o = data.Tables[0].Rows[iMom][s];
            if (o == Convert.DBNull ||  o.ToString().Equals(""))
                return false;
            else
                return Convert.ToInt32(o)==1;
        }

        public bool getB2(string s)
        {
            if (eof())
                return false;
            object o = data.Tables[0].Rows[iMom][s];
                
            if (o == Convert.DBNull ||  o.ToString().Equals(""))
                return false;
            else
                try
                {
                    return (bool) o;//.ToString().Equals("true");
                }
                catch(Exception e)
                {
                    SiAuto.LogError("Exeption in {cDB_SQL} Funktion getB2");
                    SiAuto.LogException(e);
                    Trace.Assert (false, "getB2" , "FieldName:" + s);
                    return false;
                }
        }
        #endregion

        #region Static Get_XXX Funktionen
        
        #region Exists - Prüfung
        /// <summary>
        /// Prüft über die Anzahl der Datensätze ob ein Ergebnis erhalten wurde
        /// </summary>
        /// <param name="Query">Der SQL Befehl</param>
        /// <returns>TRUE wenn Anzahl DS größer Null</returns>
        public static bool exists(string Query)
        {
            cDB_SQL_CE Qry = new cDB_SQL_CE(cDB_Settings.CE_ConnectionString);
            Qry.OPEN(Query);
            bool b=Qry.iAnzahl>0;
            Qry.FREE();
            return b;
        }
        
        /// <summary>
        /// Prüft über die Anzahl der Datensätze ob ein Ergebnis erhalten wurde
        /// </summary>
        /// <param name="Query">Der SQL Befehl</param>		
        /// <param name="ConnectionString">Ein gültiger Connectionstring</param>
        /// <returns>TRUE wenn Anzahl DS größer Null</returns>
        public static bool exists(string Query, string ConnectionString )
        {
            cDB_SQL_CE Qry = new cDB_SQL_CE(ConnectionString);
            Qry.OPEN(Query);
            bool b=Qry.iAnzahl>0;
            Qry.FREE();
            return b;
        }

        public static bool existsThrow(string Query, string ConnectionString )
        {
            bool b = true;
            cDB_SQL_CE Qry = null;

            try
            {
                Qry = new cDB_SQL_CE(ConnectionString);
                Qry.OPENTHROW(Query);
                b = Qry.iAnzahl > 0;
            }
            catch ( Exception )
            {
                throw;
            }
            finally
            {
                if ( Qry != null)
                {
                    Qry.FREE();
                }
            }

            return b;
        }
        #endregion

        #region getString
        public static string getString(string Query)
        {
            cDB_SQL_CE Qry=new cDB_SQL_CE(cDB_Settings.CE_ConnectionString);
            Qry.OPEN(Query);
            string s2="";
            if (Qry.iAnzahl>0) 
            {
                object o = Qry.data.Tables[0].Rows[0][0];
                s2= o == Convert.DBNull ? "":o.ToString();
            }
            Qry.FREE();
            return s2;
        }
        public static string getString(string Query, string ConnectionString)
        {
            cDB_SQL_CE Qry=new cDB_SQL_CE(ConnectionString);
            Qry.OPEN(Query);
            string s2="";
            if (Qry.iAnzahl>0) 
            {
                object o = Qry.data.Tables[0].Rows[0][0];
                s2= o == Convert.DBNull ? "":o.ToString();
            }
            Qry.FREE();
            return s2;
        }	
        #endregion
       
        #region getIntegerMax
        /// <summary>
        /// Liefere den MAX Wert einer Spalte zurück wobei das Feld vom Typ varchar sein
        /// darf und Alphanumerische Werte ignoriert werden.
        /// </summary>
        /// <param name="sTableName"></param>
        /// <param name="sFieldName"></param>
        /// <returns></returns>
        public static int getIntegerMax(string sTableName, string sFieldName)
        {
            string sSQL = "SELECT MAX( CONVERT( INTEGER, " + sFieldName + ") ) AS MAXWERT " +
                          "FROM  [" + sTableName + "] " +
                          "WHERE ISNUMERIC(" + sFieldName + ") = 1 ";

            //SiAuto.LogSql("SQL getIntegerMax", sSQL);

            string s2=getString(sSQL);
            int i=0;
            if (!s2.Equals(""))
                i=Convert.ToInt32(s2);
            return i;
        }
        #endregion	
            
        #region getInteger  
        public static int getInteger(string Query)
        {
            string s2=getString(Query);
            int i=0;
            if (!s2.Equals(""))
                i=Convert.ToInt32(s2);
            return i;
        }
        public static int getInteger(string Query, string ConnectionString)
        {
            string s2=getString(Query, ConnectionString);
            int i=0;
            if (!s2.Equals(""))
                i=Convert.ToInt32(s2);
            return i;
        }	
        #endregion	

        #region getLong 
        public static long getLongTS(string Query)
        {
            byte[] bt = new byte[8];
            Array.Reverse(bt);

            try
            {
                cDB_SQL_CE Qry=new cDB_SQL_CE(cDB_Settings.CE_ConnectionString);
                Qry.OPEN(Query);
                
            
                if (Qry.iAnzahl>0) 
                {             
                    bt = (byte []) Qry.data.Tables[0].Rows[0][0];
                    long l1 = BitConverter.ToInt64(bt,0);
                    return Convert.ToInt64( TSByteArrayToHexString(bt), 16 );
                }
                Qry.FREE();
            }
            catch ( Exception ex )
            {
                SiAuto.LogException("getLongTS", ex);
                throw;
            }

            return -1;
        }

        public static long getLongTS(string Query, string ConnectionString)
        {
            byte[] bt = new byte[8];
            Array.Reverse(bt);

            string s2=getString(Query, ConnectionString);
            long l=0;
            if (!s2.Equals(""))
                l=Convert.ToInt64(s2);
            return l;
        }
    
        private static string TSByteArrayToHexString(byte[] b)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append("0x");

            foreach (byte val in b)
            {
                sb.Append( val.ToString("X2") );
            }

            return sb.ToString();
        }
        #endregion	

        #region getBoolean
        public static bool getBoolean(string Query)
        {
            cDB_SQL_CE Qry=new cDB_SQL_CE(cDB_Settings.CE_ConnectionString);
            Qry.OPEN( Query );
            bool b = false;
            if (Qry.iAnzahl>0) 
            {
                object o = Qry.data.Tables[0].Rows[0][0];
                if ( o == Convert.DBNull )
                    b = false;
                else
                    b = Convert.ToBoolean(o);
            }

            Qry.FREE();
            return b;
        }
        
        public static bool getBoolean(string Query, string ConnectionString)
        {
            cDB_SQL_CE Qry=new cDB_SQL_CE(ConnectionString);
            Qry.OPEN( Query );
            bool b = false;
            if (Qry.iAnzahl>0) 
            {
                object o = Qry.data.Tables[0].Rows[0][0];
                if ( o == Convert.DBNull )
                    b = false;
                else
                    b = Convert.ToBoolean(o);
            }

            Qry.FREE();
            return b;
        }
        #endregion
        
        #region getDouble
        public static double getDouble(string Query)
        {
            string s2=getString(Query);
            double d=0;
            if (!s2.Equals(""))
                d=Convert.ToDouble(s2);
            return d;
        }
         public static double getDouble(string Query, string ConnectionString)
        {
            string s2=getString(Query, ConnectionString );
            double d=0;
            if (!s2.Equals(""))
                d=Convert.ToDouble(s2);
            return d;
        }
        #endregion
                
        #endregion
        
        #region GET_SERVER_DATUM
        /// <summary>
        /// Lese das SQL Server Datum aus
        /// </summary>
        /// <returns></returns>
        public static DateTime GET_SERVER_DATUM()
        {
            string sSQL = "SELECT CONVERT(VARCHAR(19), GETDATE(), 120) AS ServerDatum";
            string sTmp = "";
            DateTime dtReturn = DateTime.MinValue;

            try
            {
                // Lese das SQL Server Datum im Format yyyy-mm-dd hh:mi:ss damit das Serverdatum unabhängig
                // von der aktuellen Systemeinstellung des Clients immer im Englischen Format vorliegt.
                cDB_SQL_CE Qry = new cDB_SQL_CE(cDB_Settings.CE_ConnectionString);  
                Qry.OPEN(sSQL);

                sTmp = Qry.getS("ServerDatum");
                Qry.FREE();

                // Nun wird das gelieferte Serverdatum, im Englischen Format vorliegend, in ein aktuelles DateTime konvertiert
                System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("en-US");
                        
                dtReturn = Convert.ToDateTime(sTmp, ci);
            }
            catch ( Exception ex )
            {
                SiAuto.LogString("GET_SERVER_DATUM(s)", sTmp);
                SiAuto.LogException("GET_SERVER_DATUM", ex);
                throw;
            }
            
            return dtReturn;
        }
        /// <summary>
        /// Lese das SQL Server Datum aus
        /// </summary>
        /// <returns></returns>
        public static DateTime GET_SERVER_DATUM(string ConnectionString)
        {
            string sSQL = "SELECT CONVERT(VARCHAR(19), GETDATE(), 120) AS ServerDatum";
            string sTmp = "";
            DateTime dtReturn = DateTime.MinValue;

            try
            {
                // Lese das SQL Server Datum im Format yyyy-mm-dd hh:mi:ss damit das Serverdatum unabhängig
                // von der aktuellen Systemeinstellung des Clients immer im Englischen Format vorliegt.
                cDB_SQL_CE Qry = new cDB_SQL_CE(ConnectionString);
                Qry.OPEN(sSQL);

                sTmp = Qry.getS("ServerDatum");
                Qry.FREE();

                // Nun wird das gelieferte Serverdatum, im Englischen Format vorliegend, in ein aktuelles DateTime konvertiert
                System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("en-US");
                        
                dtReturn = Convert.ToDateTime(sTmp, ci);
            }
            catch ( Exception ex )
            {
                SiAuto.LogString("GET_SERVER_DATUM(s)", sTmp);
                SiAuto.LogException("GET_SERVER_DATUM", ex);
                throw;
            }
            
            return dtReturn;
        }				
         #endregion
         
        #region Dupliziere Datensätze
        /// <summary>
        /// Kopiere eine Anzahl an Datensätzen und setze in das Link Feld den Angegebenen AIC Wert ein
        /// Beispiel: INSERT INTO [SMT_EFG] SELECT 224, Zeile, MIN, MAX, efg FROM [SMT_EFG] WHERE AIC_EICHUNG=217
        ///           224 = der Fixe neue Wert für das Link Feld, 217 = die zu kopierenden Datensätze 
        /// </summary>
        /// <param name="sTable">In Welcher Tabelle wird kopiert</param>
        /// <param name="sAIC">Sowohl das Quellenfeld für WHERE als auch das Feld für den Fixen neuen Link Wert</param>
        /// <param name="iSourceAIC">Quellen AIC</param>
        /// <param name="iZielAIC">Ziel AIC Wert</param>        
        /// <returns></returns>
        public bool Duplicate_Multiple_Row(string sTable, string sAIC, int iSourceAIC, int iZielAIC)
        {     
            string s2 = "";
            string s1 = "INSERT INTO " + sTable + " SELECT ";

            for(int i=0; i < Column_GetColCount(); i++)
            {
                if (sAIC.ToUpper() == Column_GetColName(i).ToUpper()  )   
                {
                    if( s2.Length > 0)  s2 += ",";          
                    s2 += iZielAIC.ToString();			            /// Ersetze das Link Feld mit dem Fixen Wert
                }
                else
                {
                    if( s2.Length > 0)  s2 += ",";
                    s2 += Column_GetColName(i);
                }
            }

            s2 = s2 + " FROM " + sTable + " WHERE " + sAIC + " = " + iSourceAIC.ToString();
            s1+= s2;
            
            return (Exec(s1) );
        }
        
        /// <summary>
        /// Diese Funktion kopiert einen, eindeutig über den AIC identifizierten Datensatz und
        /// liefert den neu erstellten AIC zurück. Bei der INSERT Generierung wird das Angegebene 
        /// AIC Feld herausgefiltert. Auf PrimaryKeys über mehrer Felder etc. wird keine Rücksicht
        /// genommen.
        /// </summary>
        /// <param name="sTable">Sowohl die Quelle als auf die Zieltabelle</param>
        /// <param name="sAIC">Name des AIC Feld für den WHERE Filter und das nicht zu kopierende Feld</param>
        /// <param name="iAIC">Eindeutige Kennung des zu Kopierenden Datensatzes</param>
        /// <param name="bAic">Soll der AIC zurückgeliefert werden</param>
        /// <returns></returns>
        public int Duplicate_Single_Row(string sTable, string sAIC, int iAIC, bool bAic)
        {
            string s2 = "";
            string s1 = "INSERT INTO " + sTable + " SELECT ";

            for(int i=0; i < Column_GetColCount(); i++)
            {
                if (sAIC.ToUpper() != Column_GetColName(i).ToUpper()  )
                {
                    if( s2.Length > 0)  s2 += ",";
                    s2 += Column_GetColName(i);
                }
            }

            s2 = s2 + " FROM " + sTable + " WHERE " + sAIC + " = " + iAIC.ToString();
            s1+= s2;
            if (Exec(s1))
            {
                if (bAic)
                {
                    OPEN("SELECT SCOPE_IDENTITY () x");
                    int i = getI("x");

                    //SiAuto.LogInt ("{==>SQL.2V.Duplicate_Single_Row} Identity = " , i );
                    return i;
                }
                else
                    return -1;
            }

            return -1;
        }
        
        public int Duplicate_Single_Row_To_Table(string sSourceTable, string sZielTable, string sAIC, int iAIC, bool bAic)
        {
            string s2 = "";
            string s1 = "INSERT INTO " + sZielTable + " SELECT ";

            for(int i=0; i < Column_GetColCount(); i++)
            {
                if (sAIC.ToUpper() != Column_GetColName(i).ToUpper()  )
                {
                    if( s2.Length > 0)  s2 += ",";
                    s2 += Column_GetColName(i);
                }
            }

            s2 = s2 + " FROM " + sSourceTable + " WHERE " + sAIC + " = " + iAIC.ToString();
            s1+= s2;
            if (Exec(s1))
            {
                if (bAic)
                {
                    OPEN("SELECT SCOPE_IDENTITY () x");
                    int i = getI("x");
                    //SiAuto.LogInt ("{==>SQL.2V.Duplicate_Single_Row_To_Table} Identity = " , i );
                    return i;
                }
                else
                    return -1;
            }

            return -1;
        }
        
        #endregion

        #region DataReader

        public bool OPEN_DATAREADER(string s)
        {
            if(CONN.State != ConnectionState.Open) return false;
            
            try
            {
                SqlCeCommand CMD = new SqlCeCommand(s, CONN);		    
                DR = CMD.ExecuteReader();		
                DR.Read();	
                bOk=true;
            }
            catch (SqlCeException e)
            {
                PrintError(e, s, _sFunctName, _iFunctID );
                bOk=false;
            }
            
            return DR.HasRows;
        }
        
        public SqlCeDataReader DataReader
        {
            get
            {
                return DR;
            }
            set
            {
                DR = value;
            }
        }

        #endregion
                
        #region Datumsfilter
        /// <summary>
        /// Formatiere einen String für die Abfrage: (Feldname = HEUTE)
        /// Durch das CONVERT(varchar(25)) wird aus dem DateTime Feld nur ein Datumsfeld und daher die 
        /// Uhrzeit nicht berücksichtigt. Zusätzlich wird das Datum im Format MM/DD/YYYY geprüft, unabhängig
        /// der Systemeinstellungen
        /// </summary>
        /// <param name="Feldname">Welches Feld wird auf HEUTE geprüft</param>
        /// <returns></returns>
        public static string Filter_Heute(string Feldname)
        {
            string SQL = "(CONVERT(varchar(25), " + Feldname + " , 101)) = '" +
                DateTime.Now.Month.ToString("00")+ "/" + 
                DateTime.Now.Day.ToString("00")+ "/" + 
                DateTime.Now.Year.ToString() + "' ";

            SiAuto.LogString("Filter Heute", SQL);

            return SQL;		    
        }
        
        public static string Filter_Datum(string Feldname, DateTime dtSuche)
        {
            string SQL = "(CONVERT(varchar(25), " + Feldname + " , 101)) = '" +
                dtSuche.Month.ToString("00")+ "/" + 
                dtSuche.Day.ToString("00")+ "/" + 
                dtSuche.Year.ToString() + "' ";

            SiAuto.LogString("Filter Datum", SQL);

            return SQL;		    
        }		 

        #region Filtere Datum im Bereich von-bis oder zb. nur ein Tag (mit Berücksichtigung der Uhrzeit)
        /// <summary>
        /// Filtere Datum im Bereich von-bis oder zb. nur ein Tag (mit Berücksichtigung der Uhrzeit)
        /// Getestet in Winrez am 2.11.2005 nach Probleme bei Torggler
        /// </summary>
        /// <param name="Feldname"></param>
        /// <param name="von"></param>
        /// <param name="bis"></param>
        /// <returns></returns>
        public static string Filter_Datumsbereichs(string Feldname, DateTime von, DateTime bis)
        {
            string SQL = "";

            if ( von.Equals(bis) )
            {
                /// Wenn nur ein Tag gewünscht, dann muss zumindest die Zeit von 0-23:59 Berücksichtigt werden.
                /// Siehe http://www.a-m-i.de/tips/datetime/db_datetime.php
                /// oder  http://msdn.microsoft.com/library/default.asp?url=/library/en-us/tsqlref/ts_ca-co_2f3o.asp
                SQL = " (" + Feldname + " BETWEEN CONVERT(datetime, '" + von.Day.ToString("00") + "." + von.Month.ToString("00") + "." + von.Year.ToString("0000") + " 00:00:00', 104) AND " + 
                    "                           CONVERT(datetime, '" + von.Day.ToString("00") + "." + von.Month.ToString("00") + "." + von.Year.ToString("0000") + " 23:59:59', 104) )";  
            }
            else
            {
                SQL = " (" + Feldname + " BETWEEN CONVERT(datetime, '" + von.Day.ToString("00") + "." + von.Month.ToString("00") + "." + von.Year.ToString("0000") + " 00:00:00', 104) AND " + 
                    "                           CONVERT(datetime, '" + bis.Day.ToString("00") + "." + bis.Month.ToString("00") + "." + bis.Year.ToString("0000") + " 23:59:59', 104) )";  
            }

            SiAuto.LogString("Filter Bereich", SQL);
            
            return SQL;
        }

         

        #endregion

 
        
        #endregion
        
        public bool XML_out(string sFile)
        {
            try
            {
                if ( data == null )
                {
                    Debug.Assert(false, "Achtung Fehler XML Writer", "XML Writer Dataset ist leer");
                    return false;
                }
                    
                /// Create the FileStream to write with.
                System.IO.FileStream myFileStream = new System.IO.FileStream(sFile, System.IO.FileMode.Create);
                /// Create an XmlTextWriter with the fileStream.
                System.Xml.XmlTextWriter myXmlWriter = new System.Xml.XmlTextWriter(myFileStream, System.Text.Encoding.GetEncoding( "windows-1252") );
                /// Write to the file with the WriteXml method.
                data.WriteXml (myXmlWriter, XmlWriteMode.WriteSchema);
                myXmlWriter.Close();
                return true;
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message);
                MessageBox.Show(e.Message);
                return false;
            }
        }		
        
        #region Static Hilfsfunktionen (PrintError, Open_iSql, DATE_TIME_TO_DB )
        public static void PrintError(SqlCeException e, string sSQL_Query, string FunktionsName, int FunktionsID)
        {
            Trace.WriteLine("Error in SQL Ausführung");
            //if (FunktionsName.Length > 0) { SiAuto.LogString("Funktion", FunktionsName); }
            //if ( FunktionsID != 0 ) { SiAuto.LogInt  ("ID" , FunktionsID  );}
            //SiAuto.LogSql("Query", sSQL_Query);
            SiAuto.LogSQLException(sSQL_Query, FunktionsName, e);
                        
            MessageBox.Show("Error: " + sSQL_Query , "SQL Exception #13");
        }
        
        public static void PrintError(Exception e, string sSQL_Query, string FunktionsName, int FunktionsID)
        {
            SiAuto.LogError("Error in SQL Ausführung");
            //if (FunktionsName.Length > 0) { SiAuto.LogString("Funktion", FunktionsName); }
            //if ( FunktionsID != 0 ) { SiAuto.LogInt  ("ID" , FunktionsID  );}
            //SiAuto.LogSql("Query", sSQL_Query);
            SiAuto.LogSQLException(sSQL_Query, FunktionsName, e);
            
            MessageBox.Show("Error: " + sSQL_Query , "SQL Exception #14");
        }

        public static void Open_iSql(string SQLCommand, Form fMdiParent)
        {				  
            //frmISQL f=new frmISQL();
            //f.MdiParent = fMdiParent;            
            //f.SET_COMMAND_STRING = SQLCommand;
            //f.Show();
        }

        public static string DATE_TIME_TO_DB(DateTime datetime)
        {
            string s = "";
            string sOut = "";

            try
            {
                if (datetime == DateTime.MinValue) 
                {
                    return "null"; 
                } 
                else 
                { 
                    System.Globalization.CultureInfo ci = System.Globalization.CultureInfo.InvariantCulture;
                    
                    s = datetime.ToString("yyyy-MM-dd HH:mm:ss", ci);
                    sOut = "CONVERT( DATETIME ,  '" + s + "', 102 ) ";

                    return sOut;
                }
            }
            catch ( Exception ex )
            {
                SiAuto.LogString("DATE_TIME_TO_DB(s)", s);
                SiAuto.LogString("DATE_TIME_TO_DB(sOut)", sOut);
                SiAuto.LogException("DATE_TIME_TO_DB", ex);
            }

            return "null";
        }

        public static string DATE_TIME_TO_DB2(DateTime datetime)
        {
            if (datetime == DateTime.MinValue) 
            {
                return "null"; 
            } 
            else 
            { 
                string s = datetime.Date.Year.ToString()  + "-" + 
                           datetime.Date.Month.ToString() + "-" + 
                           datetime.Date.Day.ToString() + " 23:59:59";
                return "CONVERT( DATETIME ,  '" + s + "', 102 ) ";
            }			
        }
        
        #region Create_Schema_File
        public static void Create_Schema_File(string FullPathAndFileName)
        {
           CreateSchemaFile(FullPathAndFileName, cDB_Settings.CE_ConnectionString);
        }
        
        public static void Create_Schema_File(string FullPathAndFileName, string ConnectionString)
        {
           CreateSchemaFile(FullPathAndFileName, ConnectionString);
        }

        private static void CreateSchemaFile(string FullPathAndFileName, string ConnectionString)
        {
            cDB_SQL_CE Qry = new cDB_SQL_CE(ConnectionString);
            string SQL = "";
            int    i   = 0;
            ArrayList AL = new ArrayList();
                        
            System.IO.StreamWriter sr =new StreamWriter( FullPathAndFileName ,false , System.Text.Encoding.GetEncoding( "windows-1252" )    );

            sr.WriteLine ( "[INFO_HEADER]");
            sr.WriteLine ( "Erstellt=" + DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss") );
            sr.WriteLine ( "Version=" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString() );
            sr.WriteLine ("Machinename=" + SMT_Standards.AppStandards.MachineName.ToUpper());
            sr.WriteLine ( "\r\n\r\n" );
            
            #region Schreibe alle Tabellen mit ToUpper und Alpahbetisch geordnet
            SQL = "SELECT name FROM sysobjects WHERE (xtype = 'U') ORDER BY name";

            if ( Qry.OPEN (SQL) )
            {
                for (i=0; !Qry.eof(); Qry.moveNext() )	
                {
                    i++;
                    if (i==1) sr.WriteLine( "[TABLE]" );
                    sr.WriteLine ( Qry.getS("name").ToUpper() );
                    AL.Add ( Qry.getS("name").ToUpper() );
                }
            }
            
            sr.WriteLine ("\r\n");
            #endregion

            #region Schreibe alle Foreign Einträge mit ToUpper und Alpahbetisch geordnet
            SQL = "SELECT name FROM sysobjects WHERE (xtype = 'F') ORDER BY name";

            if ( Qry.OPEN (SQL) )
            {
                for (i=0; !Qry.eof(); Qry.moveNext() )	
                {
                    i++;
                    if (i==1) sr.WriteLine( "[FOREIGN]" );
                    sr.WriteLine ( Qry.getS("name").ToUpper() );
                }
            }			
            sr.WriteLine ("\r\n");
            #endregion
            
            #region Schreibe alle UNIQUE Einträge mit ToUpper und Alpahbetisch geordnet
            SQL = "SELECT name FROM sysobjects WHERE (xtype = 'UQ') ORDER BY name";

            if ( Qry.OPEN (SQL) )
            {
                for (i=0; !Qry.eof(); Qry.moveNext() )	
                {
                    i++;
                    if (i==1) sr.WriteLine( "[UNIQUE]" );
                    sr.WriteLine ( Qry.getS("name").ToUpper() );
                }
            }			
            sr.WriteLine ("\r\n");
            #endregion

            #region Schreibe alle PRIMARY KEY Einträge mit ToUpper und Alpahbetisch geordnet
            SQL = "SELECT name FROM sysobjects WHERE (xtype = 'PK') ORDER BY name";

            if ( Qry.OPEN (SQL) )
            {
                for (i=0; !Qry.eof(); Qry.moveNext() )	
                {
                    i++;
                    if (i==1) sr.WriteLine( "[PRIMARY]" );
                    sr.WriteLine ( Qry.getS("name").ToUpper() );
                }
            }			
            sr.WriteLine ("\r\n");
            #endregion

            #region Schreibe alle PRIMARY KEY Einträge mit ToUpper und Alpahbetisch geordnet
            for (int z=0; z < AL.Count ; z++)
            {
                SQL =   "SELECT c.name AS Spaltenname, t.name AS Datentyp, c.length AS Größe, " + 
                    "c.isnullable AS NullZulassen, c.colstat AS ISAutowert " +
                    "FROM [sysobjects] o INNER JOIN " +
                    "syscolumns c ON o.id = c.id INNER JOIN " + 
                    "systypes t ON c.xusertype = t.xusertype " +
                    "WHERE (o.name = '" + AL[z].ToString() +"') " +
                    "ORDER BY c.name ";

                if ( Qry.OPEN (SQL) )
                {
                    for (i=0; !Qry.eof(); Qry.moveNext() )	
                    {
                        i++;
                        if (i==1) sr.WriteLine( "[TABLE_" + AL[z].ToString() + "]" );
                        sr.WriteLine ( Qry.getS("Spaltenname").ToUpper() + ";" + 
                            Qry.getS("Datentyp").ToLower() + ";" + 
                            Qry.getS("Größe").ToUpper() + ";" +
                            Qry.getS("NullZulassen").ToUpper() + ";" +
                            Qry.getS("ISAutowert").ToUpper()  );
                    }
                }			
                sr.WriteLine ("\r\n");
            }
            #endregion

            sr.Close();
            AL.Clear();
            MessageBox.Show ("Schema wurde erstellt", "Datenbank");
        }
        #endregion
        
        #endregion		
        
        private static bool saveExec(string s,SqlCeConnection CON)
        {
            if(CON.State != ConnectionState.Open) { CON.Open(); }
            SqlCeCommand sqlCommand = new SqlCeCommand(s, CON);

            try
            {   			    
                sqlCommand.ExecuteNonQuery();
            }
            catch (SqlCeException e)
            {
                PrintError(e, s , "", 0 );
                return false;
            }
            return true;
        }

        private static bool ThrowExec(string sSQL, SqlCeConnection CON, string Funktionsname, int FuntionsID)
        {
            if (CON.State != ConnectionState.Open) { CON.Open(); }
            SqlCeCommand sqlCommand = new SqlCeCommand(sSQL, CON);

            try
            {
                sqlCommand.ExecuteNonQuery();
            }
            catch (SqlCeException e)
            {
                //SiAuto.LogError("Error SQL Execute");
                SiAuto.LogSQLException(sSQL, "SQL Execute Exception" , e);
                //SiAuto.LogSql("Query", sSQL);

                throw new Exception("SQL Exec Error", e);
            }

            return true;
        }

        private static bool saveExec(string s,SqlCeConnection CON, string Funktionsname, int FuntionsID)
        {
            if(CON.State != ConnectionState.Open) { CON.Open(); }
            SqlCeCommand sqlCommand = new SqlCeCommand(s, CON);
            try
            {   			    
                sqlCommand.ExecuteNonQuery();
            }
            catch (SqlCeException e)
            {
                PrintError(e, s , Funktionsname , 99);
                return false;
            }
            return true;
        }				
        
        public static bool EXEC(string ExecuteString)
        {
            SqlCeConnection CON = new SqlCeConnection(cDB_Settings.CE_ConnectionString);
            bool b=saveExec(ExecuteString,CON, "", 0 );
            CON.Dispose();
            return b;
        }
        public static bool EXEC(string ExecuteString, string ConnectionString, string FunktionsName, int FunktionsID)
        {
            SqlCeConnection CON = new SqlCeConnection(ConnectionString);
            bool b=saveExec(ExecuteString,CON, FunktionsName , FunktionsID  );
            CON.Dispose();
            return b;
        }
        public static bool EXEC_THROW(string ExecuteString, string ConnectionString, string FunktionsName, int FunktionsID)
        {
            SqlCeConnection CON = new SqlCeConnection(ConnectionString);
            bool b = ThrowExec (ExecuteString, CON, FunktionsName, FunktionsID);
            CON.Dispose();
            return b;
        }		
        
        public static enumDeleteError EXEC_DELETE(string s)
        {
            SqlCeConnection CON = new SqlCeConnection(cDB_Settings.CE_ConnectionString);
            if(CON.State != ConnectionState.Open) { CON.Open(); }
            SqlCeCommand sqlCommand = new SqlCeCommand(s, CON);
            try
            {
                sqlCommand.ExecuteNonQuery();
                return enumDeleteError.Delete_Okay;
            }
            catch (SqlCeException e)
            {
                return enumDeleteError.Unbekannt; 
            }
        }	
        
        public static enumDeleteError EXEC_DELETE(string s, string FunktionsName, int FunctionsID)
        {
            SqlCeConnection CON = new SqlCeConnection(cDB_Settings.CE_ConnectionString);
            if(CON.State != ConnectionState.Open) { CON.Open(); }
            SqlCeCommand sqlCommand = new SqlCeCommand(s, CON);
            try
            {
                sqlCommand.ExecuteNonQuery();
                return enumDeleteError.Delete_Okay;
            }
            catch (SqlCeException e)
            {
                SiAuto.LogError("Exception in EXEC_DELETE");
                SiAuto.LogError("Funktionsname=" + FunktionsName);
                SiAuto.LogError("FunktionsID=" + FunctionsID.ToString());
                SiAuto.LogException(e);

                //switch (e.Number )
                //{
                //    case (547):	    return enumDeleteError.InVerwendung; 
                //    default:		return enumDeleteError.Unbekannt; 
                return enumDeleteError.Unbekannt; 
                //}
            }
        }			
                    
        public static enumDeleteError EXEC_DELETE(string s, string ConnectionString)
        {
            SqlCeConnection CON = new SqlCeConnection(ConnectionString);
            if(CON.State != ConnectionState.Open) { CON.Open(); }
            SqlCeCommand sqlCommand = new SqlCeCommand(s, CON);
            try
            {
                sqlCommand.ExecuteNonQuery();
                return enumDeleteError.Delete_Okay;
            }
            catch (SqlCeException e)
            {
                SiAuto.LogError("Exception in EXEC_DELETE");
                SiAuto.LogException(e);

                //switch (e.Number )
                //{
                //    case (547):	    return enumDeleteError.InVerwendung; 
                //    default:		return enumDeleteError.Unbekannt; 
                //}
                return enumDeleteError.InVerwendung; 
            }
        }

    }
}
