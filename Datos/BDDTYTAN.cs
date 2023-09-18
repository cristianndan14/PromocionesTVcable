using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using Dato;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;

namespace Datos
{
    public class BDDTYTAN 
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
        public BDDTYTAN()
        {
            servidorBDD = ConfigurationManager.AppSettings["servidorBDDTytan"];
            nombreBDD = ConfigurationManager.AppSettings["nombreBDDTytan"];
            usuario = ConfigurationManager.AppSettings["usuarioBDDTytan"];
            password = ConfigurationManager.AppSettings["passwordBDDTytan"];
            puertoBDD = ConfigurationManager.AppSettings["puertoBDDTytan"];
            serviceNameBDD = ConfigurationManager.AppSettings["serviceNameBDDTytan"];

            cadenaConexion = @" User Id="+ usuario + "; Password="+ password + "; Data Source=(DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = "+ servidorBDD + ")(PORT = "+ puertoBDD + "))(LOAD_BALANCE = yes)(CONNECT_DATA = (SERVER = DEDICATED) (SERVICE_NAME = "+ serviceNameBDD + ")));";
        }
        #endregion

        #region [Métodos]
        //Métodos
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
                return "Datos Tytan: "+ex.Message;
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
                    return "Datos Tytan: " + ex.Message;
                }
                finally
                {
                    conexion.Close();
                }
            }
            catch (Exception ex)
            {
                return "Datos Tytan: "+ex.Message;
            }
            #endregion
        }

        public string ArmarSentencia(string sql)
        {
            string sentencia = string.Empty;
            sentencia += "DECLARE " +
            "ln_precio varchar(10); " +
            "Begin " +
            "" + sql + "" +
            "End; ";
            return sentencia;
        }

        public string EjecutarSentenciaDBMS(string sentenciaSQL, out string respuesta)
        {
            #region [Ejecutar una sentencia ORACLE]            
            try
            {                
                    using (OracleConnection conexion = new OracleConnection(cadenaConexion))
                    {
                        using (OracleCommand comando = conexion.CreateCommand())
                        {
                            try
                            {
                                conexion.Open();
                                comando.CommandText = "BEGIN DBMS_OUTPUT.ENABLE(NULL); END;";
                                comando.CommandType = CommandType.Text;
                                comando.CommandTimeout = 350;
                                comando.ExecuteNonQuery();


                                comando.CommandText = sentenciaSQL;
                                var res = comando.ExecuteNonQuery();
                                comando.CommandText = "BEGIN DBMS_OUTPUT.GET_LINES(:outString, :numLines); END;";
                                comando.CommandType = CommandType.Text;

                                comando.Parameters.Clear();

                                comando.Parameters.Add(new OracleParameter("outString", OracleDbType.Varchar2, int.MaxValue, ParameterDirection.Output));
                                comando.Parameters["outString"].CollectionType = OracleCollectionType.PLSQLAssociativeArray;
                                comando.Parameters["outString"].Size = sentenciaSQL.Length;
                                comando.Parameters["outString"].ArrayBindSize = new int[sentenciaSQL.Length];

                                // set bind size for each array element
                                for (int i = 0; i < sentenciaSQL.Length; i++)
                                {
                                    comando.Parameters["outString"].ArrayBindSize[i] = 32000;
                                }
                                comando.Parameters.Add(new OracleParameter("numLines", OracleDbType.Int32, ParameterDirection.InputOutput));
                                comando.Parameters["numLines"].Value = 10; // Get 10 lines
                                comando.ExecuteNonQuery();

                                int numLines = Convert.ToInt32(comando.Parameters["numLines"].Value.ToString());
                                string outString = string.Empty;

                                // Try to get more lines until there are zero left
                                while (numLines > 0)
                                {
                                    for (int i = 0; i < numLines; i++)
                                    {
                                        // use proper indexing here
                                        OracleString s = ((OracleString[])comando.Parameters["outString"].Value)[i];
                                        outString += s.ToString();
                                    }

                                    comando.ExecuteNonQuery();
                                    numLines = Convert.ToInt32(comando.Parameters["numLines"].Value.ToString());
                                }
                                respuesta = outString;
                                return "ok";
                            }
                            catch (OracleException e)
                            {
                                //detalleError = MensajeError(e.Number, e.Message);
                                respuesta = e.ToString();
                                return detalleError;
                            }
                            finally
                            {
                                conexion.Close();
                            }
                        }
                    }
                
                respuesta = "No hay esquema";
                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta = ex.ToString();
                return "Datos: " + ex.Message;
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
                return "Datos Tytan: " + ex.Message;
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
                return "Datos Tytan.EjecutarConsulta: " + ex.Message;
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
            return "Base de datos Tytan: " + respuesta;
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
        public string EjecutarProcedimientoAlmacenadoOracle(ProcedimientoAlmacenadoOracle procedimientoAlmacenadoOracle)
        {
            #region [Ejecutar procedimiento almacenado en oracle]
            OracleCommand oCmd = new OracleCommand();
            oCmd.CommandText = procedimientoAlmacenadoOracle.ComandText;
            oCmd.CommandType = CommandType.StoredProcedure;
            oCmd.Parameters.Clear();

            foreach (ParametroOracle oracleParameter in procedimientoAlmacenadoOracle.Parameters)
            {
                oCmd.Parameters.Add(oracleParameter.Nombre, oracleParameter.Tipo).Value = oracleParameter.Valor;
            }
            OracleConnection oCon = new OracleConnection();
            oCon.ConnectionString = cadenaConexion;
            oCmd.Connection = oCon;

            try
            {
                oCon.Open();
                oCmd.ExecuteNonQuery();
                oCon.Close();
                oCon.Dispose();
                oCmd.Dispose();
                return "OK";

            }
            catch (OracleException e)
            {
                return MensajeError(e.ErrorCode, e.Message);
            }
            catch (Exception ex)
            {
                return "Datos Tytan: " + ex.Message;
            }
            finally
            {
                oCon.Close();
                oCon.Dispose();
                oCmd.Dispose();
            }
            #endregion
        }
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
                return "Datos: " + ex.Message;
            }
            #endregion
        }

        #endregion
    }
}
