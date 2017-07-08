namespace AADSecuredWebApiCallInvoker
{
    partial class ApiTestUtility
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
            this.Invoke = new System.Windows.Forms.Button();
            this.displaypanel = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // Invoke
            // 
            this.Invoke.Location = new System.Drawing.Point(222, 32);
            this.Invoke.Name = "Invoke";
            this.Invoke.Size = new System.Drawing.Size(133, 57);
            this.Invoke.TabIndex = 0;
            this.Invoke.Text = "Invoke";
            this.Invoke.UseVisualStyleBackColor = true;
            this.Invoke.Click += new System.EventHandler(this.Invoke_Click);
            // 
            // displaypanel
            // 
            this.displaypanel.BackColor = System.Drawing.SystemColors.Info;
            this.displaypanel.Location = new System.Drawing.Point(12, 128);
            this.displaypanel.Multiline = true;
            this.displaypanel.Name = "displaypanel";
            this.displaypanel.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.displaypanel.Size = new System.Drawing.Size(607, 240);
            this.displaypanel.TabIndex = 1;
            // 
            // TestUtility
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(631, 380);
            this.Controls.Add(this.displaypanel);
            this.Controls.Add(this.Invoke);
            this.Name = "TestUtility";
            this.Text = "TestUtility";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Invoke;
        private System.Windows.Forms.TextBox displaypanel;
    }
}