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
    public partial class AddTrx : Form
    {
        public AddTrx()
        {
            InitializeComponent();
            InitializeDGV();
        }
        public Faktur_Penjualan fp;
        string strconn = "server=localhost;uid=root;pwd=Minato2004-05-05;database=db_sm";
        string query;

        MySqlConnection conn;
        MySqlCommand cmd;
        MySqlDataAdapter adapter;
        MySqlDataReader reader;

        DataTable dt;

        public string trxid;
        string kodebrg = "";


        private void dgv_listbrg_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            dgv_listbrg.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(4, 119, 255);
        }

        private void AddTrx_Load(object sender, EventArgs e)
        {
            fp.db.Enabled = false;
            fp.db.overlayPanel.Visible = true;
            fp.db.overlayPanel.BringToFront();
        }

        private void btn_dlt_Click(object sender, EventArgs e)
        {
            fp.db.Enabled = true;
            fp.db.overlayPanel.Visible = false;
            fp.LoadData(trxid);
            this.Close();
        }

        private void btnMin_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txt_qty.Text, out int currentQty))
            {
                currentQty = Math.Max(0, currentQty - 1);
                txt_qty.Text = currentQty.ToString();
            }
        }

        private void btnPlus_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txt_qty.Text, out int currentQty))
            {
                currentQty++;
                txt_qty.Text = currentQty.ToString();
            }
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            if (txt_namabrg.Text != "")
            {
                addingitems(kodebrg, txt_qty.Text);
                txt_namabrg.Text = "";
                txt_qty.Text = "";
                MessageBox.Show("Barang Berhasil Ditambahkan!","Berhasil",MessageBoxButtons.OK, MessageBoxIcon.Information);
                fp.calculation(trxid);
            }
            else
            {
                MessageBox.Show("Barang Tidak Valid!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

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
            string namaBarangValue = "";
            if (e.ColumnIndex >= 0)
            {
                namaBarangValue = dgv_listbrg.Rows[e.RowIndex].Cells["Nama_Barang"].Value.ToString();
                kodebrg = dgv_listbrg.Rows[e.RowIndex].Cells["Kode_Barang"].Value.ToString();
            }

            txt_namabrg.Text = namaBarangValue;
            txt_qty.Text = "1";
        }

        private void addingitems(string kodebrg, string qty)
        {
            string query = $"CALL InsertIntoUseTrx('{trxid}', '{kodebrg}', '{qty}');";
            conn = new MySqlConnection(strconn);
            conn.Open();
            cmd = new MySqlCommand(query, conn);
            reader = cmd.ExecuteReader();
            conn.Close();
        }

        private void txt_qty_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
