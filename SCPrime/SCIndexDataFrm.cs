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
using log4net;
using nsBaseClass;
using SCPrime.Model;
using SCPrime.Utils;

namespace SCPrime
{
    public partial class SCIndexDataFrm : Form
    {
        static readonly ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public SCIndexDataFrm()
        {
            InitializeComponent();
        }

        private static SCIndexDataFrm _instance;

        public static SCIndexDataFrm getInstance()
        {
            if (SCIndexDataFrm._instance == null || SCIndexDataFrm._instance.IsDisposed)
            {
                SCIndexDataFrm._instance = new SCIndexDataFrm();
            }
            return SCIndexDataFrm._instance;
        }

        private void SCIndexDataFrm_Load(object sender, EventArgs e)
        {
            loadData();
        }

        List<SCIndexData> listDataChange = new List<SCIndexData>();
        List<SCIndexData> listData = null;
        DataTable dataTable;

        private void loadData()
        {
            newOid = -1;
            listDataChange = new List<SCIndexData>();
            gridData.DataSource = null;
            listData = SCIndexData.getIndexData();
            if (listData == null)
                listData = new List<SCIndexData>();
            dataTable = ObjectUtils.ConvertToDataTable(listData);
            gridData.DataSource = dataTable;
        }

        private int newOid = -1;
        private void btnNew_Click(object sender, EventArgs e)
        {
            SCIndexData obj = new SCIndexData();
            obj.OID = newOid--;
            obj.IndexMonth = 0;
            obj.IndexYear = 0;
            obj.IndexValue = 0;

            dataTable = (DataTable)gridData.DataSource;
            if (dataTable == null || dataTable.Rows.Count <= 0)
            {
                dataTable = ObjectUtils.ConvertToDataTable(new List<SCIndexData> { obj });
                gridData.DataSource = dataTable;
            }
            else
            {
                DataRow drToAdd = dataTable.NewRow();
                drToAdd[Constant.OID] = obj.OID;
                drToAdd["IndexYear"] = obj.IndexYear;
                drToAdd["IndexMonth"] = obj.IndexMonth;
                drToAdd["IndexValue"] = obj.IndexValue;
                dataTable.Rows.Add(drToAdd);
                dataTable.AcceptChanges();
            }

            gridData.Refresh();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int selectedRow = gridData.SelectedRows.Count;
            if (selectedRow > 0)
            {
                foreach (DataGridViewRow r in gridData.SelectedRows)
                {
                    SCIndexData obj = new SCIndexData();
                    obj.OID = Int32.Parse(r.Cells[0].Value.ToString());
                    obj.isDelete = true;

                    //Add list delete
                    SCIndexData finder = listDataChange.Find(x => x.OID == obj.OID);
                    if(finder == null)
                        listDataChange.Add(obj);
                    else
                        finder.isDelete = true;
                    
                    if (obj.OID > 0)
                    {
                        listData.RemoveAll(x => x.OID == obj.OID);
                    }

                    r.Cells["isDelete"].Value = true;
                    //Mark as delete with SCIndexData in db
                    ViewUtils.remarkHeader(r, "isDelete");

                    //if (obj.OID > 0)
                    //{
                    //    r.Cells["isDelete"].Value = true;
                    //    //Mark as delete with SCIndexData in db
                    //    ViewUtils.remarkHeader(r, "isDelete");
                    //}
                    //else
                    //{
                    //    //Delete from datatable with SCIndexData add new
                    //    dataTable.Rows.RemoveAt(r.Index);
                    //    dataTable.AcceptChanges();
                    //}   
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SCIndexData.save(listDataChange);
            //Reload data
            loadData();
        }

        private void gridData_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.ColumnIndex == 1 || e.ColumnIndex == 2)
            {
                gridData.Rows[e.RowIndex].ErrorText = "";
                Int32 newInteger;
                if (gridData.Rows[e.RowIndex].IsNewRow) { return; }
                if (!Int32.TryParse(e.FormattedValue.ToString(),
                    out newInteger) || newInteger < 0)
                {
                    e.Cancel = true;
                    gridData.Rows[e.RowIndex].ErrorText = "The value must be a non-negative integer";
                    return;
                }

                Int32 min = 1900;
                Int32 max = 9999;
                if (e.ColumnIndex == 2)
                {
                    min = 1;
                    max = 12;
                }
                if (newInteger < min || newInteger > max)
                {
                    e.Cancel = true;
                    gridData.Rows[e.RowIndex].ErrorText = "The value must be in range {" + min + " - " + max + "}";
                    return;
                }
            }
            if (e.ColumnIndex == 3)
            {
                gridData.Rows[e.RowIndex].ErrorText = "";
                decimal newInteger;
                if (gridData.Rows[e.RowIndex].IsNewRow) { return; }
                if (!Decimal.TryParse(e.FormattedValue.ToString(), NumberStyles.Any, new CultureInfo(objGlobal.CultureInfo),
                out newInteger) || newInteger < 0)
                {
                    e.Cancel = true;
                    gridData.Rows[e.RowIndex].ErrorText = "The value must be a non-negative decimal";
                    return;
                }
            }
        }

