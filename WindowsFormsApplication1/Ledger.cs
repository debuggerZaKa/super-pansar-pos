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
    public partial class Ledger : Form
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
        private bool isDefaultFilterActive = true;

        public Ledger()
        {
            InitializeComponent();
        }

        public void Ledger_Load(object sender, EventArgs e)
        {
            this.datatable = new DataTable();
            datatable.Columns.Add("id", typeof(String));
            datatable.Columns.Add("customer_name", typeof(String));
            datatable.Columns.Add("company_name", typeof(String));
            datatable.Columns.Add("cell_number", typeof(String));
            datatable.Columns.Add("date", typeof(DateTime));
            datatable.Columns.Add("debit", typeof(decimal));
            datatable.Columns.Add("credit", typeof(decimal));

            // Set default date range (last 30 days)
            dateTimePicker2.Value = DateTime.Now;
            dateTimePicker1.Value = DateTime.Now.AddDays(-30);

            using (SqlConnection conne = new SqlConnection(Properties.Settings.Default.connetion))
            {
                SqlCommand cmd = new SqlCommand("SELECT invoice_id, customer_name, company_name, cell_number, invoice_date, total_bill, amount_paid, change, amount_remanning FROM invoice WHERE company_name !='Manual Entry' AND is_hidden=0 ORDER BY invoice_date DESC", conne);
                conne.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    decimal.TryParse(reader.GetString(5), out decimal total);
                    decimal.TryParse(reader.GetString(6), out decimal amount_paid);
                    decimal.TryParse(reader.GetString(7), out decimal change);
                    decimal.TryParse(reader.GetString(8), out decimal remaining);

                    DataRow newRow = datatable.NewRow();
                    newRow["id"] = reader.GetInt32(0).ToString();
                    newRow["customer_name"] = reader.GetValue(1).ToString();
                    newRow["company_name"] = reader.GetValue(2).ToString();
                    newRow["cell_number"] = reader.GetValue(3).ToString();
                    newRow["date"] = reader.GetValue(4);
                    newRow["debit"] = total > 0 ? total : 0m;
                    newRow["credit"] = amount_paid > 0 ?
                        (change >= 0 ? amount_paid : (amount_paid + change)) : 0m;

                    datatable.Rows.Add(newRow);
                }
                conne.Close();
            }

            DataGridViewRow row = this.dataGridView1.RowTemplate;
            row.Height = 45;
            row.MinimumHeight = 20;
            Font font_head = new Font(dataGridView1.DefaultCellStyle.Font.FontFamily, 12, FontStyle.Bold);
            row.DefaultCellStyle.Font = font_head;

            dataGridView1.DataSource = datatable;

            dataGridView1.Columns[1].Width = 350;
            dataGridView1.Columns[2].Width = 400;
            dataGridView1.Columns[4].Width = 370;
            dataGridView1.Columns[5].Width = 200;
            dataGridView1.Columns[6].Width = 200;

            dataGridView1.Columns[0].HeaderText = "Invoice Id";
            dataGridView1.Columns[1].HeaderText = "Customer Name";
            dataGridView1.Columns[2].HeaderText = "Company Name";
            dataGridView1.Columns[3].HeaderText = "Number";
            dataGridView1.Columns[4].HeaderText = "Date";
            dataGridView1.Columns[5].HeaderText = "Debit";
            dataGridView1.Columns[6].HeaderText = "Credit";

            dataGridView1.Columns[0].HeaderCell.Style.Font = font_head;
            dataGridView1.Columns[1].HeaderCell.Style.Font = font_head;
            dataGridView1.Columns[2].HeaderCell.Style.Font = font_head;
            dataGridView1.Columns[3].HeaderCell.Style.Font = font_head;
            dataGridView1.Columns[4].HeaderCell.Style.Font = font_head;
            dataGridView1.Columns[5].HeaderCell.Style.Font = font_head;
            dataGridView1.Columns[6].HeaderCell.Style.Font = font_head;

            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[3].Visible = false;

            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd MMM yyyy HH:mm:ss";

            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.CustomFormat = "dd MMM yyyy HH:mm:ss";

            // Apply default filter (last 30 days)
            ApplyDateFilter();
            isDefaultFilterActive = true;
            update_totals();
        }

        private void ApplyDateFilter()
        {
            string dateFilter = string.Format("date >= '{0:yyyy-MM-dd}' AND date <= '{1:yyyy-MM-dd}'",
                dateTimePicker1.Value, dateTimePicker2.Value.AddDays(1));

            if (isDefaultFilterActive)
            {
                datatable.DefaultView.RowFilter = dateFilter;
            }
        }

        private void ApplyCombinedFilter()
        {
            string filter = "";
            string dateFilter = string.Format("date >= '{0:yyyy-MM-dd}' AND date <= '{1:yyyy-MM-dd}'",
                dateTimePicker1.Value, dateTimePicker2.Value.AddDays(1));

            string textFilter = "";
            if (!String.IsNullOrEmpty(textBox2.Text))
            {
                textFilter += string.Format("customer_name LIKE '%{0}%'", textBox2.Text);
            }
            if (!String.IsNullOrEmpty(textBox1.Text))
            {
                if (!String.IsNullOrEmpty(textFilter)) textFilter += " AND ";
                textFilter += string.Format("company_name LIKE '%{0}%'", textBox1.Text);
            }

            if (isDefaultFilterActive)
            {
                filter = dateFilter;
                if (!String.IsNullOrEmpty(textFilter))
                {
                    filter += " AND " + textFilter;
                }
            }
            else
            {
                filter = textFilter;
            }

            datatable.DefaultView.RowFilter = filter;
            update_totals();
        }

        private void update_totals()
        {
            decimal debit = 0;
            decimal credit = 0;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[5].Value != null)
                {
                    decimal.TryParse(row.Cells[5].Value?.ToString(), out decimal cellDebit);
                    debit += cellDebit;
                }
                if (row.Cells[6].Value != null)
                {
                    decimal.TryParse(row.Cells[6].Value?.ToString(), out decimal cellCredit);
                    credit += cellCredit;
                }
            }

            label12.Text = debit.ToString("N2");
            label10.Text = credit.ToString("N2");
        }

        public void RefreshInvoice(string invoiceId)
        {
            for (int i = 0; i < datatable.Rows.Count; i++)
            {
                if (datatable.Rows[i]["id"].ToString() == invoiceId)
                {
                    using (SqlConnection conne = new SqlConnection(Properties.Settings.Default.connetion))
                    {
                        SqlCommand cmd = new SqlCommand("SELECT total_bill, amount_paid, change, amount_remanning FROM invoice WHERE invoice_id = @invoiceId", conne);
                        cmd.Parameters.AddWithValue("@invoiceId", invoiceId);
                        conne.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            decimal.TryParse(reader.GetString(0), out decimal total);
                            decimal.TryParse(reader.GetString(1), out decimal amount_paid);
                            decimal.TryParse(reader.GetString(2), out decimal change);
                            decimal.TryParse(reader.GetString(3), out decimal remaining);

                            datatable.Rows[i]["debit"] = total > 0 ? total : 0m;
                            datatable.Rows[i]["credit"] = amount_paid > 0 ?
                                (change >= 0 ? amount_paid : (amount_paid + change)) : 0m;
                        }
                        conne.Close();
                    }
                    break;
                }
            }
            update_totals();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            ApplyCombinedFilter();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            ApplyCombinedFilter();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            isDefaultFilterActive = true;
            ApplyCombinedFilter();
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            isDefaultFilterActive = true;
            ApplyCombinedFilter();
        }

        private void button5_Click(object sender, EventArgs e)
        {

            isDefaultFilterActive = false;
            textBox1.Text = "";
            textBox2.Text = "";
            datatable.DefaultView.RowFilter = "";
            update_totals();
        }

        private void button1_Click(object sender, EventArgs e)
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

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];
                PayBill pb = new PayBill(this);
                pb.textBox1.Text = selectedRow.Cells[1].Value.ToString();
                pb.textBox2.Text = selectedRow.Cells[2].Value.ToString();
                pb.textBox3.Text = selectedRow.Cells[3].Value.ToString();
                pb.start_date.Value = dateTimePicker1.Value;
                pb.end_date.Value = dateTimePicker2.Value;
                pb.customer.Text = textBox2.Text;
                pb.company.Text = textBox1.Text;
                pb.ShowDialog();
            }
            else
            {
                MessageBox.Show("Select One Row TO Add New Invoice.", "alert", MessageBoxButtons.OK);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];
                string invoiceId = selectedRow.Cells[0].Value.ToString();

                DialogResult result = MessageBox.Show("Are you sure you want to delete this invoice?",
                                                    "Confirm Delete",
                                                    MessageBoxButtons.YesNo,
                                                    MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        using (SqlConnection conne = new SqlConnection(Properties.Settings.Default.connetion))
                        {
                            SqlCommand cmd = new SqlCommand("UPDATE invoice SET is_hidden=1 WHERE invoice_id=@invoiceId", conne);
                            cmd.Parameters.AddWithValue("@invoiceId", invoiceId);
                            conne.Open();
                            int rowsAffected = cmd.ExecuteNonQuery();
                            conne.Close();

                            if (rowsAffected > 0)
                            {
                                for (int i = 0; i < datatable.Rows.Count; i++)
                                {
                                    if (datatable.Rows[i]["id"].ToString() == invoiceId)
                                    {
                                        datatable.Rows.RemoveAt(i);
                                        break;
                                    }
                                }

                                dataGridView1.DataSource = datatable;
                                update_totals();
                                MessageBox.Show("Invoice deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("Invoice not found or could not be deleted.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error deleting invoice: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select one invoice to delete.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Empty implementation
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
            // Empty implementation
        }

        private void label10_Click(object sender, EventArgs e)
        {
            // Empty implementation
        }

 
    }
}