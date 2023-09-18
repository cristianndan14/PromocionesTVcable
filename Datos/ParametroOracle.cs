using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dato
{
    public class ParametroOracle
    {
        private string nombre;
        private OracleDbType tipo;
        private string valor;

        public string Nombre { get => nombre; set => nombre = value; }
        public OracleDbType Tipo { get => tipo; set => tipo = value; }
        public string Valor { get => valor; set => valor = value; }

        public ParametroOracle()
        {
            Nombre = string.Empty;
            valor = string.Empty;
        }
        public ParametroOracle(string nombre, OracleDbType tipo, string valor)
        {
            Nombre = nombre;
            Tipo = tipo;
            Valor = valor;
        }

    }
}
