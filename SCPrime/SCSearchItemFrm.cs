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
    public partial class SCSearchItemFrm : nsBaseClass.clsBaseForm
    {
        public SCSearchItemFrm()
        {
            InitializeComponent();
            this.Visible = false;
        }

        private DataTable LoadSCViewItems(string key)
        {
            DataTable dt = new DataTable();
            List<SCViewItems> result = new List<SCViewItems>();
            result = SCViewItems.seach(key);
            dt = ObjectUtils.ConvertToDataTable(result);
            return dt;
        }

        private void SCSearchItemFrm_Load(object sender, EventArgs e)
        {
            this.txtSearch.Text = "";
            this.gridItem.DataSource = this.LoadSCViewItems("");
            this.Visible = true;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                this.gridItem.DataSource = null;
                this.gridItem.DataSource = this.LoadSCViewItems(this.txtSearch.Text.Trim());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                try
                {
                    this.gridItem.DataSource = null;
                    this.gridItem.DataSource = this.LoadSCViewItems(this.txtSearch.Text.Trim());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
