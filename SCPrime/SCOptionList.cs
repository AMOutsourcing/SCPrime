using log4net;
using SCPrime.Model;
using SCPrime.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SCPrime
{
    public partial class SCOptionList : nsBaseClass.clsBaseForm
    {
        static readonly ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static SCOptionList _instance;
        //private List<SCOptionCategory> myCategories;
        private DataTable categoryDataTable;
        private List<KeyValue> bindingList;

        public List<SCOptionCategory> saveCategories;
        private int CategoryOidSelected = -1;
        private SCOptionCategory categorySelected;

        private List<SCOption> saveOptions;
        private DataTable optionTable;

        private DataTable detailTable;
        private int OptionOidSelected;
        private SCOption optionSelected;

        private SCOptionDetail detailSelected;

        private int newOid = -1;
        private int newOptionOid = -1;
        private int newDetailOid = -1;

        // System.Globalization.CultureInfo usCultureInfo = new System.Globalization.CultureInfo("en-US");
        System.Globalization.CultureInfo oldCI = System.Threading.Thread.CurrentThread.CurrentCulture;

        //----------------------longdq add 27/06/2018---------------------------
        public delegate void SendLabour(string Message);
        public delegate void SendItem(string Message);
        public SendLabour Sender;
        public SendItem Sender2;
     //   private int editFlag = -1;
        private TreeNode treeNodeSelected;
        private string treeFullPath;

        public SCOptionList()
        {
            InitializeComponent();
            this.Visible = false;
            this.saveCategories = new List<SCOptionCategory>();
            this.saveOptions = new List<SCOption>();
            this.optionSelected = new SCOption();

            this.bindingList = new List<KeyValue>();
            this.bindingList.Add(new KeyValue(0, Constant.Empty));
            this.bindingList.Add(new KeyValue(1, Constant.FirstInvoice));
            this.bindingList.Add(new KeyValue(2, Constant.LastInvoice));
            //usCultureInfo.NumberFormat.NumberDecimalSeparator = ".";

            //Decimal i = 10000.20M;
            //MessageBox.Show(i.ToString("c", oldCI));
            Sender = new SendLabour(GetLabour);
            Sender2 = new SendItem(GetItem);

        }
        private void GetLabour(string Message)
        {
            string[] msg = Message.Split(':');
            if (msg.Count() > 0)
            {

                if (this.categorySelected != null && msg[0].Equals("0"))
                {
                    this.categorySelected.WrksId = msg[1];
                    //update grid
                    DataTable dt = new DataTable();
                    dt = (DataTable)this.dataGridViewCategory.DataSource;
                    string search = "OID = " + this.categorySelected.OID;
                    DataRow[] rows = dt.Select(search);
                    if (rows.Count() > 0)
                    {
                        DataRow r = rows[0];
                        var item = this.saveCategories.Find(x => x.OID == this.categorySelected.OID);
                        if (item != null)
                        {
                            item.WrksId = msg[1];
                            //update category
                            var index = this.saveCategories.FindIndex(x => x.OID == this.categorySelected.OID);
                            if (index > -1)
                            {
                                this.saveCategories[index] = item;
                                r["WrksId"] = msg[1];
                            }
                        }
                        dt.AcceptChanges();
                        this.dataGridViewCategory.Refresh();
                    }
                }

                if (this.optionSelected != null && msg[0].Equals("1"))
                {
                    this.optionSelected.WrksId = msg[1];
                    //update grid
                    DataTable dt = new DataTable();
                    dt = (DataTable)this.dgvOptions.DataSource;
                    string search = "OID = " + this.optionSelected.OID;
                    DataRow[] rows = dt.Select(search);
                    if (rows.Count() > 0)
                    {
                        DataRow r = rows[0];
                        var item = this.categorySelected.Options.Find(x => x.OID == this.optionSelected.OID);
                        if (item != null)
                        {
                            item.WrksId = msg[1];
                            //update category
                            var index = this.categorySelected.Options.FindIndex(x => x.OID == this.optionSelected.OID);
                            if (index > -1)
                            {
                                this.categorySelected.Options[index] = item;
                                r["WrksId"] = msg[1];
                            }
                        }
                        dt.AcceptChanges();
                        this.dgvOptions.Refresh();
                    }
                }

                //--------------Update Details-------------------
                if (this.detailSelected != null && msg[0].Equals("2"))
                {
                    this.detailSelected.WrksId = msg[1];
                    //update grid
                    DataTable dt = new DataTable();
                    dt = (DataTable)this.dgvDetails.DataSource;
                    string search = "OID = " + this.detailSelected.OID;
                    DataRow[] rows = dt.Select(search);
                    if (rows.Count() > 0)
                    {
                        DataRow r = rows[0];
                        var item = this.optionSelected.OptionDetails.Find(x => x.OID == this.detailSelected.OID);
                        if (item != null)
                        {
                            item.WrksId = msg[1];
                            //update category
                            var index = this.optionSelected.OptionDetails.FindIndex(x => x.OID == this.detailSelected.OID);
                            if (index > -1)
                            {
                                this.optionSelected.OptionDetails[index] = item;
                                r["WrksId"] = msg[1];
                            }
                        }
                        dt.AcceptChanges();
                        this.dgvDetails.Refresh();
                    }
                }
            }
        }


        private void GetItem(string Message)
        {
            string[] msgs = Message.Split(';');
            if (msgs.Count() > 0)
            {
                if (this.categorySelected != null && msgs[0].Equals("0"))
                {
                    this.categorySelected.ItemNo = msgs[1];
                    this.categorySelected.ItemSuplNo = msgs[2];
                    //update grid
                    DataTable dt = new DataTable();
                    dt = (DataTable)this.dataGridViewCategory.DataSource;
                    string search = "OID = " + this.categorySelected.OID;
                    DataRow[] rows = dt.Select(search);
                    if (rows.Count() > 0)
                    {
                        DataRow r = rows[0];
                        var item = this.saveCategories.Find(x => x.OID == this.categorySelected.OID);
                        if (item != null)
                        {
                            item.ItemNo = msgs[1];
                            item.ItemSuplNo = msgs[2];
                            //update category
                            var index = this.saveCategories.FindIndex(x => x.OID == this.categorySelected.OID);
                            if (index > -1)
                            {
                                this.saveCategories[index] = item;
                                r["ItemNo"] = msgs[1];
                                r["ItemSuplNo"] = msgs[2];
                            }
                        }
                        dt.AcceptChanges();
                        this.dataGridViewCategory.Refresh();
                    }
                }

                if (this.optionSelected != null && msgs[0].Equals("1"))
                {
                    this.optionSelected.ItemNo = msgs[1];
                    this.optionSelected.ItemSuplNo = msgs[2];
                    //update grid
                    DataTable dt = new DataTable();
                    dt = (DataTable)this.dgvOptions.DataSource;
                    string search = "OID = " + this.optionSelected.OID;
                    DataRow[] rows = dt.Select(search);
                    if (rows.Count() > 0)
                    {
                        DataRow r = rows[0];
                        var item = this.categorySelected.Options.Find(x => x.OID == this.optionSelected.OID);
                        if (item != null)
                        {
                            item.ItemNo = msgs[1];
                            item.ItemSuplNo = msgs[2];
                            //update category
                            var index = this.categorySelected.Options.FindIndex(x => x.OID == this.optionSelected.OID);
                            if (index > -1)
                            {
                                this.categorySelected.Options[index] = item;
                                r["ItemNo"] = msgs[1];
                                r["ItemSuplNo"] = msgs[2];
                            }
                        }
                        dt.AcceptChanges();
                        this.dgvOptions.Refresh();
                    }
                }

                //--------------Update Details-------------------
                if (this.detailSelected != null && msgs[0].Equals("2"))
                {
                    this.detailSelected.ItemNo = msgs[1];
                    this.detailSelected.ItemSuplNo = msgs[2];
                    //update grid
                    DataTable dt = new DataTable();
                    dt = (DataTable)this.dgvDetails.DataSource;
                    string search = "OID = " + this.detailSelected.OID;
                    DataRow[] rows = dt.Select(search);
                    if (rows.Count() > 0)
                    {
                        DataRow r = rows[0];
                        var item = this.optionSelected.OptionDetails.Find(x => x.OID == this.detailSelected.OID);
                        if (item != null)
                        {
                            item.ItemNo = msgs[1];
                            item.ItemSuplNo = msgs[2];
                            //update category
                            var index = this.optionSelected.OptionDetails.FindIndex(x => x.OID == this.detailSelected.OID);
                            if (index > -1)
                            {
                                this.optionSelected.OptionDetails[index] = item;
                                r["ItemNo"] = msgs[1];
                                r["ItemSuplNo"] = msgs[2];
                            }
                        }
                        dt.AcceptChanges();
                        this.dgvDetails.Refresh();
                    }
                }
            }
        }


        private void SelectTreeNode(int OID, int flagType)
        {

        }



        //-----------------------------------------------------------------------------
        public static SCOptionList instance
        {
            get
            {
                if (SCOptionList._instance == null || SCOptionList._instance.IsDisposed)
                {
                    SCOptionList._instance = new SCOptionList();
                }
                return SCOptionList._instance;
            }
        }


        private void SCOptionList_Load(object sender, EventArgs e)
        {
            loadCategoryData();
            loadTree();
            this.Visible = true;

        }

        private void dataGridViewCategory_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
        }
        private void dataGridViewCategory_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            if (e.StateChanged != DataGridViewElementStates.Selected)
                return;
            if (dataGridViewCategory.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dataGridViewCategory.SelectedRows[0];
                int CatOid = -1;
                var tmp = Int32.TryParse(row.Cells[Constant.OID].Value.ToString(), out CatOid);
                this.CategoryOidSelected = CatOid;
                if (this.saveCategories.Count > 0)
                {
                    this.categorySelected = this.saveCategories.Find(x => x.OID == this.CategoryOidSelected);
                }
                if (this.categorySelected != null && this.categorySelected.Options == null)
                {
                    this.categorySelected.Options = new List<SCOption>();

                }
                this.loadOptionList(CatOid);
            }

           

        }

        private void dataGridViewCategory_SelectionChanged(object sender, EventArgs e)
        {


        }

        private void dataGridViewCategory_CellClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (this.saveCategories.Count > 0)
            {
                var result = SCOptionCategory.saveOptionCategoryList(this.saveCategories);
                //MessageBox.Show(result.ToString());
                if (result)
                {
                    this.dataGridViewCategory.DataSource = null;
                }
            }
            loadCategoryData();
            loadTree();

        }

        private void dataGridViewCategory_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {


        }



        private void DelCatBtn_Click(object sender, EventArgs e)
        {

            if (dataGridViewCategory.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow r in dataGridViewCategory.SelectedRows)
                {
                    var myOid = r.Cells[0].Value;
                    string search = Constant.OID + " = " + myOid.ToString();
                    DataRow[] rows = this.categoryDataTable.Select(search);
                    foreach (DataRow row in rows)
                    {

                        SCOptionCategory item = null;

                        item = this.saveCategories.FirstOrDefault(x => x.OID == (int)r.Cells[0].Value);



                        if (row[Constant.isMarkDeleted].ToString().ToUpper() == "TRUE")
                        {
                            row[Constant.isMarkDeleted] = 0;
                            item.isMarkDeleted = false;
                        }
                        else if (row[Constant.isMarkDeleted].ToString().ToUpper() == "FALSE")
                        {
                            row[Constant.isMarkDeleted] = 1;
                            item.isMarkDeleted = true;
                        }
                        var index = saveCategories.IndexOf(item);
                        if (index != -1)
                        {
                            this.saveCategories[index] = item;
                        }


                    }
                    // change color
                    ViewUtils.remarkHeader(r, Constant.isMarkDeleted);
                }
                //this.categoryDataTable.AcceptChanges();
                //dataGridViewCategory.Refresh();
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (SCOptionList._instance != null)
            {
                SCOptionList._instance.Close();
            }
        }

        private void SCOptionList_FormClosed(object sender, FormClosedEventArgs e)
        {
            SCOptionList._instance = null;
        }

        private void NewCatBtn_Click(object sender, EventArgs e)
        {
            this.categoryDataTable = null;
            this.categoryDataTable = (DataTable)dataGridViewCategory.DataSource;
            if (categoryDataTable == null)
            {
                SCOptionCategory sc = new SCOptionCategory();
                categoryDataTable = ObjectUtils.ConvertToDataTable(new List<SCOptionCategory> { sc });
            }
            DataRow drToAdd = categoryDataTable.NewRow();

            drToAdd[Constant.OID] = newOid;
            //drToAdd[Constant.Name] = "";
            drToAdd[Constant.InvoiceFlag] = 0;
            drToAdd[Constant.isMarkDeleted] = false;
            drToAdd[Constant.isAvailable] = 1;


            categoryDataTable.Rows.InsertAt(drToAdd, 0);
            //categoryDataTable.AcceptChanges();
            dataGridViewCategory.Refresh();
            // dataGridViewCategory.Rows[0].Selected = true;

            DataGridViewCell cell = dataGridViewCategory.Rows[0].Cells[Constant.CategoryName];
            dataGridViewCategory.CurrentCell = cell;
            this.dataGridViewCategory.BeginEdit(true);


            foreach (DataGridViewRow r in dataGridViewCategory.Rows)
            {
                if ((int)r.Cells[Constant.OID].Value < 0)// object mới
                {
                    SCOptionCategory so = null;
                    so = this.saveCategories.Find(x => x.OID == (int)r.Cells[Constant.OID].Value);
                    if (so == null)
                    {
                        so = this.RowToCategory(r);
                        this.saveCategories.Add(so);
                    }

                }

            }

            newOid = newOid - 1;

        }

        private void dataGridViewCategory_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            //this.editFlag = Constant.Category;
            if (dataGridViewCategory.Columns[e.ColumnIndex].Name == Constant.PurcharPrice ||
                dataGridViewCategory.Columns[e.ColumnIndex].Name == Constant.SalePrice)
            {
                double i;

                if (!double.TryParse(Convert.ToString(e.FormattedValue), out i))
                {
                    e.Cancel = true;
                    MessageBox.Show("Please enter numeric");
                }
                else
                {
                    dataGridViewCategory.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = Convert.ToDecimal(e.FormattedValue, this.oldCI);
                }
            }
        }

        private void dataGridViewCategory_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridViewCategory_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridViewCategory_CellValidated(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridViewCategory_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            //this.editFlag = Constant.Category;
            int rowIndex = e.RowIndex;
            int colIndex = e.ColumnIndex;
            // MessageBox.Show(colIndex.ToString());

            DataGridViewRow row = dataGridViewCategory.Rows[rowIndex];
            //find object in list 
            var item2 = this.saveCategories.SingleOrDefault(x => x.OID == (int)dataGridViewCategory.Rows[rowIndex].Cells["OID"].Value);
            if (item2 != null)
            {
                SCOptionCategory sc = new SCOptionCategory();
                sc = RowToCategory(row);
                var item = this.saveCategories.Single(x => x.OID == sc.OID);
                var index = saveCategories.IndexOf(item);
                if (item != null)
                {
                    sc.Options = item.Options;
                    if (index != -1)
                        this.saveCategories[index] = sc;
                    this.categorySelected = item;
                }
                else
                {
                    this.saveCategories.Add(sc);
                    this.categorySelected = sc;
                }
            }

        }

        private void dgvDetails_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvOptions_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        //------------------------------------------- User function--------------------------------------------------------------------

        private void loadCategoryData()
        {
            SCOptionCategory sco = new SCOptionCategory();
            sco = this.categorySelected;
            this.saveCategories.Clear();
            this.saveCategories = SCOptionCategory.getOptionCategoryList();
            // MessageBox.Show(myCategories.Count.ToString());
            this.categoryDataTable = ObjectUtils.ConvertToDataTable(this.saveCategories);

            bool flag = false;
            foreach (DataGridViewColumn dc in dataGridViewCategory.Columns)
            {
                if (dc.Name == Constant.InvoiceFlag)
                {
                    flag = true;
                    break;
                }

            }
            if (!flag)
            {

                DataTable dt = new DataTable();
                dt = ObjectUtils.ConvertToDataTable(bindingList);

                DataGridViewComboBoxColumn cb = new DataGridViewComboBoxColumn();
                cb.HeaderText = "Invoice";
                cb.Name = "InvoiceFlag";
                cb.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                cb.DataSource = dt;
                cb.DataPropertyName = "InvoiceFlag";
                cb.ValueMember = "Key";
                cb.DisplayMember = "Value";
                dataGridViewCategory.Columns.Add(cb);
            }
            dataGridViewCategory.DataSource = this.categoryDataTable;
            dataGridViewCategory.Refresh();

            if (dataGridViewCategory.Rows.Count > 0)
            {
                if (sco != null)
                {
                    int rowindex = 0;
                    rowindex = GetIndexOfRowCategory(dataGridViewCategory, sco != null ? sco.OID : -1);
                    if (rowindex >= 0)
                    {
                        dataGridViewCategory.CurrentCell = dataGridViewCategory.Rows[rowindex].Cells[Constant.CategoryName];
                    }
                    else
                        dataGridViewCategory.CurrentCell = dataGridViewCategory.Rows[0].Cells[Constant.CategoryName];
                }
                else
                    dataGridViewCategory.CurrentCell = dataGridViewCategory.Rows[0].Cells[Constant.CategoryName];
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

        private void loadTree()
        {
            //load category
            treeView1.Nodes.Clear();

            List<SCOptionCategory> myCategories = SCOptionCategory.getOptionCategoryList();
            if (myCategories.Count > 0)
            {
                foreach (SCOptionCategory cat in myCategories)
                {
                    TreeNode treeNode = new TreeNode(cat.Name);
                    treeNode.Name = cat.GetType().ToString() + cat.OID.ToString();

                    treeView1.Nodes.Add(treeNode);

                    //load all Option
                    List<SCOption> myOptions = new List<SCOption>();
                    myOptions = SCOption.getOptionList(cat.OID);
                    if (myOptions.Count > 0)
                    {
                        foreach (SCOption op in myOptions)
                        {
                            TreeNode treeNodeL2 = new TreeNode(op.Name);
                            treeNodeL2.Name = op.GetType().ToString() + op.OID.ToString();
                            treeNode.Nodes.Add(treeNodeL2);
                            //load all detail
                            List<SCOptionDetail> myOptionDetails = new List<SCOptionDetail>();
                            myOptionDetails = SCOptionDetail.getOptionDetailList(op.OID);
                            if (myOptionDetails.Count > 0)
                            {
                                foreach (SCOptionDetail sod in myOptionDetails)
                                {
                                    // create childnode level3
                                    TreeNode treeNodeL3 = new TreeNode(sod.Name);
                                    treeNodeL3.Name = sod.GetType().ToString() + sod.OID.ToString();
                                    treeNodeL2.Nodes.Add(treeNodeL3);
                                }
                            }
                        }
                    }
                }
            }
            //this.treeView1.ExpandAll();
            if (!string.IsNullOrEmpty(this.treeFullPath))
            {
                this.displayTree(this.treeFullPath);
            }
        }


        private void displayTree(string path)
        {
            TreeNode[] nodes = this.treeView1.Nodes.Find(path, true);
            if (nodes.Count() > 0)
            {
                foreach (TreeNode node in nodes)
                {
                    node.Expand();
                    this.treeView1.SelectedNode = node;
                    this.treeView1.Focus();
                }
            }
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

            sc.Name = row.Cells["Category"].Value.ToString();
            sc.ItemNo = row.Cells["ItemNo"].Value.ToString();
            sc.ItemSuplNo = row.Cells["ItemSuplNo"].Value.ToString();
            sc.WrksId = row.Cells["WrksId"].Value.ToString();
            sc.ItemName = row.Cells["ItemName"].Value.ToString();
            sc.WrksName = row.Cells["WrksName"].Value.ToString();
            sc.InvoiceFlag = (int)row.Cells["InvoiceFlag"].Value;
            sc.Info = row.Cells["Info"].Value.ToString();
            sc.isAvailable = (int)row.Cells["isAvailable"].Value;
            sc.isMarkDeleted = (bool)row.Cells["isMarkDeleted"].Value;
            sc.Options = new List<SCOption>();


            var temp1 = 0;
            bool rs1 = Int32.TryParse(row.Cells["Quantity"].Value.ToString(), out temp1);
            if (rs1)
                sc.Quantity = temp1;
            else
                sc.Quantity = 0;

            var temp = (decimal)0;
            bool rs = Decimal.TryParse(row.Cells["BaseSelPr"].Value.ToString(), out temp);
            if (rs)
                sc.BaseSelPr = Math.Round(temp, 2);
            else
                sc.BaseSelPr = (decimal)0;

            temp = (decimal)0;
            bool rs2 = Decimal.TryParse(row.Cells["BuyPr"].Value.ToString(), out temp);
            if (rs2)
                sc.BuyPr = Math.Round(temp, 2);
            else
                sc.BuyPr = (decimal)0;

            temp = (decimal)0;
            bool rs3 = Decimal.TryParse(row.Cells["SelPr"].Value.ToString(), out temp);
            if (rs3)
                sc.SelPr = Math.Round(temp, 2);
            else
                sc.SelPr = (decimal)0;

            return sc;
        }



        private void DeleteCategoryList(SCOptionCategory item)
        {


            if (item != null)
            {
                this.saveCategories.Remove(item);
                item.isMarkDeleted = true;
                this.saveCategories.Add(item);

            }
        }

        private void dataGridViewCategory_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            //SCOptionCategory sc = new SCOptionCategory();
            //this.displayTree(sc.GetType().ToString() + this.dataGridViewCategory.Rows[e.RowIndex].Cells[0].Value);
        }
        private void loadOptionList(int CategoryOid)
        {
            // List<SCOption> myOptions = SCOption.getOptionList(CategoryOid);
            SCOption sco = new SCOption();
            sco = this.optionSelected;
            if (dgvOptions.Rows.Count > 0)
            {
                DataTable mydt = new DataTable();
                mydt = (DataTable)this.dgvOptions.DataSource;
                mydt.Clear();
                this.dgvOptions.DataSource = mydt;
            }
            if (CategoryOid > 0)
            {
                List<SCOption> myOptions = this.categorySelected.Options;
                optionTable = new DataTable();
                optionTable = ObjectUtils.ConvertToDataTable(myOptions);
                this.dgvOptions.DataSource = optionTable;
            }
            else if (this.categorySelected != null && CategoryOid < 0)
            {
                this.categorySelected = this.saveCategories.Find(x => x.OID == CategoryOid);
                List<SCOption> myOptions = this.categorySelected.Options;
                optionTable = new DataTable();
                optionTable = ObjectUtils.ConvertToDataTable(myOptions);
                this.dgvOptions.DataSource = optionTable;
            }

            //     dgvOptions.Refresh();

            if (dgvOptions.Rows.Count > 0)
            {
                if (sco != null)
                {
                    int rowindex = 0;
                    rowindex = GetIndexOfRowOption(dgvOptions, sco != null ? sco.OID : -1);
                    if (rowindex >= 0)
                    {
                        dgvOptions.CurrentCell = dgvOptions.Rows[rowindex].Cells[Constant.OptionName];
                    }
                    else
                        dgvOptions.CurrentCell = dgvOptions.Rows[0].Cells[Constant.OptionName];
                }
                else
                    dgvOptions.CurrentCell = dgvOptions.Rows[0].Cells[Constant.OptionName];
            }
            if (dgvOptions.Rows.Count == 0)
            {
                clearDetailView();
            }

        }




        //-----------------------------------OPTION GRID--------------------------------------------------------------------

        private void NewOptBtn_Click(object sender, EventArgs e)
        {
            DataTable dataTable = new DataTable();
            if (dgvOptions.RowCount > 0)
            {
                dataTable = (DataTable)dgvOptions.DataSource;
            }
            else
            {
                List<SCOption> scs = new List<SCOption>();
                dataTable = ObjectUtils.ConvertToDataTable(scs);

            }

            //foreach (DataColumn c in dataTable.Columns)
            //{
            //    log.Debug(c.ColumnName + ": " + c.DataType);
            //}

            DataRow myRow = dataTable.NewRow();

            myRow["OID"] = newOptionOid;
            //myRow["Name"] = "NewOption";
            //myRow["ItemNo"] = "ItemNo";
            //myRow["ItemSuplNo"] = "ItemSuplNo";
            //myRow["ItemName"] = "ItemName";
            //myRow["WrksId"] = "WrksId";
            //myRow["WrksName"] = "WrksName";
            //myRow["BaseSelPr"] = 0m;
            //myRow["BuyPr"] = 0m;
            //myRow["SelPr"] = 0m;
            //myRow["Info"] = "Info";
            //myRow["Quantity"] = 0;
            //myRow["isAvailable"] = 1;
            //myRow["isAvailable"] = 1;

            myRow["isMarkDeleted"] = 0;

            dataTable.Rows.InsertAt(myRow, 0);


            //if (dataTable.Rows.Count == 0)
            //    dataTable.Rows.Add(myRow);
            //else
            //    dataTable.Rows.Add(myRow);

            dataTable.AcceptChanges();
            this.dgvOptions.DataSource = dataTable;
            // this.dgvOptions.Rows[0].Selected = true;

            DataGridViewCell cell = dgvOptions.Rows[0].Cells[Constant.OptionName];
            dgvOptions.CurrentCell = cell;
            this.dgvOptions.BeginEdit(true);

            foreach (DataGridViewRow r in dgvOptions.Rows)
            {
                SCOption sc = null;
                if ((int)r.Cells["OptionOID"].Value < 0)
                {
                    SCOption so = null;
                    so = this.categorySelected.Options.Find(x => x.OID == (int)r.Cells["OptionOID"].Value);
                    if (so == null)
                    {
                        sc = RowToOption(r);
                        if (this.categorySelected.Options != null)
                            this.categorySelected.Options.Add(sc);
                        else
                        {
                            this.categorySelected.Options = new List<SCOption>();
                            this.categorySelected.Options.Add(sc);

                        }
                    }
                    //update object in list
                    SCOptionCategory scc = null;
                    scc = this.saveCategories.Find(x => x.OID == this.categorySelected.OID);
                    if (scc != null)
                    {
                        scc.Options = this.categorySelected.Options;
                    }


                }

            }
            newOptionOid = newOptionOid - 1;
            clearDetailView();
        }


        private void DelOptBtn_Click(object sender, EventArgs e)
        {

            if (dgvOptions.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow r in dgvOptions.SelectedRows)
                {
                    var myOid = r.Cells[0].Value;
                    string search = "OID = " + myOid.ToString();
                    DataRow[] rows = this.optionTable.Select(search);
                    foreach (DataRow row in rows)
                    {
                        SCOption sc = null;
                        sc = RowToOption(r);
                        sc.isMarkDeleted = !sc.isMarkDeleted;

                        if (sc != null)
                        {
                            var item = this.categorySelected.Options.Find(x => x.OID == sc.OID);
                            if (item == null && sc.OID > 0)// khong co trong list
                            {
                                this.categorySelected.Options.Add(sc);
                            }
                            else if (item == null && sc.OID < 0) // khong co trong list va co OID nho hon khong
                            {
                                //this.sCContractTypes.Remove(item);
                            }
                            else if (item != null && sc.OID < 0)
                            {
                                this.categorySelected.Options.Remove(item);
                            }
                            else if (item != null && sc.OID > 0)
                            {
                                this.categorySelected.Options.Where(w => w.OID == item.OID).ToList().ForEach(s => s.isMarkDeleted = !s.isMarkDeleted);
                                //item.isMarkDeleted = !item.isMarkDeleted;
                                //this.categorySelected.Options.Remove(item);
                                //this.categorySelected.Options.Add(sc);

                            }
                            if (row["isMarkDeleted"].ToString() == "True")
                            {
                                row["isMarkDeleted"] = 0;
                                //this.categorySelected.Options.Remove(sc);
                            }
                            else
                            {
                                row["isMarkDeleted"] = 1;
                            }
                        }

                    }
                    //remark row delete
                    ViewUtils.remarkHeader(r, Constant.OptionisMarkDeleted);

                }
                this.optionTable.AcceptChanges();
                this.UpdateCategoryList(this.categorySelected);
                dgvOptions.Refresh();


            }
        }



        private void dgvOptions_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //add new

            int rowIndex = e.RowIndex;
            int colIndex = e.ColumnIndex;
            // MessageBox.Show(colIndex.ToString());

            DataGridViewRow row = this.dgvOptions.Rows[rowIndex];
            SCOption sc = new SCOption();
            sc = RowToOption(row);
            //MessageBox.Show(sc.isActive.ToString());

            var item = this.categorySelected.Options.Find(x => x.OID == sc.OID);
            if (item == null)
            {
                this.categorySelected.Options.Add(sc);
                this.optionSelected = sc;
            }
            else
            {
                sc.OptionDetails = item.OptionDetails;
                this.categorySelected.Options.Remove(item);
                this.categorySelected.Options.Add(sc);
                this.optionSelected = item;
            }
            this.UpdateCategoryList(this.categorySelected);


        }

        private void dgvOptions_CellLeave(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvOptions_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //this.editFlag = Constant.Option;
            if (dgvOptions.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvOptions.SelectedRows[0];
                int index = -1;
                var tmp = Int32.TryParse(row.Cells["OptionOID"].Value.ToString(), out index);
                this.OptionOidSelected = index;
                if (this.categorySelected.Options != null)
                {
                    this.optionSelected = this.categorySelected.Options.Find(x => x.OID == this.OptionOidSelected);
                }

                //this.loadOptionDetail(OptionOidSelected);
            }
        }
        private void dgvOptions_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            
           
        }

        private void dgvOptions_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            //this.editFlag = Constant.Option;
            if (dgvOptions.Columns[e.ColumnIndex].Name == Constant.OptionPurcharPrice
                || dgvOptions.Columns[e.ColumnIndex].Name == Constant.OptionSalePrice)
            {
                double i;

                if (!double.TryParse(Convert.ToString(e.FormattedValue), out i))
                {
                    e.Cancel = true;
                    MessageBox.Show("Please enter numeric");
                }
                else
                {

                }
            }

        }
        private void dgvOptions_RowValidated(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void dgvOptions_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            this.dgvOptions.Rows[e.RowIndex].Selected = true;
            if (dgvOptions.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvOptions.SelectedRows[0];
                int index = -1;
                var tmp = Int32.TryParse(row.Cells["OptionOID"].Value.ToString(), out index);
                this.OptionOidSelected = index;
                if (this.categorySelected.Options != null)
                {
                    this.optionSelected = this.categorySelected.Options.Find(x => x.OID == this.OptionOidSelected);
                }

                this.loadOptionDetail(OptionOidSelected);

            }
            //this.displayTree(this.optionSelected.GetType().ToString() + this.dgvOptions.Rows[e.RowIndex].Cells[0].Value);

        }

        //-----------------------------------------OPTION GRID USER FUNCTION----------------------------

        private SCOption RowToOption(DataGridViewRow row)
        {
            SCOption sop = new SCOption();

            int number = -1;
            bool tmp = Int32.TryParse(row.Cells[0].Value.ToString(), out number);

            if (tmp)
                sop.OID = number;
            else
                sop.OID = -1;

            sop.Name = row.Cells["OptionName"].Value.ToString();
            sop.ItemNo = row.Cells["OptionItemNo"].Value.ToString();
            sop.ItemSuplNo = row.Cells["OptionItemSuplNo"].Value.ToString();
            sop.WrksId = row.Cells["OptionWrksId"].Value.ToString();
            sop.ItemName = row.Cells["OptionItemName"].Value.ToString();
            sop.WrksName = row.Cells["OptionWrksName"].Value.ToString();

            var temp = (decimal)0;
            bool rs = Decimal.TryParse(row.Cells["OptionBaseSelPr"].Value.ToString(), out temp);
            if (rs)
                sop.BaseSelPr = Math.Round(temp, 2);
            else
                sop.BaseSelPr = (decimal)0;

            temp = (decimal)0;
            bool rs2 = Decimal.TryParse(row.Cells["OptionBuyPr"].Value.ToString(), out temp);
            if (rs)
                sop.BuyPr = Math.Round(temp, 2);
            else
                sop.BuyPr = (decimal)0;

            temp = (decimal)0;
            bool rs3 = Decimal.TryParse(row.Cells["OptionSelPr"].Value.ToString(), out temp);
            if (rs3)
                sop.SelPr = Math.Round(temp, 2);
            else
                sop.SelPr = (decimal)0;

            return sop;

        }
        public void UpdateCategoryList(SCOptionCategory cat)
        {
            //tim trong lit category
            if (cat.OID > 0)
            {
                SCOptionCategory tmpCat = this.saveCategories.Find(x => x.OID == cat.OID);
                if (tmpCat != null)
                {
                    //cat.Options = tmpCat.Options;
                    //this.saveCategories.Remove(tmpCat);
                    //this.saveCategories.Add(cat);
                    var index = this.saveCategories.IndexOf(tmpCat);
                    if (index != -1)
                        this.saveCategories[index] = cat;
                }
            }
            else
            {
                this.saveCategories.Add(cat);
            }
        }

        private int GetIndexOfRowOption(DataGridView dataGrid, int id)
        {
            for (int i = 0; i < dataGrid.Rows.Count; i += 1)
            {

                SCOption row = new SCOption(); // or.DataBoundItem;
                row = this.RowToOption(dataGrid.Rows[i]);
                if (row.OID == id)
                {
                    return i;
                }
            }

            return 0;
        }


        //--------------------------------------DETAIL GRID-------------------------------------------------------------------
        private int GetIndexOfRowDetail(DataGridView dataGrid, int id)
        {
            for (int i = 0; i < dataGrid.Rows.Count; i += 1)
            {

                SCOptionDetail row = new SCOptionDetail(); // or.DataBoundItem;
                row = this.RowToDetail(dataGrid.Rows[i]);
                if (row.OID == id)
                {
                    return i;
                }
            }

            return 0;
        }

        private void loadOptionDetail(int OptionOid)
        {

            if (dgvDetails.Rows.Count > 0)
            {
                DataTable mydt = new DataTable();
                mydt = (DataTable)this.dgvDetails.DataSource;
                mydt.Clear();
                this.dgvDetails.DataSource = mydt;
                this.dgvDetails.Refresh();
            }
            if (OptionOid > 0)
            {
                List<SCOptionDetail> myDetails = this.optionSelected.OptionDetails;
                detailTable = new DataTable();
                detailTable = ObjectUtils.ConvertToDataTable(myDetails);
                this.dgvDetails.DataSource = detailTable;
            }
            else if (OptionOid < 0 && this.optionSelected != null)
            {
                this.optionSelected = this.categorySelected.Options.Find(x => x.OID == OptionOid);
                if (this.optionSelected != null)
                {
                    List<SCOptionDetail> myDetails = this.optionSelected.OptionDetails;
                    detailTable = new DataTable();
                    detailTable = ObjectUtils.ConvertToDataTable(myDetails);
                    this.dgvDetails.DataSource = detailTable;
                }
            }

        }

        private void clearDetailView()
        {
            //clear detailview
            if (dgvDetails.Rows.Count > 0)
            {
                DataTable mydt = new DataTable();
                mydt = (DataTable)this.dgvDetails.DataSource;
                mydt.Clear();
                this.dgvDetails.DataSource = mydt;
                this.dgvDetails.Refresh();
            }
        }

        private void NewDetailBtn_Click(object sender, EventArgs e)
        {

            // DataTable dataTable = new DataTable();
            if (dgvDetails.RowCount > 0)
            {
                this.detailTable = (DataTable)dgvDetails.DataSource;
            }
            else
            {
                List<SCOptionDetail> scs = new List<SCOptionDetail>();
                //dataTable = ObjectUtils.ConvertToDataTable(scs);
                this.detailTable = ObjectUtils.ConvertToDataTable(scs);

            }



            DataRow myRow = this.detailTable.NewRow();

            myRow["OID"] = newDetailOid;
            //myRow["Name"] = "New Detail";
            //myRow["ItemNo"] = "ItemNo";
            //myRow["ItemSuplNo"] = "ItemSuplNo";
            //myRow["ItemName"] = "ItemName";
            //myRow["WrksId"] = "WrksId";
            //myRow["WrksName"] = "WrksName";
            //myRow["BaseSelPr"] = 0m;
            //myRow["BuyPr"] = 0m;
            //myRow["SelPr"] = 0m;
            //myRow["Info"] = "Info";
            //myRow["Quantity"] = 0;
            //myRow["isAvailable"] = 1;
            //myRow["isAvailable"] = 1;

            myRow["isMarkDeleted"] = 0;

            this.detailTable.Rows.InsertAt(myRow, 0);
            this.detailTable.AcceptChanges();
            this.dgvDetails.DataSource = this.detailTable;
            this.dgvDetails.Refresh();
            this.dgvDetails.Rows[0].Selected = true;

            DataGridViewCell cell = dgvDetails.Rows[0].Cells[Constant.DetailName];
            dgvDetails.CurrentCell = cell;
            this.dgvDetails.BeginEdit(true);



            foreach (DataGridViewRow r in dgvDetails.Rows)
            {
                SCOptionDetail sc = null;
                if ((int)r.Cells["DetailOID"].Value < 0)
                {
                    SCOptionDetail sodt = null;
                    sodt = this.optionSelected.OptionDetails.Find(x => x.OID == (int)r.Cells["DetailOID"].Value);
                    if (sodt == null)
                    {
                        sc = RowToDetail(r);
                        if (this.optionSelected.OptionDetails != null)
                            this.optionSelected.OptionDetails.Add(sc);
                        else
                        {
                            this.optionSelected.OptionDetails = new List<SCOptionDetail>();
                            this.optionSelected.OptionDetails.Add(sc);

                        }
                    }
                    //update object in list
                    //SCOption sc = null;
                    //scc = this.saveCategories.Find(x => x.OID == this.categorySelected.OID);
                    //if (scc != null)
                    //{
                    //    scc.Options = this.categorySelected.Options;
                    //}


                }

            }
            newDetailOid = newDetailOid - 1;
        }

        private SCOptionDetail RowToDetail(DataGridViewRow row)
        {
            SCOptionDetail sod = new SCOptionDetail();

            int number = -1;
            bool tmp = Int32.TryParse(row.Cells[0].Value.ToString(), out number);

            if (tmp)
                sod.OID = number;
            else
                sod.OID = -1;

            sod.Name = row.Cells["DetailName"].Value.ToString();
            sod.ItemNo = row.Cells["DetailItemNo"].Value.ToString();
            sod.ItemSuplNo = row.Cells["DetailItemSuplNo"].Value.ToString();
            sod.WrksId = row.Cells["DetailWrksId"].Value.ToString();
            sod.ItemName = row.Cells["DetailItemName"].Value.ToString();
            sod.WrksName = row.Cells["DetailWrksName"].Value.ToString();

            var temp = (decimal)0;
            bool rs = Decimal.TryParse(row.Cells["DetailBaseSelPr"].Value.ToString(), out temp);
            if (rs)
                sod.BaseSelPr = Math.Round(temp, 2);
            else
                sod.BaseSelPr = (decimal)0;

            temp = (decimal)0;
            bool rs2 = Decimal.TryParse(row.Cells["DetailBuyPr"].Value.ToString(), out temp);
            if (rs)
                sod.BuyPr = Math.Round(temp, 2);
            else
                sod.BuyPr = (decimal)0;

            temp = (decimal)0;
            bool rs3 = Decimal.TryParse(row.Cells["DetailSelPr"].Value.ToString(), out temp);
            if (rs3)
                sod.SelPr = Math.Round(temp, 2);
            else
                sod.SelPr = (decimal)0;

            return sod;

        }

        private void DelDetailBtn_Click(object sender, EventArgs e)
        {

            if (dgvDetails.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow r in dgvDetails.SelectedRows)
                {
                    var myOid = r.Cells[0].Value;
                    string search = "OID = " + myOid.ToString();
                    DataRow[] rows = this.detailTable.Select(search);
                    foreach (DataRow row in rows)
                    {
                        SCOptionDetail sc = null;
                        sc = RowToDetail(r);
                        sc.isMarkDeleted = true;

                        if (sc != null)
                        {
                            var item = this.optionSelected.OptionDetails.Find(x => x.OID == sc.OID);
                            if (item == null && sc.OID > 0)
                            {
                                this.optionSelected.OptionDetails.Add(sc);
                            }
                            else if (item == null && sc.OID < 0)
                            {
                                //this.sCContractTypes.Remove(item);
                            }
                            else if (item != null && sc.OID < 0)
                            {
                                this.optionSelected.OptionDetails.Remove(item);
                            }
                            else if (item != null && sc.OID > 0)
                            {
                                this.optionSelected.OptionDetails.Remove(item);
                                this.optionSelected.OptionDetails.Add(sc);

                            }
                            if (row["isMarkDeleted"].ToString() == "True")
                            {
                                row["isMarkDeleted"] = 0;
                                this.optionSelected.OptionDetails.Remove(sc);
                            }
                            else
                            {
                                row["isMarkDeleted"] = 1;
                            }
                        }
                    }
                    //remark row delete
                    ViewUtils.remarkHeader(r, Constant.DetailisMarkDeleted);
                }
                dgvDetails.Refresh();
                // this.UpdateOptionList(this.categorySelected);
            }
        }

        public void UpdateOptionList(SCOptionDetail opt)
        {
            //tim trong lit category
            if (opt.OID > 0)
            {
                SCOptionDetail tmpCat = this.optionSelected.OptionDetails.Find(x => x.OID == opt.OID);
                if (tmpCat != null)
                {
                    this.optionSelected.OptionDetails.Remove(tmpCat);
                    this.optionSelected.OptionDetails.Add(opt);
                }
            }
            else
            {
                this.optionSelected.OptionDetails.Add(opt);
            }
        }

        private void dgvDetails_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            if (e.StateChanged != DataGridViewElementStates.Selected)
                return;
            else
            {

                int rowIndex =e.Row.Index;

                DataGridViewRow row = dgvDetails.Rows[rowIndex];
                SCOptionDetail sc = new SCOptionDetail();
                sc = RowToDetail(row);
                var item = this.optionSelected.OptionDetails.Single(x => x.OID == sc.OID);
                if (item != null)
                {
                    var index = optionSelected.OptionDetails.IndexOf(item);

                    if (item != null)
                    {
                        if (index != -1)
                        {
                            this.optionSelected.OptionDetails[index] = sc;
                            this.detailSelected = sc;
                        }

                    }
                    else
                    {
                        this.optionSelected.OptionDetails.Add(sc);
                        this.detailSelected = sc;
                    }
                }
            }
        }

        private void dgvDetails_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //this.editFlag = Constant.Detail;
            int rowIndex = e.RowIndex;

            DataGridViewRow row = dgvDetails.Rows[rowIndex];
            SCOptionDetail sc = new SCOptionDetail();
            sc = RowToDetail(row);
            var item = this.optionSelected.OptionDetails.Single(x => x.OID == sc.OID);
            if (item != null)
            {
                var index = optionSelected.OptionDetails.IndexOf(item);

                if (item != null)
                {
                    if (index != -1)
                    {
                        this.optionSelected.OptionDetails[index] = sc;
                        this.detailSelected = sc;
                    }

                }
                else
                {
                    this.optionSelected.OptionDetails.Add(sc);
                    this.detailSelected = sc;
                }
            }
            
        }





        private void dgvDetails_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            //this.editFlag = Constant.Detail;
            if (dgvDetails.Columns[e.ColumnIndex].Name == Constant.DetailPurcharPrice
               || dgvDetails.Columns[e.ColumnIndex].Name == Constant.DetailSalePrice)
            {
                double i;

                if (!double.TryParse(Convert.ToString(e.FormattedValue), out i))
                {
                    e.Cancel = true;
                    MessageBox.Show("Please enter numeric");
                }
                else
                {

                }
            }
        }

        private void dgvDetails_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            //int rowIndex = e.RowIndex;
            //DataGridViewRow row = dgvDetails.Rows[rowIndex];
            //// change color
            //ViewUtils.changeColor(row, Constant.DetailisMarkDeleted);
        }



        private void dgvOptions_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //remark row delete
            if (e.RowIndex > -1)
            {
                ViewUtils.remarkHeader(this.dgvOptions.Rows[e.RowIndex], Constant.OptionisMarkDeleted);
            }
        }

        private void dataGridViewCategory_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                ViewUtils.remarkHeader(this.dataGridViewCategory.Rows[e.RowIndex], Constant.isMarkDeleted);
            }
        }

        private void dgvDetails_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                ViewUtils.remarkHeader(this.dgvDetails.Rows[e.RowIndex], Constant.DetailisMarkDeleted);
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            SCSearchLabourCodeFrm pn = new SCSearchLabourCodeFrm();
            pn.KeySender(Constant.Category);
            pn.ShowDialog();
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //SCSearchItemFrm pn = new SCSearchItemFrm();
            //pn.ShowDialog();

            SCSearchItemFrm pn = new SCSearchItemFrm();
            pn.KeySender(Constant.Category);
            pn.ShowDialog();
        }

       

        private void treeView1_Click(object sender, EventArgs e)
        {

        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.treeSelection(e);
            //this.treeFullPath = e.Node.Name;
        }
        private void treeSelection(TreeViewEventArgs e)
        {

            if (e.Node.Level == 0)// Category
            {
                var item = this.saveCategories.Find(x => x.Name == e.Node.Text);
                if (item != null)
                {
                    this.categorySelected = item;
                    int idx = this.GetIndexOfRowCategory(this.dataGridViewCategory, item.OID);
                    if (idx > -1)
                    {
                        this.dataGridViewCategory.Rows[idx].Cells[1].Selected = true;
                        this.dataGridViewCategory.BeginEdit(true);
                    }

                }
            }

            //start Options
            if (e.Node.Level == 1)// Options
            {
                //get full path
                string[] paths = e.Node.FullPath.Split('\\');
                if (paths.Count() > 0)
                {
                    var cat = this.saveCategories.Find(x => x.Name == paths[0]);
                    if (cat != null)
                    {
                        this.categorySelected = cat;
                        int idx = this.GetIndexOfRowCategory(this.dataGridViewCategory, cat.OID);
                        if (idx > -1)
                        {
                            this.dataGridViewCategory.Rows[idx].Cells[1].Selected = true;
                            this.dataGridViewCategory.BeginEdit(true);
                        }

                    }

                    var item = this.categorySelected.Options.Find(x => x.Name == e.Node.Text);
                    if (item != null)
                    {
                        this.optionSelected = item;
                        int idx = this.GetIndexOfRowOption(this.dgvOptions, item.OID);
                        if (idx > -1)
                        {
                            this.dgvOptions.Rows[idx].Cells[1].Selected = true;
                            this.dgvOptions.BeginEdit(true);
                        }

                    }
                }
            }//-- end of Option


            //----start Detail
            if (e.Node.Level == 2)// Detail
            {
                //get full path
                string[] paths = e.Node.FullPath.Split('\\');
                if (paths.Count() > 0)
                {
                    var cat = this.saveCategories.Find(x => x.Name == paths[0]);
                    if (cat != null)
                    {
                        this.categorySelected = cat;
                        int idx = this.GetIndexOfRowCategory(this.dataGridViewCategory, cat.OID);
                        if (idx > -1)
                        {
                            this.dataGridViewCategory.Rows[idx].Cells[1].Selected = true;
                            this.dataGridViewCategory.BeginEdit(true);
                        }

                    }

                    var option = this.categorySelected.Options.Find(x => x.Name == paths[1]);
                    if (option != null)
                    {
                        this.optionSelected = option;
                        int idx = this.GetIndexOfRowOption(this.dgvOptions, option.OID);
                        if (idx > -1)
                        {
                            this.dgvOptions.Rows[idx].Cells[1].Selected = true;
                            this.dgvOptions.BeginEdit(true);
                        }

                    }

                    var item = this.optionSelected.OptionDetails.Find(x => x.Name == paths[2]);
                    if (item != null)
                    {
                        this.detailSelected = item;
                        int idx = this.GetIndexOfRowDetail(this.dgvDetails, item.OID);
                        if (idx > -1)
                        {
                            this.dgvDetails.Rows[idx].Cells[1].Selected = true;
                            this.dgvDetails.BeginEdit(true);
                        }

                    }
                }
            }//--- end of detail

            this.treeNodeSelected = e.Node;
            this.treeFullPath = e.Node.Name;
        }
        
        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            //this.treeSelection(e);
        }

        private void dgvDetails_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            //SCOptionDetail sod = new SCOptionDetail();
            //this.displayTree(sod.GetType().ToString() + this.dgvDetails.Rows[e.RowIndex].Cells[0].Value);
            
        }

        private void btnOpSearchLabour_Click(object sender, EventArgs e)
        {
            SCSearchLabourCodeFrm pn = new SCSearchLabourCodeFrm();
            pn.KeySender(Constant.Option);
            pn.ShowDialog();
        }

        private void btnDetSearchLabour_Click(object sender, EventArgs e)
        {
            SCSearchLabourCodeFrm pn = new SCSearchLabourCodeFrm();
            pn.KeySender(Constant.Detail);
            pn.ShowDialog();
        }

        private void btnOpSearchPart_Click(object sender, EventArgs e)
        {
            SCSearchItemFrm pn = new SCSearchItemFrm();
            pn.KeySender(Constant.Option);
            pn.ShowDialog();
        }

        private void btnDetSearchPart_Click(object sender, EventArgs e)
        {
            SCSearchItemFrm pn = new SCSearchItemFrm();
            pn.KeySender(Constant.Detail);
            pn.ShowDialog();
        }
    }
}
