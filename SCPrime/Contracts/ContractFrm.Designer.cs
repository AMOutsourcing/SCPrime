namespace SCPrime.Contracts
{
    partial class ContractFrm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnCopy = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnMileageReg = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPrev = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.tabRemarks = new System.Windows.Forms.TabPage();
            this.remarkFrm = new SCPrime.Contracts.RemarkFrm();
            this.tabInvoices = new System.Windows.Forms.TabPage();
            this.invoicesFrm = new SCPrime.Contracts.InvoicesFrm();
            this.tabContractData = new System.Windows.Forms.TabPage();
            this.contractDataFrm = new SCPrime.Contracts.ContractDataFrm();
            this.tabOptions = new System.Windows.Forms.TabPage();
            this.contractOption1 = new SCPrime.Contracts.ContractOptionControl();
            this.tabHeader = new System.Windows.Forms.TabPage();
            this.headerControl1 = new SCPrime.Contracts.HeaderControl();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabVehicle = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.vehicleDataTab = new SCPrime.Contracts.VehicleTab();
            this.panel1.SuspendLayout();
            this.tabRemarks.SuspendLayout();
            this.tabInvoices.SuspendLayout();
            this.tabContractData.SuspendLayout();
            this.tabOptions.SuspendLayout();
            this.tabHeader.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabVehicle.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnCopy);
            this.panel1.Controls.Add(this.btnNew);
            this.panel1.Controls.Add(this.btnMileageReg);
            this.panel1.Controls.Add(this.btnPrint);
            this.panel1.Controls.Add(this.btnNext);
            this.panel1.Controls.Add(this.btnPrev);
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(924, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(104, 753);
            this.panel1.TabIndex = 0;
            // 
            // btnCopy
            // 
            this.btnCopy.Location = new System.Drawing.Point(9, 409);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(85, 23);
            this.btnCopy.TabIndex = 7;
            this.btnCopy.Text = "Copy contract";
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(9, 375);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(85, 23);
            this.btnNew.TabIndex = 6;
            this.btnNew.Text = "New version";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnMileageReg
            // 
            this.btnMileageReg.Location = new System.Drawing.Point(9, 301);
            this.btnMileageReg.Name = "btnMileageReg";
            this.btnMileageReg.Size = new System.Drawing.Size(85, 23);
            this.btnMileageReg.TabIndex = 5;
            this.btnMileageReg.Text = "Mileage reg";
            this.btnMileageReg.UseVisualStyleBackColor = true;
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(9, 265);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(85, 23);
            this.btnPrint.TabIndex = 4;
            this.btnPrint.Text = "Print";
            this.btnPrint.UseVisualStyleBackColor = true;
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(9, 178);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(85, 23);
            this.btnNext.TabIndex = 3;
            this.btnNext.Text = "Next. version";
            this.btnNext.UseVisualStyleBackColor = true;
            // 
            // btnPrev
            // 
            this.btnPrev.Location = new System.Drawing.Point(9, 146);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(85, 23);
            this.btnPrev.TabIndex = 2;
            this.btnPrev.Text = "Prev. version";
            this.btnPrev.UseVisualStyleBackColor = true;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(9, 52);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(85, 23);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(9, 23);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(85, 23);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // tabRemarks
            // 
            this.tabRemarks.Controls.Add(this.remarkFrm);
            this.tabRemarks.Location = new System.Drawing.Point(4, 22);
            this.tabRemarks.Name = "tabRemarks";
            this.tabRemarks.Size = new System.Drawing.Size(924, 727);
            this.tabRemarks.TabIndex = 5;
            this.tabRemarks.Text = "Remarks";
            this.tabRemarks.UseVisualStyleBackColor = true;
            // 
            // remarkFrm
            // 
            this.remarkFrm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.remarkFrm.Location = new System.Drawing.Point(0, 0);
            this.remarkFrm.Name = "remarkFrm";
            this.remarkFrm.Size = new System.Drawing.Size(924, 727);
            this.remarkFrm.TabIndex = 0;
            // 
            // tabInvoices
            // 
            this.tabInvoices.Controls.Add(this.invoicesFrm);
            this.tabInvoices.Location = new System.Drawing.Point(4, 22);
            this.tabInvoices.Name = "tabInvoices";
            this.tabInvoices.Size = new System.Drawing.Size(924, 727);
            this.tabInvoices.TabIndex = 4;
            this.tabInvoices.Text = "Invoices";
            this.tabInvoices.UseVisualStyleBackColor = true;
            // 
            // invoicesFrm
            // 
            this.invoicesFrm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.invoicesFrm.Location = new System.Drawing.Point(0, 0);
            this.invoicesFrm.Name = "invoicesFrm";
            this.invoicesFrm.Size = new System.Drawing.Size(924, 727);
            this.invoicesFrm.TabIndex = 0;
            // 
            // tabContractData
            // 
            this.tabContractData.Controls.Add(this.contractDataFrm);
            this.tabContractData.Location = new System.Drawing.Point(4, 22);
            this.tabContractData.Name = "tabContractData";
            this.tabContractData.Size = new System.Drawing.Size(924, 727);
            this.tabContractData.TabIndex = 3;
            this.tabContractData.Text = "Contract data";
            this.tabContractData.UseVisualStyleBackColor = true;
            // 
            // contractDataFrm
            // 
            this.contractDataFrm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contractDataFrm.Location = new System.Drawing.Point(0, 0);
            this.contractDataFrm.Name = "contractDataFrm";
            this.contractDataFrm.Size = new System.Drawing.Size(924, 727);
            this.contractDataFrm.TabIndex = 0;
            // 
            // tabOptions
            // 
            this.tabOptions.Controls.Add(this.contractOption1);
            this.tabOptions.Location = new System.Drawing.Point(4, 22);
            this.tabOptions.Name = "tabOptions";
            this.tabOptions.Padding = new System.Windows.Forms.Padding(3);
            this.tabOptions.Size = new System.Drawing.Size(924, 727);
            this.tabOptions.TabIndex = 2;
            this.tabOptions.Text = "Options";
            this.tabOptions.UseVisualStyleBackColor = true;
            // 
            // contractOption1
            // 
            this.contractOption1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contractOption1.Location = new System.Drawing.Point(3, 3);
            this.contractOption1.Name = "contractOption1";
            this.contractOption1.Size = new System.Drawing.Size(918, 721);
            this.contractOption1.TabIndex = 0;
            // 
            // tabHeader
            // 
            this.tabHeader.Controls.Add(this.headerControl1);
            this.tabHeader.Location = new System.Drawing.Point(4, 22);
            this.tabHeader.Name = "tabHeader";
            this.tabHeader.Padding = new System.Windows.Forms.Padding(3);
            this.tabHeader.Size = new System.Drawing.Size(916, 727);
            this.tabHeader.TabIndex = 1;
            this.tabHeader.Text = "Header";
            this.tabHeader.UseVisualStyleBackColor = true;
            // 
            // headerControl1
            // 
            this.headerControl1.AutoScroll = true;
            this.headerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.headerControl1.Location = new System.Drawing.Point(3, 3);
            this.headerControl1.Name = "headerControl1";
            this.headerControl1.Size = new System.Drawing.Size(910, 721);
            this.headerControl1.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabHeader);
            this.tabControl1.Controls.Add(this.tabVehicle);
            this.tabControl1.Controls.Add(this.tabOptions);
            this.tabControl1.Controls.Add(this.tabContractData);
            this.tabControl1.Controls.Add(this.tabInvoices);
            this.tabControl1.Controls.Add(this.tabRemarks);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(924, 753);
            this.tabControl1.TabIndex = 1;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            this.tabControl1.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabControl1_Selected);
            // 
            // tabVehicle
            // 
            this.tabVehicle.Controls.Add(this.panel2);
            this.tabVehicle.Location = new System.Drawing.Point(4, 22);
            this.tabVehicle.Name = "tabVehicle";
            this.tabVehicle.Padding = new System.Windows.Forms.Padding(3);
            this.tabVehicle.Size = new System.Drawing.Size(924, 727);
            this.tabVehicle.TabIndex = 6;
            this.tabVehicle.Text = "Vehicle data";
            this.tabVehicle.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.vehicleDataTab);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(918, 721);
            this.panel2.TabIndex = 1;
            // 
            // vehicleDataTab
            // 
            this.vehicleDataTab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vehicleDataTab.Location = new System.Drawing.Point(0, 0);
            this.vehicleDataTab.Name = "vehicleDataTab";
            this.vehicleDataTab.Size = new System.Drawing.Size(918, 721);
            this.vehicleDataTab.TabIndex = 0;
            // 
            // ContractFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1028, 753);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel1);
            this.Name = "ContractFrm";
            this.Text = "Contracts";
            this.Load += new System.EventHandler(this.ContractFrm_Load);
            this.panel1.ResumeLayout(false);
            this.tabRemarks.ResumeLayout(false);
            this.tabInvoices.ResumeLayout(false);
            this.tabContractData.ResumeLayout(false);
            this.tabOptions.ResumeLayout(false);
            this.tabHeader.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabVehicle.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPrev;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnCopy;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnMileageReg;
        private System.Windows.Forms.TabPage tabRemarks;
        private System.Windows.Forms.TabPage tabInvoices;
        private System.Windows.Forms.TabPage tabContractData;
        private System.Windows.Forms.TabPage tabOptions;
        private System.Windows.Forms.TabPage tabHeader;
        public HeaderControl headerControl1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabVehicle;
        private ContractOptionControl contractOption1;
        private VehicleTab vehicleDataTab;
        private System.Windows.Forms.Panel panel2;
        private ContractDataFrm contractDataFrm;
        private InvoicesFrm invoicesFrm;
        private RemarkFrm remarkFrm;
    }
}