        private void gridData_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {

        }

        public clsGlobalVariable objGlobal = new clsGlobalVariable();

        private SCIndexData convertToIndexData(DataGridViewRow row)
        {
            SCIndexData obj = new SCIndexData();
            obj.OID = (int)row.Cells[0].Value;
            if (row.Cells[1].Value != null)
            {
                int year = 0;
                if (Int32.TryParse(row.Cells[1].Value.ToString(), out year))
                    obj.IndexYear = year;
                else
                    obj.IndexYear = 0;
            }
            if (row.Cells[2].Value != null)
            {
                int month = 0;
                if (Int32.TryParse(row.Cells[2].Value.ToString(), out month))
                    obj.IndexMonth = month;
                else
                    obj.IndexMonth = 0;
            }
            if (row.Cells[3].Value != null)
            {
                decimal value = 0;
                if (Decimal.TryParse(row.Cells[3].Value.ToString(), NumberStyles.Any, new CultureInfo(objGlobal.CultureInfo), out value))
                    obj.IndexValue = value;
                else
                    obj.IndexValue = 0;
            }
            return obj;
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (Form.ModifierKeys == Keys.None && keyData == Keys.Escape)
            {
                this.Close();
                return true;
            }
            return base.ProcessDialogKey(keyData);
        }

        private void gridData_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void gridData_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            //Validated
            SCIndexData sCIndexData = convertToIndexData(gridData.Rows[e.RowIndex]);
            if (sCIndexData.OID > 0)
            {
                if (sCIndexData.isDelete)
                    listDataChange.Add(sCIndexData);
                else
                {
                    SCIndexData finder = listData.Find(x => x.OID == sCIndexData.OID);
                    //Kiem tra thay doi so voi db
                    if (finder != null && (finder.IndexYear != sCIndexData.IndexYear
                        || finder.IndexMonth != sCIndexData.IndexMonth
                        || finder.IndexValue != sCIndexData.IndexValue))
                    {
                        sCIndexData.isUpdate = true;

                        //Duyet list change de them hoac update data
                        SCIndexData finderChange = listDataChange.Find(x => x.OID == sCIndexData.OID);
                        if (finderChange == null)
                            listDataChange.Add(sCIndexData);
                        else
                        {
                            //Update
                            finderChange.IndexYear = sCIndexData.IndexYear;
                            finderChange.IndexMonth = sCIndexData.IndexMonth;
                            finderChange.IndexValue = sCIndexData.IndexValue;
                        }

                    }
                }
            }
            else
            {
                sCIndexData.isInsert = true;
                SCIndexData finder = listDataChange.Find(x => x.OID == sCIndexData.OID);
                if (finder == null)
                    listDataChange.Add(sCIndexData);
                else
                {
                    finder.IndexYear = sCIndexData.IndexYear;
                    finder.IndexMonth = sCIndexData.IndexMonth;
                    finder.IndexValue = sCIndexData.IndexValue;
                }
            }
        }
    }
}
