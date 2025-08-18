namespace Calculator
{
    partial class Calculator
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Calculator));
            this.calcScreen = new System.Windows.Forms.TextBox();
            this.seven = new System.Windows.Forms.Button();
            this.eight = new System.Windows.Forms.Button();
            this.nine = new System.Windows.Forms.Button();
            this.sqrt = new System.Windows.Forms.Button();
            this.percent = new System.Windows.Forms.Button();
            this.multiply = new System.Windows.Forms.Button();
            this.six = new System.Windows.Forms.Button();
            this.five = new System.Windows.Forms.Button();
            this.four = new System.Windows.Forms.Button();
            this.inverse = new System.Windows.Forms.Button();
            this.subtract = new System.Windows.Forms.Button();
            this.three = new System.Windows.Forms.Button();
            this.two = new System.Windows.Forms.Button();
            this.one = new System.Windows.Forms.Button();
            this.equals = new System.Windows.Forms.Button();
            this.add = new System.Windows.Forms.Button();
            this.decimal_btn = new System.Windows.Forms.Button();
            this.switchSign = new System.Windows.Forms.Button();
            this.zero = new System.Windows.Forms.Button();
            this.backspace = new System.Windows.Forms.Button();
            this.clear_Entry = new System.Windows.Forms.Button();
            this.clear_All = new System.Windows.Forms.Button();
            this.divide = new System.Windows.Forms.Button();
            this.operation_txt = new System.Windows.Forms.TextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.status_txt = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copy_btn = new System.Windows.Forms.ToolStripMenuItem();
            this.paste_btn = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.digitGroup = new System.Windows.Forms.ToolStripMenuItem();
            this.about_btn = new System.Windows.Forms.ToolStripMenuItem();
            this.helpTopicstoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutCalculatorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.statusStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // calcScreen
            // 
            this.calcScreen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.calcScreen.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.calcScreen.Dock = System.Windows.Forms.DockStyle.Top;
            this.calcScreen.Font = new System.Drawing.Font("Microsoft Sans Serif", 52F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.calcScreen.Location = new System.Drawing.Point(0, 0);
            this.calcScreen.Margin = new System.Windows.Forms.Padding(4);
            this.calcScreen.MaximumSize = new System.Drawing.Size(450, 120);
            this.calcScreen.MaxLength = 32;
            this.calcScreen.MinimumSize = new System.Drawing.Size(450, 120);
            this.calcScreen.Name = "calcScreen";
            this.calcScreen.ReadOnly = true;
            this.calcScreen.Size = new System.Drawing.Size(450, 120);
            this.calcScreen.TabIndex = 0;
            this.calcScreen.TabStop = false;
            this.calcScreen.Text = "0";
            this.calcScreen.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // seven
            // 
            this.seven.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.seven.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.seven.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.seven.Location = new System.Drawing.Point(12, 204);
            this.seven.Margin = new System.Windows.Forms.Padding(4);
            this.seven.Name = "seven";
            this.seven.Size = new System.Drawing.Size(80, 60);
            this.seven.TabIndex = 2;
            this.seven.TabStop = false;
            this.seven.Text = "7";
            this.seven.UseVisualStyleBackColor = false;
            this.seven.Click += new System.EventHandler(this.number_btn);
            // 
            // eight
            // 
            this.eight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.eight.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.eight.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.eight.Location = new System.Drawing.Point(100, 204);
            this.eight.Margin = new System.Windows.Forms.Padding(4);
            this.eight.Name = "eight";
            this.eight.Size = new System.Drawing.Size(80, 60);
            this.eight.TabIndex = 3;
            this.eight.TabStop = false;
            this.eight.Text = "8";
            this.eight.UseVisualStyleBackColor = false;
            this.eight.Click += new System.EventHandler(this.number_btn);
            // 
            // nine
            // 
            this.nine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.nine.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nine.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.nine.Location = new System.Drawing.Point(188, 204);
            this.nine.Margin = new System.Windows.Forms.Padding(4);
            this.nine.Name = "nine";
            this.nine.Size = new System.Drawing.Size(80, 60);
            this.nine.TabIndex = 4;
            this.nine.TabStop = false;
            this.nine.Text = "9";
            this.nine.UseVisualStyleBackColor = false;
            this.nine.Click += new System.EventHandler(this.number_btn);
            // 
            // sqrt
            // 
            this.sqrt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.sqrt.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sqrt.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.sqrt.Location = new System.Drawing.Point(362, 135);
            this.sqrt.Margin = new System.Windows.Forms.Padding(4);
            this.sqrt.Name = "sqrt";
            this.sqrt.Size = new System.Drawing.Size(80, 60);
            this.sqrt.TabIndex = 6;
            this.sqrt.TabStop = false;
            this.sqrt.Text = "√";
            this.sqrt.UseVisualStyleBackColor = false;
            this.sqrt.Click += new System.EventHandler(this.other_btn);
            // 
            // percent
            // 
            this.percent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.percent.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.percent.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.percent.Location = new System.Drawing.Point(362, 203);
            this.percent.Margin = new System.Windows.Forms.Padding(4);
            this.percent.Name = "percent";
            this.percent.Size = new System.Drawing.Size(80, 60);
            this.percent.TabIndex = 11;
            this.percent.TabStop = false;
            this.percent.Text = "%";
            this.percent.UseVisualStyleBackColor = false;
            this.percent.Click += new System.EventHandler(this.other_btn);
            // 
            // multiply
            // 
            this.multiply.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.multiply.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.multiply.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.multiply.Location = new System.Drawing.Point(274, 271);
            this.multiply.Margin = new System.Windows.Forms.Padding(4);
            this.multiply.Name = "multiply";
            this.multiply.Size = new System.Drawing.Size(80, 60);
            this.multiply.TabIndex = 10;
            this.multiply.TabStop = false;
            this.multiply.Text = "×";
            this.multiply.UseVisualStyleBackColor = false;
            this.multiply.Click += new System.EventHandler(this.operation_btn);
            // 
            // six
            // 
            this.six.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.six.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.six.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.six.Location = new System.Drawing.Point(188, 272);
            this.six.Margin = new System.Windows.Forms.Padding(4);
            this.six.Name = "six";
            this.six.Size = new System.Drawing.Size(80, 60);
            this.six.TabIndex = 9;
            this.six.TabStop = false;
            this.six.Text = "6";
            this.six.UseVisualStyleBackColor = false;
            this.six.Click += new System.EventHandler(this.number_btn);
            // 
            // five
            // 
            this.five.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.five.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.five.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.five.Location = new System.Drawing.Point(100, 272);
            this.five.Margin = new System.Windows.Forms.Padding(4);
            this.five.Name = "five";
            this.five.Size = new System.Drawing.Size(80, 60);
            this.five.TabIndex = 8;
            this.five.TabStop = false;
            this.five.Text = "5";
            this.five.UseVisualStyleBackColor = false;
            this.five.Click += new System.EventHandler(this.number_btn);
            // 
            // four
            // 
            this.four.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.four.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.four.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.four.Location = new System.Drawing.Point(12, 272);
            this.four.Margin = new System.Windows.Forms.Padding(4);
            this.four.Name = "four";
            this.four.Size = new System.Drawing.Size(80, 60);
            this.four.TabIndex = 7;
            this.four.TabStop = false;
            this.four.Text = "4";
            this.four.UseVisualStyleBackColor = false;
            this.four.Click += new System.EventHandler(this.number_btn);
            // 
            // inverse
            // 
            this.inverse.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.inverse.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.inverse.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.inverse.Location = new System.Drawing.Point(362, 271);
            this.inverse.Margin = new System.Windows.Forms.Padding(4);
            this.inverse.Name = "inverse";
            this.inverse.Size = new System.Drawing.Size(80, 60);
            this.inverse.TabIndex = 16;
            this.inverse.TabStop = false;
            this.inverse.Text = "1/x";
            this.inverse.UseVisualStyleBackColor = false;
            this.inverse.Click += new System.EventHandler(this.other_btn);
            // 
            // subtract
            // 
            this.subtract.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.subtract.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.subtract.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.subtract.Location = new System.Drawing.Point(274, 339);
            this.subtract.Margin = new System.Windows.Forms.Padding(4);
            this.subtract.Name = "subtract";
            this.subtract.Size = new System.Drawing.Size(80, 60);
            this.subtract.TabIndex = 15;
            this.subtract.TabStop = false;
            this.subtract.Text = "-";
            this.subtract.UseVisualStyleBackColor = false;
            this.subtract.Click += new System.EventHandler(this.operation_btn);
            // 
            // three
            // 
            this.three.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.three.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.three.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.three.Location = new System.Drawing.Point(188, 340);
            this.three.Margin = new System.Windows.Forms.Padding(4);
            this.three.Name = "three";
            this.three.Size = new System.Drawing.Size(80, 60);
            this.three.TabIndex = 14;
            this.three.TabStop = false;
            this.three.Text = "3";
            this.three.UseVisualStyleBackColor = false;
            this.three.Click += new System.EventHandler(this.number_btn);
            // 
            // two
            // 
            this.two.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.two.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.two.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.two.Location = new System.Drawing.Point(100, 340);
            this.two.Margin = new System.Windows.Forms.Padding(4);
            this.two.Name = "two";
            this.two.Size = new System.Drawing.Size(80, 60);
            this.two.TabIndex = 13;
            this.two.TabStop = false;
            this.two.Text = "2";
            this.two.UseVisualStyleBackColor = false;
            this.two.Click += new System.EventHandler(this.number_btn);
            // 
            // one
            // 
            this.one.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.one.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.one.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.one.Location = new System.Drawing.Point(12, 340);
            this.one.Margin = new System.Windows.Forms.Padding(4);
            this.one.Name = "one";
            this.one.Size = new System.Drawing.Size(80, 60);
            this.one.TabIndex = 12;
            this.one.TabStop = false;
            this.one.Text = "1";
            this.one.UseVisualStyleBackColor = false;
            this.one.Click += new System.EventHandler(this.number_btn);
            // 
            // equals
            // 
            this.equals.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.equals.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.equals.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.equals.Location = new System.Drawing.Point(362, 339);
            this.equals.Margin = new System.Windows.Forms.Padding(4);
            this.equals.Name = "equals";
            this.equals.Size = new System.Drawing.Size(80, 132);
            this.equals.TabIndex = 21;
            this.equals.TabStop = false;
            this.equals.Text = "=";
            this.equals.UseVisualStyleBackColor = false;
            this.equals.Click += new System.EventHandler(this.equals_btn);
            // 
            // add
            // 
            this.add.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.add.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.add.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.add.Location = new System.Drawing.Point(274, 407);
            this.add.Margin = new System.Windows.Forms.Padding(4);
            this.add.Name = "add";
            this.add.Size = new System.Drawing.Size(80, 60);
            this.add.TabIndex = 20;
            this.add.TabStop = false;
            this.add.Text = "+";
            this.add.UseVisualStyleBackColor = false;
            this.add.Click += new System.EventHandler(this.operation_btn);
            // 
            // decimal_btn
            // 
            this.decimal_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.decimal_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.decimal_btn.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.decimal_btn.Location = new System.Drawing.Point(188, 407);
            this.decimal_btn.Margin = new System.Windows.Forms.Padding(4);
            this.decimal_btn.Name = "decimal_btn";
            this.decimal_btn.Size = new System.Drawing.Size(80, 60);
            this.decimal_btn.TabIndex = 19;
            this.decimal_btn.TabStop = false;
            this.decimal_btn.Text = ".";
            this.decimal_btn.UseVisualStyleBackColor = false;
            this.decimal_btn.Click += new System.EventHandler(this.other_btn);
            // 
            // switchSign
            // 
            this.switchSign.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.switchSign.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.switchSign.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.switchSign.Location = new System.Drawing.Point(274, 135);
            this.switchSign.Margin = new System.Windows.Forms.Padding(4);
            this.switchSign.Name = "switchSign";
            this.switchSign.Size = new System.Drawing.Size(80, 60);
            this.switchSign.TabIndex = 18;
            this.switchSign.TabStop = false;
            this.switchSign.Text = "+/-";
            this.switchSign.UseVisualStyleBackColor = false;
            this.switchSign.Click += new System.EventHandler(this.other_btn);
            // 
            // zero
            // 
            this.zero.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.zero.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.zero.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.zero.Location = new System.Drawing.Point(12, 407);
            this.zero.Margin = new System.Windows.Forms.Padding(4);
            this.zero.Name = "zero";
            this.zero.Size = new System.Drawing.Size(80, 60);
            this.zero.TabIndex = 17;
            this.zero.TabStop = false;
            this.zero.Text = "0";
            this.zero.UseVisualStyleBackColor = false;
            this.zero.Click += new System.EventHandler(this.number_btn);
            // 
            // backspace
            // 
            this.backspace.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.backspace.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.backspace.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.backspace.Location = new System.Drawing.Point(12, 135);
            this.backspace.Margin = new System.Windows.Forms.Padding(4);
            this.backspace.Name = "backspace";
            this.backspace.Size = new System.Drawing.Size(80, 60);
            this.backspace.TabIndex = 27;
            this.backspace.TabStop = false;
            this.backspace.Text = "←";
            this.backspace.UseVisualStyleBackColor = false;
            this.backspace.Click += new System.EventHandler(this.other_btn);
            // 
            // clear_Entry
            // 
            this.clear_Entry.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.clear_Entry.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clear_Entry.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.clear_Entry.Location = new System.Drawing.Point(188, 135);
            this.clear_Entry.Margin = new System.Windows.Forms.Padding(4);
            this.clear_Entry.Name = "clear_Entry";
            this.clear_Entry.Size = new System.Drawing.Size(80, 60);
            this.clear_Entry.TabIndex = 28;
            this.clear_Entry.TabStop = false;
            this.clear_Entry.Text = "CE";
            this.clear_Entry.UseVisualStyleBackColor = false;
            this.clear_Entry.Click += new System.EventHandler(this.clear_btn);
            // 
            // clear_All
            // 
            this.clear_All.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.clear_All.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clear_All.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.clear_All.Location = new System.Drawing.Point(100, 135);
            this.clear_All.Margin = new System.Windows.Forms.Padding(4);
            this.clear_All.Name = "clear_All";
            this.clear_All.Size = new System.Drawing.Size(80, 61);
            this.clear_All.TabIndex = 29;
            this.clear_All.TabStop = false;
            this.clear_All.Text = "C";
            this.clear_All.UseVisualStyleBackColor = false;
            this.clear_All.Click += new System.EventHandler(this.clear_btn);
            // 
            // divide
            // 
            this.divide.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.divide.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.divide.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.divide.Location = new System.Drawing.Point(274, 203);
            this.divide.Margin = new System.Windows.Forms.Padding(4);
            this.divide.Name = "divide";
            this.divide.Size = new System.Drawing.Size(80, 60);
            this.divide.TabIndex = 34;
            this.divide.TabStop = false;
            this.divide.Text = "÷";
            this.divide.UseVisualStyleBackColor = false;
            this.divide.Click += new System.EventHandler(this.operation_btn);
            // 
            // operation_txt
            // 
            this.operation_txt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.operation_txt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.operation_txt.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.operation_txt.Enabled = false;
            this.operation_txt.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.operation_txt.ForeColor = System.Drawing.Color.DimGray;
            this.operation_txt.Location = new System.Drawing.Point(362, 20);
            this.operation_txt.Margin = new System.Windows.Forms.Padding(4);
            this.operation_txt.Name = "operation_txt";
            this.operation_txt.ReadOnly = true;
            this.operation_txt.Size = new System.Drawing.Size(80, 31);
            this.operation_txt.TabIndex = 35;
            this.operation_txt.TabStop = false;
            this.operation_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.status_txt});
            this.statusStrip1.Location = new System.Drawing.Point(0, 480);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusStrip1.Size = new System.Drawing.Size(450, 22);
            this.statusStrip1.TabIndex = 62;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // status_txt
            // 
            this.status_txt.Name = "status_txt";
            this.status_txt.Size = new System.Drawing.Size(0, 16);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.divide);
            this.panel1.Controls.Add(this.equals);
            this.panel1.Controls.Add(this.seven);
            this.panel1.Controls.Add(this.operation_txt);
            this.panel1.Controls.Add(this.add);
            this.panel1.Controls.Add(this.eight);
            this.panel1.Controls.Add(this.decimal_btn);
            this.panel1.Controls.Add(this.backspace);
            this.panel1.Controls.Add(this.switchSign);
            this.panel1.Controls.Add(this.clear_All);
            this.panel1.Controls.Add(this.zero);
            this.panel1.Controls.Add(this.clear_Entry);
            this.panel1.Controls.Add(this.inverse);
            this.panel1.Controls.Add(this.nine);
            this.panel1.Controls.Add(this.subtract);
            this.panel1.Controls.Add(this.sqrt);
            this.panel1.Controls.Add(this.three);
            this.panel1.Controls.Add(this.four);
            this.panel1.Controls.Add(this.two);
            this.panel1.Controls.Add(this.five);
            this.panel1.Controls.Add(this.one);
            this.panel1.Controls.Add(this.six);
            this.panel1.Controls.Add(this.percent);
            this.panel1.Controls.Add(this.multiply);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(450, 502);
            this.panel1.TabIndex = 63;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button1.Location = new System.Drawing.Point(100, 407);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(80, 60);
            this.button1.TabIndex = 36;
            this.button1.TabStop = false;
            this.button1.Text = "00";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copy_btn,
            this.paste_btn});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(49, 24);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // copy_btn
            // 
            this.copy_btn.Name = "copy_btn";
            this.copy_btn.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.copy_btn.Size = new System.Drawing.Size(177, 26);
            this.copy_btn.Text = "Copy";
            this.copy_btn.Click += new System.EventHandler(this.menu_btn);
            // 
            // paste_btn
            // 
            this.paste_btn.Name = "paste_btn";
            this.paste_btn.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.paste_btn.Size = new System.Drawing.Size(177, 26);
            this.paste_btn.Text = "Paste";
            this.paste_btn.Click += new System.EventHandler(this.menu_btn);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.digitGroup});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(55, 24);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // digitGroup
            // 
            this.digitGroup.Name = "digitGroup";
            this.digitGroup.Size = new System.Drawing.Size(191, 26);
            this.digitGroup.Text = "Digit Grouping";
            this.digitGroup.Click += new System.EventHandler(this.menu_btn);
            // 
            // about_btn
            // 
            this.about_btn.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpTopicstoolStripMenuItem,
            this.aboutCalculatorToolStripMenuItem});
            this.about_btn.Name = "about_btn";
            this.about_btn.Size = new System.Drawing.Size(55, 24);
            this.about_btn.Text = "Help";
            // 
            // helpTopicstoolStripMenuItem
            // 
            this.helpTopicstoolStripMenuItem.Name = "helpTopicstoolStripMenuItem";
            this.helpTopicstoolStripMenuItem.Size = new System.Drawing.Size(204, 26);
            this.helpTopicstoolStripMenuItem.Text = "Documentation";
            this.helpTopicstoolStripMenuItem.Click += new System.EventHandler(this.helpPopup);
            // 
            // aboutCalculatorToolStripMenuItem
            // 
            this.aboutCalculatorToolStripMenuItem.Name = "aboutCalculatorToolStripMenuItem";
            this.aboutCalculatorToolStripMenuItem.Size = new System.Drawing.Size(204, 26);
            this.aboutCalculatorToolStripMenuItem.Text = "About Calculator";
            this.aboutCalculatorToolStripMenuItem.Click += new System.EventHandler(this.about_Popup);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.about_btn});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(450, 28);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.Visible = false;
            // 
            // Calculator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 502);
            this.Controls.Add(this.calcScreen);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(468, 549);
            this.MinimumSize = new System.Drawing.Size(468, 549);
            this.Name = "Calculator";
            this.Text = "Calculator";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Calculator_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Calculator_KeyPress);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox calcScreen;
        private System.Windows.Forms.Button seven;
        private System.Windows.Forms.Button eight;
        private System.Windows.Forms.Button sqrt;
        private System.Windows.Forms.Button percent;
        private System.Windows.Forms.Button multiply;
        private System.Windows.Forms.Button six;
        private System.Windows.Forms.Button five;
        private System.Windows.Forms.Button four;
        private System.Windows.Forms.Button inverse;
        private System.Windows.Forms.Button subtract;
        private System.Windows.Forms.Button three;
        private System.Windows.Forms.Button two;
        private System.Windows.Forms.Button one;
        private System.Windows.Forms.Button equals;
        private System.Windows.Forms.Button add;
        private System.Windows.Forms.Button decimal_btn;
        private System.Windows.Forms.Button switchSign;
        private System.Windows.Forms.Button zero;
        private System.Windows.Forms.Button backspace;
        private System.Windows.Forms.Button clear_Entry;
        private System.Windows.Forms.Button clear_All;
        private System.Windows.Forms.Button divide;
        private System.Windows.Forms.TextBox operation_txt;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel status_txt;
        private System.Windows.Forms.Button nine;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copy_btn;
        private System.Windows.Forms.ToolStripMenuItem paste_btn;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem digitGroup;
        private System.Windows.Forms.ToolStripMenuItem about_btn;
        private System.Windows.Forms.ToolStripMenuItem helpTopicstoolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutCalculatorToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.Button button1;
    }
}