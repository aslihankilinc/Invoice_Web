using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tiger_Win
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataModel dm = new Tiger_Win.DataModel();
            var lst = dm.Fatura.AsEnumerable().Select((s, x) => new { s.No, x = x + 1 }).ToList();
        }
    }
}
