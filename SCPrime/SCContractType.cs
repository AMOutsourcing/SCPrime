using SCPrime.Model;
using SCPrime.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SCPrime
{
    public partial class Form1 : nsBaseClass.clsBaseDialog
    {
        //static readonly ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        System.Globalization.CultureInfo oldCI = System.Threading.Thread.CurrentThread.CurrentCulture;
        List<SCContractType> sCContractTypes = null;
        List<SCContractType> datasource = null;
        DataTable dt = new DataTable();
        //int selectedID = -1;
        int newOid = -1;

        private static Form1 _instance;
        public Form1()
        {
            InitializeComponent();
            this.Visible = false;
            this.sCContractTypes = new List<SCContractType>();
            this.datasource = new List<SCContractType>(); ;
        }
        public static Form1 instance
        {
            get
            {
                if (Form1._instance == null || Form1._instance.IsDisposed)
                {
                    Form1._instance = new Form1();
                }
                return Form1._instance;
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form1._instance = null;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SCBase sb = new SCBase();
            datasource = sb.getContractTypes();
            this.dt = ObjectUtils.ConvertToDataTable(datasource);
            contractTypeList.DataSource = this.dt;
            this.Visible = true;

        }
        private void loaddata()
        {
            SCBase sb = new SCBase();
            datasource = sb.getContractTypes();
            this.dt = ObjectUtils.ConvertToDataTable(datasource);
            contractTypeList.DataSource = this.dt;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void delBtn_Click(object sender, EventArgs e)
        {
            if (contractTypeList.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow r in contractTypeList.SelectedRows)
                {
                    var myOid = r.Cells[0].Value;
                    string search = "OID = " + myOid.ToString();
                    DataRow[] rows = this.dt.Select(search);
                    foreach (DataRow row in rows)
                    {
                        SCContractType sc = null;
                        sc = RowToContractType(r);
                        sc.isMarkDeleted = true;

                        if (sc != null)
                        {
                            var item = sCContractTypes.Find(x => x.OID == sc.OID);
                            if (item == null && sc.OID > 0)
                            {
                                this.sCContractTypes.Add(sc);
                            }
                            else if (item == null && sc.OID < 0)
                            {
                                //this.sCContractTypes.Remove(item);
                            }
                            else if (item != null && sc.OID < 0)
                            {
                                this.sCContractTypes.Remove(item);
                            }
                            else if (item != null && sc.OID > 0)
                            {
                                this.sCContractTypes.Remove(item);
                                this.sCContractTypes.Add(sc);

                            }
                            //row.Delete();
                            if (row["isMarkDeleted"].ToString() == "True")
                            {
                                row["isMarkDeleted"] = 0;
                                this.sCContractTypes.Remove(sc);
                                ViewUtils.remarkHeader(r,Constant.isMarkDeleted);
                            }
                            else
                            {
                                row["isMarkDeleted"] = 1;
                                ViewUtils.remarkHeader(r, Constant.isMarkDeleted);
                            }
                            //MessageBox.Show(row["isMarkDeleted"].ToString());
                        }
                    }
                }
                contractTypeList.Refresh();
            }
        }

        private void newBtn_Click(object sender, EventArgs e)
        {
            // MessageBox.Show(datasource.Count.ToString());
            SCContractType sc = new SCContractType(newOid);


            sc.Name = "ContractType";
            sc.isInvoice = true;
            sc.isActive = true;
            sc.isCollective = true;
            sc.isMarkDeleted = false;


            DataTable dataTable = (DataTable)contractTypeList.DataSource;
            DataRow drToAdd = dataTable.NewRow();

            drToAdd["OID"] = newOid;
            //drToAdd["Name"] = "";
            drToAdd["isInvoice"] = false;
            drToAdd["isActive"] = false;
            drToAdd["isCollective"] = false;
            drToAdd["isMarkDeleted"] = false;

            dataTable.Rows.InsertAt(drToAdd, 0);
            dataTable.AcceptChanges();
            this.contractTypeList.Refresh();
            this.contractTypeList.Rows[0].Selected = true;

            DataGridViewCell cell = contractTypeList.Rows[0].Cells["ContractTypeName"];
            contractTypeList.CurrentCell = cell;
            this.contractTypeList.BeginEdit(true);

            newOid = newOid - 1;


            this.sCContractTypes.Add(sc);

        }


        private void closeBtn_Click(object sender, EventArgs e)
        {
            if (Form1._instance != null)
            {
                Form1._instance.Close();
            }
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            bool result = false;
            if (sCContractTypes.Count > 0)
            {
                SCBase sb = new SCBase();
                result = sb.saveContractTypes(sCContractTypes);
                this.loaddata();
                //MessageBox.Show(result.ToString());
            }
            if (result)
            {
                this.sCContractTypes = new List<SCContractType>();
            }
        }
        public SCContractType RowToContractType(DataGridViewRow row)
        {
            SCContractType sc = new SCContractType();
            int number = -1;
            bool tmp = Int32.TryParse(row.Cells[0].Value.ToString(), out number);

            if (tmp)
                sc.OID = number;
            else
                sc.OID = -1;

            sc.Name = row.Cells[1].Value.ToString();
            sc.isInvoice = (bool)row.Cells[2].Value;
            sc.isActive = (bool)row.Cells[3].Value;
            sc.isCollective = (bool)row.Cells[4].Value;
            sc.isMarkDeleted = (bool)row.Cells[5].Value;

            return sc;
        }

        //public DataGridViewRow ContractTypeToRow(SCContractType sc, DataGridViewRow row)
        //{
        //    //  DataGridViewRow row = new DataGridViewRow();


        //    row.Cells[0].Value = sc.OID.ToString();
        //    row.Cells[1].Value = sc.Name;
        //    row.Cells[2].Value = sc.isInvoice;
        //    row.Cells[3].Value = sc.isActive;
        //    row.Cells[4].Value = sc.isCollective;
        //    row.Cells[5].Value = sc.isMarkDeleted;

        //    return row;
        //}

        private void contractTypeList_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            int colIndex = e.ColumnIndex;
            // MessageBox.Show(colIndex.ToString());
            DataGridViewRow row = contractTypeList.Rows[rowIndex];
            SCContractType sc = new SCContractType();
            sc = RowToContractType(row);
            //MessageBox.Show(sc.isActive.ToString());

            var item = sCContractTypes.Find(x => x.OID == sc.OID);
            if (item == null)
            {
                sCContractTypes.Add(sc);
            }
            else
            {
                sCContractTypes.Remove(item);
                sCContractTypes.Add(sc);
            }

        }

        private void contractTypeList_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }





        private void contractTypeList_RowEnter(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void contractTypeList_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            // this.delBtn.Enabled = true;
        }

        private void contractTypeList_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            //   this.delBtn.Enabled = false;
        }

        private void contractTypeList_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            //int rowIndex = e.RowIndex;
            //DataGridViewRow row = contractTypeList.Rows[rowIndex];
            //// change color
            //ViewUtils.changeColor(row, Constant.isMarkDeleted);
        }
    }
}
