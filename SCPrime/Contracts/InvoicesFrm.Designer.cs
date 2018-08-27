namespace SCPrime.Contracts
{
    partial class InvoicesFrm
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
            this.pnSearch = new System.Windows.Forms.Panel();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker3 = new System.ComponentModel.BackgroundWorker();
            this.pnTop = new System.Windows.Forms.Panel();
            this.pnBottom = new System.Windows.Forms.Panel();
            this.gridInvoice = new System.Windows.Forms.DataGridView();
            this.pnTopLine = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.gridLines = new System.Windows.Forms.DataGridView();
            this.backgroundWorker4 = new System.ComponentModel.BackgroundWorker();
            this.oIDDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iTEMDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nAMEDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vATCDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nUMDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rSUMDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.invoiceLineBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.oIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sRECNODataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bILLDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dELDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pAIDDATEDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rSUMDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cRERECNODataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cUSTNODataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cUSTNAMEDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.unitIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.invoiceBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.panel1.SuspendLayout();
            this.pnSearch.SuspendLayout();
            this.pnTop.SuspendLayout();
            this.pnBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridInvoice)).BeginInit();
            this.pnTopLine.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridLines)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.invoiceLineBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.invoiceBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pnBottom);
            this.panel1.Controls.Add(this.pnTop);
            this.panel1.Location = new System.Drawing.Point(4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(915, 689);
            this.panel1.TabIndex = 0;
            // 
            // pnSearch
            // 
            this.pnSearch.Controls.Add(this.button5);
            this.pnSearch.Controls.Add(this.button4);
            this.pnSearch.Controls.Add(this.button3);
            this.pnSearch.Controls.Add(this.button1);
            this.pnSearch.Controls.Add(this.checkBox3);
            this.pnSearch.Controls.Add(this.checkBox2);
            this.pnSearch.Controls.Add(this.checkBox1);
            this.pnSearch.Controls.Add(this.label1);
            this.pnSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnSearch.Location = new System.Drawing.Point(0, 0);
            this.pnSearch.Name = "pnSearch";
            this.pnSearch.Size = new System.Drawing.Size(915, 61);
            this.pnSearch.TabIndex = 0;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(785, 16);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(117, 23);
            this.button5.TabIndex = 7;
            this.button5.Text = "Open invoice";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(643, 15);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(136, 23);
            this.button4.TabIndex = 6;
            this.button4.Text = "View next invoice draft";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(522, 16);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(115, 23);
            this.button3.TabIndex = 5;
            this.button3.Text = "Create new invoice";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(398, 16);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(118, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Credit selected";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new System.Drawing.Point(270, 17);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(78, 17);
            this.checkBox3.TabIndex = 3;
            this.checkBox3.Text = "Km invoice";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(168, 19);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(96, 17);
            this.checkBox2.TabIndex = 2;
            this.checkBox2.Text = "Normal invoice";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(72, 21);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(90, 17);
            this.checkBox1.TabIndex = 1;
            this.checkBox1.Text = "Credit invoice";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Invoices";
            // 
            // pnTop
            // 
            this.pnTop.Controls.Add(this.gridInvoice);
            this.pnTop.Controls.Add(this.pnSearch);
            this.pnTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnTop.Location = new System.Drawing.Point(0, 0);
            this.pnTop.Name = "pnTop";
            this.pnTop.Size = new System.Drawing.Size(915, 458);
            this.pnTop.TabIndex = 1;
            // 
            // pnBottom
            // 
            this.pnBottom.Controls.Add(this.gridLines);
            this.pnBottom.Controls.Add(this.pnTopLine);
            this.pnBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnBottom.Location = new System.Drawing.Point(0, 464);
            this.pnBottom.Name = "pnBottom";
            this.pnBottom.Size = new System.Drawing.Size(915, 225);
            this.pnBottom.TabIndex = 4;
            // 
            // gridInvoice
            // 
            this.gridInvoice.AutoGenerateColumns = false;
            this.gridInvoice.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridInvoice.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.oIDDataGridViewTextBoxColumn,
            this.sRECNODataGridViewTextBoxColumn,
            this.bILLDDataGridViewTextBoxColumn,
            this.dELDDataGridViewTextBoxColumn,
            this.pAIDDATEDataGridViewTextBoxColumn,
            this.rSUMDataGridViewTextBoxColumn,
            this.cRERECNODataGridViewTextBoxColumn,
            this.cUSTNODataGridViewTextBoxColumn,
            this.cUSTNAMEDataGridViewTextBoxColumn,
            this.unitIdDataGridViewTextBoxColumn});
            this.gridInvoice.DataSource = this.invoiceBindingSource;
            this.gridInvoice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridInvoice.Location = new System.Drawing.Point(0, 61);
            this.gridInvoice.Name = "gridInvoice";
            this.gridInvoice.Size = new System.Drawing.Size(915, 397);
            this.gridInvoice.TabIndex = 1;
            this.gridInvoice.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridInvoice_CellClick);
            // 
            // pnTopLine
            // 
            this.pnTopLine.Controls.Add(this.label2);
            this.pnTopLine.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnTopLine.Location = new System.Drawing.Point(0, 0);
            this.pnTopLine.Name = "pnTopLine";
            this.pnTopLine.Size = new System.Drawing.Size(915, 30);
            this.pnTopLine.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Invoice lines";
            // 
            // gridLines
            // 
            this.gridLines.AllowUserToAddRows = false;
            this.gridLines.AllowUserToDeleteRows = false;
            this.gridLines.AutoGenerateColumns = false;
            this.gridLines.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridLines.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.oIDDataGridViewTextBoxColumn1,
            this.iTEMDataGridViewTextBoxColumn,
            this.nAMEDataGridViewTextBoxColumn,
            this.vATCDDataGridViewTextBoxColumn,
            this.nUMDataGridViewTextBoxColumn,
            this.rSUMDataGridViewTextBoxColumn1});
            this.gridLines.DataSource = this.invoiceLineBindingSource;
            this.gridLines.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridLines.Location = new System.Drawing.Point(0, 30);
            this.gridLines.Name = "gridLines";
            this.gridLines.ReadOnly = true;
            this.gridLines.Size = new System.Drawing.Size(915, 195);
            this.gridLines.TabIndex = 1;
            // 
            // oIDDataGridViewTextBoxColumn1
            // 
            this.oIDDataGridViewTextBoxColumn1.DataPropertyName = "OID";
            this.oIDDataGridViewTextBoxColumn1.HeaderText = "OID";
            this.oIDDataGridViewTextBoxColumn1.Name = "oIDDataGridViewTextBoxColumn1";
            this.oIDDataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // iTEMDataGridViewTextBoxColumn
            // 
            this.iTEMDataGridViewTextBoxColumn.DataPropertyName = "ITEM";
            this.iTEMDataGridViewTextBoxColumn.HeaderText = "ITEM";
            this.iTEMDataGridViewTextBoxColumn.Name = "iTEMDataGridViewTextBoxColumn";
            this.iTEMDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // nAMEDataGridViewTextBoxColumn
            // 
            this.nAMEDataGridViewTextBoxColumn.DataPropertyName = "NAME";
            this.nAMEDataGridViewTextBoxColumn.HeaderText = "NAME";
            this.nAMEDataGridViewTextBoxColumn.Name = "nAMEDataGridViewTextBoxColumn";
            this.nAMEDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // vATCDDataGridViewTextBoxColumn
            // 
            this.vATCDDataGridViewTextBoxColumn.DataPropertyName = "VATCD";
            this.vATCDDataGridViewTextBoxColumn.HeaderText = "VATCD";
            this.vATCDDataGridViewTextBoxColumn.Name = "vATCDDataGridViewTextBoxColumn";
            this.vATCDDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // nUMDataGridViewTextBoxColumn
            // 
            this.nUMDataGridViewTextBoxColumn.DataPropertyName = "NUM";
            this.nUMDataGridViewTextBoxColumn.HeaderText = "NUM";
            this.nUMDataGridViewTextBoxColumn.Name = "nUMDataGridViewTextBoxColumn";
            this.nUMDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // rSUMDataGridViewTextBoxColumn1
            // 
            this.rSUMDataGridViewTextBoxColumn1.DataPropertyName = "RSUM";
            this.rSUMDataGridViewTextBoxColumn1.HeaderText = "RSUM";
            this.rSUMDataGridViewTextBoxColumn1.Name = "rSUMDataGridViewTextBoxColumn1";
            this.rSUMDataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // invoiceLineBindingSource
            // 
            this.invoiceLineBindingSource.DataSource = typeof(SCPrime.Model.InvoiceLine);
            // 
            // oIDDataGridViewTextBoxColumn
            // 
            this.oIDDataGridViewTextBoxColumn.DataPropertyName = "OID";
            this.oIDDataGridViewTextBoxColumn.HeaderText = "OID";
            this.oIDDataGridViewTextBoxColumn.Name = "oIDDataGridViewTextBoxColumn";
            // 
            // sRECNODataGridViewTextBoxColumn
            // 
            this.sRECNODataGridViewTextBoxColumn.DataPropertyName = "SRECNO";
            this.sRECNODataGridViewTextBoxColumn.HeaderText = "SRECNO";
            this.sRECNODataGridViewTextBoxColumn.Name = "sRECNODataGridViewTextBoxColumn";
            // 
            // bILLDDataGridViewTextBoxColumn
            // 
            this.bILLDDataGridViewTextBoxColumn.DataPropertyName = "BILLD";
            this.bILLDDataGridViewTextBoxColumn.HeaderText = "BILLD";
            this.bILLDDataGridViewTextBoxColumn.Name = "bILLDDataGridViewTextBoxColumn";
            // 
            // dELDDataGridViewTextBoxColumn
            // 
            this.dELDDataGridViewTextBoxColumn.DataPropertyName = "DELD";
            this.dELDDataGridViewTextBoxColumn.HeaderText = "DELD";
            this.dELDDataGridViewTextBoxColumn.Name = "dELDDataGridViewTextBoxColumn";
            // 
            // pAIDDATEDataGridViewTextBoxColumn
            // 
            this.pAIDDATEDataGridViewTextBoxColumn.DataPropertyName = "PAIDDATE";
            this.pAIDDATEDataGridViewTextBoxColumn.HeaderText = "PAIDDATE";
            this.pAIDDATEDataGridViewTextBoxColumn.Name = "pAIDDATEDataGridViewTextBoxColumn";
            // 
            // rSUMDataGridViewTextBoxColumn
            // 
            this.rSUMDataGridViewTextBoxColumn.DataPropertyName = "RSUM";
            this.rSUMDataGridViewTextBoxColumn.HeaderText = "RSUM";
            this.rSUMDataGridViewTextBoxColumn.Name = "rSUMDataGridViewTextBoxColumn";
            // 
            // cRERECNODataGridViewTextBoxColumn
            // 
            this.cRERECNODataGridViewTextBoxColumn.DataPropertyName = "CRERECNO";
            this.cRERECNODataGridViewTextBoxColumn.HeaderText = "CRERECNO";
            this.cRERECNODataGridViewTextBoxColumn.Name = "cRERECNODataGridViewTextBoxColumn";
            // 
            // cUSTNODataGridViewTextBoxColumn
            // 
            this.cUSTNODataGridViewTextBoxColumn.DataPropertyName = "CUSTNO";
            this.cUSTNODataGridViewTextBoxColumn.HeaderText = "CUSTNO";
            this.cUSTNODataGridViewTextBoxColumn.Name = "cUSTNODataGridViewTextBoxColumn";
            // 
            // cUSTNAMEDataGridViewTextBoxColumn
            // 
            this.cUSTNAMEDataGridViewTextBoxColumn.DataPropertyName = "CUSTNAME";
            this.cUSTNAMEDataGridViewTextBoxColumn.HeaderText = "CUSTNAME";
            this.cUSTNAMEDataGridViewTextBoxColumn.Name = "cUSTNAMEDataGridViewTextBoxColumn";
            // 
            // unitIdDataGridViewTextBoxColumn
            // 
            this.unitIdDataGridViewTextBoxColumn.DataPropertyName = "UnitId";
            this.unitIdDataGridViewTextBoxColumn.HeaderText = "UnitId";
            this.unitIdDataGridViewTextBoxColumn.Name = "unitIdDataGridViewTextBoxColumn";
            // 
            // invoiceBindingSource
            // 
            this.invoiceBindingSource.DataSource = typeof(SCPrime.Model.Invoice);
            // 
            // InvoicesFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "InvoicesFrm";
            this.Size = new System.Drawing.Size(919, 541);
            this.panel1.ResumeLayout(false);
            this.pnSearch.ResumeLayout(false);
            this.pnSearch.PerformLayout();
            this.pnTop.ResumeLayout(false);
            this.pnBottom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridInvoice)).EndInit();
            this.pnTopLine.ResumeLayout(false);
            this.pnTopLine.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridLines)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.invoiceLineBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.invoiceBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel pnSearch;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.ComponentModel.BackgroundWorker backgroundWorker2;
        private System.ComponentModel.BackgroundWorker backgroundWorker3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.BindingSource invoiceBindingSource;
        private System.Windows.Forms.Panel pnTop;
        private System.Windows.Forms.Panel pnBottom;
        private System.Windows.Forms.DataGridView gridInvoice;
        private System.Windows.Forms.DataGridViewTextBoxColumn oIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sRECNODataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn bILLDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dELDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn pAIDDATEDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn rSUMDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cRERECNODataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cUSTNODataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cUSTNAMEDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn unitIdDataGridViewTextBoxColumn;
        private System.Windows.Forms.Panel pnTopLine;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView gridLines;
        private System.Windows.Forms.DataGridViewTextBoxColumn oIDDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn iTEMDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nAMEDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn vATCDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nUMDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn rSUMDataGridViewTextBoxColumn1;
        private System.Windows.Forms.BindingSource invoiceLineBindingSource;
        private System.ComponentModel.BackgroundWorker backgroundWorker4;
    }
}
