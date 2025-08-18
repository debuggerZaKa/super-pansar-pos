using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class CreatorInfoForm : Form
    {
        private string invoiceId;
        private string connectionString = Properties.Settings.Default.connetion;

        public CreatorInfoForm(string invoiceId)
        {
            InitializeComponent();
            this.invoiceId = invoiceId;
            this.Load += CreatorInfoForm_Load;
        }

        private void CreatorInfoForm_Load(object sender, EventArgs e)
        {
            LoadCreatorInfo();
        }

        private void LoadCreatorInfo()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT creator_id, creator_name, creator_type FROM invoice WHERE invoice_id = @InvoiceId";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@InvoiceId", invoiceId);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        lblIdValue.Text = reader["creator_id"] != DBNull.Value ? reader["creator_id"].ToString() : "N/A";
                        lblNameValue.Text = reader["creator_name"] != DBNull.Value ? reader["creator_name"].ToString() : "N/A";
                        lblTypeValue.Text = reader["creator_type"] != DBNull.Value ? reader["creator_type"].ToString() : "N/A";
                    }
                    else
                    {
                        lblIdValue.Text = "N/A";
                        lblNameValue.Text = "N/A";
                        lblTypeValue.Text = "N/A";
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading creator information: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}