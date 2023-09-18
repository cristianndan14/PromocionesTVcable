using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Negocio
{
    [Serializable()]
    [XmlInclude(typeof(Contrato))]
    [XmlInclude(typeof(ControlName))]
    [XmlInclude(typeof(Aplicacion))]

    public class Request
    {
        #region [Atributos]
        private Contrato contrato;
        private Aplicacion aplicacion;
        #endregion

        #region [Propiedades]
        public Contrato Contrato { get => contrato; set => contrato = value; }
        public Aplicacion Aplicacion { get => aplicacion; set => aplicacion = value; }
        #endregion

        #region [Constructores]
        public Request()
        {
            Contrato = new Contrato();
            Aplicacion = new Aplicacion();
        }
        #endregion
    }
    //public class ProcessId
    //{
    //    private Int32 Id;
    //}
    //public Id  { get => Id; set => contrato = value; }
}
