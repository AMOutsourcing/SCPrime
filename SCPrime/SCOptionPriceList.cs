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

namespace SCPrime
{
    public partial class SCOptionPriceList : Form
    {
        public SCOptionPriceList()
        {
            InitializeComponent();

            sCContractType = new SCContractType();
            sCBase = new SCBase();
            listContacType = sCBase.getContractTypeActive();
            cbContactType.DataSource = listContacType;
            cbContactType.DisplayMember = "Name";
            cbContactType.ValueMember = "OID";

            //Load sCOptionCategoryBindingSource
            listCategory = SCOptionCategory.getOptionCategoryList();
            sCOptionCategoryBindingSource.DataSource = listCategory;
            sCOptionPriceBindingSource.DataSource = listContacType;

            //
            cbLstCate.DataSource = listCategory;
            cbLstCate.DisplayMember = "Name"; //Name column of contact datasource
            cbLstCate.ValueMember = "OID";//Value column of contact datasource

        }


        SCContractType sCContractType;

        SCBase sCBase;

        private static SCOptionPriceList _instance;

        List<SCOptionCategory> listCategory;

        public SCOptionPriceList instance
        {
            get
            {
                if (SCOptionPriceList._instance == null)
                {
                    SCOptionPriceList._instance = new SCOptionPriceList();
                }
                return SCOptionPriceList._instance;
            }
        }

        private List<SCContractType> listContacType;

        private void cbContactType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cmb = (ComboBox)sender;
            int selectedValue = (int)cmb.SelectedValue;

            loadDataGid(selectedValue);
        }

        private DataTable dtOptionPrice;

        delegate void SetComboBoxCellType(int iRowIndex);

        bool bIsComboBox = false;

        private void loadDataGid(int contactTypeId)
        {
            /*

            gridOptionPrice.Columns.Clear();
            gridOptionPrice.Refresh();

            //gridOptionPrice.AutoGenerateColumns = false;

            List<SCOptionPrice> listOptionPrice = SCContractType.getOptionPriceList(contactTypeId);
            this.dtOptionPrice = Utils.ObjectUtils.ConvertToDataTable(listOptionPrice);

            System.Diagnostics.Debug.WriteLine("dtOptionPrice count: " + this.dtOptionPrice.Rows.Count);
            foreach (DataRow dtRow in this.dtOptionPrice.Rows)
            {
                System.Diagnostics.Debug.WriteLine("myRow {OID}: " + dtRow["OID"].ToString() + " - " + dtRow["CategoryName"].ToString());
            }

            gridOptionPrice.DataSource = this.dtOptionPrice;

            /*
            //Category
            DataGridViewComboBoxColumn dgvCboCate = new DataGridViewComboBoxColumn();
            dgvCboCate.Name = "Category";
            gridOptionPrice.Columns.Add(dgvCboCate);

            //Option
            DataGridViewComboBoxColumn dgvCboOption = new DataGridViewComboBoxColumn();
            dgvCboOption.Name = "Option";
            gridOptionPrice.Columns.Add(dgvCboOption);

            //OptionDetail
            DataGridViewComboBoxColumn dgvCboOptionDetail = new DataGridViewComboBoxColumn();
            dgvCboOptionDetail.Name = "OptionDetail";
            gridOptionPrice.Columns.Add(dgvCboOptionDetail);
            */
            //
            /*

            List<SCOption> listOption = null;
            List<SCOptionDetail> listOptionDetail = null;

            SCOptionPrice sCOptionPrice = null;
            foreach (DataGridViewRow row in gridOptionPrice.Rows)
            {
                if(row.DataBoundItem != null)
                {
                    DataRow myRow = (row.DataBoundItem as DataRowView).Row;
                    System.Diagnostics.Debug.WriteLine("myRow {CategoryName}: " + myRow["CategoryName"].ToString());

                    String optionName = (row.Cells["OptionName"].Value != null) ? row.Cells["OptionName"].Value.ToString() : "";
                    System.Diagnostics.Debug.WriteLine("optionName: " + optionName);

                    sCOptionPrice = row.DataBoundItem as SCOptionPrice;

                    listOption = new List<SCOption>();
                    listOptionDetail = new List<SCOptionDetail>();

                    DataGridViewComboBoxCell cboCate = (DataGridViewComboBoxCell)(row.Cells["Category"]);
                    cboCate.DataSource = listCategory;
                    cboCate.DisplayMember = "Name"; //Name column of contact datasource
                    cboCate.ValueMember = "OID";//Value column of contact datasource
                    //Set value
                    gridOptionPrice.Rows[row.Index].Cells["Category"].Value = myRow["OptionCategoryOID"];

                    listOption.Add(new SCOption(Int32.Parse(row.Cells["OptionOID"].Value.ToString()), (row.Cells["OptionName"].Value != null) ? row.Cells["OptionName"].Value.ToString() : ""));

                    DataGridViewComboBoxCell cboOption = (DataGridViewComboBoxCell)(row.Cells["Option"]);
                    cboOption.DataSource = listOption;
                    cboOption.DisplayMember = "Name"; //Name column of contact datasource
                    cboOption.ValueMember = "OID";//Value column of contact datasource
                    //Set value
                    gridOptionPrice.Rows[row.Index].Cells["Option"].Value = myRow["OptionOID"];


                    listOptionDetail.Add(new SCOptionDetail(Int32.Parse(row.Cells["OptionDetailOID"].Value.ToString()), (row.Cells["OptionDetailName"].Value != null) ? row.Cells["OptionDetailName"].Value.ToString() : ""));

                    DataGridViewComboBoxCell cboOptionDetail = (DataGridViewComboBoxCell)(row.Cells["OptionDetail"]);
                    cboOptionDetail.DataSource = listOptionDetail;
                    cboOptionDetail.DisplayMember = "Name"; //Name column of contact datasource
                    cboOptionDetail.ValueMember = "OID";//Value column of contact datasource
                    //Set value
                    gridOptionPrice.Rows[row.Index].Cells["OptionDetail"].Value = myRow["OptionDetailOID"];
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("row.DataBoundItem iss null " + row.Index);
                }
                
            }
            */
        }

