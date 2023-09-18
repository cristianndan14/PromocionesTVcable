using System;

namespace Negocio
{
    [Serializable()]
    public class DetallePromocion
    {
        #region [Atributos]
        private int id;
        private string controlName;
        private string valor;
        #endregion

        #region [Propiedades]
        public int Id { get => id; set => id = value; }
        public string ControlName { get => controlName; set => controlName = value; }
        public string Valor { get => valor; set => valor = value; }
        #endregion

        #region [Constructores]
        public DetallePromocion()
        {
            Id = 0;
            ControlName = string.Empty;
            Valor = string.Empty;
        }
        public DetallePromocion(int id, string controlname, string valor)
        {
            Id = id;
            ControlName = controlname;
            Valor = valor;
        }
        #endregion
    }
}
