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
            this.pnBottom = new System.Windows.Forms.Panel();
            this.gridLines = new System.Windows.Forms.DataGridView();
            this.pnTopLine = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.pnTop = new System.Windows.Forms.Panel();
            this.gridInvoice = new System.Windows.Forms.DataGridView();
            this.pnSearch = new System.Windows.Forms.Panel();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.cbKm = new System.Windows.Forms.CheckBox();
            this.cbNormal = new System.Windows.Forms.CheckBox();
            this.cbCredit = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker3 = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker4 = new System.ComponentModel.BackgroundWorker();
            this.sCInvoiceBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lNAMEDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.eXPLDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pAIDSUMDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sCInvoiceItemBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.iTEMNODataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sUPLNODataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rTYPEDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nOTEDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rINFODataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            this.pnBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridLines)).BeginInit();
            this.pnTopLine.SuspendLayout();
            this.pnTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridInvoice)).BeginInit();
            this.pnSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sCInvoiceBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sCInvoiceItemBindingSource)).BeginInit();
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
            // gridLines
            // 
            this.gridLines.AllowUserToAddRows = false;
            this.gridLines.AllowUserToDeleteRows = false;
            this.gridLines.AutoGenerateColumns = false;
            this.gridLines.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridLines.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.iTEMNODataGridViewTextBoxColumn,
            this.sUPLNODataGridViewTextBoxColumn,
            this.dataGridViewTextBoxColumn7,
            this.rTYPEDataGridViewTextBoxColumn,
            this.dataGridViewTextBoxColumn8,
            this.dataGridViewTextBoxColumn9,
            this.dataGridViewTextBoxColumn10,
            this.nOTEDataGridViewTextBoxColumn,
            this.rINFODataGridViewTextBoxColumn});
            this.gridLines.DataSource = this.sCInvoiceItemBindingSource;
            this.gridLines.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridLines.Location = new System.Drawing.Point(0, 30);
            this.gridLines.Name = "gridLines";
            this.gridLines.ReadOnly = true;
            this.gridLines.Size = new System.Drawing.Size(915, 195);
            this.gridLines.TabIndex = 1;
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
            // gridInvoice
            // 
            this.gridInvoice.AllowUserToAddRows = false;
            this.gridInvoice.AllowUserToDeleteRows = false;
            this.gridInvoice.AutoGenerateColumns = false;
            this.gridInvoice.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridInvoice.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.lNAMEDataGridViewTextBoxColumn,
            this.eXPLDataGridViewTextBoxColumn,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6,
            this.pAIDSUMDataGridViewTextBoxColumn});
            this.gridInvoice.DataSource = this.sCInvoiceBindingSource;
            this.gridInvoice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridInvoice.Location = new System.Drawing.Point(0, 61);
            this.gridInvoice.Name = "gridInvoice";
            this.gridInvoice.ReadOnly = true;
            this.gridInvoice.Size = new System.Drawing.Size(915, 397);
            this.gridInvoice.TabIndex = 1;
            this.gridInvoice.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridInvoice_CellClick);
            // 
            // pnSearch
            // 
            this.pnSearch.Controls.Add(this.button5);
            this.pnSearch.Controls.Add(this.button4);
            this.pnSearch.Controls.Add(this.button3);
            this.pnSearch.Controls.Add(this.button1);
            this.pnSearch.Controls.Add(this.cbKm);
            this.pnSearch.Controls.Add(this.cbNormal);
            this.pnSearch.Controls.Add(this.cbCredit);
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
            this.button3.Click += new System.EventHandler(this.button3_Click);
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
            // cbKm
            // 
            this.cbKm.AutoSize = true;
            this.cbKm.Location = new System.Drawing.Point(270, 17);
            this.cbKm.Name = "cbKm";
            this.cbKm.Size = new System.Drawing.Size(78, 17);
            this.cbKm.TabIndex = 3;
            this.cbKm.Text = "Km invoice";
            this.cbKm.UseVisualStyleBackColor = true;
            this.cbKm.CheckedChanged += new System.EventHandler(this.cbKm_CheckedChanged);
            // 
            // cbNormal
            // 
            this.cbNormal.AutoSize = true;
            this.cbNormal.Location = new System.Drawing.Point(168, 19);
            this.cbNormal.Name = "cbNormal";
            this.cbNormal.Size = new System.Drawing.Size(96, 17);
            this.cbNormal.TabIndex = 2;
            this.cbNormal.Text = "Normal invoice";
            this.cbNormal.UseVisualStyleBackColor = true;
            this.cbNormal.CheckedChanged += new System.EventHandler(this.cbNormal_CheckedChanged);
            // 
            // cbCredit
            // 
            this.cbCredit.AutoSize = true;
            this.cbCredit.Location = new System.Drawing.Point(72, 21);
            this.cbCredit.Name = "cbCredit";
            this.cbCredit.Size = new System.Drawing.Size(90, 17);
            this.cbCredit.TabIndex = 1;
            this.cbCredit.Text = "Credit invoice";
            this.cbCredit.UseVisualStyleBackColor = true;
            this.cbCredit.CheckedChanged += new System.EventHandler(this.cbCredit_CheckedChanged);
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
            // sCInvoiceBindingSource
            // 
            this.sCInvoiceBindingSource.DataSource = typeof(SCPrime.Model.SCInvoice);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "OID";
            this.dataGridViewTextBoxColumn1.HeaderText = "OID";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "CustNo";
            this.dataGridViewTextBoxColumn2.HeaderText = "CustNo";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // lNAMEDataGridViewTextBoxColumn
            // 
            this.lNAMEDataGridViewTextBoxColumn.DataPropertyName = "LNAME";
            this.lNAMEDataGridViewTextBoxColumn.HeaderText = "LNAME";
            this.lNAMEDataGridViewTextBoxColumn.Name = "lNAMEDataGridViewTextBoxColumn";
            this.lNAMEDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // eXPLDataGridViewTextBoxColumn
            // 
            this.eXPLDataGridViewTextBoxColumn.DataPropertyName = "EXPL";
            this.eXPLDataGridViewTextBoxColumn.HeaderText = "EXPL";
            this.eXPLDataGridViewTextBoxColumn.Name = "eXPLDataGridViewTextBoxColumn";
            this.eXPLDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "SRECNO";
            this.dataGridViewTextBoxColumn3.HeaderText = "SRECNO";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "BILLD";
            this.dataGridViewTextBoxColumn4.HeaderText = "BILLD";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "DELD";
            this.dataGridViewTextBoxColumn5.HeaderText = "DELD";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "PAIDDATE";
            this.dataGridViewTextBoxColumn6.HeaderText = "PAIDDATE";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            // 
            // pAIDSUMDataGridViewTextBoxColumn
            // 
            this.pAIDSUMDataGridViewTextBoxColumn.DataPropertyName = "PAIDSUM";
            this.pAIDSUMDataGridViewTextBoxColumn.HeaderText = "PAIDSUM";
            this.pAIDSUMDataGridViewTextBoxColumn.Name = "pAIDSUMDataGridViewTextBoxColumn";
            this.pAIDSUMDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // sCInvoiceItemBindingSource
            // 
            this.sCInvoiceItemBindingSource.DataSource = typeof(SCPrime.Model.SCInvoiceItem);
            // 
            // iTEMNODataGridViewTextBoxColumn
            // 
            this.iTEMNODataGridViewTextBoxColumn.DataPropertyName = "ITEMNO";
            this.iTEMNODataGridViewTextBoxColumn.HeaderText = "ITEMNO";
            this.iTEMNODataGridViewTextBoxColumn.Name = "iTEMNODataGridViewTextBoxColumn";
            this.iTEMNODataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // sUPLNODataGridViewTextBoxColumn
            // 
            this.sUPLNODataGridViewTextBoxColumn.DataPropertyName = "SUPLNO";
            this.sUPLNODataGridViewTextBoxColumn.HeaderText = "SUPLNO";
            this.sUPLNODataGridViewTextBoxColumn.Name = "sUPLNODataGridViewTextBoxColumn";
            this.sUPLNODataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.DataPropertyName = "NAME";
            this.dataGridViewTextBoxColumn7.HeaderText = "NAME";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            // 
            // rTYPEDataGridViewTextBoxColumn
            // 
            this.rTYPEDataGridViewTextBoxColumn.DataPropertyName = "RTYPE";
            this.rTYPEDataGridViewTextBoxColumn.HeaderText = "RTYPE";
            this.rTYPEDataGridViewTextBoxColumn.Name = "rTYPEDataGridViewTextBoxColumn";
            this.rTYPEDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.DataPropertyName = "NUM";
            this.dataGridViewTextBoxColumn8.HeaderText = "NUM";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.DataPropertyName = "RSUM";
            this.dataGridViewTextBoxColumn9.HeaderText = "RSUM";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn10
            // 
            this.dataGridViewTextBoxColumn10.DataPropertyName = "VATCD";
            this.dataGridViewTextBoxColumn10.HeaderText = "VATCD";
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            this.dataGridViewTextBoxColumn10.ReadOnly = true;
            // 
            // nOTEDataGridViewTextBoxColumn
            // 
            this.nOTEDataGridViewTextBoxColumn.DataPropertyName = "NOTE";
            this.nOTEDataGridViewTextBoxColumn.HeaderText = "NOTE";
            this.nOTEDataGridViewTextBoxColumn.Name = "nOTEDataGridViewTextBoxColumn";
            this.nOTEDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // rINFODataGridViewTextBoxColumn
            // 
            this.rINFODataGridViewTextBoxColumn.DataPropertyName = "RINFO";
            this.rINFODataGridViewTextBoxColumn.HeaderText = "RINFO";
            this.rINFODataGridViewTextBoxColumn.Name = "rINFODataGridViewTextBoxColumn";
            this.rINFODataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // InvoicesFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "InvoicesFrm";
            this.Size = new System.Drawing.Size(919, 541);
            this.panel1.ResumeLayout(false);
            this.pnBottom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridLines)).EndInit();
            this.pnTopLine.ResumeLayout(false);
            this.pnTopLine.PerformLayout();
            this.pnTop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridInvoice)).EndInit();
            this.pnSearch.ResumeLayout(false);
            this.pnSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sCInvoiceBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sCInvoiceItemBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel pnSearch;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.ComponentModel.BackgroundWorker backgroundWorker2;
        private System.ComponentModel.BackgroundWorker backgroundWorker3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox cbCredit;
        private System.Windows.Forms.CheckBox cbNormal;
        private System.Windows.Forms.CheckBox cbKm;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
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
        private System.ComponentModel.BackgroundWorker backgroundWorker4;
        private System.Windows.Forms.DataGridViewTextBoxColumn iTEMNODataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sUPLNODataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn rTYPEDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private System.Windows.Forms.DataGridViewTextBoxColumn nOTEDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn rINFODataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource sCInvoiceItemBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn lNAMEDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn eXPLDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn pAIDSUMDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource sCInvoiceBindingSource;
    }
}
