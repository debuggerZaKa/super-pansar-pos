using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class AdminUserManagementForm : Form
    {
        private readonly SqlConnection connection = new SqlConnection(Properties.Settings.Default.connetion);
        private DataTable adminUsersDataTable;
        private int? currentEditingUserId = null;
        private bool isEditingMode = false;

        public AdminUserManagementForm()
        {
            InitializeComponent();
            InitializeForm();
            LoadAdminUsers();
            this.Resize += AdminUserManagementForm_Resize;
        }

        private void InitializeForm()
        {
            // Set default values
            createdDateTimePicker.Value = DateTime.Now;
            passwordTextBox.UseSystemPasswordChar = true;
            confirmPasswordTextBox.UseSystemPasswordChar = true;
            CenterPictureBox();
        }

        private void AdminUserManagementForm_Resize(object sender, EventArgs e)
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

        private void LoadAdminUsers()
        {
            try
            {
                adminUsersDataTable = new DataTable();

                using (var connection = new SqlConnection(Properties.Settings.Default.connetion))
                {
                    string query = "SELECT Id, user_name, password, created_at, last_login FROM admin_user";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        adapter.Fill(adminUsersDataTable);
                    }
                }

                // Create a copy for display with masked passwords
                DataTable displayTable = adminUsersDataTable.Copy();
                foreach (DataRow row in displayTable.Rows)
                {
                    row["password"] = new string('*', row["password"].ToString().Length);
                }

                // After setting the data source
                accountsGridView.DataSource = displayTable;
                accountsGridView.Columns["Id"].HeaderText = "ID";
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
                MessageBox.Show($"Error loading admin users: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void createButton_Click(object sender, EventArgs e)
        {
            if (isEditingMode)
            {
                UpdateAdminUser();
            }
            else
            {
                CreateAdminUser();
            }
        }

        private void CreateAdminUser()
        {
            if (ValidateInputs())
            {
                try
                {
                    using (var connection = new SqlConnection(Properties.Settings.Default.connetion))
                    {
                        string query = @"INSERT INTO admin_user 
                                       (user_name, password, created_at)
                                       VALUES 
                                       (@userName, @password, @createdAt)";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@userName", userNameTextBox.Text.Trim());
                            command.Parameters.AddWithValue("@password", passwordTextBox.Text);
                            command.Parameters.AddWithValue("@createdAt", createdDateTimePicker.Value);

                            connection.Open();
                            command.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("Admin user created successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearForm();
                    LoadAdminUsers();
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

        private void UpdateAdminUser()
        {
            if (ValidateInputs())
            {
                try
                {
                    using (var connection = new SqlConnection(Properties.Settings.Default.connetion))
                    {
                        string query = @"UPDATE admin_user 
                                       SET user_name = @userName, 
                                           password = @password, 
                                           created_at = @createdAt
                                       WHERE Id = @userId";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@userName", userNameTextBox.Text.Trim());
                            command.Parameters.AddWithValue("@password", passwordTextBox.Text);
                            command.Parameters.AddWithValue("@createdAt", createdDateTimePicker.Value);
                            command.Parameters.AddWithValue("@userId", currentEditingUserId);

                            connection.Open();
                            command.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("Admin user updated successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearForm();
                    ResetEditMode();
                    LoadAdminUsers();
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
            userNameTextBox.Clear();
            passwordTextBox.Clear();
            confirmPasswordTextBox.Clear();
            createdDateTimePicker.Value = DateTime.Now;
            showPasswordCheckBox.Checked = false;
            userNameTextBox.Focus();
            ResetEditMode();
        }

        private void ResetEditMode()
        {
            currentEditingUserId = null;
            isEditingMode = false;
            createButton.Text = "Create User";
            headerLabel.Text = "Create New Admin User";
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
                int userId = (int)adminUsersDataTable.Rows[selectedIndex]["Id"];
                string password = adminUsersDataTable.Rows[selectedIndex]["password"].ToString();

                MessageBox.Show($"Password for user ID {userId}:\n\n{password}", "Password Reveal", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Please select a user first.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void deleteSelectedButton_Click(object sender, EventArgs e)
        {
            if (accountsGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a user to delete.", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int selectedIndex = accountsGridView.SelectedRows[0].Index;
            int userId = (int)adminUsersDataTable.Rows[selectedIndex]["Id"];
            string userName = adminUsersDataTable.Rows[selectedIndex]["user_name"].ToString();

            var confirmResult = MessageBox.Show($"Are you sure you want to delete the user '{userName}'?",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    using (var connection = new SqlConnection(Properties.Settings.Default.connetion))
                    {
                        string query = "DELETE FROM admin_user WHERE Id = @userId";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@userId", userId);
                            connection.Open();
                            command.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("User deleted successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadAdminUsers();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting user: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void editSelectedButton_Click(object sender, EventArgs e)
        {
            if (accountsGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a user to edit.", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int selectedIndex = accountsGridView.SelectedRows[0].Index;
            DataRow selectedRow = adminUsersDataTable.Rows[selectedIndex];

            userNameTextBox.Text = selectedRow["user_name"].ToString();
            passwordTextBox.Text = selectedRow["password"].ToString();
            confirmPasswordTextBox.Text = selectedRow["password"].ToString();
            createdDateTimePicker.Value = Convert.ToDateTime(selectedRow["created_at"]);

            currentEditingUserId = (int)selectedRow["Id"];
            isEditingMode = true;
            createButton.Text = "Update User";
            headerLabel.Text = "Edit Admin User";
        }

        private void AdminUserManagementForm_Load(object sender, EventArgs e)
        {
            AdjustGridColumns();
        }

        private void AdjustGridColumns()
        {
            if (accountsGridView.Columns.Count > 0)
            {
                int totalWidth = accountsGridView.Width - 40;
                accountsGridView.Columns["Id"].Width = (int)(totalWidth * 0.15);
                accountsGridView.Columns["user_name"].Width = (int)(totalWidth * 0.25);
                accountsGridView.Columns["password"].Width = (int)(totalWidth * 0.20);
                accountsGridView.Columns["created_at"].Width = (int)(totalWidth * 0.20);
                accountsGridView.Columns["last_login"].Width = (int)(totalWidth * 0.20);
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
                LoadAdminUsers();
                return;
            }

            try
            {
                adminUsersDataTable = new DataTable();

                using (var connection = new SqlConnection(Properties.Settings.Default.connetion))
                {
                    string query = @"SELECT Id, user_name, password, created_at, last_login 
                          FROM admin_user 
                          WHERE Id = @searchTerm OR
                                user_name LIKE @searchPattern";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Try to parse as ID first
                        if (int.TryParse(searchTerm, out int userId))
                        {
                            command.Parameters.AddWithValue("@searchTerm", userId);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@searchTerm", DBNull.Value);
                        }

                        // Add wildcards for username search
                        command.Parameters.AddWithValue("@searchPattern", $"%{searchTerm}%");

                        connection.Open();
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        adapter.Fill(adminUsersDataTable);
                    }
                }

                if (adminUsersDataTable.Rows.Count == 0)
                {
                    MessageBox.Show("No user found with that ID or username", "Not Found",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadAdminUsers(); // Reload all data
                    return;
                }

                // Create a copy for display with masked passwords
                DataTable displayTable = adminUsersDataTable.Copy();
                foreach (DataRow row in displayTable.Rows)
                {
                    row["password"] = new string('*', row["password"].ToString().Length);
                }

                accountsGridView.DataSource = displayTable;
                AdjustGridColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error searching users: {ex.Message}", "Error",
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

        private void accountsGridView_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}