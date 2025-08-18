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
    public partial class account : Form
    {
        SqlConnection sqlconnection;
        SqlCommand sqlcommand;
        string ConnectionString = Properties.Settings.Default.connetion;
        string Query;
        DataSet dataset;
        DataTable datatable;
        DataTable datatable_1;
        SqlDataAdapter sqladapter;

        public account()
        {
            InitializeComponent();
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd MMM yyyy HH:mm:ss";
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.CustomFormat = "dd MMM yyyy HH:mm:ss";
            dateTimePicker2.Value = DateTime.Now;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(dateTimePicker1.Text))
            {
                DataView DV = new DataView(datatable);
                DV.RowFilter = string.Format("expense_date >= '{0:yyyy-MM-dd}' AND expense_date <= '{1:yyyy-MM-dd}'", dateTimePicker1.Value, dateTimePicker2.Value.AddDays(1));
                dataGridView1.DataSource = DV;

                DataView DV_1 = new DataView(datatable_1);
                DV_1.RowFilter = string.Format("invoice_date >= '{0:yyyy-MM-dd}' AND invoice_date <= '{1:yyyy-MM-dd}'", dateTimePicker1.Value, dateTimePicker2.Value.AddDays(1));
                dataGridView2.DataSource = DV_1;
            }
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(dateTimePicker2.Text))
            {
                DataView DV = new DataView(datatable);
                DV.RowFilter = string.Format("expense_date >= '{0:yyyy-MM-dd}' AND expense_date <= '{1:yyyy-MM-dd}'", dateTimePicker1.Value, dateTimePicker2.Value.AddDays(1));
                dataGridView1.DataSource = DV;

                DataView DV_1 = new DataView(datatable_1);
                DV_1.RowFilter = string.Format("invoice_date >= '{0:yyyy-MM-dd}' AND invoice_date <= '{1:yyyy-MM-dd}'", dateTimePicker1.Value, dateTimePicker2.Value.AddDays(1));
                dataGridView2.DataSource = DV_1;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();

        }
        private void update_total()
        {
            decimal expense = 0;
            decimal credit = 0;
            decimal amount_paid = 0;

            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                if (row.Cells[2].Value != null && decimal.TryParse(row.Cells[2].Value.ToString(), out decimal paid))
                {
                    amount_paid += paid;
                }
                if (row.Cells[3].Value != null && decimal.TryParse(row.Cells[3].Value.ToString(), out decimal cred))
                {
                    credit += cred;
                }
            }

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[1].Value != null && decimal.TryParse(row.Cells[1].Value.ToString(), out decimal exp))
                {
                    expense += exp;
                }
            }

            label3.Text = expense.ToString("N2");
            label5.Text = amount_paid.ToString("N2");
            label8.Text = credit.ToString("N2");
            label9.Text = (amount_paid - expense).ToString("N2");
        }

        private void account_Load(object sender, EventArgs e)
        {
            create_product_table();
            crete_expense_table();

            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                if (row.Cells[2].Value != null && row.Cells[3].Value != null)
                {
                    if (decimal.TryParse(row.Cells[2].Value.ToString(), out decimal amount_paid) &&
                        decimal.TryParse(row.Cells[3].Value.ToString(), out decimal amount_remaining))
                    {
                        if (amount_remaining < 0)
                        {
                            row.Cells[3].Value = 0m;
                            row.Cells[2].Value = amount_paid + amount_remaining;
                        }
                    }
                }
            }

            update_total();
        }

        public void crete_expense_table()
        {
            sqlconnection = new SqlConnection(ConnectionString);
            Query = "SELECT expense_name, expense_amount, expense_date FROM expense";
            sqlcommand = new SqlCommand(Query, sqlconnection);
            sqladapter = new SqlDataAdapter();
            datatable = new DataTable();

            // Add columns with proper data types
            datatable.Columns.Add("Expense Name", typeof(string));
            datatable.Columns.Add("Expense Value", typeof(decimal));
            datatable.Columns.Add("Expense Date", typeof(DateTime));

            try
            {
                sqlconnection.Open();
                SqlDataReader reader = sqlcommand.ExecuteReader();
                while (reader.Read())
                {
                    DataRow row = datatable.NewRow();
                    row["Expense Name"] = reader["expense_name"].ToString();

                    // Safely parse the expense amount
                    if (decimal.TryParse(reader["expense_amount"].ToString(), out decimal amount))
                    {
                        row["Expense Value"] = amount;
                    }
                    else
                    {
                        row["Expense Value"] = 0m;
                    }

                    row["Expense Date"] = reader["expense_date"];
                    datatable.Rows.Add(row);
                }
            }
            finally
            {
                sqlconnection.Close();
            }
            DataGridViewRow gridRow = this.dataGridView1.RowTemplate;
            gridRow.Height = 35;
            gridRow.MinimumHeight = 20;
            Font font_head = new Font(dataGridView1.DefaultCellStyle.Font.FontFamily, 12, FontStyle.Bold);
            gridRow.DefaultCellStyle.Font = font_head;
            dataGridView1.DataSource = datatable;

            dataGridView1.Columns[0].Width = 150;
            dataGridView1.Columns[1].Width = 100;
            dataGridView1.Columns[2].Width = 160;

            dataGridView1.Columns[0].HeaderText = "Expense Name";
            dataGridView1.Columns[1].HeaderText = "Expense Value";
            dataGridView1.Columns[2].HeaderText = "Expense Date";

            dataGridView1.Columns[0].HeaderCell.Style.Font = font_head;
            dataGridView1.Columns[1].HeaderCell.Style.Font = font_head;
            dataGridView1.Columns[2].HeaderCell.Style.Font = font_head;
        }

        public void create_product_table()
        {
            sqlconnection = new SqlConnection(ConnectionString);
            Query = "SELECT customer_name, total_bill, amount_paid, amount_remanning, invoice_date FROM invoice";
            sqlcommand = new SqlCommand(Query, sqlconnection);
            sqladapter = new SqlDataAdapter();
            datatable_1 = new DataTable();


            datatable_1.Columns.Add("Customer Name", typeof(string));
            datatable_1.Columns.Add("Total Bill", typeof(decimal));
            datatable_1.Columns.Add("Amount Paid", typeof(decimal));
            datatable_1.Columns.Add("Credit", typeof(decimal));
            datatable_1.Columns.Add("Sale Date", typeof(DateTime));

            try
            {
                sqlconnection.Open();
                SqlDataReader reader = sqlcommand.ExecuteReader();
                while (reader.Read())
                {
                    DataRow row = datatable_1.NewRow();
                    row["Customer Name"] = reader["customer_name"].ToString();

                    decimal.TryParse(reader["total_bill"].ToString(), out decimal totalBill);
                    decimal.TryParse(reader["amount_paid"].ToString(), out decimal amountPaid);
                    decimal.TryParse(reader["amount_remanning"].ToString(), out decimal amountRemaining);

                    row["Total Bill"] = totalBill;
                    row["Amount Paid"] = amountPaid;
                    row["Credit"] = amountRemaining < 0 ? 0m : amountRemaining; 
                    row["Sale Date"] = reader["invoice_date"];

                    datatable_1.Rows.Add(row);
                }
            }
            finally
            {
                sqlconnection.Close();
            }

            DataGridViewRow gridRow = this.dataGridView2.RowTemplate;
            gridRow.Height = 35;
            gridRow.MinimumHeight = 20;
            Font font_head = new Font(dataGridView2.DefaultCellStyle.Font.FontFamily, 12, FontStyle.Bold);
            gridRow.DefaultCellStyle.Font = font_head;

            dataGridView2.DataSource = datatable_1;

            dataGridView2.Columns[0].Width = 140;
            dataGridView2.Columns[1].Width = 90;
            dataGridView2.Columns[2].Width = 90;
            dataGridView2.Columns[3].Width = 90;
            dataGridView2.Columns[4].Width = 160;

            dataGridView2.Columns[0].HeaderText = "Customer Name";
            dataGridView2.Columns[1].HeaderText = "Total Bill";
            dataGridView2.Columns[2].HeaderText = "Amount Paid";
            dataGridView2.Columns[3].HeaderText = "Credit";
            dataGridView2.Columns[4].HeaderText = "Sale Date";

            dataGridView2.Columns[0].HeaderCell.Style.Font = font_head;
            dataGridView2.Columns[1].HeaderCell.Style.Font = font_head;
            dataGridView2.Columns[2].HeaderCell.Style.Font = font_head;
            dataGridView2.Columns[3].HeaderCell.Style.Font = font_head;
            dataGridView2.Columns[4].HeaderCell.Style.Font = font_head;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            account_Load(sender,e);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
