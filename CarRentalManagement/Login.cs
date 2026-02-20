using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using projekt_1;

namespace CarRentalManagement
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void txtUser_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            
            if (string.IsNullOrEmpty(txtUsername.Text) || string.IsNullOrEmpty(txtPassword.Text))
            {
                
                MessageBox.Show("Username and password cannot be empty.","warning",MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtUsername.Text=="Admin" && txtPassword.Text=="1234")
            {
               
               Main main = new Main();
                this.Hide();
                main.ShowDialog();
                main.Close();
            }

            string query = "select *from Customers Where username=@User and password=@pass; ";
            SqlParameter[] sqlParameters = 
            {
                new SqlParameter("@User", txtUsername.Text),
                new SqlParameter("@pass", txtPassword.Text)
            };
            DataTable table = SqlHelper.ExecuteQurey(query, sqlParameters);
            if (table.Rows.Count==0)
            {
                txtUsername.Text = "";
                txtPassword.Text = "";
                MessageBox.Show("UserName And Password Is Worng." ,"Warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);


            }
            else if (table.Rows.Count==1)
            {
                Customers customer = new Customers();
                customer.CustomerID = (int)table.Rows[0][0];
                customer.CustomerName=table.Rows[0][1].ToString();
                customer.Email=table.Rows[0][2].ToString();
                customer.Phone=table.Rows[0][3].ToString();
                customer.Address=table.Rows[0][4].ToString();



            }
           
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }
    }
}
