using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;
using MySql.Data.MySqlClient;
using System.Web.UI.WebControls;

namespace ALP_SAD_SM_APK
{
    public partial class Login : Form
    {
        public Form1 prt;
        public Login()
        {
            InitializeComponent();
            checkLogin();
            this.Dock = DockStyle.Fill;
            this.TopLevel = false;
            this.AcceptButton = btn_signin;
        }

        string strconn = "server=localhost;uid=root;pwd=Minato2004-05-05;database=db_sm";
        string query;

        MySqlConnection conn;
        MySqlCommand cmd;
        MySqlDataAdapter adapter;
        MySqlDataReader reader;

        DataTable dt;

        public List<string> listID;
        public List<int> listPIN;

        public void checkLogin()
        {
            listID = new List<string>();
            listPIN = new List<int>();

            using (conn = new MySqlConnection(strconn))
            {
                conn.Open();
                query = "SELECT * FROM db_sm.user_login;";

                using (cmd = new MySqlCommand(query, conn))
                using (adapter = new MySqlDataAdapter(cmd))
                {
                    dt = new DataTable();
                    adapter.Fill(dt);

                    foreach (DataRow row in dt.Rows)
                    {
                        string id = row["Admin_ID"].ToString();
                        int pin = (int)row["Admin_PIN"];

                        listID.Add(id);
                        listPIN.Add(pin);
                    }
                }
                conn.Close();
            }
        }

        private void txt_pin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
            string currentText = txt_pin.Text;
            if (currentText.Length >= 6 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
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

        private void btn_signin_Click(object sender, EventArgs e)
        {
            string usrnm = txt_usrnm.Text;
            string pin = txt_pin.Text;
            bool un = false;
            bool pi = false;

            /*if (usrnm == "Username" || pin == "Pin")
            {
                MessageBox.Show("Username atau Pin tidak boleh kosong! Silahkan coba lagi.", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                foreach (string id in listID)
                {
                    if (id == usrnm)
                    {
                        un = true;
                        break;
                    }
                    else
                    {
                        un = false;
                    }
                }
                foreach (int pn in listPIN)
                {
                    if (pn == Convert.ToInt32(pin))
                    {
                        pi = true;
                        break;
                    }
                    else
                    {
                        pi = false;
                    }
                }

                if (un == true && pi == true)
                {
                    MessageBox.Show("Berhasil! Anda akan kami alihkan ke menu utama", "Success", MessageBoxButtons.OK, MessageBoxIcon.None);
                    Dashboard db = new Dashboard();
                    db.TopLevel = false;
                    db.Parent = prt;
                    db.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Username atau Pin yang anda masukkan salah! silahkan coba lagi", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }*/
            Dashboard db = new Dashboard();
            db.TopLevel = false;
            db.Parent = prt;
            db.Show();
            this.Close();
        }

        private void btn_signin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btn_signin_Click(sender, e);
            }
        }
    }
}
