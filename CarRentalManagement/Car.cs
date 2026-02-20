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

namespace CarRentalManagement
{
    public partial class Car : Form
    {
        public Car()
        {
            InitializeComponent();
        }
        private void loadData()
        {
            string query = "select *From Cars;";
            DataTable dataTable = SqlHelper.ExecuteQurey(query);
            dataGridView1.DataSource = dataTable;


        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Car_Load(object sender, EventArgs e)
        {
            
            this.carsTableAdapter.Fill(this.carDBDataSet1.Cars);

        }

        private void lblStatus_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCarName.Text))
            {
                MessageBox.Show(
                    "Car name is required.",
                    "Validation Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            string query =
            @"INSERT INTO Cars (CarName,Model,PlateNumber,Color,DailyRate,Status )
            VALUES (@carname,@model,@platenumber,@color,@dailyrate,@status)";

            SqlParameter[] sqlParameters =
            {

        new SqlParameter("@Carname",txtCarName.Text),
        new SqlParameter("@Model",txtModel.Text),
        new SqlParameter("@platenumber",txtplateNumber.Text),
        new SqlParameter("@color",txtColor.Text),
        new SqlParameter("@dailyrate",txtDailyrate.Text),
        new SqlParameter("@status",txtStatus.Text),
    };

            SqlHelper.ExecuteNonQuery(query, sqlParameters);

            MessageBox.Show(
                "New Car has been registered successfully.",
                "Insert Completed",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
            txtCarName.Text = "";
            txtModel.Text = "";
            txtplateNumber.Text = "";
            txtColor.Text = "";
            txtDailyrate.Text = "";
            txtStatus.Text = "";
            loadData();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show(
                    "Please select a Car first.",
                    "No Selection",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
                return;
            }

            int carID =
                (int)dataGridView1.SelectedRows[0].Cells[0].Value;

            DialogResult result = MessageBox.Show(
                "Are you sure you want to delete this Car?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result != DialogResult.Yes)
                return;

            string query =
                "DELETE FROM Cars WHERE CarID = @id";

            SqlParameter[] sqlParameters =
            {
        new SqlParameter("@id", SqlDbType.Int)
        {
            Value = carID
        }
    };

            SqlHelper.ExecuteNonQuery(query, sqlParameters);

            MessageBox.Show(
                "Car deleted successfully.",
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
                    "Please select a Car.",
                    "No Selection",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
                return;
            }


            string query =
            @"UPDATE Cars  SET CarName = @carname,Model = @model,
            PlateNumber = @plateNumber,Color = @color,
            DailyRate=@dailyrate,Status=@status
            WHERE CarID = @id";

            SqlParameter[] sqlParameters =
            {
        new SqlParameter("@id", (int)dataGridView1.SelectedRows[0].Cells[0].Value),
        new SqlParameter("@carname",txtCarName.Text),
        new SqlParameter("@model",txtModel.Text),
        new SqlParameter("@plateNumber",txtplateNumber.Text),
        new SqlParameter("@color",txtColor.Text),
        new SqlParameter("@dailyrate",txtDailyrate.Text),
        new SqlParameter("@status",txtStatus.Text),
    };

            SqlHelper.ExecuteNonQuery(query, sqlParameters);

            MessageBox.Show(
                "Car updated successfully.",
                "Update Completed",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
            lblcar.Text = "";
            txtCarName.Text = "";
            txtModel.Text = "";
            txtplateNumber.Text = "";
            txtColor.Text = "";
            txtDailyrate.Text = "";
            txtStatus.Text = "";

            loadData();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            lblcar.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            txtCarName.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            txtModel.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            txtplateNumber.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            txtColor.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            txtDailyrate.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
            txtStatus.Text = dataGridView1.SelectedRows[0].Cells[6].Value.ToString();
        }
    }
}

