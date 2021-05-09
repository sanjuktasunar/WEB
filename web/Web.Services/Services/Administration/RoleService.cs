using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Entity.Dto;
using Web.Repositories.Repositories.Administration;
using Web.Repositories.Utitlities;
using Web.Services.Mapping;

namespace Web.Services.Services.Administration
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleDto>> GetAllRoles();
        Task<RoleDto> GetRoleById(int id);
        string Insert(RoleDto dto);
        string Update(RoleDto dto);
        string Delete(int id);
    }
    public class RoleService:IRoleService
    {
        private IRoleRepository _roleRepository;
        private IMessageClass _messageClass;
        public RoleService(IRoleRepository roleRepository,
            IMessageClass messageClass)
        {
            _roleRepository = roleRepository;
            _messageClass = messageClass;
        }
        public async Task<IEnumerable<RoleDto>> GetAllRoles()
        {
            return (await _roleRepository.GetAllRolesAsync());   
        }

        public async Task<RoleDto> GetRoleById(int id)
        {
            return (await _roleRepository.GetRoleByIdAsync(id));
        }

        public string Insert(RoleDto dto)
        {
            string message = "";
            try
            {
                int result = _roleRepository.Insert(dto.ToEntity());
                message = _messageClass.ShowSuccessMessage(result);
            }
            catch(SqlException ex)
            {
                message = _messageClass.ShowErrorMessage(string.Format("{0} ~ {1}", ex.Number.ToString(), ex.Message));
            }
            return message;
        }

        public string Update(RoleDto dto)
        {
            string message = "";
            try
            {
                int result = _roleRepository.Update(dto.ToEntity());
                message = _messageClass.ShowSuccessMessage(result);
            }
            catch (SqlException ex)
            {
                message = _messageClass.ShowErrorMessage(string.Format("{0} ~ {1}", ex.Number.ToString(), ex.Message));
            }
            return message;
        }

        public string Delete(int id)
        {
            string message = "";
            try
            {
                int result = _roleRepository.Delete(id);
                message = _messageClass.ShowDeleteMessage(result);
            }
            catch (SqlException ex)
            {
                message = _messageClass.ShowErrorMessage(string.Format("{0} ~ {1}", ex.Number.ToString(), ex.Message));
            }
            return message;
        }
    }
}
