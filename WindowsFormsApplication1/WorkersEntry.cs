using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class WorkerEntryForm : Form
    {
        private SqlConnection conn = new SqlConnection(Properties.Settings.Default.connetion);
        public int WorkerId { get; set; } = 0;
        public bool IsEditMode { get; set; } = false;

        public WorkerEntryForm()
        {
            InitializeComponent();
        }

        private void WorkerEntryForm_Load(object sender, EventArgs e)
        {
            if (IsEditMode)
            {
                this.Text = "Edit Worker";
                LoadWorkerData();
            }
            else
            {
                this.Text = "Add New Worker";
                ClearFields();
            }
        }

        private void LoadWorkerData()
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM workers WHERE worker_id=@id", conn);
                cmd.Parameters.AddWithValue("@id", WorkerId);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        txtWorkerName.Text = reader["worker_name"].ToString();
                        txtPhoneNumber.Text = reader["phone_number"].ToString();
                        txtAccountNumber.Text = reader["account_number"].ToString();
                        txtPaymentAmount.Text = reader["payment_amount"].ToString();
                        dtDateJoined.Value = Convert.ToDateTime(reader["date_joined"]);
                        cbPaymentStatus.Text = reader["payment_status"].ToString();
                        txtAmountPaid.Text = reader["amount_paid"].ToString();
                        txtAmountRemaining.Text = reader["amount_remaining"].ToString();

                        if (reader["last_paid_date"] != DBNull.Value)
                        {
                            dtLastPaidDate.Value = Convert.ToDateTime(reader["last_paid_date"]);
                        }
                    }
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading worker data: " + ex.Message);
                conn.Close();
            }
        }

        private void ClearFields()
        {
            txtWorkerName.Text = "";
            txtPhoneNumber.Text = "";
            txtAccountNumber.Text = "";
            txtPaymentAmount.Text = "0";
            dtDateJoined.Value = DateTime.Now;
            cbPaymentStatus.SelectedIndex = 1; // Unpaid
            txtAmountPaid.Text = "0";
            txtAmountRemaining.Text = "0";
            dtLastPaidDate.Value = DateTime.Now;
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

                if (IsEditMode)
                {
                    cmd = new SqlCommand(
                        "UPDATE workers SET worker_name=@name, phone_number=@phone, account_number=@account, " +
                        "payment_amount=@amount, date_joined=@dateJoined, payment_status=@status, " +
                        "amount_paid=@paid, amount_remaining=@remaining, last_paid_date=@lastPaid " +
                        "WHERE worker_id=@id", conn);
                    cmd.Parameters.AddWithValue("@id", WorkerId);
                }
                else
                {
                    cmd = new SqlCommand(
                        "INSERT INTO workers (worker_name, phone_number, account_number, payment_amount, date_joined, " +
                        "payment_status, amount_paid, amount_remaining, last_paid_date) " +
                        "VALUES (@name, @phone, @account, @amount, @dateJoined, @status, @paid, @remaining, @lastPaid)", conn);
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

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving worker: " + ex.Message);
                conn.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
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
    }
}