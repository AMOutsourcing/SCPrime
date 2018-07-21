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
    public partial class ContractFrm : Form
    {
        public delegate void SendStatus(string Message);
        public static SendStatus Sender;

        public delegate void SearchEmployee(SCViewEmployee epl, int flag);
        public static SearchEmployee updateEmployee;

        public static ContractFrm _instance;
        public  Contract objContact;
        public List<SCContractType> contractType;

        public static ContractFrm instance
        {
            get
            {
                if (ContractFrm._instance == null || ContractFrm._instance.IsDisposed)
                {
                    ContractFrm._instance = new ContractFrm();
                }
                return ContractFrm._instance;
            }
        }


        public ContractFrm()
        {
            InitializeComponent();
            Sender = new SendStatus(GetStatus);
            updateEmployee = new SearchEmployee(UpdateEmployee);
            this.headerControl1.cbxContractType.SelectedIndexChanged += new System.EventHandler(this.headerControl1.cbxContractType_SelectedIndexChanged);

        }



        private void UpdateEmployee(SCViewEmployee epl, int flag)
        {
            if (flag == 1)//Contract responsible person
            {
                this.headerControl1.txtEmployeeID1.Text = epl.SmanId.ToString();
                this.headerControl1.txtEmployeeName1.Text = epl.Name;
                this.headerControl1.txtEmployeePhone1.Text = epl.Phone;
                this.headerControl1.txtEmployeeEmail1.Text = epl.Email;
            }
            if (flag == 2)//Contract care taking person
            {
                this.headerControl1.txtEmployeeID2.Text = epl.SmanId.ToString();
                this.headerControl1.txtEmployeeName2.Text = epl.Name;
                this.headerControl1.txtEmployeePhone2.Text = epl.Phone;
                this.headerControl1.txtEmployeeEmail2.Text = epl.Email;
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
            objContact = null;
            this.Close();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
          
        }

        //private void headerControl1_Load(object sender, EventArgs e)
        //{

        //}

        private void headerControl1_cbxContractType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (this.headerControl1.cbxContractType.SelectedValue != null)
            //{
            //    SCContractType ct = (SCContractType)this.headerControl1.cbxContractType.SelectedItem;
            //    if (ct.isInvoice)
            //        this.headerControl1.chkInvoiceToCus.Checked = true;
            //    else
            //        this.headerControl1.chkInvoiceToCus.Checked = false;

            //}
        }

        private void ContractFrm_Load(object sender, EventArgs e)
        {
            objContact = new Contract();
            this.loadComboboxData();
            this.loadContractData();

        }
        public void loadComboboxData()
        {
            SCBase sc = new SCBase();
            //load cbxContractType
            contractType = sc.getContractTypeActive();
            if (contractType != null && contractType.Count > 0)
            {
                this.headerControl1.cbxContractType.DataSource = contractType;
                this.headerControl1.cbxContractType.DisplayMember = "Name";
                this.headerControl1.cbxContractType.ValueMember = "OID";
            }

            //load cbxResponsibleSite
            List<clsBaseListItem> listTmp = sc.getAMSites();
            List<ObjTmp> lstSites = new List<ObjTmp>(listTmp.Count);
            foreach (clsBaseListItem site in listTmp)
            {
                lstSites.Add(new ObjTmp(site.strValue1, site.strText));
            }
            this.headerControl1.cbxResponsibleSite.DataSource = lstSites;
            this.headerControl1.cbxResponsibleSite.ValueMember = "id";
            this.headerControl1.cbxResponsibleSite.DisplayMember = "text";
            //load cbxCostcenter TODO
            //load cbxValidWorkshop TODO



        }
        public void loadContractData()
        {

            this.displayStatus();
            this.headerControl1.txtInternalID.Text = this.objContact.ContractOID.ToString();
            this.headerControl1.txtContracNr.Text = this.objContact.ContractNo.ToString();
            this.headerControl1.txtExtContractNr.Text = this.objContact.ExtContractNo;
            this.headerControl1.txtVersionNr.Text = this.objContact.VersionNo.ToString();
            this.headerControl1.txtCreated.Text = this.objContact.Created.ToString();
            this.headerControl1.txtChanged.Text = this.objContact.Modified.ToString();
            this.headerControl1.txtLastInvoice.Text = this.objContact.LastInvoiceDate.ToString();

            this.headerControl1.cbxContractType.SelectedValue = this.objContact.ContractTypeOID;


        }
        public void displayStatus()
        {
            switch (this.objContact.ContractStatus.Trim())
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
           if(this.tabControl1.SelectedIndex == 1)
            {
                this.loadTree();
            }
        }
    }
}
