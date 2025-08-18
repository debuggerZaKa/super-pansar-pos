namespace WindowsFormsApplication1
{
    partial class WarehouseForm
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
            this.mainSplitContainer = new System.Windows.Forms.SplitContainer();
            this.bottomButtonPanel = new System.Windows.Forms.Panel();
            this.btnDeleteEntry = new System.Windows.Forms.Button();
            this.btnAddToWarehouse = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnViewDetails = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dateFilterPanel = new System.Windows.Forms.Panel();
            this.btnResetFilters = new System.Windows.Forms.Button();
            this.dateRightPanel = new System.Windows.Forms.Panel();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.dateLeftPanel = new System.Windows.Forms.Panel();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.filterControlsPanel = new System.Windows.Forms.Panel();
            this.searchPanel = new System.Windows.Forms.Panel();
            this.searchButton = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.searchLabel = new System.Windows.Forms.Label();
            this.warehouseSelectorPanel = new System.Windows.Forms.Panel();
            this.cbWarehouses = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.headerPanel = new System.Windows.Forms.Panel();
            this.headerLabel = new System.Windows.Forms.Label();
            this.formPanel = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.txtLocation = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtWarehouseName = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.buttonPanel = new System.Windows.Forms.Panel();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.mainSplitContainer)).BeginInit();
            this.mainSplitContainer.Panel1.SuspendLayout();
            this.mainSplitContainer.Panel2.SuspendLayout();
            this.mainSplitContainer.SuspendLayout();
            this.bottomButtonPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.dateFilterPanel.SuspendLayout();
            this.dateRightPanel.SuspendLayout();
            this.dateLeftPanel.SuspendLayout();
            this.filterControlsPanel.SuspendLayout();
            this.searchPanel.SuspendLayout();
            this.warehouseSelectorPanel.SuspendLayout();
            this.headerPanel.SuspendLayout();
            this.formPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.buttonPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainSplitContainer
            // 
            this.mainSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.mainSplitContainer.Name = "mainSplitContainer";
            // 
            // mainSplitContainer.Panel1
            // 
            this.mainSplitContainer.Panel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.mainSplitContainer.Panel1.Controls.Add(this.bottomButtonPanel);
            this.mainSplitContainer.Panel1.Controls.Add(this.dataGridView1);
            this.mainSplitContainer.Panel1.Controls.Add(this.dateFilterPanel);
            this.mainSplitContainer.Panel1.Controls.Add(this.filterControlsPanel);
            this.mainSplitContainer.Panel1.Controls.Add(this.headerPanel);
            this.mainSplitContainer.Panel1.Padding = new System.Windows.Forms.Padding(10);
            this.mainSplitContainer.Panel1MinSize = 800;
            // 
            // mainSplitContainer.Panel2
            // 
            this.mainSplitContainer.Panel2.BackColor = System.Drawing.Color.Gainsboro;
            this.mainSplitContainer.Panel2.Controls.Add(this.formPanel);
            this.mainSplitContainer.Panel2.Controls.Add(this.buttonPanel);
            this.mainSplitContainer.Panel2.Padding = new System.Windows.Forms.Padding(10);
            this.mainSplitContainer.Panel2MinSize = 200;
            this.mainSplitContainer.Size = new System.Drawing.Size(1200, 700);
            this.mainSplitContainer.SplitterDistance = 960;
            this.mainSplitContainer.SplitterWidth = 5;
            this.mainSplitContainer.TabIndex = 0;
            // 
            // bottomButtonPanel
            // 
            this.bottomButtonPanel.Controls.Add(this.btnDeleteEntry);
            this.bottomButtonPanel.Controls.Add(this.btnAddToWarehouse);
            this.bottomButtonPanel.Controls.Add(this.btnDelete);
            this.bottomButtonPanel.Controls.Add(this.btnEdit);
            this.bottomButtonPanel.Controls.Add(this.btnViewDetails);
            this.bottomButtonPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottomButtonPanel.Location = new System.Drawing.Point(10, 650);
            this.bottomButtonPanel.Name = "bottomButtonPanel";
            this.bottomButtonPanel.Size = new System.Drawing.Size(940, 40);
            this.bottomButtonPanel.TabIndex = 4;
            // 
            // btnDeleteEntry
            // 
            this.btnDeleteEntry.BackColor = System.Drawing.Color.LightCoral;
            this.btnDeleteEntry.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnDeleteEntry.Location = new System.Drawing.Point(940, 0);
            this.btnDeleteEntry.MinimumSize = new System.Drawing.Size(100, 0);
            this.btnDeleteEntry.Name = "btnDeleteEntry";
            this.btnDeleteEntry.Size = new System.Drawing.Size(235, 40);
            this.btnDeleteEntry.TabIndex = 4;
            this.btnDeleteEntry.Text = "Delete Selected Entry";
            this.btnDeleteEntry.UseVisualStyleBackColor = false;
            this.btnDeleteEntry.Click += new System.EventHandler(this.btnDeleteEntry_Click);
            // 
            // btnAddToWarehouse
            // 
            this.btnAddToWarehouse.BackColor = System.Drawing.Color.LightGreen;
            this.btnAddToWarehouse.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnAddToWarehouse.Location = new System.Drawing.Point(705, 0);
            this.btnAddToWarehouse.MinimumSize = new System.Drawing.Size(100, 0);
            this.btnAddToWarehouse.Name = "btnAddToWarehouse";
            this.btnAddToWarehouse.Size = new System.Drawing.Size(235, 40);
            this.btnAddToWarehouse.TabIndex = 3;
            this.btnAddToWarehouse.Text = "Add to Warehouse";
            this.btnAddToWarehouse.UseVisualStyleBackColor = false;
            this.btnAddToWarehouse.Click += new System.EventHandler(this.btnAddToWarehouse_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.LightCoral;
            this.btnDelete.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnDelete.Location = new System.Drawing.Point(470, 0);
            this.btnDelete.MinimumSize = new System.Drawing.Size(100, 0);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(235, 40);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "Delete Selected Warehouse";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btnEdit.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnEdit.Location = new System.Drawing.Point(235, 0);
            this.btnEdit.MinimumSize = new System.Drawing.Size(100, 0);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(235, 40);
            this.btnEdit.TabIndex = 1;
            this.btnEdit.Text = "Edit Selected Warehouse";
            this.btnEdit.UseVisualStyleBackColor = false;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnViewDetails
            // 
            this.btnViewDetails.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btnViewDetails.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnViewDetails.Location = new System.Drawing.Point(0, 0);
            this.btnViewDetails.MinimumSize = new System.Drawing.Size(100, 0);
            this.btnViewDetails.Name = "btnViewDetails";
            this.btnViewDetails.Size = new System.Drawing.Size(235, 40);
            this.btnViewDetails.TabIndex = 0;
            this.btnViewDetails.Text = "View Ledger Details";
            this.btnViewDetails.UseVisualStyleBackColor = false;
            this.btnViewDetails.Click += new System.EventHandler(this.btnViewDetails_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(10, 135);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(940, 555);
            this.dataGridView1.TabIndex = 3;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // dateFilterPanel
            // 
            this.dateFilterPanel.Controls.Add(this.btnResetFilters);
            this.dateFilterPanel.Controls.Add(this.dateRightPanel);
            this.dateFilterPanel.Controls.Add(this.dateLeftPanel);
            this.dateFilterPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.dateFilterPanel.Location = new System.Drawing.Point(10, 95);
            this.dateFilterPanel.Name = "dateFilterPanel";
            this.dateFilterPanel.Size = new System.Drawing.Size(940, 40);
            this.dateFilterPanel.TabIndex = 5;
            // 
            // btnResetFilters
            // 
            this.btnResetFilters.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnResetFilters.Location = new System.Drawing.Point(850, 5);
            this.btnResetFilters.Name = "btnResetFilters";
            this.btnResetFilters.Size = new System.Drawing.Size(80, 30);
            this.btnResetFilters.TabIndex = 4;
            this.btnResetFilters.Text = "Reset Filters";
            this.btnResetFilters.UseVisualStyleBackColor = true;
            this.btnResetFilters.Click += new System.EventHandler(this.btnResetFilters_Click);
            // 
            // dateRightPanel
            // 
            this.dateRightPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dateRightPanel.Controls.Add(this.dtpEndDate);
            this.dateRightPanel.Controls.Add(this.label4);
            this.dateRightPanel.Location = new System.Drawing.Point(470, 0);
            this.dateRightPanel.MinimumSize = new System.Drawing.Size(250, 40);
            this.dateRightPanel.Name = "dateRightPanel";
            this.dateRightPanel.Size = new System.Drawing.Size(370, 40);
            this.dateRightPanel.TabIndex = 5;
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEndDate.Location = new System.Drawing.Point(90, 7);
            this.dtpEndDate.MinimumSize = new System.Drawing.Size(150, 4);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(270, 27);
            this.dtpEndDate.TabIndex = 3;
            this.dtpEndDate.ValueChanged += new System.EventHandler(this.DateFilters_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 20);
            this.label4.TabIndex = 2;
            this.label4.Text = "End Date:";
            // 
            // dateLeftPanel
            // 
            this.dateLeftPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dateLeftPanel.Controls.Add(this.dtpStartDate);
            this.dateLeftPanel.Controls.Add(this.label3);
            this.dateLeftPanel.Location = new System.Drawing.Point(0, 0);
            this.dateLeftPanel.MinimumSize = new System.Drawing.Size(250, 40);
            this.dateLeftPanel.Name = "dateLeftPanel";
            this.dateLeftPanel.Size = new System.Drawing.Size(470, 40);
            this.dateLeftPanel.TabIndex = 4;
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpStartDate.Location = new System.Drawing.Point(90, 7);
            this.dtpStartDate.MinimumSize = new System.Drawing.Size(150, 4);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(370, 27);
            this.dtpStartDate.TabIndex = 1;
            this.dtpStartDate.ValueChanged += new System.EventHandler(this.DateFilters_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 20);
            this.label3.TabIndex = 0;
            this.label3.Text = "Start Date:";
            // 
            // filterControlsPanel
            // 
            this.filterControlsPanel.Controls.Add(this.searchPanel);
            this.filterControlsPanel.Controls.Add(this.warehouseSelectorPanel);
            this.filterControlsPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.filterControlsPanel.Location = new System.Drawing.Point(10, 50);
            this.filterControlsPanel.Name = "filterControlsPanel";
            this.filterControlsPanel.Size = new System.Drawing.Size(940, 45);
            this.filterControlsPanel.TabIndex = 6;
            // 
            // searchPanel
            // 
            this.searchPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.searchPanel.Controls.Add(this.searchButton);
            this.searchPanel.Controls.Add(this.txtSearch);
            this.searchPanel.Controls.Add(this.searchLabel);
            this.searchPanel.Location = new System.Drawing.Point(470, 0);
            this.searchPanel.MinimumSize = new System.Drawing.Size(300, 46);
            this.searchPanel.Name = "searchPanel";
            this.searchPanel.Size = new System.Drawing.Size(470, 46);
            this.searchPanel.TabIndex = 3;
            // 
            // searchButton
            // 
            this.searchButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.searchButton.Location = new System.Drawing.Point(396, 8);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(70, 27);
            this.searchButton.TabIndex = 1;
            this.searchButton.Text = "Search";
            this.searchButton.UseVisualStyleBackColor = true;
            this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.Location = new System.Drawing.Point(66, 8);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(324, 27);
            this.txtSearch.TabIndex = 2;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // searchLabel
            // 
            this.searchLabel.AutoSize = true;
            this.searchLabel.Location = new System.Drawing.Point(3, 11);
            this.searchLabel.Name = "searchLabel";
            this.searchLabel.Size = new System.Drawing.Size(56, 20);
            this.searchLabel.TabIndex = 0;
            this.searchLabel.Text = "Search:";
            // 
            // warehouseSelectorPanel
            // 
            this.warehouseSelectorPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.warehouseSelectorPanel.Controls.Add(this.cbWarehouses);
            this.warehouseSelectorPanel.Controls.Add(this.label1);
            this.warehouseSelectorPanel.Location = new System.Drawing.Point(0, 0);
            this.warehouseSelectorPanel.MinimumSize = new System.Drawing.Size(300, 46);
            this.warehouseSelectorPanel.Name = "warehouseSelectorPanel";
            this.warehouseSelectorPanel.Size = new System.Drawing.Size(470, 46);
            this.warehouseSelectorPanel.TabIndex = 2;
            // 
            // cbWarehouses
            // 
            this.cbWarehouses.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbWarehouses.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbWarehouses.FormattingEnabled = true;
            this.cbWarehouses.Location = new System.Drawing.Point(140, 6);
            this.cbWarehouses.MinimumSize = new System.Drawing.Size(200, 0);
            this.cbWarehouses.Name = "cbWarehouses";
            this.cbWarehouses.Size = new System.Drawing.Size(320, 28);
            this.cbWarehouses.TabIndex = 1;
            this.cbWarehouses.SelectedIndexChanged += new System.EventHandler(this.cbWarehouses_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(129, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select Warehouse:";
            // 
            // headerPanel
            // 
            this.headerPanel.Controls.Add(this.headerLabel);
            this.headerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerPanel.Location = new System.Drawing.Point(10, 10);
            this.headerPanel.Name = "headerPanel";
            this.headerPanel.Size = new System.Drawing.Size(940, 40);
            this.headerPanel.TabIndex = 0;
            // 
            // headerLabel
            // 
            this.headerLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.headerLabel.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.headerLabel.Location = new System.Drawing.Point(0, 0);
            this.headerLabel.Name = "headerLabel";
            this.headerLabel.Size = new System.Drawing.Size(940, 40);
            this.headerLabel.TabIndex = 0;
            this.headerLabel.Text = "Warehouse Management";
            this.headerLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // formPanel
            // 
            this.formPanel.BackColor = System.Drawing.Color.White;
            this.formPanel.Controls.Add(this.pictureBox1);
            this.formPanel.Controls.Add(this.txtLocation);
            this.formPanel.Controls.Add(this.label2);
            this.formPanel.Controls.Add(this.txtWarehouseName);
            this.formPanel.Controls.Add(this.label12);
            this.formPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.formPanel.Location = new System.Drawing.Point(10, 10);
            this.formPanel.Name = "formPanel";
            this.formPanel.Padding = new System.Windows.Forms.Padding(20);
            this.formPanel.Size = new System.Drawing.Size(215, 578);
            this.formPanel.TabIndex = 1;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImage = global::WindowsFormsApplication1.Properties.Resources.Untitled_design___2025_07_04T215835_764;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Location = new System.Drawing.Point(20, 20);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(175, 85);
            this.pictureBox1.TabIndex = 42;
            this.pictureBox1.TabStop = false;
            // 
            // txtLocation
            // 
            this.txtLocation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLocation.Location = new System.Drawing.Point(24, 314);
            this.txtLocation.Multiline = true;
            this.txtLocation.Name = "txtLocation";
            this.txtLocation.Size = new System.Drawing.Size(167, 100);
            this.txtLocation.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 291);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Location:";
            // 
            // txtWarehouseName
            // 
            this.txtWarehouseName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtWarehouseName.Location = new System.Drawing.Point(24, 219);
            this.txtWarehouseName.Name = "txtWarehouseName";
            this.txtWarehouseName.Size = new System.Drawing.Size(167, 27);
            this.txtWarehouseName.TabIndex = 1;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(21, 196);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(129, 20);
            this.label12.TabIndex = 0;
            this.label12.Text = "Warehouse Name:";
            // 
            // buttonPanel
            // 
            this.buttonPanel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.buttonPanel.Controls.Add(this.btnExit);
            this.buttonPanel.Controls.Add(this.btnClear);
            this.buttonPanel.Controls.Add(this.btnSave);
            this.buttonPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.buttonPanel.Location = new System.Drawing.Point(10, 588);
            this.buttonPanel.Name = "buttonPanel";
            this.buttonPanel.Padding = new System.Windows.Forms.Padding(10);
            this.buttonPanel.Size = new System.Drawing.Size(215, 102);
            this.buttonPanel.TabIndex = 0;
            this.buttonPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.buttonPanel_Paint);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.BackColor = System.Drawing.Color.LightGray;
            this.btnExit.BackgroundImage = global::WindowsFormsApplication1.Properties.Resources._341;
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnExit.Location = new System.Drawing.Point(143, 10);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(62, 58);
            this.btnExit.TabIndex = 4;
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.BackColor = System.Drawing.Color.LightGray;
            this.btnClear.BackgroundImage = global::WindowsFormsApplication1.Properties.Resources.clear_red;
            this.btnClear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClear.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClear.Location = new System.Drawing.Point(75, 10);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(62, 58);
            this.btnClear.TabIndex = 1;
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackColor = System.Drawing.Color.LightGray;
            this.btnSave.BackgroundImage = global::WindowsFormsApplication1.Properties.Resources.Save__1_1;
            this.btnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSave.Location = new System.Drawing.Point(7, 10);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(62, 58);
            this.btnSave.TabIndex = 0;
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // WarehouseForm
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClear;
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.mainSplitContainer);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.MinimumSize = new System.Drawing.Size(1000, 600);
            this.Name = "WarehouseForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Warehouse Management";
            this.Load += new System.EventHandler(this.WarehouseForm_Load);
            this.Resize += new System.EventHandler(this.WarehouseForm_Resize);
            this.mainSplitContainer.Panel1.ResumeLayout(false);
            this.mainSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mainSplitContainer)).EndInit();
            this.mainSplitContainer.ResumeLayout(false);
            this.bottomButtonPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.dateFilterPanel.ResumeLayout(false);
            this.dateRightPanel.ResumeLayout(false);
            this.dateRightPanel.PerformLayout();
            this.dateLeftPanel.ResumeLayout(false);
            this.dateLeftPanel.PerformLayout();
            this.filterControlsPanel.ResumeLayout(false);
            this.searchPanel.ResumeLayout(false);
            this.searchPanel.PerformLayout();
            this.warehouseSelectorPanel.ResumeLayout(false);
            this.warehouseSelectorPanel.PerformLayout();
            this.headerPanel.ResumeLayout(false);
            this.formPanel.ResumeLayout(false);
            this.formPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.buttonPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer mainSplitContainer;
        private System.Windows.Forms.Panel bottomButtonPanel;
        private System.Windows.Forms.Button btnDeleteEntry;
        private System.Windows.Forms.Button btnAddToWarehouse;
        private System.Windows.Forms.Button btnViewDetails;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel searchPanel;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.Label searchLabel;
        private System.Windows.Forms.Panel warehouseSelectorPanel;
        private System.Windows.Forms.ComboBox cbWarehouses;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.Label headerLabel;
        private System.Windows.Forms.Panel formPanel;
        private System.Windows.Forms.TextBox txtLocation;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtWarehouseName;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Panel buttonPanel;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel filterControlsPanel;
        private System.Windows.Forms.Panel dateFilterPanel;
        private System.Windows.Forms.Button btnResetFilters;
        private System.Windows.Forms.Panel dateRightPanel;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel dateLeftPanel;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.Label label3;
    }
}