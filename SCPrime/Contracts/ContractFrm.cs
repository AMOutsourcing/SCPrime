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
        public ContractFrm()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (this.tabControl1.SelectedTab == this.tabHeader)
            //{
            //    HeaderControl hc = new HeaderControl();
            //    this.tabHeader.Controls.Add(hc);
            //}
        }
    }
}
