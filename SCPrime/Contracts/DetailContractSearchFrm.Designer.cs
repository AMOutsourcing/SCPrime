namespace SCPrime.Contracts
{
    partial class DetailContractSearchFrm
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.colDetailContractOID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colContractNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colVersionNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colContractStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colVin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colInfo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.collectiveContractBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.collectiveContractBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(737, 48);
            this.panel1.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnClose);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(537, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(200, 48);
            this.panel2.TabIndex = 2;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(113, 9);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colDetailContractOID,
            this.colContractNo,
            this.colVersionNo,
            this.colContractStatus,
            this.colVin,
            this.colInfo});
            this.dataGridView1.DataSource = this.collectiveContractBindingSource;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 48);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(737, 331);
            this.dataGridView1.TabIndex = 2;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentDoubleClick);
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            this.dataGridView1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellValueChanged);
            // 
            // colDetailContractOID
            // 
            this.colDetailContractOID.DataPropertyName = "DetailContractOID";
            this.colDetailContractOID.HeaderText = "OID";
            this.colDetailContractOID.Name = "colDetailContractOID";
            this.colDetailContractOID.ReadOnly = true;
            // 
            // colContractNo
            // 
            this.colContractNo.DataPropertyName = "ContractNo";
            this.colContractNo.HeaderText = "ContractNo";
            this.colContractNo.Name = "colContractNo";
            this.colContractNo.ReadOnly = true;
            // 
            // colVersionNo
            // 
            this.colVersionNo.DataPropertyName = "VersionNo";
            this.colVersionNo.HeaderText = "VersionNo";
            this.colVersionNo.Name = "colVersionNo";
            this.colVersionNo.ReadOnly = true;
            // 
            // colContractStatus
            // 
            this.colContractStatus.DataPropertyName = "ContractStatus";
            this.colContractStatus.HeaderText = "ContractStatus";
            this.colContractStatus.Name = "colContractStatus";
            this.colContractStatus.ReadOnly = true;
            // 
            // colVin
            // 
            this.colVin.DataPropertyName = "VIN";
            this.colVin.HeaderText = "VIN";
            this.colVin.Name = "colVin";
            this.colVin.ReadOnly = true;
            this.colVin.Visible = false;
            // 
            // colInfo
            // 
            this.colInfo.DataPropertyName = "Info";
            this.colInfo.HeaderText = "Info";
            this.colInfo.Name = "colInfo";
            this.colInfo.ReadOnly = true;
            this.colInfo.Visible = false;
            // 
            // collectiveContractBindingSource
            // 
            this.collectiveContractBindingSource.DataSource = typeof(SCPrime.Model.CollectiveContract);
            // 
            // DetailContractSearchFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(737, 379);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.panel1);
            this.Name = "DetailContractSearchFrm";
            this.Text = "DetailContractSearchFrm";
            this.Load += new System.EventHandler(this.DetailContractSearchFrm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DetailContractSearchFrm_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.collectiveContractBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.BindingSource collectiveContractBindingSource;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDetailContractOID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colContractNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colVersionNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colContractStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn colVin;
        private System.Windows.Forms.DataGridViewTextBoxColumn colInfo;
    }
}