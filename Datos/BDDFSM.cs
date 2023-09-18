using System;
using System.Configuration;
using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace Datos
{
    public class BDDFSM
    {
        #region [Atributos]
        //Atributos
        private static string servidorBDD;
        private static string nombreBDD;
        private static string usuario;
        private static string password;
        private static string cadenaConexion;
        private static string puertoBDD;
        private static string serviceNameBDD;
        private static string detalleError;
        #endregion

        #region [Propiedades]
        //Propiedades
        protected static string ServidorBDD { get => servidorBDD; }
        protected static string NombreBDD { get => nombreBDD; }
        protected static string Usuario { get => usuario; }
        protected static string Password { get => password; }
        protected static string CadenaConexion { get => cadenaConexion; }
        protected static string PuertoBDD { get => puertoBDD; }
        protected static string ServiceNameBDD { get => serviceNameBDD; }
        protected static string DetalleError { get => detalleError; }
        #endregion

        #region [Constructores]
        //Constructor de instancia
        public BDDFSM()
        {
            servidorBDD = ConfigurationManager.AppSettings["servidorBDDFsm"];
            nombreBDD = ConfigurationManager.AppSettings["nombreBDDFsm"];
            usuario = ConfigurationManager.AppSettings["usuarioBDDFsm"];
            password = ConfigurationManager.AppSettings["passwordBDDFsm"];
            puertoBDD = ConfigurationManager.AppSettings["puertoBDDFsm"];
            serviceNameBDD = ConfigurationManager.AppSettings["serviceNameBDDFsm"];
            detalleError = "";
            cadenaConexion = @" User Id="+ usuario + "; Password="+ password + "; Data Source=(DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = "+ servidorBDD + ")(PORT = "+ puertoBDD + "))(LOAD_BALANCE = yes)(CONNECT_DATA = (SERVER = DEDICATED) (SERVICE_NAME = "+ serviceNameBDD + ")));";
        }
        #endregion

        #region [Métodos]
        //Métodos
        public string EjecutarSentencia(string sentenciaSQL)
        {
            #region [Ejecutar una sentencia Sql]
            try
            {
                OracleConnection conexion = new OracleConnection(cadenaConexion);
                OracleCommand comando = conexion.CreateCommand();
                try
                {
                    conexion.Open();
                    comando.CommandText = sentenciaSQL;
                    comando.ExecuteNonQuery();
                    return "ok";
                }
                catch (OracleException e)
                {
                    detalleError = MensajeError(e.Number, e.Message);
                    return detalleError;
                }
                finally
                {
                    conexion.Close();
                }
            }
            catch (Exception ex)
            {
                return "Datos Fsm: "+ex.Message;
            }
            #endregion
        }
        public string EjecutarSentenciaTransaccion(string sentenciaSQL)
        {
            #region [Ejecutar una sentencia Sql en transacción]
            try
            {
                OracleConnection conexion = new OracleConnection(cadenaConexion);
                OracleCommand comando = conexion.CreateCommand();
                conexion.Open();
                OracleTransaction sqlTran = conexion.BeginTransaction();
                try
                {
                    comando.CommandText = sentenciaSQL;
                    comando.Transaction = sqlTran;
                    comando.ExecuteNonQuery();
                    sqlTran.Commit();
                    return "ok";
                }
                catch (OracleException e)
                {
                    try
                    {
                        detalleError = MensajeError(e.Number, e.Message);
                        sqlTran.Rollback();
                    }
                    catch (OracleException exRollback)
                    {
                        detalleError = MensajeError(exRollback.Number, e.Message);
                    }
                    return detalleError;
                }
                finally
                {
                    conexion.Close();
                }
            }
            catch (Exception ex)
            {
                return "Datos Fsm: " + ex.Message;
            }
            #endregion
        }
        public string EjecutarSentenciaTransaccionScalar(string sentenciaSQL, out string valor)
        {
            #region [Ejecutar una sentencia Sql en transacción y se obtiene el id creado]
            valor =string.Empty;
            try
            {
                OracleConnection conexion = new OracleConnection(cadenaConexion);
                OracleCommand comando = conexion.CreateCommand();
                conexion.Open();
                OracleTransaction sqlTran = conexion.BeginTransaction();
                try
                {
                    comando.CommandText = sentenciaSQL;
                    comando.Transaction = sqlTran;
                    valor = comando.ExecuteScalar().ToString();
                    sqlTran.Commit();
                    return "ok";
                }
                catch (OracleException e)
                {
                    try
                    {
                        detalleError = MensajeError(e.Number, e.Message);
                        sqlTran.Rollback();
                    }
                    catch (OracleException exRollback)
                    {
                        detalleError = MensajeError(exRollback.Number, e.Message);
                    }
                    return detalleError;
                }
                finally
                {
                    conexion.Close();
                }
            }
            catch (Exception ex)
            {
                return "Datos Fsm: "+ex.Message;
            }
            #endregion
        }
        public string EjecutarComando(string sentenciaSQL, out string valor)
        {
            #region [Ejecutar el comando y obtener el valor]
            valor = string.Empty;
            try
            {
                OracleConnection conexion = new OracleConnection(cadenaConexion);
                OracleCommand comando = conexion.CreateCommand();
                try
                {
                    conexion.Open();
                    comando.CommandText = sentenciaSQL;
                    valor = comando.ExecuteScalar().ToString();
                    return "ok";
                }
                catch (OracleException e)
                {
                    return MensajeError(e.Number, e.Message);
                }
                catch (Exception ex)
                {
                    return "Datos Fsm: " + ex.Message;
                }
                finally
                {
                    conexion.Close();
                }
            }
            catch (Exception ex)
            {
                return "Datos Fsm: "+ex.Message;
            }
            #endregion
        }

        public string EjecutarConsultaTablas(string SentenciaSQL, out DataSet datos)
        {
            #region [Consultar tablas y llenar dataset]
            datos = new DataSet();
            try
            {
                OracleConnection conexion = new OracleConnection(cadenaConexion);

                try
                {
                    conexion.Open();
                    OracleDataAdapter adaptador = new OracleDataAdapter(SentenciaSQL, conexion);
                    adaptador.Fill(datos);
                    return "ok";
                }
                catch (OracleException e)
                {
                    return MensajeError(e.Number, e.Message);
                }
                finally
                {
                    conexion.Close();
                }

            }
            catch (Exception ex)
            {
                return "Datos Fsm: " + ex.Message;
            }
            #endregion
        }
        public string EjecutarConsulta(string SentenciaSQL,out DataTable respuesta)
        {
            #region [Ejecuta la consulta y obtiene la tabla resultado]
            respuesta = new DataTable();
            try
            {
                OracleConnection conexion = new OracleConnection(cadenaConexion);

                try
                {
                    conexion.Open();
                    OracleDataAdapter adaptador = new OracleDataAdapter(SentenciaSQL, conexion);
                    adaptador.Fill(respuesta);
                    return "ok";
                }
                catch (OracleException e)
                {
                    return MensajeError(e.ErrorCode, e.Message);
                }
                finally
                {
                    conexion.Close();
                }

            }
            catch (Exception ex)
            {
                return "Datos: " + ex.Message;
            }
            #endregion
        }

        private string MensajeError(int numeroError, string mensaje)
        {
            #region [Obtiene el número de error y devuelve el mensaje personalizado]
            string respuesta = "";
            switch (numeroError)
            {
                case 2:
                    respuesta = "No se puede encontrar el servidor de base de datos, por favor revise su conexión o contáctese con el administrador del sistema.";
                    break;

                case 4060:
                    respuesta = "No se puede acceder a la base de datos, por favor revise su conexión o contáctese con el administrador del sistema.";
                    break;
                case 547:
                    respuesta = "No se puede eliminar el código, tiene transacciones registradas.";
                    break;
                case 2627:
                    respuesta = "El código ingresado ya existe.";
                    break;
                default:
                    respuesta = "Se produjo un error desconocido.\n" + "número:" + numeroError + "\nMensaje:" + mensaje;
                    break;
            }
            return "Base de Datos Fsm: " + respuesta;
            #endregion
        }

        public string FormatoFecha(DateTime fecha)
        {
            #region [Formato fecha para escritura en la base]
            string respuesta = "";

            string anio = "";
            string mes = "";
            string dia = "";

            string hora = "";
            string minuto = "";
            string segundo = "";

            anio = fecha.Year.ToString();

            if (fecha.Month < 10)
                mes = "0" + fecha.Month.ToString();
            else
                mes = fecha.Month.ToString();

            if (fecha.Day < 10)
                dia = "0" + fecha.Day.ToString();
            else
                dia = fecha.Day.ToString();

            if (fecha.Hour < 10)
                hora = "0" + fecha.Hour.ToString();
            else
                hora = fecha.Hour.ToString();

            if (fecha.Minute < 10)
                minuto = "0" + fecha.Minute.ToString();
            else
                minuto = fecha.Minute.ToString();

            if (fecha.Second < 10)
                segundo = "0" + fecha.Second.ToString();
            else
                segundo = fecha.Second.ToString();

            respuesta = anio + "" + mes + "" + dia + " " + hora + ":" + minuto + ":" + segundo;

            return respuesta;
            #endregion
        }
        #endregion
    }
}
