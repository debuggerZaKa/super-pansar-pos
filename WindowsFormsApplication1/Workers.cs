using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class WorkersForm : Form
    {
        SqlConnection conn = new SqlConnection(Properties.Settings.Default.connetion);
        private int workerId = 0;

        public WorkersForm()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.Top;
        }

        private void WorkersForm_Load(object sender, EventArgs e)
        {
            // Set the font for the entire form
            this.Font = new Font("Segoe UI", 10.2f, FontStyle.Regular, GraphicsUnit.Point);

            LoadWorkers();
            ClearFields();

            ConfigureDataGridView();
            CenterPictureBox();
        }

        private void ConfigureDataGridView()
        {
            // Set font for headers and cells
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10.2f, FontStyle.Bold);
            dataGridView1.DefaultCellStyle.Font = new Font("Segoe UI", 9.5f);

            // Enable row headers and set their style
            dataGridView1.RowHeadersVisible = false;

            // Set selection style
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.LightBlue;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.Black;

            // Enable alternating row colors
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke;

            // Set grid lines and border
            dataGridView1.GridColor = SystemColors.ControlLight;
            dataGridView1.BorderStyle = BorderStyle.Fixed3D;

            // Enable auto-size columns
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void LoadWorkers()
        {
            try
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM workers", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                // Bind data to grid
                dataGridView1.DataSource = dt;

                // Set human-readable column names
                if (dataGridView1.Columns.Count > 0)
                {
                    dataGridView1.Columns["worker_id"].HeaderText = "ID"; // Show ID column
                    dataGridView1.Columns["worker_id"].DisplayIndex = 0; // Move ID to first position

                    dataGridView1.Columns["worker_name"].HeaderText = "Name";
                    dataGridView1.Columns["phone_number"].HeaderText = "Phone No.";
                    dataGridView1.Columns["account_number"].HeaderText = "Acc No.";
                    dataGridView1.Columns["payment_amount"].HeaderText = "Payment";
                    dataGridView1.Columns["date_joined"].HeaderText = "Joined On";
                    dataGridView1.Columns["payment_status"].HeaderText = "Payment Status";
                    dataGridView1.Columns["amount_paid"].HeaderText = "Paid";
                    dataGridView1.Columns["amount_remaining"].HeaderText = "Remaining";
                    dataGridView1.Columns["last_paid_date"].HeaderText = "Last Paid";

                    // Format numeric columns
                    dataGridView1.Columns["payment_amount"].DefaultCellStyle.Format = "N2";
                    dataGridView1.Columns["amount_paid"].DefaultCellStyle.Format = "N2";
                    dataGridView1.Columns["amount_remaining"].DefaultCellStyle.Format = "N2";
                    dataGridView1.Columns["payment_amount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dataGridView1.Columns["amount_paid"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dataGridView1.Columns["amount_remaining"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                    // Format date columns
                    dataGridView1.Columns["date_joined"].DefaultCellStyle.Format = "d";
                    dataGridView1.Columns["last_paid_date"].DefaultCellStyle.Format = "d";

                    // Set column widths
                    dataGridView1.Columns["worker_id"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    dataGridView1.Columns["worker_name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    dataGridView1.Columns["phone_number"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    dataGridView1.Columns["account_number"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }

                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading workers: " + ex.Message);
                conn.Close();
            }
        }

        private void ClearFields()
        {
            try
            {
                // Disable events that might interfere
                cbPaymentStatus.SelectedIndexChanged -= cbPaymentStatus_SelectedIndexChanged;

                workerId = 0;
                txtWorkerName.Clear();
                txtPhoneNumber.Clear();
                txtAccountNumber.Clear();
                txtPaymentAmount.Text = "0";
                dtDateJoined.Value = DateTime.Now;
                cbPaymentStatus.SelectedIndex = 1; // "Unpaid"
                txtAmountPaid.Text = "0";
                txtAmountRemaining.Text = "0";
                dtLastPaidDate.Value = DateTime.Now;
                headerLabel.Text = "Create New Worker";

                // Clear DataGridView selection
                if (dataGridView1.SelectedRows.Count > 0)
                    dataGridView1.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error clearing fields: " + ex.Message);
            }
            finally
            {
                // Re-enable events
                cbPaymentStatus.SelectedIndexChanged += cbPaymentStatus_SelectedIndexChanged;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtWorkerName.Text))
            {
                MessageBox.Show("Please enter worker name");
                return;
            }

            try
            {
                conn.Open();
                SqlCommand cmd;

                if (workerId == 0) // Insert new
                {
                    cmd = new SqlCommand(
                        "INSERT INTO workers (worker_name, phone_number, account_number, payment_amount, date_joined, " +
                        "payment_status, amount_paid, amount_remaining, last_paid_date) " +
                        "VALUES (@name, @phone, @account, @amount, @dateJoined, @status, @paid, @remaining, @lastPaid)", conn);
                }
                else // Update existing
                {
                    cmd = new SqlCommand(
                        "UPDATE workers SET worker_name=@name, phone_number=@phone, account_number=@account, " +
                        "payment_amount=@amount, date_joined=@dateJoined, payment_status=@status, " +
                        "amount_paid=@paid, amount_remaining=@remaining, last_paid_date=@lastPaid " +
                        "WHERE worker_id=@id", conn);
                    cmd.Parameters.AddWithValue("@id", workerId);
                }

                cmd.Parameters.AddWithValue("@name", txtWorkerName.Text);
                cmd.Parameters.AddWithValue("@phone", txtPhoneNumber.Text);
                cmd.Parameters.AddWithValue("@account", txtAccountNumber.Text);
                cmd.Parameters.AddWithValue("@amount", decimal.Parse(txtPaymentAmount.Text));
                cmd.Parameters.AddWithValue("@dateJoined", dtDateJoined.Value);
                cmd.Parameters.AddWithValue("@status", cbPaymentStatus.Text);
                cmd.Parameters.AddWithValue("@paid", decimal.Parse(txtAmountPaid.Text));
                cmd.Parameters.AddWithValue("@remaining", decimal.Parse(txtAmountRemaining.Text));
                cmd.Parameters.AddWithValue("@lastPaid", dtLastPaidDate.Value);

                cmd.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Worker saved successfully");
                LoadWorkers();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving worker: " + ex.Message);
                conn.Close();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a worker to edit");
                return;
            }

            DataGridViewRow row = dataGridView1.SelectedRows[0];
            workerId = Convert.ToInt32(row.Cells["worker_id"].Value);
            txtWorkerName.Text = row.Cells["worker_name"].Value.ToString();
            txtPhoneNumber.Text = row.Cells["phone_number"].Value.ToString();
            txtAccountNumber.Text = row.Cells["account_number"].Value.ToString();
            txtPaymentAmount.Text = row.Cells["payment_amount"].Value.ToString();
            dtDateJoined.Value = Convert.ToDateTime(row.Cells["date_joined"].Value);
            cbPaymentStatus.Text = row.Cells["payment_status"].Value.ToString();
            txtAmountPaid.Text = row.Cells["amount_paid"].Value.ToString();
            txtAmountRemaining.Text = row.Cells["amount_remaining"].Value.ToString();


            if (row.Cells["last_paid_date"].Value != DBNull.Value)
            {
                dtLastPaidDate.Value = Convert.ToDateTime(row.Cells["last_paid_date"].Value);
            }

            headerLabel.Text = "Edit Worker";
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a worker to delete");
                return;
            }

            int selectedWorkerId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["worker_id"].Value);

            if (MessageBox.Show("Are you sure you want to delete this worker?", "Confirm Delete",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM workers WHERE worker_id=@id", conn);
                    cmd.Parameters.AddWithValue("@id", selectedWorkerId);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Worker deleted successfully");
                    LoadWorkers();
                    ClearFields();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting worker: " + ex.Message);
                    conn.Close();
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(
                    "SELECT * FROM workers WHERE worker_name LIKE '%" + txtSearch.Text + "%'", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error searching workers: " + ex.Message);
                conn.Close();
            }
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            txtSearch_TextChanged(sender, e);
        }

        private void cbPaymentStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbPaymentStatus.Text == "Paid")
            {
                txtAmountPaid.Text = txtPaymentAmount.Text;
                txtAmountRemaining.Text = "0";
                dtLastPaidDate.Value = DateTime.Now;
            }
            else if (cbPaymentStatus.Text == "Unpaid")
            {
                txtAmountPaid.Text = "0";
                txtAmountRemaining.Text = txtPaymentAmount.Text;
            }
            else if (cbPaymentStatus.Text == "Partially Paid")
            {
                if (decimal.Parse(txtAmountPaid.Text) <= 0)
                {
                    txtAmountPaid.Text = "0";
                    txtAmountRemaining.Text = txtPaymentAmount.Text;
                }
            }
        }

        private void WorkerAccountManagementForm_Resize(object sender, EventArgs e)
        {
            CenterPictureBox();
            formPanel.PerformLayout();
        }


        private void CenterPictureBox()
        {
            if (pictureBox1 != null && formPanel != null)
            {
              
                int centerX = (formPanel.ClientSize.Width - pictureBox1.Width) / 2;
                centerX = Math.Max(centerX, 0);
                pictureBox1.Location = new Point(centerX, pictureBox1.Location.Y);
            }
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}