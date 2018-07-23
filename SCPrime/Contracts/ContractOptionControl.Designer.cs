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
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.sCOptionBaseBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.oidCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PartNrCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.itemSuplNoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PartNameCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LabourCodeCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LabourNameCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BaseSalesPriceCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PurchasePriceCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SalesPriceCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QuantityCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InfoCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PartialPayerCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InvoiceFlagCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
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
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.textBox2);
            this.panel2.Controls.Add(this.textBox1);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(257, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(648, 36);
            this.panel2.TabIndex = 2;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(574, 6);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(66, 20);
            this.textBox2.TabIndex = 4;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(414, 6);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(66, 20);
            this.textBox1.TabIndex = 3;
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
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.oidCol,
            this.nameCol,
            this.PartNrCol,
            this.itemSuplNoDataGridViewTextBoxColumn,
            this.PartNameCol,
            this.LabourCodeCol,
            this.LabourNameCol,
            this.BaseSalesPriceCol,
            this.PurchasePriceCol,
            this.SalesPriceCol,
            this.QuantityCol,
            this.InfoCol,
            this.PartialPayerCol,
            this.InvoiceFlagCol});
            this.dataGridView1.DataSource = this.sCOptionBaseBindingSource;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(257, 36);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(648, 409);
            this.dataGridView1.TabIndex = 3;
            // 
            // sCOptionBaseBindingSource
            // 
            this.sCOptionBaseBindingSource.DataSource = typeof(SCPrime.Model.SCOptionBase);
            // 
            // oidCol
            // 
            this.oidCol.DataPropertyName = "OID";
            this.oidCol.HeaderText = "OID";
            this.oidCol.Name = "oidCol";
            // 
            // nameCol
            // 
            this.nameCol.DataPropertyName = "Name";
            this.nameCol.HeaderText = "Name";
            this.nameCol.Name = "nameCol";
            // 
            // PartNrCol
            // 
            this.PartNrCol.DataPropertyName = "ItemNo";
            this.PartNrCol.HeaderText = "Part Nr";
            this.PartNrCol.Name = "PartNrCol";
            // 
            // itemSuplNoDataGridViewTextBoxColumn
            // 
            this.itemSuplNoDataGridViewTextBoxColumn.DataPropertyName = "ItemSuplNo";
            this.itemSuplNoDataGridViewTextBoxColumn.HeaderText = "ItemSuplNo";
            this.itemSuplNoDataGridViewTextBoxColumn.Name = "itemSuplNoDataGridViewTextBoxColumn";
            // 
            // PartNameCol
            // 
            this.PartNameCol.DataPropertyName = "ItemName";
            this.PartNameCol.HeaderText = "Part name";
            this.PartNameCol.Name = "PartNameCol";
            // 
            // LabourCodeCol
            // 
            this.LabourCodeCol.DataPropertyName = "WrksId";
            this.LabourCodeCol.HeaderText = "Labour code";
            this.LabourCodeCol.Name = "LabourCodeCol";
            // 
            // LabourNameCol
            // 
            this.LabourNameCol.DataPropertyName = "WrksName";
            this.LabourNameCol.HeaderText = "Labour name";
            this.LabourNameCol.Name = "LabourNameCol";
            // 
            // BaseSalesPriceCol
            // 
            this.BaseSalesPriceCol.DataPropertyName = "BaseSelPr";
            this.BaseSalesPriceCol.HeaderText = "Base sales price";
            this.BaseSalesPriceCol.Name = "BaseSalesPriceCol";
            // 
            // PurchasePriceCol
            // 
            this.PurchasePriceCol.DataPropertyName = "BuyPr";
            this.PurchasePriceCol.HeaderText = "Purchase price";
            this.PurchasePriceCol.Name = "PurchasePriceCol";
            // 
            // SalesPriceCol
            // 
            this.SalesPriceCol.DataPropertyName = "SelPr";
            this.SalesPriceCol.HeaderText = "Sales price";
            this.SalesPriceCol.Name = "SalesPriceCol";
            // 
            // QuantityCol
            // 
            this.QuantityCol.DataPropertyName = "Quantity";
            this.QuantityCol.HeaderText = "Quantity";
            this.QuantityCol.Name = "QuantityCol";
            // 
            // InfoCol
            // 
            this.InfoCol.DataPropertyName = "Info";
            this.InfoCol.HeaderText = "Info";
            this.InfoCol.Name = "InfoCol";
            // 
            // PartialPayerCol
            // 
            this.PartialPayerCol.HeaderText = "Partial payer";
            this.PartialPayerCol.Name = "PartialPayerCol";
            // 
            // InvoiceFlagCol
            // 
            this.InvoiceFlagCol.HeaderText = "Invoice flag";
            this.InvoiceFlagCol.Name = "InvoiceFlagCol";
            // 
            // ContractOption
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "ContractOption";
            this.Size = new System.Drawing.Size(905, 445);
            this.Load += new System.EventHandler(this.ContractOption_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sCOptionBaseBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.TreeView treeView1;
        public System.Windows.Forms.Panel panel2;
        public System.Windows.Forms.DataGridView dataGridView1;
        public System.Windows.Forms.TextBox textBox1;
        public System.Windows.Forms.Label label3;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.BindingSource sCOptionBaseBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn oidCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn PartNrCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn itemSuplNoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn PartNameCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn LabourCodeCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn LabourNameCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn BaseSalesPriceCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn PurchasePriceCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn SalesPriceCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn QuantityCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn InfoCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn PartialPayerCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn InvoiceFlagCol;
    }
}
