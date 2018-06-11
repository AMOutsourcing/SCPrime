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
    public partial class SCOptionList : Form
    {
        private static SCOptionList _instance;
        private List<SCOptionCategory> myCategories;
        private DataTable categoryDataTable;
        public SCOptionList()
        {
            InitializeComponent();
        }
        public SCOptionList instance
        {
            get
            {
                if (SCOptionList._instance == null)
                {
                    SCOptionList._instance = new SCOptionList();
                }
                return SCOptionList._instance;
            }
        }

        private void SCOptionList_Load(object sender, EventArgs e)
        {
            loadCategoryData();
            loadTree();
        }

        private void loadCategoryData()
        {
            this.myCategories = SCOptionCategory.getOptionCategoryList();
           // MessageBox.Show(myCategories.Count.ToString());
            this.categoryDataTable = ObjectUtils.ConvertToDataTable(this.myCategories);
            dataGridViewCategory.DataSource = this.categoryDataTable;
        }

        private void loadTree()
        {
            //load category

          //  List<SCOptionCategory> myCategories = SCOptionCategory.getOptionCategoryList();
            if (this.myCategories.Count > 0)
            {
                foreach (SCOptionCategory cat in myCategories)
                {
                    TreeNode treeNode = new TreeNode(cat.Name);
                    treeView1.Nodes.Add(treeNode);

                    //load all Option
                    List<SCOption> myOptions = new List<SCOption>();
                    myOptions = SCOption.getOptionList(cat.OID);
                    if (myOptions.Count > 0)
                    {
                        foreach (SCOption op in myOptions)
                        {
                            TreeNode treeNodeL2 = new TreeNode(op.Name);
                            treeNode.Nodes.Add(treeNodeL2);
                            //load all detail
                            List<SCOptionDetail> myOptionDetails = new List<SCOptionDetail>();
                            myOptionDetails = SCOptionDetail.getOptionDetailList(op.OID);
                            if (myOptionDetails.Count > 0)
                            {
                                foreach (SCOptionDetail sod in myOptionDetails)
                                {
                                    // create childnode level3
                                    TreeNode treeNodeL3 = new TreeNode(sod.Name);
                                    treeNodeL2.Nodes.Add(treeNodeL3);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (SCOptionList._instance != null)
            {
                SCOptionList._instance.Close();
            }
        }

        private void SCOptionList_FormClosed(object sender, FormClosedEventArgs e)
        {
            SCOptionList._instance = null;
        }
    }
}
