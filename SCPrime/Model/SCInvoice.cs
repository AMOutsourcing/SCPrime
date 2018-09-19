using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using nsBaseClass;
using SCPrime.Utils;
using WorkshopMonitorPrime.Model;


namespace SCPrime.Model
{
    public class SCInvoice
    {
        public int OID { get; set; }
        public int CustNo { get; set; }
        public int DCustNo;
        public String UnitId;
        public String LNAME { get; set; }
        public String EXPL { get; set; }
        public int ContractOID;
        public String ExtOrderId;
        public String SalesType;
        public String SmanId;
        public String DeptId;
        public int SORDNO;
        public int SSALID;
        public int SRECNO { get; set; }
        public String OUserId;
        public String Note;
        public String BTYPE;
        public DateTime BILLD { get; set; }
        public DateTime DELD { get; set; }
        public DateTime PAIDDATE { get; set; }
        public int PAIDSUM { get; set; }
        public int VehiId;
        public List<SCInvoiceItem> InvItems = new List<SCInvoiceItem>();

        public bool saveOrder()
        {
            bool bRet = true;
            clsSqlFactory hSql = new clsSqlFactory();
            clsBaseUtility objUtil = new clsBaseUtility();
            clsCurrExchange objCurr = new clsCurrExchange();
            objCurr.Init();
            String strSql = "";
            try
            {
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
                           "(CREATED,SSALID, CUSTNO, SMANID, DEPT, STATUS, RECORDID, SORDNO, STYPE, " +
                           " DIVISION, ISDIVIDED, BOPRIOR, OUSRSID, CDTRAN,NOTE,RDATE,EXIDNO,VEHIID)" +
                           " values (getdate(),?, ?, ?,?, 'A', 0, ?, ?, 0, 0, 0, ?, 0, ?,?,?,?) ";
                bRet = bRet && hSql.NewCommand(strSql);
                hSql.Com.Parameters.AddWithValue("SSALID", SSALID);
                hSql.Com.Parameters.AddWithValue("CUSTNO", CustNo);
                hSql.Com.Parameters.AddWithValue("SMANID", SmanId);
                hSql.Com.Parameters.AddWithValue("DEPT", DeptId);
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
                             " CURRCD, BTYPE)" +
                             " values (?, ?, ?, ?, ?," +
                             " ?, 0, 0, 1, " +
                             "?,?) ";
                bRet = bRet && hSql.NewCommand(strSql);
                hSql.Com.Parameters.AddWithValue("SSALID", SSALID);
                hSql.Com.Parameters.AddWithValue("SRECNO", SRECNO);
                hSql.Com.Parameters.AddWithValue("CUSTNO", CustNo);
                hSql.Com.Parameters.AddWithValue("LCUSTNO", CustNo);
                hSql.Com.Parameters.AddWithValue("DCUSTNO", DCustNo);
                hSql.Com.Parameters.AddWithValue("REFE", Note.Length > 20 ? Note.Substring(0, 20) : Note);
                hSql.Com.Parameters.AddWithValue("CURRCD", objCurr.BaseCurrency);
                hSql.Com.Parameters.AddWithValue("BTYPE", BTYPE);
                bRet = bRet && hSql.ExecuteNonQuery();

                strSql = "update a set a.AGRP = b.AGRP, a.CUSTNAME = ltrim(isnull(b.FNAME,'')+' ' +isnull(b.LNAME,'')) from " +
                    objUtil.getTable("SBIL", UnitId) + " a, CUST b where a.SSALID=? and a.CUSTNO = b.CUSTNO ";
                bRet = bRet && hSql.NewCommand(strSql);
                hSql.Com.Parameters.AddWithValue("SSALID", SSALID);
                bRet = bRet && hSql.ExecuteNonQuery();

                strSql = "update a set a.DADDR1 = b.ADDR1, a.DADDR2 = b.ADDR2,a.DCTRYCD=b.CTRYCD,a.DCOUNTRY=b.COUNTRY," +
                    " a.DNAME= ltrim(isnull(b.FNAME,'')+' ' +isnull(b.LNAME,'')), a.DPO=b.PO,a.DPOSTCD=b.POSTCD,a.DADDR2E=b.ADDR2E from " +
                    objUtil.getTable("SBIL", UnitId) + " a, CUST b where a.SSALID=? and a.DCUSTNO = b.CUSTNO ";
                bRet = bRet && hSql.NewCommand(strSql);
                hSql.Com.Parameters.AddWithValue("SSALID", SSALID);
                bRet = bRet && hSql.ExecuteNonQuery();

                strSql = "insert into ZSC_ContractInvoice(ContractOID,InvoiceNo,SSALID,UnitId,InvoiceType,Created,Modified) select  " +
                    "?,SRECNO,SSALID,?,0,getdate(),getdate() from " + objUtil.getTable("SBIL", UnitId) + " where SSALID=?";
                bRet = bRet && hSql.NewCommand(strSql);
                hSql.Com.Parameters.AddWithValue("ContractOID", ContractOID);
                hSql.Com.Parameters.AddWithValue("UnitId", UnitId);
                hSql.Com.Parameters.AddWithValue("SSALID", SSALID);
                bRet = bRet && hSql.ExecuteNonQuery();
                clsTaxHandling objTax = new clsTaxHandling();
                objTax.Init(6);




                foreach (SCInvoiceItem objRow in InvItems)
                {
                    strSql = "insert into " + objUtil.getTable("SROW", UnitId) + "(CREATED,SSALID, SROWID, SRECNO, SMANID, ITEMNO, SUPLNO, BUYPR, " +
                      " DISCPC, ITEM, SUPL, NAME, NUM, RNO, RTYPE, RSUM, UNITPR, VATCD, " +
                      " ONDEDNUM, ORDNUM, IGROUPID,NOTE,RINFO) values (getdate(),?,?,?,?,?,?,?," +
                      " ?,?,?,?,?,?,?,?,?,?," +
                      "0,?,?,?,?)";
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

                    }
                    else
                    {
                        hSql.Com.Parameters["SROWID"].Value = objRow.SROWID;
                        hSql.Com.Parameters["NAME"].Value = objRow.NAME;
                        hSql.Com.Parameters["RNO"].Value = objRow.SROWID;
                        hSql.Com.Parameters["RTYPE"].Value = objRow.RTYPE;
                        hSql.Com.Parameters["NOTE"].Value = objRow.NOTE;
                        if ((objRow.RINFO != null) && (objRow.RINFO != ""))
                            hSql.Com.Parameters["RINFO"].Value = objRow.RINFO;
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
        public int SROWID;
        public Decimal BUYPR;
        public Decimal DISCPC;
        public Decimal NUM { get; set; }
        public Decimal RSUM0;
        public Decimal RSUM { get; set; }
        public Decimal UNITPR;
        public int IGROUPID;
        public String VATCD { get; set; }
        public String NOTE { get; set; }
        public String RINFO { get; set; }

    }
    public class SCInvoiceUtil
    {
        protected static readonly ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public bool invoiceContract(Contract objContract, bool bDraft, bool bManual)
        {
            bool bRet = true;
            clsAppConfig objAppConfig = new clsAppConfig();
            clsGlobalVariable objGlobal = new clsGlobalVariable();
            SCInvoice objInv = new SCInvoice();
            // objInv.CustNo = objContract.InvoiceCustId.CustNo;
            clsSqlFactory hSql = new clsSqlFactory();
            hSql.NewCommand("select CUSTNO from CUST where CUSTID = ?");
            hSql.Com.Parameters.Add("CUSTID", objContract.ContractCustId.CustId);
            hSql.ExecuteReader();
            hSql.Read();
            int nCustNo = hSql.Reader.GetInt32(0);
            objInv.CustNo = nCustNo;
            objInv.DCustNo = nCustNo;
            objInv.UnitId = objContract.InvoiceSiteId.strValue1;
            objInv.ContractOID = objContract.ContractOID;
            objInv.ExtOrderId = objContract.ExtContractNo;
            objInv.SalesType = objAppConfig.getStringParam("ZSCSETTING", "TRTYPE", "C3", "");
            objInv.SmanId = objContract.RespSmanId.SmanId;
            objInv.DeptId = objContract.CostCenter.strValue1;
            objInv.OUserId = objGlobal.DMSFirstUserName;
            objInv.VehiId = objContract.VehiId.VehiId;
            objInv.Note = objContract.ContractNo.ToString() + "/" + objContract.VersionNo.ToString();
            if (objContract.ContractPaymentData.PaymentGroupingLevel == PaymentGroupingType.Customer)
                objInv.BTYPE = "0";
            else
            {
                hSql.NewCommand("select isnull(C4,'') from CORW a, VEHI b where a.CODAID='ZSCCONV' and a.C2='VEHICLASS2BTYPE' and a.C3=b.CLASS and b.VEHIID=? ");
                hSql.Com.Parameters.Add("VEHIID", objContract.VehiId.VehiId);
                hSql.ExecuteReader();
                if (hSql.Read())
                {
                    objInv.BTYPE = hSql.Reader.GetString(0);
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
            objInv.BILLD = DateTime.Now;
            objInv.DELD = DateTime.Now;
            addInvoiceText(hSql, objContract, ref objInv);

            objContract.loadDetail();
            addInvoiceRows(objContract, ref objInv);
            hSql.Close();

            //@ThuyetLV: LHD: hinh nhu anh comment cai nay?
            bRet = objInv.saveOrder();
            //@ThuyetLV: LHD: va tam comment cai phan nay di
            //if ((bRet == true) && (bManual == true))
            //{
            //    AMComClient objCom = new AMComClient();
            //    objCom.openSPOrder(objInv.UnitId, objInv.SSALID, objInv.SRECNO);
            //    //objCom.openSPInvoice(objInv.UnitId, 4000036);
            //}
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
                for (int i = 0; i < strTexts.Count; i++)
                {
                    RowId++;
                    SCInvoiceItem objRow = new SCInvoiceItem();
                    objRow.SROWID = RowId;
                    objRow.NAME = "";
                    objRow.RTYPE = 8;
                    objRow.NOTE = strTexts[i];
                    objRow.RINFO = strPrintingFlags[i];
                    objInv.InvItems.Add(objRow);
                }
            }

        }
        private void getFieldCodesFromString(String strText, ref List<String> strFieldCodes)
        {
            int nIndStart = -1;
            int nIndEnd = -1;
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
                        strText = strText.Substring(nIndEnd + 1, strText.Length - nIndEnd - 1);
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
        private void addInvoiceRows(Contract objContract, ref SCInvoice objInv)
        {
            int RowId = objInv.InvItems.Count;
            Decimal nInvoiceSum = 0;
            Decimal nInvoiceOrigSum = 0;
            int nPayPeriod = 1;
            clsTaxHandling objTax = new clsTaxHandling();
            objTax.Init(2);
            SCInvoiceItem objRow;
            if (objContract.IsInvoiceDetail == false)
            {
                RowId++;
                objRow = new SCInvoiceItem();
                objRow.SROWID = RowId;
                objRow.NAME = "~1";
                objRow.RTYPE = 8;
                objRow.NOTE = "CONTRACT NO " + objContract.ContractNo.ToString();
                objInv.InvItems.Add(objRow);
            }
            switch (objContract.ContractCostData.CostBasis.strValue1)
            {
                case CostBasisType.Monthly:
                    nInvoiceSum = objContract.ContractCostData.CostBasedOnService + objContract.ContractCapitalData.CapitalMonthAmount;
                    break;
                case CostBasisType.KmOrHour:
                    break;
                case CostBasisType.KmOrHourWithLump:
                    nInvoiceSum = objContract.ContractCostData.CostMonthBasis + objContract.ContractCapitalData.CapitalMonthAmount;
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
            nInvoiceSum = (Decimal)objTax.Add(nInvoiceSum, objTax.DefaultSalesCode, objInv.BILLD);
            nInvoiceSum = nInvoiceSum * nPayPeriod;

            foreach (SCOptionCategory objCat in objContract.OptionCategories)
            {
                if ((objCat.ItemNo != null) && (objCat.ItemNo != ""))
                {
                    RowId++;
                    objRow = new SCInvoiceItem();
                    objRow.ITEMNO = objCat.ItemNo;
                    objRow.SUPLNO = objCat.ItemSuplNo;
                    objRow.NAME = objCat.ItemName;
                    objRow.RTYPE = 2;
                    objRow.SROWID = RowId;
                    objRow.BUYPR = objCat.BuyPr;
                    objRow.DISCPC = 0;
                    objRow.NUM = objCat.Quantity;
                    if (objRow.NUM == 0) objRow.NUM = 1;
                    objRow.NUM = objRow.NUM * nPayPeriod;
                    objRow.UNITPR = (Decimal)objTax.Add(objCat.SelPr, objTax.DefaultSalesCode, objInv.BILLD);
                    objRow.RSUM = (decimal)objRow.NUM * objRow.UNITPR;
                    objRow.IGROUPID = -1;
                    objRow.VATCD = objTax.DefaultSalesCode;
                    nInvoiceOrigSum += objRow.RSUM;
                    objInv.InvItems.Add(objRow);
                }
                foreach (SCOption objOpt in objCat.Options)
                {
                    if ((objOpt.ItemNo != null) && (objOpt.ItemNo != ""))
                    {
                        RowId++;
                        objRow = new SCInvoiceItem();
                        objRow.ITEMNO = objOpt.ItemNo;
                        objRow.SUPLNO = objOpt.ItemSuplNo;
                        objRow.NAME = objOpt.ItemName;
                        objRow.RTYPE = 2;
                        objRow.SROWID = RowId;
                        objRow.BUYPR = objOpt.BuyPr;
                        objRow.DISCPC = 0;
                        objRow.NUM = objOpt.Quantity;
                        if (objRow.NUM == 0) objRow.NUM = 1;
                        objRow.NUM = objRow.NUM * nPayPeriod;
                        objRow.UNITPR = (Decimal)objTax.Add(objOpt.SelPr, objTax.DefaultSalesCode, objInv.BILLD);
                        objRow.RSUM = (decimal)objRow.NUM * objRow.UNITPR;
                        objRow.IGROUPID = -1;
                        objRow.VATCD = objTax.DefaultSalesCode;
                        nInvoiceOrigSum += objRow.RSUM;
                        objInv.InvItems.Add(objRow);
                    }
                    foreach (SCOptionDetail objOptDetail in objOpt.OptionDetails)
                    {
                        if ((objOptDetail.ItemNo != null) && (objOptDetail.ItemNo != ""))
                        {
                            RowId++;
                            objRow = new SCInvoiceItem();
                            objRow.ITEMNO = objOptDetail.ItemNo;
                            objRow.SUPLNO = objOptDetail.ItemSuplNo;
                            objRow.NAME = objOptDetail.ItemName;
                            objRow.RTYPE = 2;
                            objRow.SROWID = RowId;
                            objRow.BUYPR = objOptDetail.BuyPr;
                            objRow.DISCPC = 0;
                            objRow.NUM = objOptDetail.Quantity;
                            if (objRow.NUM == 0) objRow.NUM = 1;
                            objRow.NUM = objRow.NUM * nPayPeriod;
                            objRow.UNITPR = (Decimal)objTax.Add(objOptDetail.SelPr, objTax.DefaultSalesCode, objInv.BILLD);
                            objRow.RSUM = (decimal)objRow.NUM * objRow.UNITPR;
                            objRow.IGROUPID = -1;
                            objRow.VATCD = objTax.DefaultSalesCode;
                            nInvoiceOrigSum += objRow.RSUM;
                            objInv.InvItems.Add(objRow);
                        }
                    }
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
                objInv.InvItems.Add(objRow);
            }
            //adapt row sum
            if (nInvoiceOrigSum != 0)
            {
                for (int i = 0; i < objInv.InvItems.Count; i++)
                {
                    if (objInv.InvItems[i].RTYPE != 8)
                    {
                        Decimal nOrigRSUM = objInv.InvItems[i].RSUM;
                        objInv.InvItems[i].RSUM = nOrigRSUM * nInvoiceSum / nInvoiceOrigSum;
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
        }

        public static List<SCInvoice> getContractInvoice(int ContractOID, List<Int32> lstInvoiceType, bool creditInvoice)
        {
            if (lstInvoiceType == null || lstInvoiceType.Count <= 0)
                return new List<SCInvoice>();
            clsSqlFactory hSql = new clsSqlFactory();
            List<SCInvoice> Result = new List<SCInvoice>();
            try
            {
                String strSql = "select a.OID as OID, b.SRECNO , b.BILLD,b.DELD,b.PAIDDATE,b.PAIDSUM,b.CRERECNO,b.CUSTNO,c.LNAME,d.EXPL " +
                    "FROM ZSC_ContractInvoice a, all_sbil b LEFT JOIN cust c on b.custno = c.CUSTNO, unit d " +
                    "WHERE a.ContractOID = ? and a.SSALID = b.SSALID and a.UnitId = b._UNITID and a.UnitId = d.UnitId ";
                if (creditInvoice)
                    strSql += " AND b.CRERECNO IS NULL ";
                if (lstInvoiceType.Count == 1)
                {
                    strSql += " AND a.InvoiceType = ?";
                    hSql.NewCommand(strSql);
                    hSql.Com.Parameters.AddWithValue("ContractOID", ContractOID);
                    hSql.Com.Parameters.AddWithValue("InvoiceType", lstInvoiceType[0]);
                }
                else
                {
                    //if (lstInvoiceType.Count > 0)
                    //{
                    //    strSql += " and a.ContractTypeOID in (" + lstInvoiceType[0].ToString() + "";
                    //    for (int j = 1; j < lstInvoiceType.Count; j++)
                    //    {
                    //        strSql += "," + lstInvoiceType[j].ToString() + "";
                    //    }
                    //    strSql += ")";
                    //}

                    strSql = MyUtils.BuildWhereInClause(strSql, "InvoiceType", lstInvoiceType);
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
                String strSql = "select c.ITEM,c.NAME,c.VATCD,c.NUM,c.RSUM,c.NOTE,c.RTYPE " +
                    " FROM ZSC_ContractInvoice a, all_sbil b, ALL_SROW c " +
                    " WHERE  a.OID=? and a.SSALID = b.SSALID and a.UnitId = b._UNITID  and b._unitid = c._unitid and b.srecno = c.srecno and b.ssalid = c.ssalid";
                hSql.NewCommand(strSql);
                hSql.Com.Parameters.AddWithValue("OID", invoiceID);
                hSql.ExecuteReader();

                int colId;
                while (hSql.Read())
                {
                    SCInvoiceItem item = new SCInvoiceItem();
                    colId = hSql.Reader.GetOrdinal("ITEM");
                    if (!hSql.Reader.IsDBNull(colId)) item.ITEMNO = hSql.Reader.GetString(colId);
                    colId = hSql.Reader.GetOrdinal("NAME");
                    if (!hSql.Reader.IsDBNull(colId)) item.NAME = hSql.Reader.GetString(colId);
                    colId = hSql.Reader.GetOrdinal("VATCD");
                    if (!hSql.Reader.IsDBNull(colId)) item.VATCD = hSql.Reader.GetString(colId);
                    colId = hSql.Reader.GetOrdinal("NUM");
                    if (!hSql.Reader.IsDBNull(colId)) item.NUM = hSql.Reader.GetDecimal(colId);
                    colId = hSql.Reader.GetOrdinal("RSUM");
                    if (!hSql.Reader.IsDBNull(colId)) item.RSUM = hSql.Reader.GetDecimal(colId);
                    colId = hSql.Reader.GetOrdinal("NOTE");
                    if (!hSql.Reader.IsDBNull(colId)) item.NOTE = hSql.Reader.GetString(colId);
                    colId = hSql.Reader.GetOrdinal("RTYPE");
                    if (!hSql.Reader.IsDBNull(colId)) item.RTYPE = hSql.Reader.GetInt32(colId);
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
    }

}
