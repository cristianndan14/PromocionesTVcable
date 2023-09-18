using MySql.Data.MySqlClient;
using Npgsql;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Datos
{
    public class BDDGRUPOTVCABLE
    {
        #region [Atributos]
        //Atributos
        private static string servidorBDD;
        private static string gestorBDD;
        private static string nombreBDD;
        private static string usuario;
        private static string password;
        private static string cadenaConexion;
        private static string detalleError;
        private static string pathReporte;
        private static string puertoBDD;
        private static string sslModeBDD;
        private static string serviceNameBDD;
        #endregion

        #region [Propiedades]
        //Propiedades
        protected static string ServidorBDD { get => servidorBDD; }
        protected static string NombreBDD { get => nombreBDD; }
        protected static string Usuario { get => usuario; }
        protected static string Password { get => password; }
        protected static string CadenaConexion { get => cadenaConexion; }
        protected static string DetalleError { get => detalleError; }
        protected static string PathReporte { get => pathReporte; }
        protected static string GestorBDD { get => gestorBDD; }
        protected static string PuertoBDD { get => puertoBDD; }
        protected static string ServiceNameBDD { get => serviceNameBDD; }
        protected static string SslModeBDD1 { get => sslModeBDD; }
        #endregion

        #region [Constructores]
        //Constructor de instancia
        public BDDGRUPOTVCABLE()
        {
            servidorBDD = ConfigurationManager.AppSettings["servidorBDDGRUPOTVCABLE"];
            nombreBDD = ConfigurationManager.AppSettings["nombreBDDGRUPOTVCABLE"];
            usuario = ConfigurationManager.AppSettings["usuarioBDDGRUPOTVCABLE"];
            password = ConfigurationManager.AppSettings["passwordBDDGRUPOTVCABLE"];
            gestorBDD = ConfigurationManager.AppSettings["gestorBDDGRUPOTVCABLE"];
            puertoBDD = ConfigurationManager.AppSettings["puertoBDDGRUPOTVCABLE"];
            sslModeBDD = ConfigurationManager.AppSettings["sslModeBDDGRUPOTVCABLE"];
            serviceNameBDD = ConfigurationManager.AppSettings["serviceNameBDDGRUPOTVCABLE"];

            switch (GestorBDD)
            {
                case "MYSQL":
                    cadenaConexion = @"Datasource=" + ServidorBDD + "; Port=" + PuertoBDD + "; SSL Mode=None; Database=" + nombreBDD + "; Username=" + Usuario + "; Password=" + Password + "";

                    break;
                case "SQL-EXPRESS":
                    cadenaConexion = @"Data Source=" + ServidorBDD + ";Initial Catalog=" + nombreBDD + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Password + "";
                    break;
                case "SQL-SERVER":
                    cadenaConexion = @"Data Source=" + ServidorBDD + ";Initial Catalog=" + nombreBDD + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Password + "";
                    break;
                case "POSTGRESQL":
                    cadenaConexion = @"Data Source=" + ServidorBDD + ";Initial Catalog=" + nombreBDD + ";Persist Security Info=True;User ID=" + Usuario + ";Password=" + Password + "";
                    break;
                case "ORACLE":
                    cadenaConexion = @" User Id=" + usuario + "; Password=" + password + "; Data Source=(DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = " + servidorBDD + ")(PORT = " + puertoBDD + "))(LOAD_BALANCE = yes)(CONNECT_DATA = (SERVER = DEDICATED) (SERVICE_NAME = " + serviceNameBDD + ")));";
                    break;
            }
            detalleError = "";
            pathReporte = ConfigurationManager.AppSettings["PathReportes"];
        }
        #endregion

        #region [Métodos]
        //Métodos
        public string EjecutarSentencia(string sentenciaSQL)
        {
            #region [Ejecutar una sentencia Sql]
            try
            {
                if (GestorBDD == "SQL-EXPRESS" || GestorBDD == "SQL-SERVER")
                {
                    SqlConnection conexion = new SqlConnection(cadenaConexion);
                    SqlCommand comando = conexion.CreateCommand();
                    try
                    {
                        conexion.Open();
                        comando.CommandText = sentenciaSQL;
                        comando.ExecuteNonQuery();
                        return "ok";
                    }
                    catch (SqlException e)
                    {
                        detalleError = MensajeError(e.Number, e.Message);
                        return detalleError;
                    }
                    finally
                    {
                        conexion.Close();
                    }
                }
                else if (GestorBDD == "POSTGRESQL")
                {
                    NpgsqlConnection conexion = new NpgsqlConnection(cadenaConexion);
                    NpgsqlCommand comando = conexion.CreateCommand();
                    try
                    {
                        conexion.Open();
                        comando.CommandText = sentenciaSQL;
                        comando.ExecuteNonQuery();
                        return "ok";
                    }
                    catch (NpgsqlException e)
                    {
                        detalleError = MensajeError(e.ErrorCode, e.Message);
                        return detalleError;
                    }
                    finally
                    {
                        conexion.Close();
                    }
                }
                else if (GestorBDD == "ORACLE")
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
                else if (GestorBDD == "MYSQL")
                {
                    MySqlConnection conexion = new MySqlConnection(cadenaConexion);
                    MySqlCommand comando = conexion.CreateCommand();
                    try
                    {
                        conexion.Open();
                        comando.CommandText = sentenciaSQL;
                        comando.ExecuteNonQuery();
                        return "ok";
                    }
                    catch (MySqlException e)
                    {
                        detalleError = MensajeError(e.Number, e.Message);
                        return detalleError;
                    }
                    finally
                    {
                        conexion.Close();
                    }
                }
                else
                    return "Datos: Gestor no valido";
            }
            catch (Exception ex)
            {
                return "Datos: " + ex.Message;
            }
            #endregion
        }
        public string EjecutarSentenciaTransaccion(string sentenciaSQL)
        {
            #region [Ejecutar una sentencia Sql en transacción]
            try
            {
                if (GestorBDD == "SQL-EXPRESS" || GestorBDD == "SQL-SERVER")
                {
                    SqlConnection conexion = new SqlConnection(cadenaConexion);
                    SqlCommand comando = conexion.CreateCommand();
                    conexion.Open();
                    SqlTransaction sqlTran = conexion.BeginTransaction();
                    try
                    {
                        comando.CommandText = sentenciaSQL;
                        comando.Transaction = sqlTran;
                        comando.ExecuteNonQuery();
                        sqlTran.Commit();
                        return "ok";
                    }
                    catch (SqlException e)
                    {
                        try
                        {
                            detalleError = MensajeError(e.Number, e.Message);
                            sqlTran.Rollback();
                        }
                        catch (SqlException exRollback)
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
                else if (GestorBDD == "POSTGRESQL")
                {
                    NpgsqlConnection conexion = new NpgsqlConnection(cadenaConexion);
                    NpgsqlCommand comando = conexion.CreateCommand();
                    conexion.Open();
                    NpgsqlTransaction sqlTran = conexion.BeginTransaction();
                    try
                    {
                        comando.CommandText = sentenciaSQL;
                        comando.Transaction = sqlTran;
                        comando.ExecuteNonQuery();
                        sqlTran.Commit();
                        return "ok";
                    }
                    catch (NpgsqlException e)
                    {
                        try
                        {
                            detalleError = MensajeError(e.ErrorCode, e.Message);
                            sqlTran.Rollback();
                        }
                        catch (NpgsqlException exRollback)
                        {
                            detalleError = MensajeError(exRollback.ErrorCode, e.Message);
                        }
                        return detalleError;
                    }
                    finally
                    {
                        conexion.Close();
                    }
                }
                else if (GestorBDD == "ORACLE")
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
                else if (GestorBDD == "MYSQL")
                {
                    MySqlConnection conexion = new MySqlConnection(cadenaConexion);
                    MySqlCommand comando = conexion.CreateCommand();
                    conexion.Open();
                    MySqlTransaction sqlTran = conexion.BeginTransaction();
                    try
                    {
                        comando.CommandText = sentenciaSQL;
                        comando.Transaction = sqlTran;
                        comando.ExecuteNonQuery();
                        sqlTran.Commit();
                        return "ok";
                    }
                    catch (MySqlException e)
                    {
                        try
                        {
                            detalleError = MensajeError(e.Number, e.Message);
                            sqlTran.Rollback();
                        }
                        catch (MySqlException exRollback)
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
                else
                    return "Datos: Gestor no valido";
            }
            catch (Exception ex)
            {
                return "Datos: " + ex.Message;
            }
            #endregion
        }
        public string EjecutarSentenciaTransaccionScalar(string sentenciaSQL, out string valor)
        {
            #region [Ejecutar una sentencia Sql en transacción y se obtiene el id creado]
            valor = string.Empty;
            try
            {
                if (GestorBDD == "SQL-EXPRESS" || GestorBDD == "SQL-SERVER")
                {
                    SqlConnection conexion = new SqlConnection(cadenaConexion);
                    SqlCommand comando = conexion.CreateCommand();
                    conexion.Open();
                    SqlTransaction sqlTran = conexion.BeginTransaction();
                    try
                    {
                        comando.CommandText = sentenciaSQL;
                        comando.Transaction = sqlTran;
                        valor = comando.ExecuteScalar().ToString();
                        sqlTran.Commit();
                        return "ok";
                    }
                    catch (SqlException e)
                    {
                        try
                        {
                            detalleError = MensajeError(e.Number, e.Message);
                            sqlTran.Rollback();
                        }
                        catch (SqlException exRollback)
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
                else if (GestorBDD == "POSTGRESQL")
                {
                    NpgsqlConnection conexion = new NpgsqlConnection(cadenaConexion);
                    NpgsqlCommand comando = conexion.CreateCommand();
                    conexion.Open();
                    NpgsqlTransaction sqlTran = conexion.BeginTransaction();
                    try
                    {
                        comando.CommandText = sentenciaSQL;
                        comando.Transaction = sqlTran;
                        valor = comando.ExecuteScalar().ToString();
                        sqlTran.Commit();
                        return "ok";
                    }
                    catch (NpgsqlException e)
                    {
                        try
                        {
                            detalleError = MensajeError(e.ErrorCode, e.Message);
                            sqlTran.Rollback();
                        }
                        catch (NpgsqlException exRollback)
                        {
                            detalleError = MensajeError(exRollback.ErrorCode, e.Message);
                        }
                        return detalleError;
                    }
                    finally
                    {
                        conexion.Close();
                    }
                }
                else if (GestorBDD == "ORACLE")
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
                else if (GestorBDD == "MYSQL")
                {
                    MySqlConnection conexion = new MySqlConnection(cadenaConexion);
                    MySqlCommand comando = conexion.CreateCommand();
                    conexion.Open();
                    MySqlTransaction sqlTran = conexion.BeginTransaction();
                    try
                    {
                        comando.CommandText = sentenciaSQL;
                        comando.Transaction = sqlTran;
                        valor = comando.ExecuteScalar().ToString();
                        sqlTran.Commit();
                        return "ok";
                    }
                    catch (MySqlException e)
                    {
                        try
                        {
                            detalleError = MensajeError(e.Number, e.Message);
                            sqlTran.Rollback();
                        }
                        catch (MySqlException exRollback)
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
                else
                    return "Datos: Gestor no valido";
            }
            catch (Exception ex)
            {
                return "Datos: " + ex.Message;
            }
            #endregion
        }
        public string EjecutarComando(string sentenciaSQL, out string valor)
        {
            #region [Ejecutar el comando y obtener el valor]
            valor = string.Empty;
            try
            {
                if (GestorBDD == "SQL-EXPRESS" || GestorBDD == "SQL-SERVER")
                {
                    SqlConnection conexion = new SqlConnection(cadenaConexion);
                    SqlCommand comando = conexion.CreateCommand();
                    try
                    {
                        conexion.Open();
                        comando.CommandText = sentenciaSQL;
                        valor = comando.ExecuteScalar().ToString();
                        return "ok";
                    }
                    catch (SqlException e)
                    {
                        return MensajeError(e.Number, e.Message);
                    }
                    catch (Exception ex)
                    {
                        return "Base datos: " + ex.Message;
                    }
                    finally
                    {
                        conexion.Close();
                    }
                }
                else if (GestorBDD == "POSTGRESQL")
                {
                    NpgsqlConnection conexion = new NpgsqlConnection(cadenaConexion);
                    NpgsqlCommand comando = conexion.CreateCommand();
                    try
                    {
                        conexion.Open();
                        comando.CommandText = sentenciaSQL;
                        valor = comando.ExecuteScalar().ToString();
                        return "ok";
                    }
                    catch (NpgsqlException e)
                    {
                        return MensajeError(e.ErrorCode, e.Message);
                    }
                    catch (Exception ex)
                    {
                        return "Base datos: " + ex.Message;
                    }
                    finally
                    {
                        conexion.Close();
                    }
                }
                else if (GestorBDD == "ORACLE")
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
                        return "Base datos: " + ex.Message;
                    }
                    finally
                    {
                        conexion.Close();
                    }
                }
                else if (GestorBDD == "MYSQL")
                {
                    MySqlConnection conexion = new MySqlConnection(cadenaConexion);
                    MySqlCommand comando = conexion.CreateCommand();
                    try
                    {
                        conexion.Open();
                        comando.CommandText = sentenciaSQL;
                        valor = comando.ExecuteScalar().ToString();
                        return "ok";
                    }
                    catch (MySqlException e)
                    {
                        return MensajeError(e.Number, e.Message);
                    }
                    catch (Exception ex)
                    {
                        return "Base datos: " + ex.Message;
                    }
                    finally
                    {
                        conexion.Close();
                    }
                }
                else
                    return "Datos: Gestor no valido";
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
            return "Base de datos: " + respuesta;
            #endregion
        }
        public string EjecutarConsultaTablas(string SentenciaSQL, out DataSet datos)
        {
            #region [Consultar tablas y llenar dataset]
            datos = new DataSet();
            try
            {
                if (GestorBDD == "SQL-EXPRESS" || GestorBDD == "SQL-SERVER")
                {
                    SqlConnection conexion = new SqlConnection(cadenaConexion);

                    try
                    {
                        conexion.Open();
                        SqlDataAdapter adaptador = new SqlDataAdapter(SentenciaSQL, conexion);
                        adaptador.Fill(datos);
                        return "ok";
                    }
                    catch (SqlException e)
                    {
                        return MensajeError(e.Number, e.Message);
                    }
                    finally
                    {
                        conexion.Close();
                    }
                }
                else if (GestorBDD == "POSTGRESQL")
                {
                    NpgsqlConnection conexion = new NpgsqlConnection(cadenaConexion);

                    try
                    {
                        conexion.Open();
                        NpgsqlDataAdapter adaptador = new NpgsqlDataAdapter(SentenciaSQL, conexion);
                        adaptador.Fill(datos);
                        return "ok";
                    }
                    catch (NpgsqlException e)
                    {
                        return MensajeError(e.ErrorCode, e.Message);
                    }
                    finally
                    {
                        conexion.Close();
                    }
                }
                else if (GestorBDD == "ORACLE")
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
                else if (GestorBDD == "MYSQL")
                {
                    MySqlConnection conexion = new MySqlConnection(cadenaConexion);

                    try
                    {
                        conexion.Open();
                        MySqlDataAdapter adaptador = new MySqlDataAdapter(SentenciaSQL, conexion);
                        adaptador.Fill(datos);
                        return "ok";
                    }
                    catch (MySqlException e)
                    {
                        return MensajeError(e.Number, e.Message);
                    }
                    finally
                    {
                        conexion.Close();
                    }
                }
                else
                    return "Datos: Gestor no valido";
            }
            catch (Exception ex)
            {
                return "Datos: " + ex.Message;
            }
            #endregion
        }
        public string EjecutarConsulta(string SentenciaSQL, out DataTable respuesta)
        {
            #region [Ejecuta la consulta y obtiene la tabla resultado]
            respuesta = new DataTable();
            try
            {
                if (GestorBDD == "SQL-EXPRESS" || GestorBDD == "SQL-SERVER")
                {
                    SqlConnection conexion = new SqlConnection(cadenaConexion);

                    try
                    {
                        conexion.Open();
                        SqlDataAdapter adaptador = new SqlDataAdapter(SentenciaSQL, conexion);
                        adaptador.Fill(respuesta);
                        return "ok";
                    }
                    catch (SqlException e)
                    {
                        return MensajeError(e.Number, e.Message);
                    }
                    finally
                    {
                        conexion.Close();
                    }
                }
                else if (GestorBDD == "POSTGRESQL")
                {
                    NpgsqlConnection conexion = new NpgsqlConnection(cadenaConexion);

                    try
                    {
                        conexion.Open();
                        NpgsqlDataAdapter adaptador = new NpgsqlDataAdapter(SentenciaSQL, conexion);
                        adaptador.Fill(respuesta);
                        return "ok";
                    }
                    catch (NpgsqlException e)
                    {
                        return MensajeError(e.ErrorCode, e.Message);
                    }
                    finally
                    {
                        conexion.Close();
                    }
                }
                else if (GestorBDD == "ORACLE")
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
                        return MensajeError(e.Number, e.Message);
                    }
                    finally
                    {
                        conexion.Close();
                    }
                }
                else if (GestorBDD == "MYSQL")
                {
                    MySqlConnection conexion = new MySqlConnection(cadenaConexion);

                    try
                    {
                        conexion.Open();
                        MySqlDataAdapter adaptador = new MySqlDataAdapter(SentenciaSQL, conexion);
                        adaptador.Fill(respuesta);
                        return "ok";
                    }
                    catch (MySqlException e)
                    {
                        return MensajeError(e.Number, e.Message);
                    }
                    finally
                    {
                        conexion.Close();
                    }
                }
                else
                    return "Datos: Gestor no valido";
            }
            catch (Exception ex)
            {
                return "Datos: " + ex.Message;
            }
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

            if (GestorBDD == "MYSQL")
                respuesta = anio + "-" + mes + "-" + dia + " " + hora + ":" + minuto + ":" + segundo;
            else
                respuesta = anio + "" + mes + "" + dia + " " + hora + ":" + minuto + ":" + segundo;

            return respuesta;
            #endregion
        }
        public string FormatoFechaBDD(DateTime fecha)
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

            switch (GestorBDD)
            {
                case "MYSQL": respuesta = anio + "" + mes + "" + dia; break;
                case "SQL-EXPRESS": respuesta = anio + "" + mes + "" + dia + " " + hora + ":" + minuto + ":" + segundo; break;
                case "SQL-SERVER": respuesta = anio + "" + mes + "" + dia + " " + hora + ":" + minuto + ":" + segundo; break;
                case "POSTGRESQL": respuesta = anio + "" + mes + "" + dia + " " + hora + ":" + minuto + ":" + segundo; break;
                case "ORACLE": respuesta = anio + "" + mes + "" + dia + " " + hora + ":" + minuto + ":" + segundo; break;
            }

            return respuesta;
            #endregion
        }
        public string Sysdate()
        {
            #region [Formato sysdate para cada BDD]
            string respuesta = "";
            switch (GestorBDD)
            {
                case "MYSQL": respuesta = "SYSDATE()"; break;
                case "SQL-EXPRESS": respuesta = "GETDATE()"; break;
                case "SQL-SERVER": respuesta = "GETDATE()"; break;
                case "POSTGRESQL": respuesta = "SYSDATE()"; break;
                case "ORACLE": respuesta = "SYSDATE()"; break;
            }
            return respuesta;
            #endregion
        }
        public string CantidadRegistros()
        {
            #region [Formato sysdate para cada BDD]
            string respuesta = "";
            switch (GestorBDD)
            {
                case "MYSQL": respuesta = " limit 10 "; break;
                case "SQL-EXPRESS": respuesta = " rownum = 10 "; break;
                case "SQL-SERVER": respuesta = " rownum = 10 "; break;
                case "POSTGRESQL": respuesta = " rownum = 10 "; break;
                case "ORACLE": respuesta = " rownum = 10 "; break;
            }
            return respuesta;
            #endregion
        }
        #endregion
    }
}
