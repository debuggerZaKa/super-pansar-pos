using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class LoginForm : Form
    {
        private SqlConnection connection = new SqlConnection(Properties.Settings.Default.connetion);
        private bool isMaximized = false;
        private Point lastLocation;

        public LoginForm()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.Resize += LoginForm_Resize;
            UpdateFormState();
        }

        private void LoginForm_Resize(object sender, EventArgs e)
        {
            UpdateFormState();
        }

        private void UpdateFormState()
        {
            if (WindowState == FormWindowState.Maximized)
            {
                btnMaximize.Text = "❐";
                isMaximized = true;
            }
            else
            {
                btnMaximize.Text = "□";
                isMaximized = false;
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both username and password.", "Login Failed",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();

                string query = @"SELECT account_id, worker_name 
                      FROM worker_accounts 
                      WHERE user_name = @username 
                      AND password = @password 
                      AND is_active = 1";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@password", password);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int accountId = reader.GetInt32(0);
                            string workerName = reader.GetString(1);

                  
                            reader.Close();

                            UpdateLastLogin(accountId); 

                            Properties.Settings.Default.CurrentUserID = accountId;
                            Properties.Settings.Default.CurrentUserName = workerName;
                            Properties.Settings.Default.CurrentUserType = "worker";
                            Properties.Settings.Default.Save();

                            this.Hide();
                            Super_Pansar_store_invoice invoiceForm = new Super_Pansar_store_invoice();
                            invoiceForm.ShowDialog();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Invalid username or password, or account is inactive.",
                                "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    } 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during login: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        private void UpdateLastLogin(int accountId)
        {
            try
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();

                string query = "UPDATE worker_accounts SET last_login = GETDATE() WHERE account_id = @accountId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@accountId", accountId);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to update last login time: {ex.Message}", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnMaximize_Click(object sender, EventArgs e)
        {
            if (isMaximized)
            {
                this.WindowState = FormWindowState.Normal;
                btnMaximize.Text = "□";
                isMaximized = false;
            }
            else
            {
                this.WindowState = FormWindowState.Maximized;
                btnMaximize.Text = "❐";
                isMaximized = true;
            }
        }

        private void panelTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                lastLocation = e.Location;
            }
        }

        private void panelTitleBar_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Location = new Point(
                    (this.Location.X - lastLocation.X) + e.X,
                    (this.Location.Y - lastLocation.Y) + e.Y);
                this.Update();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            // Draw border
            ControlPaint.DrawBorder(e.Graphics, ClientRectangle,
                Color.FromArgb(80, 80, 80), 1, ButtonBorderStyle.Solid,
                Color.FromArgb(80, 80, 80), 1, ButtonBorderStyle.Solid,
                Color.FromArgb(80, 80, 80), 1, ButtonBorderStyle.Solid,
                Color.FromArgb(80, 80, 80), 1, ButtonBorderStyle.Solid);
        }

        // For rounded corners (optional)
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
            int nLeftRect, int nTopRect, int nRightRect, int nBottomRect,
            int nWidthEllipse, int nHeightEllipse);

        private void panelRight_Paint(object sender, PaintEventArgs e)
        {
            // Optional: Custom panel painting logic
        }
    }
}