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
    public partial class SCSearchLabourCodeFrm : nsBaseClass.clsBaseForm
    {
        public SCSearchLabourCodeFrm()
        {
            InitializeComponent();
            this.Visible = false;
        }

        private void SCSparePartNoFrm_Load(object sender, EventArgs e)
        {
            this.txtSearch.Text = "";
           // this.dataGridView1.DataSource = this.LoadSCViewWorks("");
            this.Visible = true;
        }
        private DataTable LoadSCViewWorks(string key)
        {
            DataTable dt = new DataTable();
            List<SCViewWorks> result = new List<SCViewWorks>();
            result = SCViewWorks.seach(key);
            dt = ObjectUtils.ConvertToDataTable(result);
            return dt;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (this.txtSearch.Text.Trim().Equals(""))
                return;
            try
            {
                this.dataGridView1.DataSource = null;
                this.dataGridView1.DataSource = this.LoadSCViewWorks(this.txtSearch.Text.Trim());
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                if (!this.txtSearch.Text.Trim().Equals(""))
                {
                    try
                    {
                        this.dataGridView1.DataSource = null;
                        this.dataGridView1.DataSource = this.LoadSCViewWorks(this.txtSearch.Text.Trim());
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }
    }
}
