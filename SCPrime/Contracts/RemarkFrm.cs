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
    public partial class RemarkFrm : UserControl
    {
        public RemarkFrm()
        {
            InitializeComponent();
        }


        //Singleton
        private static RemarkFrm _instance;

        public static RemarkFrm getInstance()
        {
            if (RemarkFrm._instance == null || RemarkFrm._instance.IsDisposed)
            {
                RemarkFrm._instance = new RemarkFrm();
            }
            return RemarkFrm._instance;
        }
    }
}
