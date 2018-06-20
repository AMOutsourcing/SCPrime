using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SCPrime.Model;
using SCPrime.Utils;

namespace SCPrime
{
    public partial class SCOptionPriceFrm : Form
    {

        private static SCOptionPriceFrm _instance;

        public static SCOptionPriceFrm getInstance()
        {
            if (SCOptionPriceFrm._instance == null || SCOptionPriceFrm._instance.IsDisposed)
            {
                SCOptionPriceFrm._instance = new SCOptionPriceFrm();
            }
            return SCOptionPriceFrm._instance;
        }

        private List<Model.SCContractType> listContacType;
        Model.SCBase sCBase;
        SCOptionPrice scPriceDao;
        List<SCOptionPrice> scPriceListChange = null;

        private SCOptionPriceFrm()
        {
            InitializeComponent();
            this.Visible = false;
        }

        private void initData()
        {
            //Init timer
            t = new System.Windows.Forms.Timer();
            t.Interval = SystemInformation.DoubleClickTime - 1;
            t.Tick += new EventHandler(t_Tick);

            //
            sCBase = new Model.SCBase();
            listContacType = sCBase.getContractTypes();

            cbContactType.DataSource = listContacType;
            cbContactType.DisplayMember = "Name";
            cbContactType.ValueMember = "OID";

            if (listContacType.Count > 0)
            {
                System.Diagnostics.Debug.WriteLine("initData listContacType[0]: " + listContacType[0].OID);
                cbContactType.SelectedIndex = 0;
                System.Diagnostics.Debug.WriteLine("initData SelectedIndex: " + cbContactType.SelectedIndex + " - " + cbContactType.SelectedValue);

                //Call load grid
                loadDataGrid(listContacType[0].OID);
            }

            //Khoi tao list update
            scPriceListChange = new List<SCOptionPrice>();

            //
            scPriceDao = new SCOptionPrice();
        }

