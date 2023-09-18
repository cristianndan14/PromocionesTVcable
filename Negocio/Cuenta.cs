using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Negocio
{
    [Serializable()]
    [XmlInclude(typeof(Citem))]
    public class Cuenta
    {
        #region [Atributos]
        private int account_id;
        private List<Citem> citems;
        #endregion

        #region [Propiedades]
        public int Account_id { get => account_id; set => account_id = value; }
        public List<Citem> Citems { get => citems; set => citems = value; }
        #endregion

        #region [Constructores]
        public Cuenta()
        {
            Account_id = 0;
            Citems = new List<Citem>();
        }
        public Cuenta(int account_id, List<Citem> citems)
        {
            Account_id = account_id;
            Citems = citems;
        }
        #endregion
    }
}
