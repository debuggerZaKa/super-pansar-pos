using System;
using System.Management;
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
using System.Drawing.Printing;

namespace WindowsFormsApplication1
{
    public partial class Super_Pansar_store_invoice : Form
    {
        SqlConnection conn = new SqlConnection(Properties.Settings.Default.connetion);
        SqlCommand cmd;
        private int id;
        private String quantityBeforeEdit;
        private String unitPriceBeforeEdit;
        private String totalPriceBeforeEdit;
        private String invoice_printer;

        public Super_Pansar_store_invoice()
        {
            InitializeComponent();
        }

        private decimal SafeParseDecimal(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return 0M;
            return decimal.TryParse(value, out decimal result) ? result : 0M;
        }

        private double SafeParseDouble(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return 0.0;
            return double.TryParse(value, out double result) ? result : 0.0;
        }

        private int SafeParseInt(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return 0;
            return int.TryParse(value, out int result) ? result : 0;
        }

        private void label1_Click(object sender, EventArgs e) { }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) { }
        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e) { }

        private void Super_Pansar_store_invoice_Closed(object sender, System.EventArgs e)
        {
            SuperPansar_store_Billing.invoice_id = "0";
        }

        private void Super_Pansar_store_invoice_Load(object sender, EventArgs e)
        {
            // Configure DataGridView for decimal quantities
            dataGridView1.Columns[4].ValueType = typeof(decimal); // Quantity column
            dataGridView1.Columns[3].ValueType = typeof(decimal); // Unit Price column
            dataGridView1.Columns[5].ValueType = typeof(decimal); // Total Price column

            // Set default formats
            dataGridView1.Columns[3].DefaultCellStyle.Format = "0.00";
            dataGridView1.Columns[5].DefaultCellStyle.Format = "0.00";
            dataGridView1.Columns[4].DefaultCellStyle.Format = "0.##"; // Allow decimal quantities

            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd MMM yyyy HH:mm:ss";
            dateTimePicker1.Value = DateTime.Now;
            string invoice = SuperPansar_store_Billing.invoice_id;
            DataGridViewRow row = this.dataGridView1.RowTemplate;
            row.Height = 35;
            row.MinimumHeight = 20;
            Font font_head = new Font(dataGridView1.DefaultCellStyle.Font.FontFamily, 12, FontStyle.Bold);
            row.DefaultCellStyle.Font = font_head;

            dataGridView1.Columns[0].HeaderCell.Style.Font = font_head;
            dataGridView1.Columns[1].HeaderCell.Style.Font = font_head;
            dataGridView1.Columns[2].HeaderCell.Style.Font = font_head;
            dataGridView1.Columns[3].HeaderCell.Style.Font = font_head;
            dataGridView1.Columns[4].HeaderCell.Style.Font = font_head;
            dataGridView1.Columns[5].HeaderCell.Style.Font = font_head;

            dataGridView1.Columns[0].Width = 120;    // Product ID
            dataGridView1.Columns[1].Width = 260;   // Product Name
            dataGridView1.Columns[2].Width = 120;    // Unit
            dataGridView1.Columns[3].Width = 200;    // Unit Price
            dataGridView1.Columns[4].Width = 150;    // Quantity
            dataGridView1.Columns[5].Width = 250;
            // Initialize received amount as empty
            textBox7.Text = "";

            this.autocomplete_suggest();
            if (String.IsNullOrEmpty(invoice) || Convert.ToInt32(invoice) <= 0)
            {
                int a;
                conn.Open();
                string que = "select MAX (invoice_id) from invoice";
                cmd = new SqlCommand(que, conn);
                SqlDataReader dr;
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    string val = dr[0].ToString();
                    if (val == "")
                    {
                        textBox1.Text = "1";
                    }
                    else
                    {
                        a = Convert.ToInt32(dr[0].ToString());
                        a = a + 1;
                        textBox1.Text = a.ToString();
                    }
                    conn.Close();
                }
                dataGridView1.Rows.Clear();
            }
            else
            {
                this.id = Convert.ToInt32(invoice);
                textBox1.Text = invoice;
                using (SqlConnection conn_new = new SqlConnection(Properties.Settings.Default.connetion))
                {
                    cmd = new SqlCommand("select * from invoice where invoice_id = '" + invoice + "' ", conn_new);
                    conn_new.Open();
                    SqlDataReader dreader = cmd.ExecuteReader();
                    while (dreader.Read())
                    {
                        textBox6.Text = dreader.GetValue(1).ToString();
                        textBox5.Text = dreader.GetValue(2).ToString();
                        textBox4.Text = dreader.GetValue(3).ToString();
                        dateTimePicker1.Text = dreader.GetValue(4).ToString();
                        textBox3.Text = dreader.GetValue(5).ToString();
                        textBox2.Text = dreader.GetValue(6).ToString();
                        textBox10.Text = dreader.GetValue(7).ToString();

                        // Show empty if amount paid is 0
                        double amountPaid = SafeParseDouble(dreader.GetValue(8).ToString());
                        textBox7.Text = amountPaid == 0 ? "" : amountPaid.ToString();

                        double amountRemaining = SafeParseDouble(dreader.GetValue(9).ToString());
                        textBox8.Text = amountRemaining == 0 ? "" : amountRemaining.ToString();

                        double changeAmount = SafeParseDouble(dreader[10]?.ToString() ?? "0");
                        textBox13.Text = changeAmount == 0 ? "" : changeAmount.ToString();
                    }
                    conn_new.Close();
                }
                using (SqlConnection conn = new SqlConnection(Properties.Settings.Default.connetion))
                {
                    SqlCommand cmd = new SqlCommand("select * from invoice_detail where invoice_id = '" + invoice + "' ", conn);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        dataGridView1.Rows.Add(reader.GetValue(2), reader.GetValue(3).ToString(), reader.GetValue(4).ToString(), reader.GetValue(5).ToString(), reader.GetValue(6).ToString());
                    }
                    conn.Close();
                }
                this.update_balance();
            }
            settings_data();
        }

        private void settings_data()
        {
            conn.Open();
            SqlCommand select = new SqlCommand("select value FROM settings where setting_key='invoice' AND type='printer'", conn);
            SqlDataReader select_reader = select.ExecuteReader();
            while (select_reader.Read())
            {
                this.invoice_printer = select_reader.GetString(0);
            }
            conn.Close();
        }

        private void SaveAndPrint_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBox6.Text))
            {
                MessageBox.Show("Customer Name is Missing", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("No Product is added to invoice", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            this.button3_Click(sender, e);
            this.print_invoice();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SuperPansar_store_Billing.invoice_id = "0";
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e) { }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(autocompleteitems.Text))
            {
                this.add_item_to_data_grid();
            }
            else if (!String.IsNullOrEmpty(textBox12.Text))
            {
                this.add_item_to_data_grid_by_id();
            }
        }

        private void label7_Click(object sender, EventArgs e) { }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return; // Header row clicked

            if (e.ColumnIndex == 4) // Quantity column
            {
                if (dataGridView1[e.ColumnIndex, e.RowIndex].Value != null &&
                    !string.IsNullOrEmpty(dataGridView1[e.ColumnIndex, e.RowIndex].Value.ToString()))
                {
                    decimal quantity = SafeParseDecimal(dataGridView1[4, e.RowIndex].Value.ToString());
                    decimal unitPrice = SafeParseDecimal(dataGridView1[3, e.RowIndex].Value.ToString());
                    dataGridView1[5, e.RowIndex].Value = (quantity * unitPrice).ToString("0.00");
                }
                else
                {
                    dataGridView1[e.ColumnIndex, e.RowIndex].Value = quantityBeforeEdit;
                }
            }
            else if (e.ColumnIndex == 3) // Unit price column
            {
                if (dataGridView1[e.ColumnIndex, e.RowIndex].Value != null &&
                    !string.IsNullOrEmpty(dataGridView1[e.ColumnIndex, e.RowIndex].Value.ToString()))
                {
                    decimal quantity = SafeParseDecimal(dataGridView1[4, e.RowIndex].Value.ToString());
                    decimal unitPrice = SafeParseDecimal(dataGridView1[3, e.RowIndex].Value.ToString());
                    dataGridView1[5, e.RowIndex].Value = (quantity * unitPrice).ToString("0.00");
                }
                else
                {
                    dataGridView1[e.ColumnIndex, e.RowIndex].Value = unitPriceBeforeEdit;
                }
            }
            else if (e.ColumnIndex == 5) // Total price column
            {
                if (dataGridView1[e.ColumnIndex, e.RowIndex].Value != null &&
                    !string.IsNullOrEmpty(dataGridView1[e.ColumnIndex, e.RowIndex].Value.ToString()))
                {
                    decimal quantity = SafeParseDecimal(dataGridView1[4, e.RowIndex].Value.ToString());
                    if (quantity != 0)
                    {
                        decimal totalPrice = SafeParseDecimal(dataGridView1[5, e.RowIndex].Value.ToString());
                        dataGridView1[3, e.RowIndex].Value = (totalPrice / quantity).ToString("0.00");
                    }
                }
                else
                {
                    dataGridView1[e.ColumnIndex, e.RowIndex].Value = totalPriceBeforeEdit;
                }
            }

            this.update_balance();
        }

        private decimal convert_quantity(String quantity, String unit)
        {
            decimal new_quantity = 0;
            decimal qty = SafeParseDecimal(quantity);

            if (unit.Equals("KG") || unit.Equals("L"))
            {
                new_quantity += (qty * 1000);
            }
            else if (unit.Equals("GM") || unit.Equals("ML"))
            {
                new_quantity += qty;
            }
            return new_quantity;
        }

        private decimal convert_quantity_to_high(String quantity, String unit)
        {
            decimal new_quantity = 0;
            decimal qty = SafeParseDecimal(quantity);

            if (unit.Equals("KG") || unit.Equals("L"))
            {
                new_quantity += qty;
            }
            else if (unit.Equals("GM") || unit.Equals("ML"))
            {
                new_quantity += (qty / 1000);
            }
            return new_quantity;
        }

        private decimal check_previous_quantity()
        {
            int n = dataGridView1.Rows.Count;
            decimal old_quantity = 0;

            for (int i = 0; i < n; i++)
            {
                if (dataGridView1.Rows[i].Cells[1].Value.ToString().Equals(autocompleteitems.Text))
                {
                    string unit = dataGridView1.Rows[i].Cells[2].Value.ToString();
                    if (unit.Equals("KG") || unit.Equals("L"))
                    {
                        old_quantity += (SafeParseDecimal(dataGridView1.Rows[i].Cells[4].Value.ToString()) * 1000);
                    }
                    else if (unit.Equals("GM") || unit.Equals("ML"))
                    {
                        old_quantity += SafeParseDecimal(dataGridView1.Rows[i].Cells[4].Value.ToString());
                    }
                }
            }
            return old_quantity;
        }

        private void update_balance()
        {
            decimal sub_total = 0;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.IsNewRow) continue;

                decimal unit_price = 0;
                decimal quantity = 0;
                decimal total_price = 0;

                if (row.Cells[3].Value != null && !string.IsNullOrEmpty(row.Cells[3].Value.ToString()))
                {
                    unit_price = SafeParseDecimal(row.Cells[3].Value.ToString());
                }

                if (row.Cells[4].Value != null && !string.IsNullOrEmpty(row.Cells[4].Value.ToString()))
                {
                    quantity = SafeParseDecimal(row.Cells[4].Value.ToString());
                }

                if (row.Cells[5].Value != null && !string.IsNullOrEmpty(row.Cells[5].Value.ToString()))
                {
                    total_price = SafeParseDecimal(row.Cells[5].Value.ToString());
                }
                else
                {
                    total_price = quantity * unit_price;
                    row.Cells[5].Value = total_price.ToString("0.00");
                }

                sub_total += total_price;
            }

            textBox3.Text = sub_total.ToString("0.00");
            textBox15.Text = textBox10.Text;
        }

        private void label18_Click(object sender, EventArgs e) { }
        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            calculate_discount_percent();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            calculate_discount_amount();
        }

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e) { }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            decimal paid = SafeParseDecimal(textBox7.Text);
            decimal total = SafeParseDecimal(textBox10.Text);

            if (paid > total)
            {
                textBox13.Text = (paid - total).ToString("0.00");
                textBox8.Text = "0";
            }
            else
            {
                textBox13.Text = "0";
                textBox8.Text = (total - paid).ToString("0.00");
            }
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
            textBox9.Text = "1";
            textBox6.Text = "";
            textBox3.Text = "";
            textBox2.Text = "0";
            textBox11.Text = "";
            textBox10.Text = "0";
            textBox7.Text = "";
            textBox8.Text = "0";
            textBox12.Text = "";
            autocompleteitems.Text = "";
            textBox14.Text = "";
            comboBox1.Items.Remove("GM");
            comboBox1.Items.Remove("L");
            comboBox1.Items.Remove("ML");
            comboBox1.Items.Remove("KG");
            this.Super_Pansar_store_invoice_Load(sender, e);
        }

        private void autocompleteitems_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(autocompleteitems.Text))
            {
                autocompleteitems.Text = "";
                textBox14.Text = "";
                textBox9.Text = "1";
                comboBox1.Items.Remove("GM");
                comboBox1.Items.Remove("L");
                comboBox1.Items.Remove("ML");
                comboBox1.Items.Remove("KG");
            }
        }

        private void label5_Click(object sender, EventArgs e) { }
        private void textBox6_TextChanged(object sender, EventArgs e) { }
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e) { }
        private void textBox1_TextChanged(object sender, EventArgs e) { }
        private void label13_Click(object sender, EventArgs e) { }
        private void label16_Click(object sender, EventArgs e) { }
        private void label8_Click(object sender, EventArgs e) { }
        private void textBox9_Leave(object sender, EventArgs e) { }

        private void autocomplete_suggest()
        {
            using (SqlConnection conn = new SqlConnection(Properties.Settings.Default.connetion))
            {
                SqlCommand cmd = new SqlCommand("select product_name,id FROM products", conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
                AutoCompleteStringCollection product_id = new AutoCompleteStringCollection();
                while (reader.Read())
                {
                    MyCollection.Add(reader.GetString(0));
                    product_id.Add(reader.GetInt32(1).ToString());
                }

                autocompleteitems.AutoCompleteCustomSource = MyCollection;
                autocompleteitems.AutoCompleteMode = AutoCompleteMode.Suggest;
                autocompleteitems.AutoCompleteSource = AutoCompleteSource.CustomSource;

                textBox12.AutoCompleteCustomSource = product_id;
                textBox12.AutoCompleteMode = AutoCompleteMode.Suggest;
                textBox12.AutoCompleteSource = AutoCompleteSource.CustomSource;
                conn.Close();
            }
        }

        private void add_item_to_data_grid()
        {
            if (string.IsNullOrEmpty(textBox9.Text))
            {
                textBox9.Text = "1";
            }

            if (!string.IsNullOrEmpty(autocompleteitems.Text))
            {
                int n = dataGridView1.Rows.Count;

                using (SqlConnection con = new SqlConnection(Properties.Settings.Default.connetion))
                {
                    SqlCommand cmd = new SqlCommand("select * FROM products where product_name='" + autocompleteitems.Text + "'", con);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            textBox12.Text = reader.GetInt32(0).ToString();
                            autocompleteitems.Text = reader.GetString(1);

                            decimal total_quantity = convert_quantity(reader.GetString(2), reader.GetString(4));
                            decimal current_quantity = convert_quantity(textBox9.Text, comboBox1.Text);

                            if (current_quantity > total_quantity)
                            {
                                MessageBox.Show("Product is not available. Check your stocks!", "OUT OF STOCK",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }

                            for (int i = 0; i < n; i++)
                            {
                                if (dataGridView1.Rows[i].Cells[1].Value.ToString().Equals(autocompleteitems.Text))
                                {
                                    decimal new_quantity = check_previous_quantity() + convert_quantity(textBox9.Text, comboBox1.Text);
                                    if (new_quantity > total_quantity)
                                    {
                                        MessageBox.Show("Product is not available. Check your stocks!", "OUT OF STOCK",
                                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        return;
                                    }

                                    if (dataGridView1.Rows[i].Cells[1].Value.ToString().Equals(autocompleteitems.Text) &&
                                        dataGridView1.Rows[i].Cells[2].Value.ToString().Equals(comboBox1.Text))
                                    {
                                        decimal existingQty = SafeParseDecimal(dataGridView1.Rows[i].Cells[4].Value.ToString());
                                        decimal newQty = existingQty + SafeParseDecimal(textBox9.Text);
                                        dataGridView1.Rows[i].Cells[4].Value = newQty.ToString("0.##");

                                        decimal unitPrice = SafeParseDecimal(dataGridView1.Rows[i].Cells[3].Value.ToString());
                                        dataGridView1.Rows[i].Cells[5].Value = (newQty * unitPrice).ToString("0.00");
                                        con.Close();
                                        return;
                                    }
                                }
                            }

                            decimal price = 0;
                            decimal quantity = SafeParseDecimal(textBox9.Text);
                            decimal total = 0;

                            if (textBox14.Text.Equals("Per Unit"))
                            {
                                if (reader.GetString(4).Equals("KG") || reader.GetString(4).Equals("L"))
                                {
                                    if (comboBox1.Text.Equals("KG") || comboBox1.Text.Equals("L"))
                                    {
                                        price = SafeParseDecimal(reader.GetString(9));
                                        total = price * quantity;
                                    }
                                    else if (comboBox1.Text.Equals("GM") || comboBox1.Text.Equals("ML"))
                                    {
                                        price = SafeParseDecimal(reader.GetString(9)) / 1000;
                                        total = price * quantity;
                                    }
                                }
                                else if (reader.GetString(4).Equals("GM") || reader.GetString(4).Equals("ML"))
                                {
                                    if (comboBox1.Text.Equals("KG") || comboBox1.Text.Equals("L"))
                                    {
                                        price = SafeParseDecimal(reader.GetString(9)) * 1000;
                                        total = price * quantity;
                                    }
                                    else if (comboBox1.Text.Equals("GM") || comboBox1.Text.Equals("ML"))
                                    {
                                        price = SafeParseDecimal(reader.GetString(9));
                                        total = price * quantity;
                                    }
                                }
                            }
                            else
                            {
                                price = SafeParseDecimal(reader.GetString(9));
                                total = price * quantity;
                            }

                            dataGridView1.Rows.Add(
                                reader.GetValue(0).ToString(),
                                reader.GetString(1),
                                comboBox1.Text,
                                price.ToString("0.00"),
                                quantity.ToString("0.##"),
                                total.ToString("0.00"));
                        }
                    }
                    else
                    {
                        MessageBox.Show("Product is not available. Check your stocks!", "OUT OF STOCK",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        con.Close();
                        return;
                    }
                    con.Close();
                }

                textBox12.Text = "";
                autocompleteitems.Text = "";
                textBox14.Text = "";
                textBox9.Text = "1";
                comboBox1.Items.Remove("GM");
                comboBox1.Items.Remove("L");
                comboBox1.Items.Remove("ML");
                comboBox1.Items.Remove("KG");

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.IsNewRow) continue;

                    row.Cells[0].ReadOnly = true;
                    row.Cells[1].ReadOnly = true;
                    row.Cells[2].ReadOnly = true;
                    row.Cells[3].ReadOnly = false;
                    row.Cells[4].ReadOnly = false;
                    row.Cells[5].ReadOnly = false;
                }

                this.update_balance();
            }
            else
            {
                MessageBox.Show("Please Select Product is missing", "Empty Field",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            textBox15.Text = textBox10.Text;
            if (!String.IsNullOrEmpty(textBox10.Text))
            {
                decimal paid = SafeParseDecimal(textBox7.Text);
                decimal total = SafeParseDecimal(textBox10.Text);

                if (paid > total)
                {
                    textBox13.Text = (paid - total).ToString("0.00");
                    textBox8.Text = "0";
                }
                else
                {
                    textBox13.Text = "0";
                    textBox8.Text = (total - paid).ToString("0.00");
                }
            }
        }

        private void textBox7_TextChanged_1(object sender, EventArgs e) { }

        private void textBox12_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (!String.IsNullOrEmpty(textBox12.Text))
                {
                    this.product_name_textbox_action(textBox12, "Product id");
                    this.add_item_to_data_grid();
                }
            }
        }

        private void product_name_textbox_action(TextBox valueText, String type)
        {
            if (!String.IsNullOrEmpty(valueText.Text))
            {
                comboBox1.Items.Remove("GM");
                comboBox1.Items.Remove("L");
                comboBox1.Items.Remove("ML");
                comboBox1.Items.Remove("KG");

                using (SqlConnection conne = new SqlConnection(Properties.Settings.Default.connetion))
                {
                    SqlCommand cmd = new SqlCommand();
                    if (type.Equals("Product Name"))
                    {
                        cmd = new SqlCommand("select * FROM products where product_name='" + valueText.Text + "'", conne);
                    }
                    else if (type.Equals("Product id"))
                    {
                        cmd = new SqlCommand("select * FROM products where id='" + valueText.Text + "'", conne);
                    }
                    conne.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        textBox12.Text = reader.GetInt32(0).ToString();
                        textBox14.Text = reader.GetString(10);
                        autocompleteitems.Text = reader.GetString(1);
                        if (reader.GetValue(10).Equals("Per Quantity"))
                        {
                            comboBox1.Items.Add(reader.GetString(4));
                            comboBox1.Text = reader.GetString(4);
                        }
                        else
                        {
                            if (reader.GetString(4).Equals("KG") || reader.GetString(4).Equals("GM"))
                            {
                                comboBox1.Items.Add("KG");
                                comboBox1.Items.Add("GM");
                                comboBox1.Text = reader.GetString(4);
                            }
                            if (reader.GetString(4).Equals("ML") || reader.GetString(4).Equals("L"))
                            {
                                comboBox1.Items.Add("ML");
                                comboBox1.Items.Add("L");
                                comboBox1.Text = reader.GetString(4);
                            }
                        }
                    }
                    conne.Close();
                }
            }
        }

        private void autocompleteitems_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.product_name_textbox_action(autocompleteitems, "Product Name");
            }
        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBox12.Text))
            {
                autocompleteitems.Text = "";
                textBox14.Text = "";
                textBox9.Text = "1";
                comboBox1.Items.Remove("GM");
                comboBox1.Items.Remove("L");
                comboBox1.Items.Remove("ML");
                comboBox1.Items.Remove("KG");
            }
        }

        private void add_item_to_data_grid_by_id()
        {
            int n = dataGridView1.Rows.Count;
            if (String.IsNullOrEmpty(textBox9.Text))
            {
                textBox9.Text = "1";
            }
            if (!String.IsNullOrEmpty(textBox12.Text))
            {
                using (SqlConnection con = new SqlConnection(Properties.Settings.Default.connetion))
                {
                    SqlCommand cmd = new SqlCommand("select * FROM products where id='" + textBox12.Text + "'", con);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            textBox12.Text = reader.GetInt32(0).ToString();
                            autocompleteitems.Text = reader.GetValue(1).ToString();

                            for (int i = 0; i < n; i++)
                            {
                                if (dataGridView1.Rows[i].Cells[1].Value.ToString().Equals(autocompleteitems.Text))
                                {
                                    decimal total_quantity = SafeParseDecimal(reader.GetValue(2).ToString());
                                    decimal new_quantity = (SafeParseDecimal(dataGridView1.Rows[i].Cells[4].Value.ToString()) + SafeParseDecimal(textBox9.Text));
                                    if (new_quantity > total_quantity)
                                    {
                                        MessageBox.Show("Product is not available. Check your stocks!", "OUT OF STOCK", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        con.Close();
                                        return;
                                    }

                                    dataGridView1.Rows[i].Cells[4].Value = (SafeParseDecimal(dataGridView1.Rows[i].Cells[4].Value.ToString()) + SafeParseDecimal(textBox9.Text)).ToString("0.##");
                                    dataGridView1.Rows[i].Cells[5].Value = (SafeParseDecimal(dataGridView1.Rows[i].Cells[4].Value.ToString()) * SafeParseDecimal(dataGridView1.Rows[i].Cells[3].Value.ToString())).ToString("0.00");
                                    con.Close();
                                    return;
                                }
                            }

                            decimal price = SafeParseDecimal(reader.GetValue(9).ToString());
                            decimal quantity = SafeParseDecimal(textBox9.Text);
                            decimal total = price * quantity;
                            dataGridView1.Rows.Add(reader.GetValue(0).ToString(), reader.GetValue(1).ToString(), reader.GetValue(4).ToString(), price.ToString("0.00"), quantity.ToString("0.##"), total.ToString("0.00"));
                        }
                    }
                    else
                    {
                        MessageBox.Show("Product is not available. Check your stocks!", "OUT OF STOCK", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        con.Close();
                        return;
                    }
                    con.Close();
                }

                for (int i = 0; i < n; i++)
                {
                    dataGridView1.Rows[i].Cells[0].ReadOnly = true;
                    dataGridView1.Rows[i].Cells[1].ReadOnly = true;
                    dataGridView1.Rows[i].Cells[2].ReadOnly = true;
                    dataGridView1.Rows[i].Cells[3].ReadOnly = false;
                    dataGridView1.Rows[i].Cells[4].ReadOnly = false;
                    dataGridView1.Rows[i].Cells[5].ReadOnly = false;
                }
                this.update_balance();
            }
            else
            {
                MessageBox.Show("Please Select Product is missing", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int check_customer_exist()
        {
            int customer_id = 0;
            using (SqlConnection conn_new = new SqlConnection(Properties.Settings.Default.connetion))
            {
                String Customer = "SELECT customer_id from customer where customer_name='" + textBox6.Text + "'";
                if (!String.IsNullOrEmpty(textBox5.Text))
                {
                    Customer += " OR company_name='" + textBox5.Text + "'";
                }
                if (!String.IsNullOrEmpty(textBox4.Text))
                {
                    Customer += " OR phone_number='" + textBox4.Text + "'";
                }
                cmd = new SqlCommand(Customer, conn_new);
                conn_new.Open();
                SqlDataReader dreader = cmd.ExecuteReader();
                while (dreader.Read())
                {
                    customer_id = dreader.GetInt32(0);
                }
                conn_new.Close();
            }
            return customer_id;
        }

        private int insert_customer()
        {
            int customer_id = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(Properties.Settings.Default.connetion))
                {
                    conn.Open();
                    string customerSql = @"
                        INSERT INTO customer(
                            customer_name, 
                            company_name, 
                            phone_number, 
                            create_date, 
                            is_hidden
                        ) 
                        VALUES (
                            @custName, 
                            @company, 
                            @phone, 
                            @createDate, 
                            @isHidden
                        ); 
                        SELECT SCOPE_IDENTITY();";

                    using (SqlCommand cmd = new SqlCommand(customerSql, conn))
                    {
                        cmd.Parameters.Add("@custName", SqlDbType.NVarChar).Value = textBox6.Text.Trim();
                        cmd.Parameters.Add("@company", SqlDbType.NVarChar).Value = string.IsNullOrEmpty(textBox5.Text) ? (object)DBNull.Value : textBox5.Text;
                        cmd.Parameters.Add("@phone", SqlDbType.NVarChar).Value = string.IsNullOrEmpty(textBox4.Text) ? (object)DBNull.Value : textBox4.Text;
                        cmd.Parameters.Add("@createDate", SqlDbType.DateTime).Value = dateTimePicker1.Value;
                        cmd.Parameters.Add("@isHidden", SqlDbType.Bit).Value = 0;

                        var result = cmd.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            customer_id = Convert.ToInt32(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating customer: {ex.Message}", "Database Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
            return customer_id;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox6.Text))
            {
                MessageBox.Show("Customer Name is Missing", "Empty Field",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("No Product is added to invoice", "Empty Field",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (SqlConnection conn = new SqlConnection(Properties.Settings.Default.connetion))
            {
                try
                {
                    conn.Open();
                    using (SqlTransaction transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            int customer_id = this.check_customer_exist();
                            if (customer_id <= 0)
                            {
                                customer_id = this.insert_customer();
                            }

                            // Handle empty values for payment fields
                            string amountPaid = string.IsNullOrWhiteSpace(textBox7.Text) ? "0" : textBox7.Text;
                            string amountRemaining = string.IsNullOrWhiteSpace(textBox8.Text) ? "0" : textBox8.Text;
                            string changeAmount = string.IsNullOrWhiteSpace(textBox13.Text) ? "0" : textBox13.Text;

                            string invoiceSql = @"
                   INSERT INTO invoice(
    customer_name, company_name, cell_number, invoice_date, 
    sub_total, discount, total_bill, amount_paid, amount_remanning, 
    [change], is_hidden, creator_id, creator_name, creator_type)
VALUES (@custName, @company, @cell, @date, 
        @subTotal, @discount, @total, @paid, @remaining, 
        @change, 0, @creatorId, @creatorName, @creatorType);
                    SELECT SCOPE_IDENTITY();";

                            using (SqlCommand cmd = new SqlCommand(invoiceSql, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@custName", textBox6.Text.Trim());
                                cmd.Parameters.AddWithValue("@company", textBox5.Text ?? (object)DBNull.Value);
                                cmd.Parameters.AddWithValue("@cell", textBox4.Text ?? (object)DBNull.Value);
                                cmd.Parameters.AddWithValue("@date", dateTimePicker1.Value);
                                cmd.Parameters.AddWithValue("@subTotal", textBox3.Text);
                                cmd.Parameters.AddWithValue("@discount", textBox2.Text);
                                cmd.Parameters.AddWithValue("@total", textBox10.Text);
                                cmd.Parameters.AddWithValue("@paid", amountPaid);
                                cmd.Parameters.AddWithValue("@remaining", amountRemaining);
                                cmd.Parameters.AddWithValue("@change", changeAmount);

                                cmd.Parameters.AddWithValue("@creatorId",
                                               Properties.Settings.Default.CurrentUserID);
                                cmd.Parameters.AddWithValue("@creatorName",
                                    Properties.Settings.Default.CurrentUserName);
                                cmd.Parameters.AddWithValue("@creatorType",
                                    Properties.Settings.Default.CurrentUserType);

                                this.id = Convert.ToInt32(cmd.ExecuteScalar());
                            }

                            foreach (DataGridViewRow row in dataGridView1.Rows)
                            {
                                if (row.IsNewRow) continue;

                                if (row.Cells[0].Value == null || row.Cells[1].Value == null ||
                                    row.Cells[4].Value == null || row.Cells[5].Value == null)
                                {
                                    throw new Exception("Invalid product data in row. Please check all product fields.");
                                }

                                string detailSql = @"
                            INSERT INTO invoice_detail(
                                invoice_id, product_id, product_name, 
                                unit, unit_price, quantity, total_price, is_hidden)
                            VALUES (@invId, @prodId, @prodName, 
                                    @unit, @price, @qty, @total, 0)";

                                using (SqlCommand cmd = new SqlCommand(detailSql, conn, transaction))
                                {
                                    cmd.Parameters.AddWithValue("@invId", this.id);
                                    cmd.Parameters.AddWithValue("@prodId", row.Cells[0].Value.ToString());
                                    cmd.Parameters.AddWithValue("@prodName", row.Cells[1].Value.ToString());
                                    cmd.Parameters.AddWithValue("@unit", row.Cells[2].Value?.ToString() ?? "");
                                    cmd.Parameters.AddWithValue("@price", row.Cells[3].Value?.ToString() ?? "0");
                                    cmd.Parameters.AddWithValue("@qty", row.Cells[4].Value.ToString());
                                    cmd.Parameters.AddWithValue("@total", row.Cells[5].Value.ToString());

                                    cmd.ExecuteNonQuery();
                                }

                                string updateSql = @"
                            UPDATE products 
                            SET quantity = CAST(CAST(quantity AS DECIMAL(18,2)) - CAST(@qty AS DECIMAL(18,2)) AS NVARCHAR(MAX)) 
                            WHERE id = @prodId";

                                using (SqlCommand cmd = new SqlCommand(updateSql, conn, transaction))
                                {
                                    cmd.Parameters.AddWithValue("@qty", row.Cells[4].Value.ToString());
                                    cmd.Parameters.AddWithValue("@prodId", row.Cells[0].Value.ToString());
                                    cmd.ExecuteNonQuery();
                                }
                            }

                            transaction.Commit();
                            MessageBox.Show("Invoice Saved Successfully!", "Success",
                                          MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            MessageBox.Show($"Error saving invoice: {ex.Message}", "Database Error",
                                          MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Connection error: {ex.Message}", "Error",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private bool check_printer_available()
        {
            ManagementScope scope = new ManagementScope(@"\root\cimv2");
            scope.Connect();
            ManagementObjectSearcher searcher = new
            ManagementObjectSearcher("SELECT * FROM Win32_Printer");
            string printerName = "";
            foreach (ManagementObject printer in searcher.Get())
            {
                printerName = printer["Name"].ToString().ToLower();
                if (printerName.Equals(invoice_printer.ToLower()))
                {
                    Console.WriteLine("Printer = " + printer["Name"]);
                    if (printer["WorkOffline"].ToString().ToLower().Equals("true"))
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private void print_invoice()
        {
            if (!String.IsNullOrEmpty(invoice_printer))
            {
                if (check_printer_available())
                {
                    CrystalReport2 crystalReport = new CrystalReport2();
                    DataSetInvoice dataset = GetData();
                    crystalReport.SetDataSource(dataset);
                    crystalReport.PrintOptions.PrinterName = invoice_printer;
                    crystalReport.PrintToPrinter(1, false, 0, 0);
                    MessageBox.Show("Invoice has been added to print list!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Printer is not connected please connect printer.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Please Check Settings For Default Printer selection and then try again.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private DataSetInvoice GetData()
        {
            string constr = Properties.Settings.Default.connetion;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT invoice.sub_total,invoice.discount,invoice.total_bill,invoice.amount_paid,invoice.amount_remanning,invoice.company_name,invoice.customer_name,invoice.cell_number,invoice.invoice_date,invoice_detail.product_id,invoice_detail.product_name,invoice_detail.quantity,invoice_detail.unit,invoice_detail.unit_price as price ,invoice_detail.total_price as total , invoice.invoice_id FROM invoice INNER JOIN invoice_detail on invoice.invoice_id = invoice_detail.invoice_id WHERE invoice.invoice_id='" + this.id + "'"))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataSetInvoice dataset_data = new DataSetInvoice())
                        {
                            sda.Fill(dataset_data, "Invoice");
                            return dataset_data;
                        }
                    }
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBox6.Text))
            {
                MessageBox.Show("Customer Name is Missing", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("No Product is added to invoice", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string que = "select invoice_id from invoice where invoice_id='" + textBox1.Text + "'";
            conn.Open();
            cmd = new SqlCommand(que, conn);
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                string val = dr[0].ToString();
                if (val == "")
                {
                    MessageBox.Show("No Invoice found. Please save invoice first!", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    this.print_invoice();
                }
            }
            else
            {
                MessageBox.Show("No Invoice found. Please save invoice first!", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            conn.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Calculator.Calculator cal = new Calculator.Calculator();
            cal.ShowDialog();
        }

        private void label4_Click(object sender, EventArgs e) { }
        private void panel1_Paint(object sender, PaintEventArgs e) { }
        private void label10_Click(object sender, EventArgs e) { }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {
            calculate_discount_percent();
        }

        private void calculate_discount_amount()
        {
            if (String.IsNullOrEmpty(textBox2.Text))
            {
                textBox10.Text = textBox3.Text;
            }
            else
            {
                decimal discount = SafeParseDecimal(textBox2.Text);
                decimal sub_total = SafeParseDecimal(textBox3.Text);
                decimal discount_amount = Math.Round((discount * sub_total) / 100);
                textBox11.Text = discount_amount.ToString("0.00");
                textBox10.Text = (sub_total - discount_amount).ToString("0.00");
            }
            textBox15.Text = textBox10.Text;
        }

        private void calculate_discount_percent()
        {
            if (!String.IsNullOrEmpty(textBox11.Text))
            {
                decimal discount = SafeParseDecimal(textBox11.Text);
                decimal sub_total = SafeParseDecimal(textBox3.Text);
                if (sub_total > 0)
                {
                    decimal discount_percent = Math.Round(((discount * 100) / sub_total));
                    textBox2.Text = discount_percent.ToString("0.00");
                    textBox10.Text = (sub_total - discount).ToString("0.00");
                }
            }
            else
            {
                textBox10.Text = textBox3.Text;
            }
            textBox15.Text = textBox10.Text;
        }

        private void textBox2_Leave(object sender, EventArgs e) { }
        public void button1_Click_1(object sender, EventArgs e) { this.add_item_to_data_grid(); }

        private void textBox9_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.add_item_to_data_grid();
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void autocompleteitems_Leave(object sender, EventArgs e)
        {
            this.product_name_textbox_action(autocompleteitems, "Product Name");
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4) // Quantity column
            {
                int n = dataGridView1.Rows.Count;
                decimal old_quantity = 0;
                decimal total_quantity = 0;

                using (SqlConnection conne = new SqlConnection(Properties.Settings.Default.connetion))
                {
                    SqlCommand cmd = new SqlCommand("select * FROM products where product_name='" +
                        dataGridView1[1, e.RowIndex].Value.ToString() + "'", conne);
                    conne.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        total_quantity = convert_quantity(reader.GetValue(2).ToString(), reader.GetValue(4).ToString());
                    }
                    conne.Close();
                }

                for (int i = 0; i < n; i++)
                {
                    if (dataGridView1.Rows[i].Cells[1].Value.ToString().Equals(dataGridView1[1, e.RowIndex].Value.ToString()))
                    {
                        string unit = dataGridView1.Rows[i].Cells[2].Value.ToString();
                        if (unit.Equals("KG") || unit.Equals("L"))
                        {
                            old_quantity += (SafeParseDecimal(dataGridView1.Rows[i].Cells[4].Value.ToString()) * 1000);
                        }
                        else if (unit.Equals("GM") || unit.Equals("ML"))
                        {
                            old_quantity += SafeParseDecimal(dataGridView1.Rows[i].Cells[4].Value.ToString());
                        }
                    }
                }

                if (old_quantity > total_quantity)
                {
                    dataGridView1[e.ColumnIndex, e.RowIndex].Value = quantityBeforeEdit;
                    quantityBeforeEdit = "";
                    MessageBox.Show("Product is not available. Check your stocks!", "OUT OF STOCK",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    quantityBeforeEdit = "";
                }
            }
            else if (e.ColumnIndex == 3) // Unit price column
            {
                if (dataGridView1[e.ColumnIndex, e.RowIndex].Value == null ||
                    string.IsNullOrEmpty(dataGridView1[e.ColumnIndex, e.RowIndex].Value.ToString()))
                {
                    dataGridView1[e.ColumnIndex, e.RowIndex].Value = unitPriceBeforeEdit;
                    unitPriceBeforeEdit = "";
                }
            }
            else if (e.ColumnIndex == 5) // Total price column
            {
                if (dataGridView1[e.ColumnIndex, e.RowIndex].Value == null ||
                    string.IsNullOrEmpty(dataGridView1[e.ColumnIndex, e.RowIndex].Value.ToString()))
                {
                    dataGridView1[e.ColumnIndex, e.RowIndex].Value = totalPriceBeforeEdit;
                    totalPriceBeforeEdit = "";
                }
            }
        }

        public void textBox12_Leave(object sender, EventArgs e)
        {
            this.product_name_textbox_action(textBox12, "Product id");
        }

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex == 4) // Quantity column
            {
                quantityBeforeEdit = dataGridView1[e.ColumnIndex, e.RowIndex].Value?.ToString();
            }
            else if (e.ColumnIndex == 3) // Unit price column
            {
                unitPriceBeforeEdit = dataGridView1[e.ColumnIndex, e.RowIndex].Value?.ToString();
            }
            else if (e.ColumnIndex == 5) // Total price column
            {
                totalPriceBeforeEdit = dataGridView1[e.ColumnIndex, e.RowIndex].Value?.ToString();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ManualProduct manual_product = new ManualProduct(this);
            manual_product.ShowDialog();
        }

        private void label20_Click(object sender, EventArgs e) { }
        private void panel2_Paint(object sender, PaintEventArgs e) { }
        private void panel4_Paint(object sender, PaintEventArgs e) { }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            ManualProduct manual_product = new ManualProduct(this);
            manual_product.ShowDialog();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Calculator.Calculator cal = new Calculator.Calculator();
            cal.ShowDialog();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            ManualProduct manual_product = new ManualProduct(this);
            manual_product.ShowDialog();
        }

        private void pictureBox13_Click(object sender, EventArgs e)
        {
            ManualProduct manual_product = new ManualProduct(this);
            manual_product.ShowDialog();
        }

        private void pictureBox4_Click_1(object sender, EventArgs e)
        {
            // Delete selected row from data grid
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Confirm deletion with user
                DialogResult result = MessageBox.Show("Are you sure you want to delete this item?",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        // Remove the selected row
                        dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);

                        // Update the balance after deletion
                        this.update_balance();

                        MessageBox.Show("Item deleted successfully.",
                            "Success",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error deleting item: {ex.Message}",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select an item to delete.",
                    "No Selection",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            // Exit the application with confirmation
            DialogResult result = MessageBox.Show("Are you sure you want to exit?",
                "Confirm Exit",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    // Close the form and exit the application
                    this.Close();
                    Application.Exit();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error while exiting: {ex.Message}",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }

        private void textBox15_TextChanged(object sender, EventArgs e)
        {

        }

        private void label13_Click_1(object sender, EventArgs e)
        {

        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dataGridView1.CurrentCell.ColumnIndex == 4) // Quantity column
            {
                TextBox tb = e.Control as TextBox;
                if (tb != null)
                {
                    tb.KeyPress -= Quantity_KeyPress; // Remove previous handler
                    tb.KeyPress += Quantity_KeyPress; // Add new handler
                }
            }
        }

        private void Quantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // Only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }
    }
}