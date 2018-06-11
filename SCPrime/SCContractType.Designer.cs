namespace SCPrime
{
    partial class Form1
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
            this.fileSystemWatcher1 = new System.IO.FileSystemWatcher();
            this.newBtn = new System.Windows.Forms.Button();
            this.delBtn = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.saveBtn = new System.Windows.Forms.Button();
            this.closeBtn = new System.Windows.Forms.Button();
            this.contractTypeList = new System.Windows.Forms.DataGridView();
            this.sCContractTypeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.oIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isInvoiceDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.isActiveDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.isCollectiveDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.isMarkDeletedDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.contractTypeList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sCContractTypeBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // pbOK
            // 
            this.pbOK.Location = new System.Drawing.Point(703, 12);
            // 
            // pbCancel
            // 
            this.pbCancel.Location = new System.Drawing.Point(703, 46);
            // 
            // pbHelp
            // 
            this.pbHelp.Location = new System.Drawing.Point(703, 80);
            // 
            // fileSystemWatcher1
            // 
            this.fileSystemWatcher1.EnableRaisingEvents = true;
            this.fileSystemWatcher1.SynchronizingObject = this;
            // 
            // newBtn
            // 
            this.newBtn.Location = new System.Drawing.Point(14, 155);
            this.newBtn.Name = "newBtn";
            this.newBtn.Size = new System.Drawing.Size(103, 29);
            this.newBtn.TabIndex = 5;
            this.newBtn.Text = "New";
            this.newBtn.UseVisualStyleBackColor = true;
            this.newBtn.Click += new System.EventHandler(this.newBtn_Click);
            // 
            // delBtn
            // 
            this.delBtn.Location = new System.Drawing.Point(14, 190);
            this.delBtn.Name = "delBtn";
            this.delBtn.Size = new System.Drawing.Size(103, 29);
            this.delBtn.TabIndex = 6;
            this.delBtn.Text = "Delete";
            this.delBtn.UseVisualStyleBackColor = true;
            this.delBtn.Click += new System.EventHandler(this.delBtn_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.saveBtn);
            this.panel1.Controls.Add(this.closeBtn);
            this.panel1.Controls.Add(this.newBtn);
            this.panel1.Controls.Add(this.delBtn);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(669, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(131, 321);
            this.panel1.TabIndex = 7;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // saveBtn
            // 
            this.saveBtn.Location = new System.Drawing.Point(14, 33);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(103, 29);
            this.saveBtn.TabIndex = 7;
            this.saveBtn.Text = "Save";
            this.saveBtn.UseVisualStyleBackColor = true;
            this.saveBtn.Click += new System.EventHandler(this.saveBtn_Click);
            // 
            // closeBtn
            // 
            this.closeBtn.Location = new System.Drawing.Point(14, 68);
            this.closeBtn.Name = "closeBtn";
            this.closeBtn.Size = new System.Drawing.Size(103, 29);
            this.closeBtn.TabIndex = 8;
            this.closeBtn.Text = "Close";
            this.closeBtn.UseVisualStyleBackColor = true;
            this.closeBtn.Click += new System.EventHandler(this.closeBtn_Click);
            // 
            // contractTypeList
            // 
            this.contractTypeList.AutoGenerateColumns = false;
            this.contractTypeList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.contractTypeList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.contractTypeList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.oIDDataGridViewTextBoxColumn,
            this.nameDataGridViewTextBoxColumn,
            this.isInvoiceDataGridViewCheckBoxColumn,
            this.isActiveDataGridViewCheckBoxColumn,
            this.isCollectiveDataGridViewCheckBoxColumn,
            this.isMarkDeletedDataGridViewCheckBoxColumn});
            this.contractTypeList.DataSource = this.sCContractTypeBindingSource;
            this.contractTypeList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contractTypeList.Location = new System.Drawing.Point(0, 0);
            this.contractTypeList.Name = "contractTypeList";
            this.contractTypeList.Size = new System.Drawing.Size(669, 321);
            this.contractTypeList.TabIndex = 0;
            this.contractTypeList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.contractTypeList_CellClick);
            this.contractTypeList.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.contractTypeList_CellEndEdit);
            // 
            // sCContractTypeBindingSource
            // 
            this.sCContractTypeBindingSource.DataSource = typeof(SCPrime.Model.SCContractType);
            // 
            // oIDDataGridViewTextBoxColumn
            // 
            this.oIDDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.oIDDataGridViewTextBoxColumn.DataPropertyName = "OID";
            this.oIDDataGridViewTextBoxColumn.FillWeight = 171.4286F;
            this.oIDDataGridViewTextBoxColumn.HeaderText = "OID";
            this.oIDDataGridViewTextBoxColumn.Name = "oIDDataGridViewTextBoxColumn";
            this.oIDDataGridViewTextBoxColumn.ReadOnly = true;
            this.oIDDataGridViewTextBoxColumn.Width = 51;
            // 
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            this.nameDataGridViewTextBoxColumn.HeaderText = "Name";
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            // 
            // isInvoiceDataGridViewCheckBoxColumn
            // 
            this.isInvoiceDataGridViewCheckBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.isInvoiceDataGridViewCheckBoxColumn.DataPropertyName = "isInvoice";
            this.isInvoiceDataGridViewCheckBoxColumn.FillWeight = 28.57143F;
            this.isInvoiceDataGridViewCheckBoxColumn.HeaderText = "Invoice";
            this.isInvoiceDataGridViewCheckBoxColumn.Name = "isInvoiceDataGridViewCheckBoxColumn";
            this.isInvoiceDataGridViewCheckBoxColumn.Width = 48;
            // 
            // isActiveDataGridViewCheckBoxColumn
            // 
            this.isActiveDataGridViewCheckBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.isActiveDataGridViewCheckBoxColumn.DataPropertyName = "isActive";
            this.isActiveDataGridViewCheckBoxColumn.HeaderText = "Active";
            this.isActiveDataGridViewCheckBoxColumn.Name = "isActiveDataGridViewCheckBoxColumn";
            this.isActiveDataGridViewCheckBoxColumn.Width = 43;
            // 
            // isCollectiveDataGridViewCheckBoxColumn
            // 
            this.isCollectiveDataGridViewCheckBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.isCollectiveDataGridViewCheckBoxColumn.DataPropertyName = "isCollective";
            this.isCollectiveDataGridViewCheckBoxColumn.HeaderText = "Collective";
            this.isCollectiveDataGridViewCheckBoxColumn.Name = "isCollectiveDataGridViewCheckBoxColumn";
            this.isCollectiveDataGridViewCheckBoxColumn.Width = 59;
            // 
            // isMarkDeletedDataGridViewCheckBoxColumn
            // 
            this.isMarkDeletedDataGridViewCheckBoxColumn.DataPropertyName = "isMarkDeleted";
            this.isMarkDeletedDataGridViewCheckBoxColumn.HeaderText = "isMarkDeleted";
            this.isMarkDeletedDataGridViewCheckBoxColumn.Name = "isMarkDeletedDataGridViewCheckBoxColumn";
            this.isMarkDeletedDataGridViewCheckBoxColumn.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 321);
            this.Controls.Add(this.contractTypeList);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "SCContractType";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Controls.SetChildIndex(this.pbOK, 0);
            this.Controls.SetChildIndex(this.pbCancel, 0);
            this.Controls.SetChildIndex(this.pbHelp, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.contractTypeList, 0);
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.contractTypeList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sCContractTypeBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.IO.FileSystemWatcher fileSystemWatcher1;
        private System.Windows.Forms.BindingSource sCContractTypeBindingSource;
        private System.Windows.Forms.Button newBtn;
        private System.Windows.Forms.Button delBtn;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button saveBtn;
        private System.Windows.Forms.Button closeBtn;
        private System.Windows.Forms.DataGridView contractTypeList;
        private System.Windows.Forms.DataGridViewTextBoxColumn oIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isInvoiceDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isActiveDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isCollectiveDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isMarkDeletedDataGridViewCheckBoxColumn;
    }
}