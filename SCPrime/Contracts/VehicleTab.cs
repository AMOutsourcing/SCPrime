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

        public static VehicleTab getInstance()
        {
            if (VehicleTab._instance == null || VehicleTab._instance.IsDisposed)
            {
                VehicleTab._instance = new VehicleTab();
            }
            return VehicleTab._instance;
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
            fillDataVehicle(Message);
        }

        private void btnSearchVehicle_Click(object sender, EventArgs e)
        {
            SCSearchVehiFrm.instance.Show();
            SCSearchVehiFrm.instance.setUserControl(this);
        }

        public void fillDataVehicle(ContractVehicle contractVehicle)
        {
            if (contractVehicle != null && contractVehicle.VehiId > 0)
            {
                txtVin.Text = contractVehicle.VIN;
                loadVehicleData(contractVehicle);
            }
            else
            {
                txtVin.Text = "";
                gridMileage.DataSource = null;
            }

        }

        private DataTable LoadGridMileage(ContractVehicle contractVehicle)
        {
            DataTable dt = new DataTable();
            List<VehicleMileage> Mileages = contractVehicle.Mileages;

            System.Diagnostics.Debug.WriteLine("---------------------LoadGridMileage: " + Mileages.Count + " - " + contractVehicle.VehiId);


            System.Diagnostics.Debug.WriteLine("---------------------Mileages data: " + Mileages[0].MileageDate + " - " + Mileages[0].Mileage + " - " + Mileages[0].InputType);

            dt = ObjectUtils.ConvertToDataTable(Mileages);

            System.Diagnostics.Debug.WriteLine("---------------------LoadGridMileage DataTable: " + dt.Rows.Count);
            return dt;
        }

        public void loadVehicleData(ContractVehicle contractVehicle)
        {
            Contract contract = new Contract();

            clsSqlFactory hSql = new clsSqlFactory();
            contractVehicle.loadDynFields(hSql);
            contractVehicle.loadMileages(hSql);

            //Load data grid
            gridMileage.DataSource = LoadGridMileage(contractVehicle);

            int diffMileage = 0;
            int diffDate = 0;
            VehicleMileage Mileage1 = null;
            VehicleMileage Mileage2 = null;
            List<VehicleMileage> Mileages = contractVehicle.Mileages;
            if(Mileages != null && Mileages.Count > 1)
            {
                Mileage1 = Mileages[0];
                Mileage2 = Mileages[1];

                //T0 is the first, Tn is the last update to date mileage time stamp. The time stamps are read from V_ZSC_MileageReg.Created
                TimeSpan difference = Mileage1.MileageDate - Mileage2.MileageDate;
                diffDate = difference.Minutes;

                //Mn are the mileages (at above time stamp), from V_ZSC_MileageReg.Mileage
                diffMileage = Mileage1.Mileage - Mileage2.Mileage;
            }
            if(diffMileage > 0)
            {
                float periodY = contract.ContractPeriodKm;
                float periodM = contract.ContractPeriodKm / 12;
                float avgDay = diffDate / diffMileage;
                float avgMonth = avgDay * 30;
                float devMonth = 100 * (avgMonth - periodM) / periodM;
            }
            
        }

    }
}
