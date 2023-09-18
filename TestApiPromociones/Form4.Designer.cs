
namespace TestApiPromociones
{
    partial class Form4
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
            this.lstPromocion = new System.Windows.Forms.ListBox();
            this.lstBeneficios = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lstPromocion
            // 
            this.lstPromocion.FormattingEnabled = true;
            this.lstPromocion.Location = new System.Drawing.Point(12, 33);
            this.lstPromocion.Name = "lstPromocion";
            this.lstPromocion.Size = new System.Drawing.Size(431, 95);
            this.lstPromocion.TabIndex = 0;
            this.lstPromocion.DoubleClick += new System.EventHandler(this.lstPromocion_DoubleClick);
            // 
            // lstBeneficios
            // 
            this.lstBeneficios.FormattingEnabled = true;
            this.lstBeneficios.Location = new System.Drawing.Point(12, 176);
            this.lstBeneficios.Name = "lstBeneficios";
            this.lstBeneficios.Size = new System.Drawing.Size(431, 199);
            this.lstBeneficios.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Promoción";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 151);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Beneficios";
            // 
            // Form4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(463, 450);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lstBeneficios);
            this.Controls.Add(this.lstPromocion);
            this.Name = "Form4";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Form4";
            this.Load += new System.EventHandler(this.Form4_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lstPromocion;
        private System.Windows.Forms.ListBox lstBeneficios;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}