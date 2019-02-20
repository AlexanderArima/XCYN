namespace XCYN.Winform.Quartz.Views
{
    partial class TimerForm
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
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.刷新ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.添加计划ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.修改计划ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.添加服务ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.服务列表ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.timerFormDataSet1BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataSet1 = new XCYN.Winform.Quartz.DataSet1();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStartTime = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEndTime = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRepeatTime = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRepeatInterval = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colServiceName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timerFormDataSet1BindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.刷新ToolStripMenuItem,
            this.添加计划ToolStripMenuItem,
            this.修改计划ToolStripMenuItem,
            this.添加服务ToolStripMenuItem,
            this.服务列表ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(747, 25);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 刷新ToolStripMenuItem
            // 
            this.刷新ToolStripMenuItem.Name = "刷新ToolStripMenuItem";
            this.刷新ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.刷新ToolStripMenuItem.Text = "刷新";
            this.刷新ToolStripMenuItem.Click += new System.EventHandler(this.刷新ToolStripMenuItem_Click);
            // 
            // 添加计划ToolStripMenuItem
            // 
            this.添加计划ToolStripMenuItem.Name = "添加计划ToolStripMenuItem";
            this.添加计划ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.添加计划ToolStripMenuItem.Text = "添加计划";
            this.添加计划ToolStripMenuItem.Click += new System.EventHandler(this.添加计划ToolStripMenuItem_Click);
            // 
            // 修改计划ToolStripMenuItem
            // 
            this.修改计划ToolStripMenuItem.Name = "修改计划ToolStripMenuItem";
            this.修改计划ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.修改计划ToolStripMenuItem.Text = "修改计划";
            // 
            // 添加服务ToolStripMenuItem
            // 
            this.添加服务ToolStripMenuItem.Name = "添加服务ToolStripMenuItem";
            this.添加服务ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.添加服务ToolStripMenuItem.Text = "添加服务";
            this.添加服务ToolStripMenuItem.Click += new System.EventHandler(this.添加服务ToolStripMenuItem_Click);
            // 
            // 服务列表ToolStripMenuItem
            // 
            this.服务列表ToolStripMenuItem.Name = "服务列表ToolStripMenuItem";
            this.服务列表ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.服务列表ToolStripMenuItem.Text = "服务列表";
            this.服务列表ToolStripMenuItem.Click += new System.EventHandler(this.服务列表ToolStripMenuItem_Click);
            // 
            // groupControl1
            // 
            this.groupControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl1.Controls.Add(this.gridControl1);
            this.groupControl1.Location = new System.Drawing.Point(0, 28);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(747, 278);
            this.groupControl1.TabIndex = 4;
            this.groupControl1.Text = "数据列表";
            // 
            // gridControl1
            // 
            this.gridControl1.DataSource = this.timerFormDataSet1BindingSource;
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(2, 21);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(743, 255);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // timerFormDataSet1BindingSource
            // 
            this.timerFormDataSet1BindingSource.DataMember = "TimerForm_DataSet1";
            this.timerFormDataSet1BindingSource.DataSource = this.dataSet1;
            // 
            // dataSet1
            // 
            this.dataSet1.DataSetName = "DataSet1";
            this.dataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colID,
            this.colStartTime,
            this.colEndTime,
            this.colRepeatTime,
            this.colRepeatInterval,
            this.colSID,
            this.colServiceName});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // colID
            // 
            this.colID.FieldName = "ID";
            this.colID.Name = "colID";
            // 
            // colStartTime
            // 
            this.colStartTime.FieldName = "StartTime";
            this.colStartTime.Name = "colStartTime";
            this.colStartTime.OptionsColumn.AllowEdit = false;
            this.colStartTime.Visible = true;
            this.colStartTime.VisibleIndex = 0;
            // 
            // colEndTime
            // 
            this.colEndTime.FieldName = "EndTime";
            this.colEndTime.Name = "colEndTime";
            this.colEndTime.OptionsColumn.AllowEdit = false;
            this.colEndTime.Visible = true;
            this.colEndTime.VisibleIndex = 1;
            // 
            // colRepeatTime
            // 
            this.colRepeatTime.FieldName = "RepeatTime";
            this.colRepeatTime.Name = "colRepeatTime";
            this.colRepeatTime.OptionsColumn.AllowEdit = false;
            this.colRepeatTime.Visible = true;
            this.colRepeatTime.VisibleIndex = 2;
            // 
            // colRepeatInterval
            // 
            this.colRepeatInterval.FieldName = "RepeatInterval";
            this.colRepeatInterval.Name = "colRepeatInterval";
            this.colRepeatInterval.OptionsColumn.AllowEdit = false;
            this.colRepeatInterval.Visible = true;
            this.colRepeatInterval.VisibleIndex = 3;
            // 
            // colSID
            // 
            this.colSID.FieldName = "SID";
            this.colSID.Name = "colSID";
            // 
            // colServiceName
            // 
            this.colServiceName.FieldName = "ServiceName";
            this.colServiceName.Name = "colServiceName";
            this.colServiceName.OptionsColumn.AllowEdit = false;
            this.colServiceName.Visible = true;
            this.colServiceName.VisibleIndex = 4;
            // 
            // TimerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(747, 304);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "TimerForm";
            this.Text = "TimerForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.TimerForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timerFormDataSet1BindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 刷新ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 添加计划ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 添加服务ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 服务列表ToolStripMenuItem;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.BindingSource timerFormDataSet1BindingSource;
        private DataSet1 dataSet1;
        private DevExpress.XtraGrid.Columns.GridColumn colID;
        private DevExpress.XtraGrid.Columns.GridColumn colStartTime;
        private DevExpress.XtraGrid.Columns.GridColumn colEndTime;
        private DevExpress.XtraGrid.Columns.GridColumn colRepeatTime;
        private DevExpress.XtraGrid.Columns.GridColumn colRepeatInterval;
        private DevExpress.XtraGrid.Columns.GridColumn colSID;
        private DevExpress.XtraGrid.Columns.GridColumn colServiceName;
        private System.Windows.Forms.ToolStripMenuItem 修改计划ToolStripMenuItem;
    }
}