using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace Web.Data
{
    public interface IDapperManager
    {
        /// <summary>
        /// Executes a parametrized query, returning the data typed as T.
        /// Manages connection internally.
        /// </summary>
        /// <typeparam name="T">The type of results to return.</typeparam>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="transaction">The transaction to use, if any.</param>
        /// <param name="buffered">Whether to buffer results in memory.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <returns>
        /// A sequence of data of the supplied type; if a basic type (int, string, etc) is
        /// queried then the data from the first column in assumed, otherwise an instance
        /// is created per row, and a direct column-name===member-name mapping is assumed
        /// (case insensitive).
        /// </returns>
        IEnumerable<T> Query<T>(string sql, object param = null, IDbTransaction transaction = null,
            bool buffered = true, int? commandTimeout = null, CommandType? commandType = null);
        /// <summary>
        /// Executes a parametrized query, returning the data typed as type.
        /// Works with externally managed connection
        /// </summary>
        /// <typeparam name="T">The type of results to return.</typeparam>
        /// <param name="dbConnection">The connection to execute on.</param>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="transaction">The transaction to use, if any.</param>
        /// <param name="buffered"> Whether to buffer results in memory.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <returns>
        /// A sequence of data of the supplied type; if a basic type (int, string, etc) is
        /// queried then the data from the first column in assumed, otherwise an instance
        /// is created per row, and a direct column-name===member-name mapping is assumed
        /// (case insensitive).
        /// </returns>
        IEnumerable<T> Query<T>(IDbConnection dbConnection, string sql, object param = null, IDbTransaction transaction = null,
            bool buffered = true, int? commandTimeout = null, CommandType? commandType = null);
        /// <summary>
        /// Executes parametrized query, returning the data typed as type.
        /// Manages connection internally.
        /// </summary>
        /// <param name="type">The type to return</param>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="transaction">The transaction to use, if any.</param>
        /// <param name="buffered"> Whether to buffer results in memory.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <returns>
        /// A sequence of data of the supplied type; if a basic type (int, string, etc) is
        /// queried then the data from the first column in assumed, otherwise an instance
        /// is created per row, and a direct column-name===member-name mapping is assumed
        /// (case insensitive).
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException:"
        /// type is null.
        /// </exception>
        IEnumerable<object> Query(Type type, string sql, object param = null,
            IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null);
        /// <summary>
        /// Executes a parametrized query, returning the data typed as type.
        /// Works with externally managed connection
        /// </summary>
        /// <param name="dbConnection">The connection to execute on.</param>
        /// <param name="type">The type to return</param>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="transaction">The transaction to use, if any.</param>
        /// <param name="buffered"> Whether to buffer results in memory.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <returns>
        /// A sequence of data of the supplied type; if a basic type (int, string, etc) is
        /// queried then the data from the first column in assumed, otherwise an instance
        /// is created per row, and a direct column-name===member-name mapping is assumed
        /// (case insensitive).
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException:"
        /// type is null.
        /// </exception>
        IEnumerable<object> Query(IDbConnection dbConnection, Type type, string sql, object param = null, IDbTransaction transaction = null,
            bool buffered = true, int? commandTimeout = null, CommandType? commandType = null);

        /// <summary>
        /// Execute parameterized SQL.
        /// Manages connection internally.
        /// </summary>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="transaction">The transaction to use, if any.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <returns>The number of rows affected.</returns>
        int Execute(string sql, object param = null, IDbTransaction transaction = null,
            int? commandTimeout = null, CommandType? commandType = null);
        /// <summary>
        /// Execute parameterized SQL.
        /// Manages connection internally. Should be used only by the deploymentScripts
        /// </summary>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="transaction">The transaction to use, if any.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <returns>The number of rows affected.</returns>
        int ExecuteDeployment(string sql, object param = null, IDbTransaction transaction = null,
            int? commandTimeout = null, CommandType? commandType = null);
        /// <summary>
        /// Execute parameterized SQL.
        /// Works with externally managed connection
        /// </summary>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="transaction">The transaction to use, if any.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <returns>The number of rows affected.</returns>
        int Execute(IDbConnection dbConnection, string sql, object param = null, IDbTransaction transaction = null,
            int? commandTimeout = null, CommandType? commandType = null);
        /// <summary>
        /// Executes a single-row query, returning the data typed as type.
        /// Works with externally managed connection
        /// </summary>
        /// <param name="dbConnection">The connection to execute on.</param>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="transaction">The transaction to use, if any.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <returns>An System.Data.IDataReader that can be used to iterate over the results of the SQL query.</returns>
        /// <remarks>
        /// This is typically used when the results of a query are not processed by Dapper,
        /// for example, used to fill a System.Data.DataTable or DataSet.
        /// </remarks>
        IDataReader ExecuteReader(IDbConnection dbConnection, string sql, object param = null, IDbTransaction transaction = null,
            int? commandTimeout = null, CommandType? commandType = null);
        /// <summary>
        /// Execute parameterized SQL that selects a single value.
        /// Manages connection internally.
        /// </summary>
        /// <typeparam name="T">The type of results to return.</typeparam>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="transaction">The transaction to use, if any.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <returns>The first cell returned, as T.</returns>
        T ExecuteScalar<T>(string sql, object param = null, IDbTransaction transaction = null,
           int? commandTimeout = null, CommandType? commandType = null);
        /// <summary>
        /// Execute parameterized SQL that selects a single value.
        /// Works with externally managed connection
        /// </summary>
        /// <typeparam name="T">The type of results to return.</typeparam>
        /// <param name="dbConnection">The connection to execute on.</param>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="transaction">The transaction to use, if any.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <returns>The first cell returned, as T.</returns>
        T ExecuteScalar<T>(IDbConnection dbConnection, string sql, object param = null, IDbTransaction transaction = null,
            int? commandTimeout = null, CommandType? commandType = null);
        /// <summary>
        /// Executes a single-row query, returning the data typed as T.
        /// Manages connection internally.
        /// </summary>
        /// <typeparam name="T">The type of results to return.</typeparam>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="transaction">The transaction to use, if any.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <returns>
        /// A first instance of the supplied type; if a basic type (int, string,
        /// etc) is queried then the data from the first column in assumed, otherwise an
        /// instance is created per row, and a direct column-name===member-name mapping is
        /// assumed (case insensitive).
        /// </returns>
        T QueryFirst<T>(string sql, object param = null, IDbTransaction transaction = null,
            int? commandTimeout = null, CommandType? commandType = null);
        /// <summary>
        /// Executes a single-row query, returning the data typed as T.
        /// Works with externally managed connection
        /// </summary>
        /// <typeparam name="T">The type of results to return.</typeparam>
        /// <param name="dbConnection">The connection to execute on.</param>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="transaction">The transaction to use, if any.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <returns>
        /// A first instance of the supplied type; if a basic type (int, string,
        /// etc) is queried then the data from the first column in assumed, otherwise an
        /// instance is created per row, and a direct column-name===member-name mapping is
        /// assumed (case insensitive).
        /// </returns>
        T QueryFirst<T>(IDbConnection dbConnection, string sql, object param = null, IDbTransaction transaction = null,
            int? commandTimeout = null, CommandType? commandType = null);
        /// <summary>
        /// Executes a single-row query, returning the data typed as T.
        /// Manages connection internally.
        /// </summary>
        /// <typeparam name="T">The type of results to return.</typeparam>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="transaction">The transaction to use, if any.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <returns>
        /// A first instance or null of the supplied type; if a basic type (int, string,
        /// etc) is queried then the data from the first column in assumed, otherwise an
        /// instance is created per row, and a direct column-name===member-name mapping is
        /// assumed (case insensitive).
        /// </returns>
        T QueryFirstOrDefault<T>(string sql, object param = null,
            IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
        /// <summary>
        /// Executes a single-row query, returning the data typed as T.
        /// Works with externally managed connection
        /// </summary>
        /// <typeparam name="T">The type of results to return.</typeparam>
        /// <param name="dbConnection">The connection to execute on.</param>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="transaction">The transaction to use, if any.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <returns>
        /// A first instance or null of the supplied type; if a basic type (int, string,
        /// etc) is queried then the data from the first column in assumed, otherwise an
        /// instance is created per row, and a direct column-name===member-name mapping is
        /// assumed (case insensitive).
        /// </returns>
        T QueryFirstOrDefault<T>(IDbConnection dbConnection, string sql, object param = null,
            IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
        /// <summary>
        /// Executes a single-row query, returning the data typed as T.
        /// Manages connection internally.
        /// </summary>
        /// <typeparam name="T">The type of results to return.</typeparam>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="transaction">The transaction to use, if any.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <returns>
        /// A single instance of the supplied type; if a basic type (int, string,
        /// etc) is queried then the data from the first column in assumed, otherwise an
        /// instance is created per row, and a direct column-name===member-name mapping is
        /// assumed (case insensitive).
        /// </returns>
        T QuerySingle<T>(string sql, object param = null,
            IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
        /// <summary>
        /// Executes a single-row query, returning the data typed as T.
        /// Works with externally managed connection
        /// </summary>
        /// <typeparam name="T">The type of results to return.</typeparam>
        /// <param name="dbConnection">The connection to execute on.</param>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="transaction">The transaction to use, if any.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <returns>
        /// A single instance of the supplied type; if a basic type (int, string,
        /// etc) is queried then the data from the first column in assumed, otherwise an
        /// instance is created per row, and a direct column-name===member-name mapping is
        /// assumed (case insensitive).
        /// </returns>
        T QuerySingle<T>(IDbConnection dbConnection, string sql, object param = null,
            IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
        /// <summary>
        /// Executes a single-row query, returning the data typed as T.
        /// Manages connection internally.
        /// </summary>
        /// <typeparam name="T">The type of results to return.</typeparam>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="transaction">The transaction to use, if any.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <returns>
        /// A single instance of the supplied type; if a basic type (int, string,
        /// etc) is queried then the data from the first column in assumed, otherwise an
        /// instance is created per row, and a direct column-name===member-name mapping is
        /// assumed (case insensitive).
        /// </returns>
        T QuerySingleOrDefault<T>(string sql, object param = null,
            IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
        /// <summary>
        /// Executes a single-row query, returning the data typed as T.
        /// Works with externally managed connection
        /// </summary>
        /// <typeparam name="T">The type of results to return.</typeparam>
        /// <param name="dbConnection">The connection to execute on.</param>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="transaction">The transaction to use, if any.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <returns>
        /// A single instance or null of the supplied type; if a basic type (int, string,
        /// etc) is queried then the data from the first column in assumed, otherwise an
        /// instance is created per row, and a direct column-name===member-name mapping is
        /// assumed (case insensitive).
        /// </returns>
        T QuerySingleOrDefault<T>(IDbConnection dbConnection, string sql, object param = null,
            IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);

        IDatabaseManager DatabaseManager { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cnn"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> QueryAsync<T>(IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
        Task<T> QuerySingleOrDefaultAsync<T>(IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
        Task<T> QuerySingleOrDefaultAsync<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
        Task<int> ExecuteAsync(IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
        Task<int> ExecuteAsync(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
        Task<int> ExecuteScalarAsync(IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
        Task<int> ExecuteScalarAsync(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
        Task<GridReader> QueryMultipleAsync(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
        Task<GridReader> QueryMultipleAsync(IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
    }
    public class DapperManager : IDapperManager
    {
        private IDatabaseManager _databaseManager;

        public DapperManager(IDatabaseManager databaseManager)
        {
            _databaseManager = databaseManager;
        }

        public IDatabaseManager DatabaseManager
        {
            get { return _databaseManager; }
        }

        public string EnsureParameterSpecification(string sql)
        {
            return sql.Replace(':', '@');
        }

        public IEnumerable<T> Query<T>(string sql, object param = null, IDbTransaction transaction = null, bool buffered = true,
            int? commandTimeout = null, CommandType? commandType = null)
        {
            using (var dbConnection = _databaseManager.CreateDbConnection())
            {
                return dbConnection.Query<T>(EnsureParameterSpecification(sql), param, transaction, buffered, commandTimeout, commandType);
            }
        }
        public IEnumerable<T> Query<T>(IDbConnection dbConnection, string sql, object param = null,
            IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            return dbConnection.Query<T>(EnsureParameterSpecification(sql), param, transaction, buffered, commandTimeout, commandType);
        }
        public IEnumerable<object> Query(Type type, string sql, object param = null, IDbTransaction transaction = null,
            bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            using (var dbConnection = _databaseManager.CreateDbConnection())
            {
                return dbConnection.Query(type, EnsureParameterSpecification(sql), param, transaction, buffered, commandTimeout, commandType);
            }
        }
        public IEnumerable<object> Query(IDbConnection dbConnection, Type type, string sql, object param = null,
            IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            return dbConnection.Query(type, EnsureParameterSpecification(sql), param, transaction, buffered, commandTimeout, commandType);
        }
        public int Execute(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null,
            CommandType? commandType = null)
        {
            using (var dbConnection = _databaseManager.CreateDbConnection())
            {
                return dbConnection.Execute(EnsureParameterSpecification(sql), param, transaction, commandTimeout, commandType);
            }
        }
        public int ExecuteDeployment(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null,
            CommandType? commandType = null)
        {
            using (var dbConnection = _databaseManager.CreateDbConnection())
            {
                return dbConnection.Execute(sql, param, transaction, commandTimeout, commandType);
            }
        }
        public int Execute(IDbConnection dbConnection, string sql, object param = null, IDbTransaction transaction = null,
            int? commandTimeout = null, CommandType? commandType = null)
        {
            return dbConnection.Execute(EnsureParameterSpecification(sql), param, transaction, commandTimeout, commandType);
        }
        public IDataReader ExecuteReader(IDbConnection dbConnection, string sql, object param = null, IDbTransaction transaction = null,
            int? commandTimeout = null, CommandType? commandType = null)
        {
            return dbConnection.ExecuteReader(EnsureParameterSpecification(sql), param, transaction, commandTimeout, commandType);
        }
        public T ExecuteScalar<T>(string sql, object param = null, IDbTransaction transaction = null,
            int? commandTimeout = null, CommandType? commandType = null)
        {
            using (var dbConnection = _databaseManager.CreateDbConnection())
            {
                return dbConnection.ExecuteScalar<T>(EnsureParameterSpecification(sql), param, transaction,
                    commandTimeout, commandType);
            }
        }
        public T ExecuteScalar<T>(IDbConnection dbConnection, string sql, object param = null, IDbTransaction transaction = null,
            int? commandTimeout = null, CommandType? commandType = null)
        {
            return dbConnection.ExecuteScalar<T>(EnsureParameterSpecification(sql), param, transaction,
                commandTimeout, commandType);
        }
        public T QueryFirst<T>(string sql, object param = null, IDbTransaction transaction = null,
            int? commandTimeout = null, CommandType? commandType = null)
        {
            using (var dbConnection = _databaseManager.CreateDbConnection())
            {
                return dbConnection.QueryFirst<T>(EnsureParameterSpecification(sql), param, transaction, commandTimeout, commandType);
            }
        }
        public T QueryFirst<T>(IDbConnection dbConnection, string sql, object param = null, IDbTransaction transaction = null,
            int? commandTimeout = null, CommandType? commandType = null)
        {
            return dbConnection.QueryFirst<T>(EnsureParameterSpecification(sql), param, transaction, commandTimeout, commandType);
        }
        public T QueryFirstOrDefault<T>(string sql, object param = null, IDbTransaction transaction = null,
            int? commandTimeout = null, CommandType? commandType = null)
        {
            using (var dbConnection = _databaseManager.CreateDbConnection())
            {
                return dbConnection.QueryFirstOrDefault<T>(EnsureParameterSpecification(sql), param, transaction, commandTimeout, commandType);
            }
        }
        public T QueryFirstOrDefault<T>(IDbConnection dbConnection, string sql, object param = null,
            IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return dbConnection.QueryFirstOrDefault<T>(EnsureParameterSpecification(sql), param, transaction, commandTimeout, commandType);
        }
        public T QuerySingle<T>(string sql, object param = null, IDbTransaction transaction = null,
            int? commandTimeout = null, CommandType? commandType = null)
        {
            using (var dbConnection = _databaseManager.CreateDbConnection())
            {
                return dbConnection.QuerySingle<T>(EnsureParameterSpecification(sql), param, transaction, commandTimeout, commandType);
            }
        }
        public T QuerySingle<T>(IDbConnection dbConnection, string sql, object param = null,
            IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return dbConnection.QuerySingle<T>(EnsureParameterSpecification(sql), param, transaction, commandTimeout, commandType);
        }
        public T QuerySingleOrDefault<T>(string sql, object param = null, IDbTransaction transaction = null,
            int? commandTimeout = null, CommandType? commandType = null)
        {
            using (var dbConnection = _databaseManager.CreateDbConnection())
            {
                return dbConnection.QuerySingleOrDefault<T>(EnsureParameterSpecification(sql), param, transaction, commandTimeout, commandType);
            }
        }
        public T QuerySingleOrDefault<T>(IDbConnection dbConnection, string sql, object param = null,
            IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return dbConnection.QuerySingleOrDefault<T>(EnsureParameterSpecification(sql), param, transaction, commandTimeout, commandType);
        }
        public async Task<IEnumerable<T>> QueryAsync<T>(IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return await cnn.QueryAsync<T>(sql, param, transaction, commandTimeout, commandType);
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            using (var dbConnection = _databaseManager.CreateDbConnection())
            {
                return await dbConnection.QueryAsync<T>(sql, param, transaction, commandTimeout, commandType);
            }
        }
        public async Task<T> QuerySingleOrDefaultAsync<T>(IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return await cnn.QuerySingleOrDefaultAsync<T>(sql, param, transaction, commandTimeout, commandType);
        }

        public async Task<T> QuerySingleOrDefaultAsync<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            using (var dbConnection = _databaseManager.CreateDbConnection())
            {
                return await dbConnection.QuerySingleOrDefaultAsync<T>(sql, param, transaction, commandTimeout, commandType);
            }

        }
        public async Task<int> ExecuteAsync(IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return await cnn.ExecuteAsync(sql, param, transaction, commandTimeout, commandType);
        }
        public async Task<int> ExecuteAsync(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            using (var dbConnection = _databaseManager.CreateDbConnection())
            {
                return await dbConnection.ExecuteAsync(sql, param, transaction, commandTimeout, commandType);
            }
        }
        public async Task<int> ExecuteScalarAsync(IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return await cnn.ExecuteScalarAsync<int>(sql, param, transaction, commandTimeout, commandType);
        }
        public async Task<int> ExecuteScalarAsync(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            using (var dbConnection = _databaseManager.CreateDbConnection())
            {
                var id = await dbConnection.ExecuteScalarAsync<int>(sql, param, transaction, commandTimeout, commandType);
                return id;
            }
        }
        public async Task<GridReader> QueryMultipleAsync(IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return await cnn.QueryMultipleAsync(sql, param, transaction, commandTimeout, commandType);
        }

        public async Task<GridReader> QueryMultipleAsync(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            using (var dbConnection = _databaseManager.CreateDbConnection())
            {
                return await dbConnection.QueryMultipleAsync(sql, param, transaction, commandTimeout, commandType);
            }
        }
    }
}
