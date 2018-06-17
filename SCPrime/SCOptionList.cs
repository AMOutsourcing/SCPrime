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



        private int newOid = -1;
        private int newOptionOid = -1;
        private int newDetailOid = -1;

        // System.Globalization.CultureInfo usCultureInfo = new System.Globalization.CultureInfo("en-US");
        System.Globalization.CultureInfo oldCI = System.Threading.Thread.CurrentThread.CurrentCulture;

        public SCOptionList()
        {
            InitializeComponent();
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

        }
        public SCOptionList instance
        {
            get
            {
                if (SCOptionList._instance == null)
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

        }

        private void dataGridViewCategory_CellLeave(object sender, DataGridViewCellEventArgs e)
        {

            //int rowIndex = e.RowIndex;
            //int colIndex = e.ColumnIndex;
            //// MessageBox.Show(colIndex.ToString());

            //DataGridViewRow row = dataGridViewCategory.Rows[rowIndex];
            //SCOptionCategory sc = new SCOptionCategory();
            //sc = RowToCategory(row);
            ////MessageBox.Show(sc.isActive.ToString());

            //var item = this.saveCategories.SingleOrDefault(x => x.OID == sc.OID);
            //var index = saveCategories.IndexOf(item);



            //if (item != null)
            //{
            //    sc.Options = item.Options;
            //    if (index != -1)
            //        this.saveCategories[index] = sc;
            //}
            //else
            //{
            //    this.saveCategories.Add(sc);
            //}


        }
        private void dataGridViewCategory_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            if (e.StateChanged != DataGridViewElementStates.Selected)
                return;
            if (dataGridViewCategory.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dataGridViewCategory.SelectedRows[0];
                int index = -1;
                var tmp = Int32.TryParse(row.Cells[Constant.OID].Value.ToString(), out index);
                this.CategoryOidSelected = index;
                if (this.saveCategories.Count > 0)
                {
                    this.categorySelected = this.saveCategories.Find(x => x.OID == this.CategoryOidSelected);
                }
                if (this.categorySelected.Options == null)
                {
                    this.categorySelected.Options = new List<SCOption>();

                }
                this.loadOptionList(index);
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
                MessageBox.Show(result.ToString());
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
            //add new

            //int rowIndex = e.RowIndex;
            //int colIndex = e.ColumnIndex;
            //// MessageBox.Show(colIndex.ToString());

            //DataGridViewRow row = dataGridViewCategory.Rows[rowIndex];
            //SCOptionCategory sc = new SCOptionCategory();
            //sc = RowToCategory(row);
            ////MessageBox.Show(sc.isActive.ToString());

            //var item = this.saveCategories.Single(x => x.OID == sc.OID);
            //var index = saveCategories.IndexOf(item);



            //if (item != null)
            //{
            //    sc.Options = item.Options;
            //    //tem = sc;
            //    if (index != -1)
            //        this.saveCategories[index] = sc;
            //}
            //else
            //{
            //    this.saveCategories.Add(sc);
            //}


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
                            //  this.saveCategories.Remove(sc);
                            item.isMarkDeleted = false;
                        }
                        else if (row[Constant.isMarkDeleted].ToString().ToUpper() == "FALSE")
                        {
                            row[Constant.isMarkDeleted] = 1;
                            item.isMarkDeleted = true;
                            // this.DeleteCategoryList(item);
                        }
                        var index = saveCategories.IndexOf(item);
                        if (index != -1)
                        {
                            this.saveCategories[index] = item;
                        }


                    }
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
            drToAdd[Constant.Name] = "NewCategory";
            drToAdd[Constant.InvoiceFlag] = 0;
            drToAdd[Constant.isMarkDeleted] = false;
            drToAdd[Constant.isAvailable] = 1;

            categoryDataTable.Rows.InsertAt(drToAdd, 0);
            // categoryDataTable.AcceptChanges();
            dataGridViewCategory.Refresh();


            foreach (DataGridViewRow r in dataGridViewCategory.Rows)
            {
                SCOptionCategory sc = null;
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
                    dataGridViewCategory.Rows[e.RowIndex].Cells[e.ColumnIndex].Value= Convert.ToDecimal(e.FormattedValue, this.oldCI);
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
            int rowIndex = e.RowIndex;
            int colIndex = e.ColumnIndex;
            // MessageBox.Show(colIndex.ToString());

            DataGridViewRow row = dataGridViewCategory.Rows[rowIndex];
            //find object in list 
            var item2 = this.saveCategories.Single(x => x.OID == (int)dataGridViewCategory.Rows[rowIndex].Cells["OID"].Value);
            if (item2 != null)
            {
                SCOptionCategory sc = new SCOptionCategory();
                sc = RowToCategory(row);
                //MessageBox.Show(sc.isActive.ToString());

                var item = this.saveCategories.Single(x => x.OID == sc.OID);
                var index = saveCategories.IndexOf(item);



                if (item != null)
                {
                    sc.Options = item.Options;
                    //tem = sc;
                    if (index != -1)
                        this.saveCategories[index] = sc;
                }
                else
                {
                    this.saveCategories.Add(sc);
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
                    treeView1.Nodes.Add(treeNode);

                    //load all Option
                    List<SCOption> myOptions = new List<SCOption>();
                    myOptions = SCOption.getOptionList(cat.OID);
                    if (myOptions.Count > 0)
                    {
                        foreach (SCOption op in myOptions)
                        {
                            TreeNode treeNodeL2 = new TreeNode(op.Name);
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
                                    treeNodeL2.Nodes.Add(treeNodeL3);
                                }
                            }
                        }
                    }
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
            else if (CategoryOid < 0)
            {
                this.categorySelected = this.saveCategories.Find(x => x.OID == CategoryOid);
                List<SCOption> myOptions = this.categorySelected.Options;
                optionTable = new DataTable();
                optionTable = ObjectUtils.ConvertToDataTable(myOptions);
                this.dgvOptions.DataSource = optionTable;
            }

            dgvOptions.Refresh();

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
            myRow["Name"] = "NewOption";
            myRow["ItemNo"] = "ItemNo";
            myRow["ItemSuplNo"] = "ItemSuplNo";
            myRow["ItemName"] = "ItemName";
            myRow["WrksId"] = "WrksId";
            myRow["WrksName"] = "WrksName";
            myRow["BaseSelPr"] = 0m;
            myRow["BuyPr"] = 0m;
            myRow["SelPr"] = 0m;
            myRow["Info"] = "Info";
            myRow["Quantity"] = 0;
            myRow["isAvailable"] = 1;
            myRow["isAvailable"] = 1;

            myRow["isMarkDeleted"] = 0;

            dataTable.Rows.Add(myRow);

            //if (dataTable.Rows.Count == 0)
            //    dataTable.Rows.Add(myRow);
            //else
            //    dataTable.Rows.Add(myRow);

            dataTable.AcceptChanges();
            this.dgvOptions.DataSource = dataTable;

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
            }
            else
            {
                sc.OptionDetails = item.OptionDetails;
                this.categorySelected.Options.Remove(item);
                this.categorySelected.Options.Add(sc);
            }
            this.UpdateCategoryList(this.categorySelected);
        }

        private void dgvOptions_CellLeave(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvOptions_CellClick(object sender, DataGridViewCellEventArgs e)
        {
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
            //if (e.StateChanged != DataGridViewElementStates.Selected)
            //return;
            //if (dgvOptions.SelectedRows.Count > 0)
            //{
            //    DataGridViewRow row = dgvOptions.SelectedRows[0];
            //    int index = -1;
            //    var tmp = Int32.TryParse(row.Cells["OptionOID"].Value.ToString(), out index);
            //    this.OptionOidSelected = index;
            //    if (this.categorySelected.Options != null)
            //    {
            //        this.optionSelected = this.categorySelected.Options.Find(x => x.OID == this.OptionOidSelected);
            //    }

            //    this.loadOptionDetail(OptionOidSelected);
            //}


        }

        private void dgvOptions_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
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
            myRow["Name"] = "New Detail";
            myRow["ItemNo"] = "ItemNo";
            myRow["ItemSuplNo"] = "ItemSuplNo";
            myRow["ItemName"] = "ItemName";
            myRow["WrksId"] = "WrksId";
            myRow["WrksName"] = "WrksName";
            myRow["BaseSelPr"] = 0m;
            myRow["BuyPr"] = 0m;
            myRow["SelPr"] = 0m;
            myRow["Info"] = "Info";
            myRow["Quantity"] = 0;
            myRow["isAvailable"] = 1;
            myRow["isAvailable"] = 1;

            myRow["isMarkDeleted"] = 0;

            this.detailTable.Rows.Add(myRow);
            this.detailTable.AcceptChanges();
            this.dgvDetails.DataSource = this.detailTable;
            this.dgvDetails.Refresh();

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
            //    if (e.StateChanged != DataGridViewElementStates.Selected)
            //        return;
            //    if (dgvDetails.SelectedRows.Count > 0)
            //    {
            //        DataGridViewRow row = dgvDetails.SelectedRows[0];
            //        int index = -1;
            //        var tmp = Int32.TryParse(row.Cells["DetailOID"].Value.ToString(), out index);
            //        SCOptionDetail sod = new SCOptionDetail();
            //        //this.OptionOidSelected = index;
            //        if (this.optionSelected.OptionDetails!= null)
            //        {
            //            this.optionSelected = this.categorySelected.Options.Find(x => x.OID == this.OptionOidSelected);
            //        }

            //        this.loadOptionDetail(OptionOidSelected);
            //    }
        }

        private void dgvDetails_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
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
                        this.optionSelected.OptionDetails[index] = sc;
                }
                else
                {
                    this.optionSelected.OptionDetails.Add(sc);
                }
            }
        }





        private void dgvDetails_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
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

        
    }
}
