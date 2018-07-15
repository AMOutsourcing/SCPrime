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

        public ChangeStatusFrm()
        {
            InitializeComponent();
            Sender = new GetContractStatus(GetStatus);
        }

        private void GetStatus(string Message)
        {
            this.status = Message;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this.status);
        }

        private void ChangeStatusFrm_Load(object sender, EventArgs e)
        {
            this.loadData();

            //this.cbxContractStatus.DataSource = lstModel;
            this.cbxContractStatus.ValueMember = "value";
            this.cbxContractStatus.DisplayMember = "text";
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

            List<ObjTmp> tmp = new List<ObjTmp>();
            ObjTmp m = new ObjTmp();
            ObjTmp o = new ObjTmp();
            ObjTmp n = new ObjTmp();
            ObjTmp w = new ObjTmp();
            ObjTmp a = new ObjTmp();
            ObjTmp h = new ObjTmp();
            ObjTmp c = new ObjTmp();
            ObjTmp d = new ObjTmp();
            m = lstModel.Find(x => x.value.Equals("M"));
            o = lstModel.Find(x => x.value.Equals("O"));
            n = lstModel.Find(x => x.value.Equals("N"));
            w = lstModel.Find(x => x.value.Equals("W"));
            a = lstModel.Find(x => x.value.Equals("A"));
            h = lstModel.Find(x => x.value.Equals("H"));
            c = lstModel.Find(x => x.value.Equals("C"));
            d = lstModel.Find(x => x.value.Equals("d"));

            switch (this.status)
            {
               
                case "M":
                    tmp.Add(o);
                    break;
                case "O":
                    tmp.Add(n);
                    tmp.Add(w);
                    tmp.Add(d);
                    break;
                case "N":
                    tmp.Add(w);
                    tmp.Add(d);
                    break;
                case "W":
                    tmp.Add(a);
                    break;
                case "A":
                    tmp.Add(h);
                    tmp.Add(c);
                    break;
                case "H":
                    tmp.Add(a);
                    tmp.Add(c);
                    tmp.Add(d);
                    break;
                case "C":
                    tmp.Add(d);
                    break;
            }
        }
    }
}