        private void SCOptionList_FormClosed(object sender, FormClosedEventArgs e)
        {
            SCOptionPriceList._instance = null;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (SCOptionPriceList._instance != null)
            {
                SCOptionPriceList._instance.Close();
                SCOptionPriceList._instance = null;
            }
        }

        //
        int newOid = -1;

        private void btnNew_Click(object sender, EventArgs e)
        {
            SCOptionPriceFrm sc = new SCOptionPriceFrm();


            DataTable dataTable = (DataTable)gridOptionPrice.DataSource;
            DataRow drToAdd = dataTable.NewRow();

            drToAdd["OID"] = newOid;
            drToAdd["ContractTypeOID"] = -1;

            dataTable.Rows.InsertAt(drToAdd, 0);
            dataTable.AcceptChanges();

            newOid = newOid - 1;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }

        //
        public SCOptionPriceFrm RowToContractType(DataGridViewRow row)
        {
            /*
            SCOptionPrice sc = new SCOptionPrice();
            int number = -1;
            bool tmp = Int32.TryParse(row.Cells[0].Value.ToString(), out number);

            if (tmp)
                sc.OID = number;
            else
                sc.OID = -1;

            sc.OptionCategoryOID = Int32.Parse(row.Cells[1].Value.ToString());
            */
            return null;
        }

