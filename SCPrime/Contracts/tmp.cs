using System;
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

namespace SCPrime.Contracts
{
    public partial class tmp : Form
    {
        static readonly ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public tmp()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Contract objContact = new Contract();
            objContact.ContractCustId.CustId = 100979; //take a CUSTID from CUST.CUSTID
            objContact.VehiId.VehiId = 311; //take a VEHIID from VEHI.VEHIID
            SCBase sc = new SCBase();
            objContact.ContractTypeOID = sc.getContractTypeActive()[0]; //randomly choose a contract type
            objContact.ContractStatus = "O";
            objContact.saveContract();
            MessageBox.Show("Insert OK");

        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<string> allsites = new List<string>();
            allsites.Add("S01");
            List<string> allstatus = new List<string>();
            allstatus.Add("O");
            SCBase sc = new SCBase();
            List<Contract> cts = SCBase.searchContracts(sc.getContractTypeActive(), allsites, allstatus, "");
            if(cts != null && cts.Count > 0)
            {
                MessageBox.Show(cts.Count.ToString());

            }
        }
    }
}
