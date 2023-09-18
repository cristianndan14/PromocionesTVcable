using System;
using System.Collections.Generic;
using System.Configuration;
using System.Windows.Forms;
using Negocio;

namespace TestApiPromociones
{
    public partial class Form1 : Form
    {
        List<Aplicacion> aplicaciones = new List<Aplicacion>();
        List<ControlName> controlnames = new List<ControlName>();
        List<Citem> listaCitems = new List<Citem>();

        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            txtUrl.Text = ConfigurationManager.AppSettings["urlApi"];
            aplicaciones.Add(new Aplicacion(1, "Tytan"));
            aplicaciones.Add(new Aplicacion(2, "App"));
            aplicaciones.Add(new Aplicacion(3, "Ecommerce"));
            cmbAplicacion.DataSource = aplicaciones;
            CargarControlnames();
            lstCitem.DataSource = listaCitems;
        }
        private void btnConsultar_Click(object sender, EventArgs e)
        {
            Cuenta cuenta = new Cuenta();
            cuenta.Account_id = Convert.ToInt32(numCuenta.Value);
            cuenta.Citems = listaCitems;

            Contrato contrato = new Contrato();
            contrato.Cparty_id = Convert.ToInt32(numContrato.Value);
            contrato.Cuentas.Add(cuenta);

            if (contrato.Cparty_id > 0)
            {
                if (cuenta.Account_id > 0)
                {
                    if (listaCitems.Count > 0)
                    {
                        Response respuestaApi = new Response();
                        Request peticion = new Request();
                        peticion.Aplicacion = cmbAplicacion.SelectedItem as Aplicacion;
                        peticion.Contrato = contrato;

                        Api api = new Api();
                        api.Objeto = "promociones/ObtenerPromociones";
                        api.BodyJson = Newtonsoft.Json.JsonConvert.SerializeObject(peticion);
                        respuestaApi = api.Post();

                        if (respuestaApi.CodigoError == 0)
                        {
                            List<PromoComercial> listapromoComercial = new List<PromoComercial>();
                            listapromoComercial = (List<PromoComercial>)Newtonsoft.Json.JsonConvert.DeserializeObject(respuestaApi.Objeto.ToString(), typeof(List<PromoComercial>));

                            if (listapromoComercial.Count > 0)
                            {
                                cmbPromociones.DataSource = null;
                                cmbPromociones.DataSource = listapromoComercial;
                                cmbPromociones.DisplayMember = "Nombre";

                                MessageBox.Show("Consulta realizada");
                            }
                            else
                                MessageBox.Show("No se tiene ofertas");
                        }
                        else
                        {
                            MessageBox.Show(respuestaApi.Descripcion);
                        }
                    }
                    else
                        MessageBox.Show("Agregue al menos 1 citem");
                }
                else
                    MessageBox.Show("La cuenta debe ser diferente de 0");
            }
            else
                MessageBox.Show("El contrato debe ser diferente de 0");
        }

        private void btnAgregarCitem_Click(object sender, EventArgs e)
        {
            Form2 frm = new Form2(controlnames);
            frm.ShowDialog();

            if (frm.Citem != null && frm.Citem.Citem_id != string.Empty)
                listaCitems.Add(frm.Citem);

            RefreshListview();
        }

        private void CargarControlnames()
        {
            Api api = new Api();
            api.Objeto = "controlname/ObtenerControlNames";
            Response respuestaApi = new Response();
            respuestaApi = api.Get("");

            if (respuestaApi.CodigoError == 0)
                controlnames = (List<ControlName>)Newtonsoft.Json.JsonConvert.DeserializeObject(respuestaApi.Objeto.ToString(), typeof(List<ControlName>));
            else
                MessageBox.Show(respuestaApi.Descripcion);
        }

        private void btnJSON_Click(object sender, EventArgs e)
        {
            Cuenta cuenta = new Cuenta();
            cuenta.Account_id = Convert.ToInt32(numCuenta.Value);
            cuenta.Citems = listaCitems;

            Contrato contrato = new Contrato();
            contrato.Cparty_id = Convert.ToInt32(numContrato.Value);
            contrato.Cuentas.Add(cuenta);

            Response respuestaApi = new Response();
            Request peticion = new Request();
            peticion.Aplicacion = cmbAplicacion.SelectedItem as Aplicacion;
            peticion.Contrato = contrato;

            Form3 frm = new Form3(Newtonsoft.Json.JsonConvert.SerializeObject(peticion));
            frm.ShowDialog();
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listaCitems.RemoveAt(lstCitem.SelectedIndex);
            RefreshListview();
        }

        private void RefreshListview()
        {
            lstCitem.DataSource = null;
            lstCitem.DataSource = listaCitems;
        }

        private void btnDetalle_Click(object sender, EventArgs e)
        {
            Form4 frm = new Form4(cmbPromociones.SelectedItem as PromoComercial);
            frm.ShowDialog();
        }

        private void btnAsignar_Click(object sender, EventArgs e)
        {
            Api api = new Api();
            api.Objeto = "promociones/AsignarPromociones";
            api.BodyJson = Newtonsoft.Json.JsonConvert.SerializeObject(cmbPromociones.SelectedItem as PromoComercial);
            Response respuestaApi = new Response();
            respuestaApi = api.Post();
            
            MessageBox.Show(respuestaApi.Descripcion);
        }

        private void cmbPromociones_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
