using log4net;
using SCPrime.Contracts;
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
    public partial class SCSearchVehiFrm : nsBaseClass.clsBaseForm
    {
        static readonly ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static SCSearchVehiFrm _instance;
        private int objectMode = -1;

        public delegate void SendKey(int ObjectMode);
        public SendKey KeySender;

        private void GetKey(int ObjectMode)
        {
            log.Debug(ObjectMode);
            this.objectMode = ObjectMode;
        }

        public SCSearchVehiFrm()
        {
            InitializeComponent();
            this.Visible = false;
            KeySender = new SendKey(GetKey);

        }

        public static SCSearchVehiFrm instance
        {
            get
            {
                if (SCSearchVehiFrm._instance == null || SCSearchVehiFrm._instance.IsDisposed)
                {
                    SCSearchVehiFrm._instance = new SCSearchVehiFrm();
                }
                return SCSearchVehiFrm._instance;
            }
        }

        private void SCSearchVehiFrm_Load(object sender, EventArgs e)
        {
            this.txtSearch.Text = "";
            this.Visible = true;
          
        }
        private DataTable LoadVehicle(string key)
        {
            DataTable dt = new DataTable();
            List<SCViewWorks> result = new List<SCViewWorks>();
            result = SCViewWorks.seach(key);
            dt = ObjectUtils.ConvertToDataTable(result);
            return dt;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (SCSearchVehiFrm._instance != null)
            {
                SCSearchVehiFrm._instance.Close();
            }

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (this.txtSearch.Text.Trim().Equals(""))
            {
                this.dataGridView1.DataSource = null;
                return;
            }
            try
            {
                this.dataGridView1.DataSource = null;
                this.dataGridView1.DataSource = this.LoadVehicle(this.txtSearch.Text.Trim());

                //Test
                System.Diagnostics.Debug.WriteLine("---------------------userControl Test: " + this.txtSearch.Text.Trim());
                userControl.Sender(this.txtSearch.Text.Trim());
            }
            catch (Exception ex)
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
                        this.dataGridView1.DataSource = this.LoadVehicle(this.txtSearch.Text.Trim());
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    this.dataGridView1.DataSource = null;
                }
            }
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string tmp = this.objectMode +":" + this.getWrksId();
            userControl.Sender(tmp);
            this.Close();
        }
        private string getWrksId()
        {
            string result = "";
            if (this.dataGridView1.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow r in this.dataGridView1.SelectedRows)
                {
                    result = r.Cells["LabourCodeColumn"].Value.ToString();
                }
            }

            return result;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string tmp = this.objectMode + ":" + this.getWrksId();
            userControl.Sender(tmp);
            this.Close();
        }

        private void SCSearchVehiFrm_FormClosed(object sender, FormClosedEventArgs e)
        {
            SCSearchVehiFrm._instance = null;
        }

        private VehicleDataTabPage userControl;
        public void setUserControl(VehicleDataTabPage userControl)
        {
            this.userControl = userControl;
        }
    }
}
