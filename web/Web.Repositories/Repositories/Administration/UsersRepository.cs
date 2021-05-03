using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Database;
using Web.Database.BaseRepo;
using Web.Entity.Dto;
using Web.Entity.Entity;

namespace Web.Repositories.Repositories.Administration
{
    public interface IUsersRepository
    {
        Task<Users> GetLoginUser(Users entity);
        int Insert(Users entity, IDbTransaction transaction, SqlConnection conn);
        int Update(Users entity, IDbTransaction transaction, SqlConnection con);
        Task<Users> GetUserByIdAsync(int id);
    }

    public class UsersRepository:IUsersRepository
    {
        private IDapperManager _dapperManager;
        BaseRepo<Users> _usersRepo = new BaseRepo<Users>();
        public UsersRepository(IDapperManager dapperManager)
        {
            _dapperManager = dapperManager;
        }

        public async Task<Users> GetLoginUser(Users entity)
        {
            var result = await _dapperManager.QueryAsync<Users>("SELECT * from Users WHERE UserName=@UserName OR ContactNumber=@UserName OR EmailAddress=@UserName",new { entity.UserName });
            if (result.Count() == 0)
                return null;

            string hashPassword =Web.Repositories.Utitlities.Security.GetMd5Sum(entity.Password);
            var loginUser = result.Where(a => a.Password.Trim() == hashPassword.Trim());
            if (loginUser.Count() == 0)
                return null;

            return loginUser.FirstOrDefault();
        }
        public int Insert(Users entity, IDbTransaction transaction,SqlConnection conn)
        {
            return (_usersRepo.Insert(entity,transaction,conn));
        }

        public int Update(Users entity, IDbTransaction transaction, SqlConnection con)
        {
            return (_usersRepo.Update(entity,transaction,con));
        }

        public async Task<Users> GetUserByIdAsync(int id)
        {
            var user =(await _dapperManager.QueryAsync<Users>("SELECT * FROM Users WHERE UserId=@id",new { id })).FirstOrDefault();
            return user;
        }
    }
}
