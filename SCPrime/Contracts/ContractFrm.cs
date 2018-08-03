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
        public delegate void SendStatus(string Message);
        public static SendStatus Sender;

        public delegate void SearchEmployee(SCViewEmployee epl, int flag);
        public static SearchEmployee updateEmployee;

        //public static ContractFrm _instance;
        public static Contract objContract;
        public List<SCContractType> contractType;
        public List<clsBaseListItem> costCenterList = new List<clsBaseListItem>();
        public List<clsBaseListItem> ws = new List<clsBaseListItem>();

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
            Sender = new SendStatus(GetStatus);
            updateEmployee = new SearchEmployee(UpdateEmployee);
            this.headerControl1.cbxContractType.SelectedIndexChanged += new System.EventHandler(this.headerControl1.cbxContractType_SelectedIndexChanged);
            this.headerControl1.btnNewSubcontractor.Click += new System.EventHandler(this.btnNewSubcontractor_click);
            this.headerControl1.dgvSubcontract.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSubcontract_CellValidated);
            this.headerControl1.txtContractStatus.TextChanged += new System.EventHandler(this.updat_satatus);
            this.headerControl1.cbxResponsibleSite.SelectedIndexChanged += new System.EventHandler(this.cbxResponsibleSite_SelectedIndexChanged);
            this.headerControl1.cbxCostcenter.SelectedIndexChanged += new System.EventHandler(this.cbxCostcenter_SelectedIndexChanged);
            this.headerControl1.cbxValidWorkshop.SelectedIndexChanged += new System.EventHandler(this.cbxValidWorkshop_SelectedIndexChanged);

            if (SCMain.ContractOid > 0)
            {
                objContract = SCBase.searchContracts(SCMain.ContractOid);
            }
            else
            {
                objContract = new Contract();
            }
            this.loadComboboxData();
            this.loadContractData();
            // this.loadCustomerEmployee();
        }

        private void cbxValidWorkshop_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.headerControl1.cbxValidWorkshop.SelectedIndex >= 0)
            {
                if (this.headerControl1.cbxValidWorkshop.SelectedItem != null)
                {
                    ObjTmp s = (ObjTmp)this.headerControl1.cbxValidWorkshop.SelectedItem;
                    objContract.ValidWorkshopCode = findclsBaseListItem(s.value, this.ws);
                    //MessageBox.Show(s.value);
                }

            }
        }

        //private clsBaseListItem findValidWorkshop(string value, List<clsBaseListItem> list)
        //{
        //    clsBaseListItem Result = new clsBaseListItem();
        //    Result = list.Find(x => x.strValue1 == value);
        //    return Result;
        //}

        private void cbxCostcenter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.headerControl1.cbxCostcenter.SelectedIndex >= 0)
            {
                if (this.headerControl1.cbxCostcenter.SelectedItem != null)
                {
                    ObjTmp s = (ObjTmp)this.headerControl1.cbxCostcenter.SelectedItem;
                    objContract.CostCenter = findclsBaseListItem(s.value, this.costCenterList);
                    //MessageBox.Show(s.value);
                }

            }
        }
        public clsBaseListItem findclsBaseListItem(string val, List<clsBaseListItem> list)
        {
            clsBaseListItem Result = new clsBaseListItem();
            Result = list.Find(x => x.strValue1 == val);
            return Result;
        }

        private void cbxResponsibleSite_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.headerControl1.cbxResponsibleSite.SelectedIndex >= 0)
            {
                if (this.headerControl1.cbxResponsibleSite.SelectedItem != null)
                {
                    ObjTmp s = (ObjTmp)this.headerControl1.cbxResponsibleSite.SelectedItem;
                    clsBaseListItem site = null;
                    site = SCBase.findSite(s.value);
                    if (site != null)
                        objContract.SiteId = site;
                    //MessageBox.Show(s.value);
                }

            }
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
                sc.OID = -1;

            sc.SubcontractNo = row.Cells["colSubcontractNo"].Value != null ? row.Cells["colSubcontractNo"].Value.ToString() : "";
            sc.Info = row.Cells["colInfo"].Value != null ? row.Cells["colInfo"].Value.ToString() : "";
            sc.Expl = row.Cells["colExpl"].Value != null ? row.Cells["colExpl"].Value.ToString() : "";
            if ((DateTime)(row.Cells["colDateLimit"].Value) > DateTime.MinValue )
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

            sc.isDeleted = (bool)row.Cells["colIsDeleted"].Value ;

            clsBaseListItem t = new clsBaseListItem();
            t.strValue1 = sc.SuplNoVal;
            t.strText = sc.SuplName;
            sc.SuplNo = t;

            return sc;
        }


        private void btnNewSubcontractor_click(object sender, EventArgs e)
        {
            //MessageBox.Show("Test");
            objContract.SubContracts.Add(new SubContractorContract());
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
            this.headerControl1.cbxResponsibleSite.ValueMember = "value";
            this.headerControl1.cbxResponsibleSite.DisplayMember = "text";

            if (!string.IsNullOrEmpty(objContract.ResponsibleSite))
            {
                this.headerControl1.cbxResponsibleSite.SelectedValue = objContract.ResponsibleSite;
            }

            //load cbxContractType
            contractType = sc.getContractTypeActive();
            if (contractType != null && contractType.Count > 0)
            {
                this.headerControl1.cbxContractType.DataSource = contractType;
                this.headerControl1.cbxContractType.ValueMember = "OID";
                this.headerControl1.cbxContractType.DisplayMember = "Name";
                this.headerControl1.cbxContractType.SelectedItem = contractType[0];

                if (objContract.ContractTypeOID != null)
                {
                    this.headerControl1.cbxContractType.SelectedValue = objContract.ContractTypeOID.OID;
                }
            }

            //load cbxCostcenter TODO
            costCenterList = Contract.getCostCenter();

            if (costCenterList != null && costCenterList.Count > 0)
            {
                List<ObjTmp> myccs = new List<ObjTmp>(costCenterList.Count);
                foreach (clsBaseListItem cc in costCenterList)
                {
                    myccs.Add(new ObjTmp(cc.strValue1, cc.strText));
                }
                this.headerControl1.cbxCostcenter.DataSource = myccs;
                this.headerControl1.cbxCostcenter.ValueMember = "value";
                this.headerControl1.cbxCostcenter.DisplayMember = "text";

                if (objContract.CostCenter != null)
                {
                    this.headerControl1.cbxCostcenter.SelectedValue = objContract.CostCenter.strValue1;
                }
            }
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
                this.headerControl1.cbxValidWorkshop.ValueMember = "value";
                this.headerControl1.cbxValidWorkshop.DisplayMember = "text";

                if (objContract.ValidWorkshopCode != null)
                {
                    this.headerControl1.cbxValidWorkshop.SelectedValue = objContract.ValidWorkshopCode.strValue1;
                }
            }



        }
        public void loadContractData()
        {

            this.displayStatus();

            this.headerControl1.txtInternalID.Text = objContract.ContractOID.ToString();
            this.headerControl1.txtContracNr.Text = objContract.ContractNo.ToString();
            this.headerControl1.txtExtContractNr.Text = objContract.ExtContractNo;
            this.headerControl1.txtVersionNr.Text = objContract.VersionNo.ToString();
            this.headerControl1.txtCreated.Text = objContract.Created.ToString();
            this.headerControl1.txtChanged.Text = objContract.Modified.ToString();
            this.headerControl1.txtLastInvoice.Text = objContract.LastInvoiceDate.ToString();

            this.headerControl1.cbxContractType.SelectedValue = objContract.ContractTypeOID;

            this.headerControl1.chkContractVariant.Checked = Contract.checkContractVariant(objContract.ContractCustId.CustId);


            this.loadDetail();
            this.addSupplierCbx();
            this.loadCustomerEmployee();


        }
        public void loadCustomerEmployee()
        {
            //loadInVoiceAddress
            ContractCustomer ccInvoice = new ContractCustomer();
            ccInvoice = objContract.InvoiceCustId;
            if (ccInvoice != null)
            {

                this.headerControl1.txtInvoiceCusNr.Text = ccInvoice.CustId.ToString();
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

                this.headerControl1.txtContractCusNr.Text = cc.CustId.ToString();
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
            cte = objContract.RespSmanId;
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

                List<ObjTmp> ls = new List<ObjTmp>();
                ls = SubContractorContract.getSuppliers();
                if (ls.Count > 0)
                {

                    DataTable dt = new DataTable();
                    dt = ObjectUtils.ConvertToDataTable(ls);

                    DataGridViewComboBoxColumn cb = new DataGridViewComboBoxColumn();
                    cb.HeaderText = "Supplier";
                    cb.Name = "colSupplier";
                    cb.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    cb.DataSource = dt;
                    cb.DataPropertyName = "SuplNoVal";
                    cb.ValueMember = "value";
                    cb.DisplayMember = "text";
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
            }

            addSupplierCbx();
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

        private void loadTree()
        {
            //load category
            this.contractOption1.treeView1.Nodes.Clear();

            List<SCOptionCategory> myCategories = SCOptionCategory.getOptionCategoryList();
            if (myCategories.Count > 0)
            {
                foreach (SCOptionCategory cat in myCategories)
                {
                    TreeNode treeNode = new TreeNode(cat.Name);
                    treeNode.Name = cat.GetType().ToString() + cat.OID.ToString();

                    this.contractOption1.treeView1.Nodes.Add(treeNode);

                    //load all Option
                    List<SCOption> myOptions = new List<SCOption>();
                    myOptions = SCOption.getOptionList(cat.OID);
                    if (myOptions.Count > 0)
                    {
                        foreach (SCOption op in myOptions)
                        {
                            TreeNode treeNodeL2 = new TreeNode(op.Name);
                            treeNodeL2.Name = op.GetType().ToString() + op.OID.ToString();
                            treeNode.Nodes.Add(treeNodeL2);
                            //load all detail
                            List<SCOptionDetail> myOptionDetails = new List<SCOptionDetail>();
                            myOptionDetails = SCOptionDetail.getOptionDetailList(op.OID);
                            if (myOptionDetails.Count > 0)
                            {
                                foreach (SCOptionDetail sod in myOptionDetails)
                                {
                                    // create childnode level3
                                    TreeNode treeNodeL3 = new TreeNode(sod.Name);
                                    treeNodeL3.Name = sod.GetType().ToString() + sod.OID.ToString();
                                    treeNodeL2.Nodes.Add(treeNodeL3);
                                }
                            }
                        }
                    }
                }
            }
            //this.treeView1.ExpandAll();

        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("---------------------tabControl1_Selected: " + this.tabControl1.SelectedIndex);


            if (this.tabControl1.SelectedIndex == 1)

            {
                System.Diagnostics.Debug.WriteLine("---------------------tabControl1_Selected loadVehice: ");
                this.loadVehice();
            }
            else if (this.tabControl1.SelectedIndex == 2)
            {
                this.loadTree();
            }
            else if (this.tabControl1.SelectedIndex == 3)
            {
                contractDataFrm.setContract(objContract);
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
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.updateContract();

            if (objContract.VehiId.VehiId <= 0)
            {
                // MessageBox.Show("Select Vehicle");
                return;
            }
            bool tmp = false;
            tmp = objContract.saveContract();
            MessageBox.Show(tmp.ToString());
            this.loadComboboxData();
            this.loadContractData();
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
                    oldContract.saveContract();

                    //  new object
                    objContract.ContractOID = 0;
                    objContract.VersionNo = objContract.VersionNo + 1;
                    this.updateContract();
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

        private void btnCopy_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to copy this contract?", "Warning", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                if (objContract.ContractOID != 0)
                {
                    //  new object
                    objContract.ContractOID = 0;
                    objContract.ContractNo = 0;
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
                MessageBox.Show("Ha ha");
            }
        }
    }
}
