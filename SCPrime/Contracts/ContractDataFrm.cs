using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SCPrime.Model;
using CashRegPrime;
using nsBaseClass;

namespace SCPrime.Contracts
{
    public partial class ContractDataFrm : UserControl
    {
        //Singleton
        private static ContractDataFrm _instance;

        public static ContractDataFrm getInstance()
        {
            if (ContractDataFrm._instance == null || ContractDataFrm._instance.IsDisposed)
            {
                ContractDataFrm._instance = new ContractDataFrm();
            }
            return ContractDataFrm._instance;
        }


        //
        private Contract contract;
        public void setContract(Contract objContract)
        {
            this.contract = objContract;
        }

        SCBase sCBase;
        //
        public ContractDataFrm()
        {
            InitializeComponent();

            sCBase = new SCBase();

            txtStartDate.Format = DateTimePickerFormat.Custom;
            txtStartDate.CustomFormat = "yyyy-MM-dd";

            txtEndDate.Format = DateTimePickerFormat.Custom;
            txtEndDate.CustomFormat = "yyyy-MM-dd";

            txtStartInvoice.Format = DateTimePickerFormat.Custom;
            txtStartInvoice.CustomFormat = "yyyy-MM-dd";

            txtEndInvoice.Format = DateTimePickerFormat.Custom;
            txtEndInvoice.CustomFormat = "yyyy-MM-dd";

            //Termination 
            List<ObjTmp> listTermination = new List<ObjTmp>();
            ObjTmp termination1 = new ObjTmp();
            termination1.strValue1 = "T";
            termination1.strText = "Time";
            listTermination.Add(termination1);

            ObjTmp termination2 = new ObjTmp();
            termination2.strValue1 = "K";
            termination2.strText = "Kilometer or hour";
            listTermination.Add(termination2);
            cbTemType.DataSource = listTermination;
            cbTemType.ValueMember = "strValue1";
            cbTemType.DisplayMember = "strText";


            //Payment period  
            List<ObjTmp> listPayment = new List<ObjTmp>();
            ObjTmp payment1 = new ObjTmp();
            payment1.strValue1 = "M";
            payment1.strText = "Monthly";
            listPayment.Add(payment1);
            cbPayPeriod.DataSource = listPayment;
            cbPayPeriod.ValueMember = "strValue1";
            cbPayPeriod.DisplayMember = "strText";


            //Invoicing site 
            List<clsBaseListItem> listSite = sCBase.getAMSites();
            List<ObjTmp> lstSites = new List<ObjTmp>(listSite.Count);
            foreach (clsBaseListItem site in listSite)
            {
                lstSites.Add(new ObjTmp(site.strValue1, site.strText));
            }
            cbInvoiceSite.DataSource = lstSites;
            cbInvoiceSite.ValueMember = "id";
            cbInvoiceSite.DisplayMember = "text";

            //Cost basis
            List<ObjTmp> listCostBs = new List<ObjTmp>();
            ObjTmp cost1 = new ObjTmp();
            cost1.strValue1 = "M";
            cost1.strText = "Monthly cost fix";
            listCostBs.Add(cost1);

            ObjTmp cost2 = new ObjTmp();
            cost2.strValue1 = "K";
            cost2.strText = "Km or hour cost";
            listCostBs.Add(cost2);

            ObjTmp cost3 = new ObjTmp();
            cost3.strValue1 = "L";
            cost3.strText = "Km or hour cost with lump amount";
            listCostBs.Add(cost3);
            cbCostBassis.DataSource = listCostBs;
            cbCostBassis.ValueMember = "strValue1";
            cbCostBassis.DisplayMember = "strText";

            //Set Billing period
            List<ObjTmp> listBiling = new List<ObjTmp>();
            ObjTmp billing1 = new ObjTmp();
            billing1.strValue1 = "M";
            billing1.strText = "Monthly";
            listBiling.Add(billing1);

            ObjTmp billing2 = new ObjTmp();
            billing2.strValue1 = "H";
            billing2.strText = "Half year";
            listBiling.Add(billing2);

            ObjTmp billing3 = new ObjTmp();
            billing3.strValue1 = "Y";
            billing3.strText = "Yearly";
            listBiling.Add(billing3);

            cbBiling.DataSource = listBiling;
            cbBiling.ValueMember = "strValue1";
            cbBiling.DisplayMember = "strText";

            //Accounting
            List<ObjTmp> listAccounting = new List<ObjTmp>();
            ObjTmp acc1 = new ObjTmp();
            acc1.strValue1 = "H";
            acc1.strText = "Only for higher km";
            listAccounting.Add(acc1);

            ObjTmp acc2 = new ObjTmp();
            acc2.strValue1 = "L";
            acc2.strText = "Only for lower km (return)";
            listAccounting.Add(acc2);

            ObjTmp acc3 = new ObjTmp();
            acc3.strValue1 = "A";
            acc3.strText = "Both higher and lower km";
            listAccounting.Add(acc3);

            cbAccounting.DataSource = listAccounting;
            cbAccounting.ValueMember = "strValue1";
            cbAccounting.DisplayMember = "strText";
        }

