using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using nsBaseClass;
using WorkshopMonitorPrime.Model;

using SCPrime.Utils;

namespace SCPrime.Model
{
    public class SCInvoice
    {
        public int OID { get; set; }
        public int InvoiceSeqNr { get; set; }
        public int CustNo { get; set; }
        public int DCustNo;
        public String UnitId { get; set; }
        public String LNAME { get; set; }
        public String EXPL { get; set; }
        public int ContractOID;
        public String ExtOrderId;
        public String SalesType;
        public String SmanId { get; set; }
        public String DeptId { get; set; }
        public int SORDNO { get; set; }
        public int SSALID { get; set; }
        public int SRECNO { get; set; }
        public String OUserId;
        public String Note { get; set; }
        public String BTYPE { get; set; }
        public DateTime BILLD { get; set; }
        public DateTime NBILLD;
        public DateTime CBILLD;
        public DateTime DELD { get; set; }
        public String PartPostFix = "";
        public String Payer = "";
        public String TPCODE;
        public int TPTIME;
        public Decimal INVSUM0 { get; set; }
        public Decimal INVSUM { get; set; }
        public DateTime PAIDDATE { get; set; }
        public int PAIDSUM { get; set; }
        public int VehiId;
        public List<SCInvoiceItem> InvItems = new List<SCInvoiceItem>();
        public SCInvoice()
            {
            SRECNO = 0;
        }
        public bool saveOrder(bool bDraft)
        {
            bool bRet = true;
            clsSqlFactory hSql = new clsSqlFactory();
            clsBaseUtility objUtil = new clsBaseUtility();
            clsCurrExchange objCurr = new clsCurrExchange();
            objCurr.Init();
            String strSql = "";
            try
            {
                if (bDraft == false)
                {
                    strSql = "select V1 from " + objUtil.getTable("CORW", UnitId) + " where CODAID='VLASKUTYY' and C1=? ";
                    bRet = bRet && hSql.NewCommand(strSql);
                    hSql.Com.Parameters.AddWithValue("BTYPE", BTYPE);
                    hSql.ExecuteReader();
                    if (hSql.Read())
                    {
                        int NuseId = hSql.Reader.GetInt32(0);
                        strSql = "update " + objUtil.getTable("NUSE", UnitId) + " set RECNO=RECNO+1 where NUSEID=? ";
                        bRet = bRet && hSql.NewCommand(strSql);
                        hSql.Com.Parameters.AddWithValue("NUSEID", NuseId);
                        hSql.ExecuteNonQuery();
                        strSql = "select RECNO from " + objUtil.getTable("NUSE", UnitId) + " where NUSEID=? ";
                        bRet = bRet && hSql.NewCommand(strSql);
                        hSql.Com.Parameters.AddWithValue("NUSEID", NuseId);
                        hSql.ExecuteReader();
                        if (hSql.Read())
                        {
                            SRECNO = hSql.Reader.GetInt32(0);
                        }
                        else
                        {
                            bDraft = true;
                        }
                    }
                    else
                    {
                        bDraft = true;
                    }

                }
                strSql = "select top 1 isnull(SSALID,0), isnull(SORDNO,0) from " + objUtil.getTable("SSALSEED", UnitId);              
                bRet = bRet && hSql.ExecuteReader(strSql);
                if (bRet && hSql.Read())
                {
                    SSALID = hSql.Reader.GetInt32(0) + 1;
                    SORDNO = hSql.Reader.GetInt32(1) + 1;

                    strSql = "update " + objUtil.getTable("SSALSEED", UnitId) + " set SSALID = SSALID + 1, SORDNO = SORDNO + 1 ";
                    bRet = bRet && hSql.ExecuteNonQuery(strSql);
                }
                strSql = "insert into " + objUtil.getTable("SSAL", UnitId) +
                           "(CREATED,SSALID, CUSTNO, SMANID, DEPT, STATUS,RECTYPE, RECORDID, SORDNO, STYPE, " +
                           " DIVISION, ISDIVIDED, BOPRIOR, OUSRSID, CDTRAN,NOTE,RDATE,EXIDNO,VEHIID)" +
                           " values (getdate(),?, ?, ?,?, ?, ?, 0, ?, ?, 0, 0, 0, ?, 0, ?,?,?,?) ";
                bRet = bRet && hSql.NewCommand(strSql);
                hSql.Com.Parameters.AddWithValue("SSALID", SSALID);
                hSql.Com.Parameters.AddWithValue("CUSTNO", CustNo);
                hSql.Com.Parameters.AddWithValue("SMANID", SmanId);
                if(DeptId!=null)
                    hSql.Com.Parameters.AddWithValue("DEPT", DeptId);
                else
                    hSql.Com.Parameters.AddWithValue("DEPT", DBNull.Value);
                
                if (bDraft == true)
                {
                    hSql.Com.Parameters.AddWithValue("STATUS", 'A');
                    hSql.Com.Parameters.AddWithValue("RECTYPE", DBNull.Value);
                }
                else
                {
                    hSql.Com.Parameters.AddWithValue("STATUS", 'S');
                    if (BTYPE == "0")
                    {
                        hSql.Com.Parameters.AddWithValue("RECTYPE", 'E');
                    }
                    else
                    {
                        hSql.Com.Parameters.AddWithValue("RECTYPE", 'N');
                    }
                }
                hSql.Com.Parameters.AddWithValue("SORDNO", SORDNO);
                hSql.Com.Parameters.AddWithValue("STYPE", SalesType);
                hSql.Com.Parameters.AddWithValue("OUSRSID", OUserId);
                hSql.Com.Parameters.AddWithValue("NOTE", Note);
                hSql.Com.Parameters.AddWithValue("RDATE", DELD.ToString("yyyy-MM-dd"));
                hSql.Com.Parameters.AddWithValue("EXIDNO", ExtOrderId);
                hSql.Com.Parameters.AddWithValue("VEHIID", VehiId);
                bRet = bRet && hSql.ExecuteNonQuery();

               
                strSql = "insert into " + objUtil.getTable("SBIL", UnitId) +
                             "(SSALID, SRECNO, CUSTNO, LCUSTNO, DCUSTNO, " +
                             " REFE, DDEL, DWP,  PDEL," +
                             " CURRCD, BTYPE,BILLD,BILLD_WITH_TIME,HSMANID,DELD,TPCODE,TPTIME)" +
                             " values (?, ?, ?, ?, ?," +
                             " ?, 0, 0, 1, " +
                             "?,?,?,?,?,?,?,?) ";
                bRet = bRet && hSql.NewCommand(strSql);
                hSql.Com.Parameters.AddWithValue("SSALID", SSALID);
                hSql.Com.Parameters.AddWithValue("SRECNO", SRECNO);
                hSql.Com.Parameters.AddWithValue("CUSTNO", CustNo);
                hSql.Com.Parameters.AddWithValue("LCUSTNO", CustNo);
                hSql.Com.Parameters.AddWithValue("DCUSTNO", DCustNo);
                hSql.Com.Parameters.AddWithValue("REFE", Note.Length > 45 ? Note.Substring(0, 45) : Note);
                hSql.Com.Parameters.AddWithValue("CURRCD", objCurr.BaseCurrency);
                hSql.Com.Parameters.AddWithValue("BTYPE", BTYPE);
                if (bDraft == true)
                {
                    hSql.Com.Parameters.AddWithValue("BILLD", DBNull.Value);
                    hSql.Com.Parameters.AddWithValue("BILLD_WITH_TIME", DBNull.Value);
                    hSql.Com.Parameters.AddWithValue("HSMANID", DBNull.Value);
                    hSql.Com.Parameters.AddWithValue("DELD", DBNull.Value);
                }
                else
                {
                    hSql.Com.Parameters.AddWithValue("BILLD", new DateTime(BILLD.Year,BILLD.Month,BILLD.Day));
                    hSql.Com.Parameters.AddWithValue("BILLD_WITH_TIME", DateTime.Now);
                    hSql.Com.Parameters.AddWithValue("HSMANID", SmanId);
                    hSql.Com.Parameters.AddWithValue("DELD", new DateTime(DELD.Year, DELD.Month, DELD.Day));
                }

                hSql.Com.Parameters.AddWithValue("TPCODE", TPCODE);
                hSql.Com.Parameters.AddWithValue("TPTIME", TPTIME);

                bRet = bRet && hSql.ExecuteNonQuery();

                strSql = "update a set a.AGRP = b.AGRP, a.CUSTNAME = ltrim(isnull(b.FNAME,'')+' ' +isnull(b.LNAME,'')) from " +
                    objUtil.getTable("SBIL", UnitId) + " a, CUST b where a.SSALID=? and a.CUSTNO = b.CUSTNO ";
                bRet = bRet && hSql.NewCommand(strSql);
                hSql.Com.Parameters.AddWithValue("SSALID", SSALID);
                bRet = bRet && hSql.ExecuteNonQuery();

                strSql = "update a set a.DADDR1 = b.ADDR1, a.DADDR2 = b.ADDR2,a.DCTRYCD=b.CTRYCD,a.DCOUNTRY=b.COUNTRY,"+
                    " a.DNAME= ltrim(isnull(b.FNAME,'')+' ' +isnull(b.LNAME,'')), a.DPO=b.PO,a.DPOSTCD=b.POSTCD,a.DADDR2E=b.ADDR2E from " +
                    objUtil.getTable("SBIL", UnitId) + " a, CUST b where a.SSALID=? and a.DCUSTNO = b.CUSTNO ";
                bRet = bRet && hSql.NewCommand(strSql);
                hSql.Com.Parameters.AddWithValue("SSALID", SSALID);
                bRet = bRet && hSql.ExecuteNonQuery();

                strSql = "insert into ZSC_ContractInvoice(ContractOID,InvoiceNo,SSALID,UnitId,InvoiceType,Created,Modified) select  "+
                    "?,?,SSALID,?,0,getdate(),getdate() from " + objUtil.getTable("SBIL", UnitId) + " where SSALID=?";
                bRet = bRet && hSql.NewCommand(strSql);
                hSql.Com.Parameters.AddWithValue("ContractOID", ContractOID);
                hSql.Com.Parameters.AddWithValue("InvoiceNo", InvoiceSeqNr);
                hSql.Com.Parameters.AddWithValue("UnitId", UnitId);
                hSql.Com.Parameters.AddWithValue("SSALID", SSALID);
                bRet = bRet && hSql.ExecuteNonQuery();

                strSql = "update ZSC_Contract set LastInvoiceDate = ?,NextInvoiceDate=? where OID=?";
                bRet = bRet && hSql.NewCommand(strSql);
                
                hSql.Com.Parameters.AddWithValue("CBILLD", CBILLD);
                hSql.Com.Parameters.AddWithValue("NBILLD", NBILLD);
                hSql.Com.Parameters.AddWithValue("ContractOID", ContractOID);
                bRet = bRet && hSql.ExecuteNonQuery();

                clsTaxHandling objTax = new clsTaxHandling();
                objTax.Init(6);


              

                foreach (SCInvoiceItem objRow in InvItems)
                {
                    strSql = "insert into " + objUtil.getTable("SROW", UnitId) + "(CREATED,SSALID, SROWID, SRECNO, SMANID, ITEMNO, SUPLNO, BUYPR, " +
                      " DISCPC, ITEM, SUPL, NAME, NUM, RNO, RTYPE, RSUM, UNITPR, VATCD, " +
                      " ONDEDNUM, ORDNUM, IGROUPID,NOTE,RINFO,EXIDNO) values (getdate(),?,?,?,?,?,?,?," +
                      " ?,?,?,?,?,?,?,?,?,?," +
                      "0,?,?,?,?,?)";
                    bRet = bRet && hSql.NewCommand(strSql);
                    hSql.Com.Parameters.AddWithValue("SSALID", SSALID);
                    hSql.Com.Parameters.AddWithValue("SROWID", DBNull.Value);
                    hSql.Com.Parameters.AddWithValue("SRECNO", SRECNO);
                    hSql.Com.Parameters.AddWithValue("SMANID", SmanId);
                    hSql.Com.Parameters.AddWithValue("ITEMNO", DBNull.Value);
                    hSql.Com.Parameters.AddWithValue("SUPLNO", DBNull.Value);
                    hSql.Com.Parameters.AddWithValue("BUYPR", DBNull.Value);
                    hSql.Com.Parameters.AddWithValue("DISCPC", DBNull.Value);
                    hSql.Com.Parameters.AddWithValue("ITEM", DBNull.Value);
                    hSql.Com.Parameters.AddWithValue("SUPL", DBNull.Value);
                    hSql.Com.Parameters.AddWithValue("NAME", DBNull.Value);
                    hSql.Com.Parameters.AddWithValue("NUM", DBNull.Value);
                    hSql.Com.Parameters.AddWithValue("RNO", DBNull.Value);
                    hSql.Com.Parameters.AddWithValue("RTYPE", DBNull.Value);
                    hSql.Com.Parameters.AddWithValue("RSUM", DBNull.Value);
                    hSql.Com.Parameters.AddWithValue("UNITPR", DBNull.Value);
                    hSql.Com.Parameters.AddWithValue("VATCD", DBNull.Value);
                    hSql.Com.Parameters.AddWithValue("ORDNUM", DBNull.Value);
                    hSql.Com.Parameters.AddWithValue("IGROUPID", DBNull.Value);
                    hSql.Com.Parameters.AddWithValue("NOTE", DBNull.Value);
                    hSql.Com.Parameters.AddWithValue("RINFO", DBNull.Value);
                    hSql.Com.Parameters.AddWithValue("EXIDNO", DBNull.Value);

                    if (objRow.RTYPE != 8)
                    {
                        hSql.Com.Parameters["SROWID"].Value = objRow.SROWID;
                        hSql.Com.Parameters["ITEMNO"].Value = objRow.ITEMNO;
                        hSql.Com.Parameters["SUPLNO"].Value = objRow.SUPLNO;
                        hSql.Com.Parameters["BUYPR"].Value = objRow.BUYPR;
                        hSql.Com.Parameters["DISCPC"].Value = objRow.DISCPC;
                        hSql.Com.Parameters["ITEM"].Value = objRow.ITEMNO;
                        hSql.Com.Parameters["SUPL"].Value = objRow.SUPLNO;
                        hSql.Com.Parameters["NAME"].Value = objRow.NAME;
                        hSql.Com.Parameters["NUM"].Value = objRow.NUM;
                        hSql.Com.Parameters["RNO"].Value = objRow.SROWID;
                        hSql.Com.Parameters["RTYPE"].Value = objRow.RTYPE;
                        hSql.Com.Parameters["RSUM"].Value = objRow.RSUM;
                        hSql.Com.Parameters["UNITPR"].Value = objRow.UNITPR;
                        hSql.Com.Parameters["VATCD"].Value = objRow.VATCD;
                        hSql.Com.Parameters["ORDNUM"].Value = objRow.NUM;
                        if (objRow.IGROUPID > 0)
                            hSql.Com.Parameters["IGROUPID"].Value = objRow.IGROUPID;
                        hSql.Com.Parameters["EXIDNO"].Value = objRow.EXIDNO;

                    }
                    else
                    {
                        hSql.Com.Parameters["SROWID"].Value = objRow.SROWID;
                        hSql.Com.Parameters["NAME"].Value = objRow.NAME;
                        hSql.Com.Parameters["RNO"].Value = objRow.SROWID;
                        hSql.Com.Parameters["RTYPE"].Value = objRow.RTYPE;
                        hSql.Com.Parameters["NOTE"].Value =objRow.NOTE;
                        if((objRow.RINFO!=null)&& (objRow.RINFO != ""))
                            hSql.Com.Parameters["RINFO"].Value = objRow.RINFO;
                        hSql.Com.Parameters["EXIDNO"].Value = objRow.EXIDNO;
                    }
                    hSql.ExecuteNonQuery();
                }
               
                hSql.Commit();
            }
            catch (Exception ex)
            {
                hSql.Rollback();
                throw ex;
            }
            finally
            {
                hSql.Close();
            }

            return bRet;
        }
    }

