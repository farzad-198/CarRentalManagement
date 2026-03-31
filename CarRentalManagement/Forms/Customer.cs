using System;
using System.Linq;
using System.Windows.Forms;

namespace CarRentalManagement
{
    public partial class Customer : Form
    {
        public Customer()
        {
            InitializeComponent();
        }

        private void LoadData()
        {
            using (var db = new CarRentalEntities())
            {
                var customerList = db.Customers
                    .Select(c => new
                    {
                        c.CustomerID,
                        c.CustomerName,
                        c.Email,
                        c.Phone,
                        c.Address
                    })
                    .ToList();

                dataGridView1.DataSource = customerList;
            }
        }

        private void ClearFields()
        {
            lblId.Text = "";
            txtCustomerName.Text = "";
            txtEmail.Text = "";
            txtPhone.Text = "";
            txtAddress.Text = "";
        }

        private void Customer_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'carRentalDBDataSet.Customers' table. You can move, or remove it, as needed.
            this.customersTableAdapter1.Fill(this.carRentalDBDataSet.Customers);
            LoadData();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCustomerName.Text))
            {
                MessageBox.Show(
                    "Customer name is required.",
                    "Validation Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            using (var db = new CarRentalEntities())
            {
                Customers newCustomer = new Customers();
                newCustomer.CustomerName = txtCustomerName.Text.Trim();
                newCustomer.Email = txtEmail.Text.Trim();
                newCustomer.Phone = txtPhone.Text.Trim();
                newCustomer.Address = txtAddress.Text.Trim();

                db.Customers.Add(newCustomer);
                db.SaveChanges();
            }

            MessageBox.Show(
                "New customer has been registered successfully.",
                "Insert Completed",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );

            ClearFields();
            LoadData();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show(
                    "Please select a customer first.",
                    "No Selection",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            int customerId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);

            DialogResult result = MessageBox.Show(
                "Are you sure you want to delete this customer?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result != DialogResult.Yes)
            {
                return;
            }

            using (var db = new CarRentalEntities())
            {
                Customers customer = db.Customers.FirstOrDefault(c => c.CustomerID == customerId);

                if (customer == null)
                {
                    MessageBox.Show(
                        "Customer not found.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                    return;
                }

                db.Customers.Remove(customer);
                db.SaveChanges();
            }

            MessageBox.Show(
                "Customer deleted successfully.",
                "Delete Completed",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );

            ClearFields();
            LoadData();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show(
                    "Please select a customer first.",
                    "No Selection",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            if (string.IsNullOrWhiteSpace(txtCustomerName.Text))
            {
                MessageBox.Show(
                    "Customer name is required.",
                    "Validation Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            int customerId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);

            using (var db = new CarRentalEntities())
            {
                Customers customer = db.Customers.FirstOrDefault(c => c.CustomerID == customerId);

                if (customer == null)
                {
                    MessageBox.Show(
                        "Customer not found.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                    return;
                }

                customer.CustomerName = txtCustomerName.Text.Trim();
                customer.Email = txtEmail.Text.Trim();
                customer.Phone = txtPhone.Text.Trim();
                customer.Address = txtAddress.Text.Trim();

                db.SaveChanges();
            }

            MessageBox.Show(
                "Customer updated successfully.",
                "Update Completed",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );

            ClearFields();
            LoadData();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

            lblId.Text = row.Cells[0].Value?.ToString();
            txtCustomerName.Text = row.Cells[1].Value?.ToString();
            txtEmail.Text = row.Cells[2].Value?.ToString();
            txtPhone.Text = row.Cells[3].Value?.ToString();
            txtAddress.Text = row.Cells[4].Value?.ToString();
        }
    }
}