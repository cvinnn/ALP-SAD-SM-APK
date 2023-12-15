using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
    public partial class Invoice_Pic : Form
    {
        public riwayat_transaksi rt;
        public Invoice_Pic()
        {
            InitializeComponent();
        }
        string strconn = "server=localhost;uid=root;pwd=Minato2004-05-05;database=db_sm";
        string query;
        public string trxid;

        MySqlConnection conn;
        MySqlCommand cmd;
        MySqlDataAdapter adapter;
        MySqlDataReader reader;

        public void InitializeDGV()
        {
            dgv_listbrg.Columns.Add("tgl", "Tanggal");
            dgv_listbrg.Columns.Add("namabrg", "Nama Barang");
            dgv_listbrg.Columns.Add("qty", "Kuantitas");
            dgv_listbrg.Columns.Add("satuan", "Satuan");
            dgv_listbrg.Columns.Add("hrg", "Harga");
            dgv_listbrg.Columns.Add("total", "Total");

            // Set DataGridView properties
            dgv_listbrg.AutoGenerateColumns = false;
            dgv_listbrg.AllowUserToAddRows = false;

            // Set DataPropertyName for each column
            dgv_listbrg.Columns["tgl"].DataPropertyName = "tgl";
            dgv_listbrg.Columns["namabrg"].DataPropertyName = "namabrg";
            dgv_listbrg.Columns["qty"].DataPropertyName = "qty";
            dgv_listbrg.Columns["satuan"].DataPropertyName = "satuan";
            dgv_listbrg.Columns["hrg"].DataPropertyName = "harga";
            dgv_listbrg.Columns["total"].DataPropertyName = "total";

            // Make columns non-editable
            dgv_listbrg.Columns["tgl"].Visible = false;
            dgv_listbrg.Columns["namabrg"].ReadOnly = true;
            dgv_listbrg.Columns["qty"].ReadOnly = true;
            dgv_listbrg.Columns["satuan"].ReadOnly = true;
            dgv_listbrg.Columns["hrg"].ReadOnly = true;
            dgv_listbrg.Columns["total"].ReadOnly = true;

            // Set text color to white for all columns
            dgv_listbrg.DefaultCellStyle.ForeColor = Color.Black;

            // Load data into the DataGridView
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                conn = new MySqlConnection(strconn);
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand($"call db_sm.GetTransactionDetails('{trxid}');", conn))
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

        private void Invoice_Pic_Load(object sender, EventArgs e)
        {
            rt.db.Enabled = false;
            rt.db.overlayPanel.Visible = true;
            rt.db.overlayPanel.BringToFront();
        }

        public void labeldataload()
        {
            string query = $"select TRX_Date from transaksi where TRX_ID = '{trxid}';";
            conn = new MySqlConnection(strconn);
            conn.Open();
            cmd = new MySqlCommand(query, conn);
            object result = cmd.ExecuteScalar();
            conn.Close();

            lbl_datep.Text = Convert.ToDateTime(result).ToString("yyyy-MM-dd");
            lbl_notrxp.Text = trxid;

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

        private Bitmap CaptureForm()
        {
            // Create a bitmap with the same size as the form
            Bitmap bitmap = new Bitmap(this.Width, this.Height);

            // Capture the form into the bitmap
            this.DrawToBitmap(bitmap, new Rectangle(0, 0, this.Width, this.Height));

            return bitmap;
        }

        private byte[] ImageToByteArray(Image image)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return ms.ToArray();
            }
        }

        public void InsertFormImageIntoDatabase()
        {
            // Capture the form as an image
            Bitmap capturedImage = CaptureForm();

            // Convert the image to a byte array
            byte[] imageBytes = ImageToByteArray(capturedImage);

            MemoryStream ms = new MemoryStream(imageBytes);
            bm = new Bitmap(ms);

            string query = $"insert into nota values ('{trxid}', '{imageBytes}');";
            conn = new MySqlConnection(strconn);
            conn.Open();
            cmd = new MySqlCommand(query, conn);
            reader = cmd.ExecuteReader();
            conn.Close();
        }

        Bitmap bm;

        private void print(Panel pnl)
        {
            PrinterSettings ps = new PrinterSettings();
            getprintarea(pnl);
            printPreviewDialog1.ShowDialog();
            rt.db.Enabled = true;
            rt.db.overlayPanel.Visible = false;
            this.Close();
        }

        private void getprintarea(Panel pnl)
        {
            bm = new Bitmap(pnl.Width, pnl.Height);
            pnl.DrawToBitmap(bm, new Rectangle(0, 0, pnl.Width, pnl.Height));
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            guna2Button1.Visible = false;
            Rectangle area = e.PageBounds;
            /*e.Graphics.DrawImage(bm, (area.Width/2)-(this.guna2Panel1.Width/2), this.guna2Panel1.Location.Y);*/
            e.Graphics.DrawImage(bm, area.Left, area.Top);
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            guna2Button1.Visible = false;
            print(guna2Panel1);
        }

        private void btn_view_Click(object sender, EventArgs e)
        {
            rt.db.Enabled = true;
            rt.db.overlayPanel.Visible = false;
            this.Close();
        }

    }
}
