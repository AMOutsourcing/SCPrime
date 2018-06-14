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

        public static SCOptionPriceFrm getInstance
        {
            get
            {
                if (SCOptionPriceFrm._instance == null)
                {
                    SCOptionPriceFrm._instance = new SCOptionPriceFrm();
                }
                return SCOptionPriceFrm._instance;
            }
        }

        private List<Model.SCContractType> listContacType;
        Model.SCBase sCBase;
        SCOptionPrice scPriceDao;
        List<SCOptionPrice> scPriceListChange = null;

        public SCOptionPriceFrm()
        {
            System.Diagnostics.Debug.WriteLine("=================================SCOptionPrice=============================================: ");
            InitializeComponent();
            initData();
        }

        private void initData()
        {
            System.Diagnostics.Debug.WriteLine("initData: ");
            sCBase = new Model.SCBase();
            listContacType = sCBase.getContractTypeActive();

            System.Diagnostics.Debug.WriteLine("---DataSource----");
            cbContactType.DataSource = listContacType;
            System.Diagnostics.Debug.WriteLine("---DisplayMember----");
            cbContactType.DisplayMember = "Name";
            System.Diagnostics.Debug.WriteLine("---ValueMember----");
            cbContactType.ValueMember = "OID";
            System.Diagnostics.Debug.WriteLine("---listContacType IF----");
            System.Diagnostics.Debug.WriteLine("listContacType: " + listContacType.Count);

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
            gridPrice.AutoGenerateColumns = true;
            gridPrice.Columns.Clear();
            gridPrice.Refresh();
            if (contactId > 0)
            {
                //Load data
                List<Model.SCOptionPrice> listData = new Model.SCContractType().getOptionPriceList(contactId);
                System.Diagnostics.Debug.WriteLine("loadDataGrid: " + listData.Count);
                //sCOptionPriceBindingSource.DataSource = listData;
                DataTable dataTable = ObjectUtils.ConvertToDataTable(listData);
                gridPrice.DataSource = dataTable;
                //gridPrice.Refresh();
            }


            //An column
            /*
            gridPrice.Columns["OID"].Visible = false;
            gridPrice.Columns["ContractTypeOID"].Visible = false;
            gridPrice.Columns["OptionCategoryOID"].Visible = false;
            gridPrice.Columns["OptionOID"].Visible = false;
            gridPrice.Columns["OptionDetailOID"].Visible = false;
            gridPrice.Columns["IsAvailable"].Visible = false;
            gridPrice.Columns["Info"].Visible = false;
            gridPrice.Columns["Created"].Visible = false;
            gridPrice.Columns["Modified"].Visible = false;
            */

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
        private void gridPrice_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (lockClick)
            {
                System.Diagnostics.Debug.WriteLine("--------------gridPrice_CellContentClick dang lock roi------------ ");
            }
            else
            {
                lockClick = true;
                System.Diagnostics.Debug.WriteLine("--------------gridPrice_CellContentClick ------------ " + e.ColumnIndex);
                if (e.ColumnIndex == 9 || e.ColumnIndex == 10 || e.ColumnIndex == 11 || e.ColumnIndex == 12)//set your checkbox column index instead of 2
                {

                    Boolean Include = Convert.ToBoolean(gridPrice.Rows[e.RowIndex].Cells[9].EditedFormattedValue);
                    Boolean Optional = Convert.ToBoolean(gridPrice.Rows[e.RowIndex].Cells[10].EditedFormattedValue);
                    Boolean NotAvailable = Convert.ToBoolean(gridPrice.Rows[e.RowIndex].Cells[11].EditedFormattedValue);
                    Boolean Exclude = Convert.ToBoolean(gridPrice.Rows[e.RowIndex].Cells[12].EditedFormattedValue);
                    changState(e.RowIndex, e.ColumnIndex, Include, Optional, NotAvailable, Exclude);
                }

                lockClick = false;
                System.Diagnostics.Debug.WriteLine("--------------gridPrice_CellContentClick giai phong lock------------ ");
            }
        }

        private void changState(int RowIndex, int ColumnIndex, Boolean Include, Boolean Optional, Boolean NotAvailable, Boolean Exclude)
        {
            if (ColumnIndex == 9)
            {
                //Doi trang thai Include
                changInclude(RowIndex, Include, Optional, NotAvailable, Exclude);
            }
            else if (ColumnIndex == 10)
            {
                //Doi trang thai changOptional
                changOptional(RowIndex, Include, Optional, NotAvailable, Exclude);
            }
            else if (ColumnIndex == 11)
            {
                //Doi trang thai changNotAvailable
                changNotAvailable(RowIndex, Include, Optional, NotAvailable, Exclude);
            }
            else if (ColumnIndex == 12)
            {
                //Doi trang thai changExclude
                changExclude(RowIndex, Include, Optional, NotAvailable, Exclude);
            }
        }

        private void changInclude(int RowIndex, Boolean Include, Boolean Optional, Boolean NotAvailable, Boolean Exclude)
        {
            if (Include)
            {
                gridPrice.Rows[RowIndex].Cells[10].Value = false;
                gridPrice.Rows[RowIndex].Cells[11].Value = false;
            }
            else
            {
                if (Exclude)
                {
                    gridPrice.Rows[RowIndex].Cells[10].Value = false;
                    gridPrice.Rows[RowIndex].Cells[11].Value = false;
                }
                else
                {
                    gridPrice.Rows[RowIndex].Cells[10].Value = true;
                    gridPrice.Rows[RowIndex].Cells[11].Value = false;
                }

            }
        }

        private void changOptional(int RowIndex, Boolean Include, Boolean Optional, Boolean NotAvailable, Boolean Exclude)
        {
            if (Optional)
            {
                gridPrice.Rows[RowIndex].Cells[9].Value = false;
                gridPrice.Rows[RowIndex].Cells[11].Value = false;
                gridPrice.Rows[RowIndex].Cells[12].Value = false;
            }
            else
            {
                gridPrice.Rows[RowIndex].Cells[9].Value = false;
                gridPrice.Rows[RowIndex].Cells[11].Value = true;
                gridPrice.Rows[RowIndex].Cells[12].Value = false;
            }
        }

        private void changNotAvailable(int RowIndex, Boolean Include, Boolean Optional, Boolean NotAvailable, Boolean Exclude)
        {
            if (NotAvailable)
            {
                gridPrice.Rows[RowIndex].Cells[9].Value = false;
                gridPrice.Rows[RowIndex].Cells[10].Value = false;
                gridPrice.Rows[RowIndex].Cells[12].Value = false;
            }
            else
            {
                gridPrice.Rows[RowIndex].Cells[9].Value = false;
                gridPrice.Rows[RowIndex].Cells[10].Value = true;
                gridPrice.Rows[RowIndex].Cells[12].Value = false;
            }
        }

        private void changExclude(int RowIndex, Boolean Include, Boolean Optional, Boolean NotAvailable, Boolean Exclude)
        {
            if (Exclude)
            {
                gridPrice.Rows[RowIndex].Cells[10].Value = false;
                gridPrice.Rows[RowIndex].Cells[12].Value = false;
            }
            else
            {
                if (Include)
                {
                    gridPrice.Rows[RowIndex].Cells[10].Value = false;
                    gridPrice.Rows[RowIndex].Cells[12].Value = false;
                }
                else
                {
                    gridPrice.Rows[RowIndex].Cells[10].Value = true;
                    gridPrice.Rows[RowIndex].Cells[12].Value = false;
                }
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            bool result = false;
            if (scPriceListChange.Count > 0)
            {
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


        public SCOptionPrice RowToOptionPrice(DataGridViewRow row)
        {
            SCOptionPrice price = new SCOptionPrice();
            int number = -1;
            bool tmp = Int32.TryParse(row.Cells[0].Value.ToString(), out number);

            if (tmp)
                price.OID = number;

            bool Include = (bool)row.Cells[9].Value;
            bool Optional = (bool)row.Cells[10].Value;
            bool NotAvailable = (bool)row.Cells[11].Value;
            bool Exclude = (bool)row.Cells[12].Value;
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

            return price;
        }

        private void gridPrice_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("--------------gridPrice_CellValueChanged ------------" + e.ColumnIndex);
        }
    }
}
