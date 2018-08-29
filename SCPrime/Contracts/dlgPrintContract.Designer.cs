namespace SCPrime.Contracts
{
    partial class dlgPrintContract
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
            this.trDocuments = new System.Windows.Forms.TreeView();
            this.pbPrint = new System.Windows.Forms.Button();
            this.pbPreview = new System.Windows.Forms.Button();
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
            // trDocuments
            // 
            this.trDocuments.CheckBoxes = true;
            this.trDocuments.Location = new System.Drawing.Point(12, 11);
            this.trDocuments.Name = "trDocuments";
            this.trDocuments.Size = new System.Drawing.Size(668, 427);
            this.trDocuments.TabIndex = 3;
            // 
            // pbPrint
            // 
            this.pbPrint.Location = new System.Drawing.Point(703, 158);
            this.pbPrint.Name = "pbPrint";
            this.pbPrint.Size = new System.Drawing.Size(85, 30);
            this.pbPrint.TabIndex = 4;
            this.pbPrint.Text = "Print";
            this.pbPrint.UseVisualStyleBackColor = true;
            this.pbPrint.Click += new System.EventHandler(this.pbPrint_Click);
            // 
            // pbPreview
            // 
            this.pbPreview.Location = new System.Drawing.Point(703, 194);
            this.pbPreview.Name = "pbPreview";
            this.pbPreview.Size = new System.Drawing.Size(85, 30);
            this.pbPreview.TabIndex = 5;
            this.pbPreview.Text = "Preview";
            this.pbPreview.UseVisualStyleBackColor = true;
            this.pbPreview.Click += new System.EventHandler(this.pbPreview_Click);
            // 
            // dlgPrintContract
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pbPreview);
            this.Controls.Add(this.pbPrint);
            this.Controls.Add(this.trDocuments);
            this.Name = "dlgPrintContract";
            this.Text = "dlgPrintContract";
            this.Load += new System.EventHandler(this.dlgPrintContract_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dlgPrintContract_KeyDown);
            this.Controls.SetChildIndex(this.pbOK, 0);
            this.Controls.SetChildIndex(this.pbCancel, 0);
            this.Controls.SetChildIndex(this.pbHelp, 0);
            this.Controls.SetChildIndex(this.trDocuments, 0);
            this.Controls.SetChildIndex(this.pbPrint, 0);
            this.Controls.SetChildIndex(this.pbPreview, 0);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView trDocuments;
        private System.Windows.Forms.Button pbPrint;
        private System.Windows.Forms.Button pbPreview;
    }
}