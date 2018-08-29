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
using System.Configuration;
using System.IO;
using Microsoft.Office.Interop.Word;
using log4net;
using Microsoft.Win32;

namespace SCPrime.Contracts
{
    public partial class dlgPrintContract : clsBaseDialog
    {
        public Contract objContact;
        String docPath = ConfigurationManager.AppSettings["DocumentPath"];
        static readonly ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public dlgPrintContract(Contract param)
        {
            InitializeComponent();
            objContact = param;
            this.Text = "Print contract " + objContact.ContractNo.ToString();
            pbOK.Visible = false;
        }

        private void dlgPrintContract_Load(object sender, EventArgs e)
        {
            TreeNode objCat = new TreeNode("Word templates");
            foreach (String strFileName in Directory.GetFiles(docPath, "*.*"))
            {
                if((strFileName.EndsWith(".dot") && !strFileName.Contains("~$rm.")))
                objCat.Nodes.Add(new TreeNode(strFileName));
                
            }
               
            trDocuments.Nodes.Add(objCat);
            
            objCat = new TreeNode("SSRS templates");
            trDocuments.Nodes.Add(objCat);
            trDocuments.ExpandAll();
        }

        private void pbPrint_Click(object sender, EventArgs e)
        {
            printToWord(false);
        }

        private void pbPreview_Click(object sender, EventArgs e)
        {
            printToWord(true);
        }

        private void printToWord(bool bPreview)
        {

            object strFile;
            Microsoft.Office.Interop.Word.Application objWord = new Microsoft.Office.Interop.Word.Application();
            Microsoft.Office.Interop.Word.Document objDoc = new Microsoft.Office.Interop.Word.Document();
            
            object missing = System.Reflection.Missing.Value;
            object myTrue = true;
            object myFalse = false;
            objWord.Options.MapPaperSize = false;
            object objBookmark = "ContractOID";
            clsGlobalVariable objGlobal = new clsGlobalVariable();
            TreeNode objWordNode = trDocuments.Nodes[0];
                foreach (TreeNode objNode in objWordNode.Nodes)
                {
                    if (objNode.Checked == true)
                    {
                        strFile = objNode.Text;
                        objDoc = objWord.Documents.AddOld(ref strFile, ref missing);

                        try
                        {
                            objBookmark = "ContractOID";
                            objDoc.FormFields.get_Item(ref objBookmark).Result = objContact.ContractOID.ToString();
                        string sqlserver= (string) Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Wow6432Node\\ODBC\\ODBC.INI\\" + objGlobal.DMSDBName,"Server","localhost");
                            objBookmark = "VSPISRV";
                            objDoc.FormFields.get_Item(ref objBookmark).Result = sqlserver;
                            objBookmark = "VSPIDB";
                            objDoc.FormFields.get_Item(ref objBookmark).Result = objGlobal.DMSDBName;
                            objBookmark = "VSPIUSER";
                            objDoc.FormFields.get_Item(ref objBookmark).Result = objGlobal.DMSUserName;
                            objBookmark = "VSPIPASS";
                            objDoc.FormFields.get_Item(ref objBookmark).Result = objGlobal.DMSDBPass;
                            objWord.Run("GetExtraData");

                        }
                        catch (Exception ex)
                        {
                        _log.Error(ex.ToString());
                        }
                    if (!bPreview)
                    {
                        objDoc.PrintOutOld(ref myFalse, ref myFalse, ref missing, ref missing, ref missing, ref missing, ref missing,
                        ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing);
                        objDoc.Close(ref myFalse, ref missing, ref missing);
                    }
                    else
                    {
                          objDoc.Activate();
                          objWord.Visible = true;
                    }
                     
                    }
                }
            if (!bPreview)
            {
                objWord.Quit(ref myFalse, ref missing, ref missing);
                objWord = null;
            }
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

        private void dlgPrintContract_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
