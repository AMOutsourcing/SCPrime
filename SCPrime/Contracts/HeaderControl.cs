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
        //public SendStatus StatusSender;
        //public static HeaderControl _instance;
        //public string status;

        public HeaderControl()
        {
            InitializeComponent();
            //StatusSender = new SendStatus(setStatus);
        }
        //public static HeaderControl instance
        //{
        //    get
        //    {
        //        if (HeaderControl._instance == null || HeaderControl._instance.IsDisposed)
        //        {
        //            HeaderControl._instance = new HeaderControl();
        //        }
        //        return HeaderControl._instance;
        //    }
        //}
        //public void updateStatus(string s)
        //{
        //    this.txtContractStatus.Text = s;
        //}


        private void btnChangeStatus_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this.txtContractStatus.Text);

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

            string currName = tmp[aRandomPos];
            // this.txtContractStatus.Text = currName;

            ChangeStatusFrm sf = new ChangeStatusFrm();
            sf.Sender(currName);
            sf.ShowDialog();
        }
        //private void setStatus(string Message)
        //{
        //    if (InvokeRequired)
        //        Invoke(new Action<string>(setStatus), Message);
        //    else
        //    {
        //        if (!string.IsNullOrEmpty(Message))
        //        {
        //            this.txtContractStatus.Text = Message;
        //            this.txtInternalID.Text = Message;
        //            this.textBox6.Text = Message;
        //            this.status = Message;
        //            MessageBox.Show(Message);
        //            Control[] tmp = this.Controls.Find("txtContractStatus", true);
        //            MessageBox.Show(tmp.Count().ToString());
        //            tmp[0].Text = Message;
        //        }
        //        this.Refresh();
        //    }
        //}

        private void HeaderControl_Load(object sender, EventArgs e)
        {

        }

        private void btnSearchCustomer1_Click(object sender, EventArgs e)
        {

        }

    }
}
