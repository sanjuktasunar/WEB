using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Web.Database
{
    public interface IDatabaseManager
    {
        IDbConnection CreateDbConnection();

        IDbConnection CreateDbConnection(string connectionString);
    }
    public class DatabaseManager : IDatabaseManager
    {
        public string ConnectionString { get; set; }
        public IDbConnection CreateDbConnection()
        {
            return CreateDbConnection(this.ConnectionString);
        }

        public IDbConnection CreateDbConnection(string connectionString)
        {
            this.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
            if (string.IsNullOrWhiteSpace(this.ConnectionString))
            {
                throw new Exception("ConnectionString is missing.");
            }
            return new SqlConnection { ConnectionString = this.ConnectionString };
        }
    }
}
