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

namespace SCPrime
{
    public partial class SCMain : nsBaseClass.clsBaseForm
    {
        static readonly ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public SCMain()
        {
            InitializeComponent();
            this.Visible = false;
        }

        private void SCMain_Load(object sender, EventArgs e)
        {
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

                //ThuyetLV
                initData();
            }
            else
            {
                Application.Exit();
            }
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
            cblModel.ValueMember = "value";
            cblModel.DisplayMember = "text";

            //Set check default
            string value;
            for (int i = 0; i < cblModel.Items.Count; i++)
            {
                value = ((ObjTmp)cblModel.Items[i]).value;
                if (value != "C" && value != "D")
                {
                    cblModel.SetItemChecked(i, true);
                }
            }


            //Load sites
            
            List<clsBaseListItem> listTmp = sCBase.getAMSites();

            List<ObjTmp> lstSites = new List<ObjTmp>(result.Count);
            foreach (clsBaseListItem site in listTmp)
            {
                lstSites.Add(new ObjTmp(site.nValue1, site.strText));
            }
            cbSites.DataSource = lstSites;
            cbSites.ValueMember = "id";
            cbSites.DisplayMember = "text";


            //Load contaactType
            cblContactType.DataSource = sCBase.getContractTypes();
            cblContactType.DisplayMember = "Name";
            cblContactType.ValueMember = "OID";
        }
    }
}
