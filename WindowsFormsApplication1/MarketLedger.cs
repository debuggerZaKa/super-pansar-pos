using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class MarketLedgerForm : Form
    {
        private bool _suppressInitialLoad = false;
        private bool _isFirstLoad = true;

        public MarketLedgerForm(bool suppressInitialLoad = false)
        {
            InitializeComponent();
            _suppressInitialLoad = suppressInitialLoad;

            btnNewEntry.Click += BtnNewEntry_Click;
            btnUpdate.Click += btnUpdate_Click;
            btnFind.Click += btnFind_Click;
            btnReset.Click += btnReset_Click;

            // Set default date range (last 30 days)
            dtpFrom.Value = DateTime.Now.AddDays(-30);
            dtpTo.Value = DateTime.Now;

            // Setup live search
            txtSearch.TextChanged += TxtSearch_TextChanged;
        }

        private void MarketLedgerForm_Load(object sender, EventArgs e)
        {
            if (!_suppressInitialLoad)
            {
                LoadLedgerData();
                CalculateTotals();
                _isFirstLoad = false;
            }
        }

        private void LoadLedgerData(string searchTerm = "", DateTime? fromDate = null, DateTime? toDate = null)
        {
            // On first load, use default 30-day filter unless suppressed
            if (_isFirstLoad && !_suppressInitialLoad)
            {
                fromDate = DateTime.Now.AddDays(-30);
                toDate = DateTime.Now;
            }

            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(Properties.Settings.Default.connetion);
                connection.Open();

                string query = @"SELECT ml.Id, ml.CompanyName, ml.DealerName, ml.Description, ml.Amount, 
                ml.TransactionType, ml.TransactionDate, ml.PaymentStatus, 
                ml.AmountPaid, ml.AmountRemaining, ml.PaymentMethod, 
                ml.ReferenceNumber, w.WarehouseName
                FROM MarketLedger ml
                LEFT JOIN Warehouses w ON ml.WarehouseId = w.WarehouseId
                WHERE (@SearchTerm = '' OR 
                      ml.CompanyName LIKE '%' + @SearchTerm + '%' OR 
                      ml.DealerName LIKE '%' + @SearchTerm + '%' OR
                      w.WarehouseName LIKE '%' + @SearchTerm + '%')
                AND (@FromDate IS NULL OR ml.TransactionDate >= @FromDate)
                AND (@ToDate IS NULL OR ml.TransactionDate <= @ToDate)
                ORDER BY ml.TransactionDate DESC";

                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@SearchTerm", string.IsNullOrEmpty(searchTerm) ? "" : searchTerm);
                cmd.Parameters.AddWithValue("@FromDate", fromDate ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@ToDate", toDate ?? (object)DBNull.Value);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                // Configure DataGridView appearance
                DataGridViewRow row = dgvLedger.RowTemplate;
                row.Height = 30;
                row.MinimumHeight = 20;
                Font font_head = new Font(dgvLedger.DefaultCellStyle.Font.FontFamily, 10, FontStyle.Bold);
                row.DefaultCellStyle.Font = font_head;

                dgvLedger.DataSource = dt;
                dgvLedger.BackgroundColor = SystemColors.ControlDark;
                dgvLedger.GridColor = SystemColors.ControlLight;
                dgvLedger.CellBorderStyle = DataGridViewCellBorderStyle.SingleVertical;

                // Set column headers and widths
                if (dgvLedger.Columns.Contains("Id"))
                    dgvLedger.Columns["Id"].Visible = false;

                if (dgvLedger.Columns.Contains("CompanyName"))
                {
                    dgvLedger.Columns["CompanyName"].HeaderText = "Company";
                    dgvLedger.Columns["CompanyName"].FillWeight = 20;
                }

                if (dgvLedger.Columns.Contains("DealerName"))
                {
                    dgvLedger.Columns["DealerName"].HeaderText = "Dealer";
                    dgvLedger.Columns["DealerName"].FillWeight = 20;
                }

                if (dgvLedger.Columns.Contains("WarehouseName"))
                {
                    dgvLedger.Columns["WarehouseName"].HeaderText = "Warehouse";
                    dgvLedger.Columns["WarehouseName"].FillWeight = 25;
                }

                if (dgvLedger.Columns.Contains("Description"))
                {
                    dgvLedger.Columns["Description"].HeaderText = "Description";
                    dgvLedger.Columns["Description"].FillWeight = 30;
                }

                if (dgvLedger.Columns.Contains("Amount"))
                {
                    dgvLedger.Columns["Amount"].HeaderText = "Amount";
                    dgvLedger.Columns["Amount"].FillWeight = 20;
                    dgvLedger.Columns["Amount"].DefaultCellStyle.Format = "N2";
                }

                if (dgvLedger.Columns.Contains("TransactionType"))
                {
                    dgvLedger.Columns["TransactionType"].HeaderText = "Type";
                    dgvLedger.Columns["TransactionType"].FillWeight = 12;
                }

                if (dgvLedger.Columns.Contains("TransactionDate"))
                {
                    dgvLedger.Columns["TransactionDate"].HeaderText = "Date";
                    dgvLedger.Columns["TransactionDate"].FillWeight = 25;
                    dgvLedger.Columns["TransactionDate"].DefaultCellStyle.Format = "dd MMM yyyy";
                }

                if (dgvLedger.Columns.Contains("PaymentStatus"))
                {
                    dgvLedger.Columns["PaymentStatus"].HeaderText = "Status";
                    dgvLedger.Columns["PaymentStatus"].FillWeight = 12;
                }

                if (dgvLedger.Columns.Contains("AmountPaid"))
                {
                    dgvLedger.Columns["AmountPaid"].HeaderText = "Paid";
                    dgvLedger.Columns["AmountPaid"].FillWeight = 20;
                    dgvLedger.Columns["AmountPaid"].DefaultCellStyle.Format = "N2";
                }

                if (dgvLedger.Columns.Contains("AmountRemaining"))
                {
                    dgvLedger.Columns["AmountRemaining"].HeaderText = "Remaining";
                    dgvLedger.Columns["AmountRemaining"].FillWeight = 20;
                    dgvLedger.Columns["AmountRemaining"].DefaultCellStyle.Format = "N2";
                }

                if (dgvLedger.Columns.Contains("PaymentMethod"))
                {
                    dgvLedger.Columns["PaymentMethod"].HeaderText = "Method";
                    dgvLedger.Columns["PaymentMethod"].FillWeight = 12;
                    dgvLedger.Columns["PaymentMethod"].DefaultCellStyle.Format = "N2";
                }

                if (dgvLedger.Columns.Contains("ReferenceNumber"))
                {
                    dgvLedger.Columns["ReferenceNumber"].HeaderText = "Ref No.";
                    dgvLedger.Columns["ReferenceNumber"].FillWeight = 12;
                    dgvLedger.Columns["ReferenceNumber"].DefaultCellStyle.Format = "N2";
                }

                // Set font styles for headers
                foreach (DataGridViewColumn column in dgvLedger.Columns)
                {
                    column.HeaderCell.Style.Font = font_head;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading ledger data: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }
        }

        public void LoadSingleRecord(int recordId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.connetion))
                {
                    connection.Open();
                    string query = @"SELECT ml.Id, ml.CompanyName, ml.DealerName, ml.Description, ml.Amount, 
                           ml.TransactionType, ml.TransactionDate, ml.PaymentStatus, 
                           ml.AmountPaid, ml.AmountRemaining, ml.PaymentMethod, 
                           ml.ReferenceNumber, w.WarehouseName
                           FROM MarketLedger ml
                           LEFT JOIN Warehouses w ON ml.WarehouseId = w.WarehouseId
                           WHERE ml.Id = @Id";

                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@Id", recordId);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Clear existing data and load the single record
                    dgvLedger.DataSource = null;
                    dgvLedger.DataSource = dt;

                    // Ensure proper selection and visibility
                    if (dgvLedger.Rows.Count > 0)
                    {
                        dgvLedger.Rows[0].Selected = true;
                        dgvLedger.CurrentCell = dgvLedger.Rows[0].Cells[0];
                        dgvLedger.FirstDisplayedScrollingRowIndex = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading record: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CalculateTotals()
        {
            decimal totalToPay = 0;
            decimal totalToReceive = 0;

            foreach (DataGridViewRow row in dgvLedger.Rows)
            {
                if (row.Cells["TransactionType"].Value?.ToString() == "Bought" &&
                    row.Cells["AmountRemaining"].Value != DBNull.Value)
                {
                    totalToPay += Convert.ToDecimal(row.Cells["AmountRemaining"].Value);
                }
                else if (row.Cells["TransactionType"].Value?.ToString() == "Sold" &&
                         row.Cells["AmountRemaining"].Value != DBNull.Value)
                {
                    totalToReceive += Convert.ToDecimal(row.Cells["AmountRemaining"].Value);
                }
            }

            lblTotalToPay.Text = totalToPay.ToString("N2");
            lblTotalToReceive.Text = totalToReceive.ToString("N2");
        }

        private void BtnNewEntry_Click(object sender, EventArgs e)
        {
            var entryForm = new MarketLedgerEntryForm();
            if (entryForm.ShowDialog() == DialogResult.OK)
            {
                LoadLedgerData(txtSearch.Text, dtpFrom.Value, dtpTo.Value);
                CalculateTotals();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvLedger.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a row to update", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DataGridViewRow selectedRow = dgvLedger.SelectedRows[0];
            int recordId = Convert.ToInt32(selectedRow.Cells["Id"].Value);

            try
            {
                using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.connetion))
                {
                    connection.Open();
                    string query = @"SELECT ml.*, w.WarehouseId 
                                   FROM MarketLedger ml
                                   LEFT JOIN Warehouses w ON ml.WarehouseId = w.WarehouseId
                                   WHERE ml.Id = @Id";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@Id", recordId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var entryForm = new MarketLedgerEntryForm();
                            entryForm.SetFormData(
                                reader["Id"].ToString(),
                                reader["CompanyName"].ToString(),
                                reader["DealerName"].ToString(),
                                reader["Description"].ToString(),
                                reader["Amount"].ToString(),
                                reader["TransactionType"].ToString(),
                                Convert.ToDateTime(reader["TransactionDate"]),
                                reader["PaymentStatus"].ToString(),
                                reader["AmountPaid"].ToString(),
                                reader["PaymentMethod"].ToString(),
                                reader["ReferenceNumber"].ToString(),
                                reader["Notes"].ToString(),
                                reader["WarehouseId"] != DBNull.Value ? (int?)Convert.ToInt32(reader["WarehouseId"]) : null
                            );

                            if (entryForm.ShowDialog() == DialogResult.OK)
                            {
                                LoadLedgerData(txtSearch.Text, dtpFrom.Value, dtpTo.Value);
                                CalculateTotals();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading record for editing: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            LoadLedgerData(txtSearch.Text, dtpFrom.Value, dtpTo.Value);
            CalculateTotals();
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            // Live search with current date filters
            LoadLedgerData(txtSearch.Text, dtpFrom.Value, dtpTo.Value);
            CalculateTotals();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            // Reset all filters including date range
            txtSearch.Text = "";
            dtpFrom.Value = DateTime.Now.AddDays(-30); // Reset to default 30-day filter
            dtpTo.Value = DateTime.Now;
            LoadLedgerData(); // Load with default filters
            CalculateTotals();
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnFind_Click(sender, e);
            }
        }

        private void dgvLedger_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvLedger.Columns[e.ColumnIndex].Name == "AmountRemaining" && e.Value != null)
            {
                DataGridViewRow row = dgvLedger.Rows[e.RowIndex];
                string transactionType = row.Cells["TransactionType"].Value?.ToString();

                if (transactionType == "Bought")
                {
                    e.CellStyle.ForeColor = Color.Red;
                }
                else if (transactionType == "Sold")
                {
                    e.CellStyle.ForeColor = Color.Green;
                }
            }
        }

        private void dgvLedger_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Empty implementation
        }

        private void lblTotalToPay_Click(object sender, EventArgs e)
        {
            // Empty implementation
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            // Empty implementation
        }

        private void dtpFrom_ValueChanged(object sender, EventArgs e)
        {
            // Auto-refresh when date changes
            if (!_isFirstLoad)
            {
                LoadLedgerData(txtSearch.Text, dtpFrom.Value, dtpTo.Value);
                CalculateTotals();
            }
        }

        private void dtpTo_ValueChanged(object sender, EventArgs e)
        {
            // Auto-refresh when date changes
            if (!_isFirstLoad)
            {
                LoadLedgerData(txtSearch.Text, dtpFrom.Value, dtpTo.Value);
                CalculateTotals();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dgvLedger.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a record to delete", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Get the selected row
            DataGridViewRow selectedRow = dgvLedger.SelectedRows[0];
            int recordId = Convert.ToInt32(selectedRow.Cells["Id"].Value);

            // Get some details for confirmation message
            string companyName = selectedRow.Cells["CompanyName"].Value?.ToString() ?? "N/A";
            string dealerName = selectedRow.Cells["DealerName"].Value?.ToString() ?? "N/A";
            string amount = selectedRow.Cells["Amount"].Value?.ToString() ?? "0";
            string transDate = selectedRow.Cells["TransactionDate"].Value?.ToString() ?? DateTime.Now.ToShortDateString();

            // Ask for confirmation
            DialogResult result = MessageBox.Show(
                $"Are you sure you want to delete this record?\n\n" +
                $"Company: {companyName}\n" +
                $"Dealer: {dealerName}\n" +
                $"Amount: {amount}\n" +
                $"Date: {transDate}",
                "Confirm Deletion",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                SqlConnection connection = null;
                try
                {
                    connection = new SqlConnection(Properties.Settings.Default.connetion);
                    connection.Open();

                    string query = "DELETE FROM MarketLedger WHERE Id = @Id";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@Id", recordId);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Record deleted successfully", "Success",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Refresh the data
                        LoadLedgerData(txtSearch.Text, dtpFrom.Value, dtpTo.Value);
                        CalculateTotals();
                    }
                    else
                    {
                        MessageBox.Show("No record was deleted", "Information",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting record: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (connection != null && connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                        connection.Dispose();
                    }
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}