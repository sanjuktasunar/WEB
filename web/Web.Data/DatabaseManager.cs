using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Data
{
    public interface IDatabaseManager
    {
        string ConnectionString { get; set; }
        IDbConnection CreateDbConnection();
        /// <summary>
        /// Creates IDbConnection instance using provided DatabaseProvider and ConnectionString
        /// </summary>
        IDbConnection CreateDbConnection(string connectionString);
    }
    public class DatabaseManager : IDatabaseManager
    {
        public string ConnectionString { get; set; }
        public IDbConnection CreateDbConnection()
        {
            return CreateDbConnection(this.ConnectionString);
        }

        public IDbConnection CreateDbConnection(string ConnectionString)
        {
            if (string.IsNullOrWhiteSpace(ConnectionString))
            {
                throw new Exception("ConnectionString is missing.");
            }
            return new SqlConnection { ConnectionString = ConnectionString };
        }
    }
}
