using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Repositories.Interface
{
    public interface IBaseInterface
    {
        SqlConnection GetConnection();
    }

    public class BaseInterface:IBaseInterface
    {
        string con = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();

        public SqlConnection GetConnection()
        {
            var db = new SqlConnection(con);
            if (db.State == ConnectionState.Closed)
                db.Open();

            return db;
        }
    }
}
