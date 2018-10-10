namespace SCPrime
{
    partial class SCIndexDataFrm
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
            this.pnFull = new System.Windows.Forms.Panel();
            this.pnRight = new System.Windows.Forms.Panel();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.pnLeft = new System.Windows.Forms.Panel();
            this.gridData = new System.Windows.Forms.DataGridView();
            this.sCIndexDataBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.oIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.indexYearDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.indexMonthDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.indexValueDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.modifiedDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.createdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isDelete = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.pnFull.SuspendLayout();
            this.pnRight.SuspendLayout();
            this.pnLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sCIndexDataBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // pnFull
            // 
            this.pnFull.Controls.Add(this.pnRight);
            this.pnFull.Controls.Add(this.pnLeft);
            this.pnFull.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnFull.Location = new System.Drawing.Point(0, 0);
            this.pnFull.Name = "pnFull";
            this.pnFull.Size = new System.Drawing.Size(939, 473);
            this.pnFull.TabIndex = 0;
            // 
            // pnRight
            // 
            this.pnRight.Controls.Add(this.btnSave);
            this.pnRight.Controls.Add(this.btnDelete);
            this.pnRight.Controls.Add(this.btnNew);
            this.pnRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnRight.Location = new System.Drawing.Point(761, 0);
            this.pnRight.Name = "pnRight";
            this.pnRight.Size = new System.Drawing.Size(178, 473);
            this.pnRight.TabIndex = 1;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(52, 134);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(52, 82);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 1;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(52, 28);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(75, 23);
            this.btnNew.TabIndex = 0;
            this.btnNew.Text = "New";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // pnLeft
            // 
            this.pnLeft.Controls.Add(this.gridData);
            this.pnLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnLeft.Location = new System.Drawing.Point(0, 0);
            this.pnLeft.Name = "pnLeft";
            this.pnLeft.Size = new System.Drawing.Size(939, 473);
            this.pnLeft.TabIndex = 0;
            // 
            // gridData
            // 
            this.gridData.AllowUserToAddRows = false;
            this.gridData.AutoGenerateColumns = false;
            this.gridData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.oIDDataGridViewTextBoxColumn,
            this.indexYearDataGridViewTextBoxColumn,
            this.indexMonthDataGridViewTextBoxColumn,
            this.indexValueDataGridViewTextBoxColumn,
            this.modifiedDataGridViewTextBoxColumn,
            this.createdDataGridViewTextBoxColumn,
            this.isDelete});
            this.gridData.DataSource = this.sCIndexDataBindingSource;
            this.gridData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridData.Location = new System.Drawing.Point(0, 0);
            this.gridData.Name = "gridData";
            this.gridData.Size = new System.Drawing.Size(939, 473);
            this.gridData.TabIndex = 0;
            this.gridData.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridData_CellValidated);
            this.gridData.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.gridData_CellValidating);
            this.gridData.RowValidating += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.gridData_RowValidating);
            this.gridData.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gridData_KeyDown);
            // 
            // sCIndexDataBindingSource
            // 
            this.sCIndexDataBindingSource.DataSource = typeof(SCPrime.Model.SCIndexData);
            // 
            // oIDDataGridViewTextBoxColumn
            // 
            this.oIDDataGridViewTextBoxColumn.DataPropertyName = "OID";
            this.oIDDataGridViewTextBoxColumn.HeaderText = "OID";
            this.oIDDataGridViewTextBoxColumn.Name = "oIDDataGridViewTextBoxColumn";
            this.oIDDataGridViewTextBoxColumn.Visible = false;
            // 
            // indexYearDataGridViewTextBoxColumn
            // 
            this.indexYearDataGridViewTextBoxColumn.DataPropertyName = "IndexYear";
            this.indexYearDataGridViewTextBoxColumn.HeaderText = "Index Year";
            this.indexYearDataGridViewTextBoxColumn.Name = "indexYearDataGridViewTextBoxColumn";
            // 
            // indexMonthDataGridViewTextBoxColumn
            // 
            this.indexMonthDataGridViewTextBoxColumn.DataPropertyName = "IndexMonth";
            this.indexMonthDataGridViewTextBoxColumn.HeaderText = "Index Month";
            this.indexMonthDataGridViewTextBoxColumn.Name = "indexMonthDataGridViewTextBoxColumn";
            // 
            // indexValueDataGridViewTextBoxColumn
            // 
            this.indexValueDataGridViewTextBoxColumn.DataPropertyName = "IndexValue";
            this.indexValueDataGridViewTextBoxColumn.HeaderText = "Index Value";
            this.indexValueDataGridViewTextBoxColumn.Name = "indexValueDataGridViewTextBoxColumn";
            // 
            // modifiedDataGridViewTextBoxColumn
            // 
            this.modifiedDataGridViewTextBoxColumn.DataPropertyName = "Modified";
            this.modifiedDataGridViewTextBoxColumn.HeaderText = "Modified";
            this.modifiedDataGridViewTextBoxColumn.Name = "modifiedDataGridViewTextBoxColumn";
            this.modifiedDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // createdDataGridViewTextBoxColumn
            // 
            this.createdDataGridViewTextBoxColumn.DataPropertyName = "Created";
            this.createdDataGridViewTextBoxColumn.HeaderText = "Created";
            this.createdDataGridViewTextBoxColumn.Name = "createdDataGridViewTextBoxColumn";
            this.createdDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // isDelete
            // 
            this.isDelete.DataPropertyName = "isDelete";
            this.isDelete.HeaderText = "isDelete";
            this.isDelete.Name = "isDelete";
            this.isDelete.ReadOnly = true;
            this.isDelete.Visible = false;
            // 
            // SCIndexDataFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(939, 473);
            this.Controls.Add(this.pnFull);
            this.Name = "SCIndexDataFrm";
            this.Text = "IndexData Manager";
            this.Load += new System.EventHandler(this.SCIndexDataFrm_Load);
            this.pnFull.ResumeLayout(false);
            this.pnRight.ResumeLayout(false);
            this.pnLeft.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sCIndexDataBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnFull;
        private System.Windows.Forms.Panel pnLeft;
        private System.Windows.Forms.Panel pnRight;
        private System.Windows.Forms.DataGridView gridData;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.BindingSource sCIndexDataBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn oIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn indexYearDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn indexMonthDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn indexValueDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn modifiedDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn createdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isDelete;
    }
}