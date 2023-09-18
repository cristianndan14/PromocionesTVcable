using System.Collections.Generic;
using Negocio;

namespace ServicioPromocionesSOAP
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "Service1" en el código, en svc y en el archivo de configuración.
    // NOTE: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione Service1.svc o Service1.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class ServicePromociones : IServicePromociones
    {
        public string ObtenerPromociones(string peticionString)
        {
            Request peticion = new Request();
            peticion = (Request)Newtonsoft.Json.JsonConvert.DeserializeObject(peticionString, typeof(Request));

            List<PromoComercial> listaPromocionesComerciales = new List<PromoComercial>();
            Promocion promocion = new Promocion();
            string respuestaMetodo = string.Empty;
            Response respuestaApi = new Response();

            if (peticion == null)
            {
                respuestaApi.CodigoError = -1;
                respuestaApi.Descripcion = "wrong request";
                respuestaApi.Objeto = null;
                return Newtonsoft.Json.JsonConvert.SerializeObject(respuestaApi);
            }

            respuestaMetodo = promocion.ObtenerPromociones(peticion.Contrato, peticion.Aplicacion, out listaPromocionesComerciales);

            if (respuestaMetodo.ToUpper() == "OK")
            {
                respuestaApi.CodigoError = 0;
                respuestaApi.Descripcion = "Ok";
                respuestaApi.Objeto = listaPromocionesComerciales;
                return Newtonsoft.Json.JsonConvert.SerializeObject(respuestaApi);
            }
            else
            {
                respuestaApi.CodigoError = -1;
                respuestaApi.Descripcion = respuestaMetodo;
                respuestaApi.Objeto = null;
                return Newtonsoft.Json.JsonConvert.SerializeObject(respuestaApi);
            }
        }
        public string ObtenerControlname()
        {
            ControlName cn = new ControlName();
            List<ControlName> controlNames = new List<ControlName>();
            string respuestaMetodo = string.Empty;
            Response respuestaApi = new Response();

            respuestaMetodo = cn.ObtenerControlNames(0,out controlNames);

            if (respuestaMetodo.ToUpper() == "OK")
            {
                respuestaApi.CodigoError = 0;
                respuestaApi.Descripcion = "Ok";
                respuestaApi.Objeto = (List<ControlName>)controlNames;

                return Newtonsoft.Json.JsonConvert.SerializeObject(respuestaApi);
            }
            else
            {
                respuestaApi.CodigoError = -1;
                respuestaApi.Descripcion = respuestaMetodo;
                respuestaApi.Objeto = null;
                return Newtonsoft.Json.JsonConvert.SerializeObject(respuestaApi);
            }
        }

    }
}
