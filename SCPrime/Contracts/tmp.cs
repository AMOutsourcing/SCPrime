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
            objContact.ContractCustId.CustId = 1017121; //take a CUSTID from CUST.CUSTID
            objContact.VehiId.VehiId = 98007993; //take a VEHIID from VEHI.VEHIID
            SCBase sc = new SCBase();
            objContact.ContractTypeOID = sc.getContractTypeActive()[5]; //randomly choose a contract type

            objContact.saveContract();
           
        }
    }
}
