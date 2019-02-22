namespace XCYN.Winform.Quartz.Views
{
    partial class ServiceForm
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
            this.修改ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.serviceFormDataSet1BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataSet1 = new XCYN.Winform.Quartz.DataSet1();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colServiceName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAssemblyName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNameSpace = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colClassName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMethodName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsDelete = new DevExpress.XtraGrid.Columns.GridColumn();
            this.添加ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.serviceFormDataSet1BindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.添加ToolStripMenuItem,
            this.修改ToolStripMenuItem,
            this.删除ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(717, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 修改ToolStripMenuItem
            // 
            this.修改ToolStripMenuItem.Name = "修改ToolStripMenuItem";
            this.修改ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.修改ToolStripMenuItem.Text = "修改";
            // 
            // 删除ToolStripMenuItem
            // 
            this.删除ToolStripMenuItem.Name = "删除ToolStripMenuItem";
            this.删除ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.删除ToolStripMenuItem.Text = "删除";
            // 
            // groupControl1
            // 
            this.groupControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl1.Controls.Add(this.gridControl1);
            this.groupControl1.Location = new System.Drawing.Point(0, 28);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(717, 320);
            this.groupControl1.TabIndex = 1;
            this.groupControl1.Text = "列表展示";
            // 
            // gridControl1
            // 
            this.gridControl1.DataSource = this.serviceFormDataSet1BindingSource;
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(2, 21);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(713, 297);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // serviceFormDataSet1BindingSource
            // 
            this.serviceFormDataSet1BindingSource.DataMember = "ServiceForm_DataSet1";
            this.serviceFormDataSet1BindingSource.DataSource = this.dataSet1;
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
            this.colServiceName,
            this.colAssemblyName,
            this.colNameSpace,
            this.colClassName,
            this.colMethodName,
            this.colIsDelete});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // colID
            // 
            this.colID.FieldName = "ID";
            this.colID.Name = "colID";
            // 
            // colServiceName
            // 
            this.colServiceName.FieldName = "ServiceName";
            this.colServiceName.Name = "colServiceName";
            this.colServiceName.OptionsColumn.AllowEdit = false;
            this.colServiceName.Visible = true;
            this.colServiceName.VisibleIndex = 0;
            // 
            // colAssemblyName
            // 
            this.colAssemblyName.FieldName = "AssemblyName";
            this.colAssemblyName.Name = "colAssemblyName";
            this.colAssemblyName.OptionsColumn.AllowEdit = false;
            this.colAssemblyName.Visible = true;
            this.colAssemblyName.VisibleIndex = 1;
            // 
            // colNameSpace
            // 
            this.colNameSpace.FieldName = "NameSpace";
            this.colNameSpace.Name = "colNameSpace";
            this.colNameSpace.OptionsColumn.AllowEdit = false;
            this.colNameSpace.Visible = true;
            this.colNameSpace.VisibleIndex = 2;
            // 
            // colClassName
            // 
            this.colClassName.FieldName = "ClassName";
            this.colClassName.Name = "colClassName";
            this.colClassName.OptionsColumn.AllowEdit = false;
            this.colClassName.Visible = true;
            this.colClassName.VisibleIndex = 3;
            // 
            // colMethodName
            // 
            this.colMethodName.FieldName = "MethodName";
            this.colMethodName.Name = "colMethodName";
            this.colMethodName.OptionsColumn.AllowEdit = false;
            this.colMethodName.Visible = true;
            this.colMethodName.VisibleIndex = 4;
            // 
            // colIsDelete
            // 
            this.colIsDelete.FieldName = "IsDelete";
            this.colIsDelete.Name = "colIsDelete";
            // 
            // 添加ToolStripMenuItem
            // 
            this.添加ToolStripMenuItem.Name = "添加ToolStripMenuItem";
            this.添加ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.添加ToolStripMenuItem.Text = "添加";
            this.添加ToolStripMenuItem.Click += new System.EventHandler(this.添加ToolStripMenuItem_Click);
            // 
            // ServiceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(717, 345);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "ServiceForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "服务列表";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.ServiceForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.serviceFormDataSet1BindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 修改ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 删除ToolStripMenuItem;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.BindingSource serviceFormDataSet1BindingSource;
        private DataSet1 dataSet1;
        private DevExpress.XtraGrid.Columns.GridColumn colID;
        private DevExpress.XtraGrid.Columns.GridColumn colServiceName;
        private DevExpress.XtraGrid.Columns.GridColumn colAssemblyName;
        private DevExpress.XtraGrid.Columns.GridColumn colNameSpace;
        private DevExpress.XtraGrid.Columns.GridColumn colClassName;
        private DevExpress.XtraGrid.Columns.GridColumn colMethodName;
        private DevExpress.XtraGrid.Columns.GridColumn colIsDelete;
        private System.Windows.Forms.ToolStripMenuItem 添加ToolStripMenuItem;
    }
}