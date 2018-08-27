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
using nsBaseClass;

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
            dataTable = ObjectUtils.BuidDataTable(new ContractOption());
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
                    else
                    {
                        treeNode.Checked = checkTree(treeNode, cat);
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
                    else
                    {
                        if (checkTree(treeNode, cat)) addRow(treeNode);
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
                        else
                        {
                            treeNodeL2.Checked = checkTree(treeNodeL2, op);
                        }

                        //Check parent
                        if (!treeNode.Checked)
                        {
                            treeNode.Checked = true;
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
                        else
                        {
                            if (checkTree(treeNodeL2, op)) addRow(treeNodeL2);
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
                            else
                            {
                                treeNodeL3.Checked = checkTree(treeNodeL3, sod);
                            }

                            //Check parent
                            if (!treeNodeL2.Checked)
                            {
                                treeNodeL2.Checked = true;
                            }

                            if (!treeNode.Checked)
                            {
                                treeNode.Checked = true;
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
                            else
                            {
                                if (checkTree(treeNodeL3, sod)) addRow(treeNodeL3);
                            }
                        }
                    }
                }
            }
        }

        private bool checkTree(TreeNode node, SCOptionPrice scprice)
        {
            if (listOptionDetailTmp.Count <= 0)
            {
                if (scprice.Include)
                {
                    return true;
                }
            }
            else
            {
                if (listOptionDetailTmp.ContainsKey(node.Name))
                {
                    return true;
                }
            }
            return false;
        }

        DataTable dataTable = new DataTable();


        List<ContractOption> listOptionDetail = new List<ContractOption>();
        Dictionary<String, String> listOptionDetailTmp = new Dictionary<String, String>();

        public void loadDataGrid()
        {
            buildTable();
            fillToListOptionDetail();
            dataTable = ObjectUtils.ConvertToDataTable(listOptionDetail);
            dataGridView1.DataSource = dataTable;
        }

        private void fillToListOptionDetail()
        {
            listOptionDetail.Clear();
            listOptionDetailTmp.Clear();
            if (ContractFrm.objContract.listContractOptions != null)
            {
                if (ContractFrm.objContract.listContractOptions.Count > 0)
                {
                    foreach (ContractOption cate in ContractFrm.objContract.listContractOptions)
                    {
                        addToDic(cate);
                    }
                }
            }

        }

        private void addToDic(ContractOption cate)
        {
            try
            {
                string key = formatKey(cate);
                listOptionDetailTmp.Add(key, "1");
            }
            catch (Exception ex)
            {
                Console.WriteLine("----addToDic Exception: " + ex.Message + " " + cate.OptionCategoryOID + " - " + cate.OptionOID + " " + cate.OptionDetailOID);
            }
        }

        private string formatKey(ContractOption cate)
        {
            if (cate.OptionDetailOID > 0)
            {
                return "D" + cate.OptionDetailOID;
            }
            if (cate.OptionOID > 0)
            {
                return "O" + cate.OptionOID;
            }
            return "C" + cate.OptionCategoryOID;
        }


        private void removeFromDic(ContractOption cate)
        {
            try
            {
                if (listOptionDetailTmp != null && listOptionDetailTmp.Count > 0)
                {
                    string key = formatKey(cate);
                    if (listOptionDetailTmp.ContainsKey(key))
                    {
                        listOptionDetailTmp.Remove(key);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("----removeFromDic Exception: " + ex.Message + " " + cate.OptionCategoryOID + " - " + cate.OptionOID + " " + cate.OptionDetailOID);
            }
        }

        private void removeFromDic(TreeNode Node)
        {
            try
            {
                if (listOptionDetailTmp != null && listOptionDetailTmp.Count > 0)
                {
                    if (listOptionDetailTmp.ContainsKey(Node.Name))
                    {
                        listOptionDetailTmp.Remove(Node.Name);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("----removeFromDic Exception: " + ex.Message + " " + Node.Name);
            }
        }

        private void treeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {

            TreeNode node = e.Node;
            int level = node.Level;
            Console.WriteLine("----------------------treeView1_AfterCheck: " + e.Node.Name + " - " + e.Node.Level);
            if (node.Checked)
            {
                addRow(node);
                if (level == 1)
                {
                    TreeNode parent = null;
                    parent = node.Parent;
                    if (parent != null && !parent.Checked)
                        parent.Checked = true;
                }
                else if (level == 2)
                {
                    TreeNode parent1 = null;
                    parent1 = node.Parent;
                    if (!parent1.Checked)
                    {
                        parent1.Checked = true;
                        TreeNode parent2 = parent1.Parent;
                        if (!parent2.Checked)
                            parent2.Checked = true;
                    }
                }
            }
            else
            {
                removeRow(node);

                //Comment chờ confirm có cần
                if (level == 0)
                {
                    foreach (TreeNode tn in node.Nodes)
                    {
                        if (tn.Checked)
                        {
                            tn.Checked = false;
                            foreach (TreeNode detail in tn.Nodes)
                            {
                                if (detail.Checked)
                                    detail.Checked = false;
                            }
                        }
                    }

                }
                else if (level == 1)
                {
                    foreach (TreeNode tn in node.Nodes)
                    {
                        if (tn.Checked)
                            tn.Checked = false;
                    }

                }
            }
        }


        private void addRow(TreeNode Node)
        {
            var item = getOptionBase(Node.Name, Node.Text);
            if (item != null)
            {
                //Add List OptionCategories
                ContractOption rtn = null;
                if (item.GetType() == typeof(SCOptionCategory))
                {
                    rtn = new ContractOption();
                    rtn.OptionCategoryOID = item.OID;
                    rtn.Name = item.Name;
                    rtn.PartNr = item.ItemNo;
                    rtn.PartName = item.ItemName;
                    rtn.LabourCode = item.WrksId;
                    rtn.LabourName = item.WrksName;
                    rtn.BaseSelPr = item.BaseSelPr;
                    rtn.PurchasePr = item.BuyPr;
                }
                else if (item.GetType() == typeof(SCOption))
                {
                    rtn = new ContractOption();
                    TreeNode parent = Node.Parent;
                    Int32 cateId = Int32.Parse(parent.Name.Substring(1));
                    SCOption option = (SCOption)item;
                    rtn.OptionCategoryOID = cateId;
                    rtn.OptionOID = option.OID;
                    rtn.Name = option.Name;
                    rtn.PartNr = option.ItemNo;
                    rtn.PartName = option.ItemName;
                    rtn.LabourCode = option.WrksId;
                    rtn.LabourName = option.WrksName;
                    rtn.BaseSelPr = option.BaseSelPr;
                    rtn.PurchasePr = option.BuyPr;
                }
                else
                {
                    rtn = new ContractOption();
                    TreeNode scOption = Node.Parent;
                    Int32 optionOID = Int32.Parse(scOption.Name.Substring(1));

                    TreeNode scCate = scOption.Parent;
                    Int32 cateOID = Int32.Parse(scCate.Name.Substring(1));

                    SCOptionDetail detail = (SCOptionDetail)item;

                    rtn.OptionCategoryOID = cateOID;
                    rtn.OptionOID = optionOID;
                    rtn.OptionDetailOID = detail.OID;
                    rtn.Name = detail.Name;
                    rtn.PartNr = detail.ItemNo;
                    rtn.PartName = detail.ItemName;
                    rtn.LabourCode = detail.WrksId;
                    rtn.LabourName = detail.WrksName;
                    rtn.BaseSelPr = detail.BaseSelPr;
                    rtn.PurchasePr = detail.BuyPr;
                }


                if (rtn != null)
                {
                    //Update info
                    try
                    {
                        ContractOption finded = ContractFrm.objContract.listContractOptions.Single(s => s.OptionCategoryOID == rtn.OptionCategoryOID && s.OptionOID == rtn.OptionOID && s.OptionDetailOID == rtn.OptionDetailOID);
                        rtn.Info = finded.Info;
                        rtn.PartialPayer = finded.PartialPayer;
                        rtn.Quantity = finded.Quantity;
                        rtn.SalePr = finded.SalePr;
                    }
                    catch (System.InvalidOperationException)
                    {
                        Console.WriteLine("The collection does not contain exactly one element.");
                    }

                    if (!listOptionDetailTmp.ContainsKey(Node.Name))
                    {
                        addToDic(rtn);
                    }

                    //Add to grid
                    try
                    {
                        ContractOption finded = listOptionDetail.Single(s => s.OptionCategoryOID == rtn.OptionCategoryOID && s.OptionOID == rtn.OptionOID && s.OptionDetailOID == rtn.OptionDetailOID);
                    }
                    catch (System.InvalidOperationException)
                    {
                        Console.WriteLine("The collection does not contain exactly one element in grid --> Add");

                        //Add to list
                        listOptionDetail.Add(rtn);

                        //Add to grid
                        DataRow drToAdd = ObjectUtils.FillDataToRow(dataTable.NewRow(), rtn);

                        Console.WriteLine("----Add row: " + drToAdd["PartialPayer"].ToString());
                        dataTable.Rows.Add(drToAdd);
                    }
                }
            }
        }

        private void removeRow(TreeNode Node)
        {
            string name = Node.Name;
            string type = name.Substring(0, 1);
            Int32 value = Int32.Parse(name.Substring(1));


            int cateId = 0;
            int optionId = 0;
            int detailId = 0;
            //Remove List OptionCategories
            var item = getOptionBase(Node.Name, Node.Text);
            if (item != null)
            {
                if (item.GetType() == typeof(SCOptionCategory))
                {
                    cateId = item.OID;
                    listOptionDetail.RemoveAll(x => x.OptionCategoryOID == item.OID && x.OptionOID <= 0 && x.OptionDetailOID <= 0);
                    //SCOptionDetail tesst = ((SCOptionCategory)item).convertToDetail();
                    //listOptionDetail.Remove(((SCOptionCategory)item).convertToDetail());
                }
                else if (item.GetType() == typeof(SCOption))
                {
                    TreeNode parent = Node.Parent;
                    cateId = Int32.Parse(parent.Name.Substring(1));
                    SCOption option = (SCOption)item;
                    optionId = option.OID;

                    listOptionDetail.RemoveAll(x => x.OptionCategoryOID == cateId && x.OptionOID == option.OID && x.OptionDetailOID <= 0);
                    //listOptionDetail.Remove(option.convertToDetail());
                }
                else
                {
                    TreeNode scOption = Node.Parent;
                    optionId = Int32.Parse(scOption.Name.Substring(1));

                    TreeNode scCate = scOption.Parent;
                    cateId = Int32.Parse(scCate.Name.Substring(1));

                    SCOptionDetail detail = (SCOptionDetail)item;
                    detailId = detail.OID;
                    listOptionDetail.RemoveAll(x => x.OptionCategoryOID == cateId && x.OptionOID == optionId && x.OptionDetailOID == detail.OID);
                    //listOptionDetail.Remove(detail);
                }

                removeFromDic(Node);

                //Xoa grid
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    DataRow recRow = dataTable.Rows[i];
                    if (cateId.Equals(recRow["OptionCategoryOID"])
                        && optionId.Equals(recRow["OptionOID"])
                        && detailId.Equals(recRow["OptionDetailOID"]))
                    {
                        recRow.Delete();
                        dataTable.AcceptChanges();
                        break;
                    }
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
            if (this.listOptionDetail.Count == 0)
            {
                ContractFrm.objContract.listContractOptions = this.listOptionDetail;
            }
            else
            {
                //Tao list xoa, update, insert
                ContractOption finded = null;

                //List delete
                foreach (ContractOption contractOption in ContractFrm.objContract.listContractOptions)
                {
                    try
                    {
                        finded = this.listOptionDetail.Single(s => s.OptionCategoryOID == contractOption.OptionCategoryOID && s.OptionOID == contractOption.OptionOID && s.OptionDetailOID == contractOption.OptionDetailOID);
                    }
                    catch (System.InvalidOperationException)
                    {
                        Console.WriteLine("List delete The collection does not contain exactly one element: " + contractOption.toString());
                        //Xoa
                        contractOption.isDelete = true;
                        this.listOptionDetail.Add(contractOption);
                    }
                }

                foreach (ContractOption contractOption in this.listOptionDetail)
                {
                    if (contractOption.isDelete)
                    {
                        continue;
                    }
                    try
                    {
                        finded = ContractFrm.objContract.listContractOptions.Single(s => s.OptionCategoryOID == contractOption.OptionCategoryOID && s.OptionOID == contractOption.OptionOID && s.OptionDetailOID == contractOption.OptionDetailOID);
                        //Check update
                        //Console.WriteLine("Info: " + finded.Info + " - " + contractOption.Info + " is: " + finded.Info.Equals(contractOption.Info));
                        if (!finded.SalePr.Equals(contractOption.SalePr)
                            || !finded.Quantity.Equals(contractOption.Quantity)
                            || !finded.Info.Equals(contractOption.Info)
                            || !finded.PartialPayer.Equals(contractOption.PartialPayer))
                        {
                            contractOption.isUpdate = true;
                        }
                    }
                    catch (System.InvalidOperationException)
                    {
                        Console.WriteLine("List update The collection does not contain exactly one element : " + contractOption.toString());
                        //Xoa
                        contractOption.isInsert = true;
                    }
                }

                //Gan lai lisst
                ContractFrm.objContract.listContractOptions = this.listOptionDetail;

            }

        }

        private void treeView1_AfterExpand(object sender, TreeViewEventArgs e)
        {

        }

        private void dataGridView1_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            Console.WriteLine("dataGridView1_RowValidated: " + e.RowIndex);
            //Update data
            if (e.RowIndex >= 0)
            {
                try
                {
                    DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                    Int32 OptionCategoryOID = 0;
                    Int32 OptionOID = 0;
                    Int32 OptionDetailOID = 0;
                    Int32.TryParse(row.Cells[12].Value.ToString(), out OptionCategoryOID);
                    Int32.TryParse(row.Cells[13].Value.ToString(), out OptionOID);
                    Int32.TryParse(row.Cells[14].Value.ToString(), out OptionDetailOID);
                    //Console.WriteLine("OptionCategoryOID: " + OptionCategoryOID + " - OptionOID: " + OptionOID + " -OptionDetailOID: " + OptionDetailOID);
                    ContractOption finded = this.listOptionDetail.Single(s => s.OptionCategoryOID == OptionCategoryOID && s.OptionOID == OptionOID && s.OptionDetailOID == OptionDetailOID);

                    Int32 sellPr = 0;
                    Int32 Quantity = 0;
                    Int32.TryParse(row.Cells[8].Value.ToString(), out sellPr);
                    Int32.TryParse(row.Cells[9].Value.ToString(), out Quantity);
                    finded.SalePr = sellPr;
                    finded.Quantity = Quantity;
                    if (row.Cells[10].Value != null)
                        finded.Info = row.Cells[10].Value.ToString();
                    if (row.Cells[11].Value != null)
                        finded.PartialPayer = row.Cells[11].Value.ToString();
                }
                catch (System.InvalidOperationException)
                {
                    Console.WriteLine("The collection does not contain exactly one element.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("" + ex.Message);
                }
            }
        }

        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.ColumnIndex == 8 || e.ColumnIndex == 8)
            {
                //Console.WriteLine("gridRisk_CellValidating " + e.ColumnIndex);
                dataGridView1.Rows[e.RowIndex].ErrorText = "";
                decimal newInteger;

                // Don't try to validate the 'new row' until finished 
                // editing since there
                // is not any point in validating its initial value.
                if (dataGridView1.Rows[e.RowIndex].IsNewRow) { return; }
                if (!decimal.TryParse(e.FormattedValue.ToString(),
                    out newInteger) || newInteger < 0)
                {
                    e.Cancel = true;
                    dataGridView1.Rows[e.RowIndex].ErrorText = "The value must be a non-negative decimal";
                }
            }
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            Console.WriteLine("======dataGridView1_DataBindingComplete======" + dataGridView1.RowCount);
            //Hide OID column
            dataGridView1.Columns[0].Visible = false;
            //dataGridView1.Columns["OID"].Visible = false;

            List<clsBaseListItem> listTmp = new SCBase().GetConfig("ZSCCAPPAYE");
            List<ObjTmp> listZSCCAPPAYE = new List<ObjTmp>(listTmp.Count);
            foreach (clsBaseListItem term in listTmp)
            {
                listZSCCAPPAYE.Add(new ObjTmp(term.nValue1.ToString(), term.strText));
            }


            DataGridViewComboBoxColumn cbox = (DataGridViewComboBoxColumn)dataGridView1.Columns[11];

            cbox.DataSource = listZSCCAPPAYE;
            cbox.ValueMember = "strValue1";
            cbox.DisplayMember = "strText";

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                DataGridViewComboBoxCell cboContacts = (DataGridViewComboBoxCell)(row.Cells[11]);
                cboContacts.DataSource = listZSCCAPPAYE;
                cboContacts.DisplayMember = "strText"; //Name column of contact datasource
                cboContacts.ValueMember = "strValue1";//Value column of contact datasource
            }
            Console.WriteLine("======dataGridView1_DataBindingComplete======");
        }
    }
}
