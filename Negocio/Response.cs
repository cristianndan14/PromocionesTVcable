using System;
using System.Xml.Serialization;

namespace Negocio
{
    [Serializable()]
    [XmlInclude(typeof(object))]
    public class Response
    {
        #region [Atributos]
        private int codigoError;
        private string descripcion;
        private object objeto;
        #endregion

        #region [Propiedades]
        public int CodigoError { get => codigoError; set => codigoError = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
        public object Objeto { get => objeto; set => objeto = value; }
        #endregion

        #region [Constructores]
        public Response()
        {
            CodigoError = -1;
            Descripcion = string.Empty;
            Objeto = new object();
        }
        #endregion

    }
}
