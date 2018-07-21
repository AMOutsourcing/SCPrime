using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SCPrime.Utils
{
    public class ViewUtils
    {
        public static void remarkHeader(DataGridViewRow r, string colName)
        {
            if (r.Cells[colName].Value.ToString().ToUpper().Equals("TRUE"))
            {
                //r.DefaultCellStyle.BackColor = Color.Red;
                r.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                r.HeaderCell.Value = "X";
            }
            if (r.Cells[colName].Value.ToString().ToUpper().Equals("FALSE"))
            {
                //r.DefaultCellStyle.BackColor = Color.White;
                r.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                r.HeaderCell.Value = "";
            }
        }

        public static IEnumerable<Control> GetAllControl(Control control, Type type)
        {
            var controls = control.Controls.Cast<Control>();
            if (type != null)
            {

                return controls.SelectMany(ctrl => GetAllControl(ctrl, type))
                                          .Concat(controls)
                                          .Where(c => c.GetType() == type);
            }
            else
                return controls;
        }
    }
}
