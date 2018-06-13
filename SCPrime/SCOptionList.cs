using log4net;
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

namespace SCPrime
{
    public partial class SCOptionList : Form
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


        private int newOid = -1;
        private int newOptionOid = -1;
        public SCOptionList()
        {
            InitializeComponent();
            this.saveCategories = new List<SCOptionCategory>();
            this.saveOptions = new List<SCOption>();

            this.bindingList = new List<KeyValue>();
            this.bindingList.Add(new KeyValue(0, "Empty"));
            this.bindingList.Add(new KeyValue(1, "First Invoice"));
            this.bindingList.Add(new KeyValue(2, "Last Invoice"));


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
            for (var i = 0; i < this.dataGridViewCategory.ColumnCount; i++)
            {
                var name = dataGridViewCategory.Columns[i].HeaderText;
                log.Debug(name);

            }
            log.Debug("OK");
        }

        private void dataGridViewCategory_CellLeave(object sender, DataGridViewCellEventArgs e)
        {

            int rowIndex = e.RowIndex;
            int colIndex = e.ColumnIndex;
            // MessageBox.Show(colIndex.ToString());

            DataGridViewRow row = dataGridViewCategory.Rows[rowIndex];
            SCOptionCategory sc = new SCOptionCategory();
            sc = RowToCategory(row);
            //MessageBox.Show(sc.isActive.ToString());

            var item = this.saveCategories.SingleOrDefault(x => x.OID == sc.OID);
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

        private void dataGridViewCategory_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            int colIndex = e.ColumnIndex;
            if (rowIndex >= 0)
            {
                DataGridViewRow row = dataGridViewCategory.Rows[rowIndex];
                int index = -1;
                var tmp = Int32.TryParse(row.Cells["OID"].Value.ToString(), out index);
                this.CategoryOidSelected = index;
                this.categorySelected = this.saveCategories.Find(x => x.OID == this.CategoryOidSelected);
                if (this.categorySelected.Options == null)
                    this.categorySelected.Options = new List<SCOption>();
                this.loadOptionList(index);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //  SCOptionCategory oc = new SCOptionCategory();
            if (this.saveCategories.Count > 0)
            {
                var result = SCOptionCategory.saveOptionCategoryList(this.saveCategories);
                MessageBox.Show(result.ToString());
            }

            loadCategoryData();
            loadTree();

        }

        private void dataGridViewCategory_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //add new

            int rowIndex = e.RowIndex;
            int colIndex = e.ColumnIndex;
            // MessageBox.Show(colIndex.ToString());

            DataGridViewRow row = dataGridViewCategory.Rows[rowIndex];
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



        private void DelCatBtn_Click(object sender, EventArgs e)
        {

            if (dataGridViewCategory.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow r in dataGridViewCategory.SelectedRows)
                {
                    var myOid = r.Cells[0].Value;
                    string search = "OID = " + myOid.ToString();
                    DataRow[] rows = this.categoryDataTable.Select(search);
                    foreach (DataRow row in rows)
                    {

                        SCOptionCategory item = null;

                        item = this.saveCategories.FirstOrDefault(x => x.OID == (int)r.Cells[0].Value);



                        if (row["isMarkDeleted"].ToString().ToUpper() == "TRUE")
                        {
                            row["isMarkDeleted"] = 0;
                            //  this.saveCategories.Remove(sc);
                            item.isMarkDeleted = false;
                        }
                        else if (row["isMarkDeleted"].ToString().ToUpper() == "FALSE")
                        {
                            row["isMarkDeleted"] = 1;
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
                this.categoryDataTable.AcceptChanges();
                dataGridViewCategory.Refresh();
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
            // MessageBox.Show(datasource.Count.ToString());
            //SCOptionCategory cat = new SCOptionCategory();


            DataTable dataTable = (DataTable)dataGridViewCategory.DataSource;
            DataRow drToAdd = dataTable.NewRow();

            drToAdd["OID"] = newOid;
            drToAdd["Name"] = "NewCategory";
            drToAdd["InvoiceFlag"] = 0;
            drToAdd["isMarkDeleted"] = false;
            drToAdd["isAvailable"] = 1;

            dataTable.Rows.InsertAt(drToAdd, 0);
            dataTable.AcceptChanges();


            foreach (DataGridViewRow r in dataGridViewCategory.Rows)
            {
                SCOptionCategory sc = null;
                if ((int)r.Cells["OID"].Value < 0)// object mới
                {
                    SCOptionCategory so = null;
                    so = this.saveCategories.Find(x => x.OID == (int)r.Cells["OID"].Value);
                    if (so == null)
                    {
                        this.saveCategories.Add(so);
                    }

                }

            }

            newOid = newOid - 1;

        }

        //------------------------------------------- User function--------------------------------------------------------------------

        private void loadCategoryData()
        {
            this.saveCategories.Clear();
            this.saveCategories = SCOptionCategory.getOptionCategoryList();
            // MessageBox.Show(myCategories.Count.ToString());
            this.categoryDataTable = ObjectUtils.ConvertToDataTable(this.saveCategories);

            bool flag = false;
            foreach (DataGridViewColumn dc in dataGridViewCategory.Columns)
            {
                if (dc.Name == "InvoiceFlag")
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

            //((DataGridViewComboBoxColumn)dataGridViewCategory.Columns["Invoice"]).DataSource = this.bindingList;
        }

        private void loadTree()
        {
            //load category
            treeView1.Nodes.Clear();

            List<SCOptionCategory> myCategories = SCOptionCategory.getOptionCategoryList();
            if (this.saveCategories.Count > 0)
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
                sc.BaseSelPr = temp;
            else
                sc.BaseSelPr = (decimal)0;

            temp = (decimal)0;
            bool rs2 = Decimal.TryParse(row.Cells["BuyPr"].Value.ToString(), out temp);
            if (rs)
                sc.BuyPr = temp;
            else
                sc.BuyPr = (decimal)0;

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
        }

        private void dataGridViewCategory_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {

        }

        private void dataGridViewCategory_SelectionChanged(object sender, EventArgs e)
        {


        }


        //-----------------------------------OPTION GRID--------------------------------------------------------------------

        private void NewOptBtn_Click(object sender, EventArgs e)
        {
            DataTable dataTable = null;
            if (dgvOptions.RowCount > 0)
            {
                dataTable = (DataTable)dgvOptions.DataSource;
            }
            else
            {
                List<SCOption> scs = new List<SCOption>();
                dataTable = ObjectUtils.ConvertToDataTable(scs);
                this.dgvOptions.DataSource = dataTable;
            }
            DataRow drToAdd = dataTable.NewRow();

            drToAdd["OID"] = newOptionOid;
            drToAdd["Name"] = "NewOption";
            drToAdd["isMarkDeleted"] = false;

            dataTable.Rows.InsertAt(drToAdd, 0);
            dataTable.AcceptChanges();


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
                        sc.isMarkDeleted = true;

                        if (sc != null)
                        {
                            var item = this.categorySelected.Options.Find(x => x.OID == sc.OID);
                            if (item == null && sc.OID > 0)
                            {
                                this.categorySelected.Options.Add(sc);
                            }
                            else if (item == null && sc.OID < 0)
                            {
                                //this.sCContractTypes.Remove(item);
                            }
                            else if (item != null && sc.OID < 0)
                            {
                                this.categorySelected.Options.Remove(item);
                            }
                            else if (item != null && sc.OID > 0)
                            {
                                this.categorySelected.Options.Remove(item);
                                this.categorySelected.Options.Add(sc);

                            }
                            if (row["isMarkDeleted"].ToString() == "True")
                            {
                                row["isMarkDeleted"] = 0;
                                this.categorySelected.Options.Remove(sc);
                            }
                            else
                            {
                                row["isMarkDeleted"] = 1;
                            }
                        }
                    }
                }
                dgvOptions.Refresh();
                this.UpdateCategoryList(this.categorySelected);
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
                this.categorySelected.Options.Remove(item);
                this.categorySelected.Options.Add(sc);
            }
            this.UpdateCategoryList(this.categorySelected);
        }

       



        private void dgvOptions_CellLeave(object sender, DataGridViewCellEventArgs e)
        {

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
                sop.BaseSelPr = temp;
            else
                sop.BaseSelPr = (decimal)0;

            temp = (decimal)0;
            bool rs2 = Decimal.TryParse(row.Cells["OptionBuyPr"].Value.ToString(), out temp);
            if (rs)
                sop.BuyPr = temp;
            else
                sop.BuyPr = (decimal)0;

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
                    this.saveCategories.Remove(tmpCat);
                    this.saveCategories.Add(cat);
                }
            }
            else
            {
                this.saveCategories.Add(cat);
            }
        }

       //--------------------------------------DETAIL GRID-------------------------------------------------------------------
    }
}
