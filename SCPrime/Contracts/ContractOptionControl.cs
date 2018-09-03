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
using log4net;

namespace SCPrime.Contracts
{
    public partial class ContractOptionControl : UserControl
    {
        static readonly ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public ContractOptionControl()
        {
            InitializeComponent();
        }

        private void ContractOption_Load(object sender, EventArgs e)
        {
            List<clsBaseListItem> listTmp = sCBase.GetConfig("ZSCCAPPAYE");
            List<ObjTmp> listZSCCAPPAYE = new List<ObjTmp>(listTmp.Count);
            foreach (clsBaseListItem term in listTmp)
            {
                listZSCCAPPAYE.Add(new ObjTmp(term.nValue1.ToString(), term.strText));
            }
            partialPayerBindingSource.DataSource = listZSCCAPPAYE;
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

        bool loadTreeDone = false;

        private void buidTree()
        {
            List<SCOptionPrice> listToClone = myCategories.ToList();

            IEnumerable<SCOptionPrice> listCategory = myCategories.Where(s => !s.NotAvailable && s.OptionOID <= 0 && s.OptionDetailOID <= 0);

            IEnumerable<SCOptionPrice> listOptions;
            IEnumerable<SCOptionPrice> listOptionDetails;


            treeView1.Nodes.Clear();
            if (myCategories.Count > 0)
            {
                //
                if (ContractFrm.objContract.ContractOID <= 0 || listOptionDetailTmp == null || listOptionDetailTmp.Count <= 0)
                {
                    //Tao moi
                    #region FillTree with Include
                    foreach (SCOptionPrice cat in listCategory)
                    {
                        TreeNode treeNode = new TreeNode(cat.CategoryName);
                        treeNode.Name = "C" + cat.CategoryOID.ToString();
                        if (cat.Include)
                        {
                            //Include -> check true and add grid
                            treeNode.Checked = true;
                            //fillDisalbeTreeNode
                            fillDisalbeTreeNode(treeNode);

                        }
                        treeView1.Nodes.Add(treeNode);

                        if (treeNode.Checked)
                        {
                            addRow(treeNode);
                        }

                        //get option
                        listOptions = myCategories.Where(s => !s.NotAvailable && s.CategoryOID == cat.CategoryOID && s.OptionOID > 0 && s.OptionDetailOID <= 0);

                        foreach (SCOptionPrice op in listOptions)
                        {
                            TreeNode treeNodeL2 = new TreeNode(op.OptionName);
                            treeNodeL2.Name = "O" + op.OptionOID.ToString();
                            if (op.Include)
                            {
                                //Include -> check true and add grid
                                treeNodeL2.Checked = true;
                                fillDisalbeTreeNode(treeNodeL2);
                            }

                            treeNode.Nodes.Add(treeNodeL2);

                            //Check parent
                            if (treeNodeL2.Checked)
                            {
                                addRow(treeNodeL2);
                                if (!treeNode.Checked)
                                {
                                    treeNode.Checked = true;
                                }
                            }

                            //load all detail
                            listOptionDetails = myCategories.Where(s => !s.NotAvailable && s.CategoryOID == cat.CategoryOID && s.OptionOID == op.OptionOID && s.OptionDetailOID > 0);

                            foreach (SCOptionPrice sod in listOptionDetails)
                            {
                                // create childnode level3
                                TreeNode treeNodeL3 = new TreeNode(sod.OptionDetailName);
                                treeNodeL3.Name = "D" + sod.OptionDetailOID.ToString();
                                if (sod.Include)
                                {
                                    //Include -> check true and add grid
                                    treeNodeL3.Checked = true;
                                    fillDisalbeTreeNode(treeNodeL3);
                                }

                                treeNodeL2.Nodes.Add(treeNodeL3);

                                if (treeNodeL3.Checked)
                                {
                                    addRow(treeNodeL3);
                                    if (!treeNodeL2.Checked)
                                    {
                                        treeNodeL2.Checked = true;
                                    }

                                    if (!treeNode.Checked)
                                    {
                                        treeNode.Checked = true;
                                    }
                                }
                            }
                        }
                    }
                    #endregion
                }
                else
                {
                    #region FillTree with zsc_contractoption
                    foreach (SCOptionPrice cat in listCategory)
                    {
                        TreeNode treeNode = new TreeNode(cat.CategoryName);
                        treeNode.Name = "C" + cat.CategoryOID.ToString();
                        if (cat.Include)
                        {
                            //Include -> check true and add grid
                            treeNode.Checked = true;
                            //fillDisalbeTreeNode
                            fillDisalbeTreeNode(treeNode);

                        }
                        else
                            treeNode.Checked = checkTree(treeNode, cat);
                        treeView1.Nodes.Add(treeNode);

                        if (treeNode.Checked)
                        {
                            addRow(treeNode);
                        }

                        //get option
                        listOptions = myCategories.Where(s => !s.NotAvailable && s.CategoryOID == cat.CategoryOID && s.OptionOID > 0 && s.OptionDetailOID <= 0);

                        foreach (SCOptionPrice op in listOptions)
                        {
                            TreeNode treeNodeL2 = new TreeNode(op.OptionName);
                            treeNodeL2.Name = "O" + op.OptionOID.ToString();
                            if (op.Include)
                            {
                                //Include -> check true and add grid
                                treeNodeL2.Checked = true;
                                //fillDisalbeTreeNode
                                fillDisalbeTreeNode(treeNodeL2);

                            }
                            else
                                treeNodeL2.Checked = checkTree(treeNodeL2, op);
                            treeNode.Nodes.Add(treeNodeL2);
                            //Check parent
                            if (treeNodeL2.Checked)
                            {
                                addRow(treeNodeL2);
                                if (!treeNode.Checked)
                                {
                                    treeNode.Checked = true;
                                }
                            }

                            //load all detail
                            listOptionDetails = myCategories.Where(s => !s.NotAvailable && s.CategoryOID == cat.CategoryOID && s.OptionOID == op.OptionOID && s.OptionDetailOID > 0);

                            foreach (SCOptionPrice sod in listOptionDetails)
                            {
                                // create childnode level3
                                TreeNode treeNodeL3 = new TreeNode(sod.OptionDetailName);
                                treeNodeL3.Name = "D" + sod.OptionDetailOID.ToString();
                                if (sod.Include)
                                {
                                    //Include -> check true and add grid
                                    treeNodeL3.Checked = true;
                                    //fillDisalbeTreeNode
                                    fillDisalbeTreeNode(treeNodeL3);

                                }
                                else
                                    treeNodeL3.Checked = checkTree(treeNodeL3, sod);
                                treeNodeL2.Nodes.Add(treeNodeL3);
                                if (treeNodeL3.Checked)
                                {
                                    addRow(treeNodeL3);
                                    if (!treeNodeL2.Checked)
                                    {
                                        treeNodeL2.Checked = true;
                                    }

                                    if (!treeNode.Checked)
                                    {
                                        treeNode.Checked = true;
                                    }
                                }
                            }
                        }
                    }
                    #endregion
                }
            }

            //Danh dau de tinh tong cho de
            loadTreeDone = true;
            calcTotal();
        }

        private void fillDisalbeTreeNode(TreeNode node)
        {
            node.ForeColor = SystemColors.GrayText;
        }

        private bool checkTree(TreeNode node, SCOptionPrice scprice)
        {
            if (listOptionDetailTmp == null || listOptionDetailTmp.Count <= 0)
            {
                return false;
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

        SCBase sCBase = null;

        public void loadDataGrid()
        {
            if (sCBase == null)
                sCBase = new SCBase();
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
                        addToDic(cate);
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
                _log.Error("ERROR addToDic: " + cate.toString(), ex);
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
                _log.Error("ERROR removeFromDic: " + cate.toString(), ex);
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
                _log.Error("ERROR removeFromDic TreeNode: " + Node.Name, ex);
            }
        }

        private void treeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {

            TreeNode node = e.Node;
            int level = node.Level;
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

                //Remove child
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

            if (loadTreeDone)
            {
                //Tinh lại tổng mỗi khi click tree (Trừ lúc bắt đầu load)
                calcTotal();
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
                    SCOptionCategory catetory = (SCOptionCategory)item;
                    rtn = new ContractOption();
                    rtn.OptionCategoryOID = item.OID;
                    rtn.Name = item.Name;
                    rtn.PartNr = item.ItemNo;
                    rtn.PartName = item.ItemName;
                    rtn.LabourCode = item.WrksId;
                    rtn.LabourName = item.WrksName;
                    rtn.BaseSelPr = item.SelPr;
                    rtn.PurchasePr = item.BuyPr;
                    rtn.InvoiceFlag = catetory.InvoiceFlag;
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
                    rtn.BaseSelPr = option.SelPr;
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
                    rtn.BaseSelPr = detail.SelPr;
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
                        rtn.BaseSelPr = finded.BaseSelPr;
                    }
                    catch (System.InvalidOperationException ex)
                    {
                        rtn.Quantity = 1;
                        _log.Error("ContractFrm.objContract.listContractOptions Single not contain exactly one element: " + rtn.toString(), ex);
                    }

                    if (!listOptionDetailTmp.ContainsKey(Node.Name))
                    {
                        addToDic(rtn);
                    }

                    //Add to grid
                    try
                    {
                        ContractOption finded = listOptionDetail.Single(s => s.OptionCategoryOID == rtn.OptionCategoryOID && s.OptionOID == rtn.OptionOID && s.OptionDetailOID == rtn.OptionDetailOID);
                        finded.Info = rtn.Info;
                        finded.PartialPayer = rtn.PartialPayer;
                        finded.Quantity = rtn.Quantity;
                        finded.SalePr = rtn.SalePr;
                        finded.BaseSelPr = rtn.BaseSelPr;
                    }
                    catch (System.InvalidOperationException ex)
                    {
                        _log.Error("listOptionDetail Single not contain exactly one element: " + rtn.toString(), ex);

                        //Add to list
                        listOptionDetail.Add(rtn);

                        //Add to grid
                        DataRow drToAdd = ObjectUtils.FillDataToRow(dataTable.NewRow(), rtn);
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
                }
                else if (item.GetType() == typeof(SCOption))
                {
                    TreeNode parent = Node.Parent;
                    cateId = Int32.Parse(parent.Name.Substring(1));
                    SCOption option = (SCOption)item;
                    optionId = option.OID;
                    listOptionDetail.RemoveAll(x => x.OptionCategoryOID == cateId && x.OptionOID == option.OID && x.OptionDetailOID <= 0);
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
                if (ContractFrm.objContract.listContractOptions != null && ContractFrm.objContract.listContractOptions.Count > 0)
                {
                    foreach (ContractOption contractOption in ContractFrm.objContract.listContractOptions)
                    {
                        try
                        {
                            finded = this.listOptionDetail.Single(s => s.OptionCategoryOID == contractOption.OptionCategoryOID && s.OptionOID == contractOption.OptionOID && s.OptionDetailOID == contractOption.OptionDetailOID);
                        }
                        catch (System.InvalidOperationException ex)
                        {
                            _log.Error("saveOptionCategories listOptionDetail Single not contain exactly one element: " + contractOption.toString(), ex);
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
                        catch (System.InvalidOperationException ex)
                        {
                            _log.Error("saveOptionCategories ContractFrm.objContract.listContractOptions Single not contain exactly one element: " + contractOption.toString(), ex);
                            //Xoa
                            contractOption.isInsert = true;
                        }
                    }
                }
                else
                {
                    foreach (ContractOption contractOption in this.listOptionDetail)
                    {
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
            //Update data
            if (e.RowIndex >= 0)
            {
                Int32 OptionCategoryOID = 0;
                Int32 OptionOID = 0;
                Int32 OptionDetailOID = 0;
                try
                {
                    DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                    Int32.TryParse(row.Cells[12].Value.ToString(), out OptionCategoryOID);
                    Int32.TryParse(row.Cells[13].Value.ToString(), out OptionOID);
                    Int32.TryParse(row.Cells[14].Value.ToString(), out OptionDetailOID);
                    //Console.WriteLine("OptionCategoryOID: " + OptionCategoryOID + " - OptionOID: " + OptionOID + " -OptionDetailOID: " + OptionDetailOID);
                    ContractOption finded = this.listOptionDetail.Single(s => s.OptionCategoryOID == OptionCategoryOID && s.OptionOID == OptionOID && s.OptionDetailOID == OptionDetailOID);

                    decimal sellPr = 0;
                    decimal Quantity = 0;
                    decimal.TryParse(row.Cells[8].Value.ToString(), out sellPr);
                    decimal.TryParse(row.Cells[9].Value.ToString(), out Quantity);
                    finded.SalePr = sellPr;
                    finded.Quantity = Quantity;
                    if (row.Cells[10].Value != null)
                        finded.Info = row.Cells[10].Value.ToString();
                    if (row.Cells[11].Value != null)
                        finded.PartialPayer = row.Cells[11].Value.ToString();
                    calcTotal();
                }
                catch (System.InvalidOperationException ex)
                {
                    _log.Error("dataGridView1_RowValidated listOptionDetail Single not contain exactly one element: " + OptionCategoryOID + " - " + OptionOID + " - " + OptionDetailOID, ex);
                }
                catch (Exception ex)
                {
                    _log.Error("dataGridView1_RowValidated Exception: " + OptionCategoryOID + " - " + OptionOID + " - " + OptionDetailOID, ex);
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
            //Hide OID column
            dataGridView1.Columns[0].Visible = false;
            //dataGridView1.Columns["OID"].Visible = false;
        }

        ContractFrm contractFrm;
        public void setContractFrm(ContractFrm contractFrm)
        {
            this.contractFrm = contractFrm;
        }
        public void calcTotal()
        {
            if (listOptionDetail == null || listOptionDetail.Count <= 0)
            {
                txtTotalPurchase.Text = "0";
                txtTotalSale.Text = "0";
                //Update Cost based on service
                ContractCost data = ContractFrm.objContract.ContractCostData;
                data = (data == null) ? new ContractCost() : data;
                data.CostBasedOnService = 0;
                ContractFrm.objContract.ContractCostData = data;
            }
            else
            {
                decimal totalPurchase = 0;
                decimal totalSale = 0;
                foreach (ContractOption contractOption in listOptionDetail)
                {
                    if (contractOption.isDelete)
                    {
                        continue;
                    }
                    if (contractOption.OptionOID <= 0 && contractOption.OptionDetailOID <= 0 && contractOption.InvoiceFlag > 0)
                    {
                        totalPurchase += contractOption.PurchasePr * contractOption.Quantity;
                        totalSale += contractOption.SalePr * contractOption.Quantity;
                    }
                }
                txtTotalPurchase.Text = totalPurchase.ToString();
                txtTotalSale.Text = totalSale.ToString();
                //Update Cost based on service
                ContractCost data = ContractFrm.objContract.ContractCostData;
                data = (data == null) ? new ContractCost() : data;
                data.CostBasedOnService = totalSale;
                ContractFrm.objContract.ContractCostData = data;
            }
        }


        private void treeView1_BeforeCheck(object sender, TreeViewCancelEventArgs e)
        {
            TreeNode node = e.Node;
            if (SystemColors.GrayText == node.ForeColor)
            {
                e.Cancel = true;
            }
        }
    }
}