    public class SCInvoiceItem
    {
        
        public String ITEMNO { get; set; }
        public String SUPLNO { get; set; }
        public String NAME { get; set; }
        public int RTYPE { get; set; }
        public int SROWID { get; set; }
        public Decimal BUYPR { get; set; }
        public Decimal DISCPC { get; set; }
        public Decimal NUM { get; set; }
        public Decimal RSUM0 { get; set; }
        public Decimal RSUM { get; set; }
        public Decimal UNITPR { get; set; }
        public int IGROUPID { get; set; }
        public String VATCD { get; set; }
        public String NOTE { get; set; }
        public String RINFO { get; set; }
        public int EXIDNO { get; set; }

    }
    public class SCInvoiceUtil
    {

        protected static readonly ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
        public bool invoiceContract(Contract objContract, bool bDraft, bool bManual)
        {
            bool bRet = true;
            int InvoiceSeqNr = 0;
            clsSqlFactory hSql = new clsSqlFactory();
            hSql.NewCommand("select isnull(max(InvoiceNo),0) from ZSC_ContractInvoice where ContractOID=? ");
            hSql.Com.Parameters.Add("ContractOID", objContract.ContractOID);
            hSql.ExecuteReader();
            if (hSql.Read())
            {
                InvoiceSeqNr = hSql.Reader.GetInt32(0);
                InvoiceSeqNr += 1;
            }
            List<String> Payers = new List<String>();
            hSql.NewCommand("select distinct PartialPayer from ZSC_ContractOption where ContractOid = ? and PartialPayer is not null and PartialPayer != '' and SelPr!=0 ");
            hSql.Com.Parameters.Add("ContractOID", objContract.ContractOID);
            hSql.ExecuteReader();
            while (hSql.Read())
            {
                Payers.Add(hSql.Reader.GetString(0));
            }
            
            if (objContract.ContractPaymentData.PaymentIsInBlock == false)
            {
                if (objContract.LastInvoiceDate <= objContract.ContractDateData.InvoiceEndDate)
                {
                    bRet = invoiceContract(objContract, bDraft, bManual, "", InvoiceSeqNr, false);
                    foreach (String Payer in Payers)
                    {
                        bRet = invoiceContract(objContract, bDraft, bManual, Payer, InvoiceSeqNr, false);
                    }

                    if ((objContract.ContractCapitalData.CapitalMonthAmount != 0) && (objContract.ContractCapitalData.CapitalMonthPayer.strValue1 != ""))
                    {
                        bRet = bRet && invoiceContract(objContract, bDraft, bManual, objContract.ContractCapitalData.CapitalMonthPayer.strValue1, InvoiceSeqNr, true);
                    }
                }
            }
            else
            {
                objContract.calcPaymentBlock(true);
                DateTime dtNextBlockEnd = objContract.ContractPaymentData.PaymentNextBlockEnd;
                if (objContract.NextInvoiceDate == DateTime.MinValue) objContract.NextInvoiceDate = DateTime.Now;
                
                while ((objContract.NextInvoiceDate <= dtNextBlockEnd)&&(objContract.LastInvoiceDate <= objContract.ContractDateData.InvoiceEndDate))
                {
                    bRet = invoiceContract(objContract, bDraft, bManual, "", InvoiceSeqNr, false);
                    foreach (String Payer in Payers)
                    {
                        bRet = invoiceContract(objContract, bDraft, bManual, Payer, InvoiceSeqNr, false);
                    }

                    if ((objContract.ContractCapitalData.CapitalMonthAmount != 0) && (objContract.ContractCapitalData.CapitalMonthPayer.strValue1 != ""))
                    {
                        bRet = bRet && invoiceContract(objContract, bDraft, bManual, objContract.ContractCapitalData.CapitalMonthPayer.strValue1, InvoiceSeqNr, true);
                    }
                    objContract = SCBase.searchContracts(objContract.ContractOID);
                    InvoiceSeqNr++;
                }
                objContract.calcPaymentBlock(true);

                bRet = bRet && hSql.NewCommand("update ZSC_Contract set PaymentNextBlockStart = ?,PaymentNextBlockEnd=? where OID=?");

                hSql.Com.Parameters.AddWithValue("PaymentNextBlockStart", objContract.ContractPaymentData.PaymentNextBlockStart);
                hSql.Com.Parameters.AddWithValue("PaymentNextBlockEnd", objContract.ContractPaymentData.PaymentNextBlockEnd);
                hSql.Com.Parameters.AddWithValue("ContractOID", objContract.ContractOID);
                bRet = bRet && hSql.ExecuteNonQuery();
                if (bRet)
                {
                    hSql.Commit();
                    objContract = SCBase.searchContracts(objContract.ContractOID);
                }
                else hSql.Rollback();


            }
            hSql.Close();
            return bRet;
        }
        private bool invoiceContract(Contract objContract, bool bDraft, bool bManual, String Payer, int InvoiceSeqNr, bool bCapital)
        {
            bool bRet = true;
            clsAppConfig objAppConfig = new clsAppConfig();
            clsGlobalVariable objGlobal = new clsGlobalVariable();
            SCInvoice objInv = new SCInvoice();
            clsSqlFactory hSql = new clsSqlFactory();
            objInv.InvoiceSeqNr = InvoiceSeqNr;
             
            if (Payer == "")
            {
                if (objContract.InvoiceCustId != null)
                {
                    objInv.CustNo = objContract.InvoiceCustId.CustNr;
                }
                else
                {
                    objInv.CustNo = objContract.ContractCustId.CustNr;
                }
            }
            else
            {
                objInv.CustNo = objAppConfig.getNumberParam("ZSCCAPPAYE", Payer, "V1", "");
            }
            objInv.DCustNo = objContract.ContractCustId.CustNr;
            objInv.UnitId = objContract.InvoiceSiteId.strValue1;
            objInv.ContractOID = objContract.ContractOID;
            objInv.ExtOrderId = objContract.ExtContractNo;
            if (objInv.ExtOrderId.Length > 10) objInv.ExtOrderId = objInv.ExtOrderId.Substring(0, 10);
            objInv.SalesType = objAppConfig.getStringParam("ZSCSETTING", "TRTYPE", "C3", "");
            objInv.SmanId = objContract.RespSmanId.SmanId;
            
            objInv.TPCODE = objContract.ContractPaymentData.PaymentTerm.strValue1;
            objInv.TPTIME = objAppConfig.getNumberParam("MAKSUEHDOT", objInv.TPCODE, "V1", "");
            if (objContract.CostCenter.strValue1 != "")
            {
                hSql.NewCommand("select isnull(a.C4,'') from CORW a where a.CODAID='ZSCCOSTCC' and a.C3=? and a.C5=? ");
                hSql.Com.Parameters.Add("UNITID", objContract.SiteId.strValue1);
                hSql.Com.Parameters.Add("COSTCENTER", objContract.CostCenter.strValue1);
                hSql.ExecuteReader();
                if (hSql.Read())
                {
                    objInv.DeptId = hSql.Reader.GetString(0);
                }
            }

            objInv.Payer = Payer;
            objInv.OUserId = objGlobal.DMSFirstUserName;
            objInv.VehiId = objContract.VehiId.VehiId;
            objInv.Note = objContract.ContractNo.ToString() + "/" + objContract.VersionNo.ToString()+"/"+ objInv.InvoiceSeqNr.ToString();

            hSql.NewCommand("select isnull(a.C4,''), isnull(a.C5,'') from CORW a, VEHI b where a.CODAID='ZSCCONV' and a.C2='VEHICLASS2BTYPE' and a.C3=b.CLASS and b.VEHIID=? ");
            hSql.Com.Parameters.Add("VEHIID", objContract.VehiId.VehiId);
            hSql.ExecuteReader();
            if (hSql.Read())
            {
                objInv.BTYPE = hSql.Reader.GetString(0);
                objInv.PartPostFix = hSql.Reader.GetString(1);
            }
            if (objContract.ContractPaymentData.PaymentGroupingLevel == PaymentGroupingType.Customer)
                objInv.BTYPE = "0";
            else
            {
              
                
                    if (objContract.PaymentCollecType == PaymentCollectionType.Transfer)
                    {
                        objInv.BTYPE = objAppConfig.getStringParam("ZSCSETTING", "INVCAT4", "C3", "");
                    }
                    else if (objContract.PaymentCollecType == PaymentCollectionType.Debit)
                    {
                        objInv.BTYPE = objAppConfig.getStringParam("ZSCSETTING", "INVCAT5", "C3", "");
                    }
                else if (objContract.PaymentCollecType == PaymentCollectionType.Plain)
                {
                    objInv.BTYPE = objAppConfig.getStringParam("ZSCSETTING", "INVCAT6", "C3", "");
                }
                else
                {
                    if (bManual == true)
                    {
                        objInv.BTYPE = objAppConfig.getStringParam("ZSCSETTING", "INVCAT2", "C3", "");
                    }
                    else
                    {
                        objInv.BTYPE = objAppConfig.getStringParam("ZSCSETTING", "INVCAT1", "C3", "");
                    }
                }
            }
            if (Payer != "")
            {
                objInv.BTYPE = objAppConfig.getStringParam("ZSCCAPPAYE", Payer, "C3", "");
            }
            if (objContract.NextInvoiceDate == DateTime.MinValue)
            {
                objInv.BILLD = DateTime.Now;
            }
            else
            {
                objInv.BILLD = objContract.NextInvoiceDate;
            }
            int nInvoiceDay = objContract.ContractPaymentData.InvoiceDate;
            if(nInvoiceDay<=0)
                nInvoiceDay =  objAppConfig.getNumberParam("ZSCSETTING", "INVDATE", "V1", "");
            if (objInv.BILLD.Day > nInvoiceDay)
            {
                objInv.BILLD = new DateTime(objInv.BILLD.Year, objInv.BILLD.Month, nInvoiceDay);
                objInv.BILLD = objInv.BILLD.AddMonths(1);
            }
            else if (objInv.BILLD.Day < nInvoiceDay)
            {
                objInv.BILLD = new DateTime(objInv.BILLD.Year, objInv.BILLD.Month, nInvoiceDay);
            }
            objInv.CBILLD = new DateTime(objInv.BILLD.Year, objInv.BILLD.Month, objInv.BILLD.Day);
            
            switch (objContract.ContractPaymentData.PaymentPeriod.strValue1)
            {
                case PaymentPeriodType.Quarterly:
                    objInv.NBILLD = objInv.CBILLD.AddMonths(3);
                    break;
                case PaymentPeriodType.HalfYear:
                    objInv.NBILLD = objInv.CBILLD.AddMonths(6);
                    break;
                case PaymentPeriodType.Yearly:
                    objInv.NBILLD = objInv.CBILLD.AddMonths(12);
                    break;
                default:
                    objInv.NBILLD = objInv.CBILLD.AddMonths(1);
                    break;
            }
            objInv.DELD = objInv.BILLD;
            addInvoiceText(hSql, objContract, ref objInv);
            objContract.listContractOptions = ContractOption.getContractOption(objContract.ContractOID);
            //objContract.loadDetail();
            addInvoiceRows(objContract, ref objInv, bCapital);
            hSql.Close();
            bRet = objInv.saveOrder(bDraft);
            if ((bRet == true) && (bManual == true))
            {

                if (bDraft == true)
                {
                    openInvoice(objInv.UnitId, objInv.SSALID, objInv.SRECNO);
                }
                else
                {
                    openInvoicePDF(objInv.UnitId, objInv.SRECNO);
                }
            }
            return bRet;
        }

