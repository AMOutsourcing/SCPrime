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
        private int SelectedRow = -1;
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
            SelectedRow = e.RowIndex;
            if (e.RowIndex > -1)
            {
                string invoiceId = gridInvoice.Rows[e.RowIndex].Cells[0].Value.ToString();
                loadInvoiceDetail(Int32.Parse(invoiceId));
            }
            else
            {
                //Clear invoice detail
                loadInvoiceDetail(-1);
            }
            disenableButton();
        }

        public void setCheckDefault()
        {
            cbCredit.Checked = true;
            cbNormal.Checked = true;
            cbKm.Checked = true;
        }

        private void disenableButton()
        {

            if (SelectedRow >= 0)
            {
                pbOpenTrans.Enabled = true;
                int SRECNO = Int32.Parse(gridInvoice.Rows[SelectedRow].Cells["SRECNO"].Value.ToString());
                if (SRECNO > 0)
                {
                    pbCredit.Enabled = true;
                    pbPDF.Enabled = true;
                }
                else
                {
                    pbCredit.Enabled = false;
                    pbPDF.Enabled = false;
                }
            }
            else
            {
                pbCredit.Enabled = false;
                pbOpenTrans.Enabled = false;
                pbPDF.Enabled = false;
            }
            if (ContractFrm.objContract != null && ContractFrm.objContract.ContractTypeOID != null
                && ContractFrm.objContract.ContractTypeOID.isInvoice == false)
            {
                pbCredit.Enabled = false;
                pbNewDraft.Enabled = false;
                pbNewInvoice.Enabled = false;
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
            SelectedRow = -1;
            disenableButton();

            if (cbNormal.Checked)
                lstInvoiceType.Add(0);
            if (cbKm.Checked)
                lstInvoiceType.Add(1);

            List<SCInvoice> lstContractInvoice = SCInvoiceUtil.getContractInvoice(ContractFrm.objContract.ContractOID, lstInvoiceType, cbCredit.Checked);

            fillDataGrid(lstContractInvoice);
        }

        public void fillDataGrid(List<SCInvoice> lstContractInvoice)
        {
            DataTable dataTable = ObjectUtils.ConvertToDataTable(lstContractInvoice);
            gridInvoice.DataSource = dataTable;

            //ThuyetLV: Load detail cho Invoice dau tien
            if (lstContractInvoice.Count > 0)
            {
                SCInvoice item = lstContractInvoice[0];
                loadInvoiceDetail(item.OID);

            }
            else
            {
                //Clear invoice detail
                loadInvoiceDetail(-1);
            }
        }

        private void loadInvoiceDetail(int invoiceID)
        {
            List<SCInvoiceItem> lstInvoiceDetail = SCInvoiceUtil.getInvoiceDetail(invoiceID);
            DataTable dataTable = ObjectUtils.ConvertToDataTable(lstInvoiceDetail);
            gridLines.DataSource = dataTable;
            //    gridLines.Refresh();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //new invoice
            SCInvoiceUtil objInv = new SCInvoiceUtil();
            objInv.invoiceContract(ContractFrm.objContract, false, true);
            ContractFrm.objContract = SCBase.searchContracts(ContractFrm.objContract.ContractOID);
            searchContractInvoice();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //new invoice draft
            SCInvoiceUtil objInv = new SCInvoiceUtil();
            objInv.invoiceContract(ContractFrm.objContract, true, true);
            ContractFrm.objContract = SCBase.searchContracts(ContractFrm.objContract.ContractOID);
            searchContractInvoice();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //open invoice

            if (SelectedRow >= 0)
            {
                String UnitId = gridInvoice.Rows[SelectedRow].Cells["UnitId"].Value.ToString();
                int SSALID = Int32.Parse(gridInvoice.Rows[SelectedRow].Cells["SSALID"].Value.ToString());
                int SRECNO = Int32.Parse(gridInvoice.Rows[SelectedRow].Cells["SRECNO"].Value.ToString());
                SCInvoiceUtil objInv = new SCInvoiceUtil();
                objInv.openInvoice(UnitId, SSALID, SRECNO);

            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (SelectedRow >= 0)
            {
                String UnitId = gridInvoice.Rows[SelectedRow].Cells["UnitId"].Value.ToString();
                int SRECNO = Int32.Parse(gridInvoice.Rows[SelectedRow].Cells["SRECNO"].Value.ToString());
                SCInvoiceUtil objInv = new SCInvoiceUtil();
                objInv.openInvoicePDF(UnitId, SRECNO);

            }
        }
    }
}
