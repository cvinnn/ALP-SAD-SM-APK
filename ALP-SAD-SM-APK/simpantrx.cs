using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ALP_SAD_SM_APK
{
    public partial class simpantrx : Form
    {
        public simpantrx()
        {
            InitializeComponent();
        }
        public Faktur_Penjualan fp;

        private void AddTrx_Load(object sender, EventArgs e)
        {
            fp.db.Enabled = false;
            fp.db.overlayPanel.Visible = true;
            fp.db.overlayPanel.BringToFront();
        }

        private void btn_nntisj_Click(object sender, EventArgs e)
        {
            fp.db.counter = 0;
            fp.db.rundb();
            this.Close();
        }

        private void btn_tmbh_Click(object sender, EventArgs e)
        {
            fp.db.runpjl(0);
            this.Close();
        }
    }
}
