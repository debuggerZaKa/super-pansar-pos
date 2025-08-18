using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Drawing;

namespace WindowsFormsApplication1
{
    public partial class WarehouseForm : Form
    {
        private bool isEditMode = false;
        private int currentWarehouseId = -1;

        SqlConnection sqlconnection;
        SqlCommand sqlcommand;
        string ConnectionString = Properties.Settings.Default.connetion;
        string Query;
        DataSet dataset;
        DataTable datatable;
        SqlDataAdapter sqladapter;

        public WarehouseForm()
        {
            InitializeComponent();
        }

        private void WarehouseForm_Load(object sender, EventArgs e)
        {
            dtpStartDate.ValueChanged -= DateFilters_ValueChanged;
            dtpEndDate.ValueChanged -= DateFilters_ValueChanged;

            dtpEndDate.Value = DateTime.Today;
            dtpStartDate.Value = DateTime.Today.AddDays(-30);

            dtpStartDate.ValueChanged += DateFilters_ValueChanged;
            dtpEndDate.ValueChanged += DateFilters_ValueChanged;

            LoadWarehouses();
            LoadMarketLedgerData();
            SetFormControlsState(false);
        }

        private void LoadWarehouses()
        {
            try
            {
                sqlconnection = new SqlConnection(ConnectionString);
                Query = "SELECT WarehouseId, WarehouseName FROM Warehouses ORDER BY WarehouseName";
                sqlcommand = new SqlCommand(Query, sqlconnection);
                sqladapter = new SqlDataAdapter();
                datatable = new DataTable();

                sqlconnection.Open();
                SqlDataReader reader = sqlcommand.ExecuteReader();
                datatable.Load(reader);

                DataRow emptyRow = datatable.NewRow();
                emptyRow["WarehouseId"] = -1;
                emptyRow["WarehouseName"] = "-- Select Warehouse --";
                datatable.Rows.InsertAt(emptyRow, 0);

                cbWarehouses.DataSource = datatable;
                cbWarehouses.DisplayMember = "WarehouseName";
                cbWarehouses.ValueMember = "WarehouseId";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading warehouses: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                sqlconnection.Close();
            }
        }

