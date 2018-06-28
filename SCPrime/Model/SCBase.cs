using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using nsBaseClass;
using System.Configuration;
using SCPrime.Utils;
using System.Text.RegularExpressions;

namespace SCPrime.Model
{
    public class SCOptionCategory : SCOptionBase
    {
        public int InvoiceFlag { get; set; }
        public List<SCOption> Options = new List<SCOption>();

        public static List<SCOptionCategory> getContractOptionCategoryPriceList(int ContractTypeOID)
        {
            
            //get list from master
            List<SCOptionCategory> Result = new List<SCOptionCategory>();
            clsSqlFactory hSql = new clsSqlFactory();
            try
            {

                String strSql = " select a.OID,a.Name,isnull(a.ItemNo,''),isnull(a.ItemSuplNo,''),isnull(a.WrksId,''),isnull(a.SelPr,0),isnull(a.InvoiceFlag,0),isnull(b.NAME,''),isnull(b.BUYPR,0),isnull(c.NAME,'') " +
                    ", isnull(d.IsAvailable,0), isnull(d.Info,'') from ZSC_OptionCategory a left join ITEM b on a.ITEMNO=b.ITEMNO and a.ITEMSUPLNO=b.SUPLNO left join WRKS c on a.WRKSID=c.WRKSID and c.WPTYPE='T' " +
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
                    item.SelPr = hSql.Reader.GetDecimal(5);
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
                        if (objCategory.isMarkDeleted == false) //longdq Case: New Object => Delete Object => Save object
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
                    item.SelPr = hSql.Reader.GetDecimal(5);
                    item.ItemName = hSql.Reader.GetString(7);
                    item.BuyPr = hSql.Reader.GetDecimal(8);
                    item.WrksName = hSql.Reader.GetString(9);
                    //item.BaseSelPr = hSql.Reader.GetDecimal(5);
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

        public SCOption()
        {

        }

        public SCOption(int oid, String name)
        {
            this.OID = oid;
            this.Name = name;
        }
    }
    public class SCOptionDetail : SCOptionBase
    {
        public SCOptionDetail()
        {

        }

        public SCOptionDetail(int oid, String name)
        {
            this.OID = oid;
            this.Name = name;
        }

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
                    item.SelPr = hSql.Reader.GetDecimal(5);
                    item.ItemName = hSql.Reader.GetString(7);
                    item.BuyPr = hSql.Reader.GetDecimal(8);
                    item.WrksName = hSql.Reader.GetString(9);
                    //item.BaseSelPr = hSql.Reader.GetDecimal(5);
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
        public int OID { get; set; }
        public string Name { get; set; } = "";
        public string ItemNo { get; set; } = "";
        public string ItemSuplNo { get; set; } = "";
        public string ItemName { get; set; } = "";
        public string WrksId { get; set; } = "";
        public string WrksName { get; set; } = "";
        public decimal BaseSelPr { get; set; } = 0;
        public decimal BuyPr { get; set; } = 0;
        public decimal SelPr { get; set; } = 0;
        public string Info { get; set; } = "";
        public int Quantity { get; set; } = 0;
        public int isAvailable { get; set; } = 0;
        public bool isMarkDeleted { get; set; } = false;
        protected static readonly ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public SCOptionBase()
        {

        }

        public SCOptionBase(int oid, String name)
        {
            this.OID = oid;
            this.Name = name;
        }

    }
    public class SCContractType
    {
        public int OID { get; set; }
        public string Name { get; set; }
        public bool isInvoice { get; set; }
        public bool isActive { get; set; }
        public bool isCollective { get; set; }
        public bool isMarkDeleted { get; set; }

        public SCContractType()
        {

        }

        public SCContractType(int oid)
        {
            this.OID = oid;
            this.Name = "Name";
            this.isInvoice = true;
            this.isActive = true;
            this.isCollective = true;
            this.isMarkDeleted = false;
        }

        public List<SCOptionPrice> listOptionPrice = new List<SCOptionPrice>();
        public List<SCOptionPrice> getOptionPriceList(int contactTypeId)
        {
            List<SCOptionPrice> Result = new List<SCOptionPrice>();
            clsSqlFactory hSql = new clsSqlFactory();
            try
            {

                String strSql = " SELECT a.OID,a.ContractTypeOID,a.OptionCategoryOID,isnull(a.OptionOID,0),isnull(a.OptionDetailOID,0),a.IsAvailable,isnull(a.Info,''),a.Created,a.Modified,isnull(c.Name,''),isnull(o.Name,''),isnull(d.Name,'') " +
                    "FROM ZSC_OptionPriceList  a " +
                    "LEFT JOIN ZSC_OptionCategory c ON a.OptionCategoryOID = c.OID " +
                    "LEFT JOIN ZSC_Option o ON a.OptionOID = o.OID " +
                    "LEFT JOIN ZSC_OptionDetail d ON a.OptionDetailOID = d.OID " +
                    "WHERE a.ContractTypeOID=? order by a.OID DESC ";
                hSql.NewCommand(strSql);
                hSql.Com.Parameters.AddWithValue("ContractTypeOID", contactTypeId);
                hSql.ExecuteReader();
                while (hSql.Read())
                {
                    SCOptionPrice item = new SCOptionPrice();
                    item.OID = hSql.Reader.GetInt32(0);
                    item.ContractTypeOID = hSql.Reader.GetInt32(1);
                    item.OptionCategoryOID = hSql.Reader.GetInt32(2);
                    item.OptionOID = hSql.Reader.GetInt32(3);
                    item.OptionDetailOID = hSql.Reader.GetInt32(4);
                    item.IsAvailable = hSql.Reader.GetInt32(5);
                    item.Info = hSql.Reader.GetString(6);
                    item.Created = hSql.Reader.GetDateTime(7);
                    item.Modified = hSql.Reader.GetDateTime(8);
                    item.CategoryName = hSql.Reader.GetString(9);
                    item.OptionName = hSql.Reader.GetString(10);
                    item.OptionDetailName = hSql.Reader.GetString(11);
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

    public class SCOptionPrice
    {
        public int OID { get; set; }
        public int ContractTypeOID { get; set; }
        public int OptionCategoryOID { get; set; }
        public int OptionOID { get; set; }
        public int OptionDetailOID { get; set; }
        public int IsAvailable { get; set; }
        public String Info { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }

        public bool Include
        {
            get
            {
                return IsAvailable == 2 || IsAvailable == 3;
            }
            set
            {
                IsAvailable = 2;
            }
        }
        public bool Optional
        {
            get
            {
                return IsAvailable == 1;
            }
            set
            {
                IsAvailable = 1;
            }
        }

        public bool NotAvailable
        {
            get
            {
                return IsAvailable == 0;
            }
            set
            {
                IsAvailable = 0;
            }
        }

        public bool Exclude
        {
            get
            {
                return IsAvailable == 3;
            }
            set
            {
                IsAvailable = 3;
            }
        }


        //Relaship
        public String CategoryName { get; set; }
        public String OptionName { get; set; }
        public String OptionDetailName { get; set; }

        public SCOptionPrice()
        {

        }

        public bool save(List<SCOptionPrice> listItem)
        {
            bool bRet = true;
            clsSqlFactory hSql = new clsSqlFactory();
            try
            {
                foreach (SCOptionPrice obj in listItem)
                {
                    if (obj.OID > 0)
                    {
                        //if (obj.isMarkDeleted == true)
                        //{
                        //    //delete
                        //    bRet = hSql.NewCommand("delete from ZSC_OptionPriceList where OID=?");
                        //    hSql.Com.Parameters.AddWithValue("OID", objContractType.OID);
                        //}
                        //else
                        //{
                        //update
                        bRet = hSql.NewCommand("update ZSC_OptionPriceList set IsAvailable=?,Modified=getdate() where OID=?");
                        hSql.Com.Parameters.AddWithValue("IsAvailable", obj.IsAvailable);
                        hSql.Com.Parameters.AddWithValue("OID", obj.OID);

                        //}
                        bRet = bRet && hSql.ExecuteNonQuery();
                    }
                    else
                    {
                        //new
                        bRet = hSql.NewCommand("insert into ZSC_OptionPriceList(ContractTypeOID,OptionCategoryOID,OptionOID,OptionDetailOID,IsAvailable,Info,CreatedModified) values(?,?,?,?,?,?,getdate(),getdate())");
                        hSql.Com.Parameters.AddWithValue("ContractTypeOID", obj.ContractTypeOID);
                        hSql.Com.Parameters.AddWithValue("OptionCategoryOID", obj.OptionCategoryOID);
                        hSql.Com.Parameters.AddWithValue("OptionOID", obj.OptionOID);
                        hSql.Com.Parameters.AddWithValue("OptionDetailOID", obj.OptionDetailOID);
                        hSql.Com.Parameters.AddWithValue("IsAvailable", obj.IsAvailable);
                        hSql.Com.Parameters.AddWithValue("Info", obj.Info);
                        bRet = bRet && hSql.ExecuteNonQuery();
                    }
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


        public List<SCContractType> getContractTypeActive()
        {
            if (ContractTypes.Count > 0)
                return ContractTypes.Where(x => x.isActive).ToList();
            return ContractTypes;
        }

        public static List<Contract> searchContracts(List<SCContractType> contractTypes, List<String> sites, List<String> statuses, string namephrase)
        {
            List<Contract> Result = new List<Contract>();
            String strSqlWhere = "";
            clsSqlFactory hSql = new clsSqlFactory();
            try
            {

                if (statuses.Count > 0)
                {
                    strSqlWhere += " and a.ContractStatus in ('" + statuses[0] + "'";
                    for (int i = 1; i < statuses.Count; i++)
                    {
                        strSqlWhere += ",'" + statuses[i] + "'";
                    }
                    strSqlWhere += ")";
                }
                if (sites.Count > 0)
                {
                    strSqlWhere += " and a.SiteId in ('" + sites[0] + "'";
                    for (int i = 1; i < sites.Count; i++)
                    {
                        strSqlWhere += ",'" + sites[i] + "'";
                    }
                    strSqlWhere += ")";
                }
                if (contractTypes.Count > 0)
                {
                    strSqlWhere += " and a.ContractTypeOID in (" + contractTypes[0].OID.ToString() + "";
                    for (int i = 1; i < contractTypes.Count; i++)
                    {
                        strSqlWhere += "," + contractTypes[i].OID.ToString() + "";
                    }
                    strSqlWhere += ")";
                }

                String strSql = "select ";
                strSql += "a.CapitalMonthAmount, a.CapitalStartAmount, a.CapitalStartPayer, a.CareSmanId, a.ContractCustId, a.ContractEndDate, a.ContractEndHour, a.ContractEndKm, " +
                    "a.ContractNo, a.ContractPeriodHour, a.ContractPeriodKm, a.ContractPeriodKmHour, a.ContractPeriodMonth, a.ContractStartDate, a.ContractStartHour, " +
                    "a.ContractStartKm, a.ContractStatus, a.ContractTypeOID, a.CostBasedOnService, a.CostBasis, a.CostCenter, a.CostKmBasis, a.CostMonthBasis, a.CostPerKm, " +
                    "a.Created, a.ExtContractNo, a.ExtraKmAccounting, a.ExtraKmHighAmount, a.ExtraKmInvoicedAmount, a.ExtraKmInvoicePeriod, a.ExtraKmLowAmount, a.ExtraKmMaxDeviation, " +
                    "a.InvocieStartDate, a.InvoiceCustId, a.InvoiceEndDate, a.InvoiceSiteId, a.IsBodyIncl, a.IsCoolingIncl, a.IsCraneIncl, a.IsInvoiceDetail, a.IsManualInvoice, " +
                    "a.IsTailLiftIncl, a.LastInvoiceDate, a.Modified, a.NextInvoiceDate, a.OID, a.PaymentCollectionType, a.PaymentGroupingLevel, a.PaymentIsInBlock, " +
                    "a.PaymentNextBlockEnd, a.PaymentNextBlockStart, a.PaymentPeriod, a.PaymentTerm, a.RespSmanId, a.RiskCustId, a.RiskLevel, a.RollingCode, a.SiteId, " +
                    "a.TerminationType, a.ValidWorkshopCode, a.VehiId, a.VersionNo, ";
                strSql += "s1.EXPL,";
                strSql += "c1.LNAME";
                strSql += " from ZSC_Contract a inner join UNIT s1 on a.SiteId = s1.UNITID ";
                strSql += " inner join CUST c1 on a.ContractCustId = c1.CUSTID ";
                if (strSqlWhere != "")
                {
                    strSql += " where " + strSqlWhere;
                }
                hSql.NewCommand(strSql);
                hSql.ExecuteReader();
                while (hSql.Read())
                {
                    Contract item = new Contract();
                    int colId;
                    item.ContractCapitalData = new ContractCapital();

                    colId = hSql.Reader.GetOrdinal("CapitalMonthAmount");
                    if (!hSql.Reader.IsDBNull(colId)) item.ContractCapitalData.CapitalMonthAmount = hSql.Reader.GetDecimal(colId);
                    colId = hSql.Reader.GetOrdinal("CapitalStartAmount");
                    if (!hSql.Reader.IsDBNull(colId)) item.ContractCapitalData.CapitalStartAmount = hSql.Reader.GetDecimal(colId);
                    colId = hSql.Reader.GetOrdinal("CapitalStartPayer");
                    if (!hSql.Reader.IsDBNull(colId)) item.ContractCapitalData.CapitalStartPayer.strValue1 = hSql.Reader.GetString(colId);

                    colId = hSql.Reader.GetOrdinal("OID");
                    item.ContractOID = hSql.Reader.GetInt32(colId);
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

    public static class ContractStatusString
    {
        public const string ModelFull = "M-Model";
        public const string OfferFull = "O-Offer";
        public const string NewFull = "N-New";
        public const string WaitingFull = "W-Waiting";
        public const string ActiveFull = "A-Active";
        public const string OnControlFull = "H-On control";
        public const string ClosedFull = "C-Closed";
        public const string DeactivatedFull = "D-Deactivated";
    }


    public class ObjTmp
    {
        public int id { get; set; }
        public String text { get; set; }
        public String value { get; set; }
        public ObjTmp()
        {
        }
        public ObjTmp(String value, String text)
        {
            this.text = text;
            this.value = value;
        }
        public ObjTmp(int id, String text)
        {
            this.text = text;
            this.value = value;
        }
    }

    public class SCViewWorks
    {
        public int _OID { get; set; }
        public string LabourCode { get; set; }
        public string Name { get; set; }
        public string SearchKey { get; set; }
        public int SuplNo { get; set; }
        public static List<SCViewWorks> seach(string namephrase)
        {
            string strSql = "select top maxresult a._OID as _OID, a.WRKSID as LabourCode, a.NAME as LabourName, a.SKEY,a.SUPLNO from WRKS a  where a.WPTYPE = 'T' ";
            var tmp = MyUtils.GetMaxResult();
            if (tmp > 0)
                strSql = Regex.Replace(strSql, "maxresult", tmp.ToString());
            else
                strSql = Regex.Replace(strSql, "maxresult", "0");
            if (namephrase != "")
            {
                String strFTSQL = Utils.MyUtils.getFTSearchSQL(namephrase, "ASVIEW_WRKS");
                if (strFTSQL != "")
                {
                    strSql += " and exists (" + strFTSQL + " and v._OID=a._OID)";
                }

            }


            List<SCViewWorks> Result = new List<SCViewWorks>();
            clsSqlFactory hSql = new clsSqlFactory();
            try
            {


                hSql.NewCommand(strSql);
                hSql.ExecuteReader();
                while (hSql.Read())
                {
                    SCViewWorks item = new SCViewWorks();

                    item._OID = hSql.Reader.GetInt32(0);
                    item.LabourCode = hSql.Reader.GetString(1);
                    item.Name = hSql.Reader.GetString(2);
                    item.SearchKey = hSql.Reader.GetString(3);
                    item.SuplNo = hSql.Reader.GetInt32(4);
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

    public class SCViewItems
    {
        public int _OID { get; set; }
        public string PartNr { get; set; }
        public string Name { get; set; }
        public string Supplier { get; set; }
        public string SearchKey { get; set; }
        public float SalesPr { get; set; }
        public float PurchasePr { get; set; }

        public static List<SCViewItems> seach(string namephrase)
        {

            string strSql = "select top maxresult a._OID as _OID, isnull(a.ITEMNO,'') as PartNr, a.NAME as Name, isnull(a.SUPLNO,'') as Supplier, isnull(a.SKEY,'') as SearchKey, isnull(a.SELPR,0) as SalesPr, isnull(a.BUYPR,0) as PurchasePr  from ITEM a  where 1=1 ";
            
            var tmp = MyUtils.GetMaxResult();
            if (tmp > 0)
                strSql = Regex.Replace(strSql, "maxresult",tmp.ToString() );
            else
                strSql = Regex.Replace(strSql, "maxresult","0");


            if (namephrase != "")
            {
                String strFTSQL = Utils.MyUtils.getFTSearchSQL(namephrase, "ASVIEW_ITEM");
                if (strFTSQL != "")
                {
                    strSql += " and exists (" + strFTSQL + " and v._OID=a._OID)";
                }

            }

            List<SCViewItems> Result = new List<SCViewItems>();
            clsSqlFactory hSql = new clsSqlFactory();
            try
            {


                hSql.NewCommand(strSql);
                hSql.ExecuteReader();
                while (hSql.Read())
                {
                    SCViewItems item = new SCViewItems();
                    item._OID = hSql.Reader.GetInt32(0);
                    item.PartNr = hSql.Reader.GetString(1);
                    item.Name = hSql.Reader.GetString(2);
                    item.Supplier = hSql.Reader.GetString(3);
                    item.SearchKey = hSql.Reader.GetString(4);
                    item.SalesPr = hSql.Reader.GetFloat(5);
                    item.PurchasePr = hSql.Reader.GetFloat(6);
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
}
