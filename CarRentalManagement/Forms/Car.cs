using System;
using System.Linq;
using System.Windows.Forms;

namespace CarRentalManagement
{
    public partial class Car : Form
    {
        public Car()
        {
            InitializeComponent();
        }

        private void LoadData()
        {
            using (var db = new CarRentalEntities())
            {
                var carList = db.Cars
                    .Select(c => new
                    {
                        c.CarID,
                        c.CarName,
                        c.Model,
                        c.PlateNumber,
                        c.Color,
                        c.DailyRate,
                        c.Status
                    })
                    .ToList();

                dataGridView1.DataSource = carList;
            }
        }

        private void ClearFields()
        {
            lblcar.Text = "";
            txtCarName.Text = "";
            txtModel.Text = "";
            txtplateNumber.Text = "";
            txtColor.Text = "";
            txtDailyrate.Text = "";
            txtStatus.Text = "";
        }

        private void Car_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCarName.Text) ||
                string.IsNullOrWhiteSpace(txtModel.Text) ||
                string.IsNullOrWhiteSpace(txtplateNumber.Text) ||
                string.IsNullOrWhiteSpace(txtDailyrate.Text) ||
                string.IsNullOrWhiteSpace(txtStatus.Text))
            {
                MessageBox.Show(
                    "Please fill all required fields.",
                    "Validation Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            if (!decimal.TryParse(txtDailyrate.Text.Trim(), out decimal dailyRate))
            {
                MessageBox.Show(
                    "Daily Rate must be a valid number.",
                    "Validation Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            using (var db = new CarRentalEntities())
            {
                bool plateExists = db.Cars.Any(c => c.PlateNumber == txtplateNumber.Text.Trim());

                if (plateExists)
                {
                    MessageBox.Show(
                        "Plate Number already exists.",
                        "Duplicate Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                Cars newCar = new Cars();
                newCar.CarName = txtCarName.Text.Trim();
                newCar.Model = txtModel.Text.Trim();
                newCar.PlateNumber = txtplateNumber.Text.Trim();
                newCar.Color = txtColor.Text.Trim();
                newCar.DailyRate = dailyRate;
                newCar.Status = txtStatus.Text.Trim();

                db.Cars.Add(newCar);
                db.SaveChanges();
            }

            MessageBox.Show(
                "New car has been added successfully.",
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
                    "Please select a car first.",
                    "No Selection",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            int carId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["CarID"].Value);

            DialogResult result = MessageBox.Show(
                "Are you sure you want to delete this car?",
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
                Cars car = db.Cars.FirstOrDefault(c => c.CarID == carId);

                if (car == null)
                {
                    MessageBox.Show(
                        "Car not found.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                    return;
                }

                db.Cars.Remove(car);
                db.SaveChanges();
            }

            MessageBox.Show(
                "Car deleted successfully.",
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
                    "Please select a car first.",
                    "No Selection",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            if (!decimal.TryParse(txtDailyrate.Text.Trim(), out decimal dailyRate))
            {
                MessageBox.Show(
                    "Daily Rate must be a valid number.",
                    "Validation Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            int carId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["CarID"].Value);

            using (var db = new CarRentalEntities())
            {
                Cars car = db.Cars.FirstOrDefault(c => c.CarID == carId);

                if (car == null)
                {
                    MessageBox.Show(
                        "Car not found.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                    return;
                }

                string newPlateNumber = txtplateNumber.Text.Trim();

                bool plateExists = db.Cars.Any(c => c.PlateNumber == newPlateNumber && c.CarID != carId);

                if (plateExists)
                {
                    MessageBox.Show(
                        "Another car already has this Plate Number.",
                        "Duplicate Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                car.CarName = txtCarName.Text.Trim();
                car.Model = txtModel.Text.Trim();
                car.PlateNumber = newPlateNumber;
                car.Color = txtColor.Text.Trim();
                car.DailyRate = dailyRate;
                car.Status = txtStatus.Text.Trim();

                db.SaveChanges();
            }

            MessageBox.Show(
                "Car updated successfully.",
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

            lblcar.Text = row.Cells["CarID"].Value?.ToString();
            txtCarName.Text = row.Cells["CarName"].Value?.ToString();
            txtModel.Text = row.Cells["Model"].Value?.ToString();
            txtplateNumber.Text = row.Cells["PlateNumber"].Value?.ToString();
            txtColor.Text = row.Cells["Color"].Value?.ToString();
            txtDailyrate.Text = row.Cells["DailyRate"].Value?.ToString();
            txtStatus.Text = row.Cells["Status"].Value?.ToString();
        }
    }
}