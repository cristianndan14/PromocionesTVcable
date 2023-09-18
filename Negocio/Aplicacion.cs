using System;

namespace Negocio
{
    [Serializable()]
    public class Aplicacion
    {
        #region [Atributos]
        private int id;
        private string nombre;
        #endregion

        #region [Propiedades]
        public int Id { get => id; set => id = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        #endregion

        #region [Constructores]
        public Aplicacion()
        {
            Id = 0;
            Nombre = string.Empty;
        }
        public Aplicacion(int id, string nombre)
        {
            Id = id;
            Nombre = nombre;
        }
        public override string ToString()
        {
            return Id + " " + Nombre;
        }
        #endregion
    }
}
