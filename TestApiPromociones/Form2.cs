using Negocio;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace TestApiPromociones
{
    public partial class Form2 : Form
    {
        private Citem citem= new Citem();

        public Citem Citem { get => citem; set => citem = value; }

        public Form2()
        {
            InitializeComponent();
        }
        public Form2(List<ControlName> controlnames)
        {
            InitializeComponent();
            cmbControlnames.DataSource = controlnames;
            cmbControlnames.DisplayMember = "Nombre";
            lstControlName.DataSource = Citem.Controlnames;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            btnAgregarCitem.Enabled = false;
        }

        private void btnAgregarControlName_Click(object sender, EventArgs e)
        {
            ControlName aux = new ControlName();
            aux = cmbControlnames.SelectedItem as ControlName;
            aux.Valor = numValorControlName.Value.ToString();

            if (Convert.ToInt32(aux.Valor) > 0)
            {
                Citem.Controlnames.Add(aux);
                RefreshListview();
                btnAgregarCitem.Enabled = true;
            }
            else
                MessageBox.Show("El valor del control name debe ser diferente de 0");
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Citem.Controlnames.RemoveAt(lstControlName.SelectedIndex);
            RefreshListview();
        }
        private void RefreshListview()
        {
            lstControlName.DataSource = null;
            lstControlName.DataSource = Citem.Controlnames;

            if (Citem.Controlnames.Count == 0)
                btnAgregarCitem.Enabled = false;
        }

        private void btnAgregarCitem_Click(object sender, EventArgs e)
        {
            Citem.Citem_id = numContrato.Value.ToString();

            if (Convert.ToInt32(Citem.Citem_id) > 0)
            {
                this.Close();
            }
            else
                MessageBox.Show("El CitemId debe ser diferente de 0");
        }
    }
}
