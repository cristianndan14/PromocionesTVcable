using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Negocio
{
    [Serializable()]
    [XmlInclude(typeof(Cuenta))]
    public class Contrato
    {
        #region [Atributos]
        private int cparty_id;
        private List<Cuenta> cuentas;
        #endregion

        #region [Propiedades]
        public int Cparty_id { get => cparty_id; set => cparty_id = value; }
        public List<Cuenta> Cuentas { get => cuentas; set => cuentas = value; }
        #endregion

        #region [Constructores]
        public Contrato()
        {
            Cparty_id = 0;
            Cuentas = new List<Cuenta>();
        }
        public Contrato(int cparty_id, List<Cuenta> cuentas)
        {
            Cparty_id = cparty_id;
            Cuentas = cuentas;
        }
        #endregion
    }
}
