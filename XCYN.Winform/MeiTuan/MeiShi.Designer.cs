namespace XCYN.Winform.MeiTuan
{
    partial class MeiShi
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.基本功能ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.更新城市ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.更新美食模块ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.取消ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.清除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.基本功能ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(645, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 基本功能ToolStripMenuItem
            // 
            this.基本功能ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.更新城市ToolStripMenuItem,
            this.更新美食模块ToolStripMenuItem,
            this.取消ToolStripMenuItem,
            this.清除ToolStripMenuItem});
            this.基本功能ToolStripMenuItem.Name = "基本功能ToolStripMenuItem";
            this.基本功能ToolStripMenuItem.Size = new System.Drawing.Size(81, 24);
            this.基本功能ToolStripMenuItem.Text = "基本功能";
            // 
            // 更新城市ToolStripMenuItem
            // 
            this.更新城市ToolStripMenuItem.Name = "更新城市ToolStripMenuItem";
            this.更新城市ToolStripMenuItem.Size = new System.Drawing.Size(216, 26);
            this.更新城市ToolStripMenuItem.Text = "更新城市";
            this.更新城市ToolStripMenuItem.Click += new System.EventHandler(this.更新城市ToolStripMenuItem_Click);
            // 
            // 更新美食模块ToolStripMenuItem
            // 
            this.更新美食模块ToolStripMenuItem.Name = "更新美食模块ToolStripMenuItem";
            this.更新美食模块ToolStripMenuItem.Size = new System.Drawing.Size(216, 26);
            this.更新美食模块ToolStripMenuItem.Text = "更新美食模块";
            this.更新美食模块ToolStripMenuItem.Click += new System.EventHandler(this.更新美食模块ToolStripMenuItem_Click);
            // 
            // 取消ToolStripMenuItem
            // 
            this.取消ToolStripMenuItem.Name = "取消ToolStripMenuItem";
            this.取消ToolStripMenuItem.Size = new System.Drawing.Size(216, 26);
            this.取消ToolStripMenuItem.Text = "取消";
            this.取消ToolStripMenuItem.Click += new System.EventHandler(this.取消ToolStripMenuItem_Click);
            // 
            // 清除ToolStripMenuItem
            // 
            this.清除ToolStripMenuItem.Name = "清除ToolStripMenuItem";
            this.清除ToolStripMenuItem.Size = new System.Drawing.Size(216, 26);
            this.清除ToolStripMenuItem.Text = "清除";
            // 
            // listBox1
            // 
            this.listBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 15;
            this.listBox1.Location = new System.Drawing.Point(0, 28);
            this.listBox1.Margin = new System.Windows.Forms.Padding(4);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(645, 548);
            this.listBox1.TabIndex = 1;
            // 
            // MeiShi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(645, 576);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MeiShi";
            this.Text = "MeiShi";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 基本功能ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 更新美食模块ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 取消ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 清除ToolStripMenuItem;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.ToolStripMenuItem 更新城市ToolStripMenuItem;
    }
}