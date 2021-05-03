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
using System.Web;

namespace Web.Database.BaseRepo
{
    public interface IBaseRepo<TModel> where TModel : class, new()
    {
        IEnumerable<TEntity> StoredProcedure<TEntity>(string sql, object param = null);
        IEnumerable<TEntity> Query<TEntity>(string sql, object param = null);
        int Insert(TModel obj);
        int Update(TModel obj);
        int Update(TModel obj, IDbTransaction transaction, SqlConnection conn);
        int Delete(object id);
        int Delete(object id, IDbTransaction transaction, SqlConnection con);
        int Insert(TModel obj, IDbTransaction transaction, SqlConnection conn);
    }
    public class BaseRepo<TModel> : IBaseRepo<TModel> where TModel : class, new()
    {
        string con = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
        public IEnumerable<TEntity> StoredProcedure<TEntity>(string sql, object param = null)
        {
            using (var db = new SqlConnection(con))
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

        public int Insert(TModel obj)
        {
            using (var db = new SqlConnection(con))
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
        public int Insert(TModel obj, IDbTransaction transaction, SqlConnection conn)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                return conn.Insert(obj, transaction);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        public int Update(TModel obj)
        {
            using (var db = new SqlConnection(con))
            {
                try
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    bool result = db.Update(obj);
                    if (result == true)
                        return 0;
                    else
                        return -1;
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

        public int Update(TModel obj, IDbTransaction transaction, SqlConnection conn)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                bool result = conn.Update(obj, transaction);
                if (result == true)
                    return 0;
                else
                    return -1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Delete(object id)
        {
            using (var db = new SqlConnection(con))
            {
                try
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    bool result = db.Delete(Get(id));
                    if (result == true)
                        return 0;
                    else
                        return -1;
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

        public int Delete(object id, IDbTransaction transaction,SqlConnection con)
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                bool result = con.Delete(Get(id), transaction);
                if (result == true)
                    return 0;
                else
                    return -1;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public TModel Get(object id)
        {
            using (var db = new SqlConnection(con))
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
        //public int CurrentUser()
        //{
        //    return Convert.ToInt32(HttpContext)
        //}
    }
}
