using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Conexion
{
    public class ConnectionDB : IDisposable
    {
        SqlConnection _connectionSql;
        SqlTransaction _transactionSql;
        SqlCommand _commandSql;
        List<SqlParameter> _parameterList;

        public string StoredProcedureName { get; set; }
        public List<SqlParameter> Parameters
        {
            get { return _parameterList; }
            set { _parameterList = value; }
        }

        /// <summary>
        /// Crea la conexion a la Base de Datos Sql
        /// </summary>
        /// <param name="connectionString">Cadena de conexión</param>
        public ConnectionDB()
        {
            try
            {
                //_connectionSql = new SqlConnection("Data Source=.\\SQLEXPRESS; Initial Catalog=ProyectoDB; Trusted_Connection=Yes");
                _connectionSql = new SqlConnection("Data Source=185.100.15.212; Initial Catalog=ProyectoDB; User Id=sa;Password=syssql;");
                _parameterList = new List<SqlParameter>();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear la conexión", ex);
            }
        }


        /// <summary>
        /// Crea la conexion a la Base de Datos Sql
        /// </summary>
        /// <param name="connectionString">Cadena de conexión</param>
        public ConnectionDB(string connectionString)
        {
            try
            {
                _connectionSql = new SqlConnection(connectionString);
                _parameterList = new List<SqlParameter>();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear la conexión", ex);
            }
        }
        /// <summary>
        /// Crea la conexion a la Base de Datos Sql
        /// </summary>
        /// <param name="_servidor">Nombre del servidor</param>
        /// <param name="_baseDeDatos">Nombre de la base de datos</param>
        /// <param name="_usuario">Nombre del usuario de sql</param>
        /// <param name="_pass">Contraseña del usuario de sql</param>
        public ConnectionDB(string serverName, string databaseName, string userName = "", string pass = "")
        {
            try
            {
                string connectionString;
                if (userName.Trim() == String.Empty)
                {
                    connectionString = $"Data Source={serverName}; Initial Catalog={databaseName}; Trusted_Connection=True;";
                }
                else
                {
                    connectionString = $"Data Source={serverName}; Initial Catalog={databaseName}; user id={userName}; password={pass};";
                }

                _connectionSql = new SqlConnection(connectionString);
                _parameterList = new List<SqlParameter>();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear la conexión", ex);
            }
        }

        /// <summary>
        /// Abre la conexion para trabajar con la base de datos
        /// </summary>
        public void OpenConnection()
        {
            if (_connectionSql.State == System.Data.ConnectionState.Closed)
            {
                _connectionSql.Open();
            }
        }
        /// <summary>
        /// Cierra la conexion a la base de datos
        /// </summary>
        public void CloseConnection()
        {
            if (_connectionSql.State == System.Data.ConnectionState.Open)
            {
                _connectionSql.Close();
            }
        }
        /// <summary>
        /// Comienza una transacción a la base de datos
        /// </summary>
        public void BeginTransaction()
        {
            _transactionSql = _connectionSql.BeginTransaction();
        }
        /// <summary>
        /// Termina la transacción a la base de datos
        /// </summary>
        public void CommitTransaction()
        {
            _transactionSql.Commit();
        }
        /// <summary>
        /// Regresa la transacción pendiente y devuelve los cambios hechos en la base de datos
        /// </summary>
        public void RollBackTransaction()
        {
            _transactionSql.Rollback();
        }
        /// <summary>
        /// Limpiar la lista de parametros de sql
        /// </summary>
        public void CleanParameters()
        {
            _parameterList.Clear();
        }
        /// <summary>
        /// Agrega un parametro sql a la lista de parametros
        /// </summary>
        /// <param name="parameterName">Nombre del parametro</param>
        /// <param name="dbType">Tipo de dato del parametro</param>
        /// <param name="value">Valor del parametro</param>
        public void AddParameter(string parameterName, SqlDbType dbType, object value,
                                 ParameterDirection direction = ParameterDirection.Input)
        {
            SqlParameter parameter = new SqlParameter(parameterName, dbType);
            parameter.Value = value;
            parameter.Direction = direction;
            _parameterList.Add(parameter);
        }
        /// <summary>
        /// Agrega un parametro sql a la lista de parametros
        /// </summary>
        /// <param name="parameterName">Nombre del parametro</param>
        /// <param name="dbType">Tipo de dato del parametro</param>
        /// <param name="size">Tamaño del dato del parametro</param>
        /// <param name="value">Valor del parametro</param>
        public void AddParameter(string parameterName, SqlDbType dbType, int size, object value,
                                 ParameterDirection direction = ParameterDirection.Input)
        {
            SqlParameter parameter = new SqlParameter(parameterName, dbType, size);
            parameter.Value = value;
            parameter.Direction = direction;
            _parameterList.Add(parameter);
        }

        /// <summary>
        /// Obtiene el valor del parametro buscando por su nombre de la lista de parametros
        /// </summary>
        /// <param name="parameterName">Nombre del parametro</param>
        public object GetParameterValue(string parameterName)
        {
            return _commandSql.Parameters[parameterName].Value;
        }

        /// <summary>
        /// Ejecuta el procedimiento almacenado
        /// </summary>
        /// <param name="storedProcedureName">Nombre del procedimiento almacenado</param>
        public void ExecuteNonQuery(int timeOut = 0)
        {
            try
            {
                _commandSql = _connectionSql.CreateCommand();
                if (_transactionSql != null)
                {
                    _commandSql.Transaction = _transactionSql;
                }

                _commandSql.CommandType = System.Data.CommandType.StoredProcedure;
                _commandSql.CommandText = StoredProcedureName;
                if (timeOut > 0)
                {
                    _commandSql.CommandTimeout = timeOut;
                }
                foreach (SqlParameter param in _parameterList)
                {
                    _commandSql.Parameters.Add(param);
                }

                _commandSql.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        /// <summary>
        /// Ejecuta el procedimiento almacenado que devuelve filas
        /// </summary>
        /// <param name="storedProcedureName">Nombre del procedimiento almacenado</param>
        /// <param name="dtResult">Datatable en el que se devolverá el resultado del procedimiento almacenado</param>
        public void ExecuteQuery(out DataTable dtResult, int timeOut = 0)
        {
            try
            {
                _commandSql = _connectionSql.CreateCommand();
                _commandSql.CommandType = System.Data.CommandType.StoredProcedure;
                _commandSql.CommandText = StoredProcedureName;
                if (timeOut > 0)
                {
                    _commandSql.CommandTimeout = timeOut;
                }
                foreach (SqlParameter param in _parameterList)
                {
                    _commandSql.Parameters.Add(param);
                }

                SqlDataAdapter adapter = new SqlDataAdapter(_commandSql);
                dtResult = new DataTable();
                adapter.Fill(dtResult);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// Ejecuta el procedimiento almacenado que devuelve filas
        /// </summary>
        /// <param name="storedProcedureName">Nombre del procedimiento almacenado</param>
        /// <param name="dtConsulta">Datatable en el que se devolverá el resultado del procedimiento almacenado</param>
        public void ExecuteQuery(out DataSet dsResult, int timeOut = 0)
        {
            try
            {
                _commandSql = _connectionSql.CreateCommand();
                _commandSql.CommandType = System.Data.CommandType.StoredProcedure;
                _commandSql.CommandText = StoredProcedureName;
                if (timeOut > 0)
                {
                    _commandSql.CommandTimeout = timeOut;
                }

                foreach (SqlParameter param in _parameterList)
                {
                    _commandSql.Parameters.Add(param);
                }

                SqlDataAdapter adapter = new SqlDataAdapter(_commandSql);
                dsResult = new DataSet();
                adapter.Fill(dsResult);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        #region IDisposable Support
        private bool disposedValue = false; // Para detectar llamadas redundantes

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: elimine el estado administrado (objetos administrados).
                }

                // TODO: libere los recursos no administrados (objetos no administrados) y reemplace el siguiente finalizador.
                // TODO: configure los campos grandes en nulos.

                disposedValue = true;
            }
        }

        // TODO: reemplace un finalizador solo si el anterior Dispose(bool disposing) tiene código para liberar los recursos no administrados.
        // ~ConnectionDB() {
        //   // No cambie este código. Coloque el código de limpieza en el anterior Dispose(colocación de bool).
        //   Dispose(false);
        // }

        // Este código se agrega para implementar correctamente el patrón descartable.
        void IDisposable.Dispose()
        {
            // No cambie este código. Coloque el código de limpieza en el anterior Dispose(colocación de bool).
            Dispose(true);
            // TODO: quite la marca de comentario de la siguiente línea si el finalizador se ha reemplazado antes.
            // GC.SuppressFinalize(this);
        }
        #endregion

    }

}
