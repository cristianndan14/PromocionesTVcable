using System;
using System.Collections.Generic;

namespace Negocio
{
    [Serializable()]
    public class Citem
    {
        #region [Atributos]
        private string citem_id;
        private List<ControlName> controlnames = new List<ControlName>(); 
        #endregion

        #region [Propiedades]
        public string Citem_id { get => citem_id; set => citem_id = value; }
        public List<ControlName> Controlnames { get => controlnames; set => controlnames = value; }
        #endregion

        #region [Constructores]
        public Citem()
        {
            Citem_id = string.Empty;
            Controlnames = new List<ControlName>();
        }
        public Citem(string citem_id, List<ControlName> controlnames)
        {
            Citem_id = citem_id;
            Controlnames = controlnames;
        }
        public override string ToString()
        {
            return "CitemId:" + Citem_id;
        }
        #endregion
    }
}
