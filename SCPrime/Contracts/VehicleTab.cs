using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SCPrime.Model;
using nsBaseClass;
using SCPrime.Utils;

namespace SCPrime.Contracts
{
    public partial class VehicleTab : UserControl
    {
        private static VehicleTab _instance;

        static bool addCol = true;

        public static VehicleTab getInstance()
        {
            if (VehicleTab._instance == null || VehicleTab._instance.IsDisposed)
            {
                VehicleTab._instance = new VehicleTab();
            }
            return VehicleTab._instance;
        }

        private Contract contract;
        ContractVehicle contractVehicle;

        public void setContract(Contract objContract)
        {
            this.contract = objContract;
            contractVehicle = objContract.VehiId;
        }


        public VehicleTab()
        {
            InitializeComponent();
            initSender();
        }
        private void initSender()
        {
            Sender = new SendVehicle(GetMessage);
        }
        //Add
        public delegate void SendVehicle(ContractVehicle vehicle);
        public SendVehicle Sender;

        private void GetMessage(ContractVehicle Message)
        {
            System.Diagnostics.Debug.WriteLine("---------------------GetMessage: " + Message.VIN);
            this.contractVehicle = Message;
            this.contract.VehiId = this.contractVehicle;
            fillDataVehicle();
        }

        private void btnSearchVehicle_Click(object sender, EventArgs e)
        {
            SCSearchVehiFrm.instance.Show();
            SCSearchVehiFrm.instance.setUserControl(this);
        }

        public void fillDataVehicle()
        {
            clearData();
            if (contractVehicle != null && contractVehicle.VehiId > 0)
            {
                loadVehicleData();
            }

        }

        private DataTable LoadGridMileage(ContractVehicle contractVehicle)
        {
            DataTable dt = new DataTable();
            List<VehicleMileage> Mileages = contractVehicle.Mileages;

            System.Diagnostics.Debug.WriteLine("---------------------LoadGridMileage: " + Mileages.Count + " - " + contractVehicle.VehiId);

            if(Mileages.Count > 0)
            {
                System.Diagnostics.Debug.WriteLine("---------------------Mileages data: " + Mileages[0].MileageDate + " - " + Mileages[0].Mileage + " - " + Mileages[0].InputType);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("---------------------Mileages data emplty ");
            }
            

            dt = ObjectUtils.ConvertToDataTable(Mileages);

            System.Diagnostics.Debug.WriteLine("---------------------LoadGridMileage DataTable: " + dt.Rows.Count);
            return dt;
        }


        private void clearData()
        {
            gridMileage.DataSource = null;

            //
            txtVin.Text = "";
            txtMake.Text = "";

            txtMonAvg.Text = "";
            txtMonDev.Text = "";
            txtQuarterAvg.Text = "";
            txtQuarterDev.Text = "";
            txtYearAvg.Text = "";

            //
            cbAn.Checked = false;
            cbAu.Checked = false;
            cbTai.Checked = false;
            cbCool.Checked = false;

            txtMake1.Text = "";
            txtTyp1.Text = "";
            txtEmail1.Text = "";
            txtSr1.Text = "";

            txtMake2.Text = "";
            txtTyp2.Text = "";
            txtEmail2.Text = "";
            txtSr2.Text = "";

            txtMake3.Text = "";
            txtTyp3.Text = "";
            txtEmail3.Text = "";
            txtSr3.Text = "";

            txtMake4.Text = "";
            txtTyp4.Text = "";
            txtEmail4.Text = "";
            txtSr4.Text = "";
        }
        public void loadVehicleData()
        {
            clearData();

            //Load Data from DB
            clsSqlFactory hSql = new clsSqlFactory();
            contractVehicle.loadDynFields(hSql);
            contractVehicle.loadMileages(hSql);

            //Set Text
            txtVin.Text = contractVehicle.VIN;
            txtMake.Text = contractVehicle.Make;

            //Set Dynamic
            fillDynField();

            //Load data grid
            gridMileage.DataSource = LoadGridMileage(contractVehicle);

            if (addCol)
            {
                addColumn();
                addCol = false;
            }

            //Load Deviation
            int diffMileage = 0;
            int diffDate = 0;
            VehicleMileage Mileage1 = null;
            VehicleMileage Mileage2 = null;
            List<VehicleMileage> Mileages = contractVehicle.Mileages;
            if(Mileages != null && Mileages.Count > 1)
            {
                Mileage1 = Mileages[0];
                Mileage2 = Mileages[1];
                System.Diagnostics.Debug.WriteLine("---------------------Mileage1: " + Mileage1.MileageDate);
                System.Diagnostics.Debug.WriteLine("---------------------Mileage2: " + Mileage2.MileageDate);

                //T0 is the first, Tn is the last update to date mileage time stamp. The time stamps are read from V_ZSC_MileageReg.Created
                TimeSpan difference = (DateTime.Compare(Mileage1.MileageDate, Mileage2.MileageDate) > 0) ? Mileage1.MileageDate - Mileage2.MileageDate : Mileage2.MileageDate - Mileage1.MileageDate;
                diffDate = difference.Minutes;

                //Mn are the mileages (at above time stamp), from V_ZSC_MileageReg.Mileage
                diffMileage = Math.Abs(Mileage1.Mileage - Mileage2.Mileage);


                System.Diagnostics.Debug.WriteLine("---------------------diffDate: " + diffDate);
                System.Diagnostics.Debug.WriteLine("---------------------diffMileage: " + diffMileage);
            }
            if(diffMileage > 0)
            {
                float periodY = contract.ContractPeriodKm;
                float periodM = contract.ContractPeriodKm / 12;
                float avgDay = diffDate / diffMileage;
                float avgMonth = avgDay * 30;
                float devMonth = 100 * Math.Abs((avgMonth - periodM)) / periodM;

                System.Diagnostics.Debug.WriteLine("---------------------avgMonth: " + avgMonth.ToString());

                //Set data
                txtMonAvg.Text = avgMonth.ToString();
                txtMonDev.Text = devMonth.ToString();
                txtQuarterAvg.Text = "";
                txtQuarterDev.Text = "";
                txtYearAvg.Text = "";
            }
            
        }