        private void gridOptionPrice_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("gridOptionPrice_EditingControlShowing: " + gridOptionPrice.CurrentCell.ColumnIndex);
            if (gridOptionPrice.CurrentCell.ColumnIndex == 2 && e.Control is ComboBox)
            {
                //sCOptionBindingSource.DataSource = SCOption.getOptionList();

                //sCOptionDetailBindingSource.DataSource = SCOptionDetail.getOptionDetailList();
                ComboBox comboBox = e.Control as ComboBox;
                if (comboBox != null)
                {
                    // Remove an existing event-handler, if present, to avoid 
                    // adding multiple handlers when the editing control is reused.
                    comboBox.SelectedIndexChanged -=
                        new EventHandler(ComboBox_SelectedIndexChanged);

                    // Add the event handler. 
                    comboBox.SelectedIndexChanged +=
                        new EventHandler(ComboBox_SelectedIndexChanged);
                }
            }
            else if (gridOptionPrice.CurrentCell.ColumnIndex == gridOptionPrice.Columns["CategoryName"].Index)
            {
                Show_Combobox(gridOptionPrice.CurrentRow.Index, gridOptionPrice.CurrentCell.ColumnIndex);

                String CategoryName = gridOptionPrice.Rows[gridOptionPrice.CurrentRow.Index].Cells["CategoryName"].Value.ToString();

                String CategoryId = gridOptionPrice.Rows[gridOptionPrice.CurrentRow.Index].Cells["OptionCategoryOID"].Value.ToString();
                int SelectedItem = cbLstCate.FindStringExact(CategoryName);
                System.Diagnostics.Debug.WriteLine("CategoryName: " + CategoryName + " - " + SelectedItem);
                cbLstCate.SelectedIndex = SelectedItem;

                System.Diagnostics.Debug.WriteLine("SelectedItem: " + cbLstCate.SelectedIndex);
                e.Control.Visible = false;
            }
        }
        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("ComboBox_SelectedIndexChanged: " +
                ((ComboBox)sender).SelectedValue + " - " +
                ((ComboBox)sender).SelectedText);
        }

        private void gridOptionPrice_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            SetComboBoxCellType objChangeCellType = new SetComboBoxCellType(ChangeCellToComboBox);
            if (e.ColumnIndex == gridOptionPrice.Columns["OptionCategoryOID"].Index)
            {
                gridOptionPrice.BeginInvoke(objChangeCellType, e.RowIndex);
                bIsComboBox = false;
            }
            else if (e.ColumnIndex == gridOptionPrice.Columns["CategoryName"].Index)
            {
                Show_Combobox(gridOptionPrice.CurrentRow.Index, gridOptionPrice.CurrentCell.ColumnIndex);

                String CategoryName = gridOptionPrice.Rows[gridOptionPrice.CurrentRow.Index].Cells["CategoryName"].Value.ToString();

                String CategoryId = gridOptionPrice.Rows[gridOptionPrice.CurrentRow.Index].Cells["OptionCategoryOID"].Value.ToString();
                int SelectedItem = cbLstCate.FindStringExact(CategoryName);
                System.Diagnostics.Debug.WriteLine("CategoryName: " + CategoryName + " - " + SelectedItem);
                cbLstCate.SelectedIndex = SelectedItem;

                System.Diagnostics.Debug.WriteLine("SelectedItem: " + cbLstCate.SelectedIndex);
            }
        }

        private void ChangeCellToComboBox(int iRowIndex)
        {
            if (bIsComboBox == false)
            {
                DataGridViewComboBoxCell dgComboCell = new DataGridViewComboBoxCell();
                dgComboCell.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;

                System.Diagnostics.Debug.WriteLine("listCategory Items: " + listCategory.Count);
                dgComboCell.DataSource = listCategory;
                dgComboCell.ValueMember = "OID";
                dgComboCell.DisplayMember = "Name";

                System.Diagnostics.Debug.WriteLine("dgComboCell Items: " + dgComboCell.Items.Count);

                if (gridOptionPrice.CurrentCell.ColumnIndex == gridOptionPrice.Columns["CategoryName"].Index)
                {
                    System.Diagnostics.Debug.WriteLine("OptionCategoryOID Value: " + gridOptionPrice.Rows[iRowIndex].Cells["OptionCategoryOID"].Value);

                    SCOptionCategory sCOptionCategory = new SCOptionCategory();
                    int idx = 0;
                    foreach (SCOptionCategory sc in listCategory)
                    {
                        if (sc.OID == Int32.Parse(gridOptionPrice.Rows[iRowIndex].Cells["OptionCategoryOID"].Value.ToString()))
                        {
                            sCOptionCategory = sc;
                            break;
                        }
                        idx++;
                    }
                    //sCOptionCategory.OID = Int32.Parse(gridOptionPrice.Rows[iRowIndex].Cells["OptionCategoryOID"].Value.ToString());
                    //sCOptionCategory.Name = gridOptionPrice.Rows[iRowIndex].Cells["CategoryName"].Value.ToString();

                    dgComboCell.Value = dgComboCell.Items[idx];
                    //dgComboCell.ValueType = typeof(Int32);
                    //dgComboCell.Value = Int32.Parse(gridOptionPrice.Rows[iRowIndex].Cells["OptionCategoryOID"].Value.ToString());
                    System.Diagnostics.Debug.WriteLine("dgComboCell Value: " + dgComboCell.Value);
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("dgComboCell Value: " + dgComboCell.Value);
                }


                gridOptionPrice.Rows[iRowIndex].Cells[gridOptionPrice.CurrentCell.ColumnIndex] = dgComboCell;
                bIsComboBox = true;
            }
        }

        //http://www.encodedna.com/2013/02/show-combobox-datagridview.htm
        // CONTROL THE KEY STROKES.
        protected override bool ProcessCmdKey(ref System.Windows.Forms.Message msg, System.Windows.Forms.Keys keyData)
        {
            System.Diagnostics.Debug.WriteLine("ProcessCmdKey : " + keyData + " ActiveControl.Name: " + ActiveControl.Name);
            if (keyData == Keys.Enter)
            {
                // ON ENTER KEY, GO TO THE NEXT CELL. 
                // WHEN THE CURSOR REACHES THE LAST COLUMN, CARRY IT ON TO THE NEXT ROW.

                if (ActiveControl.Name == "gridOptionPrice")
                {
                    // CHECK IF ITS THE LAST COLUMN
                    if (gridOptionPrice.CurrentCell.ColumnIndex == gridOptionPrice.ColumnCount - 1)
                    {
                        // GO TO THE FIRST COLUMN, NEXT ROW.
                        gridOptionPrice.CurrentCell =
                            gridOptionPrice.Rows[gridOptionPrice.CurrentCell.RowIndex + 1]
                                .Cells[0];
                    }
                    else
                    {
                        // NEXT COLUMN.
                        gridOptionPrice.CurrentCell =
                            gridOptionPrice.Rows[gridOptionPrice.CurrentRow.Index]
                            .Cells[gridOptionPrice.CurrentCell.ColumnIndex + 1];
                    }

                    return true;
                }
                else if (ActiveControl is DataGridViewTextBoxEditingControl)
                {
                    // SHOW THE COMBOBOX WHEN FOCUS IS ON A CELL CORRESPONDING TO THE "QUALIFICATION" COLUMN.
                    if (gridOptionPrice.Columns
                        [gridOptionPrice.CurrentCell.ColumnIndex].Name == "CategoryName")
                    {
                        gridOptionPrice.CurrentCell =
                            gridOptionPrice.Rows[gridOptionPrice.CurrentRow.Index]
                                .Cells[gridOptionPrice.CurrentCell.ColumnIndex + 1];

                        // SHOW COMBOBOX.
                        Show_Combobox(gridOptionPrice.CurrentRow.Index,
                            gridOptionPrice.CurrentCell.ColumnIndex);

                        SendKeys.Send("{F4}");      // DROP DOWN THE LIST.
                        return true;
                    }
                    else
                    {
                        // CHECK IF ITS THE LAST COLUMN.
                        if (gridOptionPrice.CurrentCell.ColumnIndex ==
                            gridOptionPrice.ColumnCount - 1)
                        {
                            // GO TO THE FIRST COLUMN, NEXT ROW.
                            gridOptionPrice.CurrentCell =
                                gridOptionPrice.Rows[gridOptionPrice.CurrentCell.RowIndex + 1]
                                    .Cells[0];
                        }
                        else
                        {
                            // NEXT COLUMN.
                            gridOptionPrice.CurrentCell =
                                gridOptionPrice.Rows[gridOptionPrice.CurrentRow.Index]
                                    .Cells[gridOptionPrice.CurrentCell.ColumnIndex + 1];
                        }
                        return true;
                    }
                }
                else if (ActiveControl.Name == "cbLstCate")
                {
                    // HIDE THE COMBOBOX AND ASSIGN COMBO'S VALUE TO THE CELL.
                    gridOptionPrice.Visible = false;

                    gridOptionPrice.Focus();

                    // ONCE THE COMBO IS SET AS INVISIBLE, SET FOCUS BACK TO THE GRID. (IMPORTANT)
                    gridOptionPrice[gridOptionPrice.CurrentCell.ColumnIndex, gridOptionPrice.CurrentRow.Index].Value = cbLstCate.Text;

                    gridOptionPrice.CurrentCell = gridOptionPrice.Rows[gridOptionPrice.CurrentRow.Index].Cells[gridOptionPrice.CurrentCell.ColumnIndex + 1];
                }
                else
                {
                    SendKeys.Send("{TAB}");
                }
                return true;
            }
            else if (keyData == Keys.Escape)            // PRESS ESCAPE TO HIDE THE COMBOBOX.
            {
                if (ActiveControl.Name == "cbLstCate")
                {
                    cbLstCate.Text = "";
                    cbLstCate.Visible = false;

                    gridOptionPrice.CurrentCell =
                        gridOptionPrice.Rows[gridOptionPrice.CurrentCell.RowIndex]
                            .Cells[gridOptionPrice.CurrentCell.ColumnIndex];

                    gridOptionPrice.Focus();
                }
                return true;
            }
            else
            {
                return base.ProcessCmdKey(ref msg, keyData);
            }
        }

        private void Show_Combobox(int iRowIndex, int iColumnIndex)
        {
            System.Diagnostics.Debug.WriteLine("Show_Combobox");
            // DESCRIPTION: SHOW THE COMBO BOX IN THE SELECTED CELL OF THE GRID.
            // PARAMETERS: iRowIndex - THE ROW ID OF THE GRID.
            //             iColumnIndex - THE COLUMN ID OF THE GRID.

            int x = 0;
            int y = 0;
            int Width = 0;
            int height = 0;

            // GET THE ACTIVE CELL'S DIMENTIONS TO BIND THE COMBOBOX WITH IT.
            Rectangle rect = default(Rectangle);
            rect = gridOptionPrice.GetCellDisplayRectangle(iColumnIndex, iRowIndex, false);
            x = rect.X + gridOptionPrice.Left;
            y = rect.Y + gridOptionPrice.Top;

            Width = rect.Width;
            height = rect.Height;


            cbLstCate.SetBounds(x, y, Width, height);
            cbLstCate.Visible = true;
            cbLstCate.Focus();
        }

        private void cbLstCate_SelectedIndexChanged(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("cbLstCate_SelectedIndexChanged : ");
        }
    }
}
