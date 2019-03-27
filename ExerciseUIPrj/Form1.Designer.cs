namespace ExerciseUIPrj
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuSave = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.customControl11 = new ExerciseUIPrj.controls.CustomControl1();
            this.loadControl1 = new ExerciseUIPrj.controls.LoadControl();
            this.xButton1 = new ExerciseUIPrj.XButton();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1137, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 文件ToolStripMenuItem
            // 
            this.文件ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuOpen,
            this.toolStripMenuEdit,
            this.toolStripMenuSave,
            this.toolStripMenuSaveAs});
            this.文件ToolStripMenuItem.Name = "文件ToolStripMenuItem";
            this.文件ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.文件ToolStripMenuItem.Text = "文件";
            // 
            // toolStripMenuOpen
            // 
            this.toolStripMenuOpen.Name = "toolStripMenuOpen";
            this.toolStripMenuOpen.Size = new System.Drawing.Size(100, 22);
            this.toolStripMenuOpen.Text = "打开";
            // 
            // toolStripMenuEdit
            // 
            this.toolStripMenuEdit.Name = "toolStripMenuEdit";
            this.toolStripMenuEdit.Size = new System.Drawing.Size(100, 22);
            this.toolStripMenuEdit.Text = "编辑";
            // 
            // toolStripMenuSave
            // 
            this.toolStripMenuSave.Name = "toolStripMenuSave";
            this.toolStripMenuSave.Size = new System.Drawing.Size(100, 22);
            this.toolStripMenuSave.Text = "保存";
            // 
            // toolStripMenuSaveAs
            // 
            this.toolStripMenuSaveAs.Name = "toolStripMenuSaveAs";
            this.toolStripMenuSaveAs.Size = new System.Drawing.Size(100, 22);
            this.toolStripMenuSaveAs.Text = "另存";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(169, 43);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 95);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(208, 223);
            this.textBox1.TabIndex = 3;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(295, 95);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(584, 394);
            this.flowLayoutPanel1.TabIndex = 6;
            // 
            // customControl11
            // 
            this.customControl11.BackColor = System.Drawing.Color.White;
            this.customControl11.Location = new System.Drawing.Point(58, 582);
            this.customControl11.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.customControl11.Name = "customControl11";
            this.customControl11.Size = new System.Drawing.Size(611, 58);
            this.customControl11.TabIndex = 5;
            this.customControl11.Text = "customControl11";
            // 
            // loadControl1
            // 
            this.loadControl1.Location = new System.Drawing.Point(12, 324);
            this.loadControl1.MinimumSize = new System.Drawing.Size(40, 80);
            this.loadControl1.Name = "loadControl1";
            this.loadControl1.Size = new System.Drawing.Size(221, 80);
            this.loadControl1.TabIndex = 4;
            this.loadControl1.Text = "loadControl1";
            // 
            // xButton1
            // 
            this.xButton1.EnterBackColor = System.Drawing.Color.Blue;
            this.xButton1.EnterForeColor = System.Drawing.Color.White;
            this.xButton1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.xButton1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.xButton1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.xButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xButton1.HoverBackColor = System.Drawing.Color.LightBlue;
            this.xButton1.HoverForeColor = System.Drawing.Color.White;
            this.xButton1.Location = new System.Drawing.Point(893, 64);
            this.xButton1.Name = "xButton1";
            this.xButton1.PressBackColor = System.Drawing.Color.DarkBlue;
            this.xButton1.PressForeColor = System.Drawing.Color.White;
            this.xButton1.Radius = 18;
            this.xButton1.Size = new System.Drawing.Size(142, 40);
            this.xButton1.TabIndex = 1;
            this.xButton1.Text = "xButton1";
            this.xButton1.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1137, 766);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.customControl11);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.loadControl1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.xButton1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Form1";
            this.ShowIcon = false;
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuOpen;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuSave;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuEdit;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuSaveAs;
        private XButton xButton1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private controls.LoadControl loadControl1;
        private controls.CustomControl1 customControl11;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    }
}

