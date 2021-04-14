using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Database;
using Web.Entity.Dto;
using Web.Entity.Entity;

namespace Web.Repositories.Repositories.Administration
{
    public interface IUsersRepository
    {
        Task<Users> GetLoginUser(Users entity);
    }

    public class UsersRepository:IUsersRepository
    {
        private IDapperManager _dapperManager;

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
    }
}
