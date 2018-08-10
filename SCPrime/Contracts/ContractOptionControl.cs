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
using SCPrime.Utils;

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

        public List<SCOptionCategory> myCategories = null;
        private void loadTree()
        {

        }

        DataTable dataTable = new DataTable();

        public void loadDataGrid(List<SCOptionCategory> listData)
        {
            dataTable = ObjectUtils.ConvertToDataTable(listData);
            dataGridView1.DataSource = dataTable;
        }

        private void treeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Checked)
            {
                Console.WriteLine("CHECK: " + e.Node.Name);
                if(e.Node.Level == 0)// category
                {
                    var item = getOptionCategory(e.Node.Text);
                    if (item != null)
                    {
                        DataRow drToAdd = ObjectUtils.FillDataToRow(dataTable.NewRow(),item);

                        dataTable.Rows.Add(drToAdd);

                        //int idx = this.GetIndexOfRowCategory(this.dataGridView1, item.OID);
                        //if (idx > -1)
                        //{
                        //    this.dataGridView1.Rows[idx].Cells[1].Selected = true;
                        //    this.dataGridView1.BeginEdit(true);
                        //}
                    }
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

        public SCOptionCategory getOptionCategory(string text)
        {
            var item = ContractFrm.myCategories.Find(x => x.Name == text);
            return item;
        }

        private int GetIndexOfRowCategory(DataGridView dataGrid, int id)
        {
            for (int i = 0; i < dataGrid.Rows.Count; i += 1)
            {
                SCOptionCategory row = new SCOptionCategory(); // or.DataBoundItem;
                row = this.RowToCategory(dataGrid.Rows[i]);
                if (row.OID == id)
                {
                    return i;
                }
            }

            return 0;
        }

        public SCOptionCategory RowToCategory(DataGridViewRow row)
        {
            SCOptionCategory sc = new SCOptionCategory();
            int number = -1;
            bool tmp = Int32.TryParse(row.Cells[0].Value.ToString(), out number);

            if (tmp)
                sc.OID = number;
            else
                sc.OID = -1;
            return sc;
        }
    }
}
