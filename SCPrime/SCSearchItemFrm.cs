﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using log4net;
using SCPrime.Model;
using SCPrime.Utils;

namespace SCPrime
{
    public partial class SCSearchItemFrm : nsBaseClass.clsBaseForm
    {
        static readonly ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private int objectMode = -1;
        public delegate void SendKey(int ObjectMode);
        public SendKey KeySender;

        private void GetKey(int ObjectMode)
        {
            log.Debug(ObjectMode);
            this.objectMode = ObjectMode;
        }
        public SCSearchItemFrm()
        {
            InitializeComponent();
            // remove context menu
            // this.ContextMenuStrip.Items.Clear();
            this.Visible = false;
            KeySender = new SendKey(GetKey);
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
            //  this.gridItem.DataSource = this.LoadSCViewItems("");
            this.Visible = true;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (this.txtSearch.Text.Trim().Equals(""))
            {
                this.gridItem.DataSource = null;
                return;
            }
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
                if (!this.txtSearch.Text.Trim().Equals(""))
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
                else
                {
                    this.gridItem.DataSource = null;
                    return;
                }
            }
        }

        private void gridItem_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string tmp = this.objectMode + ";" + getItemRetrun();
            SCOptionList.instance.Sender2(tmp);
            this.Close();
        }
        private string getItemRetrun()
        {
            string result = "";
            if (this.gridItem.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow r in this.gridItem.SelectedRows)
                {
                    result = r.Cells["PartNrColumn"].Value.ToString() + ";" + r.Cells["supplierColumn"].Value.ToString();
                }
            }

            return result;
        }

        private void gridItem_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string tmp = this.objectMode + ";" + getItemRetrun();
            SCOptionList.instance.Sender2(tmp);
            this.Close();
            SCOptionList.instance.Refresh();
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

        private void SCSearchItemFrm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
