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
using SCPrime.Utils;
using log4net;

namespace SCPrime.Contracts
{
    public partial class InvoicesFrm : UserControl
    {
        protected static readonly ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

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

        private void gridInvoice_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex > -1)
            {
                string invoiceId = gridInvoice.Rows[e.RowIndex].Cells[0].Value.ToString();
                loadInvoiceDetail(Int32.Parse(invoiceId));
            }
            else
            {
                //Clear invoice detail
                loadInvoiceDetail(-1);
            }
            
        }

        private void cbCredit_CheckedChanged(object sender, EventArgs e)
        {
            searchContractInvoice();
        }

        private void cbNormal_CheckedChanged(object sender, EventArgs e)
        {
            searchContractInvoice();
        }

        private void cbKm_CheckedChanged(object sender, EventArgs e)
        {
            searchContractInvoice();
        }

        private void searchContractInvoice()
        {
            List<Int32> lstInvoiceType = new List<Int32>();
            if (cbCredit.Checked)
                lstInvoiceType.Add(0);
            if (cbKm.Checked)
                lstInvoiceType.Add(1);
            List<SCInvoice> lstContractInvoice = SCInvoiceUtil.getContractInvoice( ContractFrm.objContract.ContractOID, lstInvoiceType, cbCredit.Checked);

            fillDataGrid(lstContractInvoice);
        }

        public void fillDataGrid(List<SCInvoice> lstContractInvoice)
        {
            DataTable dataTable = ObjectUtils.ConvertToDataTable(lstContractInvoice);
            gridInvoice.DataSource = dataTable;

            //Clear invoice detail
            loadInvoiceDetail(-1);
        }

        private void loadInvoiceDetail(int invoiceID)
        {
            List<SCInvoiceItem> lstInvoiceDetail = SCInvoiceUtil.getInvoiceDetail(invoiceID);
            DataTable dataTable = ObjectUtils.ConvertToDataTable(lstInvoiceDetail);
            gridLines.DataSource = dataTable;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            bool insert = new SCInvoiceUtil().invoiceContract(ContractFrm.objContract,false,false);
            _log.Info("-----------------invoiceContract " + ContractFrm.objContract.ContractOID + ": " + insert);
        }
    }
}
