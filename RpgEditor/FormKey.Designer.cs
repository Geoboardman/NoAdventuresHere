namespace RpgEditor
{
    partial class FormKey
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
            this.btnDelete.Location = new System.Drawing.Point(501, 418);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(371, 418);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(242, 418);
            // 
            // lbDetails
            // 
            this.lbDetails.ItemHeight = 16;
            // 
            // FormKey
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(865, 471);
            this.MinimizeBox = false;
            this.Name = "FormKey";
            this.Text = "Keys";
            this.ResumeLayout(false);

        }

        #endregion
    }
}