namespace SCPrime
{
    partial class SCSearchItemFrm
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
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.gridItem = new System.Windows.Forms.DataGridView();
            this.oIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.partNrDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.supplierDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.searchKeyDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.salesPrDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.purchasePrDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sCViewItemsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sCViewItemsBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(98, 17);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(399, 20);
            this.txtSearch.TabIndex = 1;
            this.txtSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSearch_KeyPress);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(665, 16);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(774, 16);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtSearch);
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(944, 66);
            this.panel1.TabIndex = 5;
            // 
            // gridItem
            // 
            this.gridItem.AutoGenerateColumns = false;
            this.gridItem.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridItem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridItem.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.oIDDataGridViewTextBoxColumn,
            this.partNrDataGridViewTextBoxColumn,
            this.nameDataGridViewTextBoxColumn,
            this.supplierDataGridViewTextBoxColumn,
            this.searchKeyDataGridViewTextBoxColumn,
            this.salesPrDataGridViewTextBoxColumn,
            this.purchasePrDataGridViewTextBoxColumn});
            this.gridItem.DataSource = this.sCViewItemsBindingSource;
            this.gridItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridItem.Location = new System.Drawing.Point(0, 66);
            this.gridItem.Name = "gridItem";
            this.gridItem.Size = new System.Drawing.Size(944, 426);
            this.gridItem.TabIndex = 6;
            // 
            // oIDDataGridViewTextBoxColumn
            // 
            this.oIDDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.oIDDataGridViewTextBoxColumn.DataPropertyName = "_OID";
            this.oIDDataGridViewTextBoxColumn.HeaderText = "_OID";
            this.oIDDataGridViewTextBoxColumn.Name = "oIDDataGridViewTextBoxColumn";
            this.oIDDataGridViewTextBoxColumn.Visible = false;
            this.oIDDataGridViewTextBoxColumn.Width = 57;
            // 
            // partNrDataGridViewTextBoxColumn
            // 
            this.partNrDataGridViewTextBoxColumn.DataPropertyName = "PartNr";
            this.partNrDataGridViewTextBoxColumn.HeaderText = "PartNr";
            this.partNrDataGridViewTextBoxColumn.Name = "partNrDataGridViewTextBoxColumn";
            // 
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            this.nameDataGridViewTextBoxColumn.HeaderText = "Name";
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            this.nameDataGridViewTextBoxColumn.Width = 60;
            // 
            // supplierDataGridViewTextBoxColumn
            // 
            this.supplierDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.supplierDataGridViewTextBoxColumn.DataPropertyName = "Supplier";
            this.supplierDataGridViewTextBoxColumn.HeaderText = "Supplier";
            this.supplierDataGridViewTextBoxColumn.Name = "supplierDataGridViewTextBoxColumn";
            this.supplierDataGridViewTextBoxColumn.Width = 70;
            // 
            // searchKeyDataGridViewTextBoxColumn
            // 
            this.searchKeyDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.searchKeyDataGridViewTextBoxColumn.DataPropertyName = "SearchKey";
            this.searchKeyDataGridViewTextBoxColumn.HeaderText = "SearchKey";
            this.searchKeyDataGridViewTextBoxColumn.Name = "searchKeyDataGridViewTextBoxColumn";
            this.searchKeyDataGridViewTextBoxColumn.Width = 84;
            // 
            // salesPrDataGridViewTextBoxColumn
            // 
            this.salesPrDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.salesPrDataGridViewTextBoxColumn.DataPropertyName = "SalesPr";
            this.salesPrDataGridViewTextBoxColumn.HeaderText = "SalesPr";
            this.salesPrDataGridViewTextBoxColumn.Name = "salesPrDataGridViewTextBoxColumn";
            this.salesPrDataGridViewTextBoxColumn.Width = 68;
            // 
            // purchasePrDataGridViewTextBoxColumn
            // 
            this.purchasePrDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.purchasePrDataGridViewTextBoxColumn.DataPropertyName = "PurchasePr";
            this.purchasePrDataGridViewTextBoxColumn.HeaderText = "PurchasePr";
            this.purchasePrDataGridViewTextBoxColumn.Name = "purchasePrDataGridViewTextBoxColumn";
            this.purchasePrDataGridViewTextBoxColumn.Width = 87;
            // 
            // sCViewItemsBindingSource
            // 
            this.sCViewItemsBindingSource.DataSource = typeof(SCPrime.Model.SCViewItems);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(39, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Filter";
            // 
            // SCSearchItemFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(944, 492);
            this.Controls.Add(this.gridItem);
            this.Controls.Add(this.panel1);
            this.Name = "SCSearchItemFrm";
            this.Text = "SCSearchItemFrm";
            this.Load += new System.EventHandler(this.SCSearchItemFrm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridItem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sCViewItemsBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.BindingSource sCViewItemsBindingSource;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView gridItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn oIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn partNrDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn supplierDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn searchKeyDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn salesPrDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn purchasePrDataGridViewTextBoxColumn;
        private System.Windows.Forms.Label label1;
    }
}