using Dapper;
using DapperExtensions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace Web.Database
{
    public interface IDapperManager
    {
        Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);

        Task<IEnumerable<T>> QueryAsyncTrans<T>(string sql, object param = null, IDbTransaction transaction = null, SqlConnection connection = null);

        Task<IEnumerable<T>> StoredProcedureAsync<T>(string proc, object param = null, IDbTransaction transaction = null, int? commandTimeout = null);
        Task<T> QuerySingleAsync<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
        T ExecuteScalar<T>(string sql, object param = null, IDbTransaction transaction = null,
          int? commandTimeout = null);
        T ExecuteScalar<T>(SqlConnection dbConnection, string sql, object param = null, IDbTransaction transaction = null,
           int? commandTimeout = null, CommandType? commandType = null);
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
            return sql;
        }
        public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            using (var dbConnection = _databaseManager.CreateDbConnection())
            {
                return await dbConnection.QueryAsync<T>(sql, param, transaction, commandTimeout, commandType);
            }
        }

        public async Task<IEnumerable<T>> QueryAsyncTrans<T>(string sql, object param = null, IDbTransaction transaction=null, SqlConnection connection = null)
        {
            return await connection.QueryAsync<T>(sql, param, transaction, commandType:CommandType.Text);
        }

        public async Task<IEnumerable<T>> StoredProcedureAsync<T>(string proc, object param = null, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            using (var dbConnection = _databaseManager.CreateDbConnection())
            {
                return (await dbConnection.QueryAsync<T>(proc, param, transaction, commandTimeout, commandType:CommandType.StoredProcedure));
            }
        }

        public async Task<T> QuerySingleAsync<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            using (var dbConnection = _databaseManager.CreateDbConnection())
            {
                return (await dbConnection.QueryAsync<T>(sql, param, transaction, commandTimeout, commandType)).FirstOrDefault();
            };
        }

        public T ExecuteScalar<T>(string sql, object param = null, IDbTransaction transaction = null,
           int? commandTimeout = null)
        {
            using (var dbConnection = _databaseManager.CreateDbConnection())
            {
                return dbConnection.ExecuteScalar<T>(EnsureParameterSpecification(sql), param, transaction,
                    commandTimeout, commandType:CommandType.Text);
            }
        }

        public T ExecuteScalar<T>(SqlConnection dbConnection, string sql, object param = null, IDbTransaction transaction = null,
           int? commandTimeout = null, CommandType? commandType = null)
        {
            return dbConnection.ExecuteScalar<T>(EnsureParameterSpecification(sql), param, transaction,
                commandTimeout, commandType);
        }
    }
}
