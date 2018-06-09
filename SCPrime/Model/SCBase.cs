using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using nsBaseClass;
using System.Configuration;


namespace SCPrime.Model
{
    public class SCOptionCategory : SCOptionBase
    {
        public int InvoiceFlag;
        public List<SCOption> Options = new List<SCOption>();

        public static List<SCOptionCategory> getContractOptionCategoryPriceList(int ContractTypeOID)
        {
            //get list from master
            List<SCOptionCategory> Result = new List<SCOptionCategory>();
            clsSqlFactory hSql = new clsSqlFactory();
            try
            {

                String strSql = " select a.OID,a.Name,isnull(a.ItemNo,''),isnull(a.ItemSuplNo,''),isnull(a.WrksId,''),isnull(a.SelPr,0),isnull(a.InvoiceFlag,0),isnull(b.NAME,''),isnull(b.BUYPR,0),isnull(c.NAME,'') " +
                    ", isnull(d.IsAvailable,0), isnull(d.Info,'') from ZSC_OptionCategory a left join ITEM b on a.ITEMNO=b.ITEMNO and a.ITEMSUPLNO=b.SUPLNO left join WRKS c on a.WRKSID=c.WRKSID and c.WPTYPE='T' "+
                    " left join ZSC_OptionPriceList d on a.OID = d.OptionCategoryOID and d.ContractTypeOID =? and d.OptionOID is null and d.OptionDetailOID is null " +
                    " order by a.OID ";
                hSql.NewCommand(strSql);
                hSql.Com.Parameters.AddWithValue("ContractTypeOID", ContractTypeOID);
                hSql.ExecuteReader();
                while (hSql.Read())
                {
                    SCOptionCategory item = new SCOptionCategory();
                    item.OID = hSql.Reader.GetInt32(0);
                    item.Name = hSql.Reader.GetString(1);
                    item.ItemNo = hSql.Reader.GetString(2);
                    item.ItemSuplNo = hSql.Reader.GetString(3);
                    item.WrksId = hSql.Reader.GetString(4);
                    item.BaseSelPr = hSql.Reader.GetDecimal(5);
                    item.InvoiceFlag = hSql.Reader.GetInt32(6);
                    item.ItemName = hSql.Reader.GetString(7);
                    item.BuyPr = hSql.Reader.GetDecimal(8);
                    item.WrksName = hSql.Reader.GetString(9);
                    item.Options = SCOption.getContractOptionPriceList(item.OID, ContractTypeOID);
                    item.isAvailable = hSql.Reader.GetInt32(10);
                    item.Info = hSql.Reader.GetString(11);
                    Result.Add(item);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                hSql.Close();
            }
            return Result;
        }

        public static List<SCOptionCategory> getOptionCategoryList()
        { 
            //get list from master
           List<SCOptionCategory> Result = new List<SCOptionCategory>();
           clsSqlFactory hSql = new clsSqlFactory();
            try
            {
               
                String strSql = " select a.OID,a.Name,isnull(a.ItemNo,''),isnull(a.ItemSuplNo,''),isnull(a.WrksId,''),isnull(a.SelPr,0),isnull(a.InvoiceFlag,0),isnull(b.NAME,''),isnull(b.BUYPR,0),isnull(c.NAME,'') " +
                    " from ZSC_OptionCategory a left join ITEM b on a.ITEMNO=b.ITEMNO and a.ITEMSUPLNO=b.SUPLNO left join WRKS c on a.WRKSID=c.WRKSID and c.WPTYPE='T' order by a.OID ";
                hSql.NewCommand(strSql);
                hSql.ExecuteReader();
                while (hSql.Read())
                {
                    SCOptionCategory item = new SCOptionCategory();
                    item.OID = hSql.Reader.GetInt32(0);
                    item.Name = hSql.Reader.GetString(1);
                    item.ItemNo = hSql.Reader.GetString(2);
                    item.ItemSuplNo = hSql.Reader.GetString(3);
                    item.WrksId = hSql.Reader.GetString(4);
                    item.BaseSelPr = hSql.Reader.GetDecimal(5);
                    item.InvoiceFlag = hSql.Reader.GetInt32(6);
                    item.ItemName = hSql.Reader.GetString(7);
                    item.BuyPr = hSql.Reader.GetDecimal(8);
                    item.WrksName = hSql.Reader.GetString(9);
                    item.Options = SCOption.getOptionList(item.OID);
                    Result.Add(item);
                }           
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                hSql.Close();
            }
            return Result;
        }

        public bool saveOptions(clsSqlFactory hSql)
        {
            bool bRet = true;
            try
            {
                foreach (SCOption objOption in Options)
                {
                    if (objOption.OID > 0)
                    {
                        if (objOption.isMarkDeleted == true)
                        {
                            //delete
                            bRet = hSql.NewCommand("delete from ZSC_OptionPriceList where OptionOID=?");
                            hSql.Com.Parameters.AddWithValue("OID", objOption.OID);
                            bRet = bRet && hSql.ExecuteNonQuery();
                            bRet = hSql.NewCommand("delete from ZSC_OptionDetail where OptionOID =? ");
                            hSql.Com.Parameters.AddWithValue("OID", objOption.OID);
                            bRet = bRet && hSql.ExecuteNonQuery();
                            bRet = hSql.NewCommand("delete from ZSC_Option where OID=? ");
                            hSql.Com.Parameters.AddWithValue("OID", objOption.OID);
                            bRet = bRet && hSql.ExecuteNonQuery();
                        }
                        else
                        {
                            //update
                            bRet = hSql.NewCommand("update ZSC_Option set Name=?,ItemNo=?,ItemSuplNo=?,WrksId=?,SelPr=?,Modified=getdate() where OID=?");
                            hSql.Com.Parameters.AddWithValue("Name", objOption.Name);
                            hSql.Com.Parameters.AddWithValue("ItemNo", objOption.ItemNo);
                            hSql.Com.Parameters.AddWithValue("ItemSuplNo", objOption.ItemSuplNo);
                            hSql.Com.Parameters.AddWithValue("WrksId", objOption.WrksId);
                            hSql.Com.Parameters.AddWithValue("SelPr", objOption.SelPr);
                            hSql.Com.Parameters.AddWithValue("OID", objOption.OID);
                            bRet = bRet && hSql.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        //insert
                        bRet = hSql.NewCommand("insert into ZSC_Option (Name,ItemNo,ItemSuplNo,WrksId,SelPr,Modified,Created,OptionCategoryOID) values (?,?,?,?,?,getdate(),getdate(),?) ");
                        hSql.Com.Parameters.AddWithValue("Name", objOption.Name);
                        hSql.Com.Parameters.AddWithValue("ItemNo", objOption.ItemNo);
                        hSql.Com.Parameters.AddWithValue("ItemSuplNo", objOption.ItemSuplNo);
                        hSql.Com.Parameters.AddWithValue("WrksId", objOption.WrksId);
                        hSql.Com.Parameters.AddWithValue("SelPr", objOption.SelPr);
                        hSql.Com.Parameters.AddWithValue("OptionCategoryOID", this.OID);
                        bRet = bRet && hSql.ExecuteNonQuery();
                        bRet = hSql.NewCommand("select max(OID) from  ZSC_Option ");
                        bRet = bRet && hSql.ExecuteReader() && hSql.Read();
                        objOption.OID = hSql.Reader.GetInt32(0);
                    }
                    if (objOption.isMarkDeleted == false) bRet = objOption.saveOptionDetails(hSql);
                }
                

            }
            catch (Exception ex)
            {
                bRet = false;
                hSql.Rollback();
                throw ex;
            }
            return bRet;
        }

        public static bool saveOptionCategoryList(List<SCOptionCategory> listCategories)
        {
            bool bRet = true;
            clsSqlFactory hSql = new clsSqlFactory();
            try
            {
                foreach (SCOptionCategory objCategory in listCategories)
                {
                    if (objCategory.OID > 0)
                    {
                        if (objCategory.isMarkDeleted == true)
                        {
                            //delete
                            bRet = hSql.NewCommand("delete from ZSC_OptionPriceList where OptionCategoryOID=?");
                            hSql.Com.Parameters.AddWithValue("OID", objCategory.OID);
                            bRet = bRet && hSql.ExecuteNonQuery();
                            bRet = hSql.NewCommand("delete from ZSC_OptionDetail where OptionOID in (select OID from ZSC_Option where OptionCategoryOID=?) ");
                            hSql.Com.Parameters.AddWithValue("OID", objCategory.OID);
                            bRet = bRet && hSql.ExecuteNonQuery();
                            bRet = hSql.NewCommand("delete from ZSC_Option where OptionCategoryOID=? ");
                            hSql.Com.Parameters.AddWithValue("OID", objCategory.OID);
                            bRet = bRet && hSql.ExecuteNonQuery();
                            bRet = hSql.NewCommand("delete from ZSC_OptionCategory where OID=? ");
                            hSql.Com.Parameters.AddWithValue("OID", objCategory.OID);
                            bRet = bRet && hSql.ExecuteNonQuery();
                        }
                        else
                        {
                            //update
                            bRet = hSql.NewCommand("update ZSC_OptionCategory set Name=?,ItemNo=?,ItemSuplNo=?,WrksId=?,SelPr=?,InvoiceFlag=?,Modified=getdate() where OID=?");
                            hSql.Com.Parameters.AddWithValue("Name", objCategory.Name);
                            hSql.Com.Parameters.AddWithValue("ItemNo", objCategory.ItemNo);
                            hSql.Com.Parameters.AddWithValue("ItemSuplNo", objCategory.ItemSuplNo);
                            hSql.Com.Parameters.AddWithValue("WrksId", objCategory.WrksId);
                            hSql.Com.Parameters.AddWithValue("SelPr", objCategory.SelPr);
                            hSql.Com.Parameters.AddWithValue("InvoiceFlag", objCategory.InvoiceFlag);
                            hSql.Com.Parameters.AddWithValue("OID", objCategory.OID);
                            bRet = bRet && hSql.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        //insert
                        bRet = hSql.NewCommand("insert into ZSC_OptionCategory (Name,ItemNo,ItemSuplNo,WrksId,SelPr,InvoiceFlag,Modified,Created) values (?,?,?,?,?,?,getdate(),getdate()) ");
                        hSql.Com.Parameters.AddWithValue("Name", objCategory.Name);
                        hSql.Com.Parameters.AddWithValue("ItemNo", objCategory.ItemNo);
                        hSql.Com.Parameters.AddWithValue("ItemSuplNo", objCategory.ItemSuplNo);
                        hSql.Com.Parameters.AddWithValue("WrksId", objCategory.WrksId);
                        hSql.Com.Parameters.AddWithValue("SelPr", objCategory.SelPr);
                        hSql.Com.Parameters.AddWithValue("InvoiceFlag", objCategory.InvoiceFlag);
                        bRet = bRet && hSql.ExecuteNonQuery();
                        bRet = hSql.NewCommand("select max(OID) from  ZSC_OptionCategory ");
                        bRet = bRet && hSql.ExecuteReader() && hSql.Read();
                        objCategory.OID = hSql.Reader.GetInt32(0);
                    }
                    if (objCategory.isMarkDeleted == false) bRet = objCategory.saveOptions(hSql);
                }
                hSql.Commit();

            }
            catch (Exception ex)
            {
                bRet = false;
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
    public class SCOption : SCOptionBase
    {
        public List<SCOptionDetail> OptionDetails = new List<SCOptionDetail>();
        public static List<SCOption> getContractOptionPriceList(int OptionCategoryOID, int ContractTypeOID)
        {
            List<SCOption> Result = new List<SCOption>();
            clsSqlFactory hSql = new clsSqlFactory();
            try
            {

                String strSql = " select a.OID,a.Name,isnull(a.ItemNo,''),isnull(a.ItemSuplNo,''),isnull(a.WrksId,''),isnull(a.SelPr,0),null,isnull(b.NAME,''),isnull(b.BUYPR,0),isnull(c.NAME,'') " +
                    ", isnull(d.IsAvailable,0), isnull(d.Info,'') from ZSC_Option a left join ITEM b on a.ITEMNO=b.ITEMNO and a.ITEMSUPLNO=b.SUPLNO left join WRKS c on a.WRKSID=c.WRKSID and c.WPTYPE='T'  " +
                    " left join ZSC_OptionPriceList d on a.OptionCategoryOID = d.OptionCategoryOID and d.ContractTypeOID =? and d.OptionOID =a.OID and d.OptionDetailOID is null " +
                    " where a.OptionCategoryOID=? order by a.Name ";
                hSql.NewCommand(strSql);
                hSql.Com.Parameters.AddWithValue("ContractTypeOID", ContractTypeOID);
                hSql.Com.Parameters.AddWithValue("OptionCategoryOID", OptionCategoryOID);
                hSql.ExecuteReader();
                while (hSql.Read())
                {
                    SCOption item = new SCOption();
                    item.OID = hSql.Reader.GetInt32(0);
                    item.Name = hSql.Reader.GetString(1);
                    item.ItemNo = hSql.Reader.GetString(2);
                    item.ItemSuplNo = hSql.Reader.GetString(3);
                    item.WrksId = hSql.Reader.GetString(4);
                    item.BaseSelPr = hSql.Reader.GetDecimal(5);
                    item.ItemName = hSql.Reader.GetString(7);
                    item.BuyPr = hSql.Reader.GetDecimal(8);
                    item.WrksName = hSql.Reader.GetString(9);
                    item.OptionDetails = SCOptionDetail.getContractOptionDetailPriceList(item.OID, OptionCategoryOID, ContractTypeOID);
                    item.isAvailable = hSql.Reader.GetInt32(10);
                    item.Info = hSql.Reader.GetString(11);
                    Result.Add(item);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                hSql.Close();
            }
            return Result;
        }
        public static List<SCOption> getOptionList(int OptionCategoryOID)
        {
            List<SCOption> Result = new List<SCOption>();
            clsSqlFactory hSql = new clsSqlFactory();
            try
            {

                String strSql = " select a.OID,a.Name,isnull(a.ItemNo,''),isnull(a.ItemSuplNo,''),isnull(a.WrksId,''),isnull(a.SelPr,0),null,isnull(b.NAME,''),isnull(b.BUYPR,0),isnull(c.NAME,'') " +
                    " from ZSC_Option a left join ITEM b on a.ITEMNO=b.ITEMNO and a.ITEMSUPLNO=b.SUPLNO left join WRKS c on a.WRKSID=c.WRKSID and c.WPTYPE='T' where a.OptionCategoryOID=? order by a.Name ";
                hSql.NewCommand(strSql);
                hSql.Com.Parameters.AddWithValue("OptionCategoryOID", OptionCategoryOID);
                hSql.ExecuteReader();
                while (hSql.Read())
                {
                    SCOption item = new SCOption();
                    item.OID = hSql.Reader.GetInt32(0);
                    item.Name = hSql.Reader.GetString(1);
                    item.ItemNo = hSql.Reader.GetString(2);
                    item.ItemSuplNo = hSql.Reader.GetString(3);
                    item.WrksId = hSql.Reader.GetString(4);
                    item.BaseSelPr = hSql.Reader.GetDecimal(5);
                    item.ItemName = hSql.Reader.GetString(7);
                    item.BuyPr = hSql.Reader.GetDecimal(8);
                    item.WrksName = hSql.Reader.GetString(9);
                    item.OptionDetails = SCOptionDetail.getOptionDetailList(item.OID);
                    Result.Add(item);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                hSql.Close();
            }
            return Result;
        }
        public bool saveOptionDetails(clsSqlFactory hSql)
        {
            bool bRet = true;
            try
            {
                foreach (SCOptionDetail objOptionDetail in OptionDetails)
                {
                    if (objOptionDetail.OID > 0)
                    {
                        if (objOptionDetail.isMarkDeleted == true)
                        {
                            //delete
                            bRet = hSql.NewCommand("delete from ZSC_OptionPriceList where OptionDetailOID=?");
                            hSql.Com.Parameters.AddWithValue("OID", objOptionDetail.OID);
                            bRet = bRet && hSql.ExecuteNonQuery();
                            bRet = hSql.NewCommand("delete from ZSC_OptionDetail where OID =? ");
                            hSql.Com.Parameters.AddWithValue("OID", objOptionDetail.OID);
                            bRet = bRet && hSql.ExecuteNonQuery();
                        }
                        else
                        {
                            //update
                            bRet = hSql.NewCommand("update ZSC_OptionDetail set Name=?,ItemNo=?,ItemSuplNo=?,WrksId=?,SelPr=?,Modified=getdate() where OID=?");
                            hSql.Com.Parameters.AddWithValue("Name", objOptionDetail.Name);
                            hSql.Com.Parameters.AddWithValue("ItemNo", objOptionDetail.ItemNo);
                            hSql.Com.Parameters.AddWithValue("ItemSuplNo", objOptionDetail.ItemSuplNo);
                            hSql.Com.Parameters.AddWithValue("WrksId", objOptionDetail.WrksId);
                            hSql.Com.Parameters.AddWithValue("SelPr", objOptionDetail.SelPr);
                            hSql.Com.Parameters.AddWithValue("OID", objOptionDetail.OID);
                            bRet = bRet && hSql.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        //insert
                        bRet = hSql.NewCommand("insert into ZSC_OptionDetail (Name,ItemNo,ItemSuplNo,WrksId,SelPr,Modified,Created,OptionOID) values (?,?,?,?,?,getdate(),getdate(),?) ");
                        hSql.Com.Parameters.AddWithValue("Name", objOptionDetail.Name);
                        hSql.Com.Parameters.AddWithValue("ItemNo", objOptionDetail.ItemNo);
                        hSql.Com.Parameters.AddWithValue("ItemSuplNo", objOptionDetail.ItemSuplNo);
                        hSql.Com.Parameters.AddWithValue("WrksId", objOptionDetail.WrksId);
                        hSql.Com.Parameters.AddWithValue("SelPr", objOptionDetail.SelPr);
                        hSql.Com.Parameters.AddWithValue("OptionOID", this.OID);
                        bRet = bRet && hSql.ExecuteNonQuery();
                        bRet = hSql.NewCommand("select max(OID) from  ZSC_OptionDetail ");
                        bRet = bRet && hSql.ExecuteReader() && hSql.Read();
                        objOptionDetail.OID = hSql.Reader.GetInt32(0);
                    }
                    
                }


            }
            catch (Exception ex)
            {
                bRet = false;
                hSql.Rollback();
                throw ex;
            }
            return bRet;
        }
    }
    public class SCOptionDetail : SCOptionBase
    {
        public static List<SCOptionDetail> getContractOptionDetailPriceList(int OptionOID, int OptionCategoryOID, int ContractTypeOID)
        {
            List<SCOptionDetail> Result = new List<SCOptionDetail>();
            clsSqlFactory hSql = new clsSqlFactory();
            try
            {

                String strSql = " select a.OID,a.Name,isnull(a.ItemNo,''),isnull(a.ItemSuplNo,''),isnull(a.WrksId,''),isnull(a.SelPr,0),null,isnull(b.NAME,''),isnull(b.BUYPR,0),isnull(c.NAME,'') " +
                    ", isnull(d.IsAvailable,0), isnull(d.Info,'')  from ZSC_OptionDetail a left join ITEM b on a.ITEMNO=b.ITEMNO and a.ITEMSUPLNO=b.SUPLNO left join WRKS c on a.WRKSID=c.WRKSID and c.WPTYPE='T' " +
                    " left join ZSC_OptionPriceList d on d.OptionCategoryOID = ? and d.ContractTypeOID =? and d.OptionOID =a.OptionOID and d.OptionDetailOID =a.OID " +
                    " where a.OptionOID=? order by a.Name ";
                hSql.NewCommand(strSql);
                hSql.Com.Parameters.AddWithValue("OptionCategoryOID", OptionCategoryOID);
                hSql.Com.Parameters.AddWithValue("ContractTypeOID", ContractTypeOID);
                hSql.Com.Parameters.AddWithValue("OptionOID", OptionOID);
                hSql.ExecuteReader();
                while (hSql.Read())
                {
                    SCOptionDetail item = new SCOptionDetail();
                    item.OID = hSql.Reader.GetInt32(0);
                    item.Name = hSql.Reader.GetString(1);
                    item.ItemNo = hSql.Reader.GetString(2);
                    item.ItemSuplNo = hSql.Reader.GetString(3);
                    item.WrksId = hSql.Reader.GetString(4);
                    item.BaseSelPr = hSql.Reader.GetDecimal(5);
                    item.ItemName = hSql.Reader.GetString(7);
                    item.BuyPr = hSql.Reader.GetDecimal(8);
                    item.WrksName = hSql.Reader.GetString(9);
                    item.isAvailable = hSql.Reader.GetInt32(10);
                    item.Info = hSql.Reader.GetString(11);
                    Result.Add(item);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                hSql.Close();
            }
            return Result;
        }
        public static List<SCOptionDetail> getOptionDetailList(int OptionOID)
        {
            List<SCOptionDetail> Result = new List<SCOptionDetail>();
            clsSqlFactory hSql = new clsSqlFactory();
            try
            {

                String strSql = " select a.OID,a.Name,isnull(a.ItemNo,''),isnull(a.ItemSuplNo,''),isnull(a.WrksId,''),isnull(a.SelPr,0),null,isnull(b.NAME,''),isnull(b.BUYPR,0),isnull(c.NAME,'') " +
                    " from ZSC_OptionDetail a left join ITEM b on a.ITEMNO=b.ITEMNO and a.ITEMSUPLNO=b.SUPLNO left join WRKS c on a.WRKSID=c.WRKSID and c.WPTYPE='T' where a.OptionOID=? order by a.Name ";
                hSql.NewCommand(strSql);
                hSql.Com.Parameters.AddWithValue("OptionOID", OptionOID);
                hSql.ExecuteReader();
                while (hSql.Read())
                {
                    SCOptionDetail item = new SCOptionDetail();
                    item.OID = hSql.Reader.GetInt32(0);
                    item.Name = hSql.Reader.GetString(1);
                    item.ItemNo = hSql.Reader.GetString(2);
                    item.ItemSuplNo = hSql.Reader.GetString(3);
                    item.WrksId = hSql.Reader.GetString(4);
                    item.BaseSelPr = hSql.Reader.GetDecimal(5);
                    item.ItemName = hSql.Reader.GetString(7);
                    item.BuyPr = hSql.Reader.GetDecimal(8);
                    item.WrksName = hSql.Reader.GetString(9);
                    Result.Add(item);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                hSql.Close();
            }
            return Result;
        }
    }
    public class SCOptionBase
    {
        public int OID;
        public string Name="";
        public string ItemNo="";
        public string ItemSuplNo="";
        public string ItemName="";
        public string WrksId="";
        public string WrksName="";
        public decimal BaseSelPr=0;
        public decimal BuyPr=0;
        public decimal SelPr=0;
        public string Info="";
        public int Quantity=0;
        public int isAvailable=0;
        public bool isMarkDeleted = false;
        protected static readonly ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    }
    public class SCContractType
    {
        public int OID;
        public string Name;
        public bool isInvoice;
        public bool isActive;
        public bool isCollective;
        public bool isMarkDeleted = false;
    }
    public class ContractStatus
    {
        public const string Model = "M";
        public const string Offer = "O";
        public const string New = "N";
        public const string Waiting = "W";
        public const string Active = "A";
        public const string OnControl = "H";
        public const string Closed = "C";
        public const string Deactivated = "D";
        public const string ModelText = "Model";
        public const string OfferText = "Offer";
        public const string NewText = "New";
        public const string WaitingText = "Waiting";
        public const string ActiveText = "Active";
        public const string OnControlText = "On control";
        public const string ClosedText = "Closed";
        public const string DeactivatedText = "Deactivated";

    }
    public class SCBase
    {
        private static List<clsBaseListItem> Sites = new List<clsBaseListItem>();
        private static List<SCContractType> ContractTypes = new List<SCContractType>();
        private static bool isInited = false;

        #region publicListSaver
        public bool saveContractTypes(List<SCContractType> listContractTypes)
        {
            bool bRet = true;
            clsSqlFactory hSql = new clsSqlFactory();
            try
            {
                foreach (SCContractType objContractType in listContractTypes)
                {
                    if (objContractType.OID > 0)
                    {
                        if (objContractType.isMarkDeleted == true)
                        {
                            //delete
                            bRet = hSql.NewCommand("delete from ZSC_ContractType where OID=?");
                            hSql.Com.Parameters.AddWithValue("OID", objContractType.OID);
                        }
                        else
                        {
                            //update
                            bRet = hSql.NewCommand("update ZSC_ContractType set Name=?,IsInvoice=?,IsActive=?,IsCollective=?,Modified=getdate() where OID=?");
                            hSql.Com.Parameters.AddWithValue("Name", objContractType.Name);
                            hSql.Com.Parameters.AddWithValue("IsInvoice", objContractType.isInvoice);
                            hSql.Com.Parameters.AddWithValue("IsActive", objContractType.isActive);
                            hSql.Com.Parameters.AddWithValue("IsCollective", objContractType.isCollective);
                            hSql.Com.Parameters.AddWithValue("OID", objContractType.OID);

                        }
                        bRet = bRet && hSql.ExecuteNonQuery();
                    }
                    else
                    {
                        //new
                        bRet = hSql.NewCommand("insert into ZSC_ContractType(Name,IsInvoice,IsActive,IsCollective,Created,Modified) values(?,?,?,?,getdate(),getdate())");
                        hSql.Com.Parameters.AddWithValue("Name", objContractType.Name);
                        hSql.Com.Parameters.AddWithValue("IsInvoice", objContractType.isInvoice);
                        hSql.Com.Parameters.AddWithValue("IsActive", objContractType.isActive);
                        hSql.Com.Parameters.AddWithValue("IsCollective", objContractType.isCollective);
                        bRet = bRet && hSql.ExecuteNonQuery();
                    }
                }
                
                hSql.Commit();
                loadContractTypes(hSql);
            }

            catch (Exception ex)
            {
                bRet = false;
                hSql.Rollback();
                throw ex;
            }
            finally
            {
                hSql.Close();
            }
            return bRet;
        }
        #endregion publicListSaver

        #region publicListGeter
        public List<clsBaseListItem> getAMSites()
        {
            return Sites;
        }
        public List<SCContractType> getContractTypes()
        {
            return ContractTypes;
        }

        #endregion publicListGeter
        #region Initializations
        private void loadContractTypes(clsSqlFactory hSql)
        {
            ContractTypes.Clear();
            String strSql = " select a.OID,a.Name,isnull(a.IsInvoice,0),isnull(a.IsActive,0),isnull(a.IsCollective,0) from ZSC_ContractType a  order by a.Name ";
            hSql.NewCommand(strSql);
            hSql.ExecuteReader();
            while (hSql.Read())
            {
                SCContractType item = new SCContractType();
                item.OID = hSql.Reader.GetInt32(0);
                item.Name = hSql.Reader.GetString(1);
                item.isInvoice = hSql.Reader.GetBoolean(2);
                item.isActive = hSql.Reader.GetBoolean(3);
                item.isCollective = hSql.Reader.GetBoolean(4);
                ContractTypes.Add(item);
            }
        }
        private void init()
        {
            clsSqlFactory hSql = new clsSqlFactory();
            try
            {
                //sites
                Sites.Clear();
                String strSql = " select a.UNITID,a.EXPL from UNIT a where ACTIVE = 1 ";
                hSql.NewCommand(strSql);
                hSql.ExecuteReader();
                while (hSql.Read())
                {
                    clsBaseListItem item = new clsBaseListItem();
                    item.strValue1 = hSql.Reader.GetString(0);
                    item.strText = hSql.Reader.GetString(1);
                    Sites.Add(item);
                }
                //contract types
                loadContractTypes(hSql);
            }

            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                hSql.Close();
                isInited = true;
            }
        }
        #endregion Initializations
        #region Constructor
        public SCBase()
        {
            if (!isInited)
            {
                init();
            }
        }
        #endregion Constructor
    }
}
