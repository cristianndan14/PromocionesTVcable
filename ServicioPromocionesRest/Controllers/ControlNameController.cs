using System.Collections.Generic;
using System.Web.Http;
using Negocio;

namespace ServicioPromocionesRest.Controllers
{
    [RoutePrefix("api/controlname")]
    public class ControlNameController : ApiController
    {
        [HttpGet]
        [AllowAnonymous]
        [Route("ObtenerControlnames")]
        public IHttpActionResult ObtenerControlname()
        {
            ControlName cn = new ControlName();
            List<ControlName> controlNames = new List<ControlName>();
            string respuestaMetodo = string.Empty;
            Response respuestaApi = new Response();

            respuestaMetodo = cn.ObtenerControlNames(0, out controlNames);

            if (respuestaMetodo.ToUpper() == "OK")
            {
                respuestaApi.CodigoError = 0;
                respuestaApi.Descripcion = "Ok";
                respuestaApi.Objeto = controlNames;
                return Ok(respuestaApi);
            }
            else
            {
                respuestaApi.CodigoError = -1;
                respuestaApi.Descripcion = respuestaMetodo;
                respuestaApi.Objeto = null;
                return Ok(respuestaApi);
            }

        }
    }
}