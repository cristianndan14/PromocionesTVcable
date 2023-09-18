using Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestApiPromociones
{
    public partial class Form4 : Form
    {
        public Form4(PromoComercial promocomercial)
        {
            InitializeComponent();
            lstPromocion.DataSource = promocomercial.Promociones;
            lstPromocion.DisplayMember = "Descripcion";
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        private void lstPromocion_DoubleClick(object sender, EventArgs e)
        {
            lstBeneficios.DataSource = null;
            lstBeneficios.DataSource = (lstPromocion.SelectedItem as Promocion).DetallesPromocion;
        }
    }
}
