using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace Allgemein
{
    internal class SiAuto
    {
        public static void WriteErrorLog(string sErrorMessage, string sOptionalMessage, Exception ex)
        {
            System.IO.StreamWriter sw = null;

            try
            {
                Trace.WriteLine(sErrorMessage);
                
                sw = new System.IO.StreamWriter(
                    @"\Hard Disk2\SMT\SystemError.smtlog", true /*Append*/ , System.Text.Encoding.GetEncoding("windows-1252"));
                sw.WriteLine(DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss") + " =>" + sErrorMessage);

                if (!string.IsNullOrEmpty(sOptionalMessage))
                {
                    sw.WriteLine("==>" + sOptionalMessage);
                }

                if (ex != null)
                {
                    Trace.WriteLine(ex.Message);
                    sw.WriteLine("==>" + ex.Message);

                    if (ex.InnerException != null)
                    {
                        Trace.WriteLine(ex.InnerException.Message);
                        sw.WriteLine(ex.InnerException.Message);
                    }
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                if (sw != null)
                {
                    sw.Close();
                    sw = null;
                }
            }
        }
        
        public static void LogException(string s, Exception ex)
        {
            SiAuto.WriteErrorLog(s,"",ex);
        }
        public static void LogException(Exception ex)
        {
            SiAuto.WriteErrorLog("","", ex);
            cGlobalHandling.MessageBox(ex.Message, "Systemzustand");            
        }
        public static void LogSQLException(string sSQL, string sMessageCaption, Exception ex)
        {
            SiAuto.WriteErrorLog(sSQL, sMessageCaption, ex);
            cGlobalHandling.MessageBox(sSQL + "\r\n\r\n"+ ex.Message, "Systemzustand #88 [" +sMessageCaption +"]" );
        }
        public static void LogException(Exception ex, string sMsgBoxCaption)
        {
            SiAuto.WriteErrorLog(sMsgBoxCaption, "", ex);
            cGlobalHandling.MessageBox(ex.Message, sMsgBoxCaption);
        }
        //public static void LogSql(string s, string sSQL)
        //{
        //    SiAuto.WriteErrorLog(s,sSQL,null);
        //}
        public static void LogMessage(string s)
        {
            SiAuto.WriteErrorLog(s, "", null);
        }
        //public static void LogBool(string s, bool bRet)
        //{
        //    SiAuto.WriteErrorLog(s, bRet.ToString(), null);
        //}
        //public static void LogInt(string s, int i)
        //{
        //    //SiAuto.WriteErrorLog(s, i.ToString(), null);
        //}
        public static void LogString(string s, string sSQL)
        {
            SiAuto.WriteErrorLog(s, sSQL, null);
        }
        public static void LogError(string s)
        {
            SiAuto.WriteErrorLog(s, "", null);     
        }        
    }
}
