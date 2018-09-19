using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using log4net;
using SCPrime.Model;
using SCPrime.Utils;

namespace SCPrime.Contracts
{
    public partial class RemarkFrm : UserControl
    {
        protected static readonly ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public RemarkFrm()
        {
            InitializeComponent();
        }


        //Singleton
        private static RemarkFrm _instance;

        public static RemarkFrm getInstance()
        {
            if (RemarkFrm._instance == null || RemarkFrm._instance.IsDisposed)
            {
                RemarkFrm._instance = new RemarkFrm();
            }
            return RemarkFrm._instance;
        }

        DataTable dataTable;
        public void loadRemark(List<SCContractRemark> listData)
        {
            dataTable = ObjectUtils.ConvertToDataTable(listData);
            gridMark.DataSource = dataTable;
            buildGrid();
        }

        private int newOid = -1;
        private void button1_Click(object sender, EventArgs e)
        {
            SCContractRemark sc = new SCContractRemark();
            sc.OID = newOid;
            sc.ContractOID = ContractFrm.objContract.ContractOID;
            sc.UserId = 1;
            sc.RemarkType = 0;
            sc.Info = "";

            this.dataTable = null;
            this.dataTable = (DataTable) gridMark.DataSource;
            if (dataTable == null || dataTable.Rows.Count <= 0)
            {
                this.dataTable = ObjectUtils.ConvertToDataTable(new List<SCContractRemark> { sc });
                gridMark.DataSource = dataTable;
            }
            else
            {
                DataRow drToAdd = this.dataTable.NewRow();
                drToAdd[Constant.OID] = sc.OID;
                drToAdd["ContractOID"] = sc.ContractOID;
                drToAdd["UserId"] = sc.UserId;
                drToAdd["RemarkType"] = sc.RemarkType;
                drToAdd["Info"] = sc.Info;
                dataTable.Rows.Add(drToAdd);
                dataTable.AcceptChanges();
            }
            
            buildGrid();
            gridMark.Refresh();
            
            newOid--;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int selectedRow = gridMark.SelectedRows.Count;
            if (selectedRow > 0)
            {
                for (int i = 0; i < selectedRow; i++)
                {
                    dataTable.Rows.RemoveAt(gridMark.SelectedCells[i].RowIndex);
                    dataTable.AcceptChanges();
                }
            }
        }

        private void gridMark_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                gridMark.Rows[e.RowIndex].ErrorText = "";
                Int32 newInteger;
                if (gridMark.Rows[e.RowIndex].IsNewRow) { return; }
                if (!Int32.TryParse(e.FormattedValue.ToString(),
                    out newInteger) || newInteger < 0)
                {
                    e.Cancel = true;
                    gridMark.Rows[e.RowIndex].ErrorText = "The value must be a non-negative integer";
                }
            }
        }

        public void buildGrid()
        {
            List<ObjTmp> listRemark = new List<ObjTmp>();
            listRemark.Add(new ObjTmp(0, "Manual"));
            listRemark.Add(new ObjTmp(1, "From mileage register"));

            // Bind the Department data to the appropriate column in the DataGridView
            ((DataGridViewComboBoxColumn)gridMark.Columns[4]).DataSource = listRemark;
            ((DataGridViewComboBoxColumn)gridMark.Columns[4]).DisplayMember = "strText";
            ((DataGridViewComboBoxColumn)gridMark.Columns[4]).ValueMember = "nValue1";
        }

        private void gridMark_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (gridMark.Rows[e.RowIndex].Cells[3].Value == null || gridMark.Rows[e.RowIndex].Cells[4].Value == null)
                e.Cancel = true;
        }

        public void saveContractRemark()
        {
            foreach (DataGridViewRow row in gridMark.Rows)
            {
                SCContractRemark sCContractRemark = convertToRemark(row);
                if (sCContractRemark.OID > 0)
                {
                    //Update
                    SCContractRemark remark = ContractFrm.objContract.listSCContractRemark.Find(x => x.OID == sCContractRemark.OID);
                    if (remark == null)
                    {
                        //Add moi
                        ContractFrm.objContract.listSCContractRemark.Add(sCContractRemark);
                    }
                    else
                    {
                        //Update gia tri
                        remark.UserId = sCContractRemark.UserId;
                        remark.RemarkType = sCContractRemark.RemarkType;
                        remark.Info = sCContractRemark.Info;
                    }
                }
                else
                {
                    ContractFrm.objContract.listSCContractRemark.Add(sCContractRemark);
                }
            }
        }

        private SCContractRemark convertToRemark(DataGridViewRow row)
        {
            SCContractRemark value = new SCContractRemark();
            value.OID = (int) row.Cells[0].Value;
            if (row.Cells[3].Value != null)
            {
                int UserId = 0;
                if (Int32.TryParse(row.Cells[3].Value.ToString(), out UserId))
                    value.UserId = UserId;
                else
                    value.UserId = 0;
            }
            if (row.Cells[4].Value != null)
            {
                int RemarkType = 0;
                if (Int32.TryParse(row.Cells[4].Value.ToString(), out RemarkType))
                    value.RemarkType = RemarkType;
                else
                    value.RemarkType = 0;
            }
            value.Info = row.Cells[5].Value != null ? row.Cells[5].Value.ToString() : "";
            return value;
    }
    }
}
