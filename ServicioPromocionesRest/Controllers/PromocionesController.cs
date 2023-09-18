using System;
using System.Collections.Generic;
using System.Web.Http;
using Negocio;

namespace ServicioPromocionesRest.Controllers
{
    [RoutePrefix("api/promociones")]
    public class PromocionesController : ApiController
    {        
        #region [Obtener promociones]
        [HttpPost]
        [AllowAnonymous]
        [Route("ObtenerPromociones")]
        
        public IHttpActionResult ObtenerPromociones(Request peticion)
        {         
            if (peticion == null)
                return BadRequest("wrong request");

            //List<Producto> listaproductos = new List<Producto>();
            List<PromoComercial> listapromoComercial = new List<PromoComercial>();
            Promocion promocion = new Promocion();
            string respuestaMetodo = string.Empty;
            Response respuestaApi = new Response();

            //respuestaMetodo = promocion.ObtenerpromocionesProductosContratados(peticion.Contrato, peticion.ControlNames, peticion.Aplicacion, out listaproductos);
            respuestaMetodo = promocion.ObtenerPromociones(peticion.Contrato, peticion.Aplicacion, out listapromoComercial);
            if (respuestaMetodo.ToUpper() == "OK")
            {
                respuestaApi.CodigoError = 0;
                respuestaApi.Descripcion = "Ok";
                respuestaApi.Objeto = listapromoComercial;
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
        #endregion
        #region [Asignar Promociones]
        [HttpPost]
        [AllowAnonymous]
        [Route("AsignarPromociones")]

        public IHttpActionResult AsignarPromociones(PromoComercial promoComercial, Int32 processId)
        {
            #region [Obtener promociones]
            if (promoComercial == null)
                return BadRequest("wrong request");

            string respuestaMetodo = string.Empty;
            Response respuestaApi = new Response();

            //respuestaMetodo = promocion.ObtenerpromocionesProductosContratados(peticion.Contrato, peticion.ControlNames, peticion.Aplicacion, out listaproductos);
            respuestaMetodo = promoComercial.RegitraPromociones(processId);//27721244
            if (respuestaMetodo.ToUpper() == "OK")
            {
                respuestaApi.CodigoError = 0;
                respuestaApi.Descripcion = "Ok";
                return Ok(respuestaApi);
            }
            else
            {
                respuestaApi.CodigoError = -1;
                respuestaApi.Descripcion = respuestaMetodo;
                respuestaApi.Objeto = null;
                return Ok(respuestaApi);
            }
            #endregion
        }

        #endregion

    }

}
