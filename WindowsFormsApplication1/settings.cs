using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class settings : Form
    {
        SqlConnection conn = new SqlConnection(Properties.Settings.Default.connetion);
        private int header_change = 0;
        public settings()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // open file dialog
            OpenFileDialog open = new OpenFileDialog();
            // image filters
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            if (open.ShowDialog() == DialogResult.OK)
            {
                // display image in picture box
                pictureBox1.Image = new Bitmap(open.FileName);
                header_change = 1;
                // image file path
                //t.Text = open.FileName;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
          
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int p_invoice = 0;
            int p_barcode = 0;
            if (!String.IsNullOrEmpty(comboBox1.Text) && !String.IsNullOrEmpty(comboBox2.Text))
            {
                conn.Open();
                SqlCommand select = new SqlCommand("select * FROM settings where type='printer'", conn);
                SqlDataReader select_reader = select.ExecuteReader();
                while (select_reader.Read())
                {
                    if (select_reader.GetString(1).Equals("invoice"))
                    {
                        p_invoice = select_reader.GetInt32(0);
                    }
                    else if (select_reader.GetString(1).Equals("barcode"))
                    {
                        p_barcode= select_reader.GetInt32(0);
                    }
                }
                conn.Close();

                if (p_invoice > 0)
                {
                    conn.Open();
                    string update_str = "update settings set value = '"+ comboBox1.Text + "' WHERE Id=" + p_invoice;
                    SqlCommand cmd = new SqlCommand(update_str, conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                else {
                    conn.Open();
                    string str = "insert into settings (setting_key,type,value ) values ('invoice','printer','" + comboBox1.Text + "')";
                    SqlCommand cmd = new SqlCommand(str, conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                if (p_barcode > 0)
                {
                    conn.Open();
                    string update_str = "update settings set value = '" + comboBox2.Text + "' WHERE Id=" + p_barcode;
                    SqlCommand cmd = new SqlCommand(update_str, conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                else {
                    conn.Open();
                    string new_str = "insert into settings (setting_key,type,value ) values ('barcode','printer','" + comboBox2.Text + "')";
                    SqlCommand new_cmd = new SqlCommand(new_str, conn);
                    new_cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            else
            {
                MessageBox.Show("Please Select Default Printers");
                return;
            }

            if (header_change > 0)
            {
                int setting_id = 0;
                conn.Open();
                SqlCommand cmd_select = new SqlCommand("select id FROM settings where setting_key='header' AND type='image'", conn);
                SqlDataReader reader = cmd_select.ExecuteReader();
                while (reader.Read())
                {
                    setting_id = reader.GetInt32(0);
                }
                conn.Close();

                byte[] imageBytes;
                using (MemoryStream ms = new MemoryStream())
                {
                    pictureBox1.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    imageBytes = ms.ToArray();
                }

                if (setting_id > 0)
                {
                    conn.Open();
                    string update_str = "update settings set image = @header_image WHERE Id="+setting_id;
                    SqlCommand cmd = new SqlCommand(update_str, conn);
                    cmd.Parameters.AddWithValue("@header_image", imageBytes);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                else {
                    conn.Open();
                    string str = "insert into settings (setting_key,type,image ) values ('header','image',@header_image)";
                    SqlCommand cmd = new SqlCommand(str, conn);
                    cmd.Parameters.AddWithValue("@header_image", imageBytes);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            MessageBox.Show("Settings Save Successfully.");

        }

        private void settings_Load(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.connetion);
            byte[] getImg = new byte[0];
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand cmd = new SqlCommand("select image FROM settings WHERE setting_key='header' AND settings.type='image'", conn);
            da.SelectCommand = cmd;
            cmd.CommandType = CommandType.Text;
            DataSet ds = new DataSet();
            da.Fill(ds);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                getImg = (byte[])dr["image"];
            }
            if (getImg != null && getImg.Length >0 ) {
                byte[] imgData = getImg;
                MemoryStream stream = new MemoryStream(imgData);
                pictureBox1.Image = Image.FromStream(stream);
            }


            String invoice_printer = "";
            String barcode_printer = "";
            conn.Open();
            SqlCommand select = new SqlCommand("select * FROM settings where type='printer'", conn);
            SqlDataReader select_reader = select.ExecuteReader();
            while (select_reader.Read())
            {
                if (select_reader.GetString(1).Equals("invoice"))
                {
                    invoice_printer = select_reader.GetString(3);
                }

                if (select_reader.GetString(1).Equals("barcode"))
                {
                    barcode_printer = select_reader.GetString(3);
                }
            }
            conn.Close();


            String pkInstalledPrinters;
            for (int i = 0; i < PrinterSettings.InstalledPrinters.Count; i++)
            {
                pkInstalledPrinters = PrinterSettings.InstalledPrinters[i];
                comboBox1.Items.Add(pkInstalledPrinters);
                comboBox2.Items.Add(pkInstalledPrinters);
            }
            comboBox1.SelectedIndex = comboBox1.Items.IndexOf(invoice_printer);
            comboBox2.SelectedIndex = comboBox2.Items.IndexOf(barcode_printer);
        }
    }
}
