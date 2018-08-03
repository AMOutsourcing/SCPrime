using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using nsBaseClass;
using SCPrime.Utils;

namespace SCPrime.Model
{
    public class ContractCustomer
    {
        public int CustId;
        public string Name;
        public string Phone;
        public string Email;
        public string PostCode;
        public string City;
        public string Address;

        public override string ToString()
        {
            return Name;
        }
        public string getPhone()
        {
            return Phone;
        }
    }
    public class ContractEmployee
    {
        public string SmanId;
        public string Name;
        public string Phone;
        public string Email;
    }
    public class ContractVehicle
    {
        public int VehiId { get; set; }
        public string LicenseNo { get; set; }
        public string VIN { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string SubModel { get; set; }

        public List<clsBaseListItem> dynFields1 = new List<clsBaseListItem>();
        public List<clsBaseListItem> dynFields2 = new List<clsBaseListItem>();
        public List<clsBaseListItem> dynFields3 = new List<clsBaseListItem>();
        public List<clsBaseListItem> dynFields4 = new List<clsBaseListItem>();
        public List<VehicleMileage> Mileages = new List<VehicleMileage>();

        public bool loadMileages(clsSqlFactory hSql)
        {
            bool bRet = true;
            try
            {
                string strSql = "select  Created,Mileage,Info,InputType from V_ZSC_MileageReg where VEHIID=? order by Created desc ";
                hSql.NewCommand(strSql);
                hSql.Com.Parameters.Add("VEHIID", this.VehiId);
                hSql.ExecuteReader();
                while (hSql.Read())
                {
                    VehicleMileage field = new VehicleMileage();
                    int colId = hSql.Reader.GetOrdinal("Created");
                    if (!hSql.Reader.IsDBNull(colId)) field.MileageDate = hSql.Reader.GetDateTime(colId);
                    colId = hSql.Reader.GetOrdinal("Mileage");
                    if (!hSql.Reader.IsDBNull(colId)) field.Mileage = hSql.Reader.GetInt32(colId);
                    colId = hSql.Reader.GetOrdinal("Info");
                    if (!hSql.Reader.IsDBNull(colId)) field.Info = hSql.Reader.GetString(colId);
                    colId = hSql.Reader.GetOrdinal("InputType");
                    if (!hSql.Reader.IsDBNull(colId)) field.InputType = hSql.Reader.GetInt32(colId);
                    Mileages.Add(field);
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
        public bool loadDynFields(clsSqlFactory hSql)
        {
            bool bRet = true;
            try
            {
                string strSql = "select  a.ADINDATA as Data,b.C2 as Title,c.C3 as FieldGroup from ADIN a, CORW b,CORW c where a.INSTYPE='V' and a.ADINID=? and b.CODAID = 'ADDINFVEHI' and b.c1 = a.FIELDID and c.CODAID='ZSCVEHADIN' and c.C4=a.FIELDID order by c.C3,c.C1 ";
                hSql.NewCommand(strSql);
                hSql.Com.Parameters.Add("VEHIID", this.VehiId.ToString());
                hSql.ExecuteReader();
                while (hSql.Read())
                {
                    int colId = hSql.Reader.GetOrdinal("ADINDATA");
                    if (!hSql.Reader.IsDBNull(colId))
                    {
                        clsBaseListItem field = new clsBaseListItem();
                        field.strValue1 = hSql.Reader.GetString(colId);
                        colId = hSql.Reader.GetOrdinal("Title");
                        if (!hSql.Reader.IsDBNull(colId)) field.strText = hSql.Reader.GetString(colId);
                        colId = hSql.Reader.GetOrdinal("FieldGroup");
                        if (!hSql.Reader.IsDBNull(colId))
                        {
                            string strGroup = hSql.Reader.GetString(colId);
                            switch (strGroup)
                            {
                                case "BODY":
                                    dynFields1.Add(field);
                                    break;
                                case "CRANE":
                                    dynFields1.Add(field);
                                    break;
                                case "TAILLIFT":
                                    dynFields1.Add(field);
                                    break;
                                case "COOLING":
                                    dynFields1.Add(field);
                                    break;
                                default:
                                    break;
                            }

                        }
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

        public static List<ContractVehicle> seach(string namephrase)
        {

            string strSql = "select top maxresult a.VEHIID as _OID, isnull(a.LICNO,'') as VehicleLicenseNo, isnull(a.SERIALNO,'') as VehicleVIN, isnull(a.MAKE,'') as VehicleMake, isnull(a.MODEL,'') as VehicleModel, isnull(a.SUBMODEL,'') as VehicleSubModel  from VEHI a  where 1=1 ";

            var tmp = MyUtils.GetMaxResult();
            if (tmp > 0)
                strSql = Regex.Replace(strSql, "maxresult", tmp.ToString());
            else
                strSql = Regex.Replace(strSql, "maxresult", "0");


            if (namephrase != "")
            {
                String strFTSQL = Utils.MyUtils.getFTSearchSQL(namephrase, "FTVIEW_VEHI");
                if (strFTSQL != "")
                {
                    strSql += " and exists (" + strFTSQL + " and v._OID=a._OID)";
                }

            }

            List<ContractVehicle> Result = new List<ContractVehicle>();
            clsSqlFactory hSql = new clsSqlFactory();
            try
            {

                hSql.NewCommand(strSql);
                hSql.ExecuteReader();
                while (hSql.Read())
                {
                    ContractVehicle item = new ContractVehicle();
                    item.VehiId = hSql.Reader.GetInt32(0);
                    item.LicenseNo = hSql.Reader.GetString(1);
                    item.VIN = hSql.Reader.GetString(2);
                    item.Make = hSql.Reader.GetString(3);
                    item.Model = hSql.Reader.GetString(4);
                    item.SubModel = hSql.Reader.GetString(5);
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
    public class VehicleMileage
    {
        public DateTime MileageDate { get; set; }
        public int Mileage { get; set; }
        public String Info { get; set; }
        public int InputType { get; set; }

        public string CustomInputType
        {
            get
            {
                if (this.InputType == 1)
                {
                    return "Manual";
                }
                else
                {
                    return "From service history";
                }
            }

            set
            {
                this.InputType = 1;
            }
        }
    }
    public class ContractDate
    {
        public DateTime ContractStartDate;
        public int ContractStartKm;
        public int ContractStartHour;
        public DateTime InvoiceStartDate;
        public int ContractPeriodMonth;
        public int ContractPeriodKm;
        public int ContractPeriodHour;
        public int ContractPeriodKmHour;
        public DateTime ContractEndDate;
        public int ContractEndKm;
        public int ContractEndHour;
        public DateTime InvoiceEndDate;
        public ContractDate()
        {
            //default values
            ContractStartDate = DateTime.Now;
            ContractStartKm = 0;
            ContractStartHour = 0;
            InvoiceStartDate = DateTime.Now;
            ContractPeriodMonth = 60;
            ContractPeriodKm = 100000;
            ContractPeriodHour = 1000;
            ContractPeriodKmHour = 1;
            ContractEndDate = ContractStartDate.AddMonths(ContractPeriodMonth);
            InvoiceEndDate = InvoiceStartDate.AddMonths(ContractPeriodMonth);
            ContractEndKm = ContractStartKm + ContractPeriodKm;
            ContractEndHour = ContractStartHour + ContractPeriodHour;

        }
    }
    public class ContractPayment
    {
        public clsBaseListItem PaymentPeriod = new clsBaseListItem();
        public bool PaymentIsInBlock;
        public DateTime PaymentNextBlockStart = DateTime.MinValue;
        public DateTime PaymentNextBlockEnd = DateTime.MinValue;
        public string PaymentCollectionType;
        public string PaymentGroupingLevel;
        public int PaymentTerm;
        public ContractPayment()
        {
            PaymentPeriod.strValue1 = PaymentPeriodType.Monthly;
            PaymentCollectionType = SCPrime.Model.PaymentCollectionType.ESR;
            PaymentGroupingLevel = SCPrime.Model.PaymentGroupingType.Contract;
            PaymentTerm = 30;
        }
    }
    public class ContractCapital
    {
        public decimal CapitalStartAmount;
        public clsBaseListItem CapitalStartPayer = new clsBaseListItem();
        public decimal CapitalMonthAmount;
        public clsBaseListItem CapitalMonthPayer = new clsBaseListItem();
    }
    public class ContractCost
    {
        public clsBaseListItem CostBasis = new clsBaseListItem();
        public Decimal CostBasedOnService;
        public Decimal CostMonthBasis;
        public Decimal CostKmBasis;
        public Decimal CostPerKm;
        public ContractCost()
        {
            CostBasis.strValue1 = CostBasisType.KmOrHourWithLump;
        }
    }
    public class ContractExtraKm
    {
        public clsBaseListItem ExtraKmInvoicePeriod = new clsBaseListItem();
        public clsBaseListItem ExtraKmAccounting = new clsBaseListItem();
        public Decimal ExtraKmMaxDeviation;
        public Decimal ExtraKmLowAmount;
        public Decimal ExtraKmHighAmount;
        public Decimal ExtraKmInvoicedAmount;
        public ContractExtraKm()
        {
            ExtraKmInvoicePeriod.strValue1 = ExtraKmBillingPeriodType.None;
            ExtraKmAccounting.strValue1 = ExtraKmAccountingType.None;
            ExtraKmMaxDeviation = (Decimal)0.05;
        }
    }
    public class SubContractorContract
    {
        public int OID { get; set; }
         public clsBaseListItem SuplNo = new clsBaseListItem();
        //public clsBaseListItem SuplNo
        //{
        //    get
        //    {
        //        return SuplNo;
        //    }
        //    set
        //    {
        //        SuplNo = value;

        //    }

        //}
        public string SuplNoVal { get; set; }
        public string SuplName { get; set; }

        public String SubcontractNo { get; set; }
        public String Info { get; set; }
        public String Expl { get; set; }
        public DateTime DateLimit { get; set; }
        public int KmLimit { get; set; }
        public Decimal BuyPrice { get; set; }

        public bool isDeleted { get; set; }

        public static List<ObjTmp> getSuppliers()
        {
            List<ObjTmp> objs = new List<ObjTmp>();
            clsSqlFactory hSql = new clsSqlFactory();
            string sql = "Select suplno, name as suplname from supl order by name";

            hSql.NewCommand(sql);
            hSql.ExecuteReader();
            while (hSql.Read())
            {
                ObjTmp obj = new ObjTmp();

                var colId = hSql.Reader.GetOrdinal("suplno");
                if (!hSql.Reader.IsDBNull(colId))
                    obj.value = hSql.Reader.GetString(colId);

                colId = hSql.Reader.GetOrdinal("suplname");
                if (!hSql.Reader.IsDBNull(colId))
                    obj.text = hSql.Reader.GetString(colId);


                objs.Add(obj);
            }

            return objs;
        }

    }

    public class CollectiveContract
    {
        public int OID;
        public int ContractNo;
        public int VersionNo;
        public string ContractStatus;
        public String VIN;
        public String Info;
    }
    public class Contract
    {
        public int ContractOID { get; set; }
        public string ContractStatus = SCPrime.Model.ContractStatus.Model;
        public int ContractNo { get; set; }
        public int VersionNo { get; set; }
        public string ExtContractNo { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public DateTime LastInvoiceDate { get; set; }
        public DateTime NextInvoiceDate { get; set; }
        public clsBaseListItem SiteId = new clsBaseListItem();
        public clsBaseListItem CostCenter = new clsBaseListItem();
        public clsBaseListItem ValidWorkshopCode = new clsBaseListItem();
        public SCContractType ContractTypeOID = new SCContractType();
        public ContractCustomer ContractCustId = new ContractCustomer();
        public ContractCustomer InvoiceCustId { get; set; }
        public ContractEmployee RespSmanId { get; set; }
        public ContractEmployee CareSmanId { get; set; }
        public ContractVehicle VehiId = new ContractVehicle();
        public bool IsBodyIncl { get; set; }
        public bool IsTailLiftIncl { get; set; }
        public bool IsCoolingIncl { get; set; }
        public bool IsCraneIncl { get; set; }
        public ContractDate ContractDateData = new ContractDate();
        public clsBaseListItem TerminationType = new clsBaseListItem();
        public ContractPayment ContractPaymentData = new ContractPayment();
        public clsBaseListItem InvoiceSiteId;
        public bool IsManualInvoice { get; set; }
        public ContractCapital ContractCapitalData;
        public ContractCost ContractCostData = new ContractCost();
        public ContractExtraKm ContractExtraKmData = new ContractExtraKm();
        public ContractCustomer RiskCustId;
        public Decimal RiskLevel { get; set; }
        public clsBaseListItem RollingCode = new clsBaseListItem();
        public bool IsInvoiceDetail { get; set; }

        public List<SubContractorContract> SubContracts;
        public List<CollectiveContract> SelfContracts;

        public Contract()
        {
            //default values
            TerminationType.strValue1 = SCPrime.Model.ContractTerminationType.TimeBase;
            SiteId.strValue1 = new clsGlobalVariable().CurrentSiteId;

        }
        public bool saveSubcontractor(List<SubContractorContract> sccs, int contractOid, clsSqlFactory hSql)
        {
            bool bRet = true;
            foreach (SubContractorContract s in sccs)
            {

                string sql = "";
                //delete subcontractor
                if (s.isDeleted && (s.OID > 0))
                {
                    sql = "delete from ZSC_SubcontractorContract where OID = ?";
                    bRet = hSql.NewCommand(sql);
                    hSql.Com.Parameters.AddWithValue("OID", s.OID);
                    bRet = bRet && hSql.ExecuteNonQuery();
                }
                else 
                {
                    //insert new subcontractor
                    if (!(s.OID > 0) && !(s.isDeleted))
                    {
                        sql = "insert into ZSC_SubcontractorContract(ContractOID, SuplNo, SubContractNo, Created, Modified, Info, Expl, BuyPr, DateLimit, KMLimit) " +
                           " values (?,?,?,getdate(),getdate(),?,?,?,?,?) ";

                        bRet = hSql.NewCommand(sql);
                        hSql.Com.Parameters.AddWithValue("ContractOID", contractOid);
                        hSql.Com.Parameters.AddWithValue("SuplNo", s.SuplNoVal);
                        hSql.Com.Parameters.AddWithValue("SubContractNo", s.SubcontractNo);
                        hSql.Com.Parameters.AddWithValue("Info", s.Info);
                        hSql.Com.Parameters.AddWithValue("Expl", s.Expl);
                        hSql.Com.Parameters.AddWithValue("BuyPr", s.BuyPrice);
                        if (s.DateLimit != null && s.DateLimit > DateTime.MinValue)
                            hSql.Com.Parameters.AddWithValue("DateLimit", s.DateLimit);
                        else
                            hSql.Com.Parameters.AddWithValue("DateLimit", DBNull.Value);
                        hSql.Com.Parameters.AddWithValue("KMLimit", s.KmLimit);
                        bRet = bRet && hSql.ExecuteNonQuery();
                    }

                    //update
                    if (s.OID > 0)
                    {
                        sql = "update ZSC_SubcontractorContract set Modified = getdate(), ContractOID=?, SuplNo =?, SubContractNo =?, Info =?, Expl =?, BuyPr = ?, DateLimit =?, KMLimit =? where OID =? ";
                        bRet = hSql.NewCommand(sql);
                        hSql.Com.Parameters.AddWithValue("ContractOID", contractOid);
                        hSql.Com.Parameters.AddWithValue("SuplNo", s.SuplNoVal);
                        hSql.Com.Parameters.AddWithValue("SubContractNo", s.SubcontractNo);
                        hSql.Com.Parameters.AddWithValue("Info", s.Info);
                        hSql.Com.Parameters.AddWithValue("Expl", s.Expl);
                        hSql.Com.Parameters.AddWithValue("BuyPr", s.BuyPrice);
                        if (s.DateLimit != null && s.DateLimit > DateTime.MinValue)
                            hSql.Com.Parameters.AddWithValue("DateLimit", s.DateLimit);
                        else
                            hSql.Com.Parameters.AddWithValue("DateLimit", DBNull.Value);
                        hSql.Com.Parameters.AddWithValue("KMLimit", s.KmLimit);
                        hSql.Com.Parameters.AddWithValue("OID", s.OID);
                        bRet = bRet && hSql.ExecuteNonQuery();
                    }
                }

            }
            return bRet;

        }
        public bool saveContract()
        {
            bool bRet = true;
            bool bNewContract = false;
            clsSqlFactory hSql = new clsSqlFactory();
            try
            {
                string strSql = "";
                if (!(ContractNo > 0))
                {
                    bRet = hSql.NewCommand("select isnull(max(ContractNo),0)+1 from  ZSC_Contract  ");
                    bRet = bRet && hSql.ExecuteReader() && hSql.Read();
                    this.ContractNo = hSql.Reader.GetInt32(0);
                    VersionNo = 1;
                }

                //insert a new contract, only not null fields
                if (!(ContractOID > 0))
                {
                    bNewContract = true;
                    strSql = "insert into ZSC_Contract(ContractStatus, ContractNo, VersionNo, Created, Modified, SiteId, ContractTypeOID, ContractCustId, VehiId, ContractStartDate, InvoiceStartDate, ContractEndDate, InvoiceEndDate, CostBasis) " +
                        " values (?,?,?,getdate(),getdate(),?,?,?,?,?,?,?,?,?) ";
                    bRet = hSql.NewCommand(strSql);
                    hSql.Com.Parameters.AddWithValue("ContractStatus", ContractStatus);
                    hSql.Com.Parameters.AddWithValue("ContractNo", ContractNo);
                    hSql.Com.Parameters.AddWithValue("VersionNo", VersionNo);
                    hSql.Com.Parameters.AddWithValue("SiteId", SiteId.strValue1);
                    hSql.Com.Parameters.AddWithValue("ContractTypeOID", ContractTypeOID.OID);
                    hSql.Com.Parameters.AddWithValue("ContractCustId", ContractCustId.CustId);
                    hSql.Com.Parameters.AddWithValue("VehiId", VehiId.VehiId);
                    hSql.Com.Parameters.AddWithValue("ContractStartDate", ContractDateData.ContractStartDate);
                    hSql.Com.Parameters.AddWithValue("InvoiceStartDate", ContractDateData.InvoiceStartDate != null ? ContractDateData.InvoiceStartDate : DateTime.Now);
                    hSql.Com.Parameters.AddWithValue("ContractEndDate", ContractDateData.ContractEndDate);
                    hSql.Com.Parameters.AddWithValue("InvoiceEndDate", ContractDateData.InvoiceEndDate);
                    hSql.Com.Parameters.AddWithValue("CostBasis", ContractCostData.CostBasis.strValue1);
                    bRet = bRet && hSql.ExecuteNonQuery();
                    bRet = hSql.NewCommand("select max(OID) from  ZSC_Contract where ContractNo = ? and VersionNo = ? ");
                    hSql.Com.Parameters.AddWithValue("ContractNo", ContractNo);
                    hSql.Com.Parameters.AddWithValue("VersionNo", VersionNo);
                    bRet = bRet && hSql.ExecuteReader() && hSql.Read();
                    this.ContractOID = hSql.Reader.GetInt32(0);
                    //longdq03082018
                    if (SubContracts != null)
                    {
                        bRet = bRet && saveSubcontractor(SubContracts, this.ContractOID, hSql);
                    }
                }
                //update data
                if (ContractOID > 0)
                {
                    if (!bNewContract)
                    {
                        strSql = "update ZSC_Contract set Modified=getdate(), ContractStatus=?, SiteId=?, ContractTypeOID=?, ContractCustId=?, VehiId = ?, ContractStartDate=?,InvoiceStartDate=?, ContractEndDate=?, InvoiceEndDate=?, CostBasis=? where OID=?  ";
                        bRet = hSql.NewCommand(strSql);
                        hSql.Com.Parameters.AddWithValue("ContractStatus", ContractStatus);
                        hSql.Com.Parameters.AddWithValue("SiteId", SiteId.strValue1);
                        hSql.Com.Parameters.AddWithValue("ContractTypeOID", ContractTypeOID.OID);
                        hSql.Com.Parameters.AddWithValue("ContractCustId", ContractCustId.CustId);
                        hSql.Com.Parameters.AddWithValue("VehiId", VehiId.VehiId);
                        hSql.Com.Parameters.AddWithValue("ContractStartDate", ContractDateData.ContractStartDate);
                        hSql.Com.Parameters.AddWithValue("InvoiceStartDate", ContractDateData.InvoiceStartDate);
                        hSql.Com.Parameters.AddWithValue("ContractEndDate", ContractDateData.ContractEndDate);
                        hSql.Com.Parameters.AddWithValue("InvoiceEndDate", ContractDateData.InvoiceEndDate);
                        hSql.Com.Parameters.AddWithValue("CostBasis", ContractCostData.CostBasis.strValue1);
                        hSql.Com.Parameters.AddWithValue("OID", ContractOID);
                        bRet = bRet && hSql.ExecuteNonQuery();
                    }
                    strSql = "update ZSC_Contract set Modified=getdate(),ExtContractNo=?, CostCenter=?, ValidWorkshopCode=?, InvoiceCustId=?, RespSmanId=?, CareSmanId=?, IsBodyIncl=?, IsTailLiftIncl=?, IsCoolingIncl=?, IsCraneIncl=?," +
                        " ContractStartKm=?, ContractStartHour=?, ContractPeriodMonth=?, ContractPeriodKm=?, ContractPeriodHour=?, ContractPeriodKmHour=?, ContractEndKm=?, ContractEndHour=?, TerminationType=?, PaymentPeriod=?, " +
                        " PaymentIsInBlock=?, PaymentNextBlockStart=?, PaymentNextBlockEnd=?, PaymentCollectionType=?, PaymentGroupingLevel=?, PaymentTerm=?, InvoiceSiteId=?, IsManualInvoice=?, CapitalStartAmount=?, CapitalStartPayer=?," +
                        " CapitalMonthAmount=?, CapitalMonthPayer=?, CostBasedOnService=?, CostMonthBasis=?, CostKmBasis=?, CostPerKm=?, ExtraKmInvoicePeriod=?, ExtraKmAccounting=?, ExtraKmMaxDeviation=?, ExtraKmLowAmount=?, ExtraKmHighAmount=?," +
                        " RiskCustId=?, RiskLevel=?, RollingCode=?, IsInvoiceDetail=? where OID=?  ";
                    bRet = hSql.NewCommand(strSql);
                    if (ExtContractNo != null) hSql.Com.Parameters.AddWithValue("ExtContractNo", ExtContractNo); else hSql.Com.Parameters.AddWithValue("ExtContractNo", DBNull.Value);
                    if (CostCenter != null) hSql.Com.Parameters.AddWithValue("CostCenter", CostCenter.strValue1); else hSql.Com.Parameters.AddWithValue("CostCenter", DBNull.Value);
                    if (ValidWorkshopCode != null) hSql.Com.Parameters.AddWithValue("ValidWorkshopCode", ValidWorkshopCode.strValue1); else hSql.Com.Parameters.AddWithValue("ValidWorkshopCode", DBNull.Value);
                    if (InvoiceCustId != null) hSql.Com.Parameters.AddWithValue("InvoiceCustId", InvoiceCustId.CustId); else hSql.Com.Parameters.AddWithValue("InvoiceCustId", DBNull.Value);
                    if (RespSmanId != null) hSql.Com.Parameters.AddWithValue("RespSmanId", RespSmanId.SmanId); else hSql.Com.Parameters.AddWithValue("RespSmanId", DBNull.Value);
                    if (CareSmanId != null) hSql.Com.Parameters.AddWithValue("CareSmanId", CareSmanId.SmanId); else hSql.Com.Parameters.AddWithValue("CareSmanId", DBNull.Value);
                    hSql.Com.Parameters.AddWithValue("IsBodyIncl", IsBodyIncl);
                    hSql.Com.Parameters.AddWithValue("IsTailLiftIncl", IsTailLiftIncl);
                    hSql.Com.Parameters.AddWithValue("IsCoolingIncl", IsCoolingIncl);
                    hSql.Com.Parameters.AddWithValue("IsCraneIncl", IsCraneIncl);

                    if (ContractDateData != null)
                    {
                        hSql.Com.Parameters.AddWithValue("ContractStartKm", ContractDateData.ContractStartKm);
                        hSql.Com.Parameters.AddWithValue("ContractStartHour", ContractDateData.ContractStartHour);
                        hSql.Com.Parameters.AddWithValue("ContractPeriodMonth", ContractDateData.ContractPeriodMonth);
                        hSql.Com.Parameters.AddWithValue("ContractPeriodKm", ContractDateData.ContractPeriodKm);
                        hSql.Com.Parameters.AddWithValue("ContractPeriodHour", ContractDateData.ContractPeriodHour);
                        hSql.Com.Parameters.AddWithValue("ContractPeriodKmHour", ContractDateData.ContractPeriodKmHour);
                        hSql.Com.Parameters.AddWithValue("ContractEndKm", ContractDateData.ContractEndKm);
                        hSql.Com.Parameters.AddWithValue("ContractEndHour", ContractDateData.ContractEndHour);
                    }
                    else
                    {
                        hSql.Com.Parameters.AddWithValue("ContractStartKm", DBNull.Value);
                        hSql.Com.Parameters.AddWithValue("ContractStartHour", DBNull.Value);
                        hSql.Com.Parameters.AddWithValue("ContractPeriodMonth", DBNull.Value);
                        hSql.Com.Parameters.AddWithValue("ContractPeriodKm", DBNull.Value);
                        hSql.Com.Parameters.AddWithValue("ContractPeriodHour", DBNull.Value);
                        hSql.Com.Parameters.AddWithValue("ContractPeriodKmHour", DBNull.Value);
                        hSql.Com.Parameters.AddWithValue("ContractEndKm", DBNull.Value);
                        hSql.Com.Parameters.AddWithValue("ContractEndHour", DBNull.Value);
                    }
                    if (TerminationType != null) hSql.Com.Parameters.AddWithValue("TerminationType", TerminationType.strValue1); else hSql.Com.Parameters.AddWithValue("TerminationType", DBNull.Value);
                    if (ContractPaymentData != null)
                    {
                        hSql.Com.Parameters.AddWithValue("PaymentPeriod", ContractPaymentData.PaymentPeriod.strValue1);
                        hSql.Com.Parameters.AddWithValue("PaymentIsInBlock", ContractPaymentData.PaymentIsInBlock);
                        if (ContractPaymentData.PaymentNextBlockStart != DateTime.MinValue)
                            hSql.Com.Parameters.AddWithValue("PaymentNextBlockStart", ContractPaymentData.PaymentNextBlockStart);
                        else hSql.Com.Parameters.AddWithValue("PaymentNextBlockStart", DBNull.Value);
                        if (ContractPaymentData.PaymentNextBlockEnd != DateTime.MinValue)
                            hSql.Com.Parameters.AddWithValue("PaymentNextBlockEnd", ContractPaymentData.PaymentNextBlockEnd);
                        else hSql.Com.Parameters.AddWithValue("PaymentNextBlockEnd", DBNull.Value);
                        hSql.Com.Parameters.AddWithValue("PaymentCollectionType", ContractPaymentData.PaymentCollectionType);
                        hSql.Com.Parameters.AddWithValue("PaymentGroupingLevel", ContractPaymentData.PaymentGroupingLevel);
                        hSql.Com.Parameters.AddWithValue("PaymentTerm", ContractPaymentData.PaymentTerm);
                    }
                    else
                    {
                        hSql.Com.Parameters.AddWithValue("PaymentPeriod", DBNull.Value);
                        hSql.Com.Parameters.AddWithValue("PaymentIsInBlock", DBNull.Value);
                        hSql.Com.Parameters.AddWithValue("PaymentNextBlockStart", DBNull.Value);
                        hSql.Com.Parameters.AddWithValue("PaymentNextBlockEnd", DBNull.Value);
                        hSql.Com.Parameters.AddWithValue("PaymentCollectionType", DBNull.Value);
                        hSql.Com.Parameters.AddWithValue("PaymentGroupingLevel", DBNull.Value);
                        hSql.Com.Parameters.AddWithValue("PaymentTerm", DBNull.Value);
                    }
                    if (InvoiceSiteId != null) hSql.Com.Parameters.AddWithValue("InvoiceSiteId", InvoiceSiteId.strValue1); else hSql.Com.Parameters.AddWithValue("InvoiceSiteId", DBNull.Value);
                    hSql.Com.Parameters.AddWithValue("IsManualInvoice", IsManualInvoice);
                    if (ContractCapitalData != null)
                    {
                        hSql.Com.Parameters.AddWithValue("CapitalStartAmount", ContractCapitalData.CapitalStartAmount);
                        hSql.Com.Parameters.AddWithValue("CapitalStartPayer", ContractCapitalData.CapitalStartPayer.strValue1);
                        hSql.Com.Parameters.AddWithValue("CapitalMonthAmount", ContractCapitalData.CapitalMonthAmount);
                        hSql.Com.Parameters.AddWithValue("CapitalMonthPayer", ContractCapitalData.CapitalMonthPayer.strValue1);
                    }
                    else
                    {
                        hSql.Com.Parameters.AddWithValue("CapitalStartAmount", DBNull.Value);
                        hSql.Com.Parameters.AddWithValue("CapitalStartPayer", DBNull.Value);
                        hSql.Com.Parameters.AddWithValue("CapitalMonthAmount", DBNull.Value);
                        hSql.Com.Parameters.AddWithValue("CapitalMonthPayer", DBNull.Value);
                    }
                    if (ContractCostData != null)
                    {
                        hSql.Com.Parameters.AddWithValue("CostBasedOnService", ContractCostData.CostBasedOnService);
                        hSql.Com.Parameters.AddWithValue("CostMonthBasis", ContractCostData.CostMonthBasis);
                        hSql.Com.Parameters.AddWithValue("CostKmBasis", ContractCostData.CostKmBasis);
                        hSql.Com.Parameters.AddWithValue("CostPerKm", ContractCostData.CostPerKm);
                    }
                    else
                    {
                        hSql.Com.Parameters.AddWithValue("CostBasedOnService", DBNull.Value);
                        hSql.Com.Parameters.AddWithValue("CostMonthBasis", DBNull.Value);
                        hSql.Com.Parameters.AddWithValue("CostKmBasis", DBNull.Value);
                        hSql.Com.Parameters.AddWithValue("CostPerKm", DBNull.Value);
                    }
                    if (ContractExtraKmData != null)
                    {
                        hSql.Com.Parameters.AddWithValue("ExtraKmInvoicePeriod", ContractExtraKmData.ExtraKmInvoicePeriod.strValue1);
                        hSql.Com.Parameters.AddWithValue("ExtraKmAccounting", ContractExtraKmData.ExtraKmAccounting.strValue1);
                        hSql.Com.Parameters.AddWithValue("ExtraKmMaxDeviation", ContractExtraKmData.ExtraKmMaxDeviation);
                        hSql.Com.Parameters.AddWithValue("ExtraKmLowAmount", ContractExtraKmData.ExtraKmLowAmount);
                        hSql.Com.Parameters.AddWithValue("ExtraKmHighAmount", ContractExtraKmData.ExtraKmHighAmount);
                    }
                    else
                    {
                        hSql.Com.Parameters.AddWithValue("ExtraKmInvoicePeriod", DBNull.Value);
                        hSql.Com.Parameters.AddWithValue("ExtraKmAccounting", DBNull.Value);
                        hSql.Com.Parameters.AddWithValue("ExtraKmMaxDeviation", DBNull.Value);
                        hSql.Com.Parameters.AddWithValue("ExtraKmLowAmount", DBNull.Value);
                        hSql.Com.Parameters.AddWithValue("ExtraKmHighAmount", DBNull.Value);
                    }
                    if (RiskCustId != null) hSql.Com.Parameters.AddWithValue("RiskCustId", RiskCustId.CustId); else hSql.Com.Parameters.AddWithValue("RiskCustId", DBNull.Value);
                    hSql.Com.Parameters.AddWithValue("RiskLevel", RiskLevel);
                    if (RollingCode != null) hSql.Com.Parameters.AddWithValue("RollingCode", RollingCode.strValue1); else hSql.Com.Parameters.AddWithValue("RollingCode", DBNull.Value);
                    hSql.Com.Parameters.AddWithValue("IsInvoiceDetail", IsInvoiceDetail);

                    hSql.Com.Parameters.AddWithValue("OID", ContractOID);
                    bRet = bRet && hSql.ExecuteNonQuery();

                    //longdq 02082018
                    if (SubContracts != null)
                    {
                        bRet = bRet && saveSubcontractor(SubContracts, this.ContractOID, hSql);
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

        //longdq added
        public int countContract(int ContractCustId, int VehiId)
        {
            int result = -1;

            clsSqlFactory hSql = new clsSqlFactory();
            string strSql = "select COUNT(1) as count from dbo.ZSC_Contract a where a.ContractCustId = ? and a.VehiId = ?";
            try
            {

                hSql.NewCommand(strSql);
                //hSql.NewCommand("select a.BuyPr, a.DateLimit, a.Expl, a.Info, a.KMLimit, a.OID, a.SubContractNo, a.SuplNo, b.Name as SuplName from ZSC_SubcontractorContract a left join SUPL b on a.SUPLNO=b.SUPLNO where ContractOID=? ");
                hSql.Com.Parameters.AddWithValue("ContractCustId", ContractCustId);
                hSql.Com.Parameters.AddWithValue("VehiId", VehiId);
                hSql.ExecuteReader();
                while (hSql.Read())
                {
                    //  SubContractorContract sc = new SubContractorContract();
                    result = hSql.Reader.GetInt32(0);

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
            return result;
        }
        public bool loadDetail()
        {
            bool bRet = true;
            clsSqlFactory hSql = new clsSqlFactory();
            try
            {
                //sub contracts
                SubContracts = new List<SubContractorContract>();
                hSql.NewCommand("select a.BuyPr, a.DateLimit, a.Expl, a.Info, a.KMLimit, a.OID, a.SubContractNo, a.SuplNo, b.Name as SuplName from ZSC_SubcontractorContract a left join SUPL b on a.SUPLNO=b.SUPLNO where ContractOID=? ");
                hSql.Com.Parameters.Add("ContractOID", this.ContractOID);
                hSql.ExecuteReader();
                while (hSql.Read())
                {
                    SubContractorContract sc = new SubContractorContract();
                    int colId = hSql.Reader.GetOrdinal("OID");
                    if (!hSql.Reader.IsDBNull(colId)) sc.OID = hSql.Reader.GetInt32(colId);
                    colId = hSql.Reader.GetOrdinal("SuplNo");
                    if (!hSql.Reader.IsDBNull(colId))
                    {
                        sc.SuplNo.strValue1 = hSql.Reader.GetString(colId);
                        sc.SuplNoVal = hSql.Reader.GetString(colId);

                        colId = hSql.Reader.GetOrdinal("SuplName");
                        if (!hSql.Reader.IsDBNull(colId))
                        {
                            sc.SuplNo.strText = hSql.Reader.GetString(colId);
                            sc.SuplName = hSql.Reader.GetString(colId);
                        }
                    }
                    colId = hSql.Reader.GetOrdinal("BuyPr");
                    if (!hSql.Reader.IsDBNull(colId)) sc.BuyPrice = hSql.Reader.GetDecimal(colId);
                    colId = hSql.Reader.GetOrdinal("DateLimit");
                    if (!hSql.Reader.IsDBNull(colId)) sc.DateLimit = hSql.Reader.GetDateTime(colId);
                    colId = hSql.Reader.GetOrdinal("Expl");
                    if (!hSql.Reader.IsDBNull(colId)) sc.Expl = hSql.Reader.GetString(colId);
                    colId = hSql.Reader.GetOrdinal("Info");
                    if (!hSql.Reader.IsDBNull(colId)) sc.Info = hSql.Reader.GetString(colId);
                    colId = hSql.Reader.GetOrdinal("KMLimit");
                    if (!hSql.Reader.IsDBNull(colId)) sc.KmLimit = hSql.Reader.GetInt32(colId);
                    colId = hSql.Reader.GetOrdinal("SubContractNo");
                    if (!hSql.Reader.IsDBNull(colId)) sc.SubcontractNo = hSql.Reader.GetString(colId);

                    SubContracts.Add(sc);
                }

                //self contracts
                SelfContracts = new List<CollectiveContract>();
                hSql.NewCommand("select  a.OID, a.Info,b.ContractNo,b.VersionNo, b.ContractStatus,c.SERIALNO from ZSC_ContractCollective a , ZSC_Contract b, VEHI c where a.ContractOID=? and a.DetailContractOID=b.OID and b.VEHIID=c.VEHIID ");
                hSql.Com.Parameters.Add("ContractOID", this.ContractOID);
                hSql.ExecuteReader();
                while (hSql.Read())
                {
                    CollectiveContract sc = new CollectiveContract();
                    int colId = hSql.Reader.GetOrdinal("OID");
                    if (!hSql.Reader.IsDBNull(colId)) sc.OID = hSql.Reader.GetInt32(colId);

                    colId = hSql.Reader.GetOrdinal("Info");
                    if (!hSql.Reader.IsDBNull(colId)) sc.Info = hSql.Reader.GetString(colId);
                    colId = hSql.Reader.GetOrdinal("ContractNo");
                    if (!hSql.Reader.IsDBNull(colId)) sc.ContractNo = hSql.Reader.GetInt32(colId);
                    colId = hSql.Reader.GetOrdinal("VersionNo");
                    if (!hSql.Reader.IsDBNull(colId)) sc.VersionNo = hSql.Reader.GetInt32(colId);
                    colId = hSql.Reader.GetOrdinal("ContractStatus");
                    if (!hSql.Reader.IsDBNull(colId)) sc.ContractStatus = hSql.Reader.GetString(colId);
                    colId = hSql.Reader.GetOrdinal("SERIALNO");
                    if (!hSql.Reader.IsDBNull(colId)) sc.VIN = hSql.Reader.GetString(colId);
                    SelfContracts.Add(sc);
                }
                if (this.VehiId != null)
                {
                    bRet = VehiId.loadDynFields(hSql);
                    bRet = VehiId.loadMileages(hSql);
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
                hSql.Close();
            }
            return bRet;
        }

        //longdq add 01082018
        public static bool checkContractVariant(int ContractCustId)
        {
            bool ret = false;

            clsSqlFactory hSql = new clsSqlFactory();
            string strSql = "select COUNT(distinct(versionNo)) as count from dbo.ZSC_Contract a where a.ContractCustId = ?";
            try
            {

                hSql.NewCommand(strSql);
                hSql.Com.Parameters.AddWithValue("ContractCustId", ContractCustId);
                hSql.ExecuteReader();
                while (hSql.Read())
                {
                    //  SubContractorContract sc = new SubContractorContract();
                    int result = hSql.Reader.GetInt32(0);
                    if (result > 0)
                        ret = true;
                    else
                        ret = false;

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


            return ret;

        }
        //ThuyetLV Add
        public String Status
        {
            get
            {
                if (ContractStatus != null)
                {
                    switch (ContractStatus)
                    {
                        case SCPrime.Model.ContractStatus.Model: return SCPrime.Model.ContractStatus.ModelText;
                        case SCPrime.Model.ContractStatus.Offer: return SCPrime.Model.ContractStatus.OfferText;
                        case SCPrime.Model.ContractStatus.New: return SCPrime.Model.ContractStatus.NewText;
                        case SCPrime.Model.ContractStatus.Waiting: return SCPrime.Model.ContractStatus.WaitingText;
                        case SCPrime.Model.ContractStatus.Active: return SCPrime.Model.ContractStatus.ActiveText;
                        case SCPrime.Model.ContractStatus.OnControl: return SCPrime.Model.ContractStatus.OnControlText;
                        case SCPrime.Model.ContractStatus.Closed: return SCPrime.Model.ContractStatus.ClosedText;
                        case SCPrime.Model.ContractStatus.Deactivated: return SCPrime.Model.ContractStatus.DeactivatedText;
                    }
                }
                return "";
            }

            set
            {
                ContractStatus = null;
            }
        }

        //
        public String ResponsibleSite
        {
            get
            {
                if (SiteId != null)
                {
                    return SiteId.strValue1;
                }
                return "";
            }

            set
            {
                SiteId = null;
            }
        }

        public String ContractTypeName
        {
            get
            {
                if (ContractTypeOID != null)
                {
                    return ContractTypeOID.Name;
                }
                return "";
            }

            set
            {
                ContractTypeOID = null;
            }
        }

        public int InvCustNo
        {
            get
            {
                if (InvoiceCustId != null)
                {
                    return InvoiceCustId.CustId;
                }
                return -1;
            }

            set
            {
                InvoiceCustId = null;
            }
        }

        public String InvCustPhone
        {
            get
            {
                if (InvoiceCustId != null)
                {
                    return InvoiceCustId.Phone;
                }
                return "";
            }

            set
            {
                InvoiceCustId = null;
            }
        }

        public int CustNo
        {
            get
            {
                if (ContractCustId != null)
                {
                    return ContractCustId.CustId;
                }
                return -1;
            }

            set
            {
                ContractCustId = null;
            }
        }
        public string CustName
        {
            get
            {
                if (ContractCustId != null)
                {
                    return ContractCustId.Name;
                }
                return "";
            }

            set
            {
                ContractCustId = null;
            }
        }
        public String CustPhone
        {
            get
            {
                if (ContractCustId != null)
                {
                    return ContractCustId.Phone;
                }
                return "";
            }

            set
            {
                ContractCustId = null;
            }
        }

        public String InvCustName
        {
            get
            {
                if (InvoiceCustId != null)
                {
                    return InvoiceCustId.Name;
                }
                return "";
            }

            set
            {
                InvoiceCustId = null;
            }
        }


        //
        public string VehiLicNo
        {
            get
            {
                if (VehiId != null)
                {
                    return VehiId.LicenseNo;
                }
                return "";
            }

            set
            {
                VehiId = null;
            }
        }
        public string VIN
        {
            get
            {
                if (VehiId != null)
                {
                    return VehiId.VIN;
                }
                return "";
            }

            set
            {
                VehiId = null;
            }
        }
        //ContractDate
        public DateTime ContractStartDate
        {
            get
            {
                if (ContractDateData != null)
                {
                    return ContractDateData.ContractStartDate;
                }
                return DateTime.Now;
            }

            set
            {
                ContractDateData = null;
            }
        }

        public DateTime ContractEndDate
        {
            get
            {
                if (ContractDateData != null)
                {
                    return ContractDateData.ContractStartDate;
                }
                return DateTime.Now;
            }

            set
            {
                ContractDateData = null;
            }
        }

        public int ContractPeriodKm
        {
            get
            {
                if (ContractDateData != null)
                {
                    return ContractDateData.ContractPeriodKm;
                }
                return -1;
            }

            set
            {
                ContractDateData = null;
            }
        }

        public int ContractPeriodHr
        {
            get
            {
                if (ContractDateData != null)
                {
                    return ContractDateData.ContractPeriodHour;
                }
                return -1;
            }

            set
            {
                ContractDateData = null;
            }
        }

        //
        public string KmOrHr
        {
            get
            {
                return "km";
            }

            set
            {
                ContractDateData = null;
            }
        }
        //
        public string getTerminationType
        {
            get
            {
                if (TerminationType != null)
                {
                    return TerminationType.strValue1;
                }
                return "";
            }

            set
            {
                TerminationType = null;
            }
        }

        //ContractPaymentData
        public string PaymentPeriod
        {
            get
            {
                if (ContractPaymentData != null)
                {
                    return ContractPaymentData.PaymentPeriod.strValue1;
                }
                return "";
            }

            set
            {
                ContractPaymentData = null;
            }
        }

        public bool PaymentInBlock
        {
            get
            {
                if (ContractPaymentData != null)
                {
                    return ContractPaymentData.PaymentIsInBlock;
                }
                return false;
            }

            set
            {
                ContractPaymentData = null;
            }
        }

        public DateTime PaymentNextBlockStart
        {
            get
            {
                if (ContractPaymentData != null)
                {
                    return ContractPaymentData.PaymentNextBlockStart;
                }
                return DateTime.Now;
            }

            set
            {
                ContractPaymentData = null;
            }
        }
        public DateTime PaymentNextBlockEnd
        {
            get
            {
                if (ContractPaymentData != null)
                {
                    return ContractPaymentData.PaymentNextBlockEnd;
                }
                return DateTime.Now;
            }

            set
            {
                ContractPaymentData = null;
            }
        }
        public string PaymentCollecType
        {
            get
            {
                if (ContractPaymentData != null)
                {
                    return ContractPaymentData.PaymentCollectionType;
                }
                return "";
            }

            set
            {
                ContractPaymentData = null;
            }
        }

        //Inv
        public string InvSites
        {
            get
            {
                if (InvoiceSiteId != null)
                {
                    return InvoiceSiteId.strValue1;
                }
                return "";
            }

            set
            {
                ContractPaymentData = null;
            }
        }
        //MileageReg
        public string LastMileDate
        {
            get
            {
                return "";
            }

            set
            {
                ContractPaymentData = null;
            }
        }
        public string LastMile
        {
            get
            {
                return "";
            }

            set
            {
                ContractPaymentData = null;
            }
        }
        public static List<clsBaseListItem> getCostCenter()
        {
            List<clsBaseListItem> Result = new List<clsBaseListItem>();
            clsSqlFactory hSql = new clsSqlFactory();
            try
            {

                String strSql = " select C1 as Code, C2 as Name from ALL_CORW where CODAID='OSASTOT' and _UNITID=? ";
                hSql.NewCommand(strSql);
                hSql.Com.Parameters.AddWithValue("_UNITID", new clsGlobalVariable().CurrentSiteId);
                hSql.ExecuteReader();
                while (hSql.Read())
                {
                    clsBaseListItem item = new clsBaseListItem();
                    //item.nValue1 = hSql.Reader.GetInt32(0);
                    item.strValue1 = hSql.Reader.GetString(0);
                    item.strText = hSql.Reader.GetString(1);

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

        public static List<clsBaseListItem> getValidWorkshop()
        {
            List<clsBaseListItem> Result = new List<clsBaseListItem>();
            clsSqlFactory hSql = new clsSqlFactory();
            try
            {

                String strSql = "select C1 as Code, C2 as Name from ALL_CORW where CODAID='ZSCVALIDWS' and _UNITID=?";
                hSql.NewCommand(strSql);
                hSql.Com.Parameters.AddWithValue("_UNITID", new clsGlobalVariable().CurrentSiteId);
                hSql.ExecuteReader();
                while (hSql.Read())
                {
                    clsBaseListItem item = new clsBaseListItem();
                    //item.nValue1 = hSql.Reader.GetInt32(0);
                    item.strValue1 = hSql.Reader.GetString(0);
                    item.strText = hSql.Reader.GetString(1);

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

    //ThuyetLV Add
    public class MileageReg
    {
        public DateTime Created { get; set; }
        public string Mileage { get; set; }
        public string InputType { get; set; }
        public string Info { get; set; }
        public string Deviation { get; set; }

        public string CustomInputType
        {
            get
            {
                if (this.InputType == "1")
                {
                    return "Manual";
                }
                else
                {
                    return "From service history";
                }
            }

            set
            {
                this.InputType = null;
            }
        }
    }



}

