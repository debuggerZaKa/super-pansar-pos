using System;
using System.Data;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class WarehouseTransferForm : Form
    {
        public int? SelectedWarehouseId { get; private set; }

        public WarehouseTransferForm(DataTable warehouses)
        {
            InitializeComponent();
            SetupComboBox(warehouses);
        }

        private void SetupComboBox(DataTable warehouses)
        {
            cbWarehouses.DataSource = warehouses;
            cbWarehouses.DisplayMember = "WarehouseName";
            cbWarehouses.ValueMember = "WarehouseId";
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (cbWarehouses.SelectedValue == null || cbWarehouses.SelectedValue == DBNull.Value)
            {
                MessageBox.Show("Please select a warehouse to transfer the entries to", "Validation Error",
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SelectedWarehouseId = Convert.ToInt32(cbWarehouses.SelectedValue);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
        }
    }
}