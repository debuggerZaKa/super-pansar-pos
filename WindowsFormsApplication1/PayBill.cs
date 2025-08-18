using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class PayBill : Form
    {
        Ledger Ledger_form;
        private string currentInvoiceId;
        private double currentTotalBill;
        private double currentAmountPaid;
        private double currentRemaining;

        public PayBill(Ledger form)
        {
            this.Ledger_form = form;
            InitializeComponent();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(textBox4.Text))
            {
                double newPayment = Convert.ToDouble(textBox4.Text);

                // Calculate new totals
                double newAmountPaid = currentAmountPaid + newPayment;
                double newRemaining = currentTotalBill - newAmountPaid;

                // Correct change calculation:
                // Change should only exist if payment exceeds remaining balance
                double change = newRemaining < 0 ? Math.Abs(newRemaining) : 0;

                // If there's change, the remaining balance should be 0
                if (change > 0)
                {
                    newRemaining = 0;
                }

                using (SqlConnection conn = new SqlConnection(Properties.Settings.Default.connetion))
                {
                    conn.Open();
                    string updateQuery = @"UPDATE invoice SET 
                        amount_paid = @amount_paid,
                        amount_remanning = @amount_remanning,
                        change = @change
                        WHERE invoice_id = @invoice_id";

                    SqlCommand cmd = new SqlCommand(updateQuery, conn);
                    cmd.Parameters.AddWithValue("@amount_paid", newAmountPaid.ToString());
                    cmd.Parameters.AddWithValue("@amount_remanning", newRemaining.ToString());
                    cmd.Parameters.AddWithValue("@change", change.ToString());
                    cmd.Parameters.AddWithValue("@invoice_id", currentInvoiceId);

                    cmd.ExecuteNonQuery();
                    conn.Close();
                }

                // Refresh the ledger
                Ledger_form.RefreshInvoice(currentInvoiceId);
                this.Hide();
            }
            else
            {
                MessageBox.Show("Please Enter The Amount Paid.", "alert", MessageBoxButtons.OK);
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // Only allow one decimal point
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void PayBill_Load(object sender, EventArgs e)
        {
            start_date.Format = DateTimePickerFormat.Custom;
            start_date.CustomFormat = "dd MMM yyyy HH:mm:ss";
            start_date.Value = DateTime.Now;

            end_date.Format = DateTimePickerFormat.Custom;
            end_date.CustomFormat = "dd MMM yyyy HH:mm:ss";
            end_date.Value = DateTime.Now;

            // Load the selected invoice data
            if (Ledger_form.dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = Ledger_form.dataGridView1.SelectedRows[0];
                currentInvoiceId = selectedRow.Cells[0].Value.ToString();

                using (SqlConnection conn = new SqlConnection(Properties.Settings.Default.connetion))
                {
                    conn.Open();
                    string query = "SELECT total_bill, amount_paid, amount_remanning FROM invoice WHERE invoice_id = @invoice_id";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@invoice_id", currentInvoiceId);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        currentTotalBill = Convert.ToDouble(reader["total_bill"].ToString());
                        currentAmountPaid = Convert.ToDouble(reader["amount_paid"].ToString());
                        currentRemaining = Convert.ToDouble(reader["amount_remanning"].ToString());

                        labelTotalBill.Text = currentTotalBill.ToString("N2");
                        labelAmountPaid.Text = currentAmountPaid.ToString("N2");
                        labelRemaining.Text = currentRemaining.ToString("N2");
                    }
                    conn.Close();
                }
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            // Empty implementation
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }
    }
}