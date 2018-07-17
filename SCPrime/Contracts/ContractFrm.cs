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
        public static ContractFrm _instance;



        public ContractFrm()
        {
            InitializeComponent();
            Sender = new SendStatus(GetStatus);
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
            this.Close();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
    }
}
