using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using Web.Entity.Dto;
using Web.Repositories.Repositories.Administration;
using Web.Services.Mapping;

namespace Web.Services.Services
{
    public interface IUsersService
    {
        Task<UsersDto> GetLoginUser(UsersDto dto);
    }
    public class UsersService:IUsersService
    {
        private IUsersRepository _usersRepository;
        public UsersService(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task<UsersDto> GetLoginUser(UsersDto dto)
        {
            var user= await _usersRepository.GetLoginUser(dto.ToEntity());
            if (user == null)
                return null;

            HttpContext.Current.Session["UserId"] = user.UserId;
            HttpContext.Current.Session["UserTypeId"] = user.UserTypeId;

            FormsAuthentication.SetAuthCookie(user.UserName, false);
            return user.ToDto();
        }
    }
}
