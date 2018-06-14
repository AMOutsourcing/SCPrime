﻿namespace SCPrime
{
    partial class SCOptionPriceFrm
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
            this.label1 = new System.Windows.Forms.Label();
            this.cbContactType = new System.Windows.Forms.ComboBox();
            this.gridPrice = new System.Windows.Forms.DataGridView();
            this.categoryNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.optionNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.optionDetailNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.includeDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.optionalDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.notAvailableDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.excludeDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.oIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contractTypeOIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.optionCategoryOIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.optionOIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.optionDetailOIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isAvailableDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.infoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.createdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.modifiedDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sCOptionPriceBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gridPrice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sCOptionPriceBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Contact type";
            // 
            // cbContactType
            // 
            this.cbContactType.FormattingEnabled = true;
            this.cbContactType.Location = new System.Drawing.Point(147, 13);
            this.cbContactType.Name = "cbContactType";
            this.cbContactType.Size = new System.Drawing.Size(475, 21);
            this.cbContactType.TabIndex = 1;
            this.cbContactType.SelectedIndexChanged += new System.EventHandler(this.cbContactType_SelectedIndexChanged);
            this.cbContactType.SelectedValueChanged += new System.EventHandler(this.cbContactType_SelectedValueChanged);
            // 
            // gridPrice
            // 
            this.gridPrice.AllowUserToAddRows = false;
            this.gridPrice.AllowUserToDeleteRows = false;
            this.gridPrice.AutoGenerateColumns = false;
            this.gridPrice.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridPrice.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.categoryNameDataGridViewTextBoxColumn,
            this.optionNameDataGridViewTextBoxColumn,
            this.optionDetailNameDataGridViewTextBoxColumn,
            this.includeDataGridViewCheckBoxColumn,
            this.optionalDataGridViewCheckBoxColumn,
            this.notAvailableDataGridViewCheckBoxColumn,
            this.excludeDataGridViewCheckBoxColumn,
            this.oIDDataGridViewTextBoxColumn,
            this.contractTypeOIDDataGridViewTextBoxColumn,
            this.optionCategoryOIDDataGridViewTextBoxColumn,
            this.optionOIDDataGridViewTextBoxColumn,
            this.optionDetailOIDDataGridViewTextBoxColumn,
            this.isAvailableDataGridViewTextBoxColumn,
            this.infoDataGridViewTextBoxColumn,
            this.createdDataGridViewTextBoxColumn,
            this.modifiedDataGridViewTextBoxColumn});
            this.gridPrice.DataSource = this.sCOptionPriceBindingSource;
            this.gridPrice.Location = new System.Drawing.Point(32, 108);
            this.gridPrice.Name = "gridPrice";
            this.gridPrice.Size = new System.Drawing.Size(756, 313);
            this.gridPrice.TabIndex = 2;
            this.gridPrice.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridPrice_CellContentClick);
            this.gridPrice.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridPrice_CellValueChanged);
            // 
            // categoryNameDataGridViewTextBoxColumn
            // 
            this.categoryNameDataGridViewTextBoxColumn.DataPropertyName = "CategoryName";
            this.categoryNameDataGridViewTextBoxColumn.HeaderText = "CategoryName";
            this.categoryNameDataGridViewTextBoxColumn.Name = "categoryNameDataGridViewTextBoxColumn";
            // 
            // optionNameDataGridViewTextBoxColumn
            // 
            this.optionNameDataGridViewTextBoxColumn.DataPropertyName = "OptionName";
            this.optionNameDataGridViewTextBoxColumn.HeaderText = "OptionName";
            this.optionNameDataGridViewTextBoxColumn.Name = "optionNameDataGridViewTextBoxColumn";
            // 
            // optionDetailNameDataGridViewTextBoxColumn
            // 
            this.optionDetailNameDataGridViewTextBoxColumn.DataPropertyName = "OptionDetailName";
            this.optionDetailNameDataGridViewTextBoxColumn.HeaderText = "OptionDetailName";
            this.optionDetailNameDataGridViewTextBoxColumn.Name = "optionDetailNameDataGridViewTextBoxColumn";
            // 
            // includeDataGridViewCheckBoxColumn
            // 
            this.includeDataGridViewCheckBoxColumn.DataPropertyName = "Include";
            this.includeDataGridViewCheckBoxColumn.HeaderText = "Include";
            this.includeDataGridViewCheckBoxColumn.Name = "includeDataGridViewCheckBoxColumn";
            // 
            // optionalDataGridViewCheckBoxColumn
            // 
            this.optionalDataGridViewCheckBoxColumn.DataPropertyName = "Optional";
            this.optionalDataGridViewCheckBoxColumn.HeaderText = "Optional";
            this.optionalDataGridViewCheckBoxColumn.Name = "optionalDataGridViewCheckBoxColumn";
            // 
            // notAvailableDataGridViewCheckBoxColumn
            // 
            this.notAvailableDataGridViewCheckBoxColumn.DataPropertyName = "NotAvailable";
            this.notAvailableDataGridViewCheckBoxColumn.HeaderText = "NotAvailable";
            this.notAvailableDataGridViewCheckBoxColumn.Name = "notAvailableDataGridViewCheckBoxColumn";
            // 
            // excludeDataGridViewCheckBoxColumn
            // 
            this.excludeDataGridViewCheckBoxColumn.DataPropertyName = "Exclude";
            this.excludeDataGridViewCheckBoxColumn.HeaderText = "Exclude";
            this.excludeDataGridViewCheckBoxColumn.Name = "excludeDataGridViewCheckBoxColumn";
            // 
            // oIDDataGridViewTextBoxColumn
            // 
            this.oIDDataGridViewTextBoxColumn.DataPropertyName = "OID";
            this.oIDDataGridViewTextBoxColumn.HeaderText = "OID";
            this.oIDDataGridViewTextBoxColumn.Name = "oIDDataGridViewTextBoxColumn";
            this.oIDDataGridViewTextBoxColumn.Visible = false;
            // 
            // contractTypeOIDDataGridViewTextBoxColumn
            // 
            this.contractTypeOIDDataGridViewTextBoxColumn.DataPropertyName = "ContractTypeOID";
            this.contractTypeOIDDataGridViewTextBoxColumn.HeaderText = "ContractTypeOID";
            this.contractTypeOIDDataGridViewTextBoxColumn.Name = "contractTypeOIDDataGridViewTextBoxColumn";
            this.contractTypeOIDDataGridViewTextBoxColumn.Visible = false;
            // 
            // optionCategoryOIDDataGridViewTextBoxColumn
            // 
            this.optionCategoryOIDDataGridViewTextBoxColumn.DataPropertyName = "OptionCategoryOID";
            this.optionCategoryOIDDataGridViewTextBoxColumn.HeaderText = "OptionCategoryOID";
            this.optionCategoryOIDDataGridViewTextBoxColumn.Name = "optionCategoryOIDDataGridViewTextBoxColumn";
            this.optionCategoryOIDDataGridViewTextBoxColumn.Visible = false;
            // 
            // optionOIDDataGridViewTextBoxColumn
            // 
            this.optionOIDDataGridViewTextBoxColumn.DataPropertyName = "OptionOID";
            this.optionOIDDataGridViewTextBoxColumn.HeaderText = "OptionOID";
            this.optionOIDDataGridViewTextBoxColumn.Name = "optionOIDDataGridViewTextBoxColumn";
            this.optionOIDDataGridViewTextBoxColumn.Visible = false;
            // 
            // optionDetailOIDDataGridViewTextBoxColumn
            // 
            this.optionDetailOIDDataGridViewTextBoxColumn.DataPropertyName = "OptionDetailOID";
            this.optionDetailOIDDataGridViewTextBoxColumn.HeaderText = "OptionDetailOID";
            this.optionDetailOIDDataGridViewTextBoxColumn.Name = "optionDetailOIDDataGridViewTextBoxColumn";
            this.optionDetailOIDDataGridViewTextBoxColumn.Visible = false;
            // 
            // isAvailableDataGridViewTextBoxColumn
            // 
            this.isAvailableDataGridViewTextBoxColumn.DataPropertyName = "IsAvailable";
            this.isAvailableDataGridViewTextBoxColumn.HeaderText = "IsAvailable";
            this.isAvailableDataGridViewTextBoxColumn.Name = "isAvailableDataGridViewTextBoxColumn";
            this.isAvailableDataGridViewTextBoxColumn.Visible = false;
            // 
            // infoDataGridViewTextBoxColumn
            // 
            this.infoDataGridViewTextBoxColumn.DataPropertyName = "Info";
            this.infoDataGridViewTextBoxColumn.HeaderText = "Info";
            this.infoDataGridViewTextBoxColumn.Name = "infoDataGridViewTextBoxColumn";
            this.infoDataGridViewTextBoxColumn.Visible = false;
            // 
            // createdDataGridViewTextBoxColumn
            // 
            this.createdDataGridViewTextBoxColumn.DataPropertyName = "Created";
            this.createdDataGridViewTextBoxColumn.HeaderText = "Created";
            this.createdDataGridViewTextBoxColumn.Name = "createdDataGridViewTextBoxColumn";
            this.createdDataGridViewTextBoxColumn.Visible = false;
            // 
            // modifiedDataGridViewTextBoxColumn
            // 
            this.modifiedDataGridViewTextBoxColumn.DataPropertyName = "Modified";
            this.modifiedDataGridViewTextBoxColumn.HeaderText = "Modified";
            this.modifiedDataGridViewTextBoxColumn.Name = "modifiedDataGridViewTextBoxColumn";
            this.modifiedDataGridViewTextBoxColumn.Visible = false;
            // 
            // sCOptionPriceBindingSource
            // 
            this.sCOptionPriceBindingSource.DataSource = typeof(SCPrime.Model.SCOptionPrice);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(696, 58);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(696, 13);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // SCOptionPrice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.gridPrice);
            this.Controls.Add(this.cbContactType);
            this.Controls.Add(this.label1);
            this.Name = "SCOptionPrice";
            this.Text = "SCOptionPrice";
            ((System.ComponentModel.ISupportInitialize)(this.gridPrice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sCOptionPriceBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbContactType;
        private System.Windows.Forms.DataGridView gridPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn categoryNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn optionNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn optionDetailNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn includeDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn optionalDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn notAvailableDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn excludeDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn oIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn contractTypeOIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn optionCategoryOIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn optionOIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn optionDetailOIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn isAvailableDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn infoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn createdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn modifiedDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource sCOptionPriceBindingSource;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
    }
}