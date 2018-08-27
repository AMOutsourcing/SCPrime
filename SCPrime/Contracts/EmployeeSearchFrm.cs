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
    public partial class EmployeeSearchFrm : Form
    {
        public delegate void UpdateFlag(int flag);
        public static UpdateFlag updateFlag;
        private int flag = -1;

        public EmployeeSearchFrm()
        {
            InitializeComponent();
            this.Visible = false;
            updateFlag = new UpdateFlag(setFlagValue);
        }

        private void setFlagValue(int flag)
        {
            this.flag = flag;
           // MessageBox.Show(this.flag.ToString());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //if (this.txtSearch.Text.Trim().Equals(""))
            //{
            //    this.dataGridView1.DataSource = null;
            //    return;
            //}
            try
            {
                this.dataGridView1.DataSource = null;
                this.dataGridView1.DataSource = this.searchEmployee(this.txtSearch.Text.Trim());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private DataTable searchEmployee(string key)
        {
            DataTable dt = new DataTable();
            List<SCViewEmployee> result = new List<SCViewEmployee>();
            result = SCViewEmployee.seach(key);
            dt = ObjectUtils.ConvertToDataTable(result);
            return dt;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void EmployeeSearchFrm_Load(object sender, EventArgs e)
        {
            this.txtSearch.Text = "";
            this.Visible = true;
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                if (this.txtSearch.Text.Trim().Equals(""))
                {
                    this.dataGridView1.DataSource = null;
                    return;
                }
                try
                {
                    this.dataGridView1.DataSource = null;
                    this.dataGridView1.DataSource = this.searchEmployee(this.txtSearch.Text.Trim());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.DataSource != null && e.RowIndex > -1)
            {
                DataTable dt = (DataTable)this.dataGridView1.DataSource;
                int oid = -1;
                oid = (int)this.dataGridView1.Rows[e.RowIndex].Cells[0].Value;
                DataRow[] rows = dt.Select("_OID = " + oid);
                if(rows != null && rows.Count() > 0)
                {
                    SCViewEmployee epl = this.datatableRowToEmployee(rows[0]);
                    ContractFrm.updateEmployee(epl, this.flag);
                    this.Close();
                }
            }
        }

        private SCViewEmployee datatableRowToEmployee(DataRow r)
        {
            SCViewEmployee epl = new SCViewEmployee();
            epl._OID = (int)r["_OID"];
            epl.SmanId = r["SmanId"].ToString();
            epl.Name = r["Name"].ToString();
            epl.Phone = r["Phone"].ToString();
            epl.Email = r["Email"].ToString();

            return epl;
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

        private void EmployeeSearchFrm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
