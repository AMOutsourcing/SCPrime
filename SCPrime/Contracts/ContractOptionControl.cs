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

        public void buildTable()
        {
            //Buid table
            dataTable = ObjectUtils.BuidDataTable(new SCOptionDetail());
        }

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
                    if (ContractFrm.objContract.ContractOID <= 0)
                    {
                        if (cat.Include)
                        {
                            //Include -> check true and add grid
                            treeNode.Checked = true;
                        }
                    }
                    treeView1.Nodes.Add(treeNode);
                    if (ContractFrm.objContract.ContractOID <= 0)
                    {
                        if (cat.Include)
                        {
                            //Include -> check true and add grid
                            addRow(treeNode);
                        }
                    }
                    //get option
                    listOptions = myCategories.Where(s => !s.NotAvailable && s.CategoryOID == cat.CategoryOID && s.OptionOID > 0 && s.OptionDetailOID <= 0);

                    foreach (SCOptionPrice op in listOptions)
                    {
                        TreeNode treeNodeL2 = new TreeNode(op.OptionName);
                        treeNodeL2.Name = "O" + op.OptionOID.ToString();
                        if (ContractFrm.objContract.ContractOID <= 0)
                        {
                            if (op.Include)
                            {
                                //Include -> check true and add grid
                                treeNodeL2.Checked = true;
                            }
                        }
                        treeNode.Nodes.Add(treeNodeL2);

                        if (ContractFrm.objContract.ContractOID <= 0)
                        {
                            if (op.Include)
                            {
                                //Include -> check true and add grid
                                addRow(treeNodeL2);
                            }
                        }

                        //load all detail
                        listOptionDetails = myCategories.Where(s => !s.NotAvailable && s.CategoryOID == cat.CategoryOID && s.OptionOID == op.OptionOID && s.OptionDetailOID > 0);

                        foreach (SCOptionPrice sod in listOptionDetails)
                        {
                            // create childnode level3
                            TreeNode treeNodeL3 = new TreeNode(sod.OptionDetailName);
                            treeNodeL3.Name = "D" + sod.OptionDetailOID.ToString();
                            if (ContractFrm.objContract.ContractOID <= 0)
                            {
                                if (sod.Include)
                                {
                                    //Include -> check true and add grid
                                    treeNodeL3.Checked = true;
                                }
                            }
                            treeNodeL2.Nodes.Add(treeNodeL3);

                            if (ContractFrm.objContract.ContractOID <= 0)
                            {
                                if (op.Include)
                                {
                                    //Include -> check true and add grid
                                    addRow(treeNodeL3);
                                }
                            }
                        }
                    }
                }
            }

            //Set checked cho 
            //checkTree();
        }

        private void checkTree()
        {
            if(listOptionDetailTmp.Count > 0)
            {
                TreeNode[] treeNodes = null;
                //Duyet list
                string name = "";
                foreach (SCOptionDetail detail in listOptionDetailTmp)
                {
                    if(detail.OID > 0)
                    {
                        name = "D" + detail.OID;
                    }
                    else
                    {
                        if (detail.optionOID > 0)
                        {
                            name = "O" + detail.optionOID;
                        }
                        else
                        {
                            name = "C" + detail.categoryOID;
                        }
                    }
                    treeNodes = treeView1.Nodes
                                    .Cast<TreeNode>()
                                    .Where(r => r.Name == name)
                                    .ToArray();

                    foreach (TreeNode node in treeNodes)
                    {
                        node.Checked = true;
                    }
                }
            }
        }

        DataTable dataTable = new DataTable();


        List<SCOptionDetail> listOptionDetail = new List<SCOptionDetail>();
        List<SCOptionDetail> listOptionDetailTmp = new List<SCOptionDetail>();

        List<SCOptionCategory> OptionCategories = new List<SCOptionCategory>();

        public void loadDataGrid()
        {
            buildTable();
            OptionCategories = ContractFrm.objContract.OptionCategories;
            fillToListOptionDetail();
            dataTable = ObjectUtils.ConvertToDataTable(listOptionDetail);
            dataGridView1.DataSource = dataTable;
        }

        private void fillToListOptionDetail()
        {
            listOptionDetail.Clear();
            listOptionDetailTmp.Clear();
            if (OptionCategories.Count > 0)
            {
                foreach (SCOptionCategory cate in OptionCategories)
                {
                    listOptionDetailTmp.Add(cate.convertToDetail());
                    foreach (SCOption option in cate.Options)
                    {
                        listOptionDetailTmp.Add(option.convertToDetail());
                        foreach (SCOptionDetail detail in option.OptionDetails)
                        {
                            listOptionDetailTmp.Add(detail);
                        }
                    }
                }
            }

        }

        private void treeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Checked)
            {
                addRow(e.Node);
            }
            else
            {
                removeRow(e.Node);
            }
        }

        private void addRow(TreeNode Node)
        {
            var item = getOptionBase(Node.Name, Node.Text);
            if (item != null)
            {
                DataRow drToAdd = ObjectUtils.FillDataToRow(dataTable.NewRow(), item);
                dataTable.Rows.Add(drToAdd);
            }
            //Add List OptionCategories
            if (item.GetType() == typeof(SCOptionCategory))
            {
                listOptionDetail.Add(((SCOptionCategory)item).convertToDetail());
            }
            else if (item.GetType() == typeof(SCOption))
            {
                TreeNode parent = Node.Parent;
                Int32 cateId = Int32.Parse(parent.Name.Substring(1));
                SCOption option = (SCOption)item;
                option.categoryOID = cateId;
                listOptionDetail.Add(option.convertToDetail());
            }
            else
            {
                TreeNode scOption = Node.Parent;
                Int32 optionOID = Int32.Parse(scOption.Name.Substring(1));

                TreeNode scCate = scOption.Parent;
                Int32 cateOID = Int32.Parse(scCate.Name.Substring(1));

                SCOptionDetail detail = (SCOptionDetail)item;
                detail.categoryOID = cateOID;
                detail.optionOID = optionOID;
                listOptionDetail.Add(detail);
            }
        }

        private void removeRow(TreeNode Node)
        {
            string name = Node.Name;
            string type = name.Substring(0, 1);
            Int32 value = Int32.Parse(name.Substring(1));
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                DataRow recRow = dataTable.Rows[i];
                if (value.Equals(recRow["OID"]) && type.Equals(recRow["type"]))
                {
                    recRow.Delete();
                    dataTable.AcceptChanges();
                    break;
                }
            }

            //
            //Remove List OptionCategories
            var item = getOptionBase(Node.Name, Node.Text);
            if (item != null)
            {
                if (item.GetType() == typeof(SCOptionCategory))
                {
                    listOptionDetail.Remove(((SCOptionCategory)item).convertToDetail());
                }
                else if (item.GetType() == typeof(SCOption))
                {
                    TreeNode parent = Node.Parent;
                    Int32 cateId = Int32.Parse(parent.Name.Substring(1));
                    SCOption option = (SCOption)item;
                    option.categoryOID = cateId;
                    listOptionDetail.Remove(option.convertToDetail());
                }
                else
                {
                    TreeNode scOption = Node.Parent;
                    Int32 optionOID = Int32.Parse(scOption.Name.Substring(1));

                    TreeNode scCate = scOption.Parent;
                    Int32 cateOID = Int32.Parse(scCate.Name.Substring(1));

                    SCOptionDetail detail = (SCOptionDetail)item;
                    detail.categoryOID = cateOID;
                    detail.optionOID = optionOID;
                    listOptionDetail.Remove(detail);
                }
            }
        }

        public SCOptionBase getOptionBase(string name, string text)
        {
            string type = name.Substring(0, 1);
            Int32 value = Int32.Parse(name.Substring(1));
            if (type.Equals("C"))
            {
                return SCOptionCategory.getByOID(value);
            }
            else if (type.Equals("O"))
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


        public void saveOptionCategories()
        {
            ContractFrm.objContract.listOptionDetail = this.listOptionDetail;
        }

        private void treeView1_AfterExpand(object sender, TreeViewEventArgs e)
        {

        }
    }
}