        private void addInvoiceText(clsSqlFactory hSql, Contract objContract, ref SCInvoice objInv)
        {
            int RowId = objInv.InvItems.Count;
            List<String> strTexts = new List<String>();
            List<String> strPrintingFlags = new List<String>();
            List<String> strFieldCodes = new List<String>();
            String strSql = "select a.C6,b.C3 from CORW a left join CORW b on b.CODAID='INFOTEXT' and a.C4=b.C1 where a.CODAID='ZSCINVTEXT' and a.V1=1 and a.C3 like '%N%' order by a.V2 ";
            hSql.NewCommand(strSql);
            hSql.ExecuteReader();
            while (hSql.Read())
            {
                String strTmp = hSql.Reader.GetString(0);
                strPrintingFlags.Add(hSql.Reader.GetString(1));
                getFieldCodesFromString(strTmp, ref strFieldCodes);
                strTexts.Add(strTmp);
                
            }
            hSql.NewCommand("exec ZSC_SP_PrintContract " + objContract.ContractOID.ToString());
            hSql.ExecuteReader();
            if (hSql.Read())
            {
                int colId = -1;
                foreach (String strFieldCode in strFieldCodes)
                {
                    colId = hSql.Reader.GetOrdinal(strFieldCode);
                    if (colId >= 0)
                    {
                        String strDataType = hSql.Reader.GetDataTypeName(colId).ToUpper();
                        String strValue = "";
                        if (!hSql.Reader.IsDBNull(colId))
                        {
                            switch (strDataType)
                            {
                                case "DATETIME":
                                    strValue = hSql.Reader.GetDateTime(colId).ToShortDateString();
                                    break;
                                case "INT":
                                    strValue = hSql.Reader.GetInt32(colId).ToString();
                                    break;
                                default:
                                    strValue = hSql.Reader.GetString(colId);
                                    break;
                            }
                            for (int i = 0; i < strTexts.Count; i++)
                            {
                                strTexts[i] = strTexts[i].Replace("$" + strFieldCode + "$", strValue);
                            }
                        }
                    }
                }
               for(int i=0;i<strTexts.Count;i++)
                {
                    RowId++;
                    SCInvoiceItem objRow = new SCInvoiceItem();
                    objRow.SROWID = RowId;
                    objRow.NAME = "";
                    objRow.RTYPE = 8;
                    objRow.NOTE = strTexts[i];
                    objRow.RINFO = strPrintingFlags[i];
                    objRow.EXIDNO = objContract.ContractNo;
                    objInv.InvItems.Add(objRow);
                }
            }
         
        }
        private void getFieldCodesFromString(String strText,ref List<String> strFieldCodes)
        {
            int nIndStart=-1;
            int nIndEnd=-1;
            while (true)
            {
                if (nIndStart >= 0)
                {
                    nIndEnd = strText.IndexOf("$");
                    if (nIndEnd < 0)
                        break;
                    else
                    {
                        nIndStart = -1;
                        String strFieldCode = strText.Substring(0, nIndEnd);
                        if ((strFieldCode != "") && strFieldCodes.IndexOf(strFieldCode) < 0)
                        {
                            strFieldCodes.Add(strFieldCode);
                        }
                        strText = strText.Substring(nIndEnd+1,strText.Length-nIndEnd-1);
                    }
                }
                nIndStart = strText.IndexOf("$");
                if (nIndStart < 0)
                    break;
                else
                {
                    nIndEnd = -1;
                    strText = strText.Substring(nIndStart + 1, strText.Length - nIndStart - 1);
                }
            }
        }
        private string getVatCode(clsTaxHandling objTax, String strBType)
        {
            String strRet = objTax.DefaultSalesCode;
            clsAppConfig objAppConfig = new clsAppConfig();
            String strTmp = objAppConfig.getStringParam("VLASKUTYY", strBType, "C5", "");
            if (strTmp.Contains("V")) strRet = "0";
            return strRet;
        }
        private void addInvoiceRows(Contract objContract, ref SCInvoice objInv, bool bCapital)
        {
            clsSqlFactory hSql = new clsSqlFactory();
            int RowId = objInv.InvItems.Count;
            Decimal nInvoiceSum = 0;
            Decimal nInvoiceOrigSum = 0;
            Decimal nInvoiceOrigBuyPr = 0;
            int nPayPeriod = 1;
            clsTaxHandling objTax = new clsTaxHandling();
            objTax.Init(2);
            String RowVatCd = getVatCode(objTax, objInv.BTYPE);
            SCInvoiceItem objRow;
            if (objContract.IsInvoiceDetail == false)
            {
                RowId++;
                objRow = new SCInvoiceItem();
                objRow.SROWID = RowId;
                objRow.NAME = "~1";
                objRow.RTYPE = 8;
                objRow.NOTE = "CONTRACT NO " + objContract.ContractNo.ToString();
                objRow.EXIDNO = objContract.ContractNo;
                objInv.InvItems.Add(objRow);
            }
            switch (objContract.ContractCostData.CostBasis.strValue1)
            {
                case CostBasisType.Monthly:
                    nInvoiceSum = objContract.ContractCostData.CostBasedOnService - objContract.ContractCapitalData.CapitalMonthAmount;
                    break;
                case CostBasisType.KmOrHour:
                    break;
                case CostBasisType.KmOrHourWithLump:
                    nInvoiceSum = objContract.ContractCostData.CostMonthBasis - objContract.ContractCapitalData.CapitalMonthAmount;
                    break;
                default:
                    break;
            }
            switch (objContract.ContractPaymentData.PaymentPeriod.strValue1)
            {
                case PaymentPeriodType.Quarterly:
                    nPayPeriod = 3;
                    break;
                case PaymentPeriodType.HalfYear:
                    nPayPeriod = 6;
                    break;
                case PaymentPeriodType.Yearly:
                    nPayPeriod = 12;
                    break;
                default:
                    break;
            }
            if (bCapital)
            {
                nInvoiceSum = objContract.ContractCapitalData.CapitalMonthAmount* nPayPeriod;
                nInvoiceSum = (Decimal)objTax.Add(nInvoiceSum, RowVatCd, objInv.BILLD);
            }
            else
            {
                if (objInv.Payer != "") nInvoiceSum = 0;

                nInvoiceSum = (Decimal)objTax.Add(nInvoiceSum, RowVatCd, objInv.BILLD);
                nInvoiceSum = nInvoiceSum * nPayPeriod;

            }
            
            foreach (ContractOption objCat in objContract.listContractOptions)
            {
                
                if ((objCat.PartNr != null) && (objCat.PartNr != "") && 
                    (((objCat.PartialPayer==objInv.Payer)&&(bCapital==false))  || ((objCat.PartialPayer == "") && (bCapital == true))
                    )
                    )
                {
                    RowId++;
                    objRow = new SCInvoiceItem();
                    objRow.ITEMNO = objCat.PartNr;
                    if (objInv.PartPostFix != "")
                    {
                        
                        hSql.NewCommand("select 1 from ITEM where ITEMNO=? and SUPLNO=? ");
                        hSql.Com.Parameters.Add("ITEMNO", objRow.ITEMNO+ objInv.PartPostFix);
                        hSql.Com.Parameters.Add("SUPLNO", objCat.PartSuplNo);
                        hSql.ExecuteReader();
                        if (hSql.Read())
                        {
                            objRow.ITEMNO += objInv.PartPostFix;
                        }

                    }
                      
                    objRow.SUPLNO = objCat.PartSuplNo;
                    objRow.NAME = objCat.Name;
                    objRow.RTYPE = 2;
                    objRow.SROWID = RowId;
                    objRow.BUYPR = (Decimal)objTax.Add(objCat.PurchasePr, RowVatCd, objInv.BILLD);
                    objRow.DISCPC = 0;
                    objRow.NUM = 1;
                    objRow.NUM = objRow.NUM * nPayPeriod;
                    objRow.UNITPR = (Decimal)objTax.Add(objCat.SalePr, RowVatCd, objInv.BILLD);
                    objRow.RSUM = (decimal)objRow.NUM * objRow.UNITPR;
                    objRow.IGROUPID = -1;
                    objRow.VATCD = RowVatCd;
                    nInvoiceOrigSum += objRow.RSUM;
                    nInvoiceOrigBuyPr += objRow.BUYPR;
                    objRow.EXIDNO = objContract.ContractNo;
                    objInv.InvItems.Add(objRow);
                    if ((objInv.Payer != "")&& (bCapital == false)) nInvoiceSum += objRow.RSUM;
                }

            }
            if (objContract.IsInvoiceDetail == false)
            {
                RowId++;
                objRow = new SCInvoiceItem();
                objRow.SROWID = RowId;
                objRow.NAME = "~1";
                objRow.RTYPE = 8;
                objRow.NOTE = "-----------------------";
                objRow.EXIDNO = objContract.ContractNo;
                objInv.InvItems.Add(objRow);
            }
            //adapt row sum
            if ((nInvoiceOrigSum != 0)&& (objInv.Payer == "") || (bCapital == true))
            {
                for (int i = 0; i < objInv.InvItems.Count; i++)
                {
                    if (objInv.InvItems[i].RTYPE != 8)
                    {
                        Decimal nOrigRSUM = objInv.InvItems[i].RSUM;
                        objInv.InvItems[i].RSUM = nOrigRSUM * nInvoiceSum / nInvoiceOrigSum;
                        objInv.InvItems[i].BUYPR = objInv.InvItems[i].BUYPR * nInvoiceSum / nInvoiceOrigSum;
                        if (nInvoiceOrigSum > nInvoiceSum)
                        {
                            if (nOrigRSUM != 0)
                            {
                                objInv.InvItems[i].DISCPC = (nOrigRSUM - objInv.InvItems[i].RSUM) / nOrigRSUM;
                            }
                        }
                        else
                        {
                            objInv.InvItems[i].UNITPR = objInv.InvItems[i].RSUM / objInv.InvItems[i].NUM;
                        }
                    }
                }
            }
            hSql.Close();
        }

