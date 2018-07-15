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
        public SendStatus Sender;

        public HeaderControl()
        {
            InitializeComponent();

            Sender = new SendStatus(GetStatus);
        }

        private void btnChangeStatus_Click(object sender, EventArgs e)
        {
            this.txtContractStatus.Text = "M";
            ChangeStatusFrm sf = new ChangeStatusFrm();
            sf.Sender(this.txtContractStatus.Text);
            sf.ShowDialog();
        }
        private void GetStatus(string Message)
        {
            MessageBox.Show(Message);
        }

        private void HeaderControl_Load(object sender, EventArgs e)
        {
           
        }
    }
}
