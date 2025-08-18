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
using System.IO;
using System.Drawing.Printing;
using System.Globalization;
using System.Management;

namespace WindowsFormsApplication1
{
    public partial class addItem : Form
    {
        public int id;
        private String barcode_printer;

        public addItem()
        {
            InitializeComponent();
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd MMM yyyy HH:mm:ss";
            dateTimePicker1.Value = DateTime.Now;
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Please enter product name first!", "Empty Field Box Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Please enter product quantity to proceed!", "Empty Field Box Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (string.IsNullOrEmpty(comboBox1.Text))
            {
                MessageBox.Show("Please select product unit to proceed!", "Empty Field Box Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (string.IsNullOrEmpty(textBox5.Text))
            {
                MessageBox.Show("Please enter product unit price to proceed!", "Empty Field Box Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (string.IsNullOrEmpty(textBox9.Text))
            {
                MessageBox.Show("Please enter product sale price to proceed!", "Empty Field Box Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (string.IsNullOrEmpty(comboBox2.Text))
            {
                MessageBox.Show("Please select product sale type to proceed!", "Empty Field Box Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (string.IsNullOrEmpty(textBox7.Text))
            {
                MessageBox.Show("Please enter total purchase price to proceed!", "Empty Field Box Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // Only check for duplicate names when adding new items, not when updating
            if (!CheckIfProductExists())
            {
                using (SqlConnection conne = new SqlConnection(Properties.Settings.Default.connetion))
                {
                    SqlCommand cmd_select = new SqlCommand("SELECT id FROM products WHERE product_name=@product_name", conne);
                    cmd_select.Parameters.AddWithValue("@product_name", textBox1.Text);
                    conne.Open();
                    object result = cmd_select.ExecuteScalar();
                    if (result != null)
                    {
                        MessageBox.Show("Product name already exists.", "Already Exists", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }

            return true;
        }

        private bool CheckIfProductExists()
        {
            if (id <= 0) return false;

            using (SqlConnection conn = new SqlConnection(Properties.Settings.Default.connetion))
            using (SqlCommand cmd = new SqlCommand("SELECT COUNT(1) FROM products WHERE id = @id", conn))
            {
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }

        private void UpdateProduct()
        {
            using (SqlConnection conn = new SqlConnection(Properties.Settings.Default.connetion))
            using (SqlCommand cmd = new SqlCommand(@"UPDATE products SET 
                product_name = @product_name, 
                quantity = @quantity, 
                description = @description, 
                unit = @unit, 
                unit_price = @unit_price, 
                total_bill = @total_bill, 
                bar_code = @bar_code, 
                purchase_date = @purchase_date, 
                sale_price = @sale_price, 
                sale_type = @sale_type 
                WHERE id = @id", conn))
            {
                cmd.Parameters.AddWithValue("@product_name", textBox1.Text);
                cmd.Parameters.AddWithValue("@quantity", textBox2.Text);
                cmd.Parameters.AddWithValue("@description", textBox3.Text);
                cmd.Parameters.AddWithValue("@unit", comboBox1.Text);
                cmd.Parameters.AddWithValue("@unit_price", textBox5.Text);
                cmd.Parameters.AddWithValue("@total_bill", textBox7.Text);
                cmd.Parameters.AddWithValue("@bar_code", GenerateBarcodeBytes());
                cmd.Parameters.AddWithValue("@purchase_date", dateTimePicker1.Value);
                cmd.Parameters.AddWithValue("@sale_price", textBox9.Text);
                cmd.Parameters.AddWithValue("@sale_type", comboBox2.Text);
                cmd.Parameters.AddWithValue("@id", id);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private void InsertProduct()
        {
            using (SqlConnection conn = new SqlConnection(Properties.Settings.Default.connetion))
            using (SqlCommand cmd = new SqlCommand(@"INSERT INTO products 
                        (product_name, quantity, description, unit, unit_price, 
                        total_bill, purchase_date, sale_price, sale_type) 
                        OUTPUT INSERTED.ID 
                        VALUES (@product_name, @quantity, @description, @unit, @unit_price, 
                        @total_bill, @purchase_date, @sale_price, @sale_type)", conn))
            {
                cmd.Parameters.AddWithValue("@product_name", textBox1.Text);
                cmd.Parameters.AddWithValue("@quantity", textBox2.Text);
                cmd.Parameters.AddWithValue("@description", textBox3.Text);
                cmd.Parameters.AddWithValue("@unit", comboBox1.Text);
                cmd.Parameters.AddWithValue("@unit_price", textBox5.Text);
                cmd.Parameters.AddWithValue("@total_bill", textBox7.Text);
                cmd.Parameters.AddWithValue("@purchase_date", dateTimePicker1.Value);
                cmd.Parameters.AddWithValue("@sale_price", textBox9.Text);
                cmd.Parameters.AddWithValue("@sale_type", comboBox2.Text);

                conn.Open();
                id = (int)cmd.ExecuteScalar();
            }
        }

        private void UpdateBarcode()
        {
            using (SqlConnection conn = new SqlConnection(Properties.Settings.Default.connetion))
            using (SqlCommand cmd = new SqlCommand("UPDATE products SET bar_code = @bar_code WHERE id = @id", conn))
            {
                cmd.Parameters.AddWithValue("@bar_code", GenerateBarcodeBytes());
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private byte[] GenerateBarcodeBytes()
        {
            var barCodeimage = genrate_bar_code(this.id, pictureBox1);
            using (MemoryStream ms = new MemoryStream())
            {
                barCodeimage.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return ms.ToArray();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (ValidateInputs() == false)
                return;

            try
            {
                bool productExists = CheckIfProductExists();

                if (productExists)
                {
                    // Update existing product
                    UpdateProduct();
                }
                else
                {
                    // Insert new product
                    InsertProduct();

                    // Generate and update barcode after we have the ID
                    UpdateBarcode();
                }

                MessageBox.Show("Item " + (productExists ? "Updated" : "Added") + " Successfully", "alert", MessageBoxButtons.OK);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving product: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public Color get_selected_color()
        {
            if (radioButton1.Checked)
            {
                return Color.LightGoldenrodYellow;
            }
            else if (radioButton2.Checked)
            {
                return Color.LightGreen;
            }
            else if (radioButton3.Checked)
            {
                return Color.LightCoral;
            }
            else if (radioButton4.Checked)
            {
                return Color.LightSkyBlue;
            }
            else if (radioButton5.Checked)
            {
                return Color.LightPink;
            }
            else if (radioButton6.Checked)
            {
                return Color.White;
            }

            return Color.White;
        }

        public Bitmap genrate_bar_code(int codeValue, PictureBox picture)
        {
            Zen.Barcode.Code128BarcodeDraw barcode = Zen.Barcode.BarcodeDrawFactory.Code128WithChecksum;

            int header_height = (picture.Height * 40) / 100;
            int barcode_height = (picture.Height * 40) / 100;
            int footer_height = picture.Height - ((picture.Height * 20) / 100);
            int p_name_height = (header_height * 50) / 100;

            var barcodeImage = barcode.Draw(codeValue.ToString(), barcode_height);
            var resultImage = new Bitmap(picture.Width, picture.Height);

            using (var graphics = Graphics.FromImage(resultImage))
            using (var font = new Font("MS Reference Sans Serif", 13))
            using (var brush = new SolidBrush(Color.Black))
            using (var format = new StringFormat()
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Far
            })
            {
                graphics.Clear(Color.White);
                SolidBrush blueBrush = new SolidBrush(get_selected_color());
                graphics.FillRectangle(blueBrush, 0, 0, pictureBox1.Width, header_height);
                graphics.DrawString("Super Pansar", font, brush, resultImage.Width / 2, p_name_height, format);
                graphics.DrawString(textBox1.Text, font, brush, resultImage.Width / 2, p_name_height + p_name_height, format);
                int location = (picture.Width - barcodeImage.Width) / 2;
                graphics.DrawImage(barcodeImage, location, header_height + 1);
                graphics.DrawString(codeValue.ToString(), font, brush, resultImage.Width / 2, picture.Height, format);
            }
            return resultImage;
        }

        private void check_print_number()
        {
            if (String.IsNullOrEmpty(textBox4.Text))
            {
                MessageBox.Show("Please enter number of prints!", "Empty Field Box Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                if (Convert.ToInt32(textBox4.Text) <= 0)
                {
                    MessageBox.Show("Please enter a number greater than ZERO(0)!", "Invalid Number Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        private bool check_printer_available()
        {
            ManagementScope scope = new ManagementScope(@"\root\cimv2");
            scope.Connect();
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Printer");
            string printerName = "";
            foreach (ManagementObject printer in searcher.Get())
            {
                printerName = printer["Name"].ToString().ToLower();
                if (printerName.Equals(barcode_printer.ToLower()))
                {
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(barcode_printer))
            {
                if (check_printer_available())
                {
                    check_print_number();
                    System.Drawing.Printing.PrintDocument printDocument1 = new System.Drawing.Printing.PrintDocument();
                    printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(Doc_printpage);
                    PaperSize size = new PaperSize("Small Size", pictureBox1.Width, pictureBox1.Height);
                    printDocument1.DefaultPageSettings.PaperSize = size;
                    printDocument1.PrinterSettings.PrinterName = this.barcode_printer;

                    if (printDocument1.PrinterSettings.IsValid)
                    {
                        int print_number = Convert.ToInt32(textBox4.Text);
                        for (int j = 0; j < print_number; j++)
                        {
                            printDocument1.Print();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Printer is invalid.");
                    }
                }
                else
                {
                    MessageBox.Show("Printer is not connected please connect printer.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please Check Settings For Default Printer selection and then try again.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Doc_printpage(object sender, PrintPageEventArgs e)
        {
            Bitmap bm = new Bitmap(pictureBox1.Image, pictureBox1.Width, pictureBox1.Height);
            pictureBox1.DrawToBitmap(bm, new Rectangle(0, 0, pictureBox1.Width, pictureBox1.Height));
            e.Graphics.DrawImage(bm, 0, 0);
            bm.Dispose();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void textBox9_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            Super_pansar_menu menuu = new Super_pansar_menu();
            menuu.ShowDialog();
        }

        private void printpage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void addItem_Load(object sender, EventArgs e)
        {
            this.autocomplete_suggest();
            comboBox3.SelectedIndex = comboBox3.Items.IndexOf("1.5" + '"' + " wide x 1" + '"' + " tall");

            if (id > 0)
            {
                LoadExistingProduct();
            }
            else
            {
                GetNextProductId();
            }

            settings_data();
        }

        private void LoadExistingProduct()
        {
            using (SqlConnection conne = new SqlConnection(Properties.Settings.Default.connetion))
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM products WHERE id=@id", conne))
            {
                cmd.Parameters.AddWithValue("@id", id);
                conne.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    textBox1.Text = reader.GetString(1);
                    textBox2.Text = reader.GetString(2);
                    textBox3.Text = reader.GetString(3);
                    comboBox1.Text = reader.GetString(4);
                    textBox5.Text = reader.GetString(5);
                    textBox7.Text = reader.GetString(6);
                    dateTimePicker1.Value = reader.GetDateTime(8);
                    textBox9.Text = reader.GetString(9);
                    comboBox2.Text = reader.GetString(10);

                    if (!string.IsNullOrEmpty(textBox1.Text))
                    {
                        pictureBox1.Image = genrate_bar_code(this.id, pictureBox1);
                    }
                }
            }
        }

        private void GetNextProductId()
        {
            using (SqlConnection conn = new SqlConnection(Properties.Settings.Default.connetion))
            using (SqlCommand cmd = new SqlCommand("SELECT ISNULL(MAX(id), 0) + 1 FROM products", conn))
            {
                conn.Open();
                id = (int)cmd.ExecuteScalar();
            }
        }

        private void settings_data()
        {
            using (SqlConnection conn = new SqlConnection(Properties.Settings.Default.connetion))
            using (SqlCommand select = new SqlCommand("select value FROM settings where setting_key='barcode' AND type='printer'", conn))
            {
                conn.Open();
                SqlDataReader select_reader = select.ExecuteReader();
                while (select_reader.Read())
                {
                    this.barcode_printer = select_reader.GetString(0);
                }
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                pictureBox1.Image = genrate_bar_code(this.id, pictureBox1);
            }
            else
            {
                pictureBox1.Image = null;
            }

            if (!string.IsNullOrEmpty(textBox1.Text) && !CheckIfProductExists())
            {
                using (SqlConnection conne = new SqlConnection(Properties.Settings.Default.connetion))
                using (SqlCommand cmd = new SqlCommand("SELECT product_name FROM products WHERE product_name=@product_name", conne))
                {
                    cmd.Parameters.AddWithValue("@product_name", textBox1.Text);
                    conne.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        MessageBox.Show("Product name already exists.", "Already Exists", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void textBox5_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox5.Text))
            {
                textBox5.Text = string.Format("{0:#,##0.00}", decimal.Parse(textBox5.Text));
            }

            if (!string.IsNullOrEmpty(textBox2.Text))
            {
                decimal quantity = decimal.Parse(textBox2.Text);
                if (!string.IsNullOrEmpty(textBox5.Text))
                {
                    decimal price = decimal.Parse(textBox5.Text);
                    textBox7.Text = string.Format("{0:#,##0.00}", quantity * price);
                }
            }
        }

        private void textBox9_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox9.Text))
            {
                textBox9.Text = string.Format("{0:#,##0.00}", decimal.Parse(textBox9.Text));
            }
        }

        private void textBox7_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox7.Text))
            {
                textBox7.Text = string.Format("{0:#,##0.00}", decimal.Parse(textBox7.Text));
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox2.Text))
            {
                decimal quantity = decimal.Parse(textBox2.Text);
                if (!string.IsNullOrEmpty(textBox5.Text))
                {
                    decimal price = decimal.Parse(textBox5.Text);
                    textBox7.Text = string.Format("{0:#,##0.00}", quantity * price);
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "*.jpg|*.jpg";
            DialogResult dr = sfd.ShowDialog();
            if (dr == DialogResult.OK)
            {
                pictureBox1.Image.Save(sfd.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                MessageBox.Show("Image Saved successfully.");
            }
        }

        private void printDocument2_PrintPage(object sender, PrintPageEventArgs e)
        {
        }

        private void label9_Click(object sender, EventArgs e)
        {
        }

        private void button6_Click(object sender, EventArgs e)
        {
        }

        private void autocomplete_suggest()
        {
            using (SqlConnection conne = new SqlConnection(Properties.Settings.Default.connetion))
            using (SqlCommand cmd = new SqlCommand("SELECT product_name FROM products", conne))
            {
                conne.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
                while (reader.Read())
                {
                    MyCollection.Add(reader.GetString(0));
                }
                textBox1.AutoCompleteCustomSource = MyCollection;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
        }

        private void label9_Click_1(object sender, EventArgs e)
        {
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
        }

        private void textBox4_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void comboBox3_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboBox3.Text.Equals("1.5" + '"' + " wide x 1" + '"' + " tall"))
            {
                pictureBox1.Width = 144;
                pictureBox1.Height = 96;
            }
            else if (comboBox3.Text.Equals("2   " + '"' + " wide x 1" + '"' + " tall"))
            {
                pictureBox1.Width = 192;
                pictureBox1.Height = 96;
            }
            else if (comboBox3.Text.Equals("3   " + '"' + " wide x 2" + '"' + " tall"))
            {
                pictureBox1.Width = 288;
                pictureBox1.Height = 192;
            }
            pictureBox1.Image = genrate_bar_code(this.id, pictureBox1);
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            pictureBox1.Image = genrate_bar_code(this.id, pictureBox1);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
        }

        private void label15_Click(object sender, EventArgs e)
        {
        }

        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
        }
    }
}