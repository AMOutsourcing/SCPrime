using log4net;
using nsBaseClass;
using SCPrime.Model;
using SCPrime.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SCPrime.Contracts
{
    public partial class ContractFrm : nsBaseClass.clsBaseForm
    {
        static readonly ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public delegate void SendStatus(string Message);
        public static SendStatus Sender;

        public delegate void SearchEmployee(SCViewEmployee epl, int flag);
        public static SearchEmployee updateEmployee;

        //public static ContractFrm _instance;
        public static Contract objContract;
        public List<SCContractType> contractType;
        public List<clsBaseListItem> costCenterList = new List<clsBaseListItem>();
        public List<clsBaseListItem> ws = new List<clsBaseListItem>();
        private int subOid = 0;
        public static string myCulture;
        public List<ObjTmp> ls = new List<ObjTmp>();

        //public static ContractFrm instance
        //{
        //    get
        //    {
        //        if (_instance == null || _instance.IsDisposed)
        //        {
        //            ContractFrm._instance = new ContractFrm();
        //        }
        //        return _instance;
        //    }
        //}


        public ContractFrm()
        {
            InitializeComponent();
            // remove context menu
         //   this.ContextMenuStrip.Items.Clear();
            // load data
            Sender = new SendStatus(GetStatus);
            updateEmployee = new SearchEmployee(UpdateEmployee);

            this.headerControl1.btnNewSubcontractor.Click += new System.EventHandler(this.btnNewSubcontractor_click);
            this.headerControl1.dgvSubcontract.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSubcontract_CellValidated);
            this.headerControl1.txtContractStatus.TextChanged += new System.EventHandler(this.updat_satatus);
            this.headerControl1.cbxResponsibleSite.SelectedIndexChanged += new System.EventHandler(this.cbxResponsibleSite_SelectedIndexChanged);
            this.headerControl1.cbxCostcenter.SelectedIndexChanged += new System.EventHandler(this.cbxCostcenter_SelectedIndexChanged);
            this.headerControl1.cbxValidWorkshop.SelectedIndexChanged += new System.EventHandler(this.cbxValidWorkshop_SelectedIndexChanged);
            //this.headerControl1.cbxContractType.SelectedIndexChanged += new System.EventHandler(this.cbxContractType_SelectedIndexChanged);
            this.headerControl1.cbxContractType.DropDownClosed += new System.EventHandler(this.cbxContractType_DropDownClosed);
            this.headerControl1.cbxContractType.KeyUp += new KeyEventHandler(this.cbxContractType_DropDownClosed);
            this.headerControl1.cbxContractType.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.cbxContractType_DropDownClosed);


            //Load data
            reloadAll();
        }
        private void checkInvoiceToCustomer()
        {

            if (this.headerControl1.cbxContractType.SelectedValue != null)
            {
                SCContractType ct = (SCContractType)this.headerControl1.cbxContractType.SelectedItem;
                if (ct != null)
                {
                    if (ct.isInvoice)
                    {
                        this.headerControl1.chkInvoiceToCus.Checked = true;
                        this.headerControl1.chkInvoiceToCus.ForeColor = SystemColors.ControlText;

                    }
                    else
                    {
                        this.headerControl1.chkInvoiceToCus.Checked = false;
                        this.headerControl1.chkInvoiceToCus.ForeColor = SystemColors.ControlText;
                    }
                    //update contract Type
                    objContract.ContractTypeOID = ct;
                    // this.headerControl1.cbxContractType.SelectedItem = ct;
                    this.disableButton(ct);
                }
            }
        }



        private void cbxContractType_DropDownClosed(object sender, EventArgs e)
        {
            checkInvoiceToCustomer();
        }

        private void cbxValidWorkshop_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        //private clsBaseListItem findValidWorkshop(string value, List<clsBaseListItem> list)
        //{
        //    clsBaseListItem Result = new clsBaseListItem();
        //    Result = list.Find(x => x.strValue1 == value);
        //    return Result;
        //}

        private void cbxCostcenter_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (this.headerControl1.cbxCostcenter.SelectedIndex >= 0)
            //{
            //    if (this.headerControl1.cbxCostcenter.SelectedItem != null)
            //    {
            //        ObjTmp s = (ObjTmp)this.headerControl1.cbxCostcenter.SelectedItem;
            //        objContract.CostCenter = findclsBaseListItem(s.strValue1, this.costCenterList);
            //        //MessageBox.Show(s.value);
            //    }

            //}
        }
        public clsBaseListItem findclsBaseListItem(string val, List<clsBaseListItem> list)
        {
            clsBaseListItem Result = new clsBaseListItem();
            Result = list.Find(x => x.strValue1 == val);
            return Result;
        }

        private void cbxResponsibleSite_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadCostCenterCmbData();
        }

        private void updat_satatus(object sender, EventArgs e)
        {
            this.updateContractStatus();
        }

        private void updateContractStatus()
        {
            switch (this.headerControl1.txtContractStatus.Text.Trim())
            {
                case ContractStatus.ModelText:
                    objContract.ContractStatus = ContractStatus.Model;
                    break;
                case ContractStatus.OfferText:
                    objContract.ContractStatus = ContractStatus.Offer;
                    break;
                case ContractStatus.NewText:
                    objContract.ContractStatus = ContractStatus.New;
                    break;
                case ContractStatus.WaitingText:
                    objContract.ContractStatus = ContractStatus.Waiting;
                    break;
                case ContractStatus.ActiveText:
                    objContract.ContractStatus = ContractStatus.Active;
                    break;
                case ContractStatus.OnControlText:
                    objContract.ContractStatus = ContractStatus.OnControl;
                    break;
                case ContractStatus.DeactivatedText:
                    objContract.ContractStatus = ContractStatus.Deactivated;
                    break;

            }
        }

        private void dgvSubcontract_cellvalidated()
        {

        }
        private void updateContractField()
        {
            // objContract.ResponsibleSite update on Event Changed Combobox
        }
        public SubContractorContract RowToSubcontractor(DataGridViewRow row)
        {
            SubContractorContract sc = new SubContractorContract();
            int number = -1;
            bool tmp = Int32.TryParse(row.Cells[0].Value.ToString(), out number);

            if (tmp)
                sc.OID = number;
            else
                sc.OID = 0;

            sc.SubcontractNo = row.Cells["colSubcontractNo"].Value != null ? row.Cells["colSubcontractNo"].Value.ToString() : "";
            sc.Info = row.Cells["colInfo"].Value != null ? row.Cells["colInfo"].Value.ToString() : "";
            sc.Expl = row.Cells["colExpl"].Value != null ? row.Cells["colExpl"].Value.ToString() : "";
            if ((DateTime)(row.Cells["colDateLimit"].Value) > DateTime.MinValue)
            {
                sc.DateLimit = MyUtils.strToDate(row.Cells["colDateLimit"].Value.ToString(), objGlobal.CultureInfo);
            }
            sc.KmLimit = row.Cells["colKmLimit"].Value != null ? (int)row.Cells["colKmLimit"].Value : 0;



            var temp = (decimal)0;
            bool rs = Decimal.TryParse(row.Cells["colBuyPrice"].Value.ToString(), out temp);
            if (rs)
                sc.BuyPrice = Math.Round(temp, 2);
            else
                sc.BuyPrice = (decimal)0;


            sc.SuplNoVal = row.Cells["colSuplNoVal"].Value != null ? row.Cells["colSuplNoVal"].Value.ToString() : "";
            sc.SuplName = row.Cells["colSuplName"].Value != null ? row.Cells["colSuplName"].Value.ToString() : "";

            sc.isDeleted = (bool)row.Cells["colIsDeleted"].Value;

            clsBaseListItem t = new clsBaseListItem();
            t.strValue1 = sc.SuplNoVal;
            t.strText = sc.SuplName;
            sc.SuplNo = t;

            return sc;
        }


        private void btnNewSubcontractor_click(object sender, EventArgs e)
        {
            //MessageBox.Show("Test");
            SubContractorContract tmp = new SubContractorContract();
            subOid--;
            tmp.OID = subOid;
            tmp.SuplNoVal = ls[0].strValue1;
            objContract.SubContracts.Add(tmp);
            var source = new BindingSource();
            source.DataSource = objContract.SubContracts;
            this.headerControl1.dgvSubcontract.DataSource = source;
            this.headerControl1.dgvSubcontract.Refresh();

        }

        private void dgvSubcontract_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            //DataGridViewCellEventArgs me = (DataGridViewCellEventArgs)e;
            //if (me != null)

            DataGridViewRow r = this.headerControl1.dgvSubcontract.Rows[e.RowIndex];
            if (r != null)
            {
                SubContractorContract s = this.RowToSubcontractor(r);
                if (s != null)
                {
                    //longdq
                    objContract.SubContracts = this.replaceListSubContractorContract(objContract.SubContracts, s);

                }

            }
        }
        private List<SubContractorContract> replaceListSubContractorContract(List<SubContractorContract> list, SubContractorContract s)
        {
            //find index  by oid
            var idx = list.FindIndex(x => x.OID == s.OID);
            if (idx != null && idx >= 0)
            {
                list[idx] = s;
            }
            return list;
        }


        private void UpdateEmployee(SCViewEmployee epl, int flag)
        {
            ContractEmployee ce = new ContractEmployee();
            ce.SmanId = epl.SmanId;
            ce.Name = epl.Name;
            ce.Phone = epl.Phone;
            ce.Email = epl.Email;

            if (flag == 1)//Contract responsible person
            {
                this.headerControl1.txtEmployeeID1.Text = epl.SmanId.ToString();
                this.headerControl1.txtEmployeeName1.Text = epl.Name;
                this.headerControl1.txtEmployeePhone1.Text = epl.Phone;
                this.headerControl1.txtEmployeeEmail1.Text = epl.Email;

                ContractFrm.objContract.RespSmanId = ce;
            }
            if (flag == 2)//Contract care taking person
            {
                this.headerControl1.txtEmployeeID2.Text = epl.SmanId.ToString();
                this.headerControl1.txtEmployeeName2.Text = epl.Name;
                this.headerControl1.txtEmployeePhone2.Text = epl.Phone;
                this.headerControl1.txtEmployeeEmail2.Text = epl.Email;
                ContractFrm.objContract.CareSmanId = ce;
            }
        }


        private void GetStatus(string Message)
        {
            if (!this.IsHandleCreated)
            {
                this.CreateHandle();
            }
            if (this.headerControl1.txtContractStatus.InvokeRequired)
                this.headerControl1.txtContractStatus.Invoke(new Action<string>(GetStatus), Message);
            else
            {
                if (!string.IsNullOrEmpty(Message))
                {
                    this.headerControl1.txtContractStatus.Text = Message;
                }


            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            objContract = null;
            this.Close();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }



        private void headerControl1_cbxContractType_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void ContractFrm_Load(object sender, EventArgs e)
        {
            //objContract = new Contract();
            //this.loadComboboxData();
            //this.loadContractData();
            disableNavigatorButton();

        }
        
        private void disableNavigatorButton()
        {
            int value = Contract.checkMinMaxVersion(objContract.ContractCustId.CustId, objContract.VehiId.VehiId, objContract.VersionNo);
            switch (value)
            {
                case 0:
                    //disable Pre
                    btnPrev.Enabled = false;
                    btnNext.Enabled = true;
                    break;
                case 1:
                    btnPrev.Enabled = true;
                    btnNext.Enabled = false;
                    break;
                case 2:
                    btnPrev.Enabled = true;
                    btnNext.Enabled = true;
                    break;
                case 3:
                    btnPrev.Enabled = false;
                    btnNext.Enabled = false;
                    break;
            }
        }

        public SCContractType findContracTypeByOiD(int oid, List<SCContractType> list)
        {
            SCContractType sc = new SCContractType();

            sc = list.SingleOrDefault(x => x.OID == oid);

            return sc;
        }
        public void loadCostCenterCmbData()
        {
            //load cbxCostcenter TODO
            ObjTmp s = (ObjTmp)this.headerControl1.cbxResponsibleSite.SelectedItem;
            costCenterList = Contract.getCostCenter(s.strValue1);

            if (costCenterList != null && costCenterList.Count > 0)
            {
                List<ObjTmp> myccs = new List<ObjTmp>(costCenterList.Count);
                foreach (clsBaseListItem cc in costCenterList)
                {
                    myccs.Add(new ObjTmp(cc.strValue1, cc.strValue1 + "-" + cc.strText));
                }
                this.headerControl1.cbxCostcenter.DataSource = myccs;
                this.headerControl1.cbxCostcenter.DisplayMember = "strText";
                this.headerControl1.cbxCostcenter.ValueMember = "strValue1";


                if (objContract.CostCenter != null)
                {
                    int costIdx = -1;
                    costIdx = myccs.FindIndex(x => x.strValue1 == objContract.CostCenter.strValue1);
                    this.headerControl1.cbxCostcenter.SelectedIndex = costIdx;//objContract.CostCenter.strValue1;
                }
            }
        }
        public void loadComboboxData()
        {
            SCBase sc = new SCBase();


            //load cbxResponsibleSite
            List<clsBaseListItem> listTmp = sc.getAMSites();
            List<ObjTmp> lstSites = new List<ObjTmp>(listTmp.Count);
            foreach (clsBaseListItem site in listTmp)
            {
                lstSites.Add(new ObjTmp(site.strValue1, site.strValue1 + "-" + site.strText));
            }

            this.headerControl1.cbxResponsibleSite.DataSource = lstSites;
            this.headerControl1.cbxResponsibleSite.ValueMember = "strValue1";
            this.headerControl1.cbxResponsibleSite.DisplayMember = "strText";

            if (objContract == null)
            {
                objContract = new Contract();
            }

            if (!string.IsNullOrEmpty(objContract.ResponsibleSite))
            {
                int siteIdx = -1;
                siteIdx = listTmp.FindIndex(x => x.strValue1 == objContract.ValidWorkshopCode.strValue1);
                this.headerControl1.cbxResponsibleSite.SelectedValue = objContract.ResponsibleSite;
            }

            //load cbxContractType
            contractType = sc.getContractTypeActive();
            if (contractType != null && contractType.Count > 0)
            {
                this.headerControl1.cbxContractType.DataSource = contractType;
                this.headerControl1.cbxContractType.ValueMember = "OID";
                this.headerControl1.cbxContractType.DisplayMember = "Name";
                //this.headerControl1.cbxContractType.SelectedItem = contractType[0];

                if (objContract.ContractTypeOID != null)
                {
                    this.headerControl1.cbxContractType.SelectedItem = this.findContracTypeByOiD(objContract.ContractTypeOID.OID, contractType);
                }
            }
            loadCostCenterCmbData();

            //load cbxValidWorkshop 
            ws = Contract.getValidWorkshop();
            if (ws != null && ws.Count > 0)
            {
                List<ObjTmp> myws = new List<ObjTmp>(ws.Count);
                foreach (clsBaseListItem w in ws)
                {
                    myws.Add(new ObjTmp(w.strValue1, w.strText));
                }
                this.headerControl1.cbxValidWorkshop.DataSource = myws;
                this.headerControl1.cbxValidWorkshop.ValueMember = "strValue1";
                this.headerControl1.cbxValidWorkshop.DisplayMember = "strText";

                if (objContract.ValidWorkshopCode != null)
                {
                    int wsIdx = -1;
                    wsIdx = myws.FindIndex(x => x.strValue1 == objContract.ValidWorkshopCode.strValue1);
                    this.headerControl1.cbxValidWorkshop.SelectedIndex = wsIdx;// objContract.ValidWorkshopCode.strValue1;
                }
            }
        }
        public void loadContractData()
        {

            this.displayStatus();
            this.disableButton(objContract.ContractTypeOID);

            this.headerControl1.txtInternalID.Text = objContract.ContractOID.ToString();
            this.headerControl1.txtContracNr.Text = objContract.ContractNo.ToString();
            this.headerControl1.txtExtContractNr.Text = objContract.ExtContractNo;
            this.headerControl1.txtVersionNr.Text = objContract.VersionNo.ToString();

            this.headerControl1.txtCreated.Text = objContract.Created.ToString("d",
                System.Globalization.CultureInfo.GetCultureInfo(objGlobal.CultureInfo));
            this.headerControl1.txtChanged.Text = objContract.Modified.ToString("d",
                System.Globalization.CultureInfo.GetCultureInfo(objGlobal.CultureInfo));

            this.headerControl1.txtLastInvoice.Text = objContract.LastInvoiceDate.ToString("d",
                System.Globalization.CultureInfo.GetCultureInfo(objGlobal.CultureInfo));

            //this.headerControl1.cbxContractType.SelectedValue = objContract.ContractTypeOID;

            this.headerControl1.chkContractVariant.Checked = Contract.checkContractVariant(objContract.ContractCustId.CustId);
            this.headerControl1.chkInvoiceToCus.Checked = objContract.ContractTypeOID.isInvoice;
            this.headerControl1.cbStatistic.Checked = objContract.IsStatistic;
            this.loadDetail();
            this.addSupplierCbx();
            this.loadCustomerEmployee();
            this.Text = "Contract: " + objContract.ContractNo.ToString() + " / " + objContract.VersionNo.ToString();


        }
        public void loadCustomerEmployee()
        {
            //loadInVoiceAddress
            ContractCustomer ccInvoice = new ContractCustomer();
            ccInvoice = objContract.InvoiceCustId;
            if (ccInvoice != null)
            {

                this.headerControl1.txtInvoiceCusNr.Text = ccInvoice.CustNr.ToString();
                this.headerControl1.txtInvoiceCusName.Text = ccInvoice.Name;
                this.headerControl1.txtInvoiceCusAdd.Text = ccInvoice.Address;
                this.headerControl1.txtInvoiceCusEmail.Text = ccInvoice.Email;
                this.headerControl1.txtInvoiceCusPhone.Text = ccInvoice.Phone;
            }
            //load ContractCustomer
            ContractCustomer cc = new ContractCustomer();
            cc = objContract.ContractCustId;
            if (cc != null)
            {

                this.headerControl1.txtContractCusNr.Text = cc.CustNr.ToString();
                this.headerControl1.txtContractCusName.Text = cc.Name;
                this.headerControl1.txtContractCusAdd.Text = cc.Address;
                this.headerControl1.txtContractCusEmail.Text = cc.Email;
                this.headerControl1.txtContractCusPhone.Text = cc.Phone;
            }

            //load Responsible Employee
            ContractEmployee ces = new ContractEmployee();
            ces = objContract.RespSmanId;
            if (ces != null)
            {
                this.headerControl1.txtEmployeeID1.Text = ces.SmanId.ToString();
                this.headerControl1.txtEmployeeName1.Text = ces.Name;
                this.headerControl1.txtEmployeePhone1.Text = ces.Phone;
                this.headerControl1.txtEmployeeEmail1.Text = ces.Email;
            }

            //load Care taking Employee
            ContractEmployee cte = new ContractEmployee();
            cte = objContract.CareSmanId;
            if (cte != null)
            {
                this.headerControl1.txtEmployeeID2.Text = cte.SmanId.ToString();
                this.headerControl1.txtEmployeeName2.Text = cte.Name;
                this.headerControl1.txtEmployeePhone2.Text = cte.Phone;
                this.headerControl1.txtEmployeeEmail2.Text = cte.Email;
            }
        }

        public void addSupplierCbx()
        {
            bool flag = false;
            foreach (DataGridViewColumn dc in headerControl1.dgvSubcontract.Columns)
            {
                if (dc.Name == "colSupplier")
                {
                    flag = true;
                    break;
                }

            }
            if (!flag)
            {


                ls = SubContractorContract.getSuppliers();
                if (ls.Count > 0)
                {

                    DataTable dt = new DataTable();
                    dt = ObjectUtils.ConvertToDataTable(ls);

                    DataGridViewComboBoxColumn cb = new DataGridViewComboBoxColumn();
                    cb.HeaderText = "Supplier";
                    cb.FlatStyle = FlatStyle.Flat;
                    cb.Name = "colSupplier";
                    cb.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    cb.DataSource = dt;
                    cb.DataPropertyName = "SuplNoVal";
                    cb.ValueMember = "strValue1";
                    cb.DisplayMember = "strText";
                    cb.DisplayIndex = 0;
                    cb.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;
                    headerControl1.dgvSubcontract.Columns.Add(cb);


                }
            }
        }
        public void loadDetail()
        {
            //load supplier

            bool tmp = objContract.loadDetail();
            if (tmp)
            {
                this.headerControl1.dgvSubcontract.DataSource = objContract.SubContracts;
                this.headerControl1.dgvSelfContract.DataSource = objContract.SelfContracts;

            }

            addSupplierCbx();
        }
        public void disableButton(SCContractType ct)
        {
            if (!ct.isCollective)
            {
                this.headerControl1.btnNewSelfContract.Enabled = false;
                this.headerControl1.btnDelSelfContract.Enabled = false;
                //delete collective contract
                if (objContract.SelfContracts != null && objContract.SelfContracts.Count > 0)
                    foreach (CollectiveContract c in objContract.SelfContracts)
                    {
                        c.isDeleted = true;
                    }

                if (this.headerControl1.dgvSelfContract.DataSource != null)
                    this.headerControl1.dgvSelfContract.DataSource = null;
            }
            else
            {
                this.headerControl1.btnNewSelfContract.Enabled = true;
                this.headerControl1.btnDelSelfContract.Enabled = true;
            }
        }
        public void displayStatus()
        {
            switch (objContract.ContractStatus.Trim())
            {
                case ContractStatus.Model:
                    this.headerControl1.txtContractStatus.Text = ContractStatus.ModelText;
                    break;
                case ContractStatus.Offer:
                    this.headerControl1.txtContractStatus.Text = ContractStatus.OfferText;
                    break;
                case ContractStatus.New:
                    this.headerControl1.txtContractStatus.Text = ContractStatus.NewText;
                    break;
                case ContractStatus.Waiting:
                    this.headerControl1.txtContractStatus.Text = ContractStatus.WaitingText;
                    break;
                case ContractStatus.Active:
                    this.headerControl1.txtContractStatus.Text = ContractStatus.ActiveText;
                    break;
                case ContractStatus.OnControl:
                    this.headerControl1.txtContractStatus.Text = ContractStatus.OnControlText;
                    break;
                case ContractStatus.Deactivated:
                    this.headerControl1.txtContractStatus.Text = ContractStatus.DeactivatedText;
                    break;

            }
        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            if (e.TabPageIndex == 3)
            {
                //Update Cost based on service
                contractDataFrm.updateCostBasedOnService();
            }
        }

        private void loadVehice()
        {
            ContractVehicle vehicleObj = objContract.VehiId;
            vehicleDataTab.setContract(objContract);
            vehicleDataTab.fillDataVehicle();
        }
        public void updateContract()
        {
            objContract.ExtContractNo = this.headerControl1.txtExtContractNr.Text;
            objContract.IsStatistic = this.headerControl1.cbStatistic.Checked;
            if (this.headerControl1.cbxCostcenter.SelectedIndex >= 0)
            {
                if (this.headerControl1.cbxCostcenter.SelectedItem != null)
                {
                    ObjTmp s = (ObjTmp)this.headerControl1.cbxCostcenter.SelectedItem;
                    objContract.CostCenter = findclsBaseListItem(s.strValue1, this.costCenterList);
                    //MessageBox.Show(s.value);
                }

            }

            if (this.headerControl1.cbxResponsibleSite.SelectedIndex >= 0)
            {
                if (this.headerControl1.cbxResponsibleSite.SelectedItem != null)
                {
                    ObjTmp s = (ObjTmp)this.headerControl1.cbxResponsibleSite.SelectedItem;
                    clsBaseListItem site = null;
                    site = SCBase.findSite(s.strValue1);
                    if (site != null)
                        objContract.SiteId = site;
                    //MessageBox.Show(s.value);
                }

            }

            if (this.headerControl1.cbxValidWorkshop.SelectedIndex >= 0)
            {
                if (this.headerControl1.cbxValidWorkshop.SelectedItem != null)
                {
                    ObjTmp s = (ObjTmp)this.headerControl1.cbxValidWorkshop.SelectedItem;
                    objContract.ValidWorkshopCode = findclsBaseListItem(s.strValue1, this.ws);
                    //MessageBox.Show(s.value);
                }

            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.updateContract();

            this.vehicleDataTab.saveVehicle();

            if (objContract.VehiId.VehiId <= 0)
            {
                // MessageBox.Show("Select Vehicle");
                return;
            }

            //Update category
            this.contractOption1.saveOptionCategories();
            //Update contract data
            objContract = contractDataFrm.saveContractData();
            this.remarkFrm.saveContractRemark();
            bool tmp = false;
            tmp = objContract.saveContract();
            // MessageBox.Show(tmp.ToString());
            objContract = SCBase.searchContracts(objContract.ContractOID);
            this.loadComboboxData();
            this.loadContractData();

            //Load lai data
            objContract.listContractOptions = ContractOption.getContractOption(objContract.ContractOID);
            objContract.listSCContractRemark = SCContractRemark.getRemark(objContract.ContractOID);
            objContract.listSCContractRemark = (objContract.listSCContractRemark == null) ? new List<SCContractRemark>() : objContract.listSCContractRemark;
            //Remark
            this.remarkFrm.loadRemark(objContract.listSCContractRemark);
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to create new version?", "Warning", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                if (objContract.ContractOID != 0)
                {
                    // get old object 
                    Contract oldContract = new Contract();
                    oldContract = SCBase.searchContracts(SCMain.ContractOid);
                    //update status old then save
                    oldContract.ContractStatus = ContractStatus.Deactivated;

                    //Load danh sach ContractOption va ZSC_SubcontractorContractRisk
                    List<ContractOption> oldContractOptions = ContractOption.getContractOption(SCMain.ContractOid);
                    List<ZSC_SubcontractorContractRisk> oldContractRisk = ZSC_SubcontractorContractRisk.getSubcontractorContractRisk(SCMain.ContractOid);

                    oldContract.saveContract();


                    //  new object
                    objContract.ContractOID = 0;
                    objContract.VersionNo = objContract.VersionNo + 1;
                    this.updateContract();

                    //Tao list [ZSC_ContractOption]
                    objContract.listContractOptions = oldContractOptions;
                    if (objContract.listContractOptions != null && objContract.listContractOptions.Count > 0)
                    {
                        foreach (ContractOption objOptionDetail in objContract.listContractOptions)
                        {
                            objOptionDetail.isInsert = true;
                        }
                    }

                    //Tao list [ZSC_SubcontractorContractRisk]
                    objContract.SubcontractorContractRisks = oldContractRisk;


                    bool tmp = objContract.saveContract();
                    if (tmp)
                    {
                        this.loadComboboxData();
                        this.loadContractData();

                        //Load Data
                        this.loadVehice();
                        this.contractOption1.loadDataGrid();
                        this.contractOption1.loadTree();
                        this.contractDataFrm.fillData();
                    }
                }
            }
            else
            {
                //MessageBox.Show("Ha ha");
            }
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to copy this contract?", "Warning", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                if (objContract.ContractOID != 0)
                {
                    //  new object
                    objContract.ContractOID = 0;
                    objContract.VersionNo = 1;
                    //objContract.VersionNo =  1;
                    // this.updateContract();
                    bool tmp = objContract.saveContract();
                    if (tmp)
                    {
                        this.loadComboboxData();
                        this.loadContractData();
                    }
                }
            }
            else
            {
                //MessageBox.Show("Ha ha");
            }
        }

        private void ContractFrm_ResizeEnd(object sender, EventArgs e)
        {



        }

        private void ContractFrm_Resize(object sender, EventArgs e)
        {

            //this.headerControl1.panel2.Width = this.headerControl1.Width / 2;
            //this.headerControl1.panel1.Width = this.headerControl1.Width / 2;

            //var x = this.headerControl1.panel2.Location.X + this.headerControl1.panel2.Width;
            //var y = this.headerControl1.panel2.Location.Y;
            //this.headerControl1.panel1.Location = new Point(x, y);




        }

        private void btnMileageReg_Click(object sender, EventArgs e)
        {
            MileageRegisterFrm frm = new MileageRegisterFrm();
            frm.ShowDialog();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            int prevOid = -1;
            prevOid = Contract.getContractOidByVersion(objContract.ContractCustId.CustId, objContract.VehiId.VehiId, objContract.VersionNo - 1);
            if (prevOid != -1)
            {
                objContract = SCBase.searchContracts(prevOid);
                if (objContract != null)
                {
                   
                    reloadAll();
                    //loadComboboxData();
                    //loadContractData();
                }
            }
            this.disableNavigatorButton();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            int nextOid = -1;
            nextOid = Contract.getContractOidByVersion(objContract.ContractCustId.CustId, objContract.VehiId.VehiId, objContract.VersionNo + 1);
            if (nextOid != -1)
            {
                objContract = SCBase.searchContracts(nextOid);
                if (objContract != null)
                {
                    
                    reloadAll();
                    //loadComboboxData();
                    //loadContractData();
                }
            }
            this.disableNavigatorButton();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            dlgPrintContract printer = new dlgPrintContract(objContract);
            printer.ShowDialog();
        }

        private void ContractFrm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (Form.ModifierKeys == Keys.None && keyData == Keys.Escape)
            {
                this.Close();
                return true;
            }
            return base.ProcessDialogKey(keyData);
        }

        private void ContractFrm_FormClosed(object sender, FormClosedEventArgs e)
        {
            //
            SCMain.getInstance().searchContract();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        public void reloadAll()
        {
            List<SCInvoice> lstContractInvoice = new List<SCInvoice>();

            if (SCMain.ContractOid > 0)
            {
                objContract = SCBase.searchContracts(SCMain.ContractOid);
                objContract.listContractOptions = ContractOption.getContractOption(objContract.ContractOID);

                //Loc du lieu bi sai
                List<ContractOption> listWrong = new List<ContractOption>();
                Dictionary<String, String> dicContractOptions = new Dictionary<String, String>();
                foreach (ContractOption tmp in objContract.listContractOptions)
                {
                    if (dicContractOptions.ContainsKey(tmp.toString()))
                    {
                        listWrong.Add(tmp);
                    }
                    else
                    {
                        dicContractOptions.Add(tmp.toString(), "1");
                    }
                }
                ///Thuc hien xoa
                objContract.deleteContractOptionWrong(objContract.ContractOID, listWrong);

                //Reload lai data
                objContract.listContractOptions = ContractOption.getContractOption(objContract.ContractOID);

                //
                List<Int32> lstInvoiceType = new List<Int32>();
                lstInvoiceType.Add(0);
                lstInvoiceType.Add(1);
                lstContractInvoice = SCInvoiceUtil.getContractInvoice(ContractFrm.objContract.ContractOID, lstInvoiceType, true);
            }
            else
            {
                objContract = new Contract();
            }
            this.loadComboboxData();
            this.loadContractData();

            myCulture = objGlobal.CultureInfo;

            //
            this.loadVehice();
            this.contractOption1.loadDataGrid();
            this.contractOption1.loadTree();
            this.contractDataFrm.fillData();
            objContract.listSCContractRemark = (objContract.listSCContractRemark == null) ? new List<SCContractRemark>() : objContract.listSCContractRemark;

            //Invoice
            invoicesFrm.setCheckDefault();
            invoicesFrm.fillDataGrid(lstContractInvoice);

            //Remark
            remarkFrm.loadRemark(objContract.listSCContractRemark);
        }
    }
}
