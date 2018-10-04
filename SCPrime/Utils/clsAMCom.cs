using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using nsBaseClass;
using System.Configuration;

namespace WorkshopMonitorPrime.Model
{
    class AMComClient
    {
        static readonly ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static String[] SiteIds = null;
        private static String[] Users = null;
        private static String[] Passwords = null;
        private String AMComClientFileName = "";
        clsGlobalVariable objGlobal = new clsGlobalVariable();
        private String LangId = "ENG";


        public void openSPInvoice(String SiteId, int SRECNO)
        {
            String AddParam = "-com:11 -param1:$OPENINVOICE$1 -param2:$START$1 -param3:$CLOSE$1 -param4:$APPLICATIONID$4 -param5:$INVOICENUMBER$" + SRECNO.ToString();
            clsSqlFactory hSql = new clsSqlFactory();
            hSql.NewCommand("select a.BTYPE,a.BILLD,b.CUSTID from ALL_SBIL a, CUST b where a.SRECNO=? and a._UNITID=? and a.CUSTNO=b.CUSTNO ");
            hSql.Com.Parameters.Add("SRECNO", SRECNO);
            hSql.Com.Parameters.Add("UNITID", SiteId);
            hSql.ExecuteReader();
            if (hSql.Read())
            {
                AddParam += " -param6:$INVOICECATEGORY$" + hSql.Reader.GetString(0);
                TimeSpan ts = hSql.Reader.GetDateTime(1) - new DateTime(1900,1,1);
                AddParam += " -param7:$INVOICEDATE$" + ts.TotalDays.ToString();
                AddParam += " -param8:$CUSTOMERID$" + hSql.Reader.GetInt32(2).ToString();
                launchCOM(SiteId, AddParam);
            }
            hSql.Close();
            
        }

        

        public void openSPOrder(String SiteId, int SSALID, int SRECNO)
        {
            String AddParam = "-com:9 -param1:" + SSALID.ToString() + " -param2:" + SRECNO.ToString();
            launchCOM(SiteId, AddParam);
        }

        public void openWorkOrder(String SiteId,int WRKORDNO,int GSALID, int GRECNO, String WOSiteId)
        {
            String AddParam = "-com:3 -param1:" + GSALID.ToString() + " -param2:" + WRKORDNO.ToString() + " -param3:" + GRECNO.ToString() + " -param4:" + WOSiteId;
            launchCOM(SiteId, AddParam);
        }
        public void openCustomer(String SiteId, int CUSTNO, int CUSTID)
        {
            String AddParam = "-com:1 -param1:" + CUSTNO.ToString() + " -param2:" + CUSTID.ToString();
            launchCOM(SiteId, AddParam);
        }

        public void openVehicle(String SiteId, String LICNO)
        {
            String AddParam = "-com:2 -param1:\"" + LICNO.Replace(" ","@@@") + "\"";
            launchCOM(SiteId, AddParam);
        }

        public void openServiceHistory(String SiteId, String LICNO)
        {
            String AddParam = "-com:4 -param1:\"" + LICNO.Replace(" ", "@@@") + "\"";
            launchCOM(SiteId, AddParam);
        }
        
        private void launchCOM(String SiteId,String AddParam)
        {
            clsWinIni objWinIni = new clsWinIni();
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.StartInfo.FileName = AMComClientFileName;
            String IntUser = Users[Array.IndexOf(SiteIds, SiteId)];
            String IntPass = Passwords[Array.IndexOf(SiteIds, SiteId)];
            proc.StartInfo.Arguments ="-database:"+objGlobal.DMSDBName+" -user:"+objGlobal.DMSFirstUserName+" -amlid:"+IntUser+" -amlpw:"+IntPass+" -lang:"+LangId+" "+AddParam;
            proc.StartInfo.WorkingDirectory =  objWinIni.getKey("AM3", "EXEPATH", "");
                       
            proc.Start();
            
        
        }
        public AMComClient()
        {
            AMComClientFileName = ConfigurationManager.AppSettings["AMComClient"].ToString();
            clsSqlFactory hSql = new clsSqlFactory();
            try
            {

                SiteIds = new String[0];
                Users = new String[0];
                Passwords = new String[0];
                hSql.NewCommand("select UnitId, UserId, Password from Z_BASE_EXTUNIT order by UnitId ");
                if (hSql.ExecuteReader())
                {
                    int i = 0;
                    while (hSql.Read())
                    {
                        i=resizeArray();
                        SiteIds[i] = hSql.Reader.GetString(0);
                        Users[i] = hSql.Reader.GetString(1);
                        Passwords[i] = hSql.Reader.GetString(2);
                    }
                }
                hSql.NewCommand("select isnull(LANGID,'ENG') from EUSR where USRSID=? ");
                hSql.Com.Parameters.Add("USRSID",objGlobal.DMSFirstUserName);
                hSql.ExecuteReader();
                if (hSql.Read()) {
                    LangId = hSql.Reader.GetString(0);
                }

            }
            catch (Exception ex)
            {
                hSql.Rollback();
                _log.Error(ex.Message);
                throw ex;
            }
            finally
            {
                hSql.Close();
            }
        }
        private int resizeArray()
        {
            int i = SiteIds.GetLength(0) + 1;
            Array.Resize(ref SiteIds, i);
            Array.Resize(ref Users, i);
            Array.Resize(ref Passwords, i);
            return i - 1;

        }
    }
}
