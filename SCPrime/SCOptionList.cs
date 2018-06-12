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
        private List<SCOptionCategory> myCategories;
        private DataTable categoryDataTable;
        private List<KeyValue> bindingList;

        private List<SCOptionCategory> saveCategories;

        private int newOid = -1;
        public SCOptionList()
        {
            InitializeComponent();
            this.saveCategories = new List<SCOptionCategory>();
            this.bindingList = new List<KeyValue>(); 
            this.bindingList.Add(new KeyValue(0,"Empty"));
            this.bindingList.Add(new KeyValue(1,"First Invoice"));
            this.bindingList.Add(new KeyValue(2,"Last Invoice"));


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

        private void loadCategoryData()
        {
            this.myCategories = SCOptionCategory.getOptionCategoryList();
            // MessageBox.Show(myCategories.Count.ToString());
            this.categoryDataTable = ObjectUtils.ConvertToDataTable(this.myCategories);

            bool flag = false;
            foreach(DataGridViewColumn dc in dataGridViewCategory.Columns)
            {
                if(dc.Name == "InvoiceFlag")
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

            //  List<SCOptionCategory> myCategories = SCOptionCategory.getOptionCategoryList();
            if (this.myCategories.Count > 0)
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
            SCOptionCategory cat = new SCOptionCategory();


            DataTable dataTable = (DataTable)dataGridViewCategory.DataSource;
            DataRow drToAdd = dataTable.NewRow();

            drToAdd["OID"] = newOid;
            drToAdd["Name"] = "NewCategory";
            drToAdd["InvoiceFlag"] = 0;
            drToAdd["isMarkDeleted"] = false;

            dataTable.Rows.InsertAt(drToAdd, 0);
            dataTable.AcceptChanges();

            newOid = newOid - 1;

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
            sc.WrksName = row.Cells["WrksName"].ToString();
            sc.InvoiceFlag = (int)row.Cells["InvoiceFlag"].Value;

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

        private void button1_Click(object sender, EventArgs e)
        {
            SCOptionCategory oc = new SCOptionCategory();
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

            var item = this.saveCategories.Find(x => x.OID == sc.OID);
            if (item == null)
            {
                this.saveCategories.Add(sc);
            }
            else
            {
                this.saveCategories.Remove(item);
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
                        SCOptionCategory sc = null;
                        sc = RowToCategory(r);
                        sc.isMarkDeleted = true;

                        if (sc != null)
                        {
                            var item = this.saveCategories.Find(x => x.OID == sc.OID);
                            if (item == null && sc.OID > 0)
                            {
                                this.saveCategories.Add(sc);
                            }
                            else if (item == null && sc.OID < 0)
                            {
                                //this.sCContractTypes.Remove(item);
                            }
                            else if (item != null && sc.OID < 0)
                            {
                                this.saveCategories.Remove(item);
                            }
                            else if (item != null && sc.OID > 0)
                            {
                                this.saveCategories.Remove(item);
                                this.saveCategories.Add(sc);

                            }
                            row.Delete();
                        }
                    }
                }
                dataGridViewCategory.Refresh();
            }
        }

        private void dataGridViewCategory_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            //int rowIndex = e.RowIndex;
            //int colIndex = e.ColumnIndex;
            //DataGridViewRow row = dgvOptions.Rows[rowIndex];
            //int index = -1;
            //var tmp = Int32.TryParse(row.Cells["OptionOID"].Value.ToString(), out index);
            //this.loadOptionList(index);
        }
        private void loadOptionList( int CategoryOid)
        {
            List<SCOption> myOptions = SCOption.getOptionList(CategoryOid);
            DataTable optionTable = new DataTable();
            optionTable = ObjectUtils.ConvertToDataTable(myOptions);
            this.dgvOptions.DataSource = optionTable;
        }

        private void dataGridViewCategory_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {

        }

        private void dataGridViewCategory_SelectionChanged(object sender, EventArgs e)
        {
          

        }

        private void dataGridViewCategory_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            int colIndex = e.ColumnIndex;
            DataGridViewRow row = dataGridViewCategory.Rows[rowIndex];
            int index = -1;
            var tmp = Int32.TryParse(row.Cells["OID"].Value.ToString(), out index);
            this.loadOptionList(index);
        }
    }
}
