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
using SCPrime.Utils;

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
        DataTable dataTable;
        
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
            listTermination.Add(new ObjTmp("T", "Time"));
            listTermination.Add(new ObjTmp("K", "Kilometer or hour"));
            cbTemType.DataSource = listTermination;
            cbTemType.ValueMember = "strValue1";
            cbTemType.DisplayMember = "strText";


            //Payment period  
            List<ObjTmp> listPayment = new List<ObjTmp>();
            listPayment.Add(new ObjTmp("M", "Month"));
            listPayment.Add(new ObjTmp("Q", "Quarter"));
            listPayment.Add(new ObjTmp("H", "Half year"));
            listPayment.Add(new ObjTmp("Y", "Year"));

            cbPayPeriod.DataSource = listPayment;
            cbPayPeriod.ValueMember = "strValue1";
            cbPayPeriod.DisplayMember = "strText";


            //Payment collection type
            List<ObjTmp> listPaymentCol = new List<ObjTmp>();
            listPaymentCol.Add(new ObjTmp("E", "ESR"));
            listPaymentCol.Add(new ObjTmp("D", "Debit"));
            listPaymentCol.Add(new ObjTmp("R", "e-Rechnung"));

            cbColType.DataSource = listPaymentCol;
            cbColType.ValueMember = "strValue1";
            cbColType.DisplayMember = "strText";


            //Payment Grouping level
            List<ObjTmp> listPaymentGrp = new List<ObjTmp>();
            listPaymentGrp.Add(new ObjTmp("C", "By customer"));
            listPaymentGrp.Add(new ObjTmp("S", "By contract"));
            listPaymentGrp.Add(new ObjTmp("F", "Flat rate"));

            cbGrpLevel.DataSource = listPaymentGrp;
            cbGrpLevel.ValueMember = "strValue1";
            cbGrpLevel.DisplayMember = "strText";

            //payment term: from control data
            List<clsBaseListItem> listPaymentTermDb = sCBase.GetConfig("MAKSUEHDOT");
            List<ObjTmp> listPaymentTerm = new List<ObjTmp>(listPaymentTermDb.Count);
            foreach (clsBaseListItem term in listPaymentTermDb)
            {
                listPaymentTerm.Add(new ObjTmp(term.nValue1, term.strText));
            }
            cbPayTerm.DataSource = listPaymentTerm;
            cbPayTerm.ValueMember = "nValue1";
            cbPayTerm.DisplayMember = "strText";

            //Invoicing site 
            List<clsBaseListItem> listSite = sCBase.getAMSites();
            List<ObjTmp> lstSites = new List<ObjTmp>(listSite.Count);
            foreach (clsBaseListItem site in listSite)
            {
                lstSites.Add(new ObjTmp(site.strValue1, site.strText));
            }
            cbInvoiceSite.DataSource = lstSites;
            cbInvoiceSite.ValueMember = "strValue1";
            cbInvoiceSite.DisplayMember = "strText";

            //Start amount payer
            listPaymentTermDb = sCBase.GetConfig("ZSCCAPPAYE");
            List<ObjTmp> listZSCCAPPAYE = new List<ObjTmp>(listPaymentTermDb.Count);
            List<ObjTmp> listMonAmount = new List<ObjTmp>(listPaymentTermDb.Count);
            foreach (clsBaseListItem term in listPaymentTermDb)
            {
                listZSCCAPPAYE.Add(new ObjTmp(term.nValue1.ToString(), term.strText));
                listMonAmount.Add(new ObjTmp(term.nValue1.ToString(), term.strText));
            }
            txtStartAmountPayer.DataSource = listZSCCAPPAYE;
            txtStartAmountPayer.ValueMember = "strValue1";
            txtStartAmountPayer.DisplayMember = "strText";

            //Monthly amount player
            txtMonAmountPayer.DataSource = listMonAmount;
            txtMonAmountPayer.ValueMember = "strValue1";
            txtMonAmountPayer.DisplayMember = "strText";

            //Cost basis
            List<ObjTmp> listCostBs = new List<ObjTmp>();
            listCostBs.Add(new ObjTmp("M", "Monthly cost fix"));
            listCostBs.Add(new ObjTmp("K", "Km or hour cost"));
            listCostBs.Add(new ObjTmp("L", "Km or hour cost with lump amount"));
            cbCostBassis.DataSource = listCostBs;
            cbCostBassis.ValueMember = "strValue1";
            cbCostBassis.DisplayMember = "strText";

            //Set Billing period
            List<ObjTmp> listBiling = new List<ObjTmp>();
            listBiling.Add(new ObjTmp("M", "Monthly"));
            listBiling.Add(new ObjTmp("H", "Half year"));
            listBiling.Add(new ObjTmp("Y", "Yearly"));
            cbBiling.DataSource = listBiling;
            cbBiling.ValueMember = "strValue1";
            cbBiling.DisplayMember = "strText";

            //Accounting
            List<ObjTmp> listAccounting = new List<ObjTmp>();
            listAccounting.Add(new ObjTmp("H", "Only for higher km"));
            listAccounting.Add(new ObjTmp("L", "Only for lower km (return)"));
            listAccounting.Add(new ObjTmp("A", "Both higher and lower km"));
            cbAccounting.DataSource = listAccounting;
            cbAccounting.ValueMember = "strValue1";
            cbAccounting.DisplayMember = "strText";


            //Rolling code
            listPaymentTermDb = sCBase.GetConfig("ZSCROLLING");
            List<ObjTmp> listRolling = new List<ObjTmp>(listPaymentTermDb.Count);
            foreach (clsBaseListItem term in listPaymentTermDb)
            {
                listRolling.Add(new ObjTmp(term.nValue1.ToString(), term.strText));
            }
            cbRoll.DataSource = listRolling;
            cbRoll.ValueMember = "strValue1";
            cbRoll.DisplayMember = "strText";

            //Grid
            dataTable = new DataTable();
        }

        public void fillData()
        {
            if (contract != null && contract.ContractOID > 0)
            {
                //Start
                txtStartDate.Value = contract.ContractDateData.ContractStartDate;
                txtStartKm.Text = contract.ContractDateData.ContractStartKm.ToString();
                txtStartHr.Text = contract.ContractDateData.ContractStartHour.ToString();
                txtStartInvoice.Value = contract.ContractDateData.InvoiceStartDate;
                txtPeriod.Value = contract.ContractDateData.ContractPeriodMonth;
                txtKm.Text = contract.ContractDateData.ContractPeriodKm.ToString();
                txtHr.Text =  contract.ContractDateData.ContractPeriodHour.ToString();


                rdKmBase.Checked = (contract.ContractDateData != null && contract.ContractDateData.ContractPeriodKmHour == 1);
                rdHrBase.Checked = (contract.ContractDateData != null && contract.ContractDateData.ContractPeriodKmHour == 2);

                //End
                txtEndDate.Value = contract.ContractDateData.ContractEndDate;
                txtEndKm.Text = contract.ContractDateData.ContractEndKm.ToString();
                txtEndHr.Text = contract.ContractDateData.ContractEndHour.ToString();
                txtEndInvoice.Value = contract.ContractDateData.InvoiceEndDate;
                cbTemType.SelectedValue = contract.TerminationType.strValue1;

                //Payment
                if (contract.ContractPaymentData.PaymentPeriod != null && contract.ContractPaymentData.PaymentPeriod.strValue1 != null)
                {
                    cbPayPeriod.SelectedValue = contract.ContractPaymentData.PaymentPeriod.strValue1;
                }
                else
                {
                    cbPayPeriod.SelectedIndex = -1;
                }

                cbPayment.Checked = contract.IsManualInvoice;
                
                cbInvoice.Checked = contract.ContractPaymentData.PaymentIsInBlock;


                if (contract.ContractPaymentData != null && contract.ContractPaymentData.PaymentNextBlockStart != null)
                {
                    txtNextBlock.Text = contract.ContractPaymentData.PaymentNextBlockStart.ToString();
                }
                else
                {
                    txtNextBlock.Text = "";
                }
                if (contract.ContractPaymentData != null && contract.ContractPaymentData.PaymentNextBlockEnd != null)
                {
                    txtNextBlockEnd.Text = contract.ContractPaymentData.PaymentNextBlockEnd.ToString();
                }
                else
                {
                    txtNextBlockEnd.Text = "";
                }

                cbColType.SelectedValue = contract.ContractPaymentData.PaymentCollectionType;
                cbGrpLevel.SelectedValue = contract.ContractPaymentData.PaymentGroupingLevel;
                cbPayTerm.SelectedValue = contract.ContractPaymentData.PaymentTerm;
                if (contract.InvoiceSiteId != null)
                {
                    cbInvoiceSite.SelectedValue = contract.InvoiceSiteId.strValue1;
                }
                else
                {
                    cbInvoiceSite.SelectedValue = -1;
                }


                //captial
                txtStartAmount.Text = contract.ContractCapitalData.CapitalStartAmount.ToString();
                if (contract.ContractCapitalData != null && contract.ContractCapitalData.CapitalStartPayer != null
                    && contract.ContractCapitalData.CapitalStartPayer.strValue1 != null && contract.ContractCapitalData.CapitalStartPayer.strValue1.Length > 0)
                {
                    txtStartAmountPayer.SelectedValue = contract.ContractCapitalData.CapitalStartPayer.strValue1;
                }
                else
                {
                    txtStartAmountPayer.SelectedValue = -1;
                }

                txtMonAmount.Text = contract.ContractCapitalData.CapitalMonthAmount.ToString();
                if (contract.ContractCapitalData != null && contract.ContractCapitalData.CapitalMonthPayer != null
                    && contract.ContractCapitalData.CapitalMonthPayer.strValue1 != null && contract.ContractCapitalData.CapitalMonthPayer.strValue1.Length > 0)
                {
                    txtMonAmountPayer.SelectedValue = contract.ContractCapitalData.CapitalMonthPayer.strValue1;
                }
                else
                {
                    txtMonAmountPayer.SelectedValue = -1;
                }
                txtTotalAmount.Text = (contract.ContractCapitalData.CapitalStartAmount
                    + contract.ContractCapitalData.CapitalMonthAmount * contract.ContractDateData.ContractPeriodMonth).ToString();

                //Cost
                if (contract.ContractCostData.CostBasis != null
                    && contract.ContractCostData.CostBasis.strValue1 != null
                    && contract.ContractCostData.CostBasis.strValue1.Length > 0)
                {
                    cbCostBassis.SelectedValue = contract.ContractCostData.CostBasis.strValue1;
                }
                else
                {
                    cbCostBassis.SelectedIndex = -1;
                }

                txtCostBase.Text = contract.ContractCostData.CostBasedOnService.ToString();
                txtMonBassis.Text = contract.ContractCostData.CostMonthBasis.ToString();
                txtKmBassis.Text = contract.ContractCostData.CostKmBasis.ToString();
                txtLastPay.Text = "txtLastPay";
                txtErr.Text = contract.ContractCostData.CostPerKm.ToString();

                //LastKm info
                VehicleMileage vehicleMileage = contract.VehiId.lastMileages();
                if (vehicleMileage != null)
                {
                    txtLastKm.Text = vehicleMileage.LastKmInfo();
                }
                else
                {
                    txtLastKm.Text = "";
                }

                //Extra
                if (contract.ContractExtraKmData.ExtraKmInvoicePeriod != null
                    && contract.ContractExtraKmData.ExtraKmInvoicePeriod.strValue1 != null
                    && contract.ContractExtraKmData.ExtraKmInvoicePeriod.strValue1 != "")
                {
                    cbBiling.SelectedValue = contract.ContractExtraKmData.ExtraKmInvoicePeriod.strValue1;
                }
                else
                {
                    cbBiling.SelectedIndex = -1;
                }


                if (contract.ContractExtraKmData.ExtraKmInvoicePeriod != null
                    && contract.ContractExtraKmData.ExtraKmAccounting.strValue1 != null
                    && contract.ContractExtraKmData.ExtraKmAccounting.strValue1 != "")
                {
                    cbAccounting.SelectedValue = contract.ContractExtraKmData.ExtraKmAccounting.strValue1;
                }
                else
                {
                    cbAccounting.SelectedIndex = -1;
                }


                txtMaxDev.Text = contract.ContractExtraKmData.ExtraKmMaxDeviation.ToString();
                txtLowKm.Text = contract.ContractExtraKmData.ExtraKmLowAmount.ToString();
                txtHighKm.Text = contract.ContractExtraKmData.ExtraKmHighAmount.ToString();
                txtInvoiceAmount.Text = contract.ContractExtraKmData.ExtraKmInvoicedAmount.ToString();

                //cbRoll
                if (contract.RollingCode != null && contract.RollingCode.strValue1 != null && contract.RollingCode.strValue1 != "")
                {
                    cbRoll.SelectedValue = contract.RollingCode.strValue1;
                }
                else
                {
                    cbRoll.SelectedIndex = -1;
                }

                cbInvoiceDetail.Checked = contract.IsInvoiceDetail;

                //Risk
                ContractCustomer RiskCustId = contract.RiskCustId;
                if (RiskCustId != null && RiskCustId.CustId > 0)
                {
                    txtRiskCusId.Text = RiskCustId.CustId.ToString();
                    txtPatnerNr.Text = RiskCustId.CustId.ToString();
                    txtParnerName.Text = RiskCustId.Name;
                }
                else
                {
                    txtRiskCusId.Text = "";
                    txtPatnerNr.Text = "";
                    txtParnerName.Text = "";
                }

                txtRishLevel.Text = contract.RiskLevel.ToString();

                //Load data grid
                List<ZSC_SubcontractorContractRisk> contractRisk = contract.loadZSC_SubcontractorContractRisk();
                dataTable = ObjectUtils.ConvertToDataTable(contractRisk);
            }
            else
            {
                //Start
                txtStartDate.Value = DateTime.Now;
                txtStartKm.Text = "";
                txtStartHr.Text = "";
                txtStartInvoice.Value = DateTime.Now;
                txtPeriod.Value = 0;
                txtKm.Text = "";
                txtHr.Text = "";

                //End
                txtEndDate.Value = DateTime.Now;
                txtEndKm.Text = "";
                txtEndHr.Text = "";
                txtEndInvoice.Value = DateTime.Now;
                cbTemType.Text = "";

                //Payment
                cbPayPeriod.SelectedIndex = -1;
                cbPayment.Checked = false;
                cbInvoice.Checked = false;
                txtNextBlock.Text = "";
                txtNextBlockEnd.Text = "";
                cbColType.SelectedIndex = -1;
                cbGrpLevel.SelectedIndex = -1;
                cbPayTerm.SelectedIndex = -1;
                cbInvoiceSite.SelectedIndex = -1;

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
                txtLowKm.Text = "";
                txtHighKm.Text = "";
                txtInvoiceAmount.Text = "";

                cbRoll.SelectedIndex = -1;
                cbInvoiceDetail.Checked = false;

                //Risk
                txtRiskCusId.Text = "";
                txtPatnerNr.Text = "";
                txtParnerName.Text = "";
                txtRishLevel.Text = "";

                //Grid
            }

            gridRisk.DataSource = dataTable;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            dlgSearchCustomer searhCustomer = new dlgSearchCustomer();
            searhCustomer.Owner = this.ParentForm;
            searhCustomer.ShowDialog();
            if (searhCustomer.Custno != "")
            {
                DataRow drToAdd = dataTable.NewRow();

                drToAdd["RiskPartnerCustId"] = 123;
                drToAdd["Name"] = searhCustomer.CustName;

                dataTable.Rows.Add(drToAdd);
                dataTable.AcceptChanges();
            }
        }

        private void btnRick_Click(object sender, EventArgs e)
        {
            dlgSearchCustomer searhCustomer = new dlgSearchCustomer();
            searhCustomer.Owner = this.ParentForm;
            searhCustomer.ShowDialog();
            if (searhCustomer.Custno != "")
            {
                txtRiskCusId.Text = searhCustomer.Custno;
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
            if (contractDate == null)
            {
                contractDate = new ContractDate();
            }
            if (ContractPaymentData == null)
            {
                ContractPaymentData = new ContractPayment();
            }
            if (ContractCapitalData == null)
            {
                ContractCapitalData = new ContractCapital();
            }
            if (ContractCostData == null)
            {
                ContractCostData = new ContractCost();
            }
            if (ContractExtraKmData == null)
            {
                ContractExtraKmData = new ContractExtraKm();
            }

            //Start
            contractDate.ContractStartDate = txtStartDate.Value;
            contractDate.ContractStartKm = Int32.Parse(txtStartKm.Text);
            contractDate.ContractStartHour = Int32.Parse(txtStartHr.Text);
            contractDate.InvoiceStartDate = txtStartInvoice.Value;
            contractDate.ContractPeriodMonth = Int32.Parse(txtPeriod.Text);
            contractDate.ContractPeriodKm = Int32.Parse(txtKm.Text);
            contractDate.ContractPeriodHour = Int32.Parse(txtHr.Text);


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
            if (cbPayPeriod.SelectedValue != null)
            {
                clsBaseListItem PaymentPeriod = new clsBaseListItem();
                PaymentPeriod.strValue1 = cbPayPeriod.SelectedValue.ToString();
                PaymentPeriod.strText = cbPayPeriod.SelectedText;
                ContractPaymentData.PaymentPeriod = PaymentPeriod;
            }


            contract.IsManualInvoice = cbPayment.Checked;
            

            ContractPaymentData.PaymentIsInBlock = cbInvoice.Checked;

            //ContractPaymentData.PaymentNextBlockEnd = DateTime.Parse(txtNextBlock.Text);
            ContractPaymentData.PaymentCollectionType = cbColType.SelectedValue.ToString();
            ContractPaymentData.PaymentGroupingLevel = cbGrpLevel.SelectedValue.ToString();
            //ContractPaymentData.PaymentTerm = Int32.Parse(cbPayTerm.Text);

            clsBaseListItem InvoiceSiteId = new clsBaseListItem();
            InvoiceSiteId.strValue1 = cbInvoiceSite.SelectedValue.ToString();
            InvoiceSiteId.strText = cbInvoiceSite.SelectedText;
            contract.InvoiceSiteId = InvoiceSiteId;

            //captial
            ContractCapitalData.CapitalStartAmount = Decimal.Parse(txtStartAmount.Text);
            clsBaseListItem CapitalStartPayer = new clsBaseListItem();
            CapitalStartPayer.strValue1 = txtStartAmountPayer.SelectedValue.ToString();
            CapitalStartPayer.strText = txtStartAmountPayer.SelectedText.ToString();
            ContractCapitalData.CapitalStartPayer = CapitalStartPayer;
            ContractCapitalData.CapitalMonthAmount = Decimal.Parse(txtMonAmount.Text);
            clsBaseListItem CapitalMonthPayer = new clsBaseListItem();
            CapitalMonthPayer.strValue1 = txtMonAmountPayer.SelectedValue.ToString();
            CapitalMonthPayer.strText = txtMonAmountPayer.SelectedText.ToString();
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
            ContractExtraKmData.ExtraKmLowAmount = Decimal.Parse(txtLowKm.Text);
            ContractExtraKmData.ExtraKmHighAmount = Decimal.Parse(txtHighKm.Text);
            ContractExtraKmData.ExtraKmInvoicedAmount = Decimal.Parse(txtInvoiceAmount.Text);

            //
            clsBaseListItem RollingCode = new clsBaseListItem();
            if (cbRoll.SelectedIndex > 0)
            {
                RollingCode.strValue1 = cbRoll.SelectedValue.ToString();
                RollingCode.strText = cbRoll.SelectedText.ToString();
            }
            contract.RollingCode = RollingCode;

            contract.IsInvoiceDetail = cbInvoiceDetail.Checked;

            //RiskCustId
            ContractCustomer RiskCustId = new ContractCustomer();
            if (txtRiskCusId.Text != null && txtRiskCusId.Text.Trim().Length > 0)
            {
                RiskCustId.CustId = Int32.Parse(txtRiskCusId.Text);
            }
            contract.RiskCustId = RiskCustId;



            contract.ContractDateData = contractDate;
            contract.ContractPaymentData = ContractPaymentData;
            contract.ContractCapitalData = ContractCapitalData;
            contract.ContractCostData = ContractCostData;
            contract.ContractExtraKmData = ContractExtraKmData;
            return contract;
        }

        private void cbCostBassis_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCostBassis.SelectedValue != null)
            {
                if (cbCostBassis.SelectedValue.ToString() == "M")
                {
                    txtMonBassis.Enabled = false;
                    txtMonBassis.Text = "";

                    txtKmBassis.Enabled = false;
                    txtKmBassis.Text = "";
                }
                else if (cbCostBassis.SelectedValue.ToString() == "K")
                {
                    txtMonBassis.Enabled = false;
                    txtMonBassis.Text = "";

                    txtKmBassis.Enabled = true;
                    txtKmBassis.Text = "";
                }
                else if (cbCostBassis.SelectedValue.ToString() == "L")
                {
                    txtMonBassis.Enabled = true;
                    txtMonBassis.Text = "";

                    txtKmBassis.Enabled = false;
                    txtKmBassis.Text = "Tinh theo txtCostBase";
                }
            }

        }
    }
}
