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
    public partial class Super_pansar_menu : Form
    {
        private Dictionary<Type, Form> openedForms = new Dictionary<Type, Form>();

        public Super_pansar_menu()
        {
            InitializeComponent();
            this.ShowInTaskbar = true;
        }

        // Generic method to show any form
        private void ShowForm<T>(params object[] constructorArgs) where T : Form
        {
            bool allowMultipleInstances = true; // Set to false to restrict to single instance

            if (!allowMultipleInstances && openedForms.ContainsKey(typeof(T)))
            {
                var existingForm = openedForms[typeof(T)];
                if (!existingForm.IsDisposed)
                {
                    existingForm.BringToFront();
                    return;
                }
                openedForms.Remove(typeof(T));
            }

            T formInstance;

            try
            {
                if (constructorArgs.Length > 0)
                {
                    formInstance = (T)Activator.CreateInstance(typeof(T), constructorArgs);
                }
                else
                {
                    formInstance = Activator.CreateInstance<T>();
                }
            }
            catch (MissingMethodException)
            {
                MessageBox.Show($"The form {typeof(T).Name} doesn't have a public parameterless constructor. " +
                               "Please use the specific method to open this form.", "Error",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            formInstance.ShowInTaskbar = true;
            formInstance.FormClosed += (sender, e) =>
            {
                if (openedForms.ContainsKey(typeof(T)))
                {
                    openedForms.Remove(typeof(T));
                }
            };

            if (!allowMultipleInstances)
            {
                openedForms[typeof(T)] = formInstance;
            }

            formInstance.Show();
        }

        // Specific method for MarketLedgerForm if it needs special handling
        private void ShowMarketLedgerForm()
        {
            try
            {
                // If MarketLedgerForm needs parameters:
                // var form = new MarketLedgerForm(param1, param2);

                // If it's parameterless:
                var form = new MarketLedgerForm();
                form.ShowInTaskbar = true;
                form.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening Market Ledger: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // All your existing event handlers with ShowForm<T>() calls
        private void pictureBox2_Click(object sender, EventArgs e) => ShowForm<addItem>();
        private void pictureBox8_Click(object sender, EventArgs e) => ShowForm<expense>();
        private void pictureBox9_Click(object sender, EventArgs e) => ShowForm<stock>();
        private void pictureBox5_Click(object sender, EventArgs e) => ShowForm<SuperPansar_store_Billing>();
        private void pictureBox6_Click(object sender, EventArgs e) => ShowForm<Form4>();
        private void pictureBox10_Click(object sender, EventArgs e) => ShowForm<Form1>();
        private void pictureBox4_Click(object sender, EventArgs e) => ShowForm<Super_Pansar_store_invoice>();
        private void pictureBox7_Click(object sender, EventArgs e) => ShowForm<account>();
        private void pictureBox11_Click(object sender, EventArgs e) => ShowForm<manual_entry>();
        private void pictureBox3_Click(object sender, EventArgs e) => ShowForm<settings>();
        private void pictureBox12_Click(object sender, EventArgs e) => ShowForm<HiddenInvoices>();
        private void pictureBox13_Click(object sender, EventArgs e) => ShowForm<Ledger>();
        private void pictureBox14_Click(object sender, EventArgs e) => ShowMarketLedgerForm(); // Using specific method
        private void pictureBox15_Click(object sender, EventArgs e) => ShowForm<WorkersForm>();
        private void pictureBox1_Click(object sender, EventArgs e) => ShowForm<WorkerAccountManagementForm>();
        private void pictureBox16_Click(object sender, EventArgs e) => ShowForm<AdminUserManagementForm>();
        private void pictureBox17_Click(object sender, EventArgs e) => ShowForm<WarehouseForm>();

        // Label click handlers
        private void label13_Click_1(object sender, EventArgs e) => ShowForm<settings>();
        private void label8_Click_1(object sender, EventArgs e) => ShowForm<Form1>();
        private void label9_Click(object sender, EventArgs e) => Application.Exit();
        private void label2_Click(object sender, EventArgs e) => ShowForm<AdminUserManagementForm>();
        private void label1_Click_1(object sender, EventArgs e) => ShowForm<WorkerAccountManagementForm>();

        // Other existing methods
        private void exit_Click(object sender, EventArgs e) => Application.Exit();
        private void Back_Click(object sender, EventArgs e) => this.Close();

        private void Super_pansar_menu_Load(object sender, EventArgs e)
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

        // Other existing empty event handlers
        private void textBox1_TextChanged(object sender, EventArgs e) { }
        private void label8_Click(object sender, EventArgs e) { }
        private void label4_Click(object sender, EventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }
        private void label6_Click(object sender, EventArgs e) { }
        private void label13_Click(object sender, EventArgs e) { }
        private void panel1_Paint(object sender, PaintEventArgs e) { }
        private void panel2_Paint(object sender, PaintEventArgs e) { }
    }
}