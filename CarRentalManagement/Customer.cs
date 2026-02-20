using projekt_1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace CarRentalManagement
{
    public partial class Customer : Form
    {
        public Customer()
        {
            InitializeComponent();
        }
        private void loadData()
        {
            string query = "select *From Customers;";
            DataTable dataTable = SqlHelper.ExecuteQurey(query);
            dataGridView1.DataSource = dataTable;
       

        }

        private void Customer_Load(object sender, EventArgs e)
        {
            
            this.customersTableAdapter.Fill(this.carDBDataSet.Customers);

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show(
                    "Please select a customer first.",
                    "No Selection",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
                return;
            }

            int customerId =
                (int)dataGridView1.SelectedRows[0].Cells[0].Value;

            DialogResult result = MessageBox.Show(
                "Are you sure you want to delete this customer?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result != DialogResult.Yes)
                return;

            string query =
                "DELETE FROM Customers WHERE CustomerID = @id";

            SqlParameter[] sqlParameters =
            {
        new SqlParameter("@id", SqlDbType.Int)
        {
            Value = customerId
        }
    };

            SqlHelper.ExecuteNonQuery(query, sqlParameters);

            MessageBox.Show(
                "Customer deleted successfully.",
                "Delete Completed",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );

            loadData();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show(
                    "Please select a customer.",
                    "No Selection",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
                return;
            }
           

            string query =
            @"UPDATE Customers 
      SET CustomerName = @name,
          Email = @email,
          Phone = @phone,
          Address = @address
      WHERE CustomerID = @id";

            SqlParameter[] sqlParameters =
            {
        new SqlParameter("@id", (int)dataGridView1.SelectedRows[0].Cells[0].Value),
        new SqlParameter("@name",txtCustomerName.Text),
        new SqlParameter("@email",txtEmail.Text),
        new SqlParameter("@phone", txtPhone.Text),
        new SqlParameter("@address",txtAddress.Text),
    };

            SqlHelper.ExecuteNonQuery(query, sqlParameters);

            MessageBox.Show(
                "Customer updated successfully.",
                "Update Completed",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
            lblId.Text = "";
            txtCustomerName.Text = "";
            txtEmail.Text = "";
            txtPhone.Text = "";
            txtAddress.Text = "";

            loadData();
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

            string query =
            @"INSERT INTO Customers (CustomerName, Email, Phone, Address)
            VALUES (@name, @email, @phone, @address)";

            SqlParameter[] sqlParameters =
            {

        new SqlParameter("@name", txtCustomerName.Text),
        new SqlParameter("@email", txtEmail.Text),
        new SqlParameter("@phone", txtPhone.Text),
        new SqlParameter("@address", txtAddress.Text),
    };

            SqlHelper.ExecuteNonQuery(query, sqlParameters);

            MessageBox.Show(
                "New customer has been registered successfully.",
                "Insert Completed",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
            txtCustomerName.Text = "";
            txtEmail.Text = "";
            txtPhone.Text = "";
            txtAddress.Text = "";
            loadData();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
            lblId.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            txtCustomerName.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            txtEmail.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            txtPhone.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            txtAddress.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();

        }
    }
}

