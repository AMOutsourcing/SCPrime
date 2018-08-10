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

        public List<SCOptionPrice> myCategories = null;
        SCContractType SCContractType = null;

        public void loadTree()
        {
            SCContractType = ContractFrm.objContract.ContractTypeOID;
            myCategories = SCContractType.getOptionPriceList(SCContractType.OID);
            buidTree();
        }

        private void buidTree()
        {
            List<SCOptionPrice> listToClone = myCategories.ToList();

            IEnumerable<SCOptionPrice> listCategory = myCategories.Where(s => !s.NotAvailable && s.OptionOID <= 0 && s.OptionDetailOID <= 0);

            IEnumerable<SCOptionPrice> listOptions;
            IEnumerable<SCOptionPrice> listOptionDetails;


            treeView1.Nodes.Clear();
            if (myCategories.Count > 0)
            {
                foreach (SCOptionPrice cat in listCategory)
                {
                    TreeNode treeNode = new TreeNode(cat.CategoryName);
                    treeNode.Name = "C" + cat.CategoryOID.ToString();
                    if (cat.Include)
                    {
                        treeNode.Checked = true;
                    }
                    treeView1.Nodes.Add(treeNode);
                    //get option
                    listOptions = myCategories.Where(s => !s.NotAvailable && s.CategoryOID == cat.CategoryOID && s.OptionOID > 0 && s.OptionDetailOID <= 0);

                    foreach (SCOptionPrice op in listOptions)
                    {
                        TreeNode treeNodeL2 = new TreeNode(op.OptionName);
                        treeNodeL2.Name = "O" + op.OptionOID.ToString();
                        if (op.Include)
                        {
                            treeNodeL2.Checked = true;
                        }
                        treeNode.Nodes.Add(treeNodeL2);
                        //load all detail
                        listOptionDetails = myCategories.Where(s => !s.NotAvailable && s.CategoryOID == cat.CategoryOID && s.OptionOID == op.OptionOID && s.OptionDetailOID > 0);

                        foreach (SCOptionPrice sod in listOptionDetails)
                        {
                            // create childnode level3
                            TreeNode treeNodeL3 = new TreeNode(sod.OptionDetailName);
                            treeNodeL3.Name = "D" + sod.OptionDetailOID.ToString();
                            if (sod.Include)
                            {
                                treeNodeL3.Checked = true;
                            }
                            treeNodeL2.Nodes.Add(treeNodeL3);
                        }
                    }

                }
            }
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
                var item = getOptionBase(e.Node.Name, e.Node.Text);
                if (item != null)
                {
                    DataRow drToAdd = ObjectUtils.FillDataToRow(dataTable.NewRow(), item);
                    dataTable.Rows.Add(drToAdd);
                }
            }
            else
                MessageBox.Show("unchecked");
        }

        public SCOptionBase getOptionBase(string name, string text)
        {
            string type = name.Substring(0, 1);
            Int32 value = Int32.Parse(name.Substring(1));
            if (type.Equals("C"))
            {
                return SCOptionCategory.getByOID(value);
            }
            else if (type.Equals("D"))
            {
                return SCOption.getByOID(value);
            }
            else
            {
                return SCOptionDetail.getByOID(value);
            }
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
