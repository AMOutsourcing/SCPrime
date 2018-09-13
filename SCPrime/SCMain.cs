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
using nsBaseClass;
using System.Configuration;
using System.Reflection;
using SCPrime.Model;
using SCPrime.Utils;
using SCPrime.Contracts;
using System.Collections;
using CsvHelper;
using System.IO;

namespace SCPrime
{
    public partial class SCMain : nsBaseClass.clsBaseForm
    {
        static readonly ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static int ContractOid;

        private static SCMain _instance;

        public static SCMain getInstance()
        {
            if (SCMain._instance == null || SCMain._instance.IsDisposed)
            {
                SCMain._instance = new SCMain();
            }
            return SCMain._instance;
        }

        SCMain()
        {
            InitializeComponent();
            this.Visible = false;
            // remove context menu
            this.ContextMenuStrip.Items.Clear();

        }

        private void SCMain_Load(object sender, EventArgs e)
        {
            ContractOid = -1;
            _log.Info("Start Service Contract Prime...Version " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString());
            clsLoginDialog f = new clsLoginDialog();
            DialogResult i = f.ShowDialog();
            if (i == DialogResult.OK)
            {
                //Check user rights
                // If user has been autenticated
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(objGlobal.CultureInfo);

                //objUtil.Localization.TranslateForm(this);
                loadProfileData();

                this.Text = objGlobal.DMSFirstUserName + "@" + objAppConfig.getSiteNameOnScreen();//+ this.Text;
                _log.Info("Version = " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString() + ", Title = " + this.Text);

                this.Visible = true;
                this.WindowState = FormWindowState.Maximized;

                //testing
                /*
                SCBase objBase = new SCBase();
                List<SCContractType> aList = objBase.getContractTypes();
                MessageBox.Show(aList.Count.ToString());
                //objBase.saveContractTypes(aList);
                List<SCOptionCategory> objOptionCats = SCOptionCategory.getContractOptionCategoryPriceList(2);
                MessageBox.Show(objOptionCats.Count.ToString());
                */
                resizeHeight();
                //ThuyetLV
                initData();
            }
            else
            {
                Application.Exit();
            }
        }
        private void resizeHeight()
        {
            this.panel4.Height = this.panel1.Height - this.panel3.Height - 20;
            this.panel4.Width = this.cblModel.Width;
        }

        private void contractTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Form1 f1 = new Form1();
            Form1.instance.Show();
            Form1.instance.Focus();
            Form1.instance.BringToFront();
        }

