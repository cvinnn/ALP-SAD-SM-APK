using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using TheArtOfDevHtmlRenderer.Adapters;
using static Guna.UI2.Native.WinApi;

namespace ALP_SAD_SM_APK
{
    public partial class Faktur_Penjualan : Form
    {
        public Dashboard db;
        public Faktur_Penjualan()
        {
            InitializeComponent();
            db.nais = true;
        }

        string strconn = "server=localhost;uid=root;pwd=Minato2004-05-05;database=db_sm";
        string query;
        public string newTransactionId = "";

        MySqlConnection conn;
        MySqlCommand cmd;
        MySqlDataAdapter adapter;
        MySqlDataReader reader;

        DataTable dt;

        private string GenerateNewTRXID()
        {
            string query = "SELECT db_sm.GenerateNewTRXID() AS NewTRXID;";
            string newTRXID = "";

            using (MySqlConnection conn = new MySqlConnection(strconn))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    // Use ExecuteScalar to retrieve a single value
                    newTRXID = cmd.ExecuteScalar()?.ToString();
                }

                conn.Close();
            }

            return newTRXID;
        }

        private void loadidbaru()
        {
            newTransactionId = GenerateNewTRXID();
            txt_notrx.Text = newTransactionId;
            db.runningfpid = newTransactionId;
            DateTime tanggal = DateTime.Now.Date;

            if (txt_tglnota != null)
            {
                txt_tglnota.Text = tanggal.ToString("yyyy-MM-dd");
            }

            query = $"INSERT INTO transaksi (TRX_ID, TRX_SubTotal, TRX_Total, TRX_Date) VALUES ('{newTransactionId}', 0, 0, NOW());";
            conn = new MySqlConnection(strconn);
            conn.Open();
            cmd = new MySqlCommand(query, conn);
            reader = cmd.ExecuteReader();
            conn.Close();
        }

        private void dgv_listitem_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            dgv_listitem.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(4, 119, 255);
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            AddTrx at = new AddTrx();
            at.fp = this;
            at.Dock = DockStyle.Fill;
            at.trxid = txt_notrx.Text;
            at.ShowDialog();
        }

        public void calculation(string trxid)
        {
            query = $"SELECT * FROM db_sm.trxsummary where TRX_ID = '{trxid}';";
            conn = new MySqlConnection(strconn);

            try
            {
                conn.Open();
                cmd = new MySqlCommand(query, conn);
                reader = cmd.ExecuteReader();

                if (reader.Read()) // Check if there is a result
                {
                    // Retrieve values from the result
                    string trxSubTotal = reader["TRX_SubTotal"].ToString();
                    string trxTotal = reader["TRX_Total"].ToString();

                    // Convert values to numeric types if necessary
                    int subTotal = int.Parse(trxSubTotal);
                    int total = int.Parse(trxTotal);

                    // Calculate values
                    int ppn = total - subTotal;

                    // Format values as strings with currency symbol and comma separators
                    string formattedSubTotal = $"Rp{subTotal:#,##0}";
                    string formattedPPN = $"Rp{ppn:#,##0}";
                    string formattedTotal = $"Rp{total:#,##0}";

                    // Update labels with the formatted strings
                    lbl_subtot.Text = formattedSubTotal;
                    lbl_ppn.Text = formattedPPN;
                    lbl_tot.Text = formattedTotal;
                }
            }
            finally
            {
                conn.Close();
            }
        }

        private void InitializeDGV()
        {
            dgv_listitem.Columns.Add("kodebrg", "Kode Barang");
            dgv_listitem.Columns.Add("namabrg", "Nama Barang");
            dgv_listitem.Columns.Add("qty", "Kuantitas");
            dgv_listitem.Columns.Add("satuan", "Satuan");
            dgv_listitem.Columns.Add("harga", "Harga");
            dgv_listitem.Columns.Add("totalharga", "Total Harga");

            // Set DataGridView properties
            dgv_listitem.AutoGenerateColumns = false;
            dgv_listitem.AllowUserToAddRows = false;

            // Set DataPropertyName for each column
            dgv_listitem.Columns["kodebrg"].DataPropertyName = "kodebrg";
            dgv_listitem.Columns["namabrg"].DataPropertyName = "namabrg";
            dgv_listitem.Columns["qty"].DataPropertyName = "qty";
            dgv_listitem.Columns["satuan"].DataPropertyName = "satuan";
            dgv_listitem.Columns["harga"].DataPropertyName = "harga";
            dgv_listitem.Columns["totalharga"].DataPropertyName = "total";

            // Make columns non-editable
            dgv_listitem.Columns["kodebrg"].ReadOnly = true;
            dgv_listitem.Columns["namabrg"].ReadOnly = true;
            dgv_listitem.Columns["qty"].ReadOnly = true;
            dgv_listitem.Columns["satuan"].ReadOnly = true;
            dgv_listitem.Columns["harga"].ReadOnly = true;
            dgv_listitem.Columns["totalharga"].ReadOnly = true;

            dgv_listitem.DefaultCellStyle.ForeColor = Color.White;

            // Load data into the DataGridView
            LoadData(txt_notrx.Text);
        }

        public void LoadData(string trxId)
        {
            try
            {
                conn = new MySqlConnection(strconn);
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand("CALL GetTransactionDetails(@p_TRX_ID)", conn))
                {
                    cmd.Parameters.AddWithValue("@p_TRX_ID", trxId);
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dgv_listitem.Columns["kodebrg"].Visible = false;
                        dgv_listitem.DataSource = dt;
                        dgv_listitem.Refresh();
                    }
                }
            }
            finally
            {
                conn.Close();
            }
        }

        private void btn_simpantrx_Click(object sender, EventArgs e)
        {
            db.nais = false;
            invoiceinsert();
            simpantrx st = new simpantrx();
            st.fp = this;
            st.Dock = DockStyle.Fill;
            st.ShowDialog();
        }

        private void invoiceinsert()
        {
            Invoice_Pic ip = new Invoice_Pic();
            ip.trxid = newTransactionId;
            ip.InsertFormImageIntoDatabase();
        }

        private void Faktur_Penjualan_Load(object sender, EventArgs e)
        {
            loadidbaru();

            InitializeDGV();
        }
    }
}
