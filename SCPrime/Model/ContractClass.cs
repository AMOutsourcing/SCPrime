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
        public DateTime InvocieStartDate;
        public int ContractPeriodMonth;
        public int ContractPeriodKm;
        public int ContractPeriodHour;
        public int ContractPeriodKmHour;
        public DateTime ContractEndDate;
        public int ContractEndKm;
        public int ContractEndHour;
        public DateTime InvoiceEndDate;
    }
    public class ContractPayment
    {
        public string PaymentPeriod;
        public bool PaymentIsInBlock;
        public DateTime PaymentNextBlockStart;
        public DateTime PaymentNextBlockEnd;
        public string PaymentCollectionType;
        public string PaymentGroupingLevel;
        public int PaymentTerm;
    }
    public class ContractCapital
    {
        public decimal CapitalStartAmount;
        public clsBaseListItem CapitalStartPayer = new clsBaseListItem();
        public decimal CapitalMonthAmount;
    }
    public class ContractCost
    {
        public clsBaseListItem CostBasis = new clsBaseListItem();
        public decimal CostBasedOnService;
        public decimal CostMonthBasis;
        public decimal CostKmBasis;
        public decimal CostPerKm;
    }
    public class ContractExtraKm
    {
        public clsBaseListItem ExtraKmInvoicePeriod = new clsBaseListItem();
        public clsBaseListItem ExtraKmAccounting = new clsBaseListItem();
        public decimal ExtraKmMaxDeviation;
        public decimal ExtraKmLowAmount;
        public decimal ExtraKmHighAmount;
        public decimal ExtraKmInvoicedAmount;
    }
    public  class Contract
    {
        public int ContractOID;
        public string ContractStatus;
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
        public SCContractType ContractTypeOID;
        public ContractCustomer ContractCustId;
        public ContractCustomer InvoiceCustId;
        public ContractEmployee RespSmanId;
        public ContractEmployee CareSmanId;
        public ContractVehicle VehiId;
        public bool IsBodyIncl;
        public bool IsTailLiftIncl;
        public bool IsCoolingIncl;
        public bool IsCraneIncl;
        public ContractDate ContractDateData;
        public string TerminationType;
        public ContractPayment ContractPaymentData;
        public clsBaseListItem InvoiceSiteId;
        public bool IsManualInvoice;
        public ContractCapital ContractCapitalData;
        public ContractCost ContractCostData;
        public ContractExtraKm ContractExtraKmData;
        public ContractCustomer RiskCustId;
        public decimal RiskLevel;
        public clsBaseListItem RollingCode;
        public bool IsInvoiceDetail;
    }
}
