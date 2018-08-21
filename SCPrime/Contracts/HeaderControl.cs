using CashRegPrime;
using log4net;
using nsBaseClass;
using SCPrime.Model;
using SCPrime.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SCPrime.Contracts
{
    public partial class HeaderControl : UserControl
    {
        public delegate void SendStatus(string Message);
        static readonly ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        //public SendStatus StatusSender;
        //public static HeaderControl _instance;
        //public string status;
        public SubContractorContract subContractor;
        public List<SubContractorContract> subContractorList;
        public DateTimePicker oDateTimePicker = new DateTimePicker();

        public HeaderControl()
        {
            InitializeComponent();
            //StatusSender = new SendStatus(setStatus);

        }



        private void btnChangeStatus_Click(object sender, EventArgs e)
        {
            string currName = "";
            if (string.IsNullOrEmpty(this.txtContractStatus.Text.Trim()))
            {
                List<string> tmp = new List<string>();
                tmp.Add("M");
                tmp.Add("O");
                tmp.Add("N");
                tmp.Add("W");
                tmp.Add("A");
                tmp.Add("H");
                tmp.Add("C");
                tmp.Add("D");

                Random randNum = new Random();
                int aRandomPos = randNum.Next(tmp.Count);//Returns a nonnegative random number less than the specified maximum (firstNames.Count).

                currName = tmp[aRandomPos];
            }
            else
            {
                SCBase sc = new SCBase();

                switch (this.txtContractStatus.Text.Trim())
                {
                    case ContractStatus.ModelText:
                        currName = ContractStatus.Model;
                        break;
                    case ContractStatus.OfferText:
                        currName = ContractStatus.Offer;
                        break;
                    case ContractStatus.NewText:
                        currName = ContractStatus.New;
                        break;
                    case ContractStatus.WaitingText:
                        currName = ContractStatus.Waiting;
                        break;
                    case ContractStatus.ActiveText:
                        currName = ContractStatus.Active;
                        break;
                    case ContractStatus.OnControlText:
                        currName = ContractStatus.OnControl;
                        break;
                    case ContractStatus.DeactivatedText:
                        currName = ContractStatus.Deactivated;
                        break;

                }
            }

            ChangeStatusFrm sf = new ChangeStatusFrm();
            sf.Sender(currName);
            sf.ShowDialog();
        }




        private void btnSearchCustomer1_Click(object sender, EventArgs e)
        {
            //Invoice address
            dlgSearchCustomer searhCustomer = new dlgSearchCustomer();
            searhCustomer.Owner = this.ParentForm;
            searhCustomer.ShowDialog();

            ContractCustomer cc = new ContractCustomer();
            int id = -1;
            bool tmp = int.TryParse(searhCustomer.Custno, out id);
            if (tmp)
                cc.CustId = id;
            cc.Name = searhCustomer.CustName;
            cc.Address = searhCustomer.CustAddress;
            cc.Email = searhCustomer.CustEmail;
            cc.Phone = searhCustomer.CustPhone;

            if (!string.IsNullOrEmpty(searhCustomer.Custno))
            {
                this.txtInvoiceCusNr.Text = searhCustomer.Custno;
                this.txtInvoiceCusName.Text = searhCustomer.CustName;
                this.txtInvoiceCusAdd.Text = searhCustomer.CustAddress;
                this.txtInvoiceCusEmail.Text = searhCustomer.CustEmail;
                this.txtInvoiceCusPhone.Text = searhCustomer.CustPhone;
                ContractFrm.objContract.InvoiceCustId = cc;

            }
        }

        private void btnSearchCustomer2_Click(object sender, EventArgs e)
        {
            //Contract customer
            dlgSearchCustomer searhCustomer = new dlgSearchCustomer();
            searhCustomer.Owner = this.ParentForm;
            searhCustomer.ShowDialog();

            ContractCustomer cc = new ContractCustomer();
            int id = -1;
            bool tmp = int.TryParse(searhCustomer.Custno, out id);
            if (tmp)
                cc.CustId = id;
            cc.Name = searhCustomer.CustName;
            cc.Address = searhCustomer.CustAddress;
            cc.Email = searhCustomer.CustEmail;
            cc.Phone = searhCustomer.CustPhone;


            if (!string.IsNullOrEmpty(searhCustomer.Custno))
            {
                this.txtContractCusNr.Text = searhCustomer.Custno;
                this.txtContractCusName.Text = searhCustomer.CustName;
                this.txtContractCusAdd.Text = searhCustomer.CustAddress;
                this.txtContractCusEmail.Text = searhCustomer.CustEmail;
                this.txtContractCusPhone.Text = searhCustomer.CustPhone;


                ContractFrm.objContract.ContractCustId = cc;

                //check contract Variant
                bool chk = Contract.checkContractVariant(cc.CustId);
                this.chkContractVariant.Checked = chk;

            }

        }

        private void btnSeachEmployee1_Click(object sender, EventArgs e)
        {
            EmployeeSearchFrm esf = new EmployeeSearchFrm();
            EmployeeSearchFrm.updateFlag(1);
            esf.ShowDialog();
        }

        private void btnSeachEmployee2_Click(object sender, EventArgs e)
        {
            EmployeeSearchFrm esf = new EmployeeSearchFrm();
            EmployeeSearchFrm.updateFlag(2);
            esf.ShowDialog();
        }
        public void enableControlabc()
        {
            IEnumerable<Control> textboxControls = ViewUtils.GetAllControl(this, typeof(TextBox));
            IEnumerable<Control> checkboxControls = ViewUtils.GetAllControl(this, typeof(CheckBox));
            IEnumerable<Control> comboboxControls = ViewUtils.GetAllControl(this, typeof(ComboBox));
            foreach (Control c in textboxControls)
            {
                ((TextBox)c).ReadOnly = false;

            }
        }

        private void HeaderControl_Load(object sender, EventArgs e)
        {
            //MessageBox.Show("1");
            //this.loadComboboxData();
            //this.loadContractData();
           // this.Dock = DockStyle.Fill;

        }

        public void cbxContractType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (this.cbxContractType.SelectedValue != null)
            //{
            //    SCContractType ct = (SCContractType)this.cbxContractType.SelectedItem;
            //    if (ct != null)
            //    {
            //        if (ct.isInvoice)
            //        {
            //            this.chkInvoiceToCus.Checked = true;
            //            this.chkInvoiceToCus.ForeColor = SystemColors.ControlText;
            //        }
            //        else
            //        {
            //            this.chkInvoiceToCus.Checked = false;
            //            this.chkInvoiceToCus.ForeColor = SystemColors.ControlText;
            //        }
            //        //update contract Type
            //        ContractFrm.objContract.ContractTypeOID = ct;
            //    }
            //}
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(this.Parent.Parent.Parent.GetType().ToString());
            //if (this.Parent.Parent.Parent.GetType() == null || this.Parent.Parent.Parent.GetType() != typeof(ContractFrm))
            //    return;

            //HeaderControl hc1 = (this.Parent as TabPage).Controls["headerControl1"] as HeaderControl;
            // dgvSubcontract.DataSource = (this.Parent.Parent.Parent as ContractFrm).objContract.SubContracts;
            //hc1.dgvSubcontract.Refresh();
        }

        private void dgvSubcontract_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            //MessageBox.Show("Test");
        }
        private SubContractorContract rowToSubContractor(DataGridViewRow r)
        {
            SubContractorContract obj = new SubContractorContract();
            //obj.SuplNo = r.Cells[]

            return obj;

        }



        private void btnDelSubcontractor_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow r in dgvSubcontract.SelectedRows)
            {
                if ((bool)r.Cells["colIsDeleted"].Value)
                {
                    r.Cells["colIsDeleted"].Value = false;
                    ViewUtils.remarkHeader(r, "colIsDeleted");
                }
                else
                {
                    r.Cells["colIsDeleted"].Value = true;
                    ViewUtils.remarkHeader(r, "colIsDeleted");
                }
            }
        }

        private void dgvSubcontract_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // If any cell is clicked on the Second column which is our date Column  
            if (e.ColumnIndex == 6)
            {
                //Initialized a new DateTimePicker Control  
                oDateTimePicker = new DateTimePicker();

                //Adding DateTimePicker control into DataGridView   
                dgvSubcontract.Controls.Add(oDateTimePicker);

                // Setting the format (i.e. 2014-10-10)  
                oDateTimePicker.Format = DateTimePickerFormat.Short;

                // It returns the retangular area that represents the Display area for a cell  
                Rectangle oRectangle = dgvSubcontract.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);

                //Setting area for DateTimePicker Control  
                oDateTimePicker.Size = new Size(oRectangle.Width, oRectangle.Height);

                // Setting Location  
                oDateTimePicker.Location = new Point(oRectangle.X, oRectangle.Y);

                // An event attached to dateTimePicker Control which is fired when DateTimeControl is closed  
                oDateTimePicker.CloseUp += new EventHandler(oDateTimePicker_CloseUp);

                // An event attached to dateTimePicker Control which is fired when any date is selected  
                oDateTimePicker.TextChanged += new EventHandler(dateTimePicker_OnTextChange);

                // Now make it visible  
                oDateTimePicker.Visible = true;
            }
        }
        void oDateTimePicker_CloseUp(object sender, EventArgs e)
        {
            // Hiding the control after use   
            oDateTimePicker.Visible = false;
        }

        private void dateTimePicker_OnTextChange(object sender, EventArgs e)
        {
            CultureInfo cu = new CultureInfo(ContractFrm.myCulture);

            // Saving the 'Selected Date on Calendar' into DataGridView current cell  
            dgvSubcontract.CurrentCell.Value = oDateTimePicker.Text.ToString(cu);
        }

        private void dgvSubcontract_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            oDateTimePicker.Visible = false;
        }

        private void btnNewSelfContract_Click(object sender, EventArgs e)
        {
            DetailContractSearchFrm frm = new DetailContractSearchFrm();
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.ShowDialog();
            if (frm.myCollectiveContract != null)
            {
                //MessageBox.Show(frm.myCollectiveContract.OID.ToString());
                this.addRowToCollectiveGrid(frm.myCollectiveContract);
            }

        }
        private void addRowToCollectiveGrid(CollectiveContract cc)
        {
            //find object in list
            int index = -1;
            index = ContractFrm.objContract.SelfContracts.FindIndex(x => x.DetailContractOID == cc.DetailContractOID);
            if (index == -1)
            {
                ContractFrm.objContract.SelfContracts.Add(cc);
                var source = new BindingSource();
                source.DataSource = ContractFrm.objContract.SelfContracts;
                this.dgvSelfContract.DataSource = source;
                this.dgvSelfContract.Refresh();
            }
            else
            {
                cc.OID = ContractFrm.objContract.SelfContracts[index].OID;
                ContractFrm.objContract.SelfContracts[index] = cc;

                var source = new BindingSource();
                source.DataSource = ContractFrm.objContract.SelfContracts;
                this.dgvSelfContract.DataSource = source;
                this.dgvSelfContract.Refresh();
            }
        }

        private void btnDelSelfContract_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow r in dgvSelfContract.SelectedRows)
            {
                if ((bool)r.Cells["colIsDeletedSelf"].Value)
                {
                    r.Cells["colIsDeletedSelf"].Value = false;
                    ViewUtils.remarkHeader(r, "colIsDeletedSelf");
                }
                else
                {
                    r.Cells["colIsDeletedSelf"].Value = true;
                    ViewUtils.remarkHeader(r, "colIsDeletedSelf");
                }
            }
        }

        private void txtContractStatus_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtExtContractNr_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void txtInternalID_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void txtContracNr_TextChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void txtVersionNr_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtChanged_TextChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void txtCreated_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void txtLastInvoice_TextChanged(object sender, EventArgs e)
        {

        }

        private void chkContractVariant_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void cbxResponsibleSite_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void cbxValidWorkshop_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbxContractType_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void chkInvoiceToCus_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void dgvSubcontract_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void btnNewSubcontractor_Click(object sender, EventArgs e)
        {

        }

        private void cbxCostcenter_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
