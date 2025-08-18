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

namespace WindowsFormsApplication1
{
    public partial class SuperPansar_store_Billing : Form
    {
        private bool isDefaultFilterApplied = true;

        public SuperPansar_store_Billing()
        {
            InitializeComponent();
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd MMM yyyy HH:mm:ss";
            dateTimePicker1.Value = DateTime.Now.AddDays(-30); // Default to 30 days ago

            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.CustomFormat = "dd MMM yyyy HH:mm:ss";
            dateTimePicker2.Value = DateTime.Now;
        }
        public static string invoice_id;
        SqlConnection sqlconnection;
        SqlCommand sqlcommand;
        SqlCommandBuilder scb;
        string ConnectionString = Properties.Settings.Default.connetion;
        string Query;
        DataSet dataset;
        DataTable datatable;
        SqlDataAdapter sqladapter;
        private void fontDialog1_Apply(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {
            this.Hide();
            Super_Pansar_store_invoice invoice = new Super_Pansar_store_invoice();
            SuperPansar_store_Billing.invoice_id = "";
            invoice.ShowDialog();
        }

        private void button12_Click(object sender, EventArgs e)
        {

        }

        private void SuperPansar_store_Billing_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData(bool applyDefaultFilter = true)
        {
            isDefaultFilterApplied = applyDefaultFilter;
            sqlconnection = new SqlConnection(ConnectionString);

            if (applyDefaultFilter)
            {
                Query = "select invoice_id,customer_name, company_name,cell_number,invoice_date,total_bill,amount_paid,change,amount_remanning from invoice where company_name !='Manual Entry' AND is_hidden=0 AND invoice_date >= @StartDate AND invoice_date <= @EndDate ORDER BY invoice_date DESC";
                sqlcommand = new SqlCommand(Query, sqlconnection);
                sqlcommand.Parameters.AddWithValue("@StartDate", dateTimePicker1.Value);
                sqlcommand.Parameters.AddWithValue("@EndDate", dateTimePicker2.Value);
            }
            else
            {
                Query = "select invoice_id,customer_name, company_name,cell_number,invoice_date,total_bill,amount_paid,change,amount_remanning from invoice where company_name !='Manual Entry' AND is_hidden=0 ORDER BY invoice_date DESC";
                sqlcommand = new SqlCommand(Query, sqlconnection);
            }

            sqladapter = new SqlDataAdapter();
            datatable = new DataTable();
            sqladapter.SelectCommand = sqlcommand;
            sqladapter.Fill(datatable);

            SetupDataGridView();
            update_totals();
        }

        private void SetupDataGridView()
        {
            DataGridViewRow row = this.dataGridView1.RowTemplate;
            row.Height = 35;
            row.MinimumHeight = 20;
            Font font_head = new Font(dataGridView1.DefaultCellStyle.Font.FontFamily, 12, FontStyle.Bold);
            row.DefaultCellStyle.Font = font_head;
            dataGridView1.DataSource = datatable;

            dataGridView1.Columns[0].Width = 100;
            dataGridView1.Columns[1].Width = 200;
            dataGridView1.Columns[2].Width = 220;
            dataGridView1.Columns[3].Width = 200;
            dataGridView1.Columns[4].Width = 250;
            dataGridView1.Columns[5].Width = 110;
            dataGridView1.Columns[6].Width = 110;
            dataGridView1.Columns[7].Width = 110;
            dataGridView1.Columns[8].Width = 110;

            dataGridView1.Columns[0].HeaderText = "Invoice Id";
            dataGridView1.Columns[1].HeaderText = "Customer Name";
            dataGridView1.Columns[2].HeaderText = "Company Name";
            dataGridView1.Columns[3].HeaderText = "Cell Number";
            dataGridView1.Columns[4].HeaderText = "Invoice Date";
            dataGridView1.Columns[5].HeaderText = "Total";
            dataGridView1.Columns[6].HeaderText = "Paid";
            dataGridView1.Columns[7].HeaderText = "Change";
            dataGridView1.Columns[8].HeaderText = "Remaining";

            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.HeaderCell.Style.Font = font_head;
            }

            int n = Convert.ToInt32(dataGridView1.Rows.Count.ToString());
            for (int i = 0; i < n; i++)
            {
                dataGridView1.Rows[i].Cells[0].ReadOnly = true;
                dataGridView1.Rows[i].Cells[5].ReadOnly = true;
                dataGridView1.Rows[i].Cells[7].ReadOnly = true;
                dataGridView1.Rows[i].Cells[8].ReadOnly = true;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            ApplySearchFilters();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            ApplySearchFilters();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            ApplySearchFilters();
        }

        private void ApplySearchFilters()
        {
            if (datatable == null) return;

            DataView DV = new DataView(datatable);
            string filter = "";

            if (isDefaultFilterApplied)
            {
                filter = string.Format("invoice_date >= '{0:yyyy-MM-dd}' AND invoice_date <= '{1:yyyy-MM-dd}'",
                    dateTimePicker1.Value, dateTimePicker2.Value.AddDays(1));
            }

            if (!String.IsNullOrEmpty(textBox1.Text))
            {
                if (!string.IsNullOrEmpty(filter)) filter += " AND ";
                filter += string.Format("customer_name LIKE '%{0}%'", textBox1.Text);
            }

            if (!String.IsNullOrEmpty(textBox2.Text))
            {
                if (!string.IsNullOrEmpty(filter)) filter += " AND ";
                filter += string.Format("cell_number LIKE '%{0}%'", textBox2.Text);
            }

            if (!String.IsNullOrEmpty(textBox3.Text))
            {
                if (!string.IsNullOrEmpty(filter)) filter += " AND ";
                filter += string.Format("Convert(invoice_id, 'System.String') like '%{0}%'", textBox3.Text.ToString());
            }

            DV.RowFilter = filter;
            dataGridView1.DataSource = DV;
            update_totals();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            isDefaultFilterApplied = true;
            LoadData(true);
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            isDefaultFilterApplied = true;
            LoadData(true);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {
                int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;

                DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];

                invoice_id = Convert.ToString(selectedRow.Cells[0].Value);
                double total = Convert.ToDouble(selectedRow.Cells[5].Value);
                if (String.IsNullOrEmpty(invoice_id))
                {
                    MessageBox.Show("You Haven't Selected any Row", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (total <= 0)
                    {
                        MessageBox.Show("No Product Has Been Sold For This Invoice", "alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        Super_Pansar_store_invoice sps = new Super_Pansar_store_invoice();
                        sps.ShowDialog();
                    }
                }
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            Super_pansar_menu store = new Super_pansar_menu();
            store.Show();
        }

        private void update_totals()
        {
            int counter;
            Double total = 0;
            Double received = 0;
            Double remaining = 0;
            Double change = 0;

            for (counter = 0; counter < (dataGridView1.Rows.Count); counter++)
            {
                Double.TryParse(dataGridView1.Rows[counter].Cells[5].Value?.ToString() ?? "0", out double cellTotal);
                Double.TryParse(dataGridView1.Rows[counter].Cells[6].Value?.ToString() ?? "0", out double cellReceived);
                Double.TryParse(dataGridView1.Rows[counter].Cells[7].Value?.ToString() ?? "0", out double cellChange);
                Double.TryParse(dataGridView1.Rows[counter].Cells[8].Value?.ToString() ?? "0", out double cellRemaining);

                total += cellTotal;
                received += cellReceived;
                change += cellChange;
                remaining += cellRemaining;
            }

            label12.Text = total.ToString();
            label10.Text = (received + change).ToString();
            label3.Text = (total - (received + change)).ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Reset all filters
            isDefaultFilterApplied = false;
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            dateTimePicker1.Value = DateTime.Now.AddDays(-30);
            dateTimePicker2.Value = DateTime.Now;

            // Reload all data without any filters
            LoadData(false);
            MessageBox.Show("All filters have been reset", "alert", MessageBoxButtons.OK);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Int32 selectedCount = dataGridView1.SelectedRows.Count;
            if (selectedCount > 0)
            {
                for (int i = 0; i < selectedCount; i++)
                {
                    dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);
                }
                scb = new SqlCommandBuilder(sqladapter);
                sqladapter.Update(datatable);
                LoadData(isDefaultFilterApplied);
                MessageBox.Show("Row has been deleted ", "alert", MessageBoxButtons.OK);
            }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                if (String.IsNullOrEmpty(dataGridView1[e.ColumnIndex, e.RowIndex].Value.ToString()))
                {
                    dataGridView1[e.ColumnIndex, e.RowIndex].Value = "";
                }
            }
            if (e.ColumnIndex == 2)
            {
                if (String.IsNullOrEmpty(dataGridView1[e.ColumnIndex, e.RowIndex].Value.ToString()))
                {
                    dataGridView1[e.ColumnIndex, e.RowIndex].Value = "";
                }
            }
            if (e.ColumnIndex == 3)
            {
                if (String.IsNullOrEmpty(dataGridView1[e.ColumnIndex, e.RowIndex].Value.ToString()))
                {
                    dataGridView1[e.ColumnIndex, e.RowIndex].Value = "0";
                }
            }
            if (e.ColumnIndex == 4)
            {
                if (String.IsNullOrEmpty(dataGridView1[e.ColumnIndex, e.RowIndex].Value.ToString()))
                {
                    dataGridView1[e.ColumnIndex, e.RowIndex].Value = DateTime.Now;
                }
            }
            if (e.ColumnIndex == 6)
            {
                if (String.IsNullOrEmpty(dataGridView1[e.ColumnIndex, e.RowIndex].Value.ToString()))
                {
                    dataGridView1[e.ColumnIndex, e.RowIndex].Value = 0;
                }
                Double total = Double.Parse(dataGridView1[5, e.RowIndex].Value.ToString());
                Double amount_paid = Double.Parse(dataGridView1[e.ColumnIndex, e.RowIndex].Value.ToString());
                Double change = 0;
                Double remaining = total - amount_paid;
                if (remaining < 0)
                {
                    change = remaining;
                    remaining = 0;
                }
                else
                {
                    change = 0;
                }
                dataGridView1[7, e.RowIndex].Value = change.ToString();
                dataGridView1[8, e.RowIndex].Value = remaining.ToString();
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            Int32 selectedRowCount = dataGridView1.Rows.GetRowCount(DataGridViewElementStates.Selected);
            if (selectedRowCount > 0)
            {
                SqlConnection conn = new SqlConnection(Properties.Settings.Default.connetion);
                conn.Open();
                for (int i = 0; i < selectedRowCount; i++)
                {
                    string invoice_id = dataGridView1.SelectedRows[i].Cells[0].Value.ToString();
                    string update = "UPDATE invoice SET is_hidden='1' where invoice_id='" + invoice_id + "'";
                    SqlCommand update_cmd = new SqlCommand(update, conn);
                    update_cmd.ExecuteNonQuery();
                }
                conn.Close();
                LoadData(isDefaultFilterApplied);
                MessageBox.Show("Selected Customer Has Been Hidden.", "alert", MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show("No Row Has Been Selected. Select Row to Hide.", "alert", MessageBoxButtons.OK);
            }
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];
                Super_Pansar_store_invoice sps = new Super_Pansar_store_invoice();
                sps.textBox6.Text = selectedRow.Cells[1].Value.ToString();
                sps.textBox5.Text = selectedRow.Cells[2].Value.ToString();
                sps.textBox4.Text = selectedRow.Cells[3].Value.ToString();
                sps.ShowDialog();
            }
            else
            {
                MessageBox.Show("Select One Row TO Add New Invoice.", "alert", MessageBoxButtons.OK);
            }
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Calculator.Calculator cal = new Calculator.Calculator();
            cal.ShowDialog();
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {
                int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];
                string invoiceId = Convert.ToString(selectedRow.Cells[0].Value);

                if (!string.IsNullOrEmpty(invoiceId))
                {
                    CreatorInfoForm creatorInfoForm = new CreatorInfoForm(invoiceId);
                    creatorInfoForm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("No invoice selected or invalid invoice ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select an invoice first.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}