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
            this.OID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ContractTypeName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isInvoice = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.isActive = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.isCollective = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.isMarkDeleted = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.sCContractTypeBindingSource = new System.Windows.Forms.BindingSource(this.components);
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
            this.contractTypeList.AllowUserToAddRows = false;
            this.contractTypeList.AutoGenerateColumns = false;
            this.contractTypeList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.contractTypeList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.contractTypeList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.OID,
            this.ContractTypeName,
            this.isInvoice,
            this.isActive,
            this.isCollective,
            this.isMarkDeleted});
            this.contractTypeList.DataSource = this.sCContractTypeBindingSource;
            this.contractTypeList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contractTypeList.Location = new System.Drawing.Point(0, 0);
            this.contractTypeList.MultiSelect = false;
            this.contractTypeList.Name = "contractTypeList";
            this.contractTypeList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.contractTypeList.Size = new System.Drawing.Size(669, 321);
            this.contractTypeList.TabIndex = 0;
            this.contractTypeList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.contractTypeList_CellClick);
            this.contractTypeList.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.contractTypeList_CellEndEdit);
            this.contractTypeList.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.contractTypeList_RowEnter);
            this.contractTypeList.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.contractTypeList_RowHeaderMouseClick);
            this.contractTypeList.RowLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.contractTypeList_RowLeave);
            // 
            // OID
            // 
            this.OID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.OID.DataPropertyName = "OID";
            this.OID.FillWeight = 171.4286F;
            this.OID.HeaderText = "OID";
            this.OID.Name = "OID";
            this.OID.ReadOnly = true;
            this.OID.Visible = false;
            this.OID.Width = 51;
            // 
            // ContractTypeName
            // 
            this.ContractTypeName.DataPropertyName = "Name";
            this.ContractTypeName.HeaderText = "Name";
            this.ContractTypeName.Name = "ContractTypeName";
            // 
            // isInvoice
            // 
            this.isInvoice.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.isInvoice.DataPropertyName = "isInvoice";
            this.isInvoice.FillWeight = 28.57143F;
            this.isInvoice.HeaderText = "Invoice";
            this.isInvoice.Name = "isInvoice";
            this.isInvoice.Width = 48;
            // 
            // isActive
            // 
            this.isActive.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.isActive.DataPropertyName = "isActive";
            this.isActive.HeaderText = "Active";
            this.isActive.Name = "isActive";
            this.isActive.Width = 43;
            // 
            // isCollective
            // 
            this.isCollective.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.isCollective.DataPropertyName = "isCollective";
            this.isCollective.HeaderText = "Collective";
            this.isCollective.Name = "isCollective";
            this.isCollective.Width = 59;
            // 
            // isMarkDeleted
            // 
            this.isMarkDeleted.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.isMarkDeleted.DataPropertyName = "isMarkDeleted";
            this.isMarkDeleted.HeaderText = "isMarkDeleted";
            this.isMarkDeleted.Name = "isMarkDeleted";
            this.isMarkDeleted.Visible = false;
            this.isMarkDeleted.Width = 81;
            // 
            // sCContractTypeBindingSource
            // 
            this.sCContractTypeBindingSource.DataSource = typeof(SCPrime.Model.SCContractType);
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
        private System.Windows.Forms.DataGridViewTextBoxColumn ContractName;
        private System.Windows.Forms.DataGridViewTextBoxColumn OID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ContractTypeName;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isInvoice;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isActive;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isCollective;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isMarkDeleted;
    }
}