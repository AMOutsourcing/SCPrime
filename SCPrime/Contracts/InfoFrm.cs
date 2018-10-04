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

namespace SCPrime.Contracts
{
    public partial class InfoFrm : clsBaseDialog
    {
        public InfoFrm()
        {
            InitializeComponent();
        }

        //Singleton
        private static InfoFrm _instance;

        public static InfoFrm getInstance()
        {
            if (InfoFrm._instance == null || InfoFrm._instance.IsDisposed)
            {
                InfoFrm._instance = new InfoFrm();
            }
            return InfoFrm._instance;
        }

        static readonly ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private RemarkFrm frm;

        public void setFrm(RemarkFrm form)
        {
            frm = form;
        }

        public void setValue(String info)
        {
            txtInfo.Text = info;
        }

        public string getValue()
        {
            return txtInfo.Text;
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (Form.ModifierKeys == Keys.None && keyData == Keys.Escape)
            {
                this.Close();
                return true;
            }
            return base.ProcessDialogKey(keyData);
        }

        private void InfoFrm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void pbOK_Click(object sender, EventArgs e)
        {
            if (frm != null)
            {
                frm.setInfoValue(txtInfo.Text);
            }
            this.Close();
            RemarkFrm.getInstance().Refresh();
        }
    }
}
