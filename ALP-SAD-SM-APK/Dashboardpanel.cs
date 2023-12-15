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

namespace ALP_SAD_SM_APK
{
    public partial class Dashboardpanel : Form
    {
        public Dashboardpanel()
        {
            InitializeComponent();
            load_data();
        }
        string strconn = "server=localhost;uid=root;pwd=Minato2004-05-05;database=db_sm";
        string query;

        MySqlConnection conn;
        MySqlCommand cmd;
        MySqlDataAdapter adapter;
        MySqlDataReader reader;

        DataTable dt;

        public Dashboard db;
        private void load_data()
        {
            // Daily Summary
            using (conn = new MySqlConnection(strconn))
            {
                conn.Open();
                query = "SELECT * FROM db_sm.dailysummary;";

                using (cmd = new MySqlCommand(query, conn))
                using (adapter = new MySqlDataAdapter(cmd))
                {
                    dt = new DataTable();
                    adapter.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        DataRow row = dt.Rows[0]; // Assuming you want the first row

                        lbl_jph.Text = row["Total_Transactions"].ToString();
                        int totalAmount = Convert.ToInt32(row["Total_Amount"]);
                        string formattedAmount = $"Rp{totalAmount:#,##0}";
                        lbl_tph.Text = formattedAmount;

                    }
                    else
                    {
                        lbl_jph.Text = "0";
                        lbl_tph.Text = "Rp0";
                    }
                }
                conn.Close();
            }

            // Monthly Summary
            using (conn = new MySqlConnection(strconn))
            {
                conn.Open();
                query = "SELECT * FROM db_sm.monthlysummary;";

                using (cmd = new MySqlCommand(query, conn))
                using (adapter = new MySqlDataAdapter(cmd))
                {
                    dt = new DataTable();
                    adapter.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        DataRow row = dt.Rows[0]; // Assuming you want the first row

                        lbl_bjp.Text = row["Total_Transactions"].ToString();
                        int totalAmount = Convert.ToInt32(row["Total_Amount"]);
                        string formattedAmount = $"Rp{totalAmount:#,##0}";
                        lbl_btp.Text = formattedAmount;
                    }
                    else
                    {
                        lbl_bjp.Text = "0";
                        lbl_btp.Text = "Rp0";
                    }
                }
                conn.Close();
            }

            // Yearly Summary
            using (conn = new MySqlConnection(strconn))
            {
                conn.Open();
                query = "SELECT * FROM db_sm.yearlysummary;";

                using (cmd = new MySqlCommand(query, conn))
                using (adapter = new MySqlDataAdapter(cmd))
                {
                    dt = new DataTable();
                    adapter.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        DataRow row = dt.Rows[0]; // Assuming you want the first row

                        lbl_tjp.Text = row["Total_Transactions"].ToString();
                        int totalAmountInt = Convert.ToInt32(row["Total_Amount"]);
                        string formattedAmount = $"Rp{totalAmountInt:#,##0}";
                        lbl_ttp.Text = formattedAmount;

                    }
                    else
                    {
                        lbl_tjp.Text = "0";
                        lbl_ttp.Text = "Rp0";
                    }
                }
                conn.Close();
            }

            // Total Summary
            using (conn = new MySqlConnection(strconn))
            {
                conn.Open();
                query = "SELECT * FROM db_sm.alltransactionssummary;";

                using (cmd = new MySqlCommand(query, conn))
                using (adapter = new MySqlDataAdapter(cmd))
                {
                    dt = new DataTable();
                    adapter.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        DataRow row = dt.Rows[0]; // Assuming you want the first row

                        lbl_kjp.Text = row["Total_Transactions"].ToString(); 
                        int totalAmount = Convert.ToInt32(row["Total_Amount"]);
                        string formattedAmount = $"Rp{totalAmount:#,##0}";
                        lbl_ktp.Text = formattedAmount;
                    }
                    else
                    {
                        lbl_kjp.Text = "0";
                        lbl_ktp.Text = "Rp0";
                    }
                }
                conn.Close();
            }

            // Avail Product
            using (conn = new MySqlConnection(strconn))
            {
                conn.Open();
                query = "SELECT COUNT(*) AS Total_Item FROM db_sm.barang_view;";

                using (cmd = new MySqlCommand(query, conn))
                using (adapter = new MySqlDataAdapter(cmd))
                {
                    dt = new DataTable();
                    adapter.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        DataRow row = dt.Rows[0]; // Assuming you want the first row

                        lbl_pyt.Text = row["Total_Item"].ToString();
                    }
                    else
                    {
                        lbl_pyt.Text = "0";
                    }
                }
                conn.Close();
            }

            // Sold Product
            using (conn = new MySqlConnection(strconn))
            {
                conn.Open();
                query = "SELECT * FROM db_sm.totalsoldquantity;";

                using (cmd = new MySqlCommand(query, conn))
                using (adapter = new MySqlDataAdapter(cmd))
                {
                    dt = new DataTable();
                    adapter.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        DataRow row = dt.Rows[0]; // Assuming you want the first row

                        lbl_tpt.Text = row["Total_Sold_Quantity"].ToString();
                    }
                    else
                    {
                        lbl_tpt.Text = "0";
                    }
                }
                conn.Close();
            }
        }
    }
}
