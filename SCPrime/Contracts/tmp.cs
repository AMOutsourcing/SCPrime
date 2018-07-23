using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CashRegPrime;
using CashRegPrime.Model;
using log4net;
using SCPrime.Model;

namespace SCPrime.Contracts
{
    public partial class tmp : Form
    {
        static readonly ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        static Random rnd = new Random();
        public tmp()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Contract objContact = new Contract();
            List<int> custIds = new List<int>();
            custIds.Add(100170);
            custIds.Add(100255);
            custIds.Add(100610);
            custIds.Add(100936);
            custIds.Add(100979);
            custIds.Add(101552);
            custIds.Add(102131);
            custIds.Add(102178);
            custIds.Add(102750);
            custIds.Add(103421);

            List<int> VehiIds = new List<int>();
            VehiIds.Add(308);
            VehiIds.Add(328);
            VehiIds.Add(306);
            VehiIds.Add(307);
            VehiIds.Add(311);
            VehiIds.Add(317);
            VehiIds.Add(274);
            VehiIds.Add(291);
            VehiIds.Add(301);
            VehiIds.Add(210);

            List<string> tmp = new List<string>();
            tmp.Add("M");
            tmp.Add("O");
            tmp.Add("N");
            tmp.Add("W");
            tmp.Add("A");
            tmp.Add("H");
            tmp.Add("C");
            tmp.Add("D");

            Random randNum = new Random();
            int aRandomPos = randNum.Next(tmp.Count);//Returns a nonnegative random number less than the specified maximum (firstNames.Count).

            string currName = tmp[aRandomPos];

            objContact.ContractCustId.CustId = custIds[rnd.Next(custIds.Count)]; //take a CUSTID from CUST.CUSTID
            objContact.VehiId.VehiId = VehiIds[rnd.Next(VehiIds.Count)]; //take a VEHIID from VEHI.VEHIID
            SCBase sc = new SCBase();
            objContact.ContractTypeOID = sc.getContractTypeActive()[0]; //randomly choose a contract type
            objContact.ContractStatus = currName;
            objContact.saveContract();
            MessageBox.Show("Insert OK");

        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<string> allsites = new List<string>();
            allsites.Add("S01");
            List<string> allstatus = new List<string>();
            allstatus.Add("M");
            allstatus.Add("O");
            allstatus.Add("N");
            allstatus.Add("W");
            allstatus.Add("A");
            allstatus.Add("H");
            allstatus.Add("C");
            allstatus.Add("D");
            SCBase sc = new SCBase();
            List<Contract> cts = SCBase.searchContracts(sc.getContractTypeActive(), allsites, allstatus, "");
            if(cts != null && cts.Count > 0)
            {
                MessageBox.Show(cts.Count.ToString());

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dlgSearchCustomer searhCustomer = new dlgSearchCustomer();
            searhCustomer.Owner = this;
            searhCustomer.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }
    }
}
