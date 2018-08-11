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
        public string MainGroupCode { get; set; }
        public List<SCOption> Options = new List<SCOption>();
        public static List<SCOptionCategory> getContractOptionCategory(int ContractOID, clsSqlFactory hSql)
        {

            //get list from master
            List<SCOptionCategory> Result = new List<SCOptionCategory>();
            try
            {
                clsGlobalVariable objGlobal = new clsGlobalVariable();
                string LangId = objGlobal.CultureInfo;

                String strSql = " select a.OID,isnull(x.Name,a.Name),isnull(a.ItemNo,''),isnull(a.ItemSuplNo,''),isnull(a.WrksId,''),isnull(a.SelPr,0) as ListPrice,isnull(a.InvoiceFlag,0),isnull(b.NAME,''),isnull(b.BUYPR,0),isnull(c.NAME,'') " +
                    ", isnull(d.Info,''), isnull(d.SelPr,0) as SalesPrice,isnull(d.Quantity,0),isnull(d.PartialPayer,''), isnull(a.MainGroupCode,'') from ZSC_OptionCategory a left join ITEM b on a.ITEMNO=b.ITEMNO and a.ITEMSUPLNO=b.SUPLNO left join WRKS c on a.WRKSID=c.WRKSID and c.WPTYPE='T' " +
                   " left join ZSC_OptionForeignName x on x.ObjectType=1 and x.ObjectOID=a.OID and x.LangId=? " + 
                    " inner join ZSC_ContractOption d on a.OID = d.OptionCategoryOID and d.ContractOID =? and d.OptionOID is null and d.OptionDetailOID is null " +
                    " order by isnull(x.Name,a.Name)  ";
                hSql.NewCommand(strSql);
                hSql.Com.Parameters.AddWithValue("LangId", LangId);
                hSql.Com.Parameters.AddWithValue("ContractOID", ContractOID);
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
                    item.Options = SCOption.getContractOptionList(ContractOID,item.OID);
                    item.Info = hSql.Reader.GetString(10);
                    item.SelPr = hSql.Reader.GetDecimal(11);
                    item.Quantity = hSql.Reader.GetInt32(12);
                    item.PartialPayer = hSql.Reader.GetString(13);
                    item.MainGroupCode = hSql.Reader.GetString(14);
                    Result.Add(item);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return Result;
        }

        public static List<SCOptionCategory> getContractOptionCategoryPriceList(int ContractTypeOID)
        {

            //get list from master
            List<SCOptionCategory> Result = new List<SCOptionCategory>();
            clsSqlFactory hSql = new clsSqlFactory();
            try
            {

                String strSql = " select a.OID,a.Name,isnull(a.ItemNo,''),isnull(a.ItemSuplNo,''),isnull(a.WrksId,''),isnull(a.SelPr,0),isnull(a.InvoiceFlag,0),isnull(b.NAME,''),isnull(b.BUYPR,0),isnull(c.NAME,'') " +
                    ", isnull(d.IsAvailable,-1), isnull(d.Info,'') from ZSC_OptionCategory a left join ITEM b on a.ITEMNO=b.ITEMNO and a.ITEMSUPLNO=b.SUPLNO left join WRKS c on a.WRKSID=c.WRKSID and c.WPTYPE='T' " +
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
                clsGlobalVariable objGlobal = new clsGlobalVariable();
                string LangId = objGlobal.CultureInfo;

                String strSql = " select a.OID,isnull(x.Name,a.Name),isnull(a.ItemNo,''),isnull(a.ItemSuplNo,''),isnull(a.WrksId,''),isnull(a.SelPr,0),isnull(a.InvoiceFlag,0),isnull(b.NAME,''),isnull(b.BUYPR,0),isnull(c.NAME,''), isnull(a.MainGroupCode,'') " +
                    " from ZSC_OptionCategory a left join ITEM b on a.ITEMNO=b.ITEMNO and a.ITEMSUPLNO=b.SUPLNO left join WRKS c on a.WRKSID=c.WRKSID and c.WPTYPE='T' " +
                    " left join ZSC_OptionForeignName x on x.ObjectType=1 and x.ObjectOID=a.OID and x.LangId=? " +
                    " order by a.OID ";
                hSql.NewCommand(strSql);
                hSql.Com.Parameters.AddWithValue("LangId", LangId);
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
                    item.MainGroupCode = hSql.Reader.GetString(10);
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
                clsGlobalVariable objGlobal = new clsGlobalVariable();
                string LangId = objGlobal.CultureInfo;
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

                            bRet = hSql.NewCommand("delete from ZSC_OptionForeignName where ObjectType=3 and ObjectOID in (select OID from ZSC_OptionDetail where OptionOID=?) ");
                            hSql.Com.Parameters.AddWithValue("OID", objOption.OID);
                            bRet = bRet && hSql.ExecuteNonQuery();

                            bRet = hSql.NewCommand("delete from ZSC_OptionDetail where OptionOID =? ");
                            hSql.Com.Parameters.AddWithValue("OID", objOption.OID);
                            bRet = bRet && hSql.ExecuteNonQuery();

                            bRet = hSql.NewCommand("delete from ZSC_OptionForeignName where ObjectType=2 and ObjectOID = ? ");
                            hSql.Com.Parameters.AddWithValue("OID", objOption.OID);
                            bRet = bRet && hSql.ExecuteNonQuery();

                            bRet = bRet && hSql.ExecuteNonQuery();
                            bRet = hSql.NewCommand("delete from ZSC_Option where OID=? ");
                            hSql.Com.Parameters.AddWithValue("OID", objOption.OID);
                            bRet = bRet && hSql.ExecuteNonQuery();
                        }
                        else
                        {
                            //update
                            bRet = hSql.NewCommand("update ZSC_Option set Name=?,ItemNo=?,ItemSuplNo=?,WrksId=?,SelPr=?,Modified=getdate(), SubGroupCode=? where OID=?");
                            hSql.Com.Parameters.AddWithValue("Name", objOption.Name);
                            hSql.Com.Parameters.AddWithValue("ItemNo", objOption.ItemNo);
                            hSql.Com.Parameters.AddWithValue("ItemSuplNo", objOption.ItemSuplNo);
                            hSql.Com.Parameters.AddWithValue("WrksId", objOption.WrksId);
                            hSql.Com.Parameters.AddWithValue("SelPr", objOption.SelPr);
                            hSql.Com.Parameters.AddWithValue("SubGroupCode", objOption.SubGroupCode);
                            hSql.Com.Parameters.AddWithValue("OID", objOption.OID);
                            bRet = bRet && hSql.ExecuteNonQuery();

                            bRet = hSql.NewCommand("select 1 from ZSC_OptionForeignName where ObjectType=2 and ObjectOID =? and LangId = ? ");
                            hSql.Com.Parameters.AddWithValue("OID", objOption.OID);
                            hSql.Com.Parameters.AddWithValue("LangId", LangId);
                            hSql.ExecuteReader();
                            if (hSql.Read())
                            {
                                bRet = hSql.NewCommand("update ZSC_OptionForeignName set Name = ? where ObjectType=2 and ObjectOID =? and LangId = ? ");
                                hSql.Com.Parameters.AddWithValue("Name", objOption.Name);
                                hSql.Com.Parameters.AddWithValue("OID", objOption.OID);
                                hSql.Com.Parameters.AddWithValue("LangId", LangId);
                                bRet = bRet && hSql.ExecuteNonQuery();

                            }
                            else
                            {
                                bRet = hSql.NewCommand("insert into ZSC_OptionForeignName(ObjectType,ObjectOID,LangId,Name) values(2,?,?,?) ");
                                hSql.Com.Parameters.AddWithValue("OID", objOption.OID);
                                hSql.Com.Parameters.AddWithValue("LangId", LangId);
                                hSql.Com.Parameters.AddWithValue("Name", objOption.Name);
                                bRet = bRet && hSql.ExecuteNonQuery();
                            }
                        }
                    }
                    else
                    {
                        //insert
                        bRet = hSql.NewCommand("insert into ZSC_Option (Name,ItemNo,ItemSuplNo,WrksId,SelPr,Modified,Created,OptionCategoryOID,SubGroupCode) values (?,?,?,?,?,getdate(),getdate(),?,?) ");
                        hSql.Com.Parameters.AddWithValue("Name", objOption.Name);
                        hSql.Com.Parameters.AddWithValue("ItemNo", objOption.ItemNo);
                        hSql.Com.Parameters.AddWithValue("ItemSuplNo", objOption.ItemSuplNo);
                        hSql.Com.Parameters.AddWithValue("WrksId", objOption.WrksId);
                        hSql.Com.Parameters.AddWithValue("SelPr", objOption.SelPr);
                        hSql.Com.Parameters.AddWithValue("OptionCategoryOID", this.OID);
                        hSql.Com.Parameters.AddWithValue("SubGroupCode", objOption.SubGroupCode);
                        bRet = bRet && hSql.ExecuteNonQuery();
                        bRet = hSql.NewCommand("select max(OID) from  ZSC_Option ");
                        bRet = bRet && hSql.ExecuteReader() && hSql.Read();
                        objOption.OID = hSql.Reader.GetInt32(0);

                        bRet = hSql.NewCommand("insert into ZSC_OptionForeignName(ObjectType,ObjectOID,LangId,Name) values(2,?,?,?) ");
                        hSql.Com.Parameters.AddWithValue("OID", objOption.OID);
                        hSql.Com.Parameters.AddWithValue("LangId", LangId);
                        hSql.Com.Parameters.AddWithValue("Name", objOption.Name);
                        bRet = bRet && hSql.ExecuteNonQuery();
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
        public bool saveContractOptions(int ContractOID, clsSqlFactory hSql)
        {
            bool bRet = true;
            try
            {            
                foreach (SCOption objOption in Options)
                {
                    if ((objOption.OID > 0)&& (objOption.isMarkDeleted == false))
                    {
                        bRet = hSql.NewCommand("insert into ZSC_ContractOption(ContractOID,OptionCategoryOID,OptionOID, SelPr,Quantity,Info,Created,Modified,PartialPayer) values(?,?,?,?,?,?,getdate(),getdate(),?) ");
                        hSql.Com.Parameters.AddWithValue("ContractOID", ContractOID);
                        hSql.Com.Parameters.AddWithValue("OptionCategoryOID", this.OID);
                        hSql.Com.Parameters.AddWithValue("OptionOID", objOption.OID);
                        hSql.Com.Parameters.AddWithValue("SelPr", objOption.SelPr);
                        hSql.Com.Parameters.AddWithValue("Quantity", objOption.Quantity);
                        hSql.Com.Parameters.AddWithValue("Info", objOption.Info);
                        hSql.Com.Parameters.AddWithValue("PartialPayer", objOption.PartialPayer);
                        bRet = bRet && hSql.ExecuteNonQuery();
                        bRet = bRet && objOption.saveContractOptionDetails(ContractOID, hSql,this.OID);
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

        public static bool saveOptionCategoryList(List<SCOptionCategory> listCategories)
        {
            bool bRet = true;
            clsSqlFactory hSql = new clsSqlFactory();
            clsGlobalVariable objGlobal = new clsGlobalVariable();
            string LangId = objGlobal.CultureInfo;
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

                            bRet = hSql.NewCommand("delete from ZSC_OptionForeignName where ObjectType=3 and ObjectOID in (select OID from ZSC_OptionDetail where OptionOID in (select OID from ZSC_Option where OptionCategoryOID=?)) ");
                            hSql.Com.Parameters.AddWithValue("OID", objCategory.OID);
                            bRet = bRet && hSql.ExecuteNonQuery();

                            bRet = hSql.NewCommand("delete from ZSC_OptionDetail where OptionOID in (select OID from ZSC_Option where OptionCategoryOID=?) ");
                            hSql.Com.Parameters.AddWithValue("OID", objCategory.OID);
                            bRet = bRet && hSql.ExecuteNonQuery();

                            bRet = hSql.NewCommand("delete from ZSC_OptionForeignName where ObjectType=2 and ObjectOID in (select OID from ZSC_Option where OptionCategoryOID=?) ");
                            hSql.Com.Parameters.AddWithValue("OID", objCategory.OID);
                            bRet = bRet && hSql.ExecuteNonQuery();

                            bRet = hSql.NewCommand("delete from ZSC_Option where OptionCategoryOID=? ");
                            hSql.Com.Parameters.AddWithValue("OID", objCategory.OID);
                            bRet = bRet && hSql.ExecuteNonQuery();

                            bRet = hSql.NewCommand("delete from ZSC_OptionForeignName where ObjectType=1 and ObjectOID =? ");
                            hSql.Com.Parameters.AddWithValue("OID", objCategory.OID);
                            bRet = bRet && hSql.ExecuteNonQuery();

                            bRet = hSql.NewCommand("delete from ZSC_OptionCategory where OID=? ");
                            hSql.Com.Parameters.AddWithValue("OID", objCategory.OID);
                            bRet = bRet && hSql.ExecuteNonQuery();
                        }
                        else
                        {
                            //update
                            bRet = hSql.NewCommand("update ZSC_OptionCategory set Name=?,ItemNo=?,ItemSuplNo=?,WrksId=?,SelPr=?,InvoiceFlag=?,MainGroupCode = ?, Modified=getdate() where OID=?");
                            hSql.Com.Parameters.AddWithValue("Name", objCategory.Name);
                            hSql.Com.Parameters.AddWithValue("ItemNo", objCategory.ItemNo);
                            hSql.Com.Parameters.AddWithValue("ItemSuplNo", objCategory.ItemSuplNo);
                            hSql.Com.Parameters.AddWithValue("WrksId", objCategory.WrksId);
                            hSql.Com.Parameters.AddWithValue("SelPr", objCategory.SelPr);
                            hSql.Com.Parameters.AddWithValue("InvoiceFlag", objCategory.InvoiceFlag);
                            hSql.Com.Parameters.AddWithValue("MainGroupCode", objCategory.MainGroupCode);
                            hSql.Com.Parameters.AddWithValue("OID", objCategory.OID);
                            bRet = bRet && hSql.ExecuteNonQuery();

                            bRet = hSql.NewCommand("select 1 from ZSC_OptionForeignName where ObjectType=1 and ObjectOID =? and LangId = ? ");
                            hSql.Com.Parameters.AddWithValue("OID", objCategory.OID);
                            hSql.Com.Parameters.AddWithValue("LangId", LangId);
                            hSql.ExecuteReader();
                            if (hSql.Read())
                            {
                                bRet = hSql.NewCommand("update ZSC_OptionForeignName set Name = ? where ObjectType=1 and ObjectOID =? and LangId = ? ");
                                hSql.Com.Parameters.AddWithValue("Name", objCategory.Name);
                                hSql.Com.Parameters.AddWithValue("OID", objCategory.OID);
                                hSql.Com.Parameters.AddWithValue("LangId", LangId);
                                bRet = bRet && hSql.ExecuteNonQuery();

                            }
                            else
                            {
                                bRet = hSql.NewCommand("insert into ZSC_OptionForeignName(ObjectType,ObjectOID,LangId,Name) values(1,?,?,?) ");
                                hSql.Com.Parameters.AddWithValue("OID", objCategory.OID);
                                hSql.Com.Parameters.AddWithValue("LangId", LangId);
                                hSql.Com.Parameters.AddWithValue("Name", objCategory.Name);
                                bRet = bRet && hSql.ExecuteNonQuery();
                            }

                        }
                    }
                    else
                    {
                        if (objCategory.isMarkDeleted == false) //longdq Case: New Object => Delete Object => Save object
                        {
                            //insert
                            bRet = hSql.NewCommand("insert into ZSC_OptionCategory (Name,ItemNo,ItemSuplNo,WrksId,SelPr,InvoiceFlag,Modified,Created,MainGroupCode) values (?,?,?,?,?,?,getdate(),getdate(),?) ");
                            hSql.Com.Parameters.AddWithValue("Name", objCategory.Name);
                            hSql.Com.Parameters.AddWithValue("ItemNo", objCategory.ItemNo);
                            hSql.Com.Parameters.AddWithValue("ItemSuplNo", objCategory.ItemSuplNo);
                            hSql.Com.Parameters.AddWithValue("WrksId", objCategory.WrksId);
                            hSql.Com.Parameters.AddWithValue("SelPr", objCategory.SelPr);
                            hSql.Com.Parameters.AddWithValue("InvoiceFlag", objCategory.InvoiceFlag);
                            hSql.Com.Parameters.AddWithValue("MainGroupCode", objCategory.MainGroupCode);
                            bRet = bRet && hSql.ExecuteNonQuery();
                            bRet = hSql.NewCommand("select max(OID) from  ZSC_OptionCategory ");
                            bRet = bRet && hSql.ExecuteReader() && hSql.Read();
                            objCategory.OID = hSql.Reader.GetInt32(0);
                            bRet = hSql.NewCommand("insert into ZSC_OptionForeignName(ObjectType,ObjectOID,LangId,Name) values(1,?,?,?) ");
                            hSql.Com.Parameters.AddWithValue("OID", objCategory.OID);
                            hSql.Com.Parameters.AddWithValue("LangId", LangId);
                            hSql.Com.Parameters.AddWithValue("Name", objCategory.Name);
                            bRet = bRet && hSql.ExecuteNonQuery();
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
        public static bool saveContractOptionCategoryList(int ContractOID, List<SCOptionCategory> listCategories, clsSqlFactory hSql)
        {
            bool bRet = true;
            try
            {
                //delete
                bRet = hSql.NewCommand("delete from ZSC_ContractOption where ContractOID=?");
                hSql.Com.Parameters.AddWithValue("ContractOID", ContractOID);
                bRet = bRet && hSql.ExecuteNonQuery();
                foreach (SCOptionCategory objCategory in listCategories)
                {
                    if ((objCategory.OID > 0) && (objCategory.isMarkDeleted == false))
                    {
                            bRet = hSql.NewCommand("insert into ZSC_ContractOption(ContractOID,OptionCategoryOID,SelPr,Quantity,Info,Created,Modified,PartialPayer) values(?,?,?,?,?,getdate(),getdate(),?) ");
                            hSql.Com.Parameters.AddWithValue("ContractOID", ContractOID);
                            hSql.Com.Parameters.AddWithValue("OptionCategoryOID", objCategory.OID);
                            hSql.Com.Parameters.AddWithValue("SelPr", objCategory.SelPr);
                            hSql.Com.Parameters.AddWithValue("Quantity", objCategory.Quantity);
                            hSql.Com.Parameters.AddWithValue("Info", objCategory.Info);
                            hSql.Com.Parameters.AddWithValue("PartialPayer", objCategory.PartialPayer);
                            bRet = bRet && hSql.ExecuteNonQuery();
                            bRet = bRet && objCategory.saveContractOptions(ContractOID, hSql);
                        
                    }

                }
           
            }
            catch (Exception ex)
            {
                bRet = false;
                hSql.Rollback();
                throw ex;
            }
            finally
            {
              
            }
            return bRet;
        }

        public static SCOptionCategory getByOID(int CategoryOID)
        {
            SCOptionCategory item = new SCOptionCategory();
            clsSqlFactory hSql = new clsSqlFactory();
            try
            {

                clsGlobalVariable objGlobal = new clsGlobalVariable();
                string LangId = objGlobal.CultureInfo;

                String strSql = " select a.OID,isnull(x.Name,a.Name),isnull(a.ItemNo,''),isnull(a.ItemSuplNo,''),isnull(a.WrksId,''),isnull(a.SelPr,0),isnull(a.InvoiceFlag,0),isnull(b.NAME,''),isnull(b.BUYPR,0),isnull(c.NAME,''), isnull(a.MainGroupCode,'') " +
                    " from ZSC_OptionCategory a left join ITEM b on a.ITEMNO=b.ITEMNO and a.ITEMSUPLNO=b.SUPLNO left join WRKS c on a.WRKSID=c.WRKSID and c.WPTYPE='T' " +
                    " left join ZSC_OptionForeignName x on x.ObjectType=1 and x.ObjectOID=a.OID and x.LangId=? " +
                    " WHERE a.OID=? order by a.OID ";
                hSql.NewCommand(strSql);
                hSql.Com.Parameters.AddWithValue("LangId", LangId);
                hSql.Com.Parameters.AddWithValue("CategoryOID", CategoryOID);
                hSql.ExecuteReader();
                item = new SCOptionCategory();
                while (hSql.Read())
                {
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
                    item.MainGroupCode = hSql.Reader.GetString(10);
                    item.type = "C";
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
            return item;
        }
    }
    public class SCOption : SCOptionBase
    {
        public string SubGroupCode { get; set; }
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
                clsGlobalVariable objGlobal = new clsGlobalVariable();
                string LangId = objGlobal.CultureInfo;
                String strSql = " select a.OID,isnull(x.Name,a.Name),isnull(a.ItemNo,''),isnull(a.ItemSuplNo,''),isnull(a.WrksId,''),isnull(a.SelPr,0),null,isnull(b.NAME,''),isnull(b.BUYPR,0),isnull(c.NAME,''), isnull(a.SubGroupCode,'') " +
                    " from ZSC_Option a left join ITEM b on a.ITEMNO=b.ITEMNO and a.ITEMSUPLNO=b.SUPLNO left join WRKS c on a.WRKSID=c.WRKSID and c.WPTYPE='T' " +
                    " left join ZSC_OptionForeignName x on x.ObjectType=2 and x.ObjectOID=a.OID and x.LangId=? " +
                    " where a.OptionCategoryOID=? order by isnull(x.Name,a.Name) ";
                hSql.NewCommand(strSql);
                hSql.Com.Parameters.AddWithValue("LangId", LangId);
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
                    item.SubGroupCode = hSql.Reader.GetString(10);
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
        public static List<SCOption> getContractOptionList(int ContractOID,int OptionCategoryOID)
        {
            List<SCOption> Result = new List<SCOption>();
            clsSqlFactory hSql = new clsSqlFactory();
            try
            {
                clsGlobalVariable objGlobal = new clsGlobalVariable();
                string LangId = objGlobal.CultureInfo;
                String strSql = " select a.OID,isnull(x.Name,a.Name),isnull(a.ItemNo,''),isnull(a.ItemSuplNo,''),isnull(a.WrksId,''),isnull(a.SelPr,0) as BaseSelPr,null,isnull(b.NAME,''),isnull(b.BUYPR,0),isnull(c.NAME,''), isnull(a.SubGroupCode,'') " +
                    ", isnull(d.Info, ''), isnull(d.SelPr, 0) as SalesPrice,isnull(d.Quantity, 0),isnull(d.PartialPayer, 0) "+
                    " from ZSC_Option a left join ITEM b on a.ITEMNO=b.ITEMNO and a.ITEMSUPLNO=b.SUPLNO left join WRKS c on a.WRKSID=c.WRKSID and c.WPTYPE='T' " +
                    " left join ZSC_OptionForeignName x on x.ObjectType=2 and x.ObjectOID=a.OID and x.LangId=? " +
                    " inner join ZSC_ContractOption d on a.OID = d.OptionOID and d.ContractOID =? and d.OptionCategoryOID=? and d.OptionDetailOID is null" +
                    " order by isnull(x.Name,a.Name) ";
                hSql.NewCommand(strSql);
                hSql.Com.Parameters.AddWithValue("LangId", LangId);
                hSql.Com.Parameters.AddWithValue("ContractOID", ContractOID);
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
                    item.SubGroupCode = hSql.Reader.GetString(10);
                    item.Info = hSql.Reader.GetString(11);
                    item.SelPr = hSql.Reader.GetDecimal(12);
                    item.Quantity = hSql.Reader.GetInt32(13);
                    item.PartialPayer = hSql.Reader.GetString(14);
                    item.OptionDetails = SCOptionDetail.getContractOptionDetailList(item.OID, ContractOID);
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
                clsGlobalVariable objGlobal = new clsGlobalVariable();
                string LangId = objGlobal.CultureInfo;
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

                            bRet = hSql.NewCommand("delete from ZSC_OptionForeignName where ObjectType=3 and ObjectOID=? ");
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

                            bRet = hSql.NewCommand("select 1 from ZSC_OptionForeignName where ObjectType=3 and ObjectOID =? and LangId = ? ");
                            hSql.Com.Parameters.AddWithValue("OID", objOptionDetail.OID);
                            hSql.Com.Parameters.AddWithValue("LangId", LangId);
                            hSql.ExecuteReader();
                            if (hSql.Read())
                            {
                                bRet = hSql.NewCommand("update ZSC_OptionForeignName set Name = ? where ObjectType=3 and ObjectOID =? and LangId = ? ");
                                hSql.Com.Parameters.AddWithValue("Name", objOptionDetail.Name);
                                hSql.Com.Parameters.AddWithValue("OID", objOptionDetail.OID);
                                hSql.Com.Parameters.AddWithValue("LangId", LangId);
                                bRet = bRet && hSql.ExecuteNonQuery();

                            }
                            else
                            {
                                bRet = hSql.NewCommand("insert into ZSC_OptionForeignName(ObjectType,ObjectOID,LangId,Name) values(3,?,?,?) ");
                                hSql.Com.Parameters.AddWithValue("OID", objOptionDetail.OID);
                                hSql.Com.Parameters.AddWithValue("LangId", LangId);
                                hSql.Com.Parameters.AddWithValue("Name", objOptionDetail.Name);
                                bRet = bRet && hSql.ExecuteNonQuery();
                            }
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

                        bRet = hSql.NewCommand("insert into ZSC_OptionForeignName(ObjectType,ObjectOID,LangId,Name) values(3,?,?,?) ");
                        hSql.Com.Parameters.AddWithValue("OID", objOptionDetail.OID);
                        hSql.Com.Parameters.AddWithValue("LangId", LangId);
                        hSql.Com.Parameters.AddWithValue("Name", objOptionDetail.Name);
                        bRet = bRet && hSql.ExecuteNonQuery();
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
        public bool saveContractOptionDetails(int ContractOID, clsSqlFactory hSql,int OptionCategoryOID)
        {
            bool bRet = true;
            try
            {
                foreach (SCOptionDetail objOptionDetail in OptionDetails)
                {
                    if ((objOptionDetail.OID > 0) && (objOptionDetail.isMarkDeleted == false))
                    {
                        bRet = hSql.NewCommand("insert into ZSC_ContractOption(ContractOID,OptionCategoryOID,OptionOID,OptionDetailOID, SelPr,Quantity,Info,Created,Modified,PartialPayer) values(?,?,?,?,?,?,?,getdate(),getdate(),?) ");
                        hSql.Com.Parameters.AddWithValue("ContractOID", ContractOID);
                        hSql.Com.Parameters.AddWithValue("OptionCategoryOID", OptionCategoryOID);
                        hSql.Com.Parameters.AddWithValue("OptionOID", this.OID);
                        hSql.Com.Parameters.AddWithValue("OptionDetailOID", objOptionDetail.OID);
                        hSql.Com.Parameters.AddWithValue("SelPr", objOptionDetail.SelPr);
                        hSql.Com.Parameters.AddWithValue("Quantity", objOptionDetail.Quantity);
                        hSql.Com.Parameters.AddWithValue("Info", objOptionDetail.Info);
                        hSql.Com.Parameters.AddWithValue("PartialPayer", objOptionDetail.PartialPayer);
                        bRet = bRet && hSql.ExecuteNonQuery();
                        
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

        public static SCOption getByOID(int OptionOID)
        {
            SCOption item = null;
            clsSqlFactory hSql = new clsSqlFactory();
            try
            {
                clsGlobalVariable objGlobal = new clsGlobalVariable();
                string LangId = objGlobal.CultureInfo;
                String strSql = " select a.OID,isnull(x.Name,a.Name),isnull(a.ItemNo,''),isnull(a.ItemSuplNo,''),isnull(a.WrksId,''),isnull(a.SelPr,0),null,isnull(b.NAME,''),isnull(b.BUYPR,0),isnull(c.NAME,''), isnull(a.SubGroupCode,'') " +
                    " from ZSC_Option a left join ITEM b on a.ITEMNO=b.ITEMNO and a.ITEMSUPLNO=b.SUPLNO left join WRKS c on a.WRKSID=c.WRKSID and c.WPTYPE='T' " +
                    " left join ZSC_OptionForeignName x on x.ObjectType=2 and x.ObjectOID=a.OID and x.LangId=? " +
                    " where a.OID=? order by isnull(x.Name,a.Name) ";
                hSql.NewCommand(strSql);
                hSql.Com.Parameters.AddWithValue("LangId", LangId);
                hSql.Com.Parameters.AddWithValue("OptionOID", OptionOID);
                hSql.ExecuteReader();
                item = new SCOption();
                while (hSql.Read())
                {   
                    item.OID = hSql.Reader.GetInt32(0);
                    item.Name = hSql.Reader.GetString(1);
                    item.ItemNo = hSql.Reader.GetString(2);
                    item.ItemSuplNo = hSql.Reader.GetString(3);
                    item.WrksId = hSql.Reader.GetString(4);
                    item.SelPr = hSql.Reader.GetDecimal(5);
                    item.ItemName = hSql.Reader.GetString(7);
                    item.BuyPr = hSql.Reader.GetDecimal(8);
                    item.WrksName = hSql.Reader.GetString(9);
                    item.SubGroupCode = hSql.Reader.GetString(10);
                    item.type = "O";
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
            return item;
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

                clsGlobalVariable objGlobal = new clsGlobalVariable();
                string LangId = objGlobal.CultureInfo;

                String strSql = " select a.OID,isnull(x.Name,a.Name),isnull(a.ItemNo,''),isnull(a.ItemSuplNo,''),isnull(a.WrksId,''),isnull(a.SelPr,0),null,isnull(b.NAME,''),isnull(b.BUYPR,0),isnull(c.NAME,'') " +
                    " from ZSC_OptionDetail a left join ITEM b on a.ITEMNO=b.ITEMNO and a.ITEMSUPLNO=b.SUPLNO left join WRKS c on a.WRKSID=c.WRKSID and c.WPTYPE='T' " +
                    " left join ZSC_OptionForeignName x on x.ObjectType=3 and x.ObjectOID=a.OID and x.LangId=? " +
                    " where a.OptionOID=? order by isnull(x.Name,a.Name) ";
                hSql.NewCommand(strSql);
                hSql.Com.Parameters.AddWithValue("LangId", LangId);
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
        public static List<SCOptionDetail> getContractOptionDetailList(int OptionOID,int ContractOID)
        {
            List<SCOptionDetail> Result = new List<SCOptionDetail>();
            clsSqlFactory hSql = new clsSqlFactory();
            try
            {

                clsGlobalVariable objGlobal = new clsGlobalVariable();
                string LangId = objGlobal.CultureInfo;

                String strSql = " select a.OID,isnull(x.Name,a.Name),isnull(a.ItemNo,''),isnull(a.ItemSuplNo,''),isnull(a.WrksId,''),isnull(a.SelPr,0),null,isnull(b.NAME,''),isnull(b.BUYPR,0),isnull(c.NAME,'') " +
                     ", isnull(d.Info, ''), isnull(d.SelPr, 0) as SalesPrice,isnull(d.Quantity, 0),isnull(d.PartialPayer, 0) " +
                    " from ZSC_OptionDetail a left join ITEM b on a.ITEMNO=b.ITEMNO and a.ITEMSUPLNO=b.SUPLNO left join WRKS c on a.WRKSID=c.WRKSID and c.WPTYPE='T' " +
                    " left join ZSC_OptionForeignName x on x.ObjectType=3 and x.ObjectOID=a.OID and x.LangId=? " +
                     " inner join ZSC_ContractOption d on a.OID = d.OptionDetailOID and d.ContractOID =? and d.OptionOID=? " +
                    " order by isnull(x.Name,a.Name) ";
                hSql.NewCommand(strSql);
                hSql.Com.Parameters.AddWithValue("LangId", LangId);
                hSql.Com.Parameters.AddWithValue("ContractOID", ContractOID);
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
                    item.Info = hSql.Reader.GetString(10);
                    item.SelPr = hSql.Reader.GetDecimal(11);
                    item.Quantity = hSql.Reader.GetInt32(12);
                    item.PartialPayer = hSql.Reader.GetString(13);
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

        public static SCOptionDetail getByOID(int detailOID)
        {
            SCOptionDetail item = null;
            clsSqlFactory hSql = new clsSqlFactory();
            try
            {
                clsGlobalVariable objGlobal = new clsGlobalVariable();
                string LangId = objGlobal.CultureInfo;

                String strSql = " select a.OID,isnull(x.Name,a.Name),isnull(a.ItemNo,''),isnull(a.ItemSuplNo,''),isnull(a.WrksId,''),isnull(a.SelPr,0),null,isnull(b.NAME,''),isnull(b.BUYPR,0),isnull(c.NAME,'') " +
                    " from ZSC_OptionDetail a left join ITEM b on a.ITEMNO=b.ITEMNO and a.ITEMSUPLNO=b.SUPLNO left join WRKS c on a.WRKSID=c.WRKSID and c.WPTYPE='T' " +
                    " left join ZSC_OptionForeignName x on x.ObjectType=3 and x.ObjectOID=a.OID and x.LangId=? " +
                    " where a.OID=? order by isnull(x.Name,a.Name) ";
                hSql.NewCommand(strSql);
                hSql.Com.Parameters.AddWithValue("LangId", LangId);
                hSql.Com.Parameters.AddWithValue("detailOID", detailOID);
                hSql.ExecuteReader();
                item = new SCOptionDetail();
                while (hSql.Read())
                {   
                    item.OID = hSql.Reader.GetInt32(0);
                    item.Name = hSql.Reader.GetString(1);
                    item.ItemNo = hSql.Reader.GetString(2);
                    item.ItemSuplNo = hSql.Reader.GetString(3);
                    item.WrksId = hSql.Reader.GetString(4);
                    item.SelPr = hSql.Reader.GetDecimal(5);
                    item.ItemName = hSql.Reader.GetString(7);
                    item.BuyPr = hSql.Reader.GetDecimal(8);
                    item.WrksName = hSql.Reader.GetString(9);
                    item.type = "D";
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
            return item;
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
        public string PartialPayer { get; set; } = "";
        public int isAvailable { get; set; } = 0;
        public bool isMarkDeleted { get; set; } = false;
        public string type { get; set; }
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
        public bool isLeadExport { get; set; }
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


            List<SCOptionCategory> categoryPriceList = SCOptionCategory.getContractOptionCategoryPriceList(contactTypeId);
            List<SCOption> Options = null;
            List<SCOptionDetail> OptionDetails = null;

            SCOptionPrice obj = null;
            if (categoryPriceList != null && categoryPriceList.Count > 0)
            {
                foreach (SCOptionCategory category in categoryPriceList)
                {
                    //Tao 1 ban ghi SCOptionPrice voi du lieu Category
                    obj = new SCOptionPrice();
                    obj.CategoryOID = category.OID;
                    obj.CategoryName = category.Name;
                    obj.ContractTypeOID = contactTypeId;
                    obj.IsAvailable = category.isAvailable;
                    Result.Add(obj);

                    //Duyet Option
                    Options = category.Options;
                    if (Options != null && Options.Count > 0)
                    {
                        foreach (SCOption option in Options)
                        {
                            //Tao ban ghi SCOptionPrice voi du lieu Options
                            obj = new SCOptionPrice();
                            obj.CategoryOID = category.OID;
                            obj.CategoryName = category.Name;
                            obj.ContractTypeOID = contactTypeId;
                            obj.OptionOID = option.OID;
                            obj.OptionName = option.Name;
                            obj.IsAvailable = option.isAvailable;
                            Result.Add(obj);

                            //Duyet OptionDetails
                            OptionDetails = option.OptionDetails;
                            if (OptionDetails != null && OptionDetails.Count > 0)
                            {
                                foreach (SCOptionDetail detail in OptionDetails)
                                {
                                    //Tao ban ghi SCOptionPrice voi du lieu OptionDetails
                                    obj = new SCOptionPrice();
                                    obj.CategoryOID = category.OID;
                                    obj.CategoryName = category.Name;
                                    obj.OptionOID = option.OID;
                                    obj.OptionName = option.Name;
                                    obj.ContractTypeOID = contactTypeId;
                                    obj.OptionDetailOID = detail.OID;
                                    obj.OptionDetailName = detail.Name;
                                    obj.IsAvailable = detail.isAvailable;
                                    //System.Diagnostics.Debug.WriteLine("CategoryOID: " + obj.CategoryOID + " - OptionDetailOID: " + obj.OptionDetailOID + " - isAvailable: " + detail.isAvailable);
                                    Result.Add(obj);
                                }
                            }
                        }
                    }
                }
            }
            return Result;
        }
    }

    public class ExtraKmAccountingType
    {
        public const string None = "";
        public const string OnlyHigher = "H";
        public const string OnlyLower = "L";
        public const string Both = "A";
    }
    public class ExtraKmBillingPeriodType
    {
        public const string None = "";
        public const string Monthly = "M";
        public const string HalfYear = "H";
        public const string Yearly = "Y";
    }
    public class PaymentPeriodType
    {
        public const string Monthly = "M";
        public const string Quarterly = "Q";
        public const string HalfYear = "H";
        public const string Yearly = "Y";
    }
    public class ContractTerminationType
    {
        public const string TimeBase = "K";
        public const string KmOrHourBase = "T";
    }
    public class CostBasisType
    {
        public const string Monthly = "M";
        public const string KmOrHour = "K";
        public const string KmOrHourWithLump = "L";
    }
    public class PaymentCollectionType
    {
        public const string ESR = "E";
        public const string Debit = "D";
        public const string Transfer = "R";
    }
    public class PaymentGroupingType
    {
        public const string Customer = "C";
        public const string Contract = "S";
        public const string FlatRate = "F";
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
        public int CategoryOID { get; set; }
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
                    //Delete
                    if (obj.OptionDetailOID <= 0)
                    {
                        if (obj.OptionOID <= 0)
                        {
                            bRet = hSql.NewCommand("DELETE FROM ZSC_OptionPriceList WHERE ContractTypeOID=? AND OptionCategoryOID=? AND OptionOID is null AND OptionDetailOID is null");
                            hSql.Com.Parameters.AddWithValue("ContractTypeOID", obj.ContractTypeOID);
                            hSql.Com.Parameters.AddWithValue("OptionCategoryOID", obj.CategoryOID);
                        }
                        else
                        {
                            bRet = hSql.NewCommand("DELETE FROM ZSC_OptionPriceList WHERE ContractTypeOID=? AND OptionCategoryOID=? AND OptionOID=? AND OptionDetailOID is null");
                            hSql.Com.Parameters.AddWithValue("ContractTypeOID", obj.ContractTypeOID);
                            hSql.Com.Parameters.AddWithValue("OptionCategoryOID", obj.CategoryOID);
                            hSql.Com.Parameters.AddWithValue("OptionOID", obj.OptionOID);
                        }
                    }
                    else
                    {
                        bRet = hSql.NewCommand("DELETE FROM ZSC_OptionPriceList WHERE ContractTypeOID=? AND OptionCategoryOID=? AND OptionOID=? AND OptionDetailOID=?");
                        hSql.Com.Parameters.AddWithValue("ContractTypeOID", obj.ContractTypeOID);
                        hSql.Com.Parameters.AddWithValue("OptionCategoryOID", obj.CategoryOID);
                        hSql.Com.Parameters.AddWithValue("OptionOID", obj.OptionOID);
                        hSql.Com.Parameters.AddWithValue("OptionDetailOID", obj.OptionDetailOID);
                    }
                    bRet = bRet && hSql.ExecuteNonQuery();

                    //Add
                    bRet = hSql.NewCommand("INSERT INTO ZSC_OptionPriceList(ContractTypeOID,OptionCategoryOID,OptionOID,OptionDetailOID,IsAvailable,Info,Created,Modified) values(?,?,?,?,?,?,getdate(),getdate())");
                    hSql.Com.Parameters.AddWithValue("ContractTypeOID", ((object)obj.ContractTypeOID) ?? DBNull.Value);
                    hSql.Com.Parameters.AddWithValue("OptionCategoryOID", obj.CategoryOID);
                    if (obj.OptionOID <= 0)
                    {
                        hSql.Com.Parameters.AddWithValue("OptionOID", DBNull.Value);
                    }
                    else
                    {
                        hSql.Com.Parameters.AddWithValue("OptionOID", ((object)obj.OptionOID) ?? DBNull.Value);
                    }

                    if (obj.OptionDetailOID <= 0)
                    {
                        hSql.Com.Parameters.AddWithValue("OptionDetailOID", DBNull.Value);
                    }
                    else
                    {
                        hSql.Com.Parameters.AddWithValue("OptionDetailOID", ((object)obj.OptionDetailOID) ?? DBNull.Value);
                    }
                    hSql.Com.Parameters.AddWithValue("IsAvailable", obj.IsAvailable);
                    hSql.Com.Parameters.AddWithValue("Info", ((object)obj.Info) ?? DBNull.Value);
                    //hSql.Com.Parameters.AddWithValue("Info", obj.Info);
                    bRet = bRet && hSql.ExecuteNonQuery();
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
        protected static readonly ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

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
                            bRet = hSql.NewCommand("update ZSC_ContractType set Name=?,IsInvoice=?,IsActive=?,IsCollective=?,Modified=getdate(),IsLeadExport=? where OID=?");
                            hSql.Com.Parameters.AddWithValue("Name", objContractType.Name);
                            hSql.Com.Parameters.AddWithValue("IsInvoice", objContractType.isInvoice);
                            hSql.Com.Parameters.AddWithValue("IsActive", objContractType.isActive);
                            hSql.Com.Parameters.AddWithValue("IsCollective", objContractType.isCollective);
                            hSql.Com.Parameters.AddWithValue("IsLeadExport", objContractType.isLeadExport);
                            hSql.Com.Parameters.AddWithValue("OID", objContractType.OID);

                        }
                        bRet = bRet && hSql.ExecuteNonQuery();
                    }
                    else
                    {
                        //new
                        bRet = hSql.NewCommand("insert into ZSC_ContractType(Name,IsInvoice,IsActive,IsCollective,Created,Modified,?) values(?,?,?,?,getdate(),getdate(),?)");
                        hSql.Com.Parameters.AddWithValue("Name", objContractType.Name);
                        hSql.Com.Parameters.AddWithValue("IsInvoice", objContractType.isInvoice);
                        hSql.Com.Parameters.AddWithValue("IsActive", objContractType.isActive);
                        hSql.Com.Parameters.AddWithValue("IsCollective", objContractType.isCollective);
                        hSql.Com.Parameters.AddWithValue("IsLeadExport", objContractType.isLeadExport);
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

        public static SCContractType findContractType(int contractTypeOID)
        {
            SCContractType Result = new SCContractType();
            Result = ContractTypes.Find(x => x.OID == contractTypeOID);
            return Result;
        }

        public static clsBaseListItem findSite(string SiteId)
        {
            clsBaseListItem Result = new clsBaseListItem();
            Result = Sites.Find(x => x.strValue1 == SiteId);
            return Result;
        }

        public static Contract searchContracts(int paramOID)
        {
            SCBase sC = new SCBase();
            List<Contract> Result = sC.searchContracts(new List<SCContractType>(), new List<String>(), new List<String>(), "", paramOID);
            if (Result.Count > 0) return Result[0];
            else return null;
        }
        public static List<Contract> searchContracts(List<SCContractType> contractTypes, List<String> sites, List<String> statuses, string namephrase)
        {
            SCBase sC = new SCBase();
            return sC.searchContracts(contractTypes, sites, statuses, namephrase, 0);
        }
        private List<Contract> searchContracts(List<SCContractType> contractTypes, List<String> sites, List<String> statuses, string namephrase, int paramOID)
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
                if (paramOID > 0)
                {
                    strSqlWhere += " and a.OID = " + paramOID.ToString();
                }

                String strSql = "select ";
                strSql += "a.CapitalMonthAmount, a.CapitalStartAmount, a.CapitalStartPayer, a.CareSmanId, a.ContractCustId, a.ContractEndDate, a.ContractEndHour, a.ContractEndKm, " +
                    "a.ContractNo, a.ContractPeriodHour, a.ContractPeriodKm, a.ContractPeriodKmHour, a.ContractPeriodMonth, a.ContractStartDate, a.ContractStartHour, " +
                    "a.ContractStartKm, a.ContractStatus, a.ContractTypeOID, a.CostBasedOnService, a.CostBasis, a.CostCenter, a.CostKmBasis, a.CostMonthBasis, a.CostPerKm, " +
                    "a.Created, a.ExtContractNo, a.ExtraKmAccounting, a.ExtraKmHighAmount, a.ExtraKmInvoicedAmount, a.ExtraKmInvoicePeriod, a.ExtraKmLowAmount, a.ExtraKmMaxDeviation, " +
                    "a.InvoiceStartDate, a.InvoiceCustId, a.InvoiceEndDate, a.InvoiceSiteId, a.IsBodyIncl, a.IsCoolingIncl, a.IsCraneIncl, a.IsInvoiceDetail, a.IsManualInvoice, " +
                    "a.IsTailLiftIncl, a.LastInvoiceDate, a.Modified, a.NextInvoiceDate, a.OID, a.PaymentCollectionType, a.PaymentGroupingLevel, a.PaymentIsInBlock, " +
                    "a.PaymentNextBlockEnd, a.PaymentNextBlockStart, a.PaymentPeriod, a.PaymentTerm, a.RespSmanId, a.RiskCustId, a.RiskLevel, a.RollingCode, a.SiteId, " +
                    "a.TerminationType, a.ValidWorkshopCode, a.VehiId, a.VersionNo, ";
                strSql += "c1.LNAME as ContractCustName,c1.POSTCD as ContractCustPostCd,c1.PO as ContractCustCity,c1.WTEL as ContractCustPhone,c1.EMAIL as ContractCustEmail,c1.ADDR2 as ContractCustAddress,";
                strSql += "c2.C2 as CapitalStartPayerName, ";
                strSql += "m1.NAME as CareSmanName, m1.PHONE as CareSmanPhone, m1.EMAIL CareSmanEmail, ";
                strSql += "m2.NAME as RespSmanName, m2.PHONE as RespSmanPhone, m2.EMAIL RespSmanEmail, ";
                strSql += "c3.LNAME as InvoiceCustName,c3.POSTCD as InvoiceCustPostCd,c3.PO as InvoiceCustCity,c3.WTEL as InvoiceCustPhone,c3.EMAIL as InvoiceCustEmail,c3.ADDR2 as InvoiceCustAddress,";
                strSql += "c4.LNAME as RiskCustName,c4.POSTCD as RiskCustPostCd,c4.PO as RiskCustCity,c4.WTEL as RiskCustPhone,c4.EMAIL as RiskCustEmail,c4.ADDR2 as RiskCustAddress,";
                strSql += "c5.C2 as RollingCodeName, c6.c2 as ValidWorkshopName, ";
                strSql += "v.LICNO as VehicleLicenseNo, v.SERIALNO as VehicleVIN, v.MAKE as VehicleMake, v.MODEL as VehicleModel, ";
                strSql += "a.CapitalMonthPayer, c7.C2 as CapitalMonthPayerName ";
                strSql += " from ZSC_Contract a ";
                strSql += " inner join CUST c1 on a.ContractCustId = c1.CUSTID ";
                strSql += " inner join VEHI v on a.VEHIID = v.VEHIID ";
                strSql += " left join ALL_CORW c2 on a.CapitalStartPayer = c2.C1 and a.SiteId = c2._UNITID and c2.CODAID='ZSCCAPPAYE' ";
                strSql += " left join CUST c3 on a.InvoiceCustId = c3.CUSTID ";
                strSql += " left join CUST c4 on a.RiskCustId = c4.CUSTID ";
                strSql += " left join ALL_CORW c5 on a.RollingCode = c5.C1 and a.SiteId = c5._UNITID and c5.CODAID='ZSCROLLING' ";
                strSql += " left join ALL_CORW c6 on a.ValidWorkshopCode = c6.C1 and a.SiteId = c6._UNITID and c6.CODAID='ZSCVALIDWS' ";
                strSql += " left join ALL_CORW c7 on a.CapitalMonthPayer = c7.C1 and a.SiteId = c7._UNITID and c7.CODAID='ZSCCAPPAYE' ";
                strSql += " left join SMAN m1 on a.CareSmanId = m1.SMANID ";
                strSql += " left join SMAN m2 on a.RespSmanId = m2.SMANID ";
                if (strSqlWhere != "")
                {
                    strSql += " where 1=1 " + strSqlWhere;
                }
                _log.Debug(strSql);
                hSql.NewCommand(strSql);
                hSql.ExecuteReader();
                while (hSql.Read())
                {
                    Contract item = new Contract();
                    int colId;
                    item.ContractCapitalData = new ContractCapital();
                    if (item.ContractCapitalData != null)
                    {
                        colId = hSql.Reader.GetOrdinal("CapitalMonthAmount");
                        if (!hSql.Reader.IsDBNull(colId)) item.ContractCapitalData.CapitalMonthAmount = hSql.Reader.GetDecimal(colId);
                        colId = hSql.Reader.GetOrdinal("CapitalStartAmount");
                        if (!hSql.Reader.IsDBNull(colId)) item.ContractCapitalData.CapitalStartAmount = hSql.Reader.GetDecimal(colId);
                        colId = hSql.Reader.GetOrdinal("CapitalStartPayer");
                        if (!hSql.Reader.IsDBNull(colId))
                        {
                            item.ContractCapitalData.CapitalStartPayer = new clsBaseListItem();
                            item.ContractCapitalData.CapitalStartPayer.strValue1 = hSql.Reader.GetString(colId);
                            colId = hSql.Reader.GetOrdinal("CapitalStartPayerName");
                            if (!hSql.Reader.IsDBNull(colId)) item.ContractCapitalData.CapitalStartPayer.strText = hSql.Reader.GetString(colId);
                        }
                        colId = hSql.Reader.GetOrdinal("CapitalMonthPayer");
                        if (!hSql.Reader.IsDBNull(colId))
                        {
                            item.ContractCapitalData.CapitalMonthPayer = new clsBaseListItem();
                            item.ContractCapitalData.CapitalMonthPayer.strValue1 = hSql.Reader.GetString(colId);
                            //longdq comment
                            colId = hSql.Reader.GetOrdinal("CapitalMonthPayerName");
                            if (!hSql.Reader.IsDBNull(colId)) item.ContractCapitalData.CapitalMonthPayer.strText = hSql.Reader.GetString(colId);
                        }
                    }
                    colId = hSql.Reader.GetOrdinal("CareSmanId");
                    if (!hSql.Reader.IsDBNull(colId))
                    {
                        item.CareSmanId = new ContractEmployee();
                        item.CareSmanId.SmanId = hSql.Reader.GetString(colId);
                        colId = hSql.Reader.GetOrdinal("CareSmanName");
                        if (!hSql.Reader.IsDBNull(colId)) item.CareSmanId.Name = hSql.Reader.GetString(colId);
                        colId = hSql.Reader.GetOrdinal("CareSmanPhone");
                        if (!hSql.Reader.IsDBNull(colId)) item.CareSmanId.Phone = hSql.Reader.GetString(colId);
                        colId = hSql.Reader.GetOrdinal("CareSmanEmail");
                        if (!hSql.Reader.IsDBNull(colId)) item.CareSmanId.Email = hSql.Reader.GetString(colId);
                    }
                    colId = hSql.Reader.GetOrdinal("RespSmanId");
                    if (!hSql.Reader.IsDBNull(colId))
                    {
                        item.RespSmanId = new ContractEmployee();
                        item.RespSmanId.SmanId = hSql.Reader.GetString(colId);
                        colId = hSql.Reader.GetOrdinal("RespSmanName");
                        if (!hSql.Reader.IsDBNull(colId)) item.RespSmanId.Name = hSql.Reader.GetString(colId);
                        colId = hSql.Reader.GetOrdinal("RespSmanPhone");
                        if (!hSql.Reader.IsDBNull(colId)) item.RespSmanId.Phone = hSql.Reader.GetString(colId);
                        colId = hSql.Reader.GetOrdinal("RespSmanEmail");
                        if (!hSql.Reader.IsDBNull(colId)) item.RespSmanId.Email = hSql.Reader.GetString(colId);
                    }
                    colId = hSql.Reader.GetOrdinal("ContractCustId");
                    if (!hSql.Reader.IsDBNull(colId))
                    {
                        item.ContractCustId = new ContractCustomer();
                        item.ContractCustId.CustId = hSql.Reader.GetInt32(colId);
                        colId = hSql.Reader.GetOrdinal("ContractCustName");
                        if (!hSql.Reader.IsDBNull(colId)) item.ContractCustId.Name = hSql.Reader.GetString(colId);
                        colId = hSql.Reader.GetOrdinal("ContractCustPostCd");
                        if (!hSql.Reader.IsDBNull(colId)) item.ContractCustId.PostCode = hSql.Reader.GetString(colId);
                        colId = hSql.Reader.GetOrdinal("ContractCustCity");
                        if (!hSql.Reader.IsDBNull(colId)) item.ContractCustId.City = hSql.Reader.GetString(colId);
                        colId = hSql.Reader.GetOrdinal("ContractCustPhone");
                        if (!hSql.Reader.IsDBNull(colId)) item.ContractCustId.Phone = hSql.Reader.GetString(colId);
                        colId = hSql.Reader.GetOrdinal("ContractCustEmail");
                        if (!hSql.Reader.IsDBNull(colId)) item.ContractCustId.Email = hSql.Reader.GetString(colId);
                        colId = hSql.Reader.GetOrdinal("ContractCustAddress");
                        if (!hSql.Reader.IsDBNull(colId)) item.ContractCustId.Address = hSql.Reader.GetString(colId);
                    }
                    if (item.ContractDateData != null)
                    {
                        colId = hSql.Reader.GetOrdinal("ContractEndDate");
                        if (!hSql.Reader.IsDBNull(colId)) item.ContractDateData.ContractEndDate = hSql.Reader.GetDateTime(colId);
                        colId = hSql.Reader.GetOrdinal("ContractEndHour");
                        if (!hSql.Reader.IsDBNull(colId)) item.ContractDateData.ContractEndHour = hSql.Reader.GetInt32(colId);
                        colId = hSql.Reader.GetOrdinal("ContractEndKm");
                        if (!hSql.Reader.IsDBNull(colId)) item.ContractDateData.ContractEndKm = hSql.Reader.GetInt32(colId);
                        colId = hSql.Reader.GetOrdinal("ContractStartDate");
                        if (!hSql.Reader.IsDBNull(colId)) item.ContractDateData.ContractStartDate = hSql.Reader.GetDateTime(colId);
                        colId = hSql.Reader.GetOrdinal("ContractStartHour");
                        if (!hSql.Reader.IsDBNull(colId)) item.ContractDateData.ContractStartHour = hSql.Reader.GetInt32(colId);
                        colId = hSql.Reader.GetOrdinal("ContractStartKm");
                        if (!hSql.Reader.IsDBNull(colId)) item.ContractDateData.ContractStartKm = hSql.Reader.GetInt32(colId);
                        colId = hSql.Reader.GetOrdinal("ContractPeriodHour");
                        if (!hSql.Reader.IsDBNull(colId)) item.ContractDateData.ContractPeriodHour = hSql.Reader.GetInt32(colId);
                        colId = hSql.Reader.GetOrdinal("ContractPeriodKm");
                        if (!hSql.Reader.IsDBNull(colId)) item.ContractDateData.ContractPeriodKm = hSql.Reader.GetInt32(colId);
                        colId = hSql.Reader.GetOrdinal("ContractPeriodKmHour");
                        if (!hSql.Reader.IsDBNull(colId)) item.ContractDateData.ContractPeriodKmHour = hSql.Reader.GetInt32(colId);
                        colId = hSql.Reader.GetOrdinal("ContractPeriodMonth");
                        if (!hSql.Reader.IsDBNull(colId)) item.ContractDateData.ContractPeriodMonth = hSql.Reader.GetInt32(colId);
                        colId = hSql.Reader.GetOrdinal("InvoiceStartDate");
                        if (!hSql.Reader.IsDBNull(colId)) item.ContractDateData.InvoiceStartDate = hSql.Reader.GetDateTime(colId);
                        colId = hSql.Reader.GetOrdinal("InvoiceEndDate");
                        if (!hSql.Reader.IsDBNull(colId)) item.ContractDateData.InvoiceEndDate = hSql.Reader.GetDateTime(colId);

                    }
                    colId = hSql.Reader.GetOrdinal("ContractNo");
                    if (!hSql.Reader.IsDBNull(colId)) item.ContractNo = hSql.Reader.GetInt32(colId);
                    colId = hSql.Reader.GetOrdinal("ContractStatus");
                    if (!hSql.Reader.IsDBNull(colId)) item.ContractStatus = hSql.Reader.GetString(colId);
                    colId = hSql.Reader.GetOrdinal("OID");
                    item.ContractOID = hSql.Reader.GetInt32(colId);
                    colId = hSql.Reader.GetOrdinal("ContractNo");
                    if (!hSql.Reader.IsDBNull(colId)) item.ContractNo = hSql.Reader.GetInt32(colId);
                    colId = hSql.Reader.GetOrdinal("ContractTypeOID");
                    if (!hSql.Reader.IsDBNull(colId))
                    {
                        item.ContractTypeOID = SCBase.findContractType(hSql.Reader.GetInt32(colId));
                    }
                    if (item.ContractCostData != null)
                    {
                        colId = hSql.Reader.GetOrdinal("CostBasedOnService");
                        if (!hSql.Reader.IsDBNull(colId)) item.ContractCostData.CostBasedOnService = hSql.Reader.GetDecimal(colId);
                        colId = hSql.Reader.GetOrdinal("CostBasis");
                        if (!hSql.Reader.IsDBNull(colId)) item.ContractCostData.CostBasis.strValue1 = hSql.Reader.GetString(colId);
                        colId = hSql.Reader.GetOrdinal("CostKmBasis");
                        if (!hSql.Reader.IsDBNull(colId)) item.ContractCostData.CostKmBasis = hSql.Reader.GetDecimal(colId);
                        colId = hSql.Reader.GetOrdinal("CostMonthBasis");
                        if (!hSql.Reader.IsDBNull(colId)) item.ContractCostData.CostMonthBasis = hSql.Reader.GetDecimal(colId);
                        colId = hSql.Reader.GetOrdinal("CostPerKm");
                        if (!hSql.Reader.IsDBNull(colId)) item.ContractCostData.CostPerKm = hSql.Reader.GetDecimal(colId);
                    }
                    colId = hSql.Reader.GetOrdinal("CostCenter");
                    if (!hSql.Reader.IsDBNull(colId)) item.CostCenter.strValue1 = hSql.Reader.GetString(colId);
                    colId = hSql.Reader.GetOrdinal("Created");
                    if (!hSql.Reader.IsDBNull(colId)) item.Created = hSql.Reader.GetDateTime(colId);
                    colId = hSql.Reader.GetOrdinal("ExtContractNo");
                    if (!hSql.Reader.IsDBNull(colId)) item.ExtContractNo = hSql.Reader.GetString(colId);
                    if (item.ContractExtraKmData != null)
                    {
                        colId = hSql.Reader.GetOrdinal("ExtraKmAccounting");
                        if (!hSql.Reader.IsDBNull(colId)) item.ContractExtraKmData.ExtraKmAccounting.strValue1 = hSql.Reader.GetString(colId);
                        colId = hSql.Reader.GetOrdinal("ExtraKmHighAmount");
                        if (!hSql.Reader.IsDBNull(colId)) item.ContractExtraKmData.ExtraKmHighAmount = hSql.Reader.GetDecimal(colId);
                        colId = hSql.Reader.GetOrdinal("ExtraKmInvoicedAmount");
                        if (!hSql.Reader.IsDBNull(colId)) item.ContractExtraKmData.ExtraKmInvoicedAmount = hSql.Reader.GetDecimal(colId);
                        colId = hSql.Reader.GetOrdinal("ExtraKmInvoicePeriod");
                        if (!hSql.Reader.IsDBNull(colId)) item.ContractExtraKmData.ExtraKmInvoicePeriod.strValue1 = hSql.Reader.GetString(colId);
                        colId = hSql.Reader.GetOrdinal("ExtraKmLowAmount");
                        if (!hSql.Reader.IsDBNull(colId)) item.ContractExtraKmData.ExtraKmLowAmount = hSql.Reader.GetDecimal(colId);
                        colId = hSql.Reader.GetOrdinal("ExtraKmMaxDeviation");
                        if (!hSql.Reader.IsDBNull(colId)) item.ContractExtraKmData.ExtraKmMaxDeviation = hSql.Reader.GetDecimal(colId);
                    }
                    colId = hSql.Reader.GetOrdinal("InvoiceCustId");
                    if (!hSql.Reader.IsDBNull(colId))
                    {
                        item.InvoiceCustId = new ContractCustomer();
                        item.InvoiceCustId.CustId = hSql.Reader.GetInt32(colId);
                        colId = hSql.Reader.GetOrdinal("InvoiceCustName");
                        if (!hSql.Reader.IsDBNull(colId)) item.InvoiceCustId.Name = hSql.Reader.GetString(colId);
                        colId = hSql.Reader.GetOrdinal("InvoiceCustPostCd");
                        if (!hSql.Reader.IsDBNull(colId)) item.InvoiceCustId.PostCode = hSql.Reader.GetString(colId);
                        colId = hSql.Reader.GetOrdinal("InvoiceCustCity");
                        if (!hSql.Reader.IsDBNull(colId)) item.InvoiceCustId.City = hSql.Reader.GetString(colId);
                        colId = hSql.Reader.GetOrdinal("InvoiceCustPhone");
                        if (!hSql.Reader.IsDBNull(colId)) item.InvoiceCustId.Phone = hSql.Reader.GetString(colId);
                        colId = hSql.Reader.GetOrdinal("InvoiceCustEmail");
                        if (!hSql.Reader.IsDBNull(colId)) item.InvoiceCustId.Email = hSql.Reader.GetString(colId);
                        colId = hSql.Reader.GetOrdinal("InvoiceCustAddress");
                        if (!hSql.Reader.IsDBNull(colId)) item.InvoiceCustId.Address = hSql.Reader.GetString(colId);
                    }
                    colId = hSql.Reader.GetOrdinal("InvoiceSiteId");
                    if (!hSql.Reader.IsDBNull(colId))
                    {
                        item.InvoiceSiteId = SCBase.findSite(hSql.Reader.GetString(colId));
                    }
                    colId = hSql.Reader.GetOrdinal("IsBodyIncl");
                    if (!hSql.Reader.IsDBNull(colId)) item.IsBodyIncl = hSql.Reader.GetBoolean(colId);
                    colId = hSql.Reader.GetOrdinal("IsCraneIncl");
                    if (!hSql.Reader.IsDBNull(colId)) item.IsCraneIncl = hSql.Reader.GetBoolean(colId);
                    colId = hSql.Reader.GetOrdinal("IsTailLiftIncl");
                    if (!hSql.Reader.IsDBNull(colId)) item.IsTailLiftIncl = hSql.Reader.GetBoolean(colId);
                    colId = hSql.Reader.GetOrdinal("IsCoolingIncl");
                    if (!hSql.Reader.IsDBNull(colId)) item.IsCoolingIncl = hSql.Reader.GetBoolean(colId);

                    colId = hSql.Reader.GetOrdinal("IsInvoiceDetail");
                    if (!hSql.Reader.IsDBNull(colId)) item.IsInvoiceDetail = hSql.Reader.GetBoolean(colId);
                    colId = hSql.Reader.GetOrdinal("IsManualInvoice");
                    if (!hSql.Reader.IsDBNull(colId)) item.IsManualInvoice = hSql.Reader.GetBoolean(colId);
                    colId = hSql.Reader.GetOrdinal("LastInvoiceDate");
                    if (!hSql.Reader.IsDBNull(colId)) item.LastInvoiceDate = hSql.Reader.GetDateTime(colId);
                    colId = hSql.Reader.GetOrdinal("NextInvoiceDate");
                    if (!hSql.Reader.IsDBNull(colId)) item.NextInvoiceDate = hSql.Reader.GetDateTime(colId);
                    colId = hSql.Reader.GetOrdinal("Modified");
                    if (!hSql.Reader.IsDBNull(colId)) item.Modified = hSql.Reader.GetDateTime(colId);
                    if (item.ContractPaymentData != null)
                    {
                        colId = hSql.Reader.GetOrdinal("PaymentCollectionType");
                        if (!hSql.Reader.IsDBNull(colId)) item.ContractPaymentData.PaymentCollectionType = hSql.Reader.GetString(colId);
                        colId = hSql.Reader.GetOrdinal("PaymentGroupingLevel");
                        if (!hSql.Reader.IsDBNull(colId)) item.ContractPaymentData.PaymentGroupingLevel = hSql.Reader.GetString(colId);
                        colId = hSql.Reader.GetOrdinal("PaymentIsInBlock");
                        if (!hSql.Reader.IsDBNull(colId)) item.ContractPaymentData.PaymentIsInBlock = hSql.Reader.GetBoolean(colId);
                        colId = hSql.Reader.GetOrdinal("PaymentNextBlockEnd");
                        if (!hSql.Reader.IsDBNull(colId)) item.ContractPaymentData.PaymentNextBlockEnd = hSql.Reader.GetDate(colId);
                        colId = hSql.Reader.GetOrdinal("PaymentNextBlockStart");
                        if (!hSql.Reader.IsDBNull(colId)) item.ContractPaymentData.PaymentNextBlockStart = hSql.Reader.GetDate(colId);
                        colId = hSql.Reader.GetOrdinal("PaymentPeriod");
                        if (!hSql.Reader.IsDBNull(colId)) item.ContractPaymentData.PaymentPeriod.strValue1 = hSql.Reader.GetString(colId);
                        colId = hSql.Reader.GetOrdinal("PaymentTerm");
                        if (!hSql.Reader.IsDBNull(colId)) item.ContractPaymentData.PaymentTerm = hSql.Reader.GetInt32(colId);
                    }
                    colId = hSql.Reader.GetOrdinal("RiskCustId");
                    if (!hSql.Reader.IsDBNull(colId))
                    {
                        item.RiskCustId = new ContractCustomer();
                        item.RiskCustId.CustId = hSql.Reader.GetInt32(colId);
                        colId = hSql.Reader.GetOrdinal("RiskCustName");
                        if (!hSql.Reader.IsDBNull(colId)) item.RiskCustId.Name = hSql.Reader.GetString(colId);
                        colId = hSql.Reader.GetOrdinal("RiskCustPostCd");
                        if (!hSql.Reader.IsDBNull(colId)) item.RiskCustId.PostCode = hSql.Reader.GetString(colId);
                        colId = hSql.Reader.GetOrdinal("RiskCustCity");
                        if (!hSql.Reader.IsDBNull(colId)) item.RiskCustId.City = hSql.Reader.GetString(colId);
                        colId = hSql.Reader.GetOrdinal("RiskCustPhone");
                        if (!hSql.Reader.IsDBNull(colId)) item.RiskCustId.Phone = hSql.Reader.GetString(colId);
                        colId = hSql.Reader.GetOrdinal("RiskCustEmail");
                        if (!hSql.Reader.IsDBNull(colId)) item.RiskCustId.Email = hSql.Reader.GetString(colId);
                        colId = hSql.Reader.GetOrdinal("RiskCustAddress");
                        if (!hSql.Reader.IsDBNull(colId)) item.RiskCustId.Address = hSql.Reader.GetString(colId);
                    }
                    //
                    colId = hSql.Reader.GetOrdinal("RiskLevel");
                    if (!hSql.Reader.IsDBNull(colId)) item.RiskLevel = hSql.Reader.GetDecimal(colId);
                    colId = hSql.Reader.GetOrdinal("RollingCode");
                    if (!hSql.Reader.IsDBNull(colId)) item.RollingCode.strValue1 = hSql.Reader.GetString(colId);
                    colId = hSql.Reader.GetOrdinal("RollingCodeName");
                    if (!hSql.Reader.IsDBNull(colId)) item.RollingCode.strText = hSql.Reader.GetString(colId);
                    colId = hSql.Reader.GetOrdinal("SiteId");
                    if (!hSql.Reader.IsDBNull(colId))
                    {
                        item.SiteId = SCBase.findSite(hSql.Reader.GetString(colId));
                    }
                    colId = hSql.Reader.GetOrdinal("TerminationType");
                    if (!hSql.Reader.IsDBNull(colId)) item.TerminationType.strValue1 = hSql.Reader.GetString(colId);
                    colId = hSql.Reader.GetOrdinal("ValidWorkshopCode");
                    if (!hSql.Reader.IsDBNull(colId)) item.ValidWorkshopCode.strValue1 = hSql.Reader.GetString(colId);
                    colId = hSql.Reader.GetOrdinal("ValidWorkshopName");
                    if (!hSql.Reader.IsDBNull(colId)) item.ValidWorkshopCode.strText = hSql.Reader.GetString(colId);
                    colId = hSql.Reader.GetOrdinal("VersionNo");
                    if (!hSql.Reader.IsDBNull(colId)) item.VersionNo = hSql.Reader.GetInt32(colId);

                    colId = hSql.Reader.GetOrdinal("VehiId");
                    if (!hSql.Reader.IsDBNull(colId))
                    {
                        item.VehiId = new ContractVehicle();
                        item.VehiId.VehiId = hSql.Reader.GetInt32(colId);
                        colId = hSql.Reader.GetOrdinal("VehicleLicenseNo");
                        if (!hSql.Reader.IsDBNull(colId)) item.VehiId.LicenseNo = hSql.Reader.GetString(colId);
                        colId = hSql.Reader.GetOrdinal("VehicleVIN");
                        if (!hSql.Reader.IsDBNull(colId)) item.VehiId.VIN = hSql.Reader.GetString(colId);
                        colId = hSql.Reader.GetOrdinal("VehicleMake");
                        if (!hSql.Reader.IsDBNull(colId)) item.VehiId.Make = hSql.Reader.GetString(colId);
                        colId = hSql.Reader.GetOrdinal("VehicleModel");
                        if (!hSql.Reader.IsDBNull(colId)) item.VehiId.Model = hSql.Reader.GetString(colId);

                    }

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


        public List<clsBaseListItem> GetConfig(string configName)
        {
            List<clsBaseListItem> rtn = new List<clsBaseListItem>();
            clsSqlFactory hSql = new clsSqlFactory();
            try
            {
                String strSql = "select V1 as PaymentTerm,C2 as PaymentTermDescription from CORW where codaid = ?";
                hSql.NewCommand(strSql);
                hSql.Com.Parameters.AddWithValue("codaid", configName);
                hSql.ExecuteReader();
                int colId;
                while (hSql.Read())
                {
                    clsBaseListItem item = new clsBaseListItem();
                    colId = hSql.Reader.GetOrdinal("PaymentTerm");
                    if (!hSql.Reader.IsDBNull(colId))
                        item.nValue1 = hSql.Reader.GetInt32(colId);
                    else
                        item.nValue1 = 0;

                    colId = hSql.Reader.GetOrdinal("PaymentTermDescription");
                    if (!hSql.Reader.IsDBNull(colId))
                        item.strText = hSql.Reader.GetString(colId);
                    else
                        item.strText = "";
                    rtn.Add(item);
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
            return rtn;
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
            String strSql = " select a.OID,a.Name,isnull(a.IsInvoice,0),isnull(a.IsActive,0),isnull(a.IsCollective,0),isnull(a.IsLeadExport,0) from ZSC_ContractType a  order by a.Name ";
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
                item.isLeadExport = hSql.Reader.GetBoolean(5);
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
        public int nValue1 { get; set; }
        public string strValue1 { get; set; }
        public string strText { get; set; }
        public ObjTmp(String value, String text)
        {
            this.strValue1 = value;
            this.strText = text;
        }
        public ObjTmp(int id, String text)
        {
            this.nValue1 = id;
            this.strText = text;
        }
        public ObjTmp()
        {
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
                strSql = Regex.Replace(strSql, "maxresult", tmp.ToString());
            else
                strSql = Regex.Replace(strSql, "maxresult", "0");


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
    public class SCViewContract
    {
        public int _OID { get; set; }
        public string PartNr { get; set; }
        public string Name { get; set; }
        public string Supplier { get; set; }
        public string SearchKey { get; set; }
        public float SalesPr { get; set; }
        public float PurchasePr { get; set; }

        public static List<SCViewContract> seach(string namephrase)
        {

            string strSql = "select top maxresult a._OID as _OID, isnull(a.ITEMNO,'') as PartNr, a.NAME as Name, isnull(a.SUPLNO,'') as Supplier, isnull(a.SKEY,'') as SearchKey, isnull(a.SELPR,0) as SalesPr, isnull(a.BUYPR,0) as PurchasePr  from ZSC_Contract a  where 1=1 ";

            var tmp = MyUtils.GetMaxResult();
            if (tmp > 0)
                strSql = Regex.Replace(strSql, "maxresult", tmp.ToString());
            else
                strSql = Regex.Replace(strSql, "maxresult", "0");


            if (namephrase != "")
            {
                String strFTSQL = Utils.MyUtils.getFTSearchSQL(namephrase, "ASVIEW_SC_Contract");
                if (strFTSQL != "")
                {
                    strSql += " and exists (" + strFTSQL + " and v._OID=a._OID)";
                }

            }

            List<SCViewContract> Result = new List<SCViewContract>();
            clsSqlFactory hSql = new clsSqlFactory();
            try
            {


                hSql.NewCommand(strSql);
                hSql.ExecuteReader();
                while (hSql.Read())
                {
                    SCViewContract item = new SCViewContract();
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
    public class SCViewEmployee
    {
        public int _OID { get; set; }
        public string SmanId { get; set; }
        //  public string SearchKey { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }


        public static List<SCViewEmployee> seach(string namephrase)
        {

            string strSql = "select top maxresult a._OID as _OID, a.SMANID , a.NAME as Name, isnull(a.PHONE,'') as Phone, isnull(a.Email,'') as Email from SMAN a  where 1=1";

            var tmp = MyUtils.GetMaxResult();
            if (tmp > 0)
                strSql = Regex.Replace(strSql, "maxresult", tmp.ToString());
            else
                strSql = Regex.Replace(strSql, "maxresult", "0");


            if (namephrase != "")
            {
                String strFTSQL = Utils.MyUtils.getFTSearchSQL(namephrase, "ASVIEW_SMAN");
                if (strFTSQL != "")
                {
                    strSql += " and exists (" + strFTSQL + " and v._OID=a._OID)";
                }

            }

            List<SCViewEmployee> Result = new List<SCViewEmployee>();
            clsSqlFactory hSql = new clsSqlFactory();
            try
            {


                hSql.NewCommand(strSql);
                hSql.ExecuteReader();
                while (hSql.Read())
                {
                    SCViewEmployee item = new SCViewEmployee();
                    item._OID = hSql.Reader.GetInt32(0);
                    item.SmanId = hSql.Reader.GetString(1);
                    //item.SearchKey = hSql.Reader.GetString(2);
                    item.Name = hSql.Reader.GetString(2);
                    item.Phone = hSql.Reader.GetString(3);
                    item.Email = hSql.Reader.GetString(4);
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
