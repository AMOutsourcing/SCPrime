using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SCPrime.Contracts
{
    public partial class VehicleDataTabPage : UserControl
    {
        private static VehicleDataTabPage _instance;

        public static VehicleDataTabPage getInstance()
        {
            if (VehicleDataTabPage._instance == null || VehicleDataTabPage._instance.IsDisposed)
            {
                VehicleDataTabPage._instance = new VehicleDataTabPage();
            }
            return VehicleDataTabPage._instance;
        }

        public VehicleDataTabPage()
        {
            InitializeComponent();

            initSender();
        }

        private void initSender()
        {
            Sender = new SendVehicle(GetMessage);
        }


        //Add
        public delegate void SendVehicle(string Message);
        public SendVehicle Sender;

        private void GetMessage(string Message)
        {
            System.Diagnostics.Debug.WriteLine("---------------------GetMessage: " + Message);
            txtVin.Text = Message;
        }

        private void btnSearchVehicle_Click(object sender, EventArgs e)
        {
            SCSearchVehiFrm.instance.Show();
            SCSearchVehiFrm.instance.setUserControl(this);
        }
    }
}
