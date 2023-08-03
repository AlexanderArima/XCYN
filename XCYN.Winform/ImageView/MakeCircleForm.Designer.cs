namespace XCYN.Winform.ImageView
{
    partial class MakeCircleForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.dSkinPictureBox1 = new DSkin.Controls.DSkinPictureBox();
            this.dSkinPictureBox2 = new DSkin.Controls.DSkinPictureBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dSkinPictureBox1);
            this.groupBox1.Location = new System.Drawing.Point(12, 24);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(260, 280);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "原图";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dSkinPictureBox2);
            this.groupBox2.Location = new System.Drawing.Point(312, 24);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(260, 280);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "结果图";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 310);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "上传原图";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(93, 310);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "转换";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(174, 310);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 4;
            this.button3.Text = "导出";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(255, 310);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 5;
            this.button4.Text = "关闭";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // dSkinPictureBox1
            // 
            this.dSkinPictureBox1.Image = null;
            this.dSkinPictureBox1.Images = null;
            this.dSkinPictureBox1.Location = new System.Drawing.Point(5, 20);
            this.dSkinPictureBox1.Name = "dSkinPictureBox1";
            this.dSkinPictureBox1.Size = new System.Drawing.Size(250, 250);
            this.dSkinPictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.dSkinPictureBox1.TabIndex = 0;
            this.dSkinPictureBox1.Text = "dSkinPictureBox1";
            // 
            // dSkinPictureBox2
            // 
            this.dSkinPictureBox2.Image = null;
            this.dSkinPictureBox2.Images = null;
            this.dSkinPictureBox2.Location = new System.Drawing.Point(4, 24);
            this.dSkinPictureBox2.Name = "dSkinPictureBox2";
            this.dSkinPictureBox2.Size = new System.Drawing.Size(250, 250);
            this.dSkinPictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.dSkinPictureBox2.TabIndex = 1;
            this.dSkinPictureBox2.Text = "dSkinPictureBox2";
            // 
            // MakeCircleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderColor = System.Drawing.Color.Gray;
            this.CanResize = false;
            this.ClientSize = new System.Drawing.Size(585, 355);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.DoubleClickMaximized = false;
            this.DrawIcon = false;
            this.EnableAnimation = false;
            this.IsLayeredWindowForm = false;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MakeCircleForm";
            this.ShowSystemButtons = false;
            this.Text = "";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private DSkin.Controls.DSkinPictureBox dSkinPictureBox1;
        private DSkin.Controls.DSkinPictureBox dSkinPictureBox2;
    }
}