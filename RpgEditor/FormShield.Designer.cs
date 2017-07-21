namespace RpgEditor
{
    partial class FormShield
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
            this.SuspendLayout();
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(499, 485);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(369, 485);
            this.btnEdit.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(240, 485);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            // 
            // lbDetails
            // 
            this.lbDetails.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbDetails.ItemHeight = 16;
            this.lbDetails.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.lbDetails.Size = new System.Drawing.Size(831, 436);
            // 
            // FormShield
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(864, 528);
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.MinimizeBox = false;
            this.Name = "FormShield";
            this.Text = "Shields";
            this.ResumeLayout(false);

        }

        #endregion

    }
}