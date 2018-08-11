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
        
        DataTable dataTable;
        
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
            if (ContractFrm.objContract != null && ContractFrm.objContract.ContractOID > 0)
            {
                //Start
                txtStartDate.Value = ContractFrm.objContract.ContractDateData.ContractStartDate;
                txtStartKm.Text = ContractFrm.objContract.ContractDateData.ContractStartKm.ToString();
                txtStartHr.Text = ContractFrm.objContract.ContractDateData.ContractStartHour.ToString();
                txtStartInvoice.Value = ContractFrm.objContract.ContractDateData.InvoiceStartDate;
                txtPeriod.Value = ContractFrm.objContract.ContractDateData.ContractPeriodMonth;
                txtKm.Text = ContractFrm.objContract.ContractDateData.ContractPeriodKm.ToString();
                txtHr.Text = ContractFrm.objContract.ContractDateData.ContractPeriodHour.ToString();


                rdKmBase.Checked = (ContractFrm.objContract.ContractDateData != null && ContractFrm.objContract.ContractDateData.ContractPeriodKmHour == 1);
                rdHrBase.Checked = (ContractFrm.objContract.ContractDateData != null && ContractFrm.objContract.ContractDateData.ContractPeriodKmHour == 2);

                //End
                txtEndDate.Value = ContractFrm.objContract.ContractDateData.ContractEndDate;
                txtEndKm.Text = ContractFrm.objContract.ContractDateData.ContractEndKm.ToString();
                txtEndHr.Text = ContractFrm.objContract.ContractDateData.ContractEndHour.ToString();
                txtEndInvoice.Value = ContractFrm.objContract.ContractDateData.InvoiceEndDate;
                cbTemType.SelectedValue = ContractFrm.objContract.TerminationType.strValue1;

                //Payment
                if (ContractFrm.objContract.ContractPaymentData.PaymentPeriod != null && ContractFrm.objContract.ContractPaymentData.PaymentPeriod.strValue1 != null)
                {
                    cbPayPeriod.SelectedValue = ContractFrm.objContract.ContractPaymentData.PaymentPeriod.strValue1;
                }
                else
                {
                    cbPayPeriod.SelectedIndex = -1;
                }

                cbPayment.Checked = ContractFrm.objContract.IsManualInvoice;

                cbInvoice.Checked = ContractFrm.objContract.ContractPaymentData.PaymentIsInBlock;


                if (ContractFrm.objContract.ContractPaymentData != null && ContractFrm.objContract.ContractPaymentData.PaymentNextBlockStart != null)
                {
                    txtNextBlock.Text = ContractFrm.objContract.ContractPaymentData.PaymentNextBlockStart.ToString();
                }
                else
                {
                    txtNextBlock.Text = "";
                }
                if (ContractFrm.objContract.ContractPaymentData != null && ContractFrm.objContract.ContractPaymentData.PaymentNextBlockEnd != null)
                {
                    txtNextBlockEnd.Text = ContractFrm.objContract.ContractPaymentData.PaymentNextBlockEnd.ToString();
                }
                else
                {
                    txtNextBlockEnd.Text = "";
                }

                cbColType.SelectedValue = ContractFrm.objContract.ContractPaymentData.PaymentCollectionType;
                cbGrpLevel.SelectedValue = ContractFrm.objContract.ContractPaymentData.PaymentGroupingLevel;
                cbPayTerm.SelectedValue = ContractFrm.objContract.ContractPaymentData.PaymentTerm;
                if (ContractFrm.objContract.InvoiceSiteId != null)
                {
                    cbInvoiceSite.SelectedValue = ContractFrm.objContract.InvoiceSiteId.strValue1;
                }
                else
                {
                    cbInvoiceSite.SelectedValue = -1;
                }


                //captial
                txtStartAmount.Text = ContractFrm.objContract.ContractCapitalData.CapitalStartAmount.ToString();
                if (ContractFrm.objContract.ContractCapitalData != null && ContractFrm.objContract.ContractCapitalData.CapitalStartPayer != null
                    && ContractFrm.objContract.ContractCapitalData.CapitalStartPayer.strValue1 != null && ContractFrm.objContract.ContractCapitalData.CapitalStartPayer.strValue1.Length > 0)
                {
                    txtStartAmountPayer.SelectedValue = ContractFrm.objContract.ContractCapitalData.CapitalStartPayer.strValue1;
                }
                else
                {
                    txtStartAmountPayer.SelectedValue = -1;
                }

                txtMonAmount.Text = ContractFrm.objContract.ContractCapitalData.CapitalMonthAmount.ToString();
                if (ContractFrm.objContract.ContractCapitalData != null && ContractFrm.objContract.ContractCapitalData.CapitalMonthPayer != null
                    && ContractFrm.objContract.ContractCapitalData.CapitalMonthPayer.strValue1 != null && ContractFrm.objContract.ContractCapitalData.CapitalMonthPayer.strValue1.Length > 0)
                {
                    txtMonAmountPayer.SelectedValue = ContractFrm.objContract.ContractCapitalData.CapitalMonthPayer.strValue1;
                }
                else
                {
                    txtMonAmountPayer.SelectedValue = -1;
                }
                txtTotalAmount.Text = (ContractFrm.objContract.ContractCapitalData.CapitalStartAmount
                    + ContractFrm.objContract.ContractCapitalData.CapitalMonthAmount * ContractFrm.objContract.ContractDateData.ContractPeriodMonth).ToString();

                //Cost
                if (ContractFrm.objContract.ContractCostData.CostBasis != null
                    && ContractFrm.objContract.ContractCostData.CostBasis.strValue1 != null
                    && ContractFrm.objContract.ContractCostData.CostBasis.strValue1.Length > 0)
                {
                    cbCostBassis.SelectedValue = ContractFrm.objContract.ContractCostData.CostBasis.strValue1;
                }
                else
                {
                    cbCostBassis.SelectedIndex = -1;
                }

                txtCostBase.Text = ContractFrm.objContract.ContractCostData.CostBasedOnService.ToString();
                txtMonBassis.Text = ContractFrm.objContract.ContractCostData.CostMonthBasis.ToString();
                txtKmBassis.Text = ContractFrm.objContract.ContractCostData.CostKmBasis.ToString();
                txtLastPay.Text = "txtLastPay";
                txtErr.Text = ContractFrm.objContract.ContractCostData.CostPerKm.ToString();

                //LastKm info
                VehicleMileage vehicleMileage = ContractFrm.objContract.VehiId.lastMileages();
                if (vehicleMileage != null)
                {
                    txtLastKm.Text = vehicleMileage.LastKmInfo();
                }
                else
                {
                    txtLastKm.Text = "";
                }

                //Extra
                if (ContractFrm.objContract.ContractExtraKmData.ExtraKmInvoicePeriod != null
                    && ContractFrm.objContract.ContractExtraKmData.ExtraKmInvoicePeriod.strValue1 != null
                    && ContractFrm.objContract.ContractExtraKmData.ExtraKmInvoicePeriod.strValue1 != "")
                {
                    cbBiling.SelectedValue = ContractFrm.objContract.ContractExtraKmData.ExtraKmInvoicePeriod.strValue1;
                }
                else
                {
                    cbBiling.SelectedIndex = -1;
                }


                if (ContractFrm.objContract.ContractExtraKmData.ExtraKmInvoicePeriod != null
                    && ContractFrm.objContract.ContractExtraKmData.ExtraKmAccounting.strValue1 != null
                    && ContractFrm.objContract.ContractExtraKmData.ExtraKmAccounting.strValue1 != "")
                {
                    cbAccounting.SelectedValue = ContractFrm.objContract.ContractExtraKmData.ExtraKmAccounting.strValue1;
                }
                else
                {
                    cbAccounting.SelectedIndex = -1;
                }


                txtMaxDev.Text = ContractFrm.objContract.ContractExtraKmData.ExtraKmMaxDeviation.ToString();
                txtLowKm.Text = ContractFrm.objContract.ContractExtraKmData.ExtraKmLowAmount.ToString();
                txtHighKm.Text = ContractFrm.objContract.ContractExtraKmData.ExtraKmHighAmount.ToString();
                txtInvoiceAmount.Text = ContractFrm.objContract.ContractExtraKmData.ExtraKmInvoicedAmount.ToString();

                //cbRoll
                if (ContractFrm.objContract.RollingCode != null && ContractFrm.objContract.RollingCode.strValue1 != null && ContractFrm.objContract.RollingCode.strValue1 != "")
                {
                    cbRoll.SelectedValue = ContractFrm.objContract.RollingCode.strValue1;
                }
                else
                {
                    cbRoll.SelectedIndex = -1;
                }

                cbInvoiceDetail.Checked = ContractFrm.objContract.IsInvoiceDetail;

                //Risk
                ContractCustomer RiskCustId = ContractFrm.objContract.RiskCustId;
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

                txtRishLevel.Text = ContractFrm.objContract.RiskLevel.ToString();

                //Load data grid

                fillRisk();

                generateColumns();
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

                drToAdd["RiskPartnerCustId"] = Int32.Parse(searhCustomer.Custno);
                drToAdd["Name"] = searhCustomer.CustName;

                List<SubContractorContract> SubContracts = ContractFrm.objContract.SubContracts;
                foreach (SubContractorContract subContract in SubContracts)
                    drToAdd["sub" + subContract.OID] = 0;

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
                txtRishLevel.Text = ContractFrm.objContract.RiskLevel.ToString();
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
            if (txtPeriod.Value > 0 && txtPeriod.Value != ContractFrm.objContract.ContractDateData.ContractPeriodMonth)
            {
                //Edit period in month -> recalculate End date = Start date + Period month
                caclEndDate();
            }
        }

        private void txtStartDate_ValueChanged(object sender, EventArgs e)
        {
            if (txtStartDate.Text != "" && txtStartDate.Value != ContractFrm.objContract.ContractDateData.ContractStartDate)
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

            ContractDate contractDate = ContractFrm.objContract.ContractDateData;
            ContractPayment ContractPaymentData = ContractFrm.objContract.ContractPaymentData;
            ContractCapital ContractCapitalData = ContractFrm.objContract.ContractCapitalData;
            ContractCost ContractCostData = ContractFrm.objContract.ContractCostData;
            ContractExtraKm ContractExtraKmData = ContractFrm.objContract.ContractExtraKmData;
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
                ContractFrm.objContract.ContractDateData.ContractPeriodKmHour = 1;
            }
            if (rdHrBase.Checked)
            {
                ContractFrm.objContract.ContractDateData.ContractPeriodKmHour = 2;
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


            ContractFrm.objContract.IsManualInvoice = cbPayment.Checked;


            ContractPaymentData.PaymentIsInBlock = cbInvoice.Checked;

            //ContractPaymentData.PaymentNextBlockEnd = DateTime.Parse(txtNextBlock.Text);
            ContractPaymentData.PaymentCollectionType = cbColType.SelectedValue.ToString();
            ContractPaymentData.PaymentGroupingLevel = cbGrpLevel.SelectedValue.ToString();
            //ContractPaymentData.PaymentTerm = Int32.Parse(cbPayTerm.Text);

            clsBaseListItem InvoiceSiteId = new clsBaseListItem();
            InvoiceSiteId.strValue1 = cbInvoiceSite.SelectedValue.ToString();
            InvoiceSiteId.strText = cbInvoiceSite.SelectedText;
            ContractFrm.objContract.InvoiceSiteId = InvoiceSiteId;

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

            txtTotalAmount.Text = (ContractFrm.objContract.ContractCapitalData.CapitalStartAmount
                + ContractFrm.objContract.ContractCapitalData.CapitalMonthAmount * ContractFrm.objContract.ContractDateData.ContractPeriodMonth).ToString();

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
            ContractFrm.objContract.RollingCode = RollingCode;

            ContractFrm.objContract.IsInvoiceDetail = cbInvoiceDetail.Checked;

            //RiskCustId
            ContractCustomer RiskCustId = new ContractCustomer();
            if (txtRiskCusId.Text != null && txtRiskCusId.Text.Trim().Length > 0)
            {
                RiskCustId.CustId = Int32.Parse(txtRiskCusId.Text);
            }
            ContractFrm.objContract.RiskCustId = RiskCustId;
            ContractFrm.objContract.RiskLevel = decimal.Parse(txtRishLevel.Text);

            

            //Save Risk
            List<ZSC_SubcontractorContractRisk> listRisk = new List<ZSC_SubcontractorContractRisk>();
            List<SubContractorContract> SubContracts = ContractFrm.objContract.SubContracts;

            ZSC_SubcontractorContractRisk objRisk = null;
            string colName = "";
            decimal newInteger;
            foreach (DataRow row in dataTable.Rows)
            {
                foreach (SubContractorContract subContract in SubContracts)
                {
                    colName = "sub" + subContract.OID;
                    if (row[colName] != null && row[colName].ToString().Trim().Length > 0
                        && decimal.TryParse(row[colName].ToString().Trim(), out newInteger) && newInteger > 0)
                    {
                        objRisk = new ZSC_SubcontractorContractRisk();
                        objRisk.SubContractOID = subContract.OID;
                        objRisk.RiskPartnerCustId = Int32.Parse(row["RiskPartnerCustId"].ToString().Trim());
                        objRisk.RiskLevel = decimal.Parse(row[colName].ToString().Trim());
                        listRisk.Add(objRisk);
                    }
                }
            }
            ContractFrm.objContract.SubcontractorContractRisks = listRisk;
            ContractFrm.objContract.ContractDateData = contractDate;
            ContractFrm.objContract.ContractPaymentData = ContractPaymentData;
            ContractFrm.objContract.ContractCapitalData = ContractCapitalData;
            ContractFrm.objContract.ContractCostData = ContractCostData;
            ContractFrm.objContract.ContractExtraKmData = ContractExtraKmData;

            return ContractFrm.objContract;
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

        private void fillRisk()
        {
            List<ZSC_SubcontractorContractRisk> contractRisk = ContractFrm.objContract.loadZSC_SubcontractorContractRisk();
            dataTable = ObjectUtils.ConvertToDataTable(contractRisk);

            //Add Column subcontracter
            List<SubContractorContract> SubContracts = ContractFrm.objContract.SubContracts;

            List<ZSC_SubcontractorContractRisk> listRiskSub = null;

            foreach (SubContractorContract subContract in SubContracts)
            {
                dataTable.Columns.Add("sub" + subContract.OID, typeof(Int32));
            }
            foreach (SubContractorContract subContract in SubContracts)
            {
                listRiskSub = ZSC_SubcontractorContractRisk.getContractRiskBySub(ContractFrm.objContract.ContractOID, subContract.OID);
                if (listRiskSub.Count > 0)
                {
                    foreach (DataRow row in dataTable.Rows)
                        row["sub" + subContract.OID] = listRiskSub.Single(s => s.RiskPartnerCustId == Int32.Parse(row["RiskPartnerCustId"].ToString())).RiskLevel;
                }
                else
                {
                    foreach (DataRow row in dataTable.Rows)
                        row["sub" + subContract.OID] = 0;
                }
            }
        }
        private void generateColumns()
        {
            gridRisk.AutoGenerateColumns = false;
            DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
            col.Name = "RiskPartnerCustId";
            col.HeaderText = "Risk partner Nr.";
            col.DataPropertyName = "RiskPartnerCustId";
            col.ReadOnly = true;
            gridRisk.Columns.Add(col);

            DataGridViewTextBoxColumn col2 = new DataGridViewTextBoxColumn();
            col2.Name = "Name";
            col2.HeaderText = "Name";
            col2.DataPropertyName = "Name";
            col2.ReadOnly = true;
            gridRisk.Columns.Add(col2);


            //Add Column subcontracter
            List<SubContractorContract> SubContracts = ContractFrm.objContract.SubContracts;

            foreach (SubContractorContract subContract in SubContracts)
            {
                DataGridViewTextBoxColumn colStatus = new DataGridViewTextBoxColumn();
                colStatus.Name = "sub" + subContract.OID;
                colStatus.HeaderText = subContract.SuplName;
                colStatus.DataPropertyName = "sub" + subContract.OID;
                gridRisk.Columns.Add(colStatus);
            }
        }

        private void gridRisk_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.ColumnIndex == 2 || e.ColumnIndex == 3)
            {
                //Console.WriteLine("gridRisk_CellValidating " + e.ColumnIndex);
                gridRisk.Rows[e.RowIndex].ErrorText = "";
                decimal newInteger;

                // Don't try to validate the 'new row' until finished 
                // editing since there
                // is not any point in validating its initial value.
                if (gridRisk.Rows[e.RowIndex].IsNewRow) { return; }
                if (!decimal.TryParse(e.FormattedValue.ToString(),
                    out newInteger) || newInteger < 0)
                {
                    e.Cancel = true;
                    gridRisk.Rows[e.RowIndex].ErrorText = "The value must be a non-negative decimal";
                }
            }
        }
    }
}
