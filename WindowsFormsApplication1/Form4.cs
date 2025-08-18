using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void Login_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Password_TextChanged(object sender, EventArgs e)
        {

        }

        private void username_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            String oldPassword = textBox2.Text;
            String newPassword = Password.Text;
            String repeatPassword = textBox1.Text;

            if (string.IsNullOrWhiteSpace(oldPassword) || string.IsNullOrWhiteSpace(newPassword) || string.IsNullOrWhiteSpace(repeatPassword))
            {
                MessageBox.Show("Data is missing", "Fill All fields", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (String.Equals(newPassword, repeatPassword, StringComparison.CurrentCulture))
            {
                SqlConnection conn = new SqlConnection(Properties.Settings.Default.connetion);
                conn.Open();
                SqlCommand command = new SqlCommand("Select * from admin_user where (user_name='admin' AND password='" + oldPassword + "') OR (user_name='master' AND password='" + oldPassword + "')", conn);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        update_password(newPassword);
                        return;
                    }
                    else
                    {
                        MessageBox.Show(" please enter the correct password", "Invalid Password", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                conn.Close();
            }
            else {
                MessageBox.Show("Password and new password does not match", "Invalid new password", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void update_password(String password) {
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.connetion);
            SqlCommand command = new SqlCommand("UPDATE admin_user SET password='"+password+"' WHERE user_name='admin';", conn);
            try
            {
                conn.Open();
                command.ExecuteNonQuery();
                MessageBox.Show("Password is updated successfully.");
                this.Hide();
                Form2 login = new Form2();
                login.Show();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Password is not updated.");
                MessageBox.Show(ex.Message);
            }
            conn.Close();

        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Super_pansar_menu pansar = new Super_pansar_menu();
            pansar.Show();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
