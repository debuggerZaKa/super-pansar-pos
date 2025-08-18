using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class MarketLedgerEntryForm : Form
    {
        private SqlConnection connection = new SqlConnection(Properties.Settings.Default.connetion);
        private bool isEditMode = false;
        private int recordId = -1;
        private bool allowClose = true;
        private bool suppressCalculation = false;

        public MarketLedgerEntryForm()
        {
            InitializeComponent();
            SetupForm();
            LoadWarehouses();
        }

        private void SetupForm()
        {
            // Set current date
            dtpTransactionDate.Value = DateTime.Now;

            // Initialize dropdowns
            cmbTransactionType.Items.AddRange(new string[] { "Bought", "Sold" });
            cmbPaymentStatus.Items.AddRange(new string[] { "Paid", "Unpaid", "Partial" });
            cmbPaymentMethod.Items.AddRange(new string[] { "Cash", "Cheque", "Bank Transfer", "Credit Card", "Other" });

            // Set default values
            cmbPaymentStatus.SelectedIndex = 1; // Unpaid
            txtAmountPaid.Text = "0";
            txtAmountRemaining.Text = "0";

            // Configure warehouse controls
            chkSendToWarehouse.CheckedChanged += ChkSendToWarehouse_CheckedChanged;
            cmbWarehouses.DropDownStyle = ComboBoxStyle.DropDownList;

            // Wire up events
            cmbPaymentStatus.SelectedIndexChanged += CmbPaymentStatus_SelectedIndexChanged;
            txtAmount.TextChanged += TxtAmount_TextChanged;
            txtAmountPaid.TextChanged += TxtAmountPaid_TextChanged;
        }

        private void LoadWarehouses()
        {
            try
            {
                connection.Open();
                string query = "SELECT WarehouseId, WarehouseName FROM Warehouses ORDER BY WarehouseName";
                SqlCommand cmd = new SqlCommand(query, connection);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                cmbWarehouses.DataSource = dt;
                cmbWarehouses.DisplayMember = "WarehouseName";
                cmbWarehouses.ValueMember = "WarehouseId";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading warehouses: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        private void ChkSendToWarehouse_CheckedChanged(object sender, EventArgs e)
        {
            cmbWarehouses.Enabled = chkSendToWarehouse.Checked;

            if (!chkSendToWarehouse.Checked)
            {
                cmbWarehouses.SelectedIndex = -1;
            }
        }

        public void SetFormData(string id, string companyName, string dealerName, string description,
                               string amount, string transactionType, DateTime transactionDate,
                               string paymentStatus, string amountPaid, string paymentMethod,
                               string referenceNumber, string notes, int? warehouseId)
        {
            suppressCalculation = true;

            isEditMode = true;
            recordId = int.Parse(id);

            // Set all form fields
            txtCompanyName.Text = companyName ?? "";
            txtDealerName.Text = dealerName ?? "";
            txtDescription.Text = description ?? "";
            txtAmount.Text = amount ?? "0";

            if (!string.IsNullOrEmpty(transactionType))
                cmbTransactionType.SelectedItem = transactionType;

            dtpTransactionDate.Value = transactionDate;

            if (!string.IsNullOrEmpty(paymentStatus))
                cmbPaymentStatus.SelectedItem = paymentStatus;

            txtAmountPaid.Text = amountPaid ?? "0";

            if (!string.IsNullOrEmpty(paymentMethod))
                cmbPaymentMethod.SelectedItem = paymentMethod;

            txtReferenceNumber.Text = referenceNumber ?? "";
            txtNotes.Text = notes ?? "";

            // Set warehouse info
            if (warehouseId.HasValue && warehouseId.Value > 0)
            {
                chkSendToWarehouse.Checked = true;
                cmbWarehouses.SelectedValue = warehouseId.Value;
            }

            // Update form title for edit mode
            this.Text = "Edit Market Ledger Entry";

            suppressCalculation = false;
            UpdatePaymentFields();
        }

        private void MarketLedgerEntryForm_Load(object sender, EventArgs e)
        {
            // Focus on first field
            txtCompanyName.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            try
            {
                connection.Open();

                string query;
                if (!isEditMode)
                {
                    query = @"INSERT INTO MarketLedger 
                            (CompanyName, DealerName, Description, Amount, 
                             TransactionType, TransactionDate, PaymentStatus, 
                             AmountPaid, AmountRemaining, PaymentMethod, 
                             ReferenceNumber, Notes, WarehouseId)
                            VALUES 
                            (@CompanyName, @DealerName, @Description, @Amount, 
                             @TransactionType, @TransactionDate, @PaymentStatus, 
                             @AmountPaid, @AmountRemaining, @PaymentMethod, 
                             @ReferenceNumber, @Notes, @WarehouseId)";
                }
                else
                {
                    query = @"UPDATE MarketLedger SET
                            CompanyName = @CompanyName,
                            DealerName = @DealerName,
                            Description = @Description,
                            Amount = @Amount,
                            TransactionType = @TransactionType,
                            TransactionDate = @TransactionDate,
                            PaymentStatus = @PaymentStatus,
                            AmountPaid = @AmountPaid,
                            AmountRemaining = @AmountRemaining,
                            PaymentMethod = @PaymentMethod,
                            ReferenceNumber = @ReferenceNumber,
                            Notes = @Notes,
                            WarehouseId = @WarehouseId
                            WHERE Id = @Id";
                }

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@CompanyName", txtCompanyName.Text.Trim());
                    cmd.Parameters.AddWithValue("@DealerName", txtDealerName.Text.Trim());
                    cmd.Parameters.AddWithValue("@Description", txtDescription.Text.Trim());
                    cmd.Parameters.AddWithValue("@Amount", decimal.Parse(txtAmount.Text));
                    cmd.Parameters.AddWithValue("@TransactionType", cmbTransactionType.SelectedItem);
                    cmd.Parameters.AddWithValue("@TransactionDate", dtpTransactionDate.Value);
                    cmd.Parameters.AddWithValue("@PaymentStatus", cmbPaymentStatus.SelectedItem);
                    cmd.Parameters.AddWithValue("@AmountPaid", decimal.Parse(txtAmountPaid.Text));
                    cmd.Parameters.AddWithValue("@AmountRemaining", decimal.Parse(txtAmountRemaining.Text));
                    cmd.Parameters.AddWithValue("@PaymentMethod", cmbPaymentMethod.SelectedItem?.ToString() ?? "");
                    cmd.Parameters.AddWithValue("@ReferenceNumber", txtReferenceNumber.Text.Trim());
                    cmd.Parameters.AddWithValue("@Notes", txtNotes.Text.Trim());

                    // Add warehouse parameter (NULL if not selected)
                    if (chkSendToWarehouse.Checked && cmbWarehouses.SelectedValue != null)
                    {
                        cmd.Parameters.AddWithValue("@WarehouseId", cmbWarehouses.SelectedValue);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@WarehouseId", DBNull.Value);
                    }

                    if (isEditMode)
                    {
                        cmd.Parameters.AddWithValue("@Id", recordId);
                    }

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Transaction saved successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    allowClose = true;
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving transaction: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            allowClose = true;
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void CmbPaymentStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdatePaymentFields();
        }

        private void UpdatePaymentFields()
        {
            if (suppressCalculation) return;

            decimal amount;
            if (!decimal.TryParse(txtAmount.Text, out amount))
            {
                amount = 0;
            }

            switch (cmbPaymentStatus.SelectedItem?.ToString())
            {
                case "Paid":
                    txtAmountPaid.Text = amount.ToString("0.00");
                    txtAmountRemaining.Text = "0.00";
                    txtAmountPaid.Enabled = false;
                    txtAmountRemaining.Enabled = false;
                    break;

                case "Unpaid":
                    txtAmountPaid.Text = "0.00";
                    txtAmountRemaining.Text = amount.ToString("0.00");
                    txtAmountPaid.Enabled = false;
                    txtAmountRemaining.Enabled = false;
                    break;

                case "Partial":
                    txtAmountPaid.Enabled = true;
                    txtAmountRemaining.Enabled = false;
                    if (string.IsNullOrEmpty(txtAmountPaid.Text) || txtAmountPaid.Text == "0.00")
                    {
                        txtAmountPaid.Text = "";
                    }
                    CalculateRemaining();
                    break;
            }
        }

        private void TxtAmount_TextChanged(object sender, EventArgs e)
        {
            CalculateRemaining();
        }

        private void TxtAmountPaid_TextChanged(object sender, EventArgs e)
        {
            CalculateRemaining();
        }

        private void CalculateRemaining()
        {
            if (suppressCalculation) return;
            if (cmbPaymentStatus.SelectedItem?.ToString() != "Partial") return;

            decimal amount, paid;
            if (decimal.TryParse(txtAmount.Text, out amount) &&
                decimal.TryParse(txtAmountPaid.Text, out paid))
            {
                decimal remaining = amount - paid;
                txtAmountRemaining.Text = remaining.ToString("0.00");
            }
            else
            {
                txtAmountRemaining.Text = "0.00";
            }
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtCompanyName.Text))
            {
                MessageBox.Show("Company name is required", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCompanyName.Focus();
                return false;
            }

            if (!decimal.TryParse(txtAmount.Text, out decimal amount) || amount <= 0)
            {
                MessageBox.Show("Please enter a valid amount greater than 0", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAmount.Focus();
                return false;
            }

            if (cmbTransactionType.SelectedIndex == -1)
            {
                MessageBox.Show("Please select transaction type", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbTransactionType.Focus();
                return false;
            }

            if (cmbPaymentStatus.SelectedIndex == -1)
            {
                MessageBox.Show("Please select payment status", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbPaymentStatus.Focus();
                return false;
            }

            if (!decimal.TryParse(txtAmountPaid.Text, out decimal paid) || paid < 0)
            {
                MessageBox.Show("Please enter valid paid amount", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAmountPaid.Focus();
                return false;
            }

            if (paid > amount)
            {
                MessageBox.Show("Paid amount cannot be greater than total amount", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAmountPaid.Focus();
                return false;
            }

            if (cmbPaymentStatus.SelectedItem?.ToString() == "Partial" && paid == 0)
            {
                MessageBox.Show("Partial payment must have some amount paid", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAmountPaid.Focus();
                return false;
            }

            if (chkSendToWarehouse.Checked && cmbWarehouses.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a warehouse", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbWarehouses.Focus();
                return false;
            }

            return true;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (!allowClose && e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                return;
            }
            base.OnFormClosing(e);
        }

        private void panelMain_Paint(object sender, PaintEventArgs e)
        {
            // Empty implementation
        }

        private void txtNotes_TextChanged(object sender, EventArgs e)
        {
            // Empty implementation
        }
    }
}