        private void optionListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SCOptionList f2 = new SCOptionList();
            f2.Show();
        }

        private void contractTypeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form1.instance.ShowDialog();
        }

        private void optionListToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SCOptionList.instance.ShowDialog();

        }

        private void optionPriceListToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SCOptionPriceFrm.getInstance().ShowDialog();
        }

        private void exitApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //ThuyetLV
        SCBase sCBase;
        private void initData()
        {
            sCBase = new SCBase();

            //Init model
            List<string> result = typeof(ContractStatusString).GetAllPublicConstantValues<string>();
            List<ObjTmp> lstModel = new List<ObjTmp>(result.Count);
            string[] words;
            string[] stringSeparators = new string[] { "-" };

            foreach (string s in result)
            {
                words = s.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
                lstModel.Add(new ObjTmp(words[0], words[1]));
            }
            cblModel.DataSource = lstModel;
            cblModel.ValueMember = "strValue1";
            cblModel.DisplayMember = "strText";


            //Load sites
            List<clsBaseListItem> listTmp = sCBase.getAMSites();

            List<ObjTmp> lstSites = new List<ObjTmp>(listTmp.Count);
            foreach (clsBaseListItem site in listTmp)
            {
                lstSites.Add(new ObjTmp(site.strValue1, site.strText));
            }
            cbSites.DataSource = lstSites;
            cbSites.ValueMember = "strValue1";
            cbSites.DisplayMember = "strValue1";


            //Load contaactType
            cblContactType.DataSource = sCBase.getContractTypeActive();
            cblContactType.DisplayMember = "Name";
            cblContactType.ValueMember = "OID";

            //Set check default
            setCheckDefault();


            //View searchContract
            searchContract();
        }

        private void setCheckDefault()
        {
            //check defaut staus
            string value;
            for (int i = 0; i < cblModel.Items.Count; i++)
            {
                value = ((ObjTmp)cblModel.Items[i]).strValue1;
                if (value != "C" && value != "D")
                {
                    cblModel.SetItemChecked(i, true);
                }
            }

            //Set check All contracttype
            for (int i = 0; i < cblContactType.Items.Count; i++)
            {
                cblContactType.SetItemChecked(i, true);
            }

            //Default site
            for (int i = 0; i < cbSites.Items.Count; i++)
            {
                cbSites.SetItemChecked(i, true);
            }
        }

        private void nEwContractToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SCMain.ContractOid = 0;
            ContractFrm cf = new ContractFrm();
            cf.StartPosition = FormStartPosition.CenterParent;
            cf.ShowDialog();
        }



        private void cblContactType_MouseHover(object sender, EventArgs e)
        {
            this.displayTooltip();
        }

        private void cblContactType_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void cblContactType_MouseEnter(object sender, EventArgs e)
        {

        }
        public void displayTooltip()
        {
            Point pos = cblContactType.PointToClient(MousePosition);
            var ttIndex = cblContactType.IndexFromPoint(pos);
            ToolTip toolTip1 = new ToolTip();
            toolTip1.UseFading = true;
            toolTip1.UseAnimation = true;
            toolTip1.IsBalloon = true;
            toolTip1.ShowAlways = true;
            toolTip1.AutoPopDelay = 5000;
            toolTip1.InitialDelay = 1000;
            toolTip1.ReshowDelay = 500;
            if (ttIndex > -1 && ttIndex < cblContactType.Items.Count)
            {
                // pos = PointToClient(MousePosition);
                toolTip1.Hide(cblContactType);
                toolTip1 = new ToolTip();
                toolTip1.ToolTipTitle = "";

                string s = cblContactType.GetItemText(cblContactType.Items[ttIndex]);
                toolTip1.SetToolTip(cblContactType, s);



            }
        }

        private void cblContactType_SelectedIndexChanged(object sender, EventArgs e)
        {
            // this.displayTooltip();
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            searchContract();
        }

        List<Contract> listContract = new List<Contract>();
        public void searchContract()
        {
            List<SCContractType> listContractType = new List<SCContractType>();
            foreach (SCContractType itemChecked in cblContactType.CheckedItems)
            {
                listContractType.Add(itemChecked);
            }

            List<String> listSite = new List<String>();
            foreach (ObjTmp itemChecked in cbSites.CheckedItems)
            {
                listSite.Add(itemChecked.strValue1);
            }

            List<String> listStatus = new List<String>();
            foreach (ObjTmp itemChecked in cblModel.CheckedItems)
            {
                listStatus.Add(itemChecked.strValue1);
            }

            System.Diagnostics.Debug.WriteLine("toolStripButton9_Click Text: " + toolStripTextBox1.Text);

            listContract = SCBase.searchContracts(listContractType, listSite, listStatus, toolStripTextBox1.Text);

            buildGridView(listContract);

            System.Diagnostics.Debug.WriteLine("toolStripButton9_Click listContract: " + listContract.Count);
        }

        bool init = true;
        private void buildGridView(List<Contract> listContract)
        {
            //gridContract.DataSource = null;
            //gridContract.Columns.Clear();
            gridContract.Refresh();
            //gridContract.AutoGenerateColumns = false;
            //gridContract.AllowUserToAddRows = false;

            var bs = new BindingSource();
            bs.DataSource = listContract.AsQueryable();

            gridContract.DataSource = bs;

            if (init)
            {
                generateColumns();
                init = false;
            }
        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tmp fr = new tmp();
            fr.ShowDialog();
        }

        private void generateColumns()
        {
            ////Generate column
            int i = 0;
            DataGridViewTextBoxColumn colStatus = new DataGridViewTextBoxColumn();
            colStatus.Name = "Status";
            colStatus.HeaderText = "Status";
            colStatus.DataPropertyName = "Status";
            gridContract.Columns.Insert(i, colStatus);

            i = 9;
            DataGridViewTextBoxColumn colSite = new DataGridViewTextBoxColumn();
            colSite.Name = "ResponsibleSite";
            colSite.HeaderText = "Responsible Site";
            colSite.DataPropertyName = "ResponsibleSite";
            gridContract.Columns.Insert(i++, colSite);

            DataGridViewTextBoxColumn colContractType = new DataGridViewTextBoxColumn();
            colContractType.Name = "ContractType";
            colContractType.HeaderText = "Contract Type";
            colContractType.DataPropertyName = "ContractTypeName";
            gridContract.Columns.Insert(i++, colContractType);

            DataGridViewTextBoxColumn colCusNo = new DataGridViewTextBoxColumn();
            colCusNo.Name = "CustNo";
            colCusNo.HeaderText = "Cust No";
            colCusNo.DataPropertyName = "CustNo";
            gridContract.Columns.Insert(i++, colCusNo);

            DataGridViewTextBoxColumn colCusName = new DataGridViewTextBoxColumn();
            colCusName.Name = "CustName";
            colCusName.HeaderText = "Cust Name";
            colCusName.DataPropertyName = "CustName";
            gridContract.Columns.Insert(i++, colCusName);

            DataGridViewTextBoxColumn colInvCusNo = new DataGridViewTextBoxColumn();
            colInvCusNo.Name = "InvCustNo";
            colInvCusNo.HeaderText = "INV Cust No";
            colInvCusNo.DataPropertyName = "InvCustNo";
            gridContract.Columns.Insert(i++, colInvCusNo);

            DataGridViewTextBoxColumn InvCustName = new DataGridViewTextBoxColumn();
            InvCustName.Name = "InvCustName";
            InvCustName.HeaderText = "INV Cust Name";
            InvCustName.DataPropertyName = "InvCustName";
            gridContract.Columns.Insert(i++, InvCustName);

            DataGridViewTextBoxColumn colVehiLic = new DataGridViewTextBoxColumn();
            colVehiLic.Name = "VehiLicNo";
            colVehiLic.HeaderText = "Lic. No";
            colVehiLic.DataPropertyName = "VehiLicNo";
            gridContract.Columns.Insert(i++, colVehiLic);

            DataGridViewTextBoxColumn colVIN = new DataGridViewTextBoxColumn();
            colVIN.Name = "VIN";
            colVIN.HeaderText = "VIN";
            colVIN.DataPropertyName = "VIN";
            gridContract.Columns.Insert(i++, colVIN);

            //ContractDAte
            DataGridViewTextBoxColumn colStartDate = new DataGridViewTextBoxColumn();
            colStartDate.Name = "ContractStartDate";
            colStartDate.HeaderText = "Start Date";
            colStartDate.DataPropertyName = "ContractStartDate";
            gridContract.Columns.Insert(i++, colStartDate);

            DataGridViewTextBoxColumn colEndDate = new DataGridViewTextBoxColumn();
            colEndDate.Name = "ContractEndDate";
            colEndDate.HeaderText = "End Date";
            colEndDate.DataPropertyName = "ContractEndDate";
            gridContract.Columns.Insert(i++, colEndDate);

            DataGridViewTextBoxColumn colPeriodkm = new DataGridViewTextBoxColumn();
            colPeriodkm.Name = "ContractPeriodKm";
            colPeriodkm.HeaderText = "Period km";
            colPeriodkm.DataPropertyName = "ContractPeriodKm";
            gridContract.Columns.Insert(i++, colPeriodkm);

            DataGridViewTextBoxColumn colPeriodHr = new DataGridViewTextBoxColumn();
            colPeriodHr.Name = "ContractPeriodHr";
            colPeriodHr.HeaderText = "Period Hr";
            colPeriodHr.DataPropertyName = "ContractPeriodHr";
            gridContract.Columns.Insert(i++, colPeriodHr);

            DataGridViewTextBoxColumn colKmOrHr = new DataGridViewTextBoxColumn();
            colKmOrHr.Name = "KmOrHr";
            colKmOrHr.HeaderText = "Km/Hour";
            colKmOrHr.DataPropertyName = "KmOrHr";
            gridContract.Columns.Insert(i++, colKmOrHr);

            DataGridViewTextBoxColumn colTerminationType = new DataGridViewTextBoxColumn();
            colTerminationType.Name = "getTerminationType";
            colTerminationType.HeaderText = "Termination type";
            colTerminationType.DataPropertyName = "getTerminationType";
            gridContract.Columns.Insert(i++, colTerminationType);

            //Payment 
            DataGridViewTextBoxColumn colPayPeriod = new DataGridViewTextBoxColumn();
            colPayPeriod.Name = "PaymentPeriod";
            colPayPeriod.HeaderText = "Payment period";
            colPayPeriod.DataPropertyName = "PaymentPeriod";
            gridContract.Columns.Insert(i++, colPayPeriod);

            DataGridViewCheckBoxColumn colPayBlock = new DataGridViewCheckBoxColumn();
            colPayBlock.Name = "PaymentInBlock";
            colPayBlock.HeaderText = "Payment in block";
            colPayBlock.DataPropertyName = "PaymentInBlock";
            colPayBlock.SortMode = DataGridViewColumnSortMode.Automatic;
            gridContract.Columns.Insert(i++, colPayBlock);

            DataGridViewTextBoxColumn colPayNextStart = new DataGridViewTextBoxColumn();
            colPayNextStart.Name = "PaymentNextBlockStart";
            colPayNextStart.HeaderText = "Next block start";
            colPayNextStart.DataPropertyName = "PaymentNextBlockStart";
            gridContract.Columns.Insert(i++, colPayNextStart);

            DataGridViewTextBoxColumn colPayNextEnd = new DataGridViewTextBoxColumn();
            colPayNextEnd.Name = "PaymentNextBlockEnd";
            colPayNextEnd.HeaderText = "Next block end";
            colPayNextEnd.DataPropertyName = "PaymentNextBlockEnd";
            gridContract.Columns.Insert(i++, colPayNextEnd);

            DataGridViewTextBoxColumn colPayCollectType = new DataGridViewTextBoxColumn();
            colPayCollectType.Name = "PaymentCollecType";
            colPayCollectType.HeaderText = "Payment collection type";
            colPayCollectType.DataPropertyName = "PaymentCollecType";
            gridContract.Columns.Insert(i++, colPayCollectType);


            DataGridViewTextBoxColumn colInvSites = new DataGridViewTextBoxColumn();
            colInvSites.Name = "InvSites";
            colInvSites.HeaderText = "Invoice site";
            colInvSites.DataPropertyName = "InvSites";
            gridContract.Columns.Insert(i++, colInvSites);

            i = 30;
            DataGridViewTextBoxColumn LastMileDate = new DataGridViewTextBoxColumn();
            LastMileDate.Name = "LastMileDate";
            LastMileDate.HeaderText = "Last mileage date";
            LastMileDate.DataPropertyName = "LastMileDate";
            gridContract.Columns.Insert(i++, LastMileDate);

            DataGridViewTextBoxColumn LastMile = new DataGridViewTextBoxColumn();
            LastMile.Name = "LastMile";
            LastMile.HeaderText = "Last mileage";
            LastMile.DataPropertyName = "LastMile";
            gridContract.Columns.Insert(i++, LastMile);
        }

        private void gridContract_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void gridContract_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                DataGridViewRow r = gridContract.Rows[e.RowIndex];
                if (r != null && !string.IsNullOrEmpty(r.Cells["colCcontractOID"].Value.ToString()))
                {
                    ContractOid = (int)r.Cells["colCcontractOID"].Value;
                    ContractFrm cf = new ContractFrm();
                    cf.StartPosition = FormStartPosition.CenterParent;
                    cf.ShowDialog();
                }
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            SCMain.ContractOid = 0;
            ContractFrm cf = new ContractFrm();
            cf.StartPosition = FormStartPosition.CenterParent;
            cf.ShowDialog();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow r in this.gridContract.SelectedRows)
            {
                ContractOid = (int)r.Cells["colCcontractOID"].Value;
            }
            ContractFrm cf = new ContractFrm();
            cf.StartPosition = FormStartPosition.CenterParent;
            cf.ShowDialog();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow r in this.gridContract.SelectedRows)
            {
                ContractOid = (int)r.Cells["colCcontractOID"].Value;
            }
            ContractFrm cf = new ContractFrm();
            cf.StartPosition = FormStartPosition.CenterParent;
            cf.ShowDialog();
        }

        private void prinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow r in this.gridContract.SelectedRows)
            {
                ContractOid = (int)r.Cells["colCcontractOID"].Value;
            }
            Contract mycontract = SCBase.searchContracts(SCMain.ContractOid);
            dlgPrintContract printer = new dlgPrintContract(mycontract);
            printer.ShowDialog();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow r in this.gridContract.SelectedRows)
            {
                ContractOid = (int)r.Cells["colCcontractOID"].Value;
            }
            Contract mycontract = SCBase.searchContracts(SCMain.ContractOid);
            dlgPrintContract printer = new dlgPrintContract(mycontract);
            printer.ShowDialog();
        }
        private void customerDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow r in this.gridContract.SelectedRows)
            {
                ContractOid = (int)r.Cells["colCcontractOID"].Value;
            }
            Contract mycontract = SCBase.searchContracts(SCMain.ContractOid);

            //TODO display customer data
        }

        private void vehicleDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow r in this.gridContract.SelectedRows)
            {
                ContractOid = (int)r.Cells["colCcontractOID"].Value;
            }
            Contract mycontract = SCBase.searchContracts(SCMain.ContractOid);

            //TODO display vehicle data
        }

        private void serviceHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow r in this.gridContract.SelectedRows)
            {
                ContractOid = (int)r.Cells["colCcontractOID"].Value;
            }
            Contract mycontract = SCBase.searchContracts(SCMain.ContractOid);

            //TODO display service history data
        }

        private void eLOArchiveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("eLOArchiveToolStripMenuItem_Click");
        }
        private void mileageRegisterToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void invoiceToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void invoiceToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //Export data
            bool exportSuccess = false;
            string myTempFile = Path.Combine(Path.GetTempPath(), Path.GetTempFileName() + ".csv");
            System.IO.TextWriter writeFile = null;
            try
            {
                writeFile = new StreamWriter(myTempFile);
                var csv = new CsvWriter(writeFile);

                //NOW CSV is Opened OK, can we make semicolon as separator (;) instead of comma.
                csv.Configuration.Delimiter = ";";

                if (listContract != null && listContract.Count > 0)
                {
                    csv.WriteRecords(listContract);
                }

                _log.Info("----------CsvWriter done: " + myTempFile);

                exportSuccess = true;

            }
            catch (Exception ex)
            {
                _log.Error("ERROR CsvWriter: ", ex);
            }
            finally
            {
                try
                {
                    if (writeFile != null)
                    {
                        writeFile.Flush();
                        writeFile.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    _log.Error("ERROR Close TextWriter: ", ex);
                }
            }

            if (exportSuccess)
            {
                //Open file
                System.Diagnostics.Process.Start(myTempFile);
            }

        }

        //---------------------------right click menu-----------------------------

        private void gridContract_MouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Right:
                    {
                        rightClickMenuStrip.Show(this, new Point(e.X + 200, e.Y + 50));//places the menu at the pointer position
                    }
                    break;
            }
        }

        private void openMenu_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow r in this.gridContract.SelectedRows)
            {
                ContractOid = (int)r.Cells["colCcontractOID"].Value;
            }
            ContractFrm cf = new ContractFrm();
            cf.StartPosition = FormStartPosition.CenterParent;
            cf.ShowDialog();
        }

        private void customerData_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow r in this.gridContract.SelectedRows)
            {
                ContractOid = (int)r.Cells["colCcontractOID"].Value;
            }
            Contract mycontract = SCBase.searchContracts(SCMain.ContractOid);

            //TODO display customer data
        }

        private void vehicleData_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("vehicleData_Click");
            foreach (DataGridViewRow r in this.gridContract.SelectedRows)
            {
                ContractOid = (int)r.Cells["colCcontractOID"].Value;
            }
            Contract mycontract = SCBase.searchContracts(SCMain.ContractOid);

            //TODO display vehicle data
        }

        private void serviceHistory_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow r in this.gridContract.SelectedRows)
            {
                ContractOid = (int)r.Cells["colCcontractOID"].Value;
            }
            Contract mycontract = SCBase.searchContracts(SCMain.ContractOid);

            //TODO display service history data
        }

        private void eLOArchive_Click(object sender, EventArgs e)
        {
            MessageBox.Show("eLOArchive_Right_Click");
        }

        private void mileageRegister_Click(object sender, EventArgs e)
        {
            MileageRegisterFrm frm = new MileageRegisterFrm();
            frm.ShowDialog();
        }

        private void NewContractItem_Click(object sender, EventArgs e)
        {
            SCMain.ContractOid = 0;
            ContractFrm cf = new ContractFrm();
            cf.StartPosition = FormStartPosition.CenterParent;
            cf.ShowDialog();
        }

        private void printMenuItem_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow r in this.gridContract.SelectedRows)
            {
                ContractOid = (int)r.Cells["colCcontractOID"].Value;
            }
            Contract mycontract = SCBase.searchContracts(SCMain.ContractOid);
            dlgPrintContract printer = new dlgPrintContract(mycontract);
            printer.ShowDialog();
        }

        private void invoicesMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("eLOArchive_Right_Click");
        }

        private void SCMain_Resize(object sender, EventArgs e)
        {
            this.resizeHeight();
        }

        private void cblModel_MouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Right:
                    {
                        rightClickMenuStripModel.Show(this, new Point(e.X, e.Y + 80));//places the menu at the pointer position
                    }
                    break;
            }
        }

        private void modelSelectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < cblModel.Items.Count; i++)
                cblModel.SetItemChecked(i, true);

            searchContract();
        }

        private void modelSelectNoneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < cblModel.Items.Count; i++)
                cblModel.SetItemChecked(i, false);

            //clear Grid
            this.gridContract.DataSource = null;
        }

        private void siteSelectAllToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < cbSites.Items.Count; i++)
                cbSites.SetItemChecked(i, true);

            searchContract();
        }

        private void siteSelectNoneToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < cbSites.Items.Count; i++)
                cbSites.SetItemChecked(i, false);

            //clear Grid
            this.gridContract.DataSource = null;
        }

        private void cbSites_MouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Right:
                    {
                        rightClickMenuStripSites.Show(this, new Point(e.X, e.Y + 270));//places the menu at the pointer position
                    }
                    break;
            }
        }

        private void cblContactType_MouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Right:
                    {
                        rightClickMenuStripContractType.Show(this, new Point(e.X, e.Y + 450));//places the menu at the pointer position
                    }
                    break;
            }
        }

        private void CTselectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < cblContactType.Items.Count; i++)
                cblContactType.SetItemChecked(i, true);

            searchContract();
        }

        private void CTselectNoneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < cblContactType.Items.Count; i++)
                cblContactType.SetItemChecked(i, false);

            //clear Grid
            this.gridContract.DataSource = null;
        }

        int lastSort = -1;
        bool sortAsc = true;
        private void gridContract_ColumnHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int colIdx = e.ColumnIndex;
            SortOrder sortOrder = gridContract.Columns[colIdx].HeaderCell.SortGlyphDirection;
            DataGridViewColumn newColumn = gridContract.Columns[colIdx];
            if (lastSort >= 0 && (lastSort == colIdx && sortAsc))
                sortAsc = false;
            else
                sortAsc = true;

            if (sortAsc)
            {
                switch (colIdx)
                {
                    case 0:
                        listContract = listContract.OrderBy(r => r.Status).ToList(); break;
                    case 1:
                        listContract = listContract.OrderBy(r => r.ContractOID).ToList(); break;
                    case 2:
                        listContract = listContract.OrderBy(r => r.ContractNo).ToList(); break;
                    case 3:
                        listContract = listContract.OrderBy(r => r.VersionNo).ToList(); break;
                    case 4:
                        listContract = listContract.OrderBy(r => r.ExtContractNo).ToList(); break;
                    case 5:
                        listContract = listContract.OrderBy(r => r.Created).ToList(); break;
                    case 6:
                        listContract = listContract.OrderBy(r => r.Modified).ToList(); break;
                    case 7:
                        listContract = listContract.OrderBy(r => r.LastInvoiceDate).ToList(); break;
                    case 8:
                        listContract = listContract.OrderBy(r => r.NextInvoiceDate).ToList(); break;
                    case 9:
                        listContract = listContract.OrderBy(r => r.ResponsibleSite).ToList(); break;
                    case 10:
                        listContract = listContract.OrderBy(r => r.ContractTypeName).ToList();
                        break;
                    case 11:
                        listContract = listContract.OrderBy(r => r.CustNo).ToList(); break;
                    case 12:
                        listContract = listContract.OrderBy(r => r.CustName).ToList(); break;
                    case 13:
                        listContract = listContract.OrderBy(r => r.InvCustNo).ToList(); break;
                    case 14:
                        listContract = listContract.OrderBy(r => r.InvCustName).ToList(); break;
                    case 15:
                        listContract = listContract.OrderBy(r => r.VehiLicNo).ToList(); break;
                    case 16:
                        listContract = listContract.OrderBy(r => r.VIN).ToList(); break;
                    case 17:
                        listContract = listContract.OrderBy(r => r.ContractStartDate).ToList(); break;
                    case 18:
                        listContract = listContract.OrderBy(r => r.ContractEndDate).ToList(); break;
                    case 19:
                        listContract = listContract.OrderBy(r => r.ContractPeriodKm).ToList(); break;
                    case 20:
                        listContract = listContract.OrderBy(r => r.ContractPeriodHr).ToList(); break;
                    case 21:
                        listContract = listContract.OrderBy(r => r.KmOrHr).ToList(); break;
                    case 22:
                        listContract = listContract.OrderBy(r => r.getTerminationType).ToList(); break;
                    case 23:
                        listContract = listContract.OrderBy(r => r.PaymentPeriod).ToList(); break;
                    case 24:
                        listContract = listContract.OrderBy(r => r.PaymentInBlock).ToList(); break;
                    case 25:
                        listContract = listContract.OrderBy(r => r.PaymentNextBlockStart).ToList(); break;
                    case 26:
                        listContract = listContract.OrderBy(r => r.PaymentNextBlockEnd).ToList(); break;
                    case 27:
                        listContract = listContract.OrderBy(r => r.PaymentCollecType).ToList(); break;
                    case 28:
                        listContract = listContract.OrderBy(r => r.InvSites).ToList(); break;
                    case 29:
                        listContract = listContract.OrderBy(r => r.IsManualInvoice).ToList(); break;
                    case 30:
                        listContract = listContract.OrderBy(r => r.LastMileDate).ToList(); break;
                    case 31:
                        listContract = listContract.OrderBy(r => r.LastMile).ToList(); break;
                }
            }
            else
            {
                switch (colIdx)
                {
                    case 0:
                        listContract = listContract.OrderByDescending(r => r.Status).ToList(); break;
                    case 1:
                        listContract = listContract.OrderByDescending(r => r.ContractOID).ToList(); break;
                    case 2:
                        listContract = listContract.OrderByDescending(r => r.ContractNo).ToList(); break;
                    case 3:
                        listContract = listContract.OrderByDescending(r => r.VersionNo).ToList(); break;
                    case 4:
                        listContract = listContract.OrderByDescending(r => r.ExtContractNo).ToList(); break;
                    case 5:
                        listContract = listContract.OrderByDescending(r => r.Created).ToList(); break;
                    case 6:
                        listContract = listContract.OrderByDescending(r => r.Modified).ToList(); break;
                    case 7:
                        listContract = listContract.OrderByDescending(r => r.LastInvoiceDate).ToList(); break;
                    case 8:
                        listContract = listContract.OrderByDescending(r => r.NextInvoiceDate).ToList(); break;
                    case 9:
                        listContract = listContract.OrderByDescending(r => r.ResponsibleSite).ToList(); break;
                    case 10:
                        listContract = listContract.OrderByDescending(r => r.ContractTypeName).ToList();
                        break;
                    case 11:
                        listContract = listContract.OrderByDescending(r => r.CustNo).ToList(); break;
                    case 12:
                        listContract = listContract.OrderByDescending(r => r.CustName).ToList(); break;
                    case 13:
                        listContract = listContract.OrderByDescending(r => r.InvCustNo).ToList(); break;
                    case 14:
                        listContract = listContract.OrderByDescending(r => r.InvCustName).ToList(); break;
                    case 15:
                        listContract = listContract.OrderByDescending(r => r.VehiLicNo).ToList(); break;
                    case 16:
                        listContract = listContract.OrderByDescending(r => r.VIN).ToList(); break;
                    case 17:
                        listContract = listContract.OrderByDescending(r => r.ContractStartDate).ToList(); break;
                    case 18:
                        listContract = listContract.OrderByDescending(r => r.ContractEndDate).ToList(); break;
                    case 19:
                        listContract = listContract.OrderByDescending(r => r.ContractPeriodKm).ToList(); break;
                    case 20:
                        listContract = listContract.OrderByDescending(r => r.ContractPeriodHr).ToList(); break;
                    case 21:
                        listContract = listContract.OrderByDescending(r => r.KmOrHr).ToList(); break;
                    case 22:
                        listContract = listContract.OrderByDescending(r => r.getTerminationType).ToList(); break;
                    case 23:
                        listContract = listContract.OrderByDescending(r => r.PaymentPeriod).ToList(); break;
                    case 24:
                        listContract = listContract.OrderByDescending(r => r.PaymentInBlock).ToList(); break;
                    case 25:
                        listContract = listContract.OrderByDescending(r => r.PaymentNextBlockStart).ToList(); break;
                    case 26:
                        listContract = listContract.OrderByDescending(r => r.PaymentNextBlockEnd).ToList(); break;
                    case 27:
                        listContract = listContract.OrderByDescending(r => r.PaymentCollecType).ToList(); break;
                    case 28:
                        listContract = listContract.OrderByDescending(r => r.InvSites).ToList(); break;
                    case 29:
                        listContract = listContract.OrderByDescending(r => r.IsManualInvoice).ToList(); break;
                    case 30:
                        listContract = listContract.OrderByDescending(r => r.LastMileDate).ToList(); break;
                    case 31:
                        listContract = listContract.OrderByDescending(r => r.LastMile).ToList(); break;
                }
            }


            buildGridView(listContract);

            if (sortAsc)
                gridContract.Columns[colIdx].HeaderCell.SortGlyphDirection = SortOrder.Ascending;
            else
                gridContract.Columns[colIdx].HeaderCell.SortGlyphDirection = SortOrder.Descending;
            if (lastSort >= 0)
                gridContract.Columns[lastSort].HeaderCell.SortGlyphDirection = SortOrder.None;

            lastSort = colIdx;
        }
    }
}
