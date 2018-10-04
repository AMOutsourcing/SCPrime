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
    public partial class ChangeStatusFrm : nsBaseClass.clsBaseForm
    {

        public delegate void GetContractStatus(string Message);
        public GetContractStatus Sender;
        private string status;
        private List<ObjTmp> statusList = new List<ObjTmp>();

        public ChangeStatusFrm()
        {
            InitializeComponent();
            Sender = new GetContractStatus(GetStatus);
        }

        private void GetStatus(string Message)
        {
            this.status = Message;
          //  MessageBox.Show(this.status);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ContractFrm.Sender(cbxContractStatus.Text);
            //ContractFrm.instance.BringToFront();
            this.Close() ;

        }

        private void ChangeStatusFrm_Load(object sender, EventArgs e)
        {
            statusList = new List<ObjTmp>();
            this.cbxContractStatus.DataSource = null;
            this.loadData();

            this.cbxContractStatus.DataSource = this.statusList;
            this.cbxContractStatus.ValueMember = "strValue1";
            this.cbxContractStatus.DisplayMember = "strText";
        }
        private void loadData()
        {
            SCBase sCBase = new SCBase();
            List<string> result = typeof(ContractStatusString).GetAllPublicConstantValues<string>();
            List<ObjTmp> lstModel = new List<ObjTmp>(result.Count);
            string[] words;
            string[] stringSeparators = new string[] { "-" };

            foreach (string s in result)
            {
                words = s.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
                lstModel.Add(new ObjTmp(words[0], words[1]));
            }

            //List<ObjTmp> statusList = new List<ObjTmp>();
            ObjTmp m = new ObjTmp();
            ObjTmp o = new ObjTmp();
            ObjTmp n = new ObjTmp();
            ObjTmp w = new ObjTmp();
            ObjTmp a = new ObjTmp();
            ObjTmp h = new ObjTmp();
            ObjTmp c = new ObjTmp();
            ObjTmp d = new ObjTmp();
            m = lstModel.Find(x => x.strValue1.Equals("M"));
            o = lstModel.Find(x => x.strValue1.Equals("O"));
            n = lstModel.Find(x => x.strValue1.Equals("N"));
            w = lstModel.Find(x => x.strValue1.Equals("W"));
            a = lstModel.Find(x => x.strValue1.Equals("A"));
            h = lstModel.Find(x => x.strValue1.Equals("H"));
            c = lstModel.Find(x => x.strValue1.Equals("C"));
            d = lstModel.Find(x => x.strValue1.Equals("D"));

            switch (this.status)
            {
               
                case "M":
                    statusList.Add(o);
                    break;
                case "O":
                    statusList.Add(n);
                    statusList.Add(w);
                    statusList.Add(d);
                    break;
                case "N":
                    statusList.Add(w);
                    statusList.Add(d);
                    break;
                case "W":
                    statusList.Add(a);
                    break;
                case "A":
                    statusList.Add(h);
                    statusList.Add(c);
                    break;
                case "H":
                    statusList.Add(a);
                    statusList.Add(c);
                    statusList.Add(d);
                    break;
                case "C":
                    statusList.Add(d);
                    break;
            }
            
        }

        private void ChangeStatusFrm_KeyDown(object sender, KeyEventArgs e)
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
    }
}
