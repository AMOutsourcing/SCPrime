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
            if (this.headerControl1.cbxContractType.SelectedValue != null)
            {
                SCContractType ct = (SCContractType)this.headerControl1.cbxContractType.SelectedItem;

                //MessageBox.Show(this.headerControl1.cbxContractType.SelectedItem.GetType().ToString());
                //if (ct.isInvoice)
                //    this.headerControl1.chkInvoiceToCus.Checked = true;
                //else
                //    this.headerControl1.chkInvoiceToCus.Checked = false;

            }
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

            this.headerControl1.txtContractStatus.Text = this.objContact.ContractStatus;
            this.headerControl1.txtInternalID.Text = this.objContact.ContractOID.ToString();
            this.headerControl1.txtContracNr.Text = this.objContact.ContractNo.ToString();
            this.headerControl1.txtExtContractNr.Text = this.objContact.ExtContractNo;
            this.headerControl1.txtVersionNr.Text = this.objContact.VersionNo.ToString();
            this.headerControl1.txtCreated.Text = this.objContact.Created.ToString();
            this.headerControl1.txtChanged.Text = this.objContact.Modified.ToString();
            this.headerControl1.txtLastInvoice.Text = this.objContact.LastInvoiceDate.ToString();

            this.headerControl1.cbxContractType.SelectedValue = this.objContact.ContractTypeOID;


        }

    }
}
