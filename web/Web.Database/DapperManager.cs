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

        public async Task<IEnumerable<T>> QueryAsync<T>(IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return await cnn.QueryAsync<T>(sql, param, transaction, commandTimeout, commandType);
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
