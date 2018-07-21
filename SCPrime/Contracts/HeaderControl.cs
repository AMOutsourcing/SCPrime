﻿using CashRegPrime;
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
    public partial class HeaderControl : UserControl
    {
        public delegate void SendStatus(string Message);
        static readonly ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        //public SendStatus StatusSender;
        //public static HeaderControl _instance;
        //public string status;

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
            dlgSearchCustomer searhCustomer = new dlgSearchCustomer();
            searhCustomer.Owner = this.ParentForm;
            searhCustomer.ShowDialog();
            if (searhCustomer.Custno != "")
            {
                this.txtInvoiceCusNr.Text = searhCustomer.Custno;
                this.txtInvoiceCusName.Text = searhCustomer.CustName;
                this.txtInvoiceCusAdd.Text = searhCustomer.CustAddress;
                this.txtInvoiceCusEmail.Text = searhCustomer.CustEmail;
                this.txtInvoiceCusPhone.Text = searhCustomer.CustPhone;

            }
        }

        private void btnSearchCustomer2_Click(object sender, EventArgs e)
        {
            dlgSearchCustomer searhCustomer = new dlgSearchCustomer();
            searhCustomer.Owner = this.ParentForm;
            searhCustomer.ShowDialog();
            if (searhCustomer.Custno != "")
            {
                this.txtContractCusNr.Text = searhCustomer.Custno;
                this.txtContractCusName.Text = searhCustomer.CustName;
                this.txtContractCusAdd.Text = searhCustomer.CustAddress;
                this.txtContractCusEmail.Text = searhCustomer.CustEmail;
                this.txtContractCusPhone.Text = searhCustomer.CustPhone;

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

        }

        public void cbxContractType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cbxContractType.SelectedValue != null)
            {
                SCContractType ct = (SCContractType)this.cbxContractType.SelectedItem;
                if (ct.isInvoice)
                {
                    this.chkInvoiceToCus.Checked = true;
                    this.chkInvoiceToCus.ForeColor = SystemColors.ControlText;
                }
                else
                {
                    this.chkInvoiceToCus.Checked = false;
                    this.chkInvoiceToCus.ForeColor = SystemColors.ControlText;
                }

            }
        }
    }
}
