using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using static Guna.UI2.Native.WinApi;

namespace ALP_SAD_SM_APK
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent(); 
            this.Dock = DockStyle.Fill;
            this.TopLevel = false;
            loadpanel();
            /*loadusername(username);*/
        }

        Form runningform;

        public Panel overlayPanel;
        public string username;
        public string runningfpid = "";
        public bool nais = false;
        public int counter = 0;

        string strconn = "server=localhost;uid=root;pwd=Minato2004-05-05;database=db_sm";
        string query;

        MySqlConnection conn;
        MySqlCommand cmd;
        MySqlDataAdapter adapter;
        MySqlDataReader reader;

        DataTable dt;

        public void loadusername(string username)
        {
            lbl_username.Text = $"Hi, {username}";
        }
        
        private void loadpanel()
        {
            overlayPanel = new Panel();
            overlayPanel.BackColor = Color.FromArgb(30, Color.Black);
            overlayPanel.Dock = DockStyle.Fill;
            this.Controls.Add(overlayPanel);
            overlayPanel.Visible = false;
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            rundb();
        }

        public void rundb ()
        {
            if (nais == false)
            {
                nais = true;
                Dashboardpanel dbp = new Dashboardpanel();
                dbp.Dock = DockStyle.Fill;
                txt_namamenu.Text = "Dashboard";
                LoadChildForm(dbp);
            }
            else
            {
                runningform.Close();
                Dashboardpanel dbp = new Dashboardpanel();
                dbp.Dock = DockStyle.Fill;
                txt_namamenu.Text = "Dashboard";
                LoadChildForm(dbp);
            }
        }

        private void LoadChildForm(Form childForm)
        {
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            pnl_ftr.Controls.Add(childForm);
            childForm.Show();
            runningform = childForm;
        }
        
        private void btn_fkturpjl_Click(object sender, EventArgs e)
        {
            runpjl(counter);
        }

        public void runpjl (int counterpj)
        {
            switch (counterpj)
            {
                case 0:
                    runningform.Close();
                    Faktur_Penjualan fp = new Faktur_Penjualan();
                    fp.db = this;
                    txt_namamenu.Text = "Faktur Penjualan";
                    LoadChildForm(fp);
                    counter = 1;
                    break;
                case 1:
                    break;
                case 2:
                    deletedata();
                    runningform.Close();
                    fp = new Faktur_Penjualan();
                    fp.db = this;
                    txt_namamenu.Text = "Faktur Penjualan";
                    LoadChildForm(fp);
                    break;
            }
        }

        private void btn_db_Click(object sender, EventArgs e)
        {
            if (nais == true)
            {
                deletedata();
            }
            runningform.Close();
            Dashboardpanel dbp = new Dashboardpanel();
            dbp.db = this;
            txt_namamenu.Text = "Dashboard";
            counter = 0;
            LoadChildForm(dbp);
        }

        private void deletedata()
        {
            string query = $"CALL RemoveDataByTRXID('{runningfpid}')";
            conn = new MySqlConnection(strconn);
            conn.Open();
            cmd = new MySqlCommand(query, conn);
            reader = cmd.ExecuteReader();
            conn.Close();
        }

        private void btn_dlt_Click(object sender, EventArgs e)
        {
            if(nais == true)
            {
                deletedata();
            }

            List<Form> formsToClose = new List<Form>();

            foreach (Form form in Application.OpenForms)
            {
                if (form != this)
                {
                    formsToClose.Add(form);
                }
            }

            foreach (Form form in formsToClose)
            {
                form.Close();
            }
        }

        private void btn_pglnbrg_Click(object sender, EventArgs e)
        {
            if (nais == true)
            {
                deletedata();
            }
            runningform.Close();
            Pengelolahan_Barang pgbrg = new Pengelolahan_Barang();
            pgbrg.Dock= DockStyle.Fill;
            pgbrg.db = this;
            txt_namamenu.Text = "Pengelolaan Barang";
            counter = 0;
            LoadChildForm(pgbrg);
        }

        private void btn_rwtrx_Click(object sender, EventArgs e)
        {
            if (nais == true)
            {
                deletedata();
            }
            runningform.Close();
            riwayat_transaksi rt = new riwayat_transaksi();
            rt.db = this;
            txt_namamenu.Text = "Riwayat Transaksi";
            counter = 0;
            LoadChildForm(rt);
        }
    }
}
