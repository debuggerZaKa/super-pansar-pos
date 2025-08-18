namespace WindowsFormsApplication1
{
    partial class WarehouseLedgerEntryForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.panelMain = new System.Windows.Forms.Panel();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.labelNotes = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.labelAmount = new System.Windows.Forms.Label();
            this.cmbTransactionType = new System.Windows.Forms.ComboBox();
            this.labelTransactionType = new System.Windows.Forms.Label();
            this.dtpTransactionDate = new System.Windows.Forms.DateTimePicker();
            this.labelTransactionDate = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.labelDescription = new System.Windows.Forms.Label();
            this.txtDealerName = new System.Windows.Forms.TextBox();
            this.labelDealerName = new System.Windows.Forms.Label();
            this.txtCompanyName = new System.Windows.Forms.TextBox();
            this.labelCompanyName = new System.Windows.Forms.Label();
            this.txtWarehouse = new System.Windows.Forms.TextBox();
            this.labelWarehouse = new System.Windows.Forms.Label();
            this.panelMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.txtNotes);
            this.panelMain.Controls.Add(this.labelNotes);
            this.panelMain.Controls.Add(this.btnCancel);
            this.panelMain.Controls.Add(this.btnSave);
            this.panelMain.Controls.Add(this.txtAmount);
            this.panelMain.Controls.Add(this.labelAmount);
            this.panelMain.Controls.Add(this.cmbTransactionType);
            this.panelMain.Controls.Add(this.labelTransactionType);
            this.panelMain.Controls.Add(this.dtpTransactionDate);
            this.panelMain.Controls.Add(this.labelTransactionDate);
            this.panelMain.Controls.Add(this.txtDescription);
            this.panelMain.Controls.Add(this.labelDescription);
            this.panelMain.Controls.Add(this.txtDealerName);
            this.panelMain.Controls.Add(this.labelDealerName);
            this.panelMain.Controls.Add(this.txtCompanyName);
            this.panelMain.Controls.Add(this.labelCompanyName);
            this.panelMain.Controls.Add(this.txtWarehouse);
            this.panelMain.Controls.Add(this.labelWarehouse);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelMain.Name = "panelMain";
            this.panelMain.Padding = new System.Windows.Forms.Padding(20, 16, 20, 16);
            this.panelMain.Size = new System.Drawing.Size(500, 480);
            this.panelMain.TabIndex = 0;
            this.panelMain.Paint += new System.Windows.Forms.PaintEventHandler(this.panelMain_Paint);
            // 
            // txtNotes
            // 
            this.txtNotes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNotes.Location = new System.Drawing.Point(23, 368);
            this.txtNotes.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtNotes.Multiline = true;
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Size = new System.Drawing.Size(454, 65);
            this.txtNotes.TabIndex = 17;
            // 
            // labelNotes
            // 
            this.labelNotes.AutoSize = true;
            this.labelNotes.Location = new System.Drawing.Point(20, 350);
            this.labelNotes.Name = "labelNotes";
            this.labelNotes.Size = new System.Drawing.Size(46, 16);
            this.labelNotes.TabIndex = 16;
            this.labelNotes.Text = "Notes:";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(383, 444);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(94, 23);
            this.btnCancel.TabIndex = 15;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(283, 444);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(94, 23);
            this.btnSave.TabIndex = 14;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtAmount
            // 
            this.txtAmount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAmount.Location = new System.Drawing.Point(23, 320);
            this.txtAmount.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(454, 22);
            this.txtAmount.TabIndex = 13;
            // 
            // labelAmount
            // 
            this.labelAmount.AutoSize = true;
            this.labelAmount.Location = new System.Drawing.Point(20, 302);
            this.labelAmount.Name = "labelAmount";
            this.labelAmount.Size = new System.Drawing.Size(55, 16);
            this.labelAmount.TabIndex = 12;
            this.labelAmount.Text = "Amount:";
            // 
            // cmbTransactionType
            // 
            this.cmbTransactionType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbTransactionType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTransactionType.FormattingEnabled = true;
            this.cmbTransactionType.Location = new System.Drawing.Point(23, 272);
            this.cmbTransactionType.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmbTransactionType.Name = "cmbTransactionType";
            this.cmbTransactionType.Size = new System.Drawing.Size(454, 24);
            this.cmbTransactionType.TabIndex = 11;
            // 
            // labelTransactionType
            // 
            this.labelTransactionType.AutoSize = true;
            this.labelTransactionType.Location = new System.Drawing.Point(20, 254);
            this.labelTransactionType.Name = "labelTransactionType";
            this.labelTransactionType.Size = new System.Drawing.Size(116, 16);
            this.labelTransactionType.TabIndex = 10;
            this.labelTransactionType.Text = "Transaction Type:";
            // 
            // dtpTransactionDate
            // 
            this.dtpTransactionDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpTransactionDate.Location = new System.Drawing.Point(23, 224);
            this.dtpTransactionDate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dtpTransactionDate.Name = "dtpTransactionDate";
            this.dtpTransactionDate.Size = new System.Drawing.Size(454, 22);
            this.dtpTransactionDate.TabIndex = 9;
            // 
            // labelTransactionDate
            // 
            this.labelTransactionDate.AutoSize = true;
            this.labelTransactionDate.Location = new System.Drawing.Point(20, 206);
            this.labelTransactionDate.Name = "labelTransactionDate";
            this.labelTransactionDate.Size = new System.Drawing.Size(113, 16);
            this.labelTransactionDate.TabIndex = 8;
            this.labelTransactionDate.Text = "Transaction Date:";
            // 
            // txtDescription
            // 
            this.txtDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDescription.Location = new System.Drawing.Point(23, 176);
            this.txtDescription.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(454, 22);
            this.txtDescription.TabIndex = 7;
            // 
            // labelDescription
            // 
            this.labelDescription.AutoSize = true;
            this.labelDescription.Location = new System.Drawing.Point(20, 158);
            this.labelDescription.Name = "labelDescription";
            this.labelDescription.Size = new System.Drawing.Size(78, 16);
            this.labelDescription.TabIndex = 6;
            this.labelDescription.Text = "Description:";
            // 
            // txtDealerName
            // 
            this.txtDealerName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDealerName.Location = new System.Drawing.Point(23, 128);
            this.txtDealerName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtDealerName.Name = "txtDealerName";
            this.txtDealerName.Size = new System.Drawing.Size(454, 22);
            this.txtDealerName.TabIndex = 5;
            // 
            // labelDealerName
            // 
            this.labelDealerName.AutoSize = true;
            this.labelDealerName.Location = new System.Drawing.Point(20, 110);
            this.labelDealerName.Name = "labelDealerName";
            this.labelDealerName.Size = new System.Drawing.Size(91, 16);
            this.labelDealerName.TabIndex = 4;
            this.labelDealerName.Text = "Dealer Name:";
            // 
            // txtCompanyName
            // 
            this.txtCompanyName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCompanyName.Location = new System.Drawing.Point(23, 80);
            this.txtCompanyName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtCompanyName.Name = "txtCompanyName";
            this.txtCompanyName.Size = new System.Drawing.Size(454, 22);
            this.txtCompanyName.TabIndex = 3;
            // 
            // labelCompanyName
            // 
            this.labelCompanyName.AutoSize = true;
            this.labelCompanyName.Location = new System.Drawing.Point(20, 62);
            this.labelCompanyName.Name = "labelCompanyName";
            this.labelCompanyName.Size = new System.Drawing.Size(108, 16);
            this.labelCompanyName.TabIndex = 2;
            this.labelCompanyName.Text = "Company Name:";
            // 
            // txtWarehouse
            // 
            this.txtWarehouse.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtWarehouse.Location = new System.Drawing.Point(23, 32);
            this.txtWarehouse.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtWarehouse.Name = "txtWarehouse";
            this.txtWarehouse.ReadOnly = true;
            this.txtWarehouse.Size = new System.Drawing.Size(454, 22);
            this.txtWarehouse.TabIndex = 1;
            this.txtWarehouse.TextChanged += new System.EventHandler(this.txtWarehouse_TextChanged);
            // 
            // labelWarehouse
            // 
            this.labelWarehouse.AutoSize = true;
            this.labelWarehouse.Location = new System.Drawing.Point(20, 14);
            this.labelWarehouse.Name = "labelWarehouse";
            this.labelWarehouse.Size = new System.Drawing.Size(80, 16);
            this.labelWarehouse.TabIndex = 0;
            this.labelWarehouse.Text = "Warehouse:";
            // 
            // WarehouseLedgerEntryForm
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(500, 480);
            this.Controls.Add(this.panelMain);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MinimumSize = new System.Drawing.Size(500, 489);
            this.Name = "WarehouseLedgerEntryForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add Warehouse Entry";
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.TextBox txtWarehouse;
        private System.Windows.Forms.Label labelWarehouse;
        private System.Windows.Forms.TextBox txtCompanyName;
        private System.Windows.Forms.Label labelCompanyName;
        private System.Windows.Forms.TextBox txtDealerName;
        private System.Windows.Forms.Label labelDealerName;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label labelDescription;
        private System.Windows.Forms.DateTimePicker dtpTransactionDate;
        private System.Windows.Forms.Label labelTransactionDate;
        private System.Windows.Forms.ComboBox cmbTransactionType;
        private System.Windows.Forms.Label labelTransactionType;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.Label labelAmount;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtNotes;
        private System.Windows.Forms.Label labelNotes;
    }
}