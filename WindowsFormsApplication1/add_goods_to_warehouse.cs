using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class WarehouseLedgerEntryForm : Form
    {
        private SqlConnection connection = new SqlConnection(Properties.Settings.Default.connetion);
        private int warehouseId = -1;

        public WarehouseLedgerEntryForm(int warehouseId)
        {
            InitializeComponent();
            this.warehouseId = warehouseId;
            SetupForm();
        }

        private void SetupForm()
        {
            // Set current date
            dtpTransactionDate.Value = DateTime.Now;

            // Initialize dropdowns
            cmbTransactionType.Items.AddRange(new string[] { "Bought", "Sold" });

            // Set default values
            cmbTransactionType.SelectedIndex = 0; // Bought

            // Configure warehouse controls
            txtWarehouse.Text = GetWarehouseName(warehouseId);
            txtWarehouse.Enabled = false;
        }

        private string GetWarehouseName(int warehouseId)
        {
            try
            {
                connection.Open();
                string query = "SELECT WarehouseName FROM Warehouses WHERE WarehouseId = @WarehouseId";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@WarehouseId", warehouseId);
                return cmd.ExecuteScalar()?.ToString() ?? "Unknown Warehouse";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading warehouse: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "Unknown Warehouse";
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            try
            {
                connection.Open();

                string query = @"INSERT INTO MarketLedger 
                            (CompanyName, DealerName, Description, Amount, 
                             TransactionType, TransactionDate, PaymentStatus, 
                             AmountPaid, AmountRemaining, PaymentMethod, 
                             ReferenceNumber, Notes, WarehouseId)
                            VALUES 
                            (@CompanyName, @DealerName, @Description, @Amount, 
                             @TransactionType, @TransactionDate, @PaymentStatus, 
                             @AmountPaid, @AmountRemaining, @PaymentMethod, 
                             @ReferenceNumber, @Notes, @WarehouseId)";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@CompanyName", txtCompanyName.Text.Trim());
                    cmd.Parameters.AddWithValue("@DealerName", txtDealerName.Text.Trim());
                    cmd.Parameters.AddWithValue("@Description", txtDescription.Text.Trim());
                    cmd.Parameters.AddWithValue("@Amount", decimal.Parse(txtAmount.Text));
                    cmd.Parameters.AddWithValue("@TransactionType", cmbTransactionType.SelectedItem);
                    cmd.Parameters.AddWithValue("@TransactionDate", dtpTransactionDate.Value);
                    cmd.Parameters.AddWithValue("@PaymentStatus", "Paid"); // Default to Paid for warehouse entries
                    cmd.Parameters.AddWithValue("@AmountPaid", decimal.Parse(txtAmount.Text)); // Full amount paid
                    cmd.Parameters.AddWithValue("@AmountRemaining", 0); // Nothing remaining
                    cmd.Parameters.AddWithValue("@PaymentMethod", "Cash"); // Default payment method
                    cmd.Parameters.AddWithValue("@ReferenceNumber", "");
                    cmd.Parameters.AddWithValue("@Notes", txtNotes.Text.Trim());
                    cmd.Parameters.AddWithValue("@WarehouseId", warehouseId);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Warehouse entry added successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving warehouse entry: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtDescription.Text))
            {
                MessageBox.Show("Description is required", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDescription.Focus();
                return false;
            }

            if (!decimal.TryParse(txtAmount.Text, out decimal amount) || amount <= 0)
            {
                MessageBox.Show("Please enter a valid amount", "Validation Error",
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

            return true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void panelMain_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtWarehouse_TextChanged(object sender, EventArgs e)
        {

        }
    }
}