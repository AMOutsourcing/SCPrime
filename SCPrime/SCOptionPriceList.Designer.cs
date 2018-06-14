namespace SCPrime
{
    partial class SCOptionPriceList
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.gridOptionPrice = new System.Windows.Forms.DataGridView();
            this.cbContactType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.cbLstCate = new System.Windows.Forms.ComboBox();
            this.sCOptionCategoryBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.sCOptionPriceBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.sCOptionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.sCOptionDetailBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridOptionPrice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sCOptionCategoryBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sCOptionPriceBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sCOptionBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sCOptionDetailBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cbLstCate);
            this.panel1.Controls.Add(this.gridOptionPrice);
            this.panel1.Controls.Add(this.cbContactType);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(0, -2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(799, 450);
            this.panel1.TabIndex = 0;
            // 
            // gridOptionPrice
            // 
            this.gridOptionPrice.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridOptionPrice.Location = new System.Drawing.Point(18, 155);
            this.gridOptionPrice.Name = "gridOptionPrice";
            this.gridOptionPrice.Size = new System.Drawing.Size(754, 265);
            this.gridOptionPrice.TabIndex = 3;
            this.gridOptionPrice.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridOptionPrice_CellEnter);
            this.gridOptionPrice.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.gridOptionPrice_EditingControlShowing);
            // 
            // cbContactType
            // 
            this.cbContactType.DisplayMember = "OID";
            this.cbContactType.FormattingEnabled = true;
            this.cbContactType.Location = new System.Drawing.Point(105, 21);
            this.cbContactType.Name = "cbContactType";
            this.cbContactType.Size = new System.Drawing.Size(503, 21);
            this.cbContactType.TabIndex = 2;
            this.cbContactType.ValueMember = "OID";
            this.cbContactType.SelectedIndexChanged += new System.EventHandler(this.cbContactType_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Contact type";
            this.label1.UseMnemonic = false;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(844, 213);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(844, 13);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(844, 60);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(75, 23);
            this.btnNew.TabIndex = 3;
            this.btnNew.Text = "New";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(844, 115);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 4;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // cbLstCate
            // 
            this.cbLstCate.FormattingEnabled = true;
            this.cbLstCate.Location = new System.Drawing.Point(541, 81);
            this.cbLstCate.Name = "cbLstCate";
            this.cbLstCate.Size = new System.Drawing.Size(121, 21);
            this.cbLstCate.TabIndex = 4;
            this.cbLstCate.Visible = false;
            this.cbLstCate.SelectedIndexChanged += new System.EventHandler(this.cbLstCate_SelectedIndexChanged);
            // 
            // sCOptionCategoryBindingSource
            // 
            this.sCOptionCategoryBindingSource.DataSource = typeof(SCPrime.Model.SCOptionCategory);
            // 
            // sCOptionPriceBindingSource
            // 
            this.sCOptionPriceBindingSource.DataSource = typeof(SCPrime.Model.SCOptionPrice);
            // 
            // sCOptionBindingSource
            // 
            this.sCOptionBindingSource.DataSource = typeof(SCPrime.Model.SCOption);
            // 
            // sCOptionDetailBindingSource
            // 
            this.sCOptionDetailBindingSource.DataSource = typeof(SCPrime.Model.SCOptionDetail);
            // 
            // SCOptionPriceList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(950, 450);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.panel1);
            this.Name = "SCOptionPriceList";
            this.Text = "SCOptionPriceList";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridOptionPrice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sCOptionCategoryBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sCOptionPriceBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sCOptionBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sCOptionDetailBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbContactType;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isAvailableDataGridViewCheckBoxColumn;
        private System.Windows.Forms.BindingSource sCOptionPriceBindingSource;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.BindingSource sCOptionCategoryBindingSource;
        private System.Windows.Forms.BindingSource sCOptionBindingSource;
        private System.Windows.Forms.BindingSource sCOptionDetailBindingSource;
        private System.Windows.Forms.DataGridView gridOptionPrice;
        private System.Windows.Forms.ComboBox cbLstCate;
    }
}