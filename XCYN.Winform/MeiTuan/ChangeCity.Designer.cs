namespace XCYN.Winform.MeiTuan
{
    partial class ChangeCity
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
            this.基础功能BToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.启动SToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.取消CToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.清空LToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.基础功能BToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(435, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 基础功能BToolStripMenuItem
            // 
            this.基础功能BToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.启动SToolStripMenuItem,
            this.取消CToolStripMenuItem,
            this.清空LToolStripMenuItem});
            this.基础功能BToolStripMenuItem.Name = "基础功能BToolStripMenuItem";
            this.基础功能BToolStripMenuItem.Size = new System.Drawing.Size(84, 21);
            this.基础功能BToolStripMenuItem.Text = "基础功能[B]";
            // 
            // 启动SToolStripMenuItem
            // 
            this.启动SToolStripMenuItem.Name = "启动SToolStripMenuItem";
            this.启动SToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.启动SToolStripMenuItem.Text = "启动[S]";
            this.启动SToolStripMenuItem.Click += new System.EventHandler(this.启动SToolStripMenuItem_Click);
            // 
            // 取消CToolStripMenuItem
            // 
            this.取消CToolStripMenuItem.Name = "取消CToolStripMenuItem";
            this.取消CToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.取消CToolStripMenuItem.Text = "取消[C]";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(12, 28);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(411, 328);
            this.listBox1.TabIndex = 1;
            // 
            // 清空LToolStripMenuItem
            // 
            this.清空LToolStripMenuItem.Name = "清空LToolStripMenuItem";
            this.清空LToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.清空LToolStripMenuItem.Text = "清空[L]";
            this.清空LToolStripMenuItem.Click += new System.EventHandler(this.清空LToolStripMenuItem_Click);
            // 
            // ChangeCity
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(435, 376);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "ChangeCity";
            this.Text = "ChangeCity";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 基础功能BToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 启动SToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 取消CToolStripMenuItem;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.ToolStripMenuItem 清空LToolStripMenuItem;
    }
}