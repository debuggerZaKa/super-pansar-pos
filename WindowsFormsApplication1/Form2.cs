using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace WindowsFormsApplication1
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, EventArgs e)
        {
            string username = usernameTextBox.Text;
            string password = passwordTextBox.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both username and password", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            SqlConnection conn = new SqlConnection(Properties.Settings.Default.connetion);
            try
            {
                // Parameterized query to prevent SQL injection
                SqlDataAdapter sqd = new SqlDataAdapter(
                    "SELECT count(*) FROM admin_user WHERE user_name=@username AND password=@password",
                    conn);
                sqd.SelectCommand.Parameters.AddWithValue("@username", username);
                sqd.SelectCommand.Parameters.AddWithValue("@password", password);

                DataTable dt = new DataTable();
                sqd.Fill(dt);

                if (dt.Rows[0][0].ToString() == "1")
                {
                    conn.Open();

                    // Update last login timestamp
                    SqlCommand updateCmd = new SqlCommand(
                        "UPDATE admin_user SET last_login = @lastLogin WHERE user_name = @username",
                        conn);
                    updateCmd.Parameters.AddWithValue("@lastLogin", DateTime.Now);
                    updateCmd.Parameters.AddWithValue("@username", username);
                    updateCmd.ExecuteNonQuery();

                    // Get admin details
                    SqlCommand cmd = new SqlCommand(
                        "SELECT Id, user_name FROM admin_user WHERE user_name=@username",
                        conn);
                    cmd.Parameters.AddWithValue("@username", username);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        Properties.Settings.Default.CurrentUserID = reader.GetInt32(0);
                        Properties.Settings.Default.CurrentUserName = reader.GetString(1);
                        Properties.Settings.Default.CurrentUserType = "admin";
                        Properties.Settings.Default.Save();
                    }
                    reader.Close();

                    this.Hide();
                    Super_pansar_menu login = new Super_pansar_menu();
                    login.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Invalid username or password", "Login Failed",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error during login: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        private void Form2_Resize(object sender, EventArgs e)
        {
            // Center the login panel when form is resized
            loginPanel.Location = new Point(
                (mainPanel.Width - loginPanel.Width) / 2,
                (mainPanel.Height - loginPanel.Height) / 2);
        }

        private void loginButton_MouseEnter(object sender, EventArgs e)
        {
            loginButton.BackColor = Color.FromArgb(50, 50, 50);
            exitButton.FlatAppearance.BorderColor = Color.FromArgb(50, 50, 50);
        }

        private void loginButton_MouseLeave(object sender, EventArgs e)
        {
            loginButton.BackColor = Color.Black;
            exitButton.FlatAppearance.BorderColor = Color.Black;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // Center the login panel initially
            loginPanel.Location = new Point(
                (mainPanel.Width - loginPanel.Width) / 2,
                (mainPanel.Height - loginPanel.Height) / 2);

            // Load header image if needed
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.connetion);
            try
            {
                byte[] getImg = new byte[0];
                SqlDataAdapter da = new SqlDataAdapter();
                SqlCommand cmd = new SqlCommand(
                    "SELECT image FROM settings WHERE setting_key='header' AND settings.type='image'",
                    conn);
                da.SelectCommand = cmd;
                cmd.CommandType = CommandType.Text;
                DataSet ds = new DataSet();
                da.Fill(ds);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    getImg = (byte[])dr["image"];
                }

                if (getImg != null && getImg.Length > 0)
                {
                    byte[] imgData = getImg;
                    MemoryStream stream = new MemoryStream(imgData);
                    // You can use the stream to set an image if needed
                }
            }
            catch (Exception ex)
            {
                // Silently handle error - image loading is not critical
                Console.WriteLine("Error loading header image: " + ex.Message);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        private void mainPanel_Paint(object sender, PaintEventArgs e)
        {
        }

        private void showPasswordCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            passwordTextBox.PasswordChar = showPasswordCheckBox.Checked ? '\0' : '•';
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 main = new Form1();
            main.ShowDialog();
        }

        private void exitButton_MouseEnter(object sender, EventArgs e)
        {
            exitButton.ForeColor = Color.White;
            exitButton.BackColor = Color.Black;
        }

        private void exitButton_MouseLeave(object sender, EventArgs e)
        {
            exitButton.ForeColor = Color.Black;
            exitButton.BackColor = Color.White;
        }
    }
}