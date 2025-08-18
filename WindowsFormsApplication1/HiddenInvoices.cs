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
    public partial class HiddenInvoices : Form
    {
        public static string invoice_id;
        SqlConnection sqlconnection;
        SqlCommand sqlcommand;
        SqlCommandBuilder scb;
        string ConnectionString = Properties.Settings.Default.connetion;
        string Query;
        DataSet dataset;
        DataTable datatable;
        SqlDataAdapter sqladapter;
        private bool isInitialLoad = true;

        public HiddenInvoices()
        {
            InitializeComponent();
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd MMM yyyy HH:mm:ss";
            dateTimePicker1.Value = DateTime.Now.AddDays(-30); // Default to 30 days ago

            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.CustomFormat = "dd MMM yyyy HH:mm:ss";
            dateTimePicker2.Value = DateTime.Now;
        }

        private void HiddenInvoices_Load(object sender, EventArgs e)
        {
            LoadDataWithDefaultFilter();
            isInitialLoad = false;
        }

        private void LoadDataWithDefaultFilter()
        {
            sqlconnection = new SqlConnection(ConnectionString);

            // Default query with 30-day filter and newest first
            Query = @"select invoice_id, customer_name, company_name, cell_number, invoice_date, 
                     total_bill, amount_paid, change, amount_remanning 
                     from invoice 
                     where company_name !='Manual Entry' AND is_hidden=1 
                     AND invoice_date BETWEEN @StartDate AND @EndDate
                     ORDER BY invoice_date DESC";

            sqlcommand = new SqlCommand(Query, sqlconnection);
            sqlcommand.Parameters.AddWithValue("@StartDate", dateTimePicker1.Value);
            sqlcommand.Parameters.AddWithValue("@EndDate", dateTimePicker2.Value);

            sqladapter = new SqlDataAdapter();
            datatable = new DataTable();
            sqladapter.SelectCommand = sqlcommand;
            sqladapter.Fill(datatable);

            SetupDataGridView();
        }

        private void LoadAllData()
        {
            sqlconnection = new SqlConnection(ConnectionString);

            // Query without date filter
            Query = @"select invoice_id, customer_name, company_name, cell_number, invoice_date, 
                     total_bill, amount_paid, change, amount_remanning 
                     from invoice 
                     where company_name !='Manual Entry' AND is_hidden=1 
                     ORDER BY invoice_date DESC";

            sqlcommand = new SqlCommand(Query, sqlconnection);

            sqladapter = new SqlDataAdapter();
            datatable = new DataTable();
            sqladapter.SelectCommand = sqlcommand;
            sqladapter.Fill(datatable);

            SetupDataGridView();
        }

        private void SetupDataGridView()
        {
            DataGridViewRow row = this.dataGridView1.RowTemplate;
            row.Height = 35;
            row.MinimumHeight = 20;
            Font font_head = new Font(dataGridView1.DefaultCellStyle.Font.FontFamily, 12, FontStyle.Bold);
            row.DefaultCellStyle.Font = font_head;
            dataGridView1.DataSource = datatable;

            // Column width settings
            dataGridView1.Columns[0].Width = 130;
            dataGridView1.Columns[1].Width = 230;
            dataGridView1.Columns[2].Width = 230;
            dataGridView1.Columns[3].Width = 250;
            dataGridView1.Columns[4].Width = 200;
            dataGridView1.Columns[5].Width = 120;
            dataGridView1.Columns[6].Width = 110;
            dataGridView1.Columns[7].Width = 110;
            dataGridView1.Columns[8].Width = 120;

            // Header text
            dataGridView1.Columns[0].HeaderText = "Invoice Id";
            dataGridView1.Columns[1].HeaderText = "Customer Name";
            dataGridView1.Columns[2].HeaderText = "Company Name";
            dataGridView1.Columns[3].HeaderText = "Cell Number";
            dataGridView1.Columns[4].HeaderText = "Invoice Date";
            dataGridView1.Columns[5].HeaderText = "Total";
            dataGridView1.Columns[6].HeaderText = "Paid";
            dataGridView1.Columns[7].HeaderText = "Change";
            dataGridView1.Columns[8].HeaderText = "Remaining";

            // Header font
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.HeaderCell.Style.Font = font_head;
            }

            // Make certain columns read-only
            int n = Convert.ToInt32(dataGridView1.Rows.Count.ToString());
            for (int i = 0; i < n; i++)
            {
                dataGridView1.Rows[i].Cells[0].ReadOnly = true;
                dataGridView1.Rows[i].Cells[5].ReadOnly = true;
                dataGridView1.Rows[i].Cells[7].ReadOnly = true;
                dataGridView1.Rows[i].Cells[8].ReadOnly = true;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Reset all filters and show ALL data
            dateTimePicker1.Value = DateTime.Now.AddDays(-30);
            dateTimePicker2.Value = DateTime.Now;
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            LoadAllData(); // This loads ALL data, not just last 30 days
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(textBox3.Text))
            {
                DataView DV = new DataView(datatable);
                DV.RowFilter = string.Format("Convert(invoice_id, 'System.String') like '%{0}%'", textBox3.Text.ToString());
                dataGridView1.DataSource = DV;
            }
            else
            {
                dataGridView1.DataSource = datatable;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(textBox1.Text))
            {
                DataView DV = new DataView(datatable);
                DV.RowFilter = string.Format("customer_name LIKE '%{0}%'", textBox1.Text);
                dataGridView1.DataSource = DV;
            }
            else
            {
                dataGridView1.DataSource = datatable;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(textBox2.Text))
            {
                DataView DV = new DataView(datatable);
                DV.RowFilter = string.Format("cell_number LIKE '%{0}%'", textBox2.Text);
                dataGridView1.DataSource = DV;
            }
            else
            {
                dataGridView1.DataSource = datatable;
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialLoad)
            {
                if (!String.IsNullOrEmpty(dateTimePicker1.Value.ToString()) && !String.IsNullOrEmpty(dateTimePicker2.Value.ToString()))
                {
                    // Reload data from database with new date range
                    sqlconnection = new SqlConnection(ConnectionString);
                    Query = @"select invoice_id, customer_name, company_name, cell_number, invoice_date, 
                             total_bill, amount_paid, change, amount_remanning 
                             from invoice 
                             where company_name !='Manual Entry' AND is_hidden=1 
                             AND invoice_date BETWEEN @StartDate AND @EndDate
                             ORDER BY invoice_date DESC";

                    sqlcommand = new SqlCommand(Query, sqlconnection);
                    sqlcommand.Parameters.AddWithValue("@StartDate", dateTimePicker1.Value);
                    sqlcommand.Parameters.AddWithValue("@EndDate", dateTimePicker2.Value);

                    datatable = new DataTable();
                    sqladapter.SelectCommand = sqlcommand;
                    sqladapter.Fill(datatable);
                    dataGridView1.DataSource = datatable;
                }
            }
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialLoad)
            {
                if (!String.IsNullOrEmpty(dateTimePicker2.Value.ToString()) && !String.IsNullOrEmpty(dateTimePicker1.Value.ToString()))
                {
                    // Reload data from database with new date range
                    sqlconnection = new SqlConnection(ConnectionString);
                    Query = @"select invoice_id, customer_name, company_name, cell_number, invoice_date, 
                             total_bill, amount_paid, change, amount_remanning 
                             from invoice 
                             where company_name !='Manual Entry' AND is_hidden=1 
                             AND invoice_date BETWEEN @StartDate AND @EndDate
                             ORDER BY invoice_date DESC";

                    sqlcommand = new SqlCommand(Query, sqlconnection);
                    sqlcommand.Parameters.AddWithValue("@StartDate", dateTimePicker1.Value);
                    sqlcommand.Parameters.AddWithValue("@EndDate", dateTimePicker2.Value);

                    datatable = new DataTable();
                    sqladapter.SelectCommand = sqlcommand;
                    sqladapter.Fill(datatable);
                    dataGridView1.DataSource = datatable;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Int32 selectedRowCount = dataGridView1.Rows.GetRowCount(DataGridViewElementStates.Selected);
            if (selectedRowCount > 0)
            {
                SqlConnection conn = new SqlConnection(Properties.Settings.Default.connetion);
                conn.Open();
                for (int i = 0; i < selectedRowCount; i++)
                {
                    string invoice_id = dataGridView1.SelectedRows[i].Cells[0].Value.ToString();
                    string update = "UPDATE invoice SET is_hidden='0' where invoice_id='" + invoice_id + "'";
                    SqlCommand update_cmd = new SqlCommand(update, conn);
                    update_cmd.ExecuteNonQuery();
                }
                conn.Close();
                LoadDataWithDefaultFilter();
                MessageBox.Show("Selected Customer Has Been Hidden.", "alert", MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show("No Row Has Been Selected. Select Row to Hide.", "alert", MessageBoxButtons.OK);
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            // No changes needed
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // No changes needed
        }

        private void AllRecords_Click(object sender, EventArgs e)
        {
            // No changes needed
        }
    }
}