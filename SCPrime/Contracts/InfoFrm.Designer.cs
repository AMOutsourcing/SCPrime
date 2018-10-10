namespace SCPrime.Contracts
{
    partial class InfoFrm
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
            this.txtInfo = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // pbOK
            // 
            this.pbOK.Click += new System.EventHandler(this.pbOK_Click);
            // 
            // txtInfo
            // 
            this.txtInfo.Location = new System.Drawing.Point(100, 12);
            this.txtInfo.Name = "txtInfo";
            this.txtInfo.Size = new System.Drawing.Size(363, 102);
            this.txtInfo.TabIndex = 0;
            this.txtInfo.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Info";
            // 
            // InfoFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(649, 149);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtInfo);
            this.Name = "InfoFrm";
            this.Text = "InfoFrm";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.InfoFrm_KeyDown);
            this.Controls.SetChildIndex(this.txtInfo, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.pbOK, 0);
            this.Controls.SetChildIndex(this.pbCancel, 0);
            this.Controls.SetChildIndex(this.pbHelp, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox txtInfo;
        private System.Windows.Forms.Label label1;
    }
}