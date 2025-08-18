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
    public partial class ManualProduct : Form
    {
        private Super_Pansar_store_invoice invoice;
        public ManualProduct(Super_Pansar_store_invoice invoice_form)
        {
            InitializeComponent();
            this.invoice = invoice_form;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Please enter product name first!", "Empty Field Box Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBox2.Text) && Convert.ToInt32(textBox2.Text) > 0)
            {
                MessageBox.Show("Please enter product quantity to proceed!", "Empty Field Box Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(comboBox1.Text))
            {
                MessageBox.Show("Please select product unit to proceed!", "Empty Field Box Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBox5.Text))
            {
                MessageBox.Show("Please enter product unit price to proceed!", "Empty Field Box Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBox9.Text))
            {
                MessageBox.Show("Please enter product sale price to proceed!", "Empty Field Box Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(comboBox2.Text))
            {
                MessageBox.Show("Please select product sale type to proceed!", "Empty Field Box Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBox7.Text))
            {
                MessageBox.Show("Please enter total purchase price to proceed!", "Empty Field Box Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int product_id = 0;
            double product_quantity = 0;
            double total_price = 0;
            using (SqlConnection conne = new SqlConnection(Properties.Settings.Default.connetion))
            {
                SqlCommand cmd_select = new SqlCommand("select  * FROM products where product_name='" + textBox1.Text + "' AND description='Manual Entry Add Product Manual'", conne);
                conne.Open();
                SqlDataReader reader = cmd_select.ExecuteReader();
                while (reader.Read())
                {
                    product_id = reader.GetInt32(0);
                    product_quantity = Convert.ToDouble(reader.GetValue(2)) + Convert.ToDouble(textBox2.Text);
                    total_price= Convert.ToDouble(reader.GetValue(6)) + Convert.ToDouble(textBox7.Text);
                }
                conne.Close();
            }
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.connetion);
            if (product_id > 0)
            {
                conn.Open();
                string update = "UPDATE products SET product_name='"+ textBox1.Text + "', quantity='"+ product_quantity +"' ,unit='"+ comboBox1.Text + "',unit_price='"+ textBox5.Text + "',total_bill='"+ total_price + "',bar_code='',purchase_date='"+ Convert.ToDateTime(DateTime.Now) + "',sale_price='"+ textBox9.Text + "',sale_type='"+ comboBox2.Text + "' WHERE Id='"+product_id+"'";
                SqlCommand update_cmd = new SqlCommand(update, conn);
                update_cmd.ExecuteNonQuery();
                conn.Close();
            }
            else {
                conn.Open();
                string str = "insert into products (product_name,description,quantity,unit,unit_price,total_bill,bar_code,purchase_date,sale_price,sale_type ) OUTPUT INSERTED.ID values ('" + textBox1.Text + "','Manual Entry Add Product Manual', '" + textBox2.Text + "','" + comboBox1.Text + "', '" + textBox5.Text + "','" + textBox7.Text + "',' ','" + Convert.ToDateTime(DateTime.Now) + "','" + textBox9.Text + "','" + comboBox2.Text + "')";
                SqlCommand cmd = new SqlCommand(str, conn);
                product_id = (int)cmd.ExecuteScalar();
                conn.Close();
            }

            invoice.textBox12.Text = product_id.ToString();
            invoice.textBox9.Text = textBox2.Text;
            invoice.textBox12_Leave(sender,e);
            invoice.button1_Click_1(sender, e);
            //double total = Convert.ToDouble(textBox9.Text) * Convert.ToDouble(textBox2.Text);
            //object[] row = new object[] { product_id, textBox1.Text, comboBox1.Text, textBox9.Text, textBox2.Text, total };
            //invoice.dataGridView1.Rows.Add(row);
        }

        private void EXIT_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox2.Text))
            {
                int qunatity = Int32.Parse(textBox2.Text);
                if (!string.IsNullOrEmpty(textBox5.Text))
                {
                    int price = Int32.Parse(string.Format("{0}", double.Parse(textBox5.Text)));
                    textBox7.Text = string.Format("{0:#,##0.00}", double.Parse((qunatity * price).ToString()));
                }
            }
        }

        private void textBox5_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox5.Text))
            {
                textBox5.Text = string.Format("{0:#,##0.00}", double.Parse(textBox5.Text));
            }
            if (!string.IsNullOrEmpty(textBox2.Text))
            {
                int qunatity = Int32.Parse(textBox2.Text);
                if (!string.IsNullOrEmpty(textBox5.Text))
                {
                    int price = Int32.Parse(string.Format("{0}", double.Parse(textBox5.Text)));
                    textBox7.Text = string.Format("{0:#,##0.00}", double.Parse((qunatity * price).ToString()));
                }
            }
        }

        private void textBox9_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox9.Text))
            {
                textBox9.Text = string.Format("{0:#,##0.00}", double.Parse(textBox9.Text));
            }
        }

        private void textBox7_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox7.Text))
            {
                textBox7.Text = string.Format("{0:#,##0.00}", double.Parse(textBox7.Text));
            }
        }
    }
}
