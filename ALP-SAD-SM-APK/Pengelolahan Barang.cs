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

namespace ALP_SAD_SM_APK
{
    public partial class Pengelolahan_Barang : Form
    {
        public Dashboard db;
        public Pengelolahan_Barang()
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

        string namaBarangValue = "";
        string kodebrg = "";
        string satuan = "";
        string harga = "";
        string newTransactionId = "";

        private void InitializeDGV()
        {
            dgv_listbrg.Columns.Add("Kode_Barang", "Kode Barang");
            dgv_listbrg.Columns.Add("Nama_Barang", "Nama Barang");
            dgv_listbrg.Columns.Add("Satuanbrg", "Satuan");
            dgv_listbrg.Columns.Add("Hargabrg", "Harga");

            // Set DataGridView properties
            dgv_listbrg.AutoGenerateColumns = false;
            dgv_listbrg.AllowUserToAddRows = false;

            // Set DataPropertyName for each column
            dgv_listbrg.Columns["Kode_Barang"].DataPropertyName = "Kode Barang";
            dgv_listbrg.Columns["Nama_Barang"].DataPropertyName = "Nama Barang";
            dgv_listbrg.Columns["Satuanbrg"].DataPropertyName = "Satuan";
            dgv_listbrg.Columns["Hargabrg"].DataPropertyName = "Harga";

            // Make columns non-editable
            dgv_listbrg.Columns["Kode_Barang"].ReadOnly = true;
            dgv_listbrg.Columns["Nama_Barang"].ReadOnly = true;
            dgv_listbrg.Columns["Satuanbrg"].ReadOnly = true;
            dgv_listbrg.Columns["Hargabrg"].ReadOnly = true;

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

                using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM db_sm.barang_view", conn))
                {
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dgv_listbrg.Columns["Kode_Barang"].Visible = false;
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

        private void dgv_listitem_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            dgv_listbrg.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(4, 119, 255);
        }

        private void Pengelolahan_Barang_Load(object sender, EventArgs e)
        {

        }

        private void btn_simpantrx_Click(object sender, EventArgs e)
        {
            updatebrg(kodebrg, txt_satuan.Text, txt_hrgbrg.Text);

            resetbtn();
            MessageBox.Show("Barang Berhasil Diperbaharui!", "Diperbaharui", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadData();
        }

        private void updatebrg(string kodebrg, string satuan, string hargabrg)
        {
            string query = $"CALL UpdateBarangSatuanHarga('{kodebrg}', '{satuan}', {hargabrg});";
            conn = new MySqlConnection(strconn);
            conn.Open();
            cmd = new MySqlCommand(query, conn);
            reader = cmd.ExecuteReader();
            conn.Close();
            LoadData();
        }

        private void dgv_listbrg_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0)
            {
                namaBarangValue = dgv_listbrg.Rows[e.RowIndex].Cells["Nama_Barang"].Value.ToString();
                kodebrg = dgv_listbrg.Rows[e.RowIndex].Cells["Kode_Barang"].Value.ToString();
                satuan = dgv_listbrg.Rows[e.RowIndex].Cells["Satuanbrg"].Value.ToString();
                harga = dgv_listbrg.Rows[e.RowIndex].Cells["Hargabrg"].Value.ToString();
            }
            txt_namabrg.Text = namaBarangValue;
            txt_satuan.Text = satuan;
            txt_hrgbrg.Text = harga;
            txt_satuan.ReadOnly = false;
            txt_hrgbrg.ReadOnly = false;
            btn_simpantrx.Visible = true;
            btn_add.Visible = false;
            btn_del.Visible = true;
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            txt_namabrg.ReadOnly = false;
            txt_satuan.ReadOnly = false;
            txt_hrgbrg.ReadOnly = false;
            btn_spn2.Visible = true;
            btn_add.Visible = false;
            btn_del.Visible = false;
        }

        private void btn_spn2_Click(object sender, EventArgs e)
        {
            kodebrg = GenerateNewBarangKode();
            string query = $"insert into barang values ('{kodebrg}', '{txt_namabrg.Text}', '{txt_satuan.Text}', {txt_hrgbrg.Text} , '1');";
            conn = new MySqlConnection(strconn);
            conn.Open();
            cmd = new MySqlCommand(query, conn);
            reader = cmd.ExecuteReader();
            conn.Close();

            resetbtn();
            MessageBox.Show("Barang Berhasil Dimasukkan!", "Berhasil", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadData();
        }

        private string GenerateNewBarangKode()
        {
            string newBarangKode = string.Empty;

            using (conn = new MySqlConnection(strconn))
            {
                conn.Open();

                using (cmd = new MySqlCommand("SELECT GenerateNewBarangKode() AS NewBarangKode", conn))
                {
                    newBarangKode = cmd.ExecuteScalar().ToString();
                }

                conn.Close();
            }
            return newBarangKode;
        }

        private void btn_del_Click(object sender, EventArgs e)
        {
            string query = $"update barang Set Barang_Stat = '0' Where Barang_Kode = '{kodebrg}'";
            conn = new MySqlConnection(strconn);
            conn.Open();
            cmd = new MySqlCommand(query, conn);
            reader = cmd.ExecuteReader();
            conn.Close();

            resetbtn();
            MessageBox.Show("Barang Berhasil Dihapus!","Terhapus",MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadData();
        }

        private void resetbtn()
        {
            txt_namabrg.Text = "";
            txt_satuan.Text = "";
            txt_hrgbrg.Text = "";
            kodebrg = "";
            txt_namabrg.ReadOnly = true;
            txt_satuan.ReadOnly = true;
            txt_hrgbrg.ReadOnly = true;
            btn_simpantrx.Visible = false;
            btn_spn2.Visible = false;
            btn_add.Visible = true;
            btn_del.Visible = false;
            btn_add.Visible = true;
        }

        private void txt_hrgbrg_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
            string currentText = txt_hrgbrg.Text;
            if (currentText.Length >= 6 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
