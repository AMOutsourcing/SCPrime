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

namespace SCPrime.Contracts
{
    public partial class DetailContractSearchFrm : Form
    {
        private int collectiveOid = -1;
        public CollectiveContract myCollectiveContract;
        public List<CollectiveContract> ls = new List<CollectiveContract>();

        public DetailContractSearchFrm()
        {
            InitializeComponent();
            this.Visible = false;
        }
        public void loadData()
        {

            ls = CollectiveContract.searchSelfContract(ContractFrm.objContract.ContractCustId.CustId, ContractFrm.objContract.ContractOID);
            if (ls != null && ls.Count > 0)
            {
                DataTable dt = ObjectUtils.ConvertToDataTable(ls);
                if (dt != null)
                {
                    this.dataGridView1.DataSource = dt;
                }
            }
            //bool tmp = checkColumnSelect();
            //if (!tmp)
            //{
            //    addCheckColumn();
            //}


        }

        private void DetailContractSearchFrm_Load(object sender, EventArgs e)
        {
            this.loadData();
            this.Visible = true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //private bool checkColumnSelect()
        //{
        //    bool ret = false;
        //    foreach (DataGridViewColumn c in this.dataGridView1.Columns)
        //    {
        //        if (c.Name.Equals("colSelect"))
        //        {
        //            ret = true;
        //            return ret;
        //        }
        //    }
        //    return ret;
        //}
        //private void addCheckColumn()
        //{

        //    DataGridViewCheckBoxColumn doWork = new DataGridViewCheckBoxColumn();
        //    doWork.Name = "colSelect";
        //    doWork.HeaderText = "Select";
        //    doWork.FalseValue = false;
        //    doWork.TrueValue = true;
        //    dataGridView1.Columns.Insert(0, doWork);
        //}

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.ColumnIndex == 0 && e.RowIndex != -1)
            //{
            //    this.dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
            //}
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
           
        }
        //private void updateCheckboxColumn(int rowindex)
        //{
        //    foreach (DataGridViewRow row in this.dataGridView1.Rows)
        //    {
        //        if ((Boolean)((DataGridViewCheckBoxCell)row.Cells["colSelect"]).FormattedValue)
        //        {
        //            if (row.Index != rowindex)
        //            {
        //                row.Cells["colSelect"].Value = 0;
        //            }
        //        }

        //    }
        //}

        private void btnSelect_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow r = this.dataGridView1.Rows[e.RowIndex];

            this.collectiveOid = (int)r.Cells["colOid"].Value;
            this.myCollectiveContract = ls.Find(x => x.OID == (int)r.Cells["colOid"].Value);
            MessageBox.Show("Test: " + this.myCollectiveContract.OID);

            if (this.myCollectiveContract != null)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow r = this.dataGridView1.Rows[e.RowIndex];

            this.collectiveOid = (int)r.Cells["colDetailContractOID"].Value;
            this.myCollectiveContract = ls.Find(x => x.DetailContractOID == (int)r.Cells["colDetailContractOID"].Value);
            //MessageBox.Show("Test: " + this.myCollectiveContract.DetailContractOID);
            if (this.myCollectiveContract != null)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void DetailContractSearchFrm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
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
    }
}
