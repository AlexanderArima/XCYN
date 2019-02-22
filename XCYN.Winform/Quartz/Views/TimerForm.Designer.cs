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
            this.执行ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.停止ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.计划任务ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.添加ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.修改ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.服务ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.列表显示ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.添加ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
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
            this.colNextExecuteTime = new DevExpress.XtraGrid.Columns.GridColumn();
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
            this.执行ToolStripMenuItem,
            this.停止ToolStripMenuItem,
            this.计划任务ToolStripMenuItem,
            this.服务ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(585, 25);
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
            // 执行ToolStripMenuItem
            // 
            this.执行ToolStripMenuItem.Name = "执行ToolStripMenuItem";
            this.执行ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.执行ToolStripMenuItem.Text = "启动";
            this.执行ToolStripMenuItem.Click += new System.EventHandler(this.执行ToolStripMenuItem_Click);
            // 
            // 停止ToolStripMenuItem
            // 
            this.停止ToolStripMenuItem.Name = "停止ToolStripMenuItem";
            this.停止ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.停止ToolStripMenuItem.Text = "停止";
            // 
            // 计划任务ToolStripMenuItem
            // 
            this.计划任务ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.添加ToolStripMenuItem,
            this.修改ToolStripMenuItem});
            this.计划任务ToolStripMenuItem.Name = "计划任务ToolStripMenuItem";
            this.计划任务ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.计划任务ToolStripMenuItem.Text = "计划任务";
            // 
            // 添加ToolStripMenuItem
            // 
            this.添加ToolStripMenuItem.Name = "添加ToolStripMenuItem";
            this.添加ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.添加ToolStripMenuItem.Text = "添加";
            this.添加ToolStripMenuItem.Click += new System.EventHandler(this.添加ToolStripMenuItem_Click);
            // 
            // 修改ToolStripMenuItem
            // 
            this.修改ToolStripMenuItem.Name = "修改ToolStripMenuItem";
            this.修改ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.修改ToolStripMenuItem.Text = "修改";
            this.修改ToolStripMenuItem.Click += new System.EventHandler(this.修改ToolStripMenuItem_Click);
            // 
            // 服务ToolStripMenuItem
            // 
            this.服务ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.列表显示ToolStripMenuItem,
            this.添加ToolStripMenuItem1});
            this.服务ToolStripMenuItem.Name = "服务ToolStripMenuItem";
            this.服务ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.服务ToolStripMenuItem.Text = "后台服务";
            // 
            // 列表显示ToolStripMenuItem
            // 
            this.列表显示ToolStripMenuItem.Name = "列表显示ToolStripMenuItem";
            this.列表显示ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.列表显示ToolStripMenuItem.Text = "列表";
            this.列表显示ToolStripMenuItem.Click += new System.EventHandler(this.列表显示ToolStripMenuItem_Click);
            // 
            // 添加ToolStripMenuItem1
            // 
            this.添加ToolStripMenuItem1.Name = "添加ToolStripMenuItem1";
            this.添加ToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.添加ToolStripMenuItem1.Text = "添加";
            this.添加ToolStripMenuItem1.Click += new System.EventHandler(this.添加ToolStripMenuItem1_Click);
            // 
            // groupControl1
            // 
            this.groupControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl1.Controls.Add(this.gridControl1);
            this.groupControl1.Location = new System.Drawing.Point(0, 28);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(585, 223);
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
            this.gridControl1.Size = new System.Drawing.Size(581, 200);
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
            this.colServiceName,
            this.colNextExecuteTime});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.MultiSelect = true;
            this.gridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // colID
            // 
            this.colID.FieldName = "ID";
            this.colID.Name = "colID";
            // 
            // colStartTime
            // 
            this.colStartTime.DisplayFormat.FormatString = "yyyy-MM-dd HH:mm:ss";
            this.colStartTime.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colStartTime.FieldName = "StartTime";
            this.colStartTime.Name = "colStartTime";
            this.colStartTime.OptionsColumn.AllowEdit = false;
            this.colStartTime.Visible = true;
            this.colStartTime.VisibleIndex = 1;
            // 
            // colEndTime
            // 
            this.colEndTime.DisplayFormat.FormatString = "yyyy-MM-dd HH:mm:ss";
            this.colEndTime.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colEndTime.FieldName = "EndTime";
            this.colEndTime.Name = "colEndTime";
            this.colEndTime.OptionsColumn.AllowEdit = false;
            this.colEndTime.Visible = true;
            this.colEndTime.VisibleIndex = 2;
            // 
            // colRepeatTime
            // 
            this.colRepeatTime.FieldName = "RepeatTime";
            this.colRepeatTime.Name = "colRepeatTime";
            this.colRepeatTime.OptionsColumn.AllowEdit = false;
            this.colRepeatTime.Visible = true;
            this.colRepeatTime.VisibleIndex = 3;
            // 
            // colRepeatInterval
            // 
            this.colRepeatInterval.FieldName = "RepeatInterval";
            this.colRepeatInterval.Name = "colRepeatInterval";
            this.colRepeatInterval.OptionsColumn.AllowEdit = false;
            this.colRepeatInterval.Visible = true;
            this.colRepeatInterval.VisibleIndex = 4;
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
            this.colServiceName.VisibleIndex = 5;
            // 
            // colNextExecuteTime
            // 
            this.colNextExecuteTime.Caption = "下次执行时间";
            this.colNextExecuteTime.FieldName = "NextExecuteTime";
            this.colNextExecuteTime.Name = "colNextExecuteTime";
            this.colNextExecuteTime.OptionsColumn.AllowEdit = false;
            this.colNextExecuteTime.Visible = true;
            this.colNextExecuteTime.VisibleIndex = 6;
            // 
            // TimerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(585, 355);
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
        private System.Windows.Forms.ToolStripMenuItem 计划任务ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 添加ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 修改ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 服务ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 列表显示ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 添加ToolStripMenuItem1;
        private DevExpress.XtraGrid.Columns.GridColumn colNextExecuteTime;
        private System.Windows.Forms.ToolStripMenuItem 执行ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 停止ToolStripMenuItem;

    }
}