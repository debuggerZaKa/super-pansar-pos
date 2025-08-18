
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        //Worker Login
        private void button2_Click(object sender, EventArgs e)
        {
         
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
        }



        //Admin Login
        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 login = new Form2();
            login.ShowDialog();
        }
        private void EXIT_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
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
            if (getImg != null && getImg.Length > 0)
            {
                byte[] imgData = getImg;
                MemoryStream stream = new MemoryStream(imgData);
           
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
