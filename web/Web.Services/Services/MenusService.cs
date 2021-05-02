using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
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
        Task<string> Insert(MenusDto dto);
        Task<string> Update(MenusDto dto);
        Task<string> Delete(int id);
    }
    public class MenusService : IMenusService
    {
        private IMenusRepository _menusRepository;
        private IBaseInterface _baseInterface;
        MessageClass _message = new MessageClass();
        public MenusService(IMenusRepository menusRepository,
            IBaseInterface baseInterface)
        {
            _menusRepository = menusRepository;
            _baseInterface = baseInterface;
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

        public async Task<string> Insert(MenusDto dto)
        {
            string message = "";
            var con = _baseInterface.GetConnection();
            var transaction = con.BeginTransaction();
            try
            {
                var entity = dto.ToEntity();
                int result = _menusRepository.Insert(entity,con,transaction);
                await _menusRepository.MenuOrder(entity, con, transaction);
                 message = _message.ShowSuccessMessage(result) + "+" + result;
            }
            catch(SqlException ex)
            {
                message = _message.ShowErrorMessage(string.Format("{0} ~ {1}", ex.Number.ToString(), ex.Message));
            }
           
            return message;
        }

        public async Task<string> Update(MenusDto dto)
        {
            string message = "";
            var con = _baseInterface.GetConnection();
            var transaction = con.BeginTransaction();
            try
            {
                var entity = dto.ToEntity();
                int result = _menusRepository.Update(entity,con,transaction);
                await _menusRepository.MenuOrder(entity, con, transaction);
                message = _message.ShowSuccessMessage(result) + "+" + result;
                transaction.Commit();
            }
            catch (SqlException ex)
            {
                message = _message.ShowErrorMessage(string.Format("{0} ~ {1}", ex.Number.ToString(), ex.Message));
                transaction.Rollback();
            }

            return message;
        }

        public async Task<string> Delete(int id) 
        {
            string message = "";
            var con = _baseInterface.GetConnection();
            var transaction = con.BeginTransaction();
            try
            {
                await _menusRepository.MenuOrderOnDelete(id, con, transaction);
                int result = _menusRepository.Delete(id,con,transaction);
                message = _message.ShowDeleteMessage(result);
                transaction.Commit();
            }
            catch (SqlException ex)
            {
                message = _message.ShowErrorMessage(string.Format("{0} ~ {1}", ex.Number.ToString(), ex.Message));
                transaction.Rollback();
            }
            return message;
        }
    }
}
