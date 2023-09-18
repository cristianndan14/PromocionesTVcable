using Negocio;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TestApiPromociones
{
    public class Api
    {
        #region [Atributos]
        private static string url = ConfigurationManager.AppSettings["urlApi"];
        private string objeto;
        private string tokenAutorizacion;
        private string bodyJson;
        #endregion

        #region [Propiedades]
        public string Objeto { get => objeto; set => objeto = value; }
        public string TokenAutorizacion { get => tokenAutorizacion; set => tokenAutorizacion = value; }
        public string BodyJson { get => bodyJson; set => bodyJson = value; }
        #endregion

        #region [Constructores]
        public Api()
        {
            Objeto = string.Empty;
            TokenAutorizacion = string.Empty;
            BodyJson = string.Empty;
        }
        public Api(string objeto, string tokenAutorizacion, string bodyJson)
        {
            Objeto = objeto;
            TokenAutorizacion = tokenAutorizacion;
            BodyJson = bodyJson;
        }
        #endregion

        #region [Métodos]
        public Response Get(string filtro)
        {
            #region [Consumir Get]
            return Ejecutar("GET", filtro);
            #endregion
        }
        public Response Post()
        {
            #region [Consumir Post]
            return Ejecutar("POST", "");
            #endregion
        }
        public Response Put()
        {
            #region [Consumir Put]
            return Ejecutar("PUT", "");
            #endregion
        }
        public Response Delete(string filtro)
        {
            #region [Consumir Delete]
            return Ejecutar("DELETE", filtro);
            #endregion
        }
        private Response Ejecutar(string metodo, string filtro)
        {
            #region [Ejecutar Rest Api]
            Response respuestaApi = new Response();
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url + Objeto + filtro);
                request.Method = metodo;
                request.ContentType = "application/json";
                if (TokenAutorizacion != string.Empty)
                    request.Headers.Add("Authorization", "Bearer " + TokenAutorizacion);

                if (metodo == "POST" || metodo == "PUT")
                {
                    byte[] byteArray = Encoding.UTF8.GetBytes(BodyJson);
                    request.ContentLength = byteArray.Length;
                    Stream dataStream = request.GetRequestStream();
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    dataStream.Close();
                }
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.Accepted || response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.Created)
                {
                    StreamReader read = new StreamReader(response.GetResponseStream());
                    String result = read.ReadToEnd();
                    respuestaApi = (Response)Newtonsoft.Json.JsonConvert.DeserializeObject(result, typeof(Response));
                }
                else
                {
                    respuestaApi.CodigoError = (int)response.StatusCode;
                    respuestaApi.Descripcion = response.StatusDescription;
                    respuestaApi.Objeto = null;
                }
                return respuestaApi;
            }
            catch (WebException ex)
            {
                if (ex.Response != null)
                {
                    respuestaApi.CodigoError = Convert.ToInt32(((HttpWebResponse)ex.Response).StatusCode);
                    respuestaApi.Descripcion = ((HttpWebResponse)ex.Response).StatusDescription;
                    respuestaApi.Objeto = null;
                }
                else
                {
                    respuestaApi.CodigoError = -1;
                    respuestaApi.Descripcion = ex.Message;
                    respuestaApi.Objeto = null;
                }

                return respuestaApi; ;

            }
            catch (Exception ex)
            {
                respuestaApi.CodigoError = ex.HResult;
                respuestaApi.Descripcion = ex.Message;
                respuestaApi.Objeto = null;
                return respuestaApi; ;
            }
            #endregion
        }
        #endregion
    }
}