        private void fillData()
        {
            if (contract != null && contract.ContractOID > 0)
            {
                //Start
                txtStartDate.Value = contract.ContractDateData.ContractStartDate;
                txtStartKm.Text = contract.ContractDateData.ContractStartKm.ToString();
                txtStartHr.Text = contract.ContractDateData.ContractStartHour.ToString();
                txtStartInvoice.Value = contract.ContractDateData.InvoiceStartDate;
                txtPeriod.Value = contract.ContractDateData.ContractPeriodMonth;
                txtKmHr.Text = contract.ContractDateData.ContractPeriodKmHour.ToString();
                if (contract.ContractDateData.ContractPeriodKmHour == 1)
                {
                    rdKmBase.Checked = true;
                    rdHrBase.Checked = false;
                }
                else if (contract.ContractDateData.ContractPeriodKmHour == 2)
                {
                    rdKmBase.Checked = false;
                    rdHrBase.Checked = true;
                }

                //End
                txtEndDate.Value = contract.ContractDateData.ContractEndDate;
                txtEndKm.Text = contract.ContractDateData.ContractEndKm.ToString();
                txtEndHr.Text = contract.ContractDateData.ContractEndHour.ToString();
                txtEndInvoice.Value = contract.ContractDateData.InvoiceEndDate;
                cbTemType.Text = contract.TerminationType.ToString();

                //Payment
                cbPayPeriod.Text = contract.ContractPaymentData.PaymentPeriod.ToString();
                if (contract.IsManualInvoice)
                {
                    cbPayment.Checked = true;
                }
                else
                {
                    cbPayment.Checked = false;
                }
                if (contract.PaymentInBlock)
                {
                    cbInvoice.Checked = true;
                }
                else
                {
                    cbInvoice.Checked = false;
                }

                txtNextBlock.Text = contract.ContractPaymentData.PaymentNextBlockEnd.ToString();
                cbColType.Text = contract.ContractPaymentData.PaymentCollectionType.ToString();
                cbGrpLevel.Text = contract.ContractPaymentData.PaymentGroupingLevel.ToString();
                cbPayTerm.Text = contract.ContractPaymentData.PaymentTerm.ToString();
                cbInvoiceSite.Text = contract.InvoiceSiteId.ToString();

                //captial
                txtStartAmount.Text = contract.ContractCapitalData.CapitalStartAmount.ToString();
                txtStartAmountPayer.Text = contract.ContractCapitalData.CapitalStartPayer.strValue1;
                txtMonAmount.Text = contract.ContractCapitalData.CapitalMonthAmount.ToString();
                txtMonAmountPayer.Text = contract.ContractCapitalData.CapitalMonthPayer.strValue1;
                txtTotalAmount.Text = (contract.ContractCapitalData.CapitalStartAmount
                    + contract.ContractCapitalData.CapitalMonthAmount * contract.ContractDateData.ContractPeriodMonth).ToString();

                //Cost
                cbCostBassis.Text = contract.ContractCostData.CostBasis.ToString();
                txtCostBase.Text = contract.ContractCostData.CostBasedOnService.ToString();
                txtMonBassis.Text = contract.ContractCostData.CostMonthBasis.ToString();
                txtKmBassis.Text = contract.ContractCostData.CostKmBasis.ToString();
                txtLastPay.Text = "txtLastPay";
                txtErr.Text = contract.ContractCostData.CostPerKm.ToString();
                txtLastKm.Text = "";

                //Extra
                cbBiling.Text = contract.ContractExtraKmData.ExtraKmInvoicePeriod.strValue1;
                cbAccounting.Text = contract.ContractExtraKmData.ExtraKmAccounting.strValue1;
                txtMaxDev.Text = contract.ContractExtraKmData.ExtraKmMaxDeviation.ToString();
                txtMinKm.Text = contract.ContractExtraKmData.ExtraKmLowAmount.ToString();
                txtMehrKm.Text = contract.ContractExtraKmData.ExtraKmHighAmount.ToString();
                txtBetrag.Text = contract.ContractExtraKmData.ExtraKmInvoicedAmount.ToString();
            }
            else
            {
                //Start
                txtStartDate.Value = DateTime.Now;
                txtStartKm.Text = "";
                txtStartHr.Text = "";
                txtStartInvoice.Value = DateTime.Now;
                txtPeriod.Value = 0;
                txtKmHr.Text = "";

                //End
                txtEndDate.Value = DateTime.Now;
                txtEndKm.Text = "";
                txtEndHr.Text = "";
                txtEndInvoice.Value = DateTime.Now;
                cbTemType.Text = "";

                //Payment
                cbPayPeriod.Text = "";
                cbPayment.Checked = false;
                cbInvoice.Checked = false;
                txtNextBlock.Text = "";
                cbColType.Text = "";
                cbGrpLevel.Text = "";
                cbPayTerm.Text = "";
                cbInvoiceSite.Text = "";

                //captial
                txtStartAmount.Text = "";
                txtStartAmountPayer.Text = "";
                txtMonAmount.Text = "";
                txtMonAmountPayer.Text = "";
                txtTotalAmount.Text = "";

                //Cost
                cbCostBassis.Text = "";
                txtCostBase.Text = "";
                txtMonBassis.Text = "";
                txtKmBassis.Text = "";
                txtLastPay.Text = "";
                txtErr.Text = "";
                txtLastKm.Text = "";

                //Extra
                cbBiling.Text = "";
                cbAccounting.Text = "";
                txtMaxDev.Text = "";
                txtMinKm.Text = "";
                txtMehrKm.Text = "";
                txtBetrag.Text = "";

                //Grid
            }

        }

