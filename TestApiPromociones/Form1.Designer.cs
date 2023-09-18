
namespace TestApiPromociones
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnConsultar = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbPromociones = new System.Windows.Forms.ComboBox();
            this.cmbAplicacion = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.numContrato = new System.Windows.Forms.NumericUpDown();
            this.numCuenta = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnAgregarCitem = new System.Windows.Forms.Button();
            this.lstCitem = new System.Windows.Forms.ListBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.eliminarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.txtUrl = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnJSON = new System.Windows.Forms.Button();
            this.btnDetalle = new System.Windows.Forms.Button();
            this.btnAsignar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numContrato)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCuenta)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnConsultar
            // 
            this.btnConsultar.Location = new System.Drawing.Point(12, 356);
            this.btnConsultar.Name = "btnConsultar";
            this.btnConsultar.Size = new System.Drawing.Size(165, 61);
            this.btnConsultar.TabIndex = 6;
            this.btnConsultar.Text = "Consultar Promociones";
            this.btnConsultar.UseVisualStyleBackColor = true;
            this.btnConsultar.Click += new System.EventHandler(this.btnConsultar_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 420);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(120, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Promociones que aplica";
            // 
            // cmbPromociones
            // 
            this.cmbPromociones.FormattingEnabled = true;
            this.cmbPromociones.Location = new System.Drawing.Point(12, 446);
            this.cmbPromociones.Name = "cmbPromociones";
            this.cmbPromociones.Size = new System.Drawing.Size(369, 21);
            this.cmbPromociones.TabIndex = 7;
            this.cmbPromociones.SelectedIndexChanged += new System.EventHandler(this.cmbPromociones_SelectedIndexChanged);
            // 
            // cmbAplicacion
            // 
            this.cmbAplicacion.FormattingEnabled = true;
            this.cmbAplicacion.Location = new System.Drawing.Point(84, 45);
            this.cmbAplicacion.Name = "cmbAplicacion";
            this.cmbAplicacion.Size = new System.Drawing.Size(297, 21);
            this.cmbAplicacion.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Aplicación";
            // 
            // numContrato
            // 
            this.numContrato.Location = new System.Drawing.Point(84, 72);
            this.numContrato.Maximum = new decimal(new int[] {
            1410065408,
            2,
            0,
            0});
            this.numContrato.Name = "numContrato";
            this.numContrato.Size = new System.Drawing.Size(297, 20);
            this.numContrato.TabIndex = 11;
            this.numContrato.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // numCuenta
            // 
            this.numCuenta.Location = new System.Drawing.Point(84, 98);
            this.numCuenta.Maximum = new decimal(new int[] {
            1410065408,
            2,
            0,
            0});
            this.numCuenta.Name = "numCuenta";
            this.numCuenta.Size = new System.Drawing.Size(297, 20);
            this.numCuenta.TabIndex = 12;
            this.numCuenta.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Contrato";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 100);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Cuenta";
            // 
            // btnAgregarCitem
            // 
            this.btnAgregarCitem.Location = new System.Drawing.Point(15, 141);
            this.btnAgregarCitem.Name = "btnAgregarCitem";
            this.btnAgregarCitem.Size = new System.Drawing.Size(117, 23);
            this.btnAgregarCitem.TabIndex = 15;
            this.btnAgregarCitem.Text = "Agregar Citem";
            this.btnAgregarCitem.UseVisualStyleBackColor = true;
            this.btnAgregarCitem.Click += new System.EventHandler(this.btnAgregarCitem_Click);
            // 
            // lstCitem
            // 
            this.lstCitem.ContextMenuStrip = this.contextMenuStrip1;
            this.lstCitem.FormattingEnabled = true;
            this.lstCitem.Location = new System.Drawing.Point(15, 170);
            this.lstCitem.Name = "lstCitem";
            this.lstCitem.Size = new System.Drawing.Size(366, 173);
            this.lstCitem.TabIndex = 16;
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
            // txtUrl
            // 
            this.txtUrl.Enabled = false;
            this.txtUrl.Location = new System.Drawing.Point(84, 12);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(297, 20);
            this.txtUrl.TabIndex = 17;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 13);
            this.label5.TabIndex = 18;
            this.label5.Text = "Url Servicio";
            // 
            // btnJSON
            // 
            this.btnJSON.Location = new System.Drawing.Point(216, 356);
            this.btnJSON.Name = "btnJSON";
            this.btnJSON.Size = new System.Drawing.Size(165, 61);
            this.btnJSON.TabIndex = 19;
            this.btnJSON.Text = "Ver JSON";
            this.btnJSON.UseVisualStyleBackColor = true;
            this.btnJSON.Click += new System.EventHandler(this.btnJSON_Click);
            // 
            // btnDetalle
            // 
            this.btnDetalle.Location = new System.Drawing.Point(12, 477);
            this.btnDetalle.Name = "btnDetalle";
            this.btnDetalle.Size = new System.Drawing.Size(165, 23);
            this.btnDetalle.TabIndex = 21;
            this.btnDetalle.Text = "Detalle";
            this.btnDetalle.UseVisualStyleBackColor = true;
            this.btnDetalle.Click += new System.EventHandler(this.btnDetalle_Click);
            // 
            // btnAsignar
            // 
            this.btnAsignar.Location = new System.Drawing.Point(216, 477);
            this.btnAsignar.Name = "btnAsignar";
            this.btnAsignar.Size = new System.Drawing.Size(165, 23);
            this.btnAsignar.TabIndex = 22;
            this.btnAsignar.Text = "Asignar";
            this.btnAsignar.UseVisualStyleBackColor = true;
            this.btnAsignar.Click += new System.EventHandler(this.btnAsignar_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(410, 512);
            this.Controls.Add(this.btnAsignar);
            this.Controls.Add(this.btnDetalle);
            this.Controls.Add(this.btnJSON);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtUrl);
            this.Controls.Add(this.lstCitem);
            this.Controls.Add(this.btnAgregarCitem);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.numCuenta);
            this.Controls.Add(this.numContrato);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbAplicacion);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmbPromociones);
            this.Controls.Add(this.btnConsultar);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numContrato)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCuenta)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnConsultar;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbPromociones;
        private System.Windows.Forms.ComboBox cmbAplicacion;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numContrato;
        private System.Windows.Forms.NumericUpDown numCuenta;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnAgregarCitem;
        private System.Windows.Forms.ListBox lstCitem;
        private System.Windows.Forms.TextBox txtUrl;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnJSON;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem eliminarToolStripMenuItem;
        private System.Windows.Forms.Button btnDetalle;
        private System.Windows.Forms.Button btnAsignar;
    }
}

