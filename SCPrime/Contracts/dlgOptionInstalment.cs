using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using nsBaseClass;
using SCPrime.Model;

namespace SCPrime.Contracts
{
    public partial class dlgOptionInstalment : clsBaseDialog
    {
        public List<ContractOptionInstalment> listInstalment;
        
        public dlgOptionInstalment()
        {
            InitializeComponent();
        }

        private void dlgOptionInstalment_Load(object sender, EventArgs e)
        {

        }
    }
}
