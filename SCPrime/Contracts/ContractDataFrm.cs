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

        //
        public ContractDataFrm()
        {
            InitializeComponent();
        }

        private void fillData()
        {
            if (contract != null && contract.ContractOID > 0)
            {
                //Start
                txtStartDate.Text = contract.ContractDateData.ContractStartDate.ToString();
                txtStartKm.Text = contract.ContractDateData.ContractStartKm.ToString();
                txtStartHr.Text = contract.ContractDateData.ContractStartHour.ToString();
                txtStartInvoice.Text = contract.ContractDateData.InvoiceStartDate.ToString();
                txtPeriod.Text = contract.ContractDateData.ContractPeriodMonth.ToString();
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
                txtEndDate.Text = contract.ContractDateData.ContractEndDate.ToString();
                txtEndKm.Text = contract.ContractDateData.ContractEndKm.ToString();
                txtEndHr.Text = contract.ContractDateData.ContractEndHour.ToString();
                txtEndInvoice.Text = contract.ContractDateData.InvoiceEndDate.ToString();
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
                txtStartDate.Text = "";
                txtStartKm.Text = "";
                txtStartHr.Text = "";
                txtStartInvoice.Text = "";
                txtPeriod.Text = "";
                txtKmHr.Text = "";

                //End
                txtEndDate.Text = "";
                txtEndKm.Text = "";
                txtEndHr.Text = "";
                txtEndInvoice.Text = "";
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
    }
}