        private void btnNew_Click(object sender, EventArgs e)
        {

        }

        private void btnRick_Click(object sender, EventArgs e)
        {
            dlgSearchCustomer searhCustomer = new dlgSearchCustomer();
            searhCustomer.Owner = this.ParentForm;
            searhCustomer.ShowDialog();
            if (searhCustomer.Custno != "")
            {
                txtPatnerNr.Text = searhCustomer.Custno;
                txtParnerName.Text = searhCustomer.CustName;
                txtRishLevel.Text = contract.RiskLevel.ToString();

            }
        }

        private void caclEndDate()
        {
            Console.WriteLine("------caclEndDate----------");
            DateTime ContractStartDate = txtStartDate.Value.AddDays(1).AddMonths(Int32.Parse(txtPeriod.Text)).AddDays(-1);
            txtEndDate.Value = ContractStartDate;
        }

        private void txtPeriod_ValueChanged(object sender, EventArgs e)
        {
            if (txtPeriod.Value > 0 && txtPeriod.Value != contract.ContractDateData.ContractPeriodMonth)
            {
                //Edit period in month -> recalculate End date = Start date + Period month
                caclEndDate();
            }
        }

        private void txtStartDate_ValueChanged(object sender, EventArgs e)
        {
            if (txtStartDate.Text != "" && txtStartDate.Value != contract.ContractDateData.ContractStartDate)
            {
                //Edit stat date -> recalculate End date = Start date + Period month
                caclEndDate();
            }
        }

