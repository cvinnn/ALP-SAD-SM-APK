using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using static Guna.UI2.Native.WinApi;

namespace ALP_SAD_SM_APK
{
    public partial class riwayat_transaksi : Form
    {
        public Dashboard db;
        public riwayat_transaksi()
        {
            InitializeComponent();
            InitializeDGV();
        }
        string strconn = "server=localhost;uid=root;pwd=Minato2004-05-05;database=db_sm";
        string query;

        MySqlConnection conn;
        MySqlCommand cmd;
        MySqlDataAdapter adapter;
        MySqlDataReader reader;

        DataTable dt;
        Bitmap bm;
        string trxid;

        private void InitializeDGV()
        {
            dgv_listbrg.Columns.Add("nonota", "Nomor Nota");
            dgv_listbrg.Columns.Add("tglterbit", "Tanggal Diterbitkan");
            dgv_listbrg.Columns.Add("total", "Total");

            // Set DataGridView properties
            dgv_listbrg.AutoGenerateColumns = false;
            dgv_listbrg.AllowUserToAddRows = false;

            // Set DataPropertyName for each column
            dgv_listbrg.Columns["nonota"].DataPropertyName = "Nomor Nota";
            dgv_listbrg.Columns["tglterbit"].DataPropertyName = "Tanggal Diterbitkan";
            dgv_listbrg.Columns["total"].DataPropertyName = "Total";

            // Make columns non-editable
            dgv_listbrg.Columns["nonota"].ReadOnly = true;
            dgv_listbrg.Columns["tglterbit"].ReadOnly = true;
            dgv_listbrg.Columns["total"].ReadOnly = true;

            // Set text color to white for all columns
            dgv_listbrg.DefaultCellStyle.ForeColor = Color.White;

            // Load data into the DataGridView
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                conn = new MySqlConnection(strconn);
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM db_sm.nota_view;", conn))
                {
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dgv_listbrg.DataSource = dt;
                        dgv_listbrg.Refresh();
                    }
                }
            }
            finally
            {
                conn.Close();
            }
        }

        private void dgv_listbrg_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0)
            {
                trxid = dgv_listbrg.Rows[e.RowIndex].Cells["nonota"].Value.ToString();
            }
            txt_idtrx.Text = trxid;
            txt_idtrx.ReadOnly = true;
            btn_view.Visible = true;
        }

        private void dgv_listitem_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            dgv_listbrg.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(4, 119, 255);
        }

        private void btn_view_Click(object sender, EventArgs e)
        {
            Invoice_Pic ip = new Invoice_Pic();
            ip.trxid = this.trxid;
            ip.InitializeDGV();
            ip.labeldataload();
            ip.rt = this;
            ip.Dock = DockStyle.Fill;
            ip.ShowDialog();
        }
    }
}
