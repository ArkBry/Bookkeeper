using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace Bookkeeper
{
    public partial class LogOnForm : Form
    {
        public LogOnForm()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            if (tbxPassword.UseSystemPasswordChar == true)
            {
                tbxPassword.UseSystemPasswordChar = false;
                btnPreview.Image = Image.FromFile(@"C:\Users\Arkadiusz\source\repos\Bookkeeper\Bookkeeper\img\eyeclosesmall.png");
            }
            else
            {
                tbxPassword.UseSystemPasswordChar = true;
                btnPreview.Image = Image.FromFile(@"C:\Users\Arkadiusz\source\repos\Bookkeeper\Bookkeeper\img\eyeopensmall.png");
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=DESKTOP-UI9D9F2;Initial Catalog=Bookkeeper;Integrated Security=True";

            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string sql = "SELECT PASSWORD FROM SYS_USR_USERS WHERE NAME=@name";

            SqlCommand command = new SqlCommand(sql, connection);
            SqlParameter param = new SqlParameter();
            param.ParameterName = "@name";
            param.Value = tbxLogin.Text;
            command.Parameters.Add(param);
            SqlDataReader reader = command.ExecuteReader();

            string PasswordfromSQL = "";

            while (reader.Read())
            {
                PasswordfromSQL = reader.GetString(0);
            }

            SHA256 sha256 = SHA256.Create();
            byte[] hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(tbxPassword.Text));
            string PaswordFromUser = Convert.ToBase64String(hash);     

            if (tbxLogin.Text == "")
            {
                MessageBox.Show("Username can not be empty");
            }
            else if (PaswordFromUser.Equals(PasswordfromSQL))
            {
                Program.validLogin = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Incorrect username or password");
            }

            reader.Close();
            connection.Close();
        }

        private void linkChangeDataSource_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LogonSourceForm sourceForm = new LogonSourceForm();
            sourceForm.ShowDialog();
        }
    }
}
