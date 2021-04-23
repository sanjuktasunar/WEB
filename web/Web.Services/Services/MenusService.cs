using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Web.Database.BaseRepo;
using Web.Entity.Dto;
using Web.Entity.Entity;
using Web.Repositories.Interface;
using Web.Repositories.Repositories.Administration;
using Web.Repositories.Utitlities;
using Web.Services.Mapping;

namespace Web.Services.Services
{
    public interface IMenusService
    {
        Task<IEnumerable<MenusDto>> GetMenusAsync();
        IEnumerable<MenuAccessPermissionDto> GetMenusForUser();
        Task<MenusDto> GetMenusByIdAsync(int id);
        Task<IEnumerable<MenusDto>> GetParentMenusAsync();
        string Insert(MenusDto dto);
        string Update(MenusDto dto);
        string Delete(int id);
    }
    public class MenusService : IMenusService
    {
        private IMenusRepository _menusRepository;
       
        MessageClass _message = new MessageClass();
        public MenusService(IMenusRepository menusRepository)
        {
            _menusRepository = menusRepository;
        }

        public async Task<IEnumerable<MenusDto>> GetMenusAsync()
        {
            var menus = (await _menusRepository.GetMenusAsync());
            return menus;
        }

        public IEnumerable<MenuAccessPermissionDto> GetMenusForUser()
        {
            return (_menusRepository.GetMenusByUserId(null));
        }

        public async Task<MenusDto> GetMenusByIdAsync(int id)
        {
            return (await _menusRepository.GetMenusByIdAsync(id));
        }
        public async Task<IEnumerable<MenusDto>> GetParentMenusAsync()
        {
            return (await _menusRepository.GetParentMenusAsync());
        }

        public string Insert(MenusDto dto)
        {
            string message = "";
            try
            {
                int result = _menusRepository.Insert(dto.ToEntity());
                 message = _message.ShowSuccessMessage(result) + "+" + result;
            }
            catch(SqlException ex)
            {
                message = _message.ShowErrorMessage(string.Format("{0} ~ {1}", ex.Number.ToString(), ex.Message));
            }
           
            return message;
        }

        public string Update(MenusDto dto)
        {
            string message = "";
            try
            {
                int result = _menusRepository.Update(dto.ToEntity());
                message = _message.ShowSuccessMessage(result) + "+" + result;
            }
            catch (SqlException ex)
            {
                message = _message.ShowErrorMessage(string.Format("{0} ~ {1}", ex.Number.ToString(), ex.Message));
            }

            return message;
        }

        public string Delete(int id) 
        {
            string message = "";
            try
            {
                int result = _menusRepository.Delete(id);
                message = _message.ShowDeleteMessage(result);
            }
            catch (SqlException ex)
            {
                message = _message.ShowErrorMessage(string.Format("{0} ~ {1}", ex.Number.ToString(), ex.Message));
            }
            return message;
        }
    }
}
