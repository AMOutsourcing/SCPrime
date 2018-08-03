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
    public partial class InvoicesFrm : UserControl
    {
        public InvoicesFrm()
        {
            InitializeComponent();
        }

        //Singleton
        private static InvoicesFrm _instance;

        public static InvoicesFrm getInstance()
        {
            if (InvoicesFrm._instance == null || InvoicesFrm._instance.IsDisposed)
            {
                InvoicesFrm._instance = new InvoicesFrm();
            }
            return InvoicesFrm._instance;
        }
    }
}