        private void cbContactType_SelectedIndexChanged(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("SelectedIndexChanged: " + cbContactType.SelectedIndex);
            try
            {
                if (cbContactType.SelectedIndex >= 0)
                {
                    System.Diagnostics.Debug.WriteLine("SelectedIndex: " + cbContactType.SelectedIndex);
                    ComboBox cmb = (ComboBox)sender;


                    Int32 selectedValue = Int32.Parse(cmb.SelectedValue.ToString());
                    System.Diagnostics.Debug.WriteLine("cbContactType_SelectedIndexChanged: " + cmb.SelectedValue);
                    loadDataGrid(selectedValue);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("cbContactType_SelectedIndexChanged Exception: " + ex.Message);
                loadDataGrid(-1);
            }
        }

        private void loadDataGrid(Int32 contactId)
        {
            System.Diagnostics.Debug.WriteLine("--------------call loadDataGrid : " + contactId + " ------------");
            //Clear data
            gridPrice.DataSource = null;

            if (contactId > 0)
            {
                //Load data
                List<Model.SCOptionPrice> listData = new Model.SCContractType().getOptionPriceList(contactId);
                System.Diagnostics.Debug.WriteLine("loadDataGrid: " + listData.Count);
                //sCOptionPriceBindingSource.DataSource = listData;
                DataTable dataTable = ObjectUtils.ConvertToDataTable(listData);
                gridPrice.DataSource = dataTable;
                //gridPrice.Refresh();;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            close();
        }

        private void close()
        {
            if (SCOptionPriceFrm._instance != null)
            {
                SCOptionPriceFrm._instance.Close();
                SCOptionPriceFrm._instance.Dispose();
                SCOptionPriceFrm._instance = null;
                //SCOptionPrice._instance = null;

            }
        }

        private void SCOptionPrice_FormClosed(object sender, FormClosedEventArgs e)
        {
            close();
        }

        private void cbContactType_SelectedValueChanged(object sender, EventArgs e)
        {

        }

        bool lockClick = false;

        int IncludeColumnIndex = 4;
        int OptionalColumnIndex = 5;
        int NotAvailableColumnIndex = 6;
        int ExcludeColumnIndex = 7;

        //stackoverflow.com/questions/13453992/cell-double-click-event-in-data-grid-view
        System.Windows.Forms.Timer t;

        void t_Tick(object sender, EventArgs e)
        {
            t.Stop();
            DataGridViewCellEventArgs dgvcea = (DataGridViewCellEventArgs)t.Tag;
            //do whatever you do in single click
        }

        private void gridPrice_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //Start Timer
            t.Tag = e;
            t.Start();


            System.Diagnostics.Debug.WriteLine("--------------gridPrice_CellContentClick ------------ " + e.ColumnIndex + " lockClick: " + lockClick);
            if (lockClick)
            {
            }
            else
            {
                lockClick = true;

                if (e.ColumnIndex == IncludeColumnIndex || e.ColumnIndex == OptionalColumnIndex || e.ColumnIndex == NotAvailableColumnIndex || e.ColumnIndex == ExcludeColumnIndex)//set your checkbox column index instead of 2
                {

                    Boolean Include = Convert.ToBoolean(gridPrice.Rows[e.RowIndex].Cells[IncludeColumnIndex].EditedFormattedValue);
                    Boolean Optional = Convert.ToBoolean(gridPrice.Rows[e.RowIndex].Cells[OptionalColumnIndex].EditedFormattedValue);
                    Boolean NotAvailable = Convert.ToBoolean(gridPrice.Rows[e.RowIndex].Cells[NotAvailableColumnIndex].EditedFormattedValue);
                    Boolean Exclude = Convert.ToBoolean(gridPrice.Rows[e.RowIndex].Cells[ExcludeColumnIndex].EditedFormattedValue);
                    changState(e.RowIndex, e.ColumnIndex, Include, Optional, NotAvailable, Exclude);
                }

                lockClick = false;
            }


        }

        private void changState(int RowIndex, int ColumnIndex, Boolean Include, Boolean Optional, Boolean NotAvailable, Boolean Exclude)
        {
            if (ColumnIndex == IncludeColumnIndex)
            {
                //Doi trang thai Include
                changInclude(RowIndex, Include, Optional, NotAvailable, Exclude);
            }
            else if (ColumnIndex == OptionalColumnIndex)
            {
                //Doi trang thai changOptional
                changOptional(RowIndex, Include, Optional, NotAvailable, Exclude);
            }
            else if (ColumnIndex == NotAvailableColumnIndex)
            {
                //Doi trang thai changNotAvailable
                changNotAvailable(RowIndex, Include, Optional, NotAvailable, Exclude);
            }
            else if (ColumnIndex == ExcludeColumnIndex)
            {
                //Doi trang thai changExclude
                changExclude(RowIndex, Include, Optional, NotAvailable, Exclude);
            }
        }

        private void changInclude(int RowIndex, Boolean Include, Boolean Optional, Boolean NotAvailable, Boolean Exclude)
        {
            if (Include)
            {
                gridPrice.Rows[RowIndex].Cells[OptionalColumnIndex].Value = false;
                gridPrice.Rows[RowIndex].Cells[NotAvailableColumnIndex].Value = false;
            }
            else
            {
                if (Exclude)
                {
                    gridPrice.Rows[RowIndex].Cells[OptionalColumnIndex].Value = false;
                    gridPrice.Rows[RowIndex].Cells[NotAvailableColumnIndex].Value = false;
                }
                else
                {
                    gridPrice.Rows[RowIndex].Cells[OptionalColumnIndex].Value = true;
                    gridPrice.Rows[RowIndex].Cells[NotAvailableColumnIndex].Value = false;
                }

            }
        }

        private void changOptional(int RowIndex, Boolean Include, Boolean Optional, Boolean NotAvailable, Boolean Exclude)
        {
            if (Optional)
            {
                gridPrice.Rows[RowIndex].Cells[IncludeColumnIndex].Value = false;
                gridPrice.Rows[RowIndex].Cells[NotAvailableColumnIndex].Value = false;
                gridPrice.Rows[RowIndex].Cells[ExcludeColumnIndex].Value = false;
            }
            else
            {
                gridPrice.Rows[RowIndex].Cells[IncludeColumnIndex].Value = false;
                gridPrice.Rows[RowIndex].Cells[NotAvailableColumnIndex].Value = true;
                gridPrice.Rows[RowIndex].Cells[ExcludeColumnIndex].Value = false;
            }
        }

        private void changNotAvailable(int RowIndex, Boolean Include, Boolean Optional, Boolean NotAvailable, Boolean Exclude)
        {
            if (NotAvailable)
            {
                gridPrice.Rows[RowIndex].Cells[IncludeColumnIndex].Value = false;
                gridPrice.Rows[RowIndex].Cells[OptionalColumnIndex].Value = false;
                gridPrice.Rows[RowIndex].Cells[ExcludeColumnIndex].Value = false;
            }
            else
            {
                gridPrice.Rows[RowIndex].Cells[IncludeColumnIndex].Value = false;
                gridPrice.Rows[RowIndex].Cells[OptionalColumnIndex].Value = true;
                gridPrice.Rows[RowIndex].Cells[ExcludeColumnIndex].Value = false;
            }
        }

        private void changExclude(int RowIndex, Boolean Include, Boolean Optional, Boolean NotAvailable, Boolean Exclude)
        {
            if (Exclude)
            {
                gridPrice.Rows[RowIndex].Cells[OptionalColumnIndex].Value = false;
                gridPrice.Rows[RowIndex].Cells[NotAvailableColumnIndex].Value = false;
            }
            else
            {
                if (Include)
                {
                    gridPrice.Rows[RowIndex].Cells[OptionalColumnIndex].Value = false;
                    gridPrice.Rows[RowIndex].Cells[NotAvailableColumnIndex].Value = false;
                }
                else
                {
                    gridPrice.Rows[RowIndex].Cells[OptionalColumnIndex].Value = true;
                    gridPrice.Rows[RowIndex].Cells[NotAvailableColumnIndex].Value = false;
                }
            }
        }


        //SAVE
        private void btnSave_Click(object sender, EventArgs e)
        {
            bool result = false;

            if (scPriceListChange.Count > 0)
            {
                foreach (SCOptionPrice sc in scPriceListChange)
                {
                    System.Diagnostics.Debug.WriteLine("--------------SCOptionPrice: " + sc.OID + " - " + sc.IsAvailable);
                }
                result = scPriceDao.save(scPriceListChange);
            }
            if (result)
            {
                //Reload data
                Int32 selectedValue = Int32.Parse(cbContactType.SelectedValue.ToString());
                loadDataGrid(selectedValue);

                //Clear list da dc update
                scPriceListChange.Clear();
            }
            else
            {
                MessageBox.Show("ERROR btnSave_Click");
            }
        }

        private void addToListChange(int rowIndex)
        {
            DataGridViewRow row = gridPrice.Rows[rowIndex];
            SCOptionPrice optionPrice = RowToOptionPrice(row);
            //scPriceListChange
            var item = scPriceListChange.Find(x => x.OID == optionPrice.OID);
            if (item == null)
            {
                scPriceListChange.Add(optionPrice);
            }
            else
            {
                item.IsAvailable = optionPrice.IsAvailable;
            }
        }


        public SCOptionPrice RowToOptionPrice(DataGridViewRow row)
        {
            System.Diagnostics.Debug.WriteLine("--------------RowToOptionPrice: " + row.Cells[0].Value.ToString());
            SCOptionPrice price = new SCOptionPrice();
            int number = -1;
            bool tmp = Int32.TryParse(row.Cells[0].Value.ToString(), out number);

            if (tmp)
                price.OID = number;

            bool Include = (bool)row.Cells[IncludeColumnIndex].Value;
            bool Optional = (bool)row.Cells[OptionalColumnIndex].Value;
            bool NotAvailable = (bool)row.Cells[NotAvailableColumnIndex].Value;
            bool Exclude = (bool)row.Cells[ExcludeColumnIndex].Value;
            if (Include && Exclude)
            {
                price.IsAvailable = 3;
            }
            else if (Include && !Exclude)
            {
                price.IsAvailable = 2;
            }
            else if (Optional)
            {
                price.IsAvailable = 1;
            }
            else if (NotAvailable)
            {
                price.IsAvailable = 0;
            }
            else
            {
                price.IsAvailable = -1;
            }

            System.Diagnostics.Debug.WriteLine("--------------RowToOptionPrice: " + price.OID + " - " + price.IsAvailable);

            return price;
        }

        private void gridPrice_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("--------------CellValueChanged addToListChange ------------" + e.ColumnIndex);
                addToListChange(e.RowIndex);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("-------------- CellValueChangedaddToListChange ERROR: " + ex.Message);
            }
        }

        private void gridPrice_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            t.Stop();
            MessageBox.Show("Double");
        }

        private void SCOptionPriceFrm_Load(object sender, EventArgs e)
        {
            this.Visible = true;
            initData();
        }
    }
}