        public static List<SCInvoice> getContractInvoice(int ContractOID, List<Int32> lstInvoiceType, bool creditInvoice)
        {
            if (lstInvoiceType == null || lstInvoiceType.Count <= 0)
                return new List<SCInvoice>();
            clsSqlFactory hSql = new clsSqlFactory();
            List<SCInvoice> Result = new List<SCInvoice>();
            try
            {
                String strSql = "select a.OID as OID, b.SRECNO , b.BILLD,b.DELD,b.PAIDDATE,b.PAIDSUM,b.CRERECNO,b.CUSTNO,c.LNAME,d.EXPL, b.SSALID,b._UNITID " +
                    ",b.BTYPE,e.SMANID as SMANID ,e.SORDNO as SORDNO,e.DEPT as DEPT, a.InvoiceNo, x.INVSUM,x.INVSUM0 "+
                    "FROM ZSC_ContractInvoice a, all_sbil b LEFT JOIN cust c on b.custno = c.CUSTNO, unit d, all_SSAL e,"+
                    " (select _UNITID,SRECNO,SSALID,sum(isnull(RSUM,0)) as INVSUM,sum(isnull(RSUM/isnull(dbo.fn_AMVATValue(_UNITID,VATCD),1),0)) as INVSUM0 from all_SROW group by _UNITID,SRECNO,SSALID ) x " +
                    "WHERE a.ContractOID = ? and a.SSALID = b.SSALID and a.UnitId = b._UNITID and a.UnitId = d.UnitId and b.SSALID=e.SSALID and b._UNITID=e._UNITID and b._UNITID=x._UNITID and b.SSALID=x.SSALID and b.SRECNO=x.SRECNO ";
                if (creditInvoice == false)
                    strSql += " AND b.CRERECNO IS NULL ";
                if (lstInvoiceType.Count == 1)
                {
                    strSql += " AND a.InvoiceType = ?";
                    strSql += " order by a.InvoiceNo desc ";
                    hSql.NewCommand(strSql);
                    hSql.Com.Parameters.AddWithValue("ContractOID", ContractOID);
                    hSql.Com.Parameters.AddWithValue("InvoiceType", lstInvoiceType[0]);
                }
                else
                {


                    strSql = MyUtils.BuildWhereInClause(strSql, "InvoiceType", lstInvoiceType);
                    strSql += " order by a.OID desc ";
                    hSql.NewCommand(strSql);
                    hSql.Com.Parameters.AddWithValue("ContractOID", ContractOID);
                    int i = 0;
                    foreach (Int32 InvoiceType in lstInvoiceType)
                    {
                        hSql.Com.Parameters.AddWithValue("InvoiceType" + i++, InvoiceType);
                    }
                }

                hSql.ExecuteReader();

                int colId;
                while (hSql.Read())
                {
                    SCInvoice item = new SCInvoice();
                    colId = hSql.Reader.GetOrdinal("OID");
                    if (!hSql.Reader.IsDBNull(colId)) item.OID = hSql.Reader.GetInt32(colId);
                    colId = hSql.Reader.GetOrdinal("SRECNO");
                    if (!hSql.Reader.IsDBNull(colId)) item.SRECNO = hSql.Reader.GetInt32(colId);
                    colId = hSql.Reader.GetOrdinal("BILLD");
                    if (!hSql.Reader.IsDBNull(colId)) item.BILLD = hSql.Reader.GetDateTime(colId);
                    colId = hSql.Reader.GetOrdinal("DELD");
                    if (!hSql.Reader.IsDBNull(colId)) item.DELD = hSql.Reader.GetDateTime(colId);
                    colId = hSql.Reader.GetOrdinal("PAIDDATE");
                    if (!hSql.Reader.IsDBNull(colId)) item.PAIDDATE = hSql.Reader.GetDateTime(colId);
                    colId = hSql.Reader.GetOrdinal("PAIDSUM");
                    if (!hSql.Reader.IsDBNull(colId)) item.PAIDSUM = hSql.Reader.GetInt32(colId);
                    colId = hSql.Reader.GetOrdinal("CRERECNO");
                    if (!hSql.Reader.IsDBNull(colId)) item.SRECNO = hSql.Reader.GetInt32(colId);
                    colId = hSql.Reader.GetOrdinal("CUSTNO");
                    if (!hSql.Reader.IsDBNull(colId)) item.CustNo = hSql.Reader.GetInt32(colId);
                    colId = hSql.Reader.GetOrdinal("LNAME");
                    if (!hSql.Reader.IsDBNull(colId)) item.LNAME = hSql.Reader.GetString(colId);
                    colId = hSql.Reader.GetOrdinal("EXPL");
                    if (!hSql.Reader.IsDBNull(colId)) item.EXPL = hSql.Reader.GetString(colId);
                    colId = hSql.Reader.GetOrdinal("SSALID");
                    if (!hSql.Reader.IsDBNull(colId)) item.SSALID = hSql.Reader.GetInt32(colId);
                    colId = hSql.Reader.GetOrdinal("_UNITID");
                    if (!hSql.Reader.IsDBNull(colId)) item.UnitId = hSql.Reader.GetString(colId);
                    colId = hSql.Reader.GetOrdinal("BTYPE");
                    if (!hSql.Reader.IsDBNull(colId)) item.BTYPE = hSql.Reader.GetString(colId);
                    colId = hSql.Reader.GetOrdinal("SMANID");
                    if (!hSql.Reader.IsDBNull(colId)) item.SmanId = hSql.Reader.GetString(colId);
                    colId = hSql.Reader.GetOrdinal("SORDNO");
                    if (!hSql.Reader.IsDBNull(colId)) item.SORDNO = (Int32)hSql.Reader.GetDecimal(colId);
                    colId = hSql.Reader.GetOrdinal("DEPT");
                    if (!hSql.Reader.IsDBNull(colId)) item.DeptId = hSql.Reader.GetString(colId);
                    colId = hSql.Reader.GetOrdinal("InvoiceNo");
                    if (!hSql.Reader.IsDBNull(colId)) item.InvoiceSeqNr = hSql.Reader.GetInt32(colId);
                    colId = hSql.Reader.GetOrdinal("INVSUM");
                    if (!hSql.Reader.IsDBNull(colId)) item.INVSUM = hSql.Reader.GetDecimal(colId);
                    colId = hSql.Reader.GetOrdinal("INVSUM0");
                    if (!hSql.Reader.IsDBNull(colId)) item.INVSUM0 = hSql.Reader.GetDecimal(colId);
                    Result.Add(item);
                }
            }
            catch (Exception ex)
            {
                _log.Error("ERROR getContractInvoice " + ContractOID + ": ", ex);
                throw ex;
            }
            finally
            {
                hSql.Close();
            }
            return Result;
        }
        public static List<SCInvoiceItem> getInvoiceDetail(Int32 invoiceID)
        {
            if (invoiceID < 0)
                return new List<SCInvoiceItem>();
            clsSqlFactory hSql = new clsSqlFactory();
            List<SCInvoiceItem> Result = new List<SCInvoiceItem>();
            try
            {
                clsGlobalVariable objGlobal = new clsGlobalVariable();
                String strSql = "select isnull(c.ITEM,'') as ITEMNO, isnull(c.NAME,'') as ITEMNAME,isnull(c.VATCD,'') as VATCD,isnull(c.NUM,0) as NUM,isnull(c.RSUM,0) as RSUM,isnull(c.NOTE,'') as NOTE,c.RTYPE as RTYPE " +
                    ", isnull(c.RSUM,0) / isnull(dbo.fn_AMVATValue(c._UNITID,c.VATCD),1) as RSUM0 " +
                    " FROM ZSC_ContractInvoice a, all_sbil b, ALL_SROW c " +
                    " WHERE  a.OID=? and a.SSALID = b.SSALID and a.UnitId = b._UNITID  and b._unitid = c._unitid and b.srecno = c.srecno and b.ssalid = c.ssalid";
                hSql.NewCommand(strSql);
                hSql.Com.Parameters.AddWithValue("OID", invoiceID);
                hSql.ExecuteReader();

                int colId;

                while (hSql.Read())
                {
                    SCInvoiceItem item = new SCInvoiceItem();
                    colId = hSql.Reader.GetOrdinal("ITEMNO");
                    if (!hSql.Reader.IsDBNull(colId)) item.ITEMNO = hSql.Reader.GetString(colId);
                    colId = hSql.Reader.GetOrdinal("ITEMNAME");
                    if (!hSql.Reader.IsDBNull(colId)) item.NAME = hSql.Reader.GetString(colId);
                    colId = hSql.Reader.GetOrdinal("VATCD");
                    if (!hSql.Reader.IsDBNull(colId)) item.VATCD = hSql.Reader.GetString(colId);
                    colId = hSql.Reader.GetOrdinal("NUM");
                    if (!hSql.Reader.IsDBNull(colId)) item.NUM = hSql.Reader.GetDecimal(colId);
                    colId = hSql.Reader.GetOrdinal("RSUM");
                    if (!hSql.Reader.IsDBNull(colId)) item.RSUM = hSql.Reader.GetDecimal(colId);
                    colId = hSql.Reader.GetOrdinal("RSUM0");
                    if (!hSql.Reader.IsDBNull(colId)) item.RSUM0 = hSql.Reader.GetDecimal(colId);
                    colId = hSql.Reader.GetOrdinal("NOTE");
                    if (!hSql.Reader.IsDBNull(colId)) item.NOTE = hSql.Reader.GetString(colId);
                    colId = hSql.Reader.GetOrdinal("RTYPE");
                    if (!hSql.Reader.IsDBNull(colId)) item.RTYPE = hSql.Reader.GetInt16(colId);
                    Result.Add(item);
                }
                
            }
            catch (Exception ex)
            {
                _log.Error("ERROR getInvoiceDetail " + invoiceID + ": ", ex);
                throw ex;
            }
            finally
            {
                hSql.Close();
            }
            return Result;
        }

        public void openInvoice(String UnitId, int SSALID, int SRECNO)
        {
            AMComClient objCom = new AMComClient();
            objCom.openSPOrder(UnitId, SSALID, SRECNO);

        }
        public void openInvoicePDF(String UnitId, int SRECNO)
        {
            AMComClient objCom = new AMComClient();
            objCom.openSPInvoice(UnitId, SRECNO);

        }
    }

}
