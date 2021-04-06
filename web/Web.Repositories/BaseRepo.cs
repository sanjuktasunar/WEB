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

namespace Web.Repositories
{
    public class BaseRepo<TModel> : IBaseRepo<TModel> where TModel : class, new()
    {
        public SqlConnection con;
        public void connection()
        {
            string conStr = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
            con = new SqlConnection(conStr);
        }

        public async Task<List<TModel>> GetAll(string ProcName)
        {
            List<TModel> list = new List<TModel>();
            connection();
            con.Open();
            list = await con.QueryAsync<TModel>(ProcName, commandType: CommandType.Text).ToListAsync();
            con.Close();
            return list;
        }

        public dynamic Insert(TModel obj)
        {
            using (var db = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    return db.Insert(obj);
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                finally
                {
                    db.Close();
                }
            }
        }

        public dynamic Update(TModel obj)
        {
            using (var db = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    bool result = db.Update(obj);
                    if (result == true)
                    {
                        return 0;
                    }
                    else
                    {
                        return -1;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    db.Close();
                }
            }
        }

        public IEnumerable<TEntity> Query<TEntity>(string sql, object param = null)
        {
            using (var db = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    return db.Query<TEntity>(sql, param);
                }
                finally
                {
                    db.Close();
                }
            }
        }

        public TEntity QueryForSingle<TEntity>(string sql, object param = null)
        {
            using (var db = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    return db.Query<TEntity>(sql, param).FirstOrDefault();
                }
                finally
                {
                    db.Close();
                }
            }
        }

        public dynamic Delete(object id)
        {
            using (var db = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    bool result = db.Delete(Get(id));
                    if (result == true)
                    {
                        return 0;
                    }
                    else
                    {
                        return -1;
                    }
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                finally
                {
                    db.Close();
                }
            }
        }

        public TModel Get(object id)
        {
            using (var db = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();

                    return db.Get<TModel>(id);
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                finally
                {
                    db.Close();
                }
            }
        }

       
        public IEnumerable<TEntity> StoredProcedure<TEntity>(string sql, object param = null)
        {
            using (var db = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    return db.Query<TEntity>(sql, param, commandType: CommandType.StoredProcedure);
                }
                finally
                {
                    db.Close();
                }
            }
        }

       
    }
}
