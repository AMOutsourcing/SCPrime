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
            // remove context menu
            this.ContextMenuStrip.Items.Clear();
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
            this.gridVehicle.DataSource = null;
            this.gridVehicle.DataSource = this.LoadVehicle(this.txtSearch.Text.Trim());
        }
        private DataTable LoadVehicle(string key)
        {
            DataTable dt = new DataTable();
            List<ContractVehicle> result = new List<ContractVehicle>();
            result = ContractVehicle.seach(key);
            System.Diagnostics.Debug.WriteLine("---------------------ContractVehicle seach: " + result.Count);
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
            try
            {
                this.gridVehicle.DataSource = null;
                this.gridVehicle.DataSource = this.LoadVehicle(this.txtSearch.Text.Trim());
                //userControl.Sender(this.txtSearch.Text.Trim());
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
                        this.gridVehicle.DataSource = null;
                        this.gridVehicle.DataSource = this.LoadVehicle(this.txtSearch.Text.Trim());
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    this.gridVehicle.DataSource = null;
                }
            }
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.gridVehicle.Rows[e.RowIndex];
                userControl.Sender(getData(row));
                this.Close();
            }
        }
        private ContractVehicle getData(DataGridViewRow row)
        {
            ContractVehicle contractVehicle = new ContractVehicle();
            contractVehicle.VehiId = Int32.Parse(row.Cells[0].Value.ToString());
            contractVehicle.LicenseNo = row.Cells[1].Value.ToString();
            contractVehicle.VIN = row.Cells[2].Value.ToString();
            contractVehicle.Make = row.Cells[3].Value.ToString();
            contractVehicle.Model = row.Cells[4].Value.ToString();
            contractVehicle.SubModel = row.Cells[5].Value.ToString();
            return contractVehicle;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.gridVehicle.Rows[e.RowIndex];
                userControl.Sender(getData(row));
                this.Close();
            }
        }

        private void SCSearchVehiFrm_FormClosed(object sender, FormClosedEventArgs e)
        {
            SCSearchVehiFrm._instance = null;
        }

        private VehicleTab userControl;
        public void setUserControl(VehicleTab userControl)
        {
            this.userControl = userControl;
        }
    }
}
