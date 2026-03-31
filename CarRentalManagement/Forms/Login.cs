using System;
using System.Linq;
using System.Windows.Forms;

namespace CarRentalManagement
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            this.AcceptButton = btnLogin; 
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show(
                    "Username and password cannot be empty.",
                    "Warning",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            using (var db = new CarRentalEntities())
            {
                var user = db.Users
                    .FirstOrDefault(u => u.Username == username && u.Password == password);

                if (user != null)
                {
                    MessageBox.Show(
                        "Login successful.",
                        "Success",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    Main main = new Main();
                    this.Hide();
                    main.ShowDialog();
                    this.Close();
                }
                else
                {
                    txtUsername.Clear();
                    txtPassword.Clear();
                    txtUsername.Focus();

                    MessageBox.Show(
                        "Username or password is incorrect.",
                        "Login Failed",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                }
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }
    }
}