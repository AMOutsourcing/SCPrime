using SCPrime.Model;
using SCPrime.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SCPrime.Contracts
{
    public partial class MileageRegisterFrm : nsBaseClass.clsBaseForm
    {
        public ContractVehicle contractVehicle;
        private static string separator = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator;
        private string filePath;
        private static string myCulture;


        public MileageRegisterFrm()
        {
            InitializeComponent();
            Sender = new SendVehicle(GetMessage);
            myCulture = objGlobal.CultureInfo;
        }

        private void MileageRegisterFrm_Load(object sender, EventArgs e)
        {
            if (ContractFrm.objContract != null)
            {
                contractVehicle = ContractFrm.objContract.VehiId;
                txtVin.Text = contractVehicle.VIN;
                txtMake.Text = contractVehicle.Make;
                this.dgvMileages.DataSource = this.loadMileage(contractVehicle);

            }

        }

        private DataTable loadMileage(ContractVehicle contractVehicle)
        {
            List<VehicleMileage> ret = new List<VehicleMileage>();
            DataTable dt = new DataTable();
            List<VehicleMileage> Mileages = contractVehicle.Mileages;
            //  MessageBox.Show(Mileages.Count.ToString());
            dt = ObjectUtils.ConvertToDataTable(Mileages);
            return dt;

        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (this.contractVehicle == null)
            {
                MessageBox.Show("Please select a Vehicle");
            }
            else
            {
                VehicleMileage vm = new VehicleMileage();
                int tmp = 0;
                bool bTmp = false;
                bTmp = int.TryParse(this.txtMileage.Text, out tmp);
                if (bTmp)
                    vm.Mileage = tmp;
                else
                    vm.Mileage = 0;

                vm.MileageDate = this.datePicker1.Value;
                vm.Info = "";
                bool bTmp2 = VehicleMileage.saveMileages(vm, contractVehicle.VehiId);
                if (bTmp2)
                {
                    contractVehicle.Mileages.Add(vm);
                    this.dgvMileages.DataSource = this.loadMileage(contractVehicle);
                }
                else
                    MessageBox.Show("Not OK");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Add
        public delegate void SendVehicle(ContractVehicle vehicle);
        public SendVehicle Sender;

        private void GetMessage(ContractVehicle Message)
        {
            System.Diagnostics.Debug.WriteLine("---------------------GetMessage: " + Message.VIN);
            this.contractVehicle = Message;

            this.txtVin.Text = this.contractVehicle.VIN;
            this.txtMake.Text = this.contractVehicle.Make;
            this.dgvMileages.DataSource = this.loadMileage(contractVehicle);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

            SCSearchVehiFrm.instance.setMileageControl(this);
            SCSearchVehiFrm.instance.ShowDialog();

        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = @"C:\";
            openFileDialog1.Title = "Browse Files";
            openFileDialog1.CheckFileExists = true;
            openFileDialog1.CheckPathExists = true;
            openFileDialog1.DefaultExt = "csv";
            openFileDialog1.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.ReadOnlyChecked = true;
            openFileDialog1.ShowReadOnly = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                filePath = openFileDialog1.FileName;
                this.readFile();
                MessageBox.Show("Import finished");
            }
        }
        private void readFile()
        {
            var csvRows = File.ReadAllLines(filePath);
            foreach (string r in csvRows)
            {
                if(r.Length > 0) {
                string[] fileds = r.Split(new string[] { separator }, StringSplitOptions.None);
                System.Console.WriteLine(fileds[0]);//vin
                System.Console.WriteLine(fileds[1]);//date
                System.Console.WriteLine(fileds[2]);//mileage
                List<ContractVehicle> cvList = new List<ContractVehicle>();
                cvList = ContractVehicle.getByVIN(fileds[0]);
                    if (cvList != null && cvList.Count > 0)
                    {
                        this.contractVehicle = cvList[0];
                        VehicleMileage vm = new VehicleMileage();
                        int tmp = 0;
                        bool bTmp = false;
                        bTmp = int.TryParse(fileds[2], out tmp);
                        if (bTmp)
                            vm.Mileage = tmp;
                        else
                            vm.Mileage = 0;
                        DateTime myDate = new DateTime();
                        bool bTmp2 = false;
                        bTmp2 = DateTime.TryParse(fileds[1],
                            System.Globalization.CultureInfo.GetCultureInfo(myCulture),
                            System.Globalization.DateTimeStyles.None, out myDate);
                        if (bTmp2)
                        {
                            vm.MileageDate = myDate;
                        }

                        vm.Info = "";

                        VehicleMileage.saveMileages(vm, contractVehicle.VehiId);
                    }
                }
            }
        }

        private void txtMileage_KeyPress(object sender, KeyPressEventArgs e)
        {
            int isNumber = 0;
            bool tmp = false;
            tmp = int.TryParse(e.KeyChar.ToString(), 
                System.Globalization.NumberStyles.Integer, 
                System.Globalization.CultureInfo.GetCultureInfo(myCulture), out isNumber);
            if(!tmp)
            {
                e.Handled = true;
            }
        }
    }
}
