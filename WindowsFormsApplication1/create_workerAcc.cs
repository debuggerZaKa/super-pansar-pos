using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class WorkerAccountManagementForm : Form
    {
        private readonly SqlConnection connection = new SqlConnection(Properties.Settings.Default.connetion);
        private DataTable workerAccountsDataTable;
        private int? currentEditingAccountId = null;
        private bool isEditingMode = false;

        public WorkerAccountManagementForm()
        {
            InitializeComponent();
            InitializeForm();
            LoadWorkerAccounts();
            this.Resize += WorkerAccountManagementForm_Resize;
        }

        private void InitializeForm()
        {
            // Set default values
            createdDateTimePicker.Value = DateTime.Now;
            passwordTextBox.UseSystemPasswordChar = true;
            confirmPasswordTextBox.UseSystemPasswordChar = true;
            CenterPictureBox();
        }

        private void WorkerAccountManagementForm_Resize(object sender, EventArgs e)
        {
            AdjustGridColumns();
            CenterPictureBox();
            AdjustButtonWidths();
        }

        private void CenterPictureBox()
        {
            if (pictureBox1 != null && formPanel != null)
            {
                pictureBox1.Left = (formPanel.Width - pictureBox1.Width) / 2;
            }
        }

        private void AdjustButtonWidths()
        {
            if (bottomButtonPanel != null && bottomButtonPanel.Width > 0)
            {
                int buttonWidth = bottomButtonPanel.Width / 3;

                deleteSelectedButton.Width = buttonWidth;
                editSelectedButton.Width = buttonWidth;
                showSelectedPasswordButton.Width = buttonWidth;

                // Optional: Set minimum width to prevent buttons from becoming too small
                int minWidth = 120;
                if (buttonWidth < minWidth)
                {
                    deleteSelectedButton.Width = minWidth;
                    editSelectedButton.Width = minWidth;
                    showSelectedPasswordButton.Width = minWidth;
                }
            }
        }

        private void LoadWorkerAccounts()
        {
            try
            {
                workerAccountsDataTable = new DataTable();

                using (var connection = new SqlConnection(Properties.Settings.Default.connetion))
                {
                    string query = "SELECT account_id, worker_name, user_name, password, created_at, last_login FROM worker_accounts";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        adapter.Fill(workerAccountsDataTable);
                    }
                }

                // Create a copy for display with masked passwords
                DataTable displayTable = workerAccountsDataTable.Copy();
                foreach (DataRow row in displayTable.Rows)
                {
                    row["password"] = new string('*', row["password"].ToString().Length);
                }

                // After setting the data source
                accountsGridView.DataSource = displayTable;
                accountsGridView.Columns["account_id"].HeaderText = "Id";
                accountsGridView.Columns["worker_name"].HeaderText = "Name";
                accountsGridView.Columns["user_name"].HeaderText = "Username";
                accountsGridView.Columns["password"].HeaderText = "Password";
                accountsGridView.Columns["created_at"].HeaderText = "Created On";
                accountsGridView.Columns["last_login"].HeaderText = "Last Login";

                // Format the datetime columns
                accountsGridView.Columns["created_at"].DefaultCellStyle.Format = "g";
                accountsGridView.Columns["last_login"].DefaultCellStyle.Format = "g";

                accountsGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading worker accounts: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void createButton_Click(object sender, EventArgs e)
        {
            if (isEditingMode)
            {
                UpdateWorkerAccount();
            }
            else
            {
                CreateWorkerAccount();
            }
        }

        private void CreateWorkerAccount()
        {
            if (ValidateInputs())
            {
                try
                {
                    using (var connection = new SqlConnection(Properties.Settings.Default.connetion))
                    {
                        string query = @"INSERT INTO worker_accounts 
                                       (worker_name, user_name, password, created_at)
                                       VALUES 
                                       (@workerName, @userName, @password, @createdAt)";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@workerName", workerNameTextBox.Text.Trim());
                            command.Parameters.AddWithValue("@userName", userNameTextBox.Text.Trim());
                            command.Parameters.AddWithValue("@password", passwordTextBox.Text);
                            command.Parameters.AddWithValue("@createdAt", createdDateTimePicker.Value);

                            connection.Open();
                            command.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("Worker account created successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearForm();
                    LoadWorkerAccounts();
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 2627)
                    {
                        MessageBox.Show("Username already exists. Please choose a different username.",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show($"An error occurred: {ex.Message}", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void UpdateWorkerAccount()
        {
            if (ValidateInputs())
            {
                try
                {
                    using (var connection = new SqlConnection(Properties.Settings.Default.connetion))
                    {
                        string query = @"UPDATE worker_accounts 
                                       SET worker_name = @workerName, 
                                           user_name = @userName, 
                                           password = @password, 
                                           created_at = @createdAt
                                       WHERE account_id = @accountId";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@workerName", workerNameTextBox.Text.Trim());
                            command.Parameters.AddWithValue("@userName", userNameTextBox.Text.Trim());
                            command.Parameters.AddWithValue("@password", passwordTextBox.Text);
                            command.Parameters.AddWithValue("@createdAt", createdDateTimePicker.Value);
                            command.Parameters.AddWithValue("@accountId", currentEditingAccountId);

                            connection.Open();
                            command.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("Worker account updated successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearForm();
                    ResetEditMode();
                    LoadWorkerAccounts();
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 2627)
                    {
                        MessageBox.Show("Username already exists. Please choose a different username.",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show($"An error occurred: {ex.Message}", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(workerNameTextBox.Text))
            {
                MessageBox.Show("Worker name is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                workerNameTextBox.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(userNameTextBox.Text))
            {
                MessageBox.Show("Username is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                userNameTextBox.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(passwordTextBox.Text))
            {
                MessageBox.Show("Password is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                passwordTextBox.Focus();
                return false;
            }

            if (passwordTextBox.Text != confirmPasswordTextBox.Text)
            {
                MessageBox.Show("Password and Confirm Password must match.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                confirmPasswordTextBox.Focus();
                return false;
            }

            return true;
        }

        private void ClearForm()
        {
            workerNameTextBox.Clear();
            userNameTextBox.Clear();
            passwordTextBox.Clear();
            confirmPasswordTextBox.Clear();
            createdDateTimePicker.Value = DateTime.Now;
            showPasswordCheckBox.Checked = false;
            workerNameTextBox.Focus();
            ResetEditMode();
        }

        private void ResetEditMode()
        {
            currentEditingAccountId = null;
            isEditingMode = false;
            createButton.Text = "Create Account";
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void showPasswordCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            passwordTextBox.UseSystemPasswordChar = !showPasswordCheckBox.Checked;
            confirmPasswordTextBox.UseSystemPasswordChar = !showPasswordCheckBox.Checked;
        }

        private void showSelectedPasswordButton_Click(object sender, EventArgs e)
        {
            if (accountsGridView.SelectedRows.Count > 0)
            {
                int selectedIndex = accountsGridView.SelectedRows[0].Index;
                int accountId = (int)workerAccountsDataTable.Rows[selectedIndex]["account_id"];
                string password = workerAccountsDataTable.Rows[selectedIndex]["password"].ToString();

                MessageBox.Show($"Password for account ID {accountId}:\n\n{password}", "Password Reveal", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Please select an account first.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void deleteSelectedButton_Click(object sender, EventArgs e)
        {
            if (accountsGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an account to delete.", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int selectedIndex = accountsGridView.SelectedRows[0].Index;
            int accountId = (int)workerAccountsDataTable.Rows[selectedIndex]["account_id"];
            string workerName = workerAccountsDataTable.Rows[selectedIndex]["worker_name"].ToString();

            var confirmResult = MessageBox.Show($"Are you sure you want to delete the account for {workerName}?",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    using (var connection = new SqlConnection(Properties.Settings.Default.connetion))
                    {
                        string query = "DELETE FROM worker_accounts WHERE account_id = @accountId";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@accountId", accountId);
                            connection.Open();
                            command.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("Account deleted successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadWorkerAccounts();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting account: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void editSelectedButton_Click(object sender, EventArgs e)
        {
            if (accountsGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an account to edit.", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int selectedIndex = accountsGridView.SelectedRows[0].Index;
            DataRow selectedRow = workerAccountsDataTable.Rows[selectedIndex];

            workerNameTextBox.Text = selectedRow["worker_name"].ToString();
            userNameTextBox.Text = selectedRow["user_name"].ToString();
            passwordTextBox.Text = selectedRow["password"].ToString();
            confirmPasswordTextBox.Text = selectedRow["password"].ToString();
            createdDateTimePicker.Value = Convert.ToDateTime(selectedRow["created_at"]);

            currentEditingAccountId = (int)selectedRow["account_id"];
            isEditingMode = true;
            createButton.Text = "Update Account";
        }

        private void WorkerAccountManagementForm_Load(object sender, EventArgs e)
        {
            AdjustGridColumns();
        }

  

        private void AdjustGridColumns()
        {
            if (accountsGridView.Columns.Count > 0)
            {
                int totalWidth = accountsGridView.Width - 40;
                accountsGridView.Columns["worker_name"].Width = (int)(totalWidth * 0.22);
                accountsGridView.Columns["user_name"].Width = (int)(totalWidth * 0.17);
                accountsGridView.Columns["password"].Width = (int)(totalWidth * 0.15);
                accountsGridView.Columns["created_at"].Width = (int)(totalWidth * 0.23);
                accountsGridView.Columns["last_login"].Width = (int)(totalWidth * 0.23);
            }
        }



        private void searchTextBox_TextChanged(object sender, EventArgs e)
        {
           
            if (searchTextBox.Text.Length > 1 || string.IsNullOrWhiteSpace(searchTextBox.Text))
            {
                searchButton_Click(sender, e);
            }
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            string searchTerm = searchTextBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                LoadWorkerAccounts();
                return;
            }


            try
            {
                workerAccountsDataTable = new DataTable();

                using (var connection = new SqlConnection(Properties.Settings.Default.connetion))
                {
                    string query = @"SELECT account_id, worker_name, user_name, password, created_at 
                          FROM worker_accounts 
                          WHERE account_id = @searchTerm OR
                                worker_name LIKE @searchPattern OR
                                user_name LIKE @searchPattern";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                // Try to parse as ID first
                if (int.TryParse(searchTerm, out int accountId))
                {
                    command.Parameters.AddWithValue("@searchTerm", accountId);
                }
                else
                {
                    command.Parameters.AddWithValue("@searchTerm", DBNull.Value);
                }
                
                // Add wildcards for name/username search
                command.Parameters.AddWithValue("@searchPattern", $"%{searchTerm}%");
                
                connection.Open();
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        adapter.Fill(workerAccountsDataTable);
                    }
                }

                if (workerAccountsDataTable.Rows.Count == 0)
                {
                    MessageBox.Show("No account found with that ID", "Not Found",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadWorkerAccounts(); // Reload all data
                    return;
                }

                // Create a copy for display with masked passwords
                DataTable displayTable = workerAccountsDataTable.Copy();
                foreach (DataRow row in displayTable.Rows)
                {
                    row["password"] = new string('*', row["password"].ToString().Length);
                }

                accountsGridView.DataSource = displayTable;
                AdjustGridColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error searching accounts: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void accountsGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void passwordTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void formPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void headerLabel_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}