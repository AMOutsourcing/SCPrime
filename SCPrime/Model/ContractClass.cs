using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using nsBaseClass;

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
        public int VehiId;
        public string LicenseNo;
        public string VIN;
        public string Make;
        public string Model;
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
    public  class Contract
    {
        public int ContractOID;
        public string ContractStatus = SCPrime.Model.ContractStatus.Model;
        public int ContractNo;
        public int VersionNo;
        public string ExtContractNo;
        public DateTime Created;
        public DateTime Modified;
        public DateTime LastInvoiceDate;
        public DateTime NextInvoiceDate;
        public clsBaseListItem SiteId = new clsBaseListItem();
        public clsBaseListItem CostCenter = new clsBaseListItem();
        public clsBaseListItem ValidWorkshopCode = new clsBaseListItem();
        public SCContractType ContractTypeOID = new SCContractType();
        public ContractCustomer ContractCustId = new ContractCustomer();
        public ContractCustomer InvoiceCustId;
        public ContractEmployee RespSmanId;
        public ContractEmployee CareSmanId;
        public ContractVehicle VehiId = new ContractVehicle();
        public bool IsBodyIncl;
        public bool IsTailLiftIncl;
        public bool IsCoolingIncl;
        public bool IsCraneIncl;
        public ContractDate ContractDateData = new ContractDate();
        public clsBaseListItem TerminationType = new clsBaseListItem();
        public ContractPayment ContractPaymentData = new ContractPayment();
        public clsBaseListItem InvoiceSiteId;
        public bool IsManualInvoice;
        public ContractCapital ContractCapitalData;
        public ContractCost ContractCostData = new ContractCost();
        public ContractExtraKm ContractExtraKmData = new ContractExtraKm();
        public ContractCustomer RiskCustId;
        public Decimal RiskLevel;
        public clsBaseListItem RollingCode = new clsBaseListItem();
        public bool IsInvoiceDetail;

        public Contract()
        {
            //default values
            TerminationType.strValue1 = SCPrime.Model.ContractTerminationType.TimeBase;
            SiteId.strValue1 = new clsGlobalVariable().CurrentSiteId;

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
                    strSql = "insert into ZSC_Contract(ContractStatus, ContractNo, VersionNo, Created, Modified, SiteId, ContractTypeOID, ContractCustId, VehiId, ContractStartDate, InvoiceStartDate, ContractEndDate, InvoiceEndDate, CostBasis) "+
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
                    hSql.Com.Parameters.AddWithValue("InvoiceStartDate", ContractDateData.InvoiceStartDate);
                    hSql.Com.Parameters.AddWithValue("ContractEndDate", ContractDateData.ContractEndDate);
                    hSql.Com.Parameters.AddWithValue("InvoiceEndDate", ContractDateData.InvoiceEndDate);
                    hSql.Com.Parameters.AddWithValue("CostBasis", ContractCostData.CostBasis.strValue1);
                    bRet = bRet && hSql.ExecuteNonQuery();
                    bRet = hSql.NewCommand("select max(OID) from  ZSC_Contract where ContractNo = ? and VersionNo = ? ");
                    hSql.Com.Parameters.AddWithValue("ContractNo", ContractNo);
                    hSql.Com.Parameters.AddWithValue("VersionNo", VersionNo);
                    bRet = bRet && hSql.ExecuteReader() && hSql.Read();
                    this.ContractOID  = hSql.Reader.GetInt32(0);
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
                    strSql = "update ZSC_Contract set Modified=getdate(),ExtContractNo=?, CostCenter=?, ValidWorkshopCode=?, InvoiceCustId=?, RespSmanId=?, CareSmanId=?, IsBodyIncl=?, IsTailLiftIncl=?, IsCoolingIncl=?, IsCraneIncl=?,"+
                        " ContractStartKm=?, ContractStartHour=?, ContractPeriodMonth=?, ContractPeriodKm=?, ContractPeriodHour=?, ContractPeriodKmHour=?, ContractEndKm=?, ContractEndHour=?, TerminationType=?, PaymentPeriod=?, "+
                        " PaymentIsInBlock=?, PaymentNextBlockStart=?, PaymentNextBlockEnd=?, PaymentCollectionType=?, PaymentGroupingLevel=?, PaymentTerm=?, InvoiceSiteId=?, IsManualInvoice=?, CapitalStartAmount=?, CapitalStartPayer=?,"+
                        " CapitalMonthAmount=?, CapitalMonthPayer=?, CostBasedOnService=?, CostMonthBasis=?, CostKmBasis=?, CostPerKm=?, ExtraKmInvoicePeriod=?, ExtraKmAccounting=?, ExtraKmMaxDeviation=?, ExtraKmLowAmount=?, ExtraKmHighAmount=?," +
                        " RiskCustId=?, RiskLevel=?, RollingCode=?, IsInvoiceDetail=? where OID=?  ";
                    bRet = hSql.NewCommand(strSql);
                    if (ExtContractNo!=null) hSql.Com.Parameters.AddWithValue("ExtContractNo", ExtContractNo); else hSql.Com.Parameters.AddWithValue("ExtContractNo", DBNull.Value);
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
                        if (ContractPaymentData.PaymentNextBlockEnd!= DateTime.MinValue)
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
                    if (InvoiceSiteId!= null) hSql.Com.Parameters.AddWithValue("InvoiceSiteId", InvoiceSiteId.strValue1); else hSql.Com.Parameters.AddWithValue("InvoiceSiteId", DBNull.Value);
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
                    if (RiskCustId!=null) hSql.Com.Parameters.AddWithValue("RiskCustId", RiskCustId.CustId); else hSql.Com.Parameters.AddWithValue("RiskCustId", DBNull.Value);
                    hSql.Com.Parameters.AddWithValue("RiskLevel", RiskLevel);
                    if (RollingCode!=null) hSql.Com.Parameters.AddWithValue("RollingCode", RollingCode.strValue1); else hSql.Com.Parameters.AddWithValue("RollingCode", DBNull.Value);
                    hSql.Com.Parameters.AddWithValue("IsInvoiceDetail", IsInvoiceDetail);
                    
                    hSql.Com.Parameters.AddWithValue("OID", ContractOID);
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
}