        private void LoadMarketLedgerData(int warehouseId = -1)
        {
            try
            {
                sqlconnection = new SqlConnection(ConnectionString);
                Query = @"SELECT ml.Id, ml.CompanyName AS 'Company', ml.DealerName AS 'Dealer', 
                        ml.Description, ml.Amount, ml.TransactionType, 
                        ml.TransactionDate AS 'Date', w.WarehouseName AS 'Warehouse'
                        FROM MarketLedger ml
                        LEFT JOIN Warehouses w ON ml.WarehouseId = w.WarehouseId
                        WHERE (@WarehouseId = -1 OR ml.WarehouseId = @WarehouseId)
                        AND ml.TransactionDate BETWEEN @StartDate AND @EndDate ORDER BY ml.TransactionDate DESC";

                sqlcommand = new SqlCommand(Query, sqlconnection);
                sqlcommand.Parameters.AddWithValue("@WarehouseId", warehouseId);
                sqlcommand.Parameters.AddWithValue("@StartDate", dtpStartDate.Value.Date);
                sqlcommand.Parameters.AddWithValue("@EndDate", dtpEndDate.Value.Date.AddDays(1).AddSeconds(-1));

                sqladapter = new SqlDataAdapter();
                datatable = new DataTable();

                sqlconnection.Open();
                SqlDataReader reader = sqlcommand.ExecuteReader();
                datatable.Load(reader);

                dataGridView1.DataSource = datatable;

                if (dataGridView1.Columns.Count > 0)
                {
                    dataGridView1.Columns["Description"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    dataGridView1.Columns["Id"].Width = 50;
                    dataGridView1.Columns["Company"].Width = 120;
                    dataGridView1.Columns["Dealer"].Width = 120;
                    dataGridView1.Columns["Amount"].Width = 80;
                    dataGridView1.Columns["TransactionType"].Width = 100;
                    dataGridView1.Columns["Date"].Width = 120;
                    dataGridView1.Columns["Warehouse"].Width = 150;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading market ledger data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                sqlconnection.Close();
            }
        }

        private void cbWarehouses_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbWarehouses.SelectedIndex <= 0)
            {
                LoadMarketLedgerData();
                ResetForm();
                SetFormControlsState(true); // Enable form when no warehouse selected
                return;
            }

            if (cbWarehouses.SelectedValue != null && cbWarehouses.SelectedValue != DBNull.Value)
            {
                try
                {
                    int warehouseId = Convert.ToInt32(cbWarehouses.SelectedValue);
                    LoadMarketLedgerData(warehouseId);
                    LoadWarehouseDetails(warehouseId);
                    SetFormControlsState(false); // Disable form when warehouse selected
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error processing selection: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void LoadWarehouseDetails(int warehouseId)
        {
            try
            {
                sqlconnection = new SqlConnection(ConnectionString);
                Query = "SELECT WarehouseName, Location FROM Warehouses WHERE WarehouseId = @WarehouseId";
                sqlcommand = new SqlCommand(Query, sqlconnection);
                sqlcommand.Parameters.AddWithValue("@WarehouseId", warehouseId);

                sqlconnection.Open();
                using (SqlDataReader reader = sqlcommand.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        txtWarehouseName.Text = reader["WarehouseName"].ToString();
                        txtLocation.Text = reader["Location"].ToString();
                        currentWarehouseId = warehouseId;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading warehouse details: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                sqlconnection.Close();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtWarehouseName.Text))
            {
                MessageBox.Show("Warehouse name is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                sqlconnection = new SqlConnection(ConnectionString);

                if (isEditMode && currentWarehouseId > 0)
                {
                    Query = "UPDATE Warehouses SET WarehouseName = @Name, Location = @Location WHERE WarehouseId = @Id";
                    sqlcommand = new SqlCommand(Query, sqlconnection);
                    sqlcommand.Parameters.AddWithValue("@Name", txtWarehouseName.Text);
                    sqlcommand.Parameters.AddWithValue("@Location", string.IsNullOrWhiteSpace(txtLocation.Text) ? (object)DBNull.Value : txtLocation.Text);
                    sqlcommand.Parameters.AddWithValue("@Id", currentWarehouseId);

                    sqlconnection.Open();
                    int rowsAffected = sqlcommand.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Warehouse updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadWarehouses();
                        ResetForm();
                    }
                }
                else
                {
                    Query = "INSERT INTO Warehouses (WarehouseName, Location) VALUES (@Name, @Location)";
                    sqlcommand = new SqlCommand(Query, sqlconnection);
                    sqlcommand.Parameters.AddWithValue("@Name", txtWarehouseName.Text);
                    sqlcommand.Parameters.AddWithValue("@Location", string.IsNullOrWhiteSpace(txtLocation.Text) ? (object)DBNull.Value : txtLocation.Text);

                    sqlconnection.Open();
                    int rowsAffected = sqlcommand.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Warehouse added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadWarehouses();
                        ResetForm();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving warehouse: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                sqlconnection.Close();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (currentWarehouseId <= 0)
            {
                MessageBox.Show("Please select a warehouse to edit.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            isEditMode = true;
            SetFormControlsState(true);
        }

        private void btnViewDetails_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0) return;

            int selectedId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Id"].Value);
            var ledgerForm = new MarketLedgerForm(true);
            ledgerForm.LoadSingleRecord(selectedId);
            ledgerForm.Show();
        }

        private void btnAddToWarehouse_Click(object sender, EventArgs e)
        {
            if (cbWarehouses.SelectedIndex <= 0)
            {
                MessageBox.Show("Please select a warehouse first", "Validation Error",
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int selectedWarehouseId = Convert.ToInt32(cbWarehouses.SelectedValue);

            using (var ledgerEntryForm = new WarehouseLedgerEntryForm(selectedWarehouseId))
            {
                if (ledgerEntryForm.ShowDialog() == DialogResult.OK)
                {
                    LoadMarketLedgerData(selectedWarehouseId);
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (currentWarehouseId <= 0) return;

            try
            {
                // First check if warehouse has any entries
                bool hasEntries = false;
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    string checkSql = "SELECT COUNT(*) FROM MarketLedger WHERE WarehouseId = @WarehouseId";
                    using (SqlCommand cmd = new SqlCommand(checkSql, conn))
                    {
                        cmd.Parameters.AddWithValue("@WarehouseId", currentWarehouseId);
                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        hasEntries = count > 0;
                    }
                }

                if (hasEntries)
                {
                    // Show transfer form only if warehouse has entries
                    using (var transferForm = new WarehouseTransferForm(GetOtherWarehouses(currentWarehouseId)))
                    {
                        if (transferForm.ShowDialog() == DialogResult.OK)
                        {
                            DeleteWarehouseWithTransfer(transferForm.SelectedWarehouseId);
                        }
                    }
                }
                else
                {
                    // Just delete if no entries
                    DeleteWarehouseWithTransfer(null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void DeleteWarehouseWithTransfer(int? newWarehouseId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    using (SqlTransaction transaction = conn.BeginTransaction())
                    {
                        if (newWarehouseId.HasValue)
                        {
                            // Update MarketLedger records only if transferring
                            string updateSql = @"UPDATE MarketLedger 
                                      SET WarehouseId = @NewWarehouseId 
                                      WHERE WarehouseId = @CurrentId";

                            using (SqlCommand cmd = new SqlCommand(updateSql, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@CurrentId", currentWarehouseId);
                                cmd.Parameters.AddWithValue("@NewWarehouseId", newWarehouseId.Value);
                                int affectedRows = cmd.ExecuteNonQuery();
                                MessageBox.Show($"Transferred {affectedRows} ledger entries");
                            }
                        }

                        // Delete warehouse
                        string deleteSql = "DELETE FROM Warehouses WHERE WarehouseId = @Id";
                        using (SqlCommand cmd = new SqlCommand(deleteSql, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@Id", currentWarehouseId);
                            cmd.ExecuteNonQuery();
                        }

                        transaction.Commit();
                        MessageBox.Show("Warehouse deleted successfully");
                        LoadWarehouses();
                        ResetForm();
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                if (sqlEx.Number == 547) // FK violation
                {
                    MessageBox.Show("Could not complete operation. There are still references to this warehouse.");
                }
                else
                {
                    MessageBox.Show($"Database error: {sqlEx.Message}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }


        private DataTable GetOtherWarehouses(int excludeWarehouseId)
        {
            DataTable warehouses = new DataTable();

            try
            {
                sqlconnection = new SqlConnection(ConnectionString);
                Query = "SELECT WarehouseId, WarehouseName FROM Warehouses WHERE WarehouseId != @ExcludeId ORDER BY WarehouseName";
                sqlcommand = new SqlCommand(Query, sqlconnection);
                sqlcommand.Parameters.AddWithValue("@ExcludeId", excludeWarehouseId);

                sqlconnection.Open();
                SqlDataReader reader = sqlcommand.ExecuteReader();
                warehouses.Load(reader);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading warehouses: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                sqlconnection.Close();
            }

            return warehouses;
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            ResetForm();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ResetForm()
        {
            txtWarehouseName.Clear();
            txtLocation.Clear();
            currentWarehouseId = -1;
            isEditMode = false;
            cbWarehouses.SelectedIndex = 0;
            SetFormControlsState(true);
        }

        private void SetFormControlsState(bool enabled)
        {
            txtWarehouseName.Enabled = enabled;
            txtLocation.Enabled = enabled;
            btnSave.Enabled = enabled;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            ApplySearchFilter();
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            ApplySearchFilter();
        }

        private void ApplySearchFilter()
        {
            if (dataGridView1.DataSource is DataTable dt)
            {
                string searchText = txtSearch.Text.Trim();
                if (!string.IsNullOrEmpty(searchText))
                {
                    string filter = $"Company LIKE '%{searchText}%' OR Dealer LIKE '%{searchText}%' OR Description LIKE '%{searchText}%'";
                    dt.DefaultView.RowFilter = filter;
                }
                else
                {
                    dt.DefaultView.RowFilter = string.Empty;
                }
            }
        }

        private void DateFilters_ValueChanged(object sender, EventArgs e)
        {
            if (dtpStartDate.Value == DateTime.MinValue || dtpEndDate.Value == DateTime.MinValue)
                return;

            if (dtpStartDate.Value > dtpEndDate.Value)
            {
                if (sender != null)
                {
                    MessageBox.Show("Start date cannot be after end date.", "Invalid Date Range",
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dtpStartDate.Value = dtpEndDate.Value.AddDays(-1);
                }
                return;
            }

            int warehouseId = cbWarehouses.SelectedIndex > 0 ? Convert.ToInt32(cbWarehouses.SelectedValue) : -1;
            LoadMarketLedgerData(warehouseId);
        }

        private void btnResetFilters_Click(object sender, EventArgs e)
        {
            dtpStartDate.ValueChanged -= DateFilters_ValueChanged;
            dtpEndDate.ValueChanged -= DateFilters_ValueChanged;

            dtpEndDate.Value = DateTime.Today;
            dtpStartDate.Value = DateTime.Today.AddDays(-30);

            dtpStartDate.ValueChanged += DateFilters_ValueChanged;
            dtpEndDate.ValueChanged += DateFilters_ValueChanged;

            txtSearch.Text = "";

            int warehouseId = cbWarehouses.SelectedIndex > 0 ? Convert.ToInt32(cbWarehouses.SelectedValue) : -1;
            LoadMarketLedgerData(warehouseId);
        }
        private void btnDeleteEntry_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an entry to delete.", "Warning",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int selectedId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Id"].Value);
            string description = dataGridView1.SelectedRows[0].Cells["Description"].Value.ToString();

            var result = MessageBox.Show($"Are you sure you want to delete this entry?\n\nDescription: {description}",
                                       "Confirm Delete",
                                       MessageBoxButtons.YesNo,
                                       MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(ConnectionString))
                    {
                        conn.Open();
                        string deleteSql = "DELETE FROM MarketLedger WHERE Id = @Id";
                        using (SqlCommand cmd = new SqlCommand(deleteSql, conn))
                        {
                            cmd.Parameters.AddWithValue("@Id", selectedId);
                            int rowsAffected = cmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Entry deleted successfully.", "Success",
                                              MessageBoxButtons.OK, MessageBoxIcon.Information);

                                // Refresh the data
                                int warehouseId = cbWarehouses.SelectedIndex > 0 ?
                                                 Convert.ToInt32(cbWarehouses.SelectedValue) : -1;
                                LoadMarketLedgerData(warehouseId);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting entry: {ex.Message}", "Error",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void WarehouseForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (sqlconnection != null && sqlconnection.State == ConnectionState.Open)
            {
                sqlconnection.Close();
            }
        }

        private void WarehouseForm_Resize(object sender, EventArgs e)
        {
            // Calculate available width for panels
            int availableWidth = filterControlsPanel.Width - 20;

            // Warehouse selector (40%)
            warehouseSelectorPanel.Width = (int)(availableWidth * 0.4);
            warehouseSelectorPanel.MinimumSize = new Size(300, warehouseSelectorPanel.Height);

            // Search panel (60%)
            searchPanel.Width = availableWidth - warehouseSelectorPanel.Width;
            searchPanel.Left = warehouseSelectorPanel.Right;
            searchPanel.MinimumSize = new Size(300, searchPanel.Height);

            // Date panels (45% each, 10% for reset button)
            dateLeftPanel.Width = (int)(dateFilterPanel.Width * 0.45);
            dateRightPanel.Width = (int)(dateFilterPanel.Width * 0.45);
            dateRightPanel.Left = dateLeftPanel.Right;
            btnResetFilters.Left = dateRightPanel.Right + 10;

            // Ensure minimum sizes
            dateLeftPanel.MinimumSize = new Size(250, dateLeftPanel.Height);
            dateRightPanel.MinimumSize = new Size(250, dateRightPanel.Height);

            // Bottom buttons - now 5 buttons
            int buttonWidth = (bottomButtonPanel.Width - 4) / 5; // Changed from 4 to 5 buttons
            buttonWidth = Math.Max(buttonWidth, 100);

            btnViewDetails.Width = buttonWidth;
            btnEdit.Width = buttonWidth;
            btnDelete.Width = buttonWidth;
            btnAddToWarehouse.Width = buttonWidth;
            btnDeleteEntry.Width = buttonWidth;

            btnViewDetails.Left = 0;
            btnEdit.Left = btnViewDetails.Right + 1;
            btnDelete.Left = btnEdit.Right + 1;
            btnAddToWarehouse.Left = btnDelete.Right + 1;
            btnDeleteEntry.Left = btnAddToWarehouse.Right + 1;

            // Picture box
            int pictureWidth = formPanel.Width - 40;
            int pictureHeight = (int)(pictureWidth * 0.25);

            pictureBox1.Width = pictureWidth;
            pictureBox1.Height = pictureHeight;
            pictureBox1.Location = new Point(
                (formPanel.Width - pictureBox1.Width) / 2,
                20);

            // Form buttons
            int buttonSize = Math.Min(buttonPanel.Width - 40, buttonPanel.Height - 40);
            buttonSize = Math.Min(buttonSize, 100);

            btnExit.Size = new Size(buttonSize, buttonSize);
            btnClear.Size = new Size(buttonSize, buttonSize);
            btnSave.Size = new Size(buttonSize, buttonSize);

            int totalButtonWidth = (buttonSize * 3) + 20;
            int startX = (buttonPanel.Width - totalButtonWidth) / 2;

            btnExit.Location = new Point(startX + (buttonSize * 2) + 20, (buttonPanel.Height - buttonSize) / 2);
            btnClear.Location = new Point(startX + buttonSize + 10, (buttonPanel.Height - buttonSize) / 2);
            btnSave.Location = new Point(startX, (buttonPanel.Height - buttonSize) / 2);
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Cell content click event handler
        }

        private void buttonPanel_Paint(object sender, PaintEventArgs e)
        {
            // Panel paint event handler
        }
    }
}