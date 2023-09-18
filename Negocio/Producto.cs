using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Negocio
{
    [Serializable()]
    [XmlInclude(typeof(Promocion))]

    public class Producto
    {
        #region [Atributos]
        private int id;
        private string nombre;
        private List<PromoComercial> promoComercials;
        #endregion

        #region [Propiedades]
        public int Id { get => id; set => id = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public List<PromoComercial> PromoComercials { get => promoComercials; set => promoComercials = value; }
        #endregion

        #region [Constructores]
        public Producto()
        {
            Id = 0;
            Nombre = string.Empty;
            PromoComercials = new List<PromoComercial>();
        }
        public Producto(int id,string nombre,List<PromoComercial> promoComercials)
        {
            Id = id;
            Nombre = nombre;
            PromoComercials = promoComercials;
        }
        #endregion
    }
}
