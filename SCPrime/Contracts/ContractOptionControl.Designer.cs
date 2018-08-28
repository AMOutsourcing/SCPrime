namespace SCPrime.Contracts
{
    partial class ContractOptionControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtTotalSale = new System.Windows.Forms.TextBox();
            this.txtTotalPurchase = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.oIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.partNrDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.partNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.labourCodeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.labourNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.baseSelPrDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.purchasePrDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.salePrDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.quantityDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.infoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PartialPayerCol = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.optionCategoryOIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.optionOIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.optionDetailOIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.createdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.modifiedDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.partialPayerDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.InvoiceFlagCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contractOIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contractOptionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.sCOptionBaseBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.contractOptionBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sCOptionBaseBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.treeView1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(257, 445);
            this.panel1.TabIndex = 0;
            // 
            // treeView1
            // 
            this.treeView1.CheckBoxes = true;
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.ShowNodeToolTips = true;
            this.treeView1.Size = new System.Drawing.Size(257, 445);
            this.treeView1.TabIndex = 0;
            this.treeView1.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterCheck);
            this.treeView1.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterExpand);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.txtTotalSale);
            this.panel2.Controls.Add(this.txtTotalPurchase);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(257, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(648, 36);
            this.panel2.TabIndex = 2;
            // 
            // txtTotalSale
            // 
            this.txtTotalSale.Location = new System.Drawing.Point(574, 6);
            this.txtTotalSale.Name = "txtTotalSale";
            this.txtTotalSale.ReadOnly = true;
            this.txtTotalSale.Size = new System.Drawing.Size(66, 20);
            this.txtTotalSale.TabIndex = 4;
            // 
            // txtTotalPurchase
            // 
            this.txtTotalPurchase.Location = new System.Drawing.Point(414, 6);
            this.txtTotalPurchase.Name = "txtTotalPurchase";
            this.txtTotalPurchase.ReadOnly = true;
            this.txtTotalPurchase.Size = new System.Drawing.Size(66, 20);
            this.txtTotalPurchase.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(486, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Sales price total";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(311, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Purchase price total";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Selected";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.oIDDataGridViewTextBoxColumn,
            this.nameDataGridViewTextBoxColumn,
            this.partNrDataGridViewTextBoxColumn,
            this.partNameDataGridViewTextBoxColumn,
            this.labourCodeDataGridViewTextBoxColumn,
            this.labourNameDataGridViewTextBoxColumn,
            this.baseSelPrDataGridViewTextBoxColumn,
            this.purchasePrDataGridViewTextBoxColumn,
            this.salePrDataGridViewTextBoxColumn,
            this.quantityDataGridViewTextBoxColumn,
            this.infoDataGridViewTextBoxColumn,
            this.PartialPayerCol,
            this.optionCategoryOIDDataGridViewTextBoxColumn,
            this.optionOIDDataGridViewTextBoxColumn,
            this.optionDetailOIDDataGridViewTextBoxColumn,
            this.createdDataGridViewTextBoxColumn,
            this.modifiedDataGridViewTextBoxColumn,
            this.partialPayerDataGridViewTextBoxColumn,
            this.InvoiceFlagCol,
            this.contractOIDDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.contractOptionBindingSource;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(257, 36);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(648, 409);
            this.dataGridView1.TabIndex = 3;
            this.dataGridView1.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dataGridView1_CellValidating);
            this.dataGridView1.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dataGridView1_DataBindingComplete);
            this.dataGridView1.RowValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_RowValidated);
            // 
            // oIDDataGridViewTextBoxColumn
            // 
            this.oIDDataGridViewTextBoxColumn.DataPropertyName = "OID";
            this.oIDDataGridViewTextBoxColumn.HeaderText = "OID";
            this.oIDDataGridViewTextBoxColumn.Name = "oIDDataGridViewTextBoxColumn";
            this.oIDDataGridViewTextBoxColumn.ReadOnly = true;
            this.oIDDataGridViewTextBoxColumn.Visible = false;
            // 
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            this.nameDataGridViewTextBoxColumn.HeaderText = "Name";
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            this.nameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // partNrDataGridViewTextBoxColumn
            // 
            this.partNrDataGridViewTextBoxColumn.DataPropertyName = "PartNr";
            this.partNrDataGridViewTextBoxColumn.HeaderText = "PartNr";
            this.partNrDataGridViewTextBoxColumn.Name = "partNrDataGridViewTextBoxColumn";
            this.partNrDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // partNameDataGridViewTextBoxColumn
            // 
            this.partNameDataGridViewTextBoxColumn.DataPropertyName = "PartName";
            this.partNameDataGridViewTextBoxColumn.HeaderText = "PartName";
            this.partNameDataGridViewTextBoxColumn.Name = "partNameDataGridViewTextBoxColumn";
            this.partNameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // labourCodeDataGridViewTextBoxColumn
            // 
            this.labourCodeDataGridViewTextBoxColumn.DataPropertyName = "LabourCode";
            this.labourCodeDataGridViewTextBoxColumn.HeaderText = "LabourCode";
            this.labourCodeDataGridViewTextBoxColumn.Name = "labourCodeDataGridViewTextBoxColumn";
            this.labourCodeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // labourNameDataGridViewTextBoxColumn
            // 
            this.labourNameDataGridViewTextBoxColumn.DataPropertyName = "LabourName";
            this.labourNameDataGridViewTextBoxColumn.HeaderText = "LabourName";
            this.labourNameDataGridViewTextBoxColumn.Name = "labourNameDataGridViewTextBoxColumn";
            this.labourNameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // baseSelPrDataGridViewTextBoxColumn
            // 
            this.baseSelPrDataGridViewTextBoxColumn.DataPropertyName = "BaseSelPr";
            this.baseSelPrDataGridViewTextBoxColumn.HeaderText = "BaseSelPr";
            this.baseSelPrDataGridViewTextBoxColumn.Name = "baseSelPrDataGridViewTextBoxColumn";
            this.baseSelPrDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // purchasePrDataGridViewTextBoxColumn
            // 
            this.purchasePrDataGridViewTextBoxColumn.DataPropertyName = "PurchasePr";
            this.purchasePrDataGridViewTextBoxColumn.HeaderText = "PurchasePr";
            this.purchasePrDataGridViewTextBoxColumn.Name = "purchasePrDataGridViewTextBoxColumn";
            this.purchasePrDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // salePrDataGridViewTextBoxColumn
            // 
            this.salePrDataGridViewTextBoxColumn.DataPropertyName = "SalePr";
            this.salePrDataGridViewTextBoxColumn.HeaderText = "SalePr";
            this.salePrDataGridViewTextBoxColumn.Name = "salePrDataGridViewTextBoxColumn";
            // 
            // quantityDataGridViewTextBoxColumn
            // 
            this.quantityDataGridViewTextBoxColumn.DataPropertyName = "Quantity";
            this.quantityDataGridViewTextBoxColumn.HeaderText = "Quantity";
            this.quantityDataGridViewTextBoxColumn.Name = "quantityDataGridViewTextBoxColumn";
            // 
            // infoDataGridViewTextBoxColumn
            // 
            this.infoDataGridViewTextBoxColumn.DataPropertyName = "Info";
            this.infoDataGridViewTextBoxColumn.HeaderText = "Info";
            this.infoDataGridViewTextBoxColumn.Name = "infoDataGridViewTextBoxColumn";
            // 
            // PartialPayerCol
            // 
            this.PartialPayerCol.HeaderText = "Partial payer";
            this.PartialPayerCol.Name = "PartialPayerCol";
            this.PartialPayerCol.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.PartialPayerCol.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // optionCategoryOIDDataGridViewTextBoxColumn
            // 
            this.optionCategoryOIDDataGridViewTextBoxColumn.DataPropertyName = "OptionCategoryOID";
            this.optionCategoryOIDDataGridViewTextBoxColumn.HeaderText = "OptionCategoryOID";
            this.optionCategoryOIDDataGridViewTextBoxColumn.Name = "optionCategoryOIDDataGridViewTextBoxColumn";
            this.optionCategoryOIDDataGridViewTextBoxColumn.ReadOnly = true;
            this.optionCategoryOIDDataGridViewTextBoxColumn.Visible = false;
            // 
            // optionOIDDataGridViewTextBoxColumn
            // 
            this.optionOIDDataGridViewTextBoxColumn.DataPropertyName = "OptionOID";
            this.optionOIDDataGridViewTextBoxColumn.HeaderText = "OptionOID";
            this.optionOIDDataGridViewTextBoxColumn.Name = "optionOIDDataGridViewTextBoxColumn";
            this.optionOIDDataGridViewTextBoxColumn.ReadOnly = true;
            this.optionOIDDataGridViewTextBoxColumn.Visible = false;
            // 
            // optionDetailOIDDataGridViewTextBoxColumn
            // 
            this.optionDetailOIDDataGridViewTextBoxColumn.DataPropertyName = "OptionDetailOID";
            this.optionDetailOIDDataGridViewTextBoxColumn.HeaderText = "OptionDetailOID";
            this.optionDetailOIDDataGridViewTextBoxColumn.Name = "optionDetailOIDDataGridViewTextBoxColumn";
            this.optionDetailOIDDataGridViewTextBoxColumn.ReadOnly = true;
            this.optionDetailOIDDataGridViewTextBoxColumn.Visible = false;
            // 
            // createdDataGridViewTextBoxColumn
            // 
            this.createdDataGridViewTextBoxColumn.DataPropertyName = "Created";
            this.createdDataGridViewTextBoxColumn.HeaderText = "Created";
            this.createdDataGridViewTextBoxColumn.Name = "createdDataGridViewTextBoxColumn";
            this.createdDataGridViewTextBoxColumn.ReadOnly = true;
            this.createdDataGridViewTextBoxColumn.Visible = false;
            // 
            // modifiedDataGridViewTextBoxColumn
            // 
            this.modifiedDataGridViewTextBoxColumn.DataPropertyName = "Modified";
            this.modifiedDataGridViewTextBoxColumn.HeaderText = "Modified";
            this.modifiedDataGridViewTextBoxColumn.Name = "modifiedDataGridViewTextBoxColumn";
            this.modifiedDataGridViewTextBoxColumn.ReadOnly = true;
            this.modifiedDataGridViewTextBoxColumn.Visible = false;
            // 
            // partialPayerDataGridViewTextBoxColumn
            // 
            this.partialPayerDataGridViewTextBoxColumn.DataPropertyName = "PartialPayer";
            this.partialPayerDataGridViewTextBoxColumn.HeaderText = "PartialPayer";
            this.partialPayerDataGridViewTextBoxColumn.Name = "partialPayerDataGridViewTextBoxColumn";
            this.partialPayerDataGridViewTextBoxColumn.ReadOnly = true;
            this.partialPayerDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.partialPayerDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.partialPayerDataGridViewTextBoxColumn.Visible = false;
            // 
            // InvoiceFlagCol
            // 
            this.InvoiceFlagCol.HeaderText = "Invoice flag";
            this.InvoiceFlagCol.Name = "InvoiceFlagCol";
            this.InvoiceFlagCol.ReadOnly = true;
            this.InvoiceFlagCol.Visible = false;
            // 
            // contractOIDDataGridViewTextBoxColumn
            // 
            this.contractOIDDataGridViewTextBoxColumn.DataPropertyName = "ContractOID";
            this.contractOIDDataGridViewTextBoxColumn.HeaderText = "ContractOID";
            this.contractOIDDataGridViewTextBoxColumn.Name = "contractOIDDataGridViewTextBoxColumn";
            this.contractOIDDataGridViewTextBoxColumn.ReadOnly = true;
            this.contractOIDDataGridViewTextBoxColumn.Visible = false;
            // 
            // contractOptionBindingSource
            // 
            this.contractOptionBindingSource.DataSource = typeof(SCPrime.Model.ContractOption);
            // 
            // sCOptionBaseBindingSource
            // 
            this.sCOptionBaseBindingSource.DataSource = typeof(SCPrime.Model.SCOptionBase);
            // 
            // ContractOptionControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "ContractOptionControl";
            this.Size = new System.Drawing.Size(905, 445);
            this.Load += new System.EventHandler(this.ContractOption_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.contractOptionBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sCOptionBaseBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.TreeView treeView1;
        public System.Windows.Forms.Panel panel2;
        public System.Windows.Forms.DataGridView dataGridView1;
        public System.Windows.Forms.TextBox txtTotalPurchase;
        public System.Windows.Forms.Label label3;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox txtTotalSale;
        private System.Windows.Forms.BindingSource sCOptionBaseBindingSource;
        private System.Windows.Forms.BindingSource contractOptionBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn oIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn partNrDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn partNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn labourCodeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn labourNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn baseSelPrDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn purchasePrDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn salePrDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn quantityDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn infoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn PartialPayerCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn optionCategoryOIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn optionOIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn optionDetailOIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn createdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn modifiedDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn partialPayerDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn InvoiceFlagCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn contractOIDDataGridViewTextBoxColumn;
    }
}
