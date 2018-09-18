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
        }

        private int newOid = -1;
        private void button1_Click(object sender, EventArgs e)
        {
            this.dataTable = null;
            this.dataTable = (DataTable)gridMark.DataSource;
            if (dataTable == null || dataTable.Rows.Count <= 0)
            {
                SCContractRemark sc = new SCContractRemark();
                this.dataTable = ObjectUtils.ConvertToDataTable(new List<SCContractRemark> { sc });
            }
            DataRow drToAdd = this.dataTable.NewRow();
            drToAdd[Constant.OID] = newOid;
            drToAdd["ContractOID"] = ContractFrm.objContract.ContractOID;
            drToAdd["UserId"] = -1;
            drToAdd["RemarkType"] = 0;
            drToAdd["Info"] = "";
            saveRow(drToAdd);
            newOid--;
        }

        private void saveRow(DataRow drToAdd)
        {
            SCContractRemark remark = ContractFrm.objContract.listSCContractRemark.Find(x => x.OID == (int)drToAdd[Constant.OID]);
            if (remark == null)
            {
                //Add moi
                remark = new SCContractRemark();
                remark.OID = (int)drToAdd[Constant.OID];
                if (drToAdd["UserId"] != null)
                    remark.UserId = (int)drToAdd["UserId"];
                if (drToAdd["RemarkType"] != null)
                    remark.RemarkType = (int)drToAdd["RemarkType"];
                if (drToAdd["Info"] != null)
                    remark.Info = (string)drToAdd["Info"];
                ContractFrm.objContract.listSCContractRemark.Add(remark);
            }
            else
            {
                //Update gia tri
                if (drToAdd["UserId"] != null)
                    remark.UserId = (int)drToAdd["UserId"];
                if (drToAdd["RemarkType"] != null)
                    remark.RemarkType = (int)drToAdd["RemarkType"];
                if (drToAdd["Info"] != null)
                    remark.Info = (string)drToAdd["Info"];
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int selectedRow = gridMark.SelectedRows.Count;
            if (selectedRow > 0)
            {
                for (int i = 0; i < selectedRow; i++)
                {

                    SCContractRemark remark = ContractFrm.objContract.listSCContractRemark.Find(x => x.OID == Int32.Parse(gridMark.SelectedRows[i].Cells[0].Value.ToString()));
                    if (remark != null)
                    {
                        if (remark.OID > 0)
                            remark.isMarkDeleted = true;
                        else
                            ContractFrm.objContract.listSCContractRemark.RemoveAll(x => x.OID == remark.OID);
                    }

                    dataTable.Rows.RemoveAt(gridMark.SelectedCells[i].RowIndex);
                    dataTable.AcceptChanges();
                }
            }
        }

        private void gridMark_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            //DataGridViewRow r = gridMark.Rows[e.RowIndex];
            //if (r != null)
            //{
            //    saveRow(r);
            //}
        }

        private void gridMark_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.ColumnIndex == 3 || e.ColumnIndex == 4)
            {
                gridMark.Rows[e.RowIndex].ErrorText = "";
                Int32 newInteger;
                if (gridMark.Rows[e.RowIndex].IsNewRow) { return; }
                if (!Int32.TryParse(e.FormattedValue.ToString(),
                    out newInteger) || newInteger < 0)
                {
                    e.Cancel = true;
                    gridMark.Rows[e.RowIndex].ErrorText = "The value must be a non-negative decimal";
                }
            }
        }

        private void gridMark_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0)
            {
                SCContractRemark value = new SCContractRemark();
                DataGridViewRow row = gridMark.Rows[e.RowIndex];
                if (row.Cells[0] != null && row.Cells[0].Value != null && row.Cells[0].Value.ToString() != "")
                    value.OID = (int)row.Cells[0].Value;
                else
                    value.OID = newOid--;
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
                saveRemask(value);
            }
        }

        private void saveRemask(SCContractRemark value)
        {
            if (value.OID > 0)
            {
                //Update
                SCContractRemark remark = ContractFrm.objContract.listSCContractRemark.Find(x => x.OID == value.OID);
                if (remark == null)
                {
                    //Add moi
                    ContractFrm.objContract.listSCContractRemark.Add(value);
                }
                else
                {
                    //Update gia tri
                    remark.UserId = value.UserId;
                    remark.RemarkType = value.RemarkType;
                    remark.Info = value.Info;
                }
            }
            else
            {
                ContractFrm.objContract.listSCContractRemark.Add(value);
            }
        }
    }
}
