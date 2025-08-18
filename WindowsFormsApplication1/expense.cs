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
    public partial class expense : Form
    {
        String previous_value;
        SqlConnection sqlconnection;
        SqlCommand sqlcommand;
        string ConnectionString = Properties.Settings.Default.connetion;
        string Query;
        SqlCommandBuilder scb;
        DataTable datatable;
        SqlDataAdapter sqladapter;
        private bool isDefaultFilterApplied = true;
        private DateTime? currentStartDateFilter = null;
        private DateTime? currentEndDateFilter = null;

        public expense()
        {
            InitializeComponent();
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd MMM yyyy HH:mm:ss";
            dateTimePicker1.Value = DateTime.Now;

            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.CustomFormat = "dd MMM yyyy HH:mm:ss";
            dateTimePicker2.Value = DateTime.Now.AddDays(-30); // Default to 30 days ago

            dateTimePicker3.Format = DateTimePickerFormat.Custom;
            dateTimePicker3.CustomFormat = "dd MMM yyyy HH:mm:ss";
            dateTimePicker3.Value = DateTime.Now;
        }

        private void label2_Click(object sender, EventArgs e) { }

        private void button1_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Please add expense name first", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (String.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Please add expense amount first", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (String.IsNullOrEmpty(dateTimePicker1.Text))
            {
                MessageBox.Show("Please add expense date", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            SqlConnection conn = new SqlConnection(Properties.Settings.Default.connetion);
            conn.Open();
            string str = "INSERT into expense (expense_name,expense_amount,expense_date) VALUES ('" + textBox1.Text + "', '" + textBox2.Text + "', '" + Convert.ToDateTime(dateTimePicker1.Text) + "')";
            SqlCommand cmd = new SqlCommand(str, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("Item Added Successfully", "alert", MessageBoxButtons.OK);
            expense_Load(sender, e);
            dataGridView1.Update();
            dataGridView1.Refresh();
        }

        private void Search_Click(object sender, EventArgs e) { }

        private void expense_Load(object sender, EventArgs e)
        {
            sqlconnection = new SqlConnection(ConnectionString);

            if (isDefaultFilterApplied)
            {
                // Apply default 30-day filter with newest first
                Query = "select expense_id,expense_name,expense_amount,expense_date from expense " +
                         "WHERE expense_date >= @startDate " +
                         "ORDER BY expense_date DESC";
                currentStartDateFilter = dateTimePicker2.Value;
                currentEndDateFilter = null;
            }
            else
            {
                // No date filter, show all with newest first
                Query = "select expense_id,expense_name,expense_amount,expense_date from expense " +
                         "ORDER BY expense_date DESC";
                currentStartDateFilter = null;
                currentEndDateFilter = null;
            }

            sqlcommand = new SqlCommand(Query, sqlconnection);

            if (isDefaultFilterApplied)
            {
                sqlcommand.Parameters.AddWithValue("@startDate", dateTimePicker2.Value);
            }

            sqladapter = new SqlDataAdapter();
            datatable = new DataTable();
            sqladapter.SelectCommand = sqlcommand;
            sqladapter.Fill(datatable);

            DataGridViewRow row = this.dataGridView1.RowTemplate;
            row.Height = 35;
            row.MinimumHeight = 30;
            Font font_head = new Font(dataGridView1.DefaultCellStyle.Font.FontFamily, 12, FontStyle.Bold);
            row.DefaultCellStyle.Font = font_head;
            dataGridView1.DataSource = datatable;

            dataGridView1.Columns[0].Width = 150;
            dataGridView1.Columns[1].Width = 300;
            dataGridView1.Columns[2].Width = 200;
            dataGridView1.Columns[3].Width = 200;

            dataGridView1.Columns[0].HeaderText = "Expense Id";
            dataGridView1.Columns[1].HeaderText = "Expense Name";
            dataGridView1.Columns[2].HeaderText = "Expense Value";
            dataGridView1.Columns[3].HeaderText = "Expense Date";

            dataGridView1.Columns[0].HeaderCell.Style.Font = font_head;
            dataGridView1.Columns[1].HeaderCell.Style.Font = font_head;
            dataGridView1.Columns[2].HeaderCell.Style.Font = font_head;
            dataGridView1.Columns[3].HeaderCell.Style.Font = font_head;

            int n = Convert.ToInt32(dataGridView1.Rows.Count.ToString());
            for (int i = 0; i < n; i++)
            {
                dataGridView1.Rows[i].Cells[0].ReadOnly = true;
            }
            update_total();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // Only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            ApplySearchFilters();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            ApplySearchFilters();
        }

        private void ApplySearchFilters()
        {
            if (datatable == null) return;

            DataView DV = new DataView(datatable);
            string filter = "";

            // Apply date filter if one is active
            if (currentStartDateFilter != null && currentEndDateFilter == null)
            {
                filter = string.Format("expense_date >= '{0:yyyy-MM-dd}'", currentStartDateFilter);
            }
            else if (currentStartDateFilter != null && currentEndDateFilter != null)
            {
                filter = string.Format("expense_date >= '{0:yyyy-MM-dd}' AND expense_date <= '{1:yyyy-MM-dd}'",
                    currentStartDateFilter, currentEndDateFilter);
            }

            // Apply text filters
            if (!String.IsNullOrEmpty(textBox1.Text))
            {
                if (!string.IsNullOrEmpty(filter))
                    filter += " AND ";
                filter += string.Format("expense_name LIKE '%{0}%'", textBox1.Text);
            }

            if (!String.IsNullOrEmpty(textBox2.Text))
            {
                if (!string.IsNullOrEmpty(filter))
                    filter += " AND ";
                filter += string.Format("expense_amount LIKE '%{0}%'", textBox2.Text);
            }

            DV.RowFilter = filter;
            dataGridView1.DataSource = DV;
            update_total();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(dateTimePicker1.Text))
            {
                currentStartDateFilter = dateTimePicker1.Value;
                currentEndDateFilter = dateTimePicker1.Value.AddDays(1);
                isDefaultFilterApplied = false;
                ApplySearchFilters();
            }
            update_total();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            scb = new SqlCommandBuilder(sqladapter);
            sqladapter.Update(datatable);
            expense_Load(sender, e);
            MessageBox.Show("Record Has Been Updated", "alert", MessageBoxButtons.OK);
        }

        private void button2_Click_1(object sender, EventArgs e)
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
                expense_Load(sender, e);
                MessageBox.Show("Row has been deleted ", "alert", MessageBoxButtons.OK);
            }
        }

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            previous_value = dataGridView1[e.ColumnIndex, e.RowIndex].Value.ToString();
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (String.IsNullOrEmpty(dataGridView1[e.ColumnIndex, e.RowIndex].Value.ToString()))
            {
                dataGridView1[e.ColumnIndex, e.RowIndex].Value = previous_value;
            }
        }

        private void update_total()
        {
            Double total = 0;
            Double temp = 0;
            int n = Convert.ToInt32(dataGridView1.Rows.Count.ToString());
            if (n > 0)
            {
                for (int i = 0; i < n; i++)
                {
                    if (dataGridView1.Rows[i].Cells[2].Value != null &&
                        double.TryParse(dataGridView1.Rows[i].Cells[2].Value.ToString(), out temp))
                    {
                        total += temp;
                        label5.Text = total.ToString("N2");
                    }
                    else
                    {
                        label5.Text = "0.00";
                        MessageBox.Show("Expense Contain Invalid data Which Can't Be Converted.", "Invalid Values", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                label5.Text = "0.00";
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e) { }

        private void label3_Click(object sender, EventArgs e) { }

        private void label1_Click(object sender, EventArgs e) { }

        private void panel2_Paint(object sender, PaintEventArgs e) { }

        private void panel3_Paint(object sender, PaintEventArgs e) { }

        private void label7_Click(object sender, EventArgs e) { }

        private void label5_Click(object sender, EventArgs e) { }

        private void button6_Click(object sender, EventArgs e)
        {
            // Reset all filters including the default 30-day filter
            isDefaultFilterApplied = false;
            currentStartDateFilter = null;
            currentEndDateFilter = null;
            textBox1.Text = "";
            textBox2.Text = "";
            expense_Load(sender, e);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(dateTimePicker2.Text) && !String.IsNullOrEmpty(dateTimePicker3.Text))
            {
                currentStartDateFilter = dateTimePicker2.Value;
                currentEndDateFilter = dateTimePicker3.Value;
                isDefaultFilterApplied = false;
                ApplySearchFilters();
            }
            update_total();
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs anError)
        {
            MessageBox.Show("Error happened " + anError.Context.ToString());

            if (anError.Context == DataGridViewDataErrorContexts.Commit)
            {
                MessageBox.Show("Commit error");
            }
            if (anError.Context == DataGridViewDataErrorContexts.CurrentCellChange)
            {
                MessageBox.Show("Cell change");
            }
            if (anError.Context == DataGridViewDataErrorContexts.Parsing)
            {
                MessageBox.Show("parsing error");
            }
            if (anError.Context == DataGridViewDataErrorContexts.LeaveControl)
            {
                MessageBox.Show("leave control error");
            }

            if ((anError.Exception) is ConstraintException)
            {
                DataGridView view = (DataGridView)sender;
                view.Rows[anError.RowIndex].ErrorText = "an error";
                view.Rows[anError.RowIndex].Cells[anError.ColumnIndex].ErrorText = "an error";

                anError.ThrowException = false;
            }
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e) { }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e) { }

        private void dateTimePicker3_ValueChanged(object sender, EventArgs e) { }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            ApplySearchFilters();
        }
    }
}