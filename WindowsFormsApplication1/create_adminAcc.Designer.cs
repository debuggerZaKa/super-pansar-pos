using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    partial class AdminUserManagementForm
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
            this.deleteSelectedButton = new System.Windows.Forms.Button();
            this.editSelectedButton = new System.Windows.Forms.Button();
            this.showSelectedPasswordButton = new System.Windows.Forms.Button();
            this.accountsGridView = new System.Windows.Forms.DataGridView();
            this.searchPanel = new System.Windows.Forms.Panel();
            this.searchTextBox = new System.Windows.Forms.TextBox();
            this.searchButton = new System.Windows.Forms.Button();
            this.searchLabel = new System.Windows.Forms.Label();
            this.formPanel = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.showPasswordCheckBox = new System.Windows.Forms.CheckBox();
            this.confirmPasswordTextBox = new System.Windows.Forms.TextBox();
            this.confirmPasswordLabel = new System.Windows.Forms.Label();
            this.createdDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.passwordTextBox = new System.Windows.Forms.TextBox();
            this.userNameTextBox = new System.Windows.Forms.TextBox();
            this.createdAtLabel = new System.Windows.Forms.Label();
            this.passwordLabel = new System.Windows.Forms.Label();
            this.userNameLabel = new System.Windows.Forms.Label();
            this.headerLabel = new System.Windows.Forms.Label();
            this.buttonPanel = new System.Windows.Forms.Panel();
            this.cancelButton = new System.Windows.Forms.Button();
            this.createButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.mainSplitContainer)).BeginInit();
            this.mainSplitContainer.Panel1.SuspendLayout();
            this.mainSplitContainer.Panel2.SuspendLayout();
            this.mainSplitContainer.SuspendLayout();
            this.bottomButtonPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.accountsGridView)).BeginInit();
            this.searchPanel.SuspendLayout();
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
            this.mainSplitContainer.Panel1.Controls.Add(this.accountsGridView);
            this.mainSplitContainer.Panel1.Controls.Add(this.searchPanel);
            this.mainSplitContainer.Panel1.Padding = new System.Windows.Forms.Padding(10);
            // 
            // mainSplitContainer.Panel2
            // 
            this.mainSplitContainer.Panel2.BackColor = System.Drawing.Color.Gainsboro;
            this.mainSplitContainer.Panel2.Controls.Add(this.formPanel);
            this.mainSplitContainer.Panel2.Controls.Add(this.headerLabel);
            this.mainSplitContainer.Panel2.Controls.Add(this.buttonPanel);
            this.mainSplitContainer.Panel2.Padding = new System.Windows.Forms.Padding(10);
            this.mainSplitContainer.Size = new System.Drawing.Size(984, 561);
            this.mainSplitContainer.SplitterDistance = 500;
            this.mainSplitContainer.TabIndex = 0;
            // 
            // bottomButtonPanel
            // 
            this.bottomButtonPanel.Controls.Add(this.deleteSelectedButton);
            this.bottomButtonPanel.Controls.Add(this.editSelectedButton);
            this.bottomButtonPanel.Controls.Add(this.showSelectedPasswordButton);
            this.bottomButtonPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottomButtonPanel.Location = new System.Drawing.Point(10, 511);
            this.bottomButtonPanel.Name = "bottomButtonPanel";
            this.bottomButtonPanel.Size = new System.Drawing.Size(480, 40);
            this.bottomButtonPanel.TabIndex = 3;
            // 
            // deleteSelectedButton
            // 
            this.deleteSelectedButton.BackColor = System.Drawing.Color.LightCoral;
            this.deleteSelectedButton.Dock = System.Windows.Forms.DockStyle.Left;
            this.deleteSelectedButton.Location = new System.Drawing.Point(320, 0);
            this.deleteSelectedButton.Name = "deleteSelectedButton";
            this.deleteSelectedButton.Size = new System.Drawing.Size(160, 40);
            this.deleteSelectedButton.TabIndex = 2;
            this.deleteSelectedButton.Text = "Delete Selected";
            this.deleteSelectedButton.UseVisualStyleBackColor = false;
            this.deleteSelectedButton.Click += new System.EventHandler(this.deleteSelectedButton_Click);
            // 
            // editSelectedButton
            // 
            this.editSelectedButton.BackColor = System.Drawing.Color.LightSkyBlue;
            this.editSelectedButton.Dock = System.Windows.Forms.DockStyle.Left;
            this.editSelectedButton.Location = new System.Drawing.Point(160, 0);
            this.editSelectedButton.Name = "editSelectedButton";
            this.editSelectedButton.Size = new System.Drawing.Size(160, 40);
            this.editSelectedButton.TabIndex = 3;
            this.editSelectedButton.Text = "Edit Selected";
            this.editSelectedButton.UseVisualStyleBackColor = false;
            this.editSelectedButton.Click += new System.EventHandler(this.editSelectedButton_Click);
            // 
            // showSelectedPasswordButton
            // 
            this.showSelectedPasswordButton.BackColor = System.Drawing.Color.Silver;
            this.showSelectedPasswordButton.Dock = System.Windows.Forms.DockStyle.Left;
            this.showSelectedPasswordButton.Location = new System.Drawing.Point(0, 0);
            this.showSelectedPasswordButton.Name = "showSelectedPasswordButton";
            this.showSelectedPasswordButton.Size = new System.Drawing.Size(160, 40);
            this.showSelectedPasswordButton.TabIndex = 1;
            this.showSelectedPasswordButton.Text = "Show Password";
            this.showSelectedPasswordButton.UseVisualStyleBackColor = false;
            this.showSelectedPasswordButton.Click += new System.EventHandler(this.showSelectedPasswordButton_Click);
            // 
            // accountsGridView
            // 
            this.accountsGridView.AllowUserToAddRows = false;
            this.accountsGridView.AllowUserToDeleteRows = false;
            this.accountsGridView.BackgroundColor = System.Drawing.Color.White;
            this.accountsGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.accountsGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.accountsGridView.Location = new System.Drawing.Point(10, 50);
            this.accountsGridView.Name = "accountsGridView";
            this.accountsGridView.ReadOnly = true;
            this.accountsGridView.RowHeadersVisible = false;
            this.accountsGridView.RowHeadersWidth = 51;
            this.accountsGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.accountsGridView.Size = new System.Drawing.Size(480, 501);
            this.accountsGridView.TabIndex = 0;
            this.accountsGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.accountsGridView_CellContentClick_1);
            // 
            // searchPanel
            // 
            this.searchPanel.Controls.Add(this.searchTextBox);
            this.searchPanel.Controls.Add(this.searchButton);
            this.searchPanel.Controls.Add(this.searchLabel);
            this.searchPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.searchPanel.Location = new System.Drawing.Point(10, 10);
            this.searchPanel.Name = "searchPanel";
            this.searchPanel.Size = new System.Drawing.Size(480, 40);
            this.searchPanel.TabIndex = 4;
            // 
            // searchTextBox
            // 
            this.searchTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.searchTextBox.Location = new System.Drawing.Point(66, 8);
            this.searchTextBox.Name = "searchTextBox";
            this.searchTextBox.Size = new System.Drawing.Size(334, 27);
            this.searchTextBox.TabIndex = 2;
            this.searchTextBox.TextChanged += new System.EventHandler(this.searchTextBox_TextChanged);
            // 
            // searchButton
            // 
            this.searchButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.searchButton.Location = new System.Drawing.Point(406, 8);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(70, 27);
            this.searchButton.TabIndex = 1;
            this.searchButton.Text = "Search";
            this.searchButton.UseVisualStyleBackColor = true;
            this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
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
            // formPanel
            // 
            this.formPanel.BackColor = System.Drawing.Color.White;
            this.formPanel.Controls.Add(this.pictureBox1);
            this.formPanel.Controls.Add(this.showPasswordCheckBox);
            this.formPanel.Controls.Add(this.confirmPasswordTextBox);
            this.formPanel.Controls.Add(this.confirmPasswordLabel);
            this.formPanel.Controls.Add(this.createdDateTimePicker);
            this.formPanel.Controls.Add(this.passwordTextBox);
            this.formPanel.Controls.Add(this.userNameTextBox);
            this.formPanel.Controls.Add(this.createdAtLabel);
            this.formPanel.Controls.Add(this.passwordLabel);
            this.formPanel.Controls.Add(this.userNameLabel);
            this.formPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.formPanel.Location = new System.Drawing.Point(10, 50);
            this.formPanel.Name = "formPanel";
            this.formPanel.Padding = new System.Windows.Forms.Padding(20);
            this.formPanel.Size = new System.Drawing.Size(460, 421);
            this.formPanel.TabIndex = 2;
            this.formPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.formPanel_Paint);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImage = global::WindowsFormsApplication1.Properties.Resources.Untitled_design___2025_07_04T215835_764;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(141, 16);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(192, 87);
            this.pictureBox1.TabIndex = 38;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // showPasswordCheckBox
            // 
            this.showPasswordCheckBox.AutoSize = true;
            this.showPasswordCheckBox.Location = new System.Drawing.Point(164, 264);
            this.showPasswordCheckBox.Name = "showPasswordCheckBox";
            this.showPasswordCheckBox.Size = new System.Drawing.Size(132, 24);
            this.showPasswordCheckBox.TabIndex = 11;
            this.showPasswordCheckBox.Text = "Show Password";
            this.showPasswordCheckBox.UseVisualStyleBackColor = true;
            this.showPasswordCheckBox.CheckedChanged += new System.EventHandler(this.showPasswordCheckBox_CheckedChanged);
            // 
            // confirmPasswordTextBox
            // 
            this.confirmPasswordTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.confirmPasswordTextBox.Location = new System.Drawing.Point(162, 224);
            this.confirmPasswordTextBox.Name = "confirmPasswordTextBox";
            this.confirmPasswordTextBox.Size = new System.Drawing.Size(249, 27);
            this.confirmPasswordTextBox.TabIndex = 10;
            // 
            // confirmPasswordLabel
            // 
            this.confirmPasswordLabel.AutoSize = true;
            this.confirmPasswordLabel.Location = new System.Drawing.Point(23, 227);
            this.confirmPasswordLabel.Name = "confirmPasswordLabel";
            this.confirmPasswordLabel.Size = new System.Drawing.Size(130, 20);
            this.confirmPasswordLabel.TabIndex = 9;
            this.confirmPasswordLabel.Text = "Confirm Password:";
            // 
            // createdDateTimePicker
            // 
            this.createdDateTimePicker.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.createdDateTimePicker.CustomFormat = "MM/dd/yyyy hh:mm tt";
            this.createdDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.createdDateTimePicker.Location = new System.Drawing.Point(162, 304);
            this.createdDateTimePicker.Name = "createdDateTimePicker";
            this.createdDateTimePicker.Size = new System.Drawing.Size(249, 27);
            this.createdDateTimePicker.TabIndex = 8;
            // 
            // passwordTextBox
            // 
            this.passwordTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.passwordTextBox.Location = new System.Drawing.Point(162, 184);
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.Size = new System.Drawing.Size(249, 27);
            this.passwordTextBox.TabIndex = 7;
            this.passwordTextBox.TextChanged += new System.EventHandler(this.passwordTextBox_TextChanged);
            // 
            // userNameTextBox
            // 
            this.userNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.userNameTextBox.Location = new System.Drawing.Point(162, 144);
            this.userNameTextBox.Name = "userNameTextBox";
            this.userNameTextBox.Size = new System.Drawing.Size(249, 27);
            this.userNameTextBox.TabIndex = 6;
            // 
            // createdAtLabel
            // 
            this.createdAtLabel.AutoSize = true;
            this.createdAtLabel.Location = new System.Drawing.Point(23, 310);
            this.createdAtLabel.Name = "createdAtLabel";
            this.createdAtLabel.Size = new System.Drawing.Size(83, 20);
            this.createdAtLabel.TabIndex = 4;
            this.createdAtLabel.Text = "Created At:";
            // 
            // passwordLabel
            // 
            this.passwordLabel.AutoSize = true;
            this.passwordLabel.Location = new System.Drawing.Point(23, 187);
            this.passwordLabel.Name = "passwordLabel";
            this.passwordLabel.Size = new System.Drawing.Size(73, 20);
            this.passwordLabel.TabIndex = 3;
            this.passwordLabel.Text = "Password:";
            // 
            // userNameLabel
            // 
            this.userNameLabel.AutoSize = true;
            this.userNameLabel.Location = new System.Drawing.Point(23, 147);
            this.userNameLabel.Name = "userNameLabel";
            this.userNameLabel.Size = new System.Drawing.Size(78, 20);
            this.userNameLabel.TabIndex = 2;
            this.userNameLabel.Text = "Username:";
            // 
            // headerLabel
            // 
            this.headerLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerLabel.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.headerLabel.Location = new System.Drawing.Point(10, 10);
            this.headerLabel.Name = "headerLabel";
            this.headerLabel.Size = new System.Drawing.Size(460, 40);
            this.headerLabel.TabIndex = 1;
            this.headerLabel.Text = "Create New Admin User";
            this.headerLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonPanel
            // 
            this.buttonPanel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.buttonPanel.Controls.Add(this.cancelButton);
            this.buttonPanel.Controls.Add(this.createButton);
            this.buttonPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.buttonPanel.Location = new System.Drawing.Point(10, 471);
            this.buttonPanel.Name = "buttonPanel";
            this.buttonPanel.Padding = new System.Windows.Forms.Padding(10);
            this.buttonPanel.Size = new System.Drawing.Size(460, 80);
            this.buttonPanel.TabIndex = 0;
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.BackColor = System.Drawing.Color.Gainsboro;
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cancelButton.Location = new System.Drawing.Point(354, 13);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(100, 54);
            this.cancelButton.TabIndex = 1;
            this.cancelButton.Text = "Clear Form";
            this.cancelButton.UseVisualStyleBackColor = false;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // createButton
            // 
            this.createButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.createButton.BackColor = System.Drawing.Color.Silver;
            this.createButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.createButton.Location = new System.Drawing.Point(248, 13);
            this.createButton.Name = "createButton";
            this.createButton.Size = new System.Drawing.Size(100, 54);
            this.createButton.TabIndex = 0;
            this.createButton.Text = "Create User";
            this.createButton.UseVisualStyleBackColor = false;
            this.createButton.Click += new System.EventHandler(this.createButton_Click);
            // 
            // AdminUserManagementForm
            // 
            this.AcceptButton = this.createButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(984, 561);
            this.Controls.Add(this.mainSplitContainer);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.MinimumSize = new System.Drawing.Size(1000, 600);
            this.Name = "AdminUserManagementForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Admin User Management";
            this.Load += new System.EventHandler(this.AdminUserManagementForm_Load);
            this.mainSplitContainer.Panel1.ResumeLayout(false);
            this.mainSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mainSplitContainer)).EndInit();
            this.mainSplitContainer.ResumeLayout(false);
            this.bottomButtonPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.accountsGridView)).EndInit();
            this.searchPanel.ResumeLayout(false);
            this.searchPanel.PerformLayout();
            this.formPanel.ResumeLayout(false);
            this.formPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.buttonPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer mainSplitContainer;
        private System.Windows.Forms.DataGridView accountsGridView;
        private System.Windows.Forms.Panel formPanel;
        private System.Windows.Forms.CheckBox showPasswordCheckBox;
        private System.Windows.Forms.TextBox confirmPasswordTextBox;
        private System.Windows.Forms.Label confirmPasswordLabel;
        private System.Windows.Forms.DateTimePicker createdDateTimePicker;
        private System.Windows.Forms.TextBox passwordTextBox;
        private System.Windows.Forms.TextBox userNameTextBox;
        private System.Windows.Forms.Label createdAtLabel;
        private System.Windows.Forms.Label passwordLabel;
        private System.Windows.Forms.Label userNameLabel;
        private System.Windows.Forms.Label headerLabel;
        private System.Windows.Forms.Panel buttonPanel;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button createButton;
        private System.Windows.Forms.Panel bottomButtonPanel;
        private System.Windows.Forms.Button deleteSelectedButton;
        private System.Windows.Forms.Button showSelectedPasswordButton;
        private System.Windows.Forms.Button editSelectedButton;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel searchPanel;
        private System.Windows.Forms.TextBox searchTextBox;
        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.Label searchLabel;
    }
}