        private void addColumn()
        {
            DataGridViewTextBoxColumn colStatus = new DataGridViewTextBoxColumn();
            colStatus.Name = "CustomInputType";
            colStatus.HeaderText = "Type";
            colStatus.DataPropertyName = "CustomInputType";
            gridMileage.Columns.Insert(2, colStatus);
        }

        private void fillDynField()
        {
            cbAn.Checked = contract.IsBodyIncl;
            cbAu.Checked = contract.IsCraneIncl;
            cbTai.Checked = contract.IsTailLiftIncl;
            cbCool.Checked = contract.IsCoolingIncl;

            clsBaseListItem tmp = null;
            List<clsBaseListItem> dynFields1 = contractVehicle.dynFields1;
            int count = dynFields1.Count;
            if(count > 0)
            {
                tmp = dynFields1[0];
                txtMake1.Text = tmp.strValue1;
            }
            if (count > 1)
            {
                tmp = dynFields1[1];
                txtTyp1.Text = tmp.strValue1;
            }
            if (count > 2)
            {
                tmp = dynFields1[2];
                txtEmail1.Text = tmp.strValue1;
            }
            if (count > 3)
            {
                tmp = dynFields1[3];
                txtSr1.Text = tmp.strValue1;
            }

            List<clsBaseListItem> dynFields2 = contractVehicle.dynFields2;
            count = dynFields2.Count;
            if (count > 0)
            {
                tmp = dynFields2[0];
                txtMake2.Text = tmp.strValue1;
            }
            if (count > 1)
            {
                tmp = dynFields2[1];
                txtTyp2.Text = tmp.strValue1;
            }
            if (count > 2)
            {
                tmp = dynFields2[2];
                txtEmail2.Text = tmp.strValue1;
            }
            if (count > 3)
            {
                tmp = dynFields2[3];
                txtSr2.Text = tmp.strValue1;
            }
            List<clsBaseListItem> dynFields3 = contractVehicle.dynFields3;
            count = dynFields3.Count;
            if (count > 0)
            {
                tmp = dynFields3[0];
                txtMake3.Text = tmp.strValue1;
            }
            if (count > 1)
            {
                tmp = dynFields3[1];
                txtTyp3.Text = tmp.strValue1;
            }
            if (count > 2)
            {
                tmp = dynFields3[2];
                txtEmail3.Text = tmp.strValue1;
            }
            if (count > 3)
            {
                tmp = dynFields3[3];
                txtSr3.Text = tmp.strValue1;
            }
            List<clsBaseListItem> dynFields4 = contractVehicle.dynFields4;
            count = dynFields4.Count;
            if (count > 0)
            {
                tmp = dynFields4[0];
                txtMake4.Text = tmp.strValue1;
            }
            if (count > 1)
            {
                tmp = dynFields4[1];
                txtTyp4.Text = tmp.strValue1;
            }
            if (count > 2)
            {
                tmp = dynFields4[2];
                txtEmail4.Text = tmp.strValue1;
            }
            if (count > 3)
            {
                tmp = dynFields4[3];
                txtSr4.Text = tmp.strValue1;
            }
        }
    }
}
