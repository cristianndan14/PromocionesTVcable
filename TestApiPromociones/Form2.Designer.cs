
namespace TestApiPromociones
{
    partial class Form2
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
            this.label2 = new System.Windows.Forms.Label();
            this.numContrato = new System.Windows.Forms.NumericUpDown();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbControlnames = new System.Windows.Forms.ComboBox();
            this.btnAgregarControlName = new System.Windows.Forms.Button();
            this.lstControlName = new System.Windows.Forms.ListBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.eliminarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label3 = new System.Windows.Forms.Label();
            this.numValorControlName = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAgregarCitem = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numContrato)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numValorControlName)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "CitemId";
            // 
            // numContrato
            // 
            this.numContrato.Location = new System.Drawing.Point(110, 20);
            this.numContrato.Maximum = new decimal(new int[] {
            1410065408,
            2,
            0,
            0});
            this.numContrato.Name = "numContrato";
            this.numContrato.Size = new System.Drawing.Size(221, 20);
            this.numContrato.TabIndex = 15;
            this.numContrato.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbControlnames);
            this.groupBox1.Controls.Add(this.btnAgregarControlName);
            this.groupBox1.Controls.Add(this.lstControlName);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.numValorControlName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(4, 74);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(333, 253);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Control name asociados al Citem";
            // 
            // cmbControlnames
            // 
            this.cmbControlnames.FormattingEnabled = true;
            this.cmbControlnames.Location = new System.Drawing.Point(104, 22);
            this.cmbControlnames.Name = "cmbControlnames";
            this.cmbControlnames.Size = new System.Drawing.Size(221, 21);
            this.cmbControlnames.TabIndex = 25;
            // 
            // btnAgregarControlName
            // 
            this.btnAgregarControlName.Location = new System.Drawing.Point(11, 88);
            this.btnAgregarControlName.Name = "btnAgregarControlName";
            this.btnAgregarControlName.Size = new System.Drawing.Size(128, 23);
            this.btnAgregarControlName.TabIndex = 24;
            this.btnAgregarControlName.Text = "Agregar control name";
            this.btnAgregarControlName.UseVisualStyleBackColor = true;
            this.btnAgregarControlName.Click += new System.EventHandler(this.btnAgregarControlName_Click);
            // 
            // lstControlName
            // 
            this.lstControlName.ContextMenuStrip = this.contextMenuStrip1;
            this.lstControlName.FormattingEnabled = true;
            this.lstControlName.Location = new System.Drawing.Point(11, 127);
            this.lstControlName.Name = "lstControlName";
            this.lstControlName.Size = new System.Drawing.Size(314, 108);
            this.lstControlName.TabIndex = 23;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.eliminarToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(118, 26);
            // 
            // eliminarToolStripMenuItem
            // 
            this.eliminarToolStripMenuItem.Name = "eliminarToolStripMenuItem";
            this.eliminarToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.eliminarToolStripMenuItem.Text = "Eliminar";
            this.eliminarToolStripMenuItem.Click += new System.EventHandler(this.eliminarToolStripMenuItem_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 13);
            this.label3.TabIndex = 22;
            this.label3.Text = "Valor Controlname";
            // 
            // numValorControlName
            // 
            this.numValorControlName.Location = new System.Drawing.Point(104, 51);
            this.numValorControlName.Maximum = new decimal(new int[] {
            1410065408,
            2,
            0,
            0});
            this.numValorControlName.Name = "numValorControlName";
            this.numValorControlName.Size = new System.Drawing.Size(221, 20);
            this.numValorControlName.TabIndex = 21;
            this.numValorControlName.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 20;
            this.label1.Text = "Id Controlname";
            // 
            // btnAgregarCitem
            // 
            this.btnAgregarCitem.Location = new System.Drawing.Point(12, 341);
            this.btnAgregarCitem.Name = "btnAgregarCitem";
            this.btnAgregarCitem.Size = new System.Drawing.Size(131, 23);
            this.btnAgregarCitem.TabIndex = 25;
            this.btnAgregarCitem.Text = "Agregar Citem";
            this.btnAgregarCitem.UseVisualStyleBackColor = true;
            this.btnAgregarCitem.Click += new System.EventHandler(this.btnAgregarCitem_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(237, 341);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(100, 23);
            this.btnCancelar.TabIndex = 26;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(349, 383);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnAgregarCitem);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.numContrato);
            this.Name = "Form2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Form2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numContrato)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numValorControlName)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numContrato;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnAgregarControlName;
        private System.Windows.Forms.ListBox lstControlName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAgregarCitem;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.NumericUpDown numValorControlName;
        private System.Windows.Forms.ComboBox cmbControlnames;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem eliminarToolStripMenuItem;
    }
}