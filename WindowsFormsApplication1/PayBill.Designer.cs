namespace WindowsFormsApplication1
{
    partial class PayBill
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.start_date = new System.Windows.Forms.DateTimePicker();
            this.end_date = new System.Windows.Forms.DateTimePicker();
            this.company = new System.Windows.Forms.TextBox();
            this.customer = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.labelTotalBill = new System.Windows.Forms.Label();
            this.labelAmountPaid = new System.Windows.Forms.Label();
            this.labelRemaining = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.AutoCompleteCustomSource.AddRange(new string[] {
            "abc",
            "def",
            "ghi",
            "jkl"});
            this.textBox1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.textBox1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.textBox1.Font = new System.Drawing.Font("MS Reference Sans Serif", 15F);
            this.textBox1.Location = new System.Drawing.Point(278, 67);
            this.textBox1.MaximumSize = new System.Drawing.Size(378, 32);
            this.textBox1.MinimumSize = new System.Drawing.Size(260, 32);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(308, 38);
            this.textBox1.TabIndex = 131;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("MS Reference Sans Serif", 15F, System.Drawing.FontStyle.Bold);
            this.label8.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label8.Location = new System.Drawing.Point(80, 70);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(230, 32);
            this.label8.TabIndex = 132;
            this.label8.Text = "Customer Name";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("MS Reference Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(259, 19);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(198, 29);
            this.label12.TabIndex = 130;
            this.label12.Text = "BILL PAYMENT";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBox2
            // 
            this.textBox2.AutoCompleteCustomSource.AddRange(new string[] {
            "abc",
            "def",
            "ghi",
            "jkl"});
            this.textBox2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.textBox2.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.textBox2.Font = new System.Drawing.Font("MS Reference Sans Serif", 15F);
            this.textBox2.Location = new System.Drawing.Point(278, 116);
            this.textBox2.MaximumSize = new System.Drawing.Size(378, 32);
            this.textBox2.MinimumSize = new System.Drawing.Size(260, 32);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(308, 38);
            this.textBox2.TabIndex = 133;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS Reference Sans Serif", 15F, System.Drawing.FontStyle.Bold);
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(80, 119);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(226, 32);
            this.label1.TabIndex = 134;
            this.label1.Text = "Company Name";
            // 
            // textBox3
            // 
            this.textBox3.AutoCompleteCustomSource.AddRange(new string[] {
            "abc",
            "def",
            "ghi",
            "jkl"});
            this.textBox3.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.textBox3.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.textBox3.Font = new System.Drawing.Font("MS Reference Sans Serif", 15F);
            this.textBox3.Location = new System.Drawing.Point(278, 164);
            this.textBox3.MaximumSize = new System.Drawing.Size(378, 32);
            this.textBox3.MinimumSize = new System.Drawing.Size(260, 32);
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(308, 38);
            this.textBox3.TabIndex = 135;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("MS Reference Sans Serif", 15F, System.Drawing.FontStyle.Bold);
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(80, 167);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(213, 32);
            this.label2.TabIndex = 136;
            this.label2.Text = "Phone Number";
            // 
            // textBox4
            // 
            this.textBox4.AutoCompleteCustomSource.AddRange(new string[] {
            "abc",
            "def",
            "ghi",
            "jkl"});
            this.textBox4.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.textBox4.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.textBox4.Font = new System.Drawing.Font("MS Reference Sans Serif", 15F);
            this.textBox4.Location = new System.Drawing.Point(278, 211);
            this.textBox4.MaximumSize = new System.Drawing.Size(378, 32);
            this.textBox4.MinimumSize = new System.Drawing.Size(260, 32);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(308, 38);
            this.textBox4.TabIndex = 137;
            this.textBox4.TextChanged += new System.EventHandler(this.textBox4_TextChanged);
            this.textBox4.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox4_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("MS Reference Sans Serif", 15F, System.Drawing.FontStyle.Bold);
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(80, 214);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(184, 32);
            this.label3.TabIndex = 138;
            this.label3.Text = "Amount Paid";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.button1.Location = new System.Drawing.Point(278, 380);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(147, 45);
            this.button1.TabIndex = 140;
            this.button1.Text = "Save";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button6
            // 
            this.button6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.button6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.button6.ForeColor = System.Drawing.Color.White;
            this.button6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.button6.Location = new System.Drawing.Point(439, 380);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(147, 45);
            this.button6.TabIndex = 139;
            this.button6.Text = "Back";
            this.button6.UseVisualStyleBackColor = false;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // start_date
            // 
            this.start_date.Location = new System.Drawing.Point(12, 333);
            this.start_date.Name = "start_date";
            this.start_date.Size = new System.Drawing.Size(200, 30);
            this.start_date.TabIndex = 141;
            this.start_date.Visible = false;
            // 
            // end_date
            // 
            this.end_date.Location = new System.Drawing.Point(218, 333);
            this.end_date.Name = "end_date";
            this.end_date.Size = new System.Drawing.Size(200, 30);
            this.end_date.TabIndex = 142;
            this.end_date.Visible = false;
            // 
            // company
            // 
            this.company.Location = new System.Drawing.Point(530, 333);
            this.company.Name = "company";
            this.company.Size = new System.Drawing.Size(100, 30);
            this.company.TabIndex = 143;
            this.company.Visible = false;
            // 
            // customer
            // 
            this.customer.Location = new System.Drawing.Point(424, 333);
            this.customer.Name = "customer";
            this.customer.Size = new System.Drawing.Size(100, 30);
            this.customer.TabIndex = 144;
            this.customer.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("MS Reference Sans Serif", 15F, System.Drawing.FontStyle.Bold);
            this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label4.Location = new System.Drawing.Point(80, 260);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(141, 32);
            this.label4.TabIndex = 145;
            this.label4.Text = "Total Bill:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("MS Reference Sans Serif", 15F, System.Drawing.FontStyle.Bold);
            this.label5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label5.Location = new System.Drawing.Point(80, 300);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(196, 32);
            this.label5.TabIndex = 146;
            this.label5.Text = "Amount Paid:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("MS Reference Sans Serif", 15F, System.Drawing.FontStyle.Bold);
            this.label6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label6.Location = new System.Drawing.Point(80, 340);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(166, 32);
            this.label6.TabIndex = 147;
            this.label6.Text = "Remaining:";
            // 
            // labelTotalBill
            // 
            this.labelTotalBill.AutoSize = true;
            this.labelTotalBill.Font = new System.Drawing.Font("MS Reference Sans Serif", 15F);
            this.labelTotalBill.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelTotalBill.Location = new System.Drawing.Point(273, 260);
            this.labelTotalBill.Name = "labelTotalBill";
            this.labelTotalBill.Size = new System.Drawing.Size(0, 32);
            this.labelTotalBill.TabIndex = 148;
            // 
            // labelAmountPaid
            // 
            this.labelAmountPaid.AutoSize = true;
            this.labelAmountPaid.Font = new System.Drawing.Font("MS Reference Sans Serif", 15F);
            this.labelAmountPaid.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelAmountPaid.Location = new System.Drawing.Point(273, 300);
            this.labelAmountPaid.Name = "labelAmountPaid";
            this.labelAmountPaid.Size = new System.Drawing.Size(0, 32);
            this.labelAmountPaid.TabIndex = 149;
            // 
            // labelRemaining
            // 
            this.labelRemaining.AutoSize = true;
            this.labelRemaining.Font = new System.Drawing.Font("MS Reference Sans Serif", 15F);
            this.labelRemaining.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelRemaining.Location = new System.Drawing.Point(273, 340);
            this.labelRemaining.Name = "labelRemaining";
            this.labelRemaining.Size = new System.Drawing.Size(0, 32);
            this.labelRemaining.TabIndex = 150;
            // 
            // PayBill
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(215)))), ((int)(((byte)(155)))));
            this.ClientSize = new System.Drawing.Size(682, 453);
            this.Controls.Add(this.labelRemaining);
            this.Controls.Add(this.labelAmountPaid);
            this.Controls.Add(this.labelTotalBill);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.customer);
            this.Controls.Add(this.company);
            this.Controls.Add(this.end_date);
            this.Controls.Add(this.start_date);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label12);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximumSize = new System.Drawing.Size(700, 500);
            this.MinimumSize = new System.Drawing.Size(700, 500);
            this.Name = "PayBill";
            this.Text = "PayBill";
            this.Load += new System.EventHandler(this.PayBill_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox textBox1;
        public System.Windows.Forms.TextBox textBox2;
        public System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button6;
        public System.Windows.Forms.DateTimePicker start_date;
        public System.Windows.Forms.DateTimePicker end_date;
        public System.Windows.Forms.TextBox company;
        public System.Windows.Forms.TextBox customer;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label labelTotalBill;
        private System.Windows.Forms.Label labelAmountPaid;
        private System.Windows.Forms.Label labelRemaining;
    }
}