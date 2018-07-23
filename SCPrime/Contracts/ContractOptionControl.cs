using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SCPrime.Model;

namespace SCPrime.Contracts
{
    public partial class ContractOptionControl : UserControl
    {
        
        public ContractOptionControl()
        {
            InitializeComponent();
        }

       

        private void ContractOption_Load(object sender, EventArgs e)
        {
           
        }

        private void treeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Checked)
            {
                if(e.Node.Level == 0)// category
                {

                }
            }
            else
                MessageBox.Show("unchecked");
        }
        public SCOptionCategory getOptionCategory(int oid)
        {
            List<SCOptionCategory> myCategories = SCOptionCategory.getOptionCategoryList();
            SCOptionCategory sc = new SCOptionCategory();
            sc = myCategories.Find(x => x.OID == oid);
            return sc;
            
        }
    }
}
