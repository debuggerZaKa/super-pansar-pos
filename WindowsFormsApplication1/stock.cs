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

namespace WindowsFormsApplication1
{
    public partial class stock : Form
    {

        SqlDataAdapter sqa;
        SqlCommandBuilder scb;
        DataTable dt;

        int cell_column = 0;
        int cell_row = 0;

        SqlConnection sqlconnection;
        SqlCommand sqlcommand;
        string ConnectionString = Properties.Settings.Default.connetion;
        string Query;
        DataTable data_table;
        SqlDataAdapter sqladapter;

        public stock()
        {
            InitializeComponent();
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd MMM yyyy HH:mm:ss";
            dateTimePicker1.Value = DateTime.Now;
        }

        private void load_data_grid_view()
        {
            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();

            sqlconnection = new SqlConnection(ConnectionString);
            Query = "select Id,product_name,quantity,unit,unit_price,sale_price,sale_type,total_bill,purchase_date from products";
            sqlcommand = new SqlCommand(Query, sqlconnection);
            sqladapter = new SqlDataAdapter();
            data_table = new DataTable();
            sqladapter.SelectCommand = sqlcommand;
            sqladapter.Fill(data_table);

            DataGridViewRow row = this.dataGridView1.RowTemplate;
            row.Height = 35;
            row.MinimumHeight = 20;
            Font font_head = new Font(dataGridView1.DefaultCellStyle.Font.FontFamily, 12, FontStyle.Bold);
            row.DefaultCellStyle.Font = font_head;

            dataGridView1.DataSource = data_table;

            dataGridView1.Columns[0].Width = 90;
            dataGridView1.Columns[1].Width = 300;
            dataGridView1.Columns[2].Width = 100;
            dataGridView1.Columns[3].Width = 100;
            dataGridView1.Columns[4].Width = 150;
            dataGridView1.Columns[5].Width = 150;
            dataGridView1.Columns[6].Width = 150;
            dataGridView1.Columns[7].Width = 150;
            dataGridView1.Columns[8].Width = 300;

            dataGridView1.Columns[0].HeaderCell.Style.Font = font_head;
            dataGridView1.Columns[1].HeaderCell.Style.Font = font_head;
            dataGridView1.Columns[2].HeaderCell.Style.Font = font_head;
            dataGridView1.Columns[3].HeaderCell.Style.Font = font_head;
            dataGridView1.Columns[4].HeaderCell.Style.Font = font_head;
            dataGridView1.Columns[5].HeaderCell.Style.Font = font_head;
            dataGridView1.Columns[6].HeaderCell.Style.Font = font_head;
            dataGridView1.Columns[7].HeaderCell.Style.Font = font_head;
            dataGridView1.Columns[8].HeaderCell.Style.Font = font_head;

            dataGridView1.Columns[0].HeaderText = "Id";
            dataGridView1.Columns[1].HeaderText = "Product Name";
            dataGridView1.Columns[2].HeaderText = "Quantity";
            dataGridView1.Columns[3].HeaderText = "Unit";
            dataGridView1.Columns[4].HeaderText = "Unit Price";
            dataGridView1.Columns[5].HeaderText = "Sale Price";
            dataGridView1.Columns[6].HeaderText = "Sale Type";
            dataGridView1.Columns[7].HeaderText = "Total Bill";
            dataGridView1.Columns[8].HeaderText = "Purchase Date";


            dataGridView1.Columns[0].ReadOnly = true;
            dataGridView1.Columns[1].ReadOnly = true;
            dataGridView1.Columns[3].ReadOnly = true;
            dataGridView1.Columns[6].ReadOnly = true;
            dataGridView1.Columns[7].ReadOnly = true;
            dataGridView1.Columns[8].ReadOnly = true;


        }
        private void button1_Click(object sender, EventArgs e)
        {
            load_data_grid_view();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            scb = new SqlCommandBuilder(sqladapter);
            sqladapter.Update(data_table);
            button1_Click(sender, e);
            MessageBox.Show("Record Has Been Updated", "alert", MessageBoxButtons.OK);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Int32 selectedCount = dataGridView1.SelectedRows.Count;
            for (int i = 0; i < selectedCount; i++)
            {
                dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);
            }
            scb = new SqlCommandBuilder(sqladapter);
            sqladapter.Update(data_table);
            button1_Click(sender, e);
            MessageBox.Show("Row has been deleted ", "alert", MessageBoxButtons.OK);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            addItem menuu1 = new addItem();
            menuu1.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            Super_pansar_menu menuu11 = new Super_pansar_menu();
            menuu11.ShowDialog();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(textBox1.Text))
            {
                DataView DV = new DataView(data_table);
                DV.RowFilter = string.Format("product_name like '%{0}%'", textBox1.Text);
                dataGridView1.DataSource = DV;
            }
            else
            {
                dataGridView1.DataSource = data_table;
            }
        }

        private void stock_Load(object sender, EventArgs e)
        {
            //load_data_grid_view();
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void stock_Shown(object sender, EventArgs e)
        {
            load_data_grid_view();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(textBox2.Text))
            {
                DataView DV = new DataView(data_table);
                DV.RowFilter = string.Format("Convert(Id, 'System.String') like '%{0}%'", textBox2.Text.ToString());
                dataGridView1.DataSource = DV;
            }
            else
            {
                dataGridView1.DataSource = data_table;
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(dateTimePicker1.Text))
            {
                DataView DV = new DataView(data_table);
                DV.RowFilter = string.Format("purchase_date >= '{0:yyyy-MM-dd}' AND purchase_date < '{1:yyyy-MM-dd}'", dateTimePicker1.Value, dateTimePicker1.Value.AddDays(1));
                dataGridView1.DataSource = DV;
            }
            else
            {
                dataGridView1.DataSource = data_table;
            }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4 || e.ColumnIndex == 5 || e.ColumnIndex == 7)
            {
                if (String.IsNullOrEmpty(dataGridView1[e.ColumnIndex, e.RowIndex].Value.ToString()))
                {
                    dataGridView1[e.ColumnIndex, e.RowIndex].Value = 0;
                }
                else
                {
                    dataGridView1[e.ColumnIndex, e.RowIndex].Value = string.Format("{0:#,##0.00}", double.Parse(dataGridView1[e.ColumnIndex, e.RowIndex].Value.ToString()));
                }
            }
            if (e.ColumnIndex == 4 || e.ColumnIndex == 2)
            {
                if (String.IsNullOrEmpty(dataGridView1[2, e.RowIndex].Value.ToString()))
                {
                    dataGridView1[2, e.RowIndex].Value = 0;
                }
                if (String.IsNullOrEmpty(dataGridView1[4, e.RowIndex].Value.ToString()))
                {
                    dataGridView1[4, e.RowIndex].Value = 0;
                }
                if (String.IsNullOrEmpty(dataGridView1[7, e.RowIndex].Value.ToString()))
                {
                    dataGridView1[7, e.RowIndex].Value = 0;
                }
                Double quantity = double.Parse(dataGridView1[2, e.RowIndex].Value.ToString());
                Double price = double.Parse(dataGridView1[4, e.RowIndex].Value.ToString());
                dataGridView1[7, e.RowIndex].Value = (quantity * price).ToString();
                dataGridView1[7, e.RowIndex].Value = string.Format("{0:#,##0.00}", double.Parse(dataGridView1[7, e.RowIndex].Value.ToString()));

            }
        }

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex == 4 || e.ColumnIndex == 5 || e.ColumnIndex == 7)
            {
                double temp = double.Parse(dataGridView1[e.ColumnIndex, e.RowIndex].Value.ToString());
                dataGridView1[e.ColumnIndex, e.RowIndex].Value = Convert.ToInt32(temp);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox1.Visible = false;
            dataGridView1[cell_column, cell_row].Value = comboBox1.Text;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.Visible = false;
            dataGridView1[cell_column, cell_row].Value = comboBox2.Text;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                cell_column = e.ColumnIndex;
                cell_row = e.RowIndex;
                int selected = 0;
                if (dataGridView1[e.ColumnIndex, e.RowIndex].Value.Equals("GM"))
                {
                    selected = 0;
                }
                if (dataGridView1[e.ColumnIndex, e.RowIndex].Value.Equals("KG"))
                {
                    selected = 1;
                }
                if (dataGridView1[e.ColumnIndex, e.RowIndex].Value.Equals("ML"))
                {
                    selected = 2;
                }
                if (dataGridView1[e.ColumnIndex, e.RowIndex].Value.Equals("L"))
                {
                    selected = 3;
                }
                Show_Combobox(e.RowIndex, e.ColumnIndex, comboBox1, selected);
                SendKeys.Send("{F4}");
            }
            if (e.ColumnIndex == 6)
            {
                cell_column = e.ColumnIndex;
                cell_row = e.RowIndex;

                int selected = 0;
                if (dataGridView1[e.ColumnIndex, e.RowIndex].Value.Equals("Per Unit"))
                {
                    selected = 0;
                }
                if (dataGridView1[e.ColumnIndex, e.RowIndex].Value.Equals("Per Quantity"))
                {
                    selected = 1;
                }
                Show_Combobox(e.RowIndex, e.ColumnIndex, comboBox2, selected);
                SendKeys.Send("{F4}");
            }
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (this.dataGridView1.CurrentCell.ColumnIndex == 2 || this.dataGridView1.CurrentCell.ColumnIndex == 4 || this.dataGridView1.CurrentCell.ColumnIndex == 5)
            {
                e.Control.KeyPress += new KeyPressEventHandler(Control_KeyPress);
            }
        }
        private void Control_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
            // only allow one decimal point
            /*
            if (e.KeyChar == '.'&& (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }*/
        }
        private void Show_Combobox(int iRowIndex, int iColumnIndex, ComboBox combo_box, int index)
        {
            // DESCRIPTION: SHOW THE COMBO BOX IN THE SELECTED CELL OF THE GRID.
            // PARAMETERS: iRowIndex - THE ROW ID OF THE GRID.
            //             iColumnIndex - THE COLUMN ID OF THE GRID.

            int x = 0;
            int y = 0;
            int Width = 0;
            int height = 0;

            // GET THE ACTIVE CELL'S DIMENTIONS TO BIND THE COMBOBOX WITH IT.
            Rectangle rect = default(Rectangle);
            rect = dataGridView1.GetCellDisplayRectangle(iColumnIndex, iRowIndex, false);
            x = rect.X + dataGridView1.Left;
            y = rect.Y + dataGridView1.Top;

            Width = rect.Width;
            height = rect.Height;
            combo_box.SetBounds(x, y, Width, height);
            combo_box.Visible = true;
            combo_box.SelectedIndex = index;
            combo_box.Focus();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {
                int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];
                String product_id = Convert.ToString(selectedRow.Cells[0].Value);

                if (String.IsNullOrEmpty(product_id))
                {
                    MessageBox.Show("You Haven't Selected any Row ");
                }
                else
                {
                    addItem product = new addItem();
                    product.id = Convert.ToInt32(product_id);
                    if (product.ShowDialog() == DialogResult.OK)
                    {
                        load_data_grid_view();
                    }
                }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Calculator.Calculator cal = new Calculator.Calculator();
            cal.ShowDialog();
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