        public Contract saveContractData()
        {
            //ContractDate contractDate = contract.ContractDateData;
            //ContractPayment ContractPaymentData = contract.ContractPaymentData;
            //ContractCapital ContractCapitalData = contract.ContractCapitalData;
            //ContractCost ContractCostData = contract.ContractCostData;
            //ContractExtraKm ContractExtraKmData = contract.ContractExtraKmData;

            ContractDate contractDate = contract.ContractDateData;
            ContractPayment ContractPaymentData = contract.ContractPaymentData;
            ContractCapital ContractCapitalData = contract.ContractCapitalData;
            ContractCost ContractCostData = contract.ContractCostData;
            ContractExtraKm ContractExtraKmData = contract.ContractExtraKmData;

            //Start
            contractDate.ContractStartDate = txtStartDate.Value;
            contractDate.ContractStartKm = Int32.Parse(txtStartKm.Text);
            contractDate.ContractStartHour = Int32.Parse(txtStartHr.Text);
            contractDate.InvoiceStartDate = txtStartInvoice.Value;
            contractDate.ContractPeriodMonth = Int32.Parse(txtPeriod.Text);
            contractDate.ContractPeriodKmHour = Int32.Parse(txtKmHr.Text);
            if (rdKmBase.Checked)
            {
                contract.ContractDateData.ContractPeriodKmHour = 1;
            }
            if (rdHrBase.Checked)
            {
                contract.ContractDateData.ContractPeriodKmHour = 2;
            }

            //End
            contractDate.ContractEndDate = txtEndDate.Value;
            contractDate.ContractEndKm = Int32.Parse(txtEndKm.Text);
            contractDate.ContractEndHour = Int32.Parse(txtEndHr.Text);
            contractDate.InvoiceEndDate = txtEndInvoice.Value;

            //Payment
            clsBaseListItem PaymentPeriod = new clsBaseListItem();
            PaymentPeriod.strValue1 = cbPayPeriod.SelectedText.ToString();
            PaymentPeriod.strText = cbPayPeriod.SelectedValue.ToString();
            ContractPaymentData.PaymentPeriod = PaymentPeriod;
            contract.IsManualInvoice = cbPayment.Checked;
            contract.PaymentInBlock = cbInvoice.Checked;

            //ContractPaymentData.PaymentNextBlockEnd = DateTime.Parse(txtNextBlock.Text);
            ContractPaymentData.PaymentCollectionType = cbColType.Text;
            ContractPaymentData.PaymentGroupingLevel = cbGrpLevel.Text;
            //ContractPaymentData.PaymentTerm = Int32.Parse(cbPayTerm.Text);

            clsBaseListItem InvoiceSiteId = new clsBaseListItem();
            InvoiceSiteId.strValue1 = cbInvoiceSite.SelectedText.ToString();
            InvoiceSiteId.strText = cbInvoiceSite.SelectedValue.ToString();
            contract.InvoiceSiteId = InvoiceSiteId;

            //captial
            ContractCapitalData.CapitalStartAmount = Decimal.Parse(txtStartAmount.Text);
            clsBaseListItem CapitalStartPayer = new clsBaseListItem();
            CapitalStartPayer.strValue1 = txtStartAmountPayer.Text.ToString();
            CapitalStartPayer.strText = txtStartAmountPayer.Text.ToString();
            ContractCapitalData.CapitalStartPayer = CapitalStartPayer;
            ContractCapitalData.CapitalMonthAmount = Decimal.Parse(txtMonAmount.Text);
            clsBaseListItem CapitalMonthPayer = new clsBaseListItem();
            CapitalMonthPayer.strValue1 = txtMonAmountPayer.Text.ToString();
            CapitalMonthPayer.strText = txtMonAmountPayer.Text.ToString();
            ContractCapitalData.CapitalMonthPayer = CapitalMonthPayer;
            
            txtTotalAmount.Text = (contract.ContractCapitalData.CapitalStartAmount
                + contract.ContractCapitalData.CapitalMonthAmount * contract.ContractDateData.ContractPeriodMonth).ToString();

            //Cost
            clsBaseListItem CostBasis = new clsBaseListItem();
            CostBasis.strValue1 = cbCostBassis.SelectedValue.ToString();
            CostBasis.strText = cbCostBassis.SelectedText.ToString();
            ContractCostData.CostBasis = CostBasis;
            ContractCostData.CostBasedOnService = Decimal.Parse(txtCostBase.Text);
            ContractCostData.CostMonthBasis = Decimal.Parse(txtMonBassis.Text);
            ContractCostData.CostKmBasis = Decimal.Parse(txtKmBassis.Text);
            ContractCostData.CostPerKm = Decimal.Parse(txtErr.Text);
            txtLastPay.Text = "txtLastPay";
            txtLastKm.Text = "";

            //Extra


            clsBaseListItem ExtraKmInvoicePeriod = new clsBaseListItem();
            ExtraKmInvoicePeriod.strValue1 = cbBiling.SelectedValue.ToString();
            ExtraKmInvoicePeriod.strText = cbBiling.SelectedText.ToString();
            ContractExtraKmData.ExtraKmInvoicePeriod = ExtraKmInvoicePeriod;
            clsBaseListItem ExtraKmAccounting = new clsBaseListItem();
            ExtraKmAccounting.strValue1 = cbAccounting.SelectedValue.ToString();
            ExtraKmAccounting.strText = cbAccounting.SelectedText.ToString();
            ContractExtraKmData.ExtraKmAccounting = ExtraKmAccounting;
            ContractExtraKmData.ExtraKmMaxDeviation = Decimal.Parse(txtMaxDev.Text);
            ContractExtraKmData.ExtraKmLowAmount = Decimal.Parse(txtMinKm.Text);
            ContractExtraKmData.ExtraKmHighAmount = Decimal.Parse(txtMehrKm.Text);
            ContractExtraKmData.ExtraKmInvoicedAmount = Decimal.Parse(txtBetrag.Text);


            contract.ContractDateData = contractDate;
            contract.ContractPaymentData = ContractPaymentData;
            contract.ContractCapitalData = ContractCapitalData;
            contract.ContractCostData = ContractCostData;
            contract.ContractExtraKmData = ContractExtraKmData;
            return contract;
        }
    }
}
