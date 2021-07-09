using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Entity.Dto;
using Web.Repositories.Interface;
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
        Task<RoleDto> MenuAccessPermissionAsync(int RoleId);
        Task<string> AddMenuAccess(IEnumerable<MenuAccessPermissionDto> dtos);
    }
    public class RoleService:IRoleService
    {
        private IRoleRepository _roleRepository;
        private IMessageClass _messageClass;
        private IBaseInterface _baseInterface;
        public RoleService(IRoleRepository roleRepository,
            IMessageClass messageClass, IBaseInterface baseInterface)
        {
            _roleRepository = roleRepository;
            _messageClass = messageClass;
            _baseInterface = baseInterface;
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

        public async Task<RoleDto> MenuAccessPermissionAsync(int RoleId)
        {
            var role = (await GetRoleById(RoleId));
            if (role is null)
                return null;

            role.MenuAccessPermissions = await _roleRepository.GetMenuAccessPermissionsByRoleIdAsync(RoleId);
            return role;
        }

        public async Task<string> AddMenuAccess(IEnumerable<MenuAccessPermissionDto> dtos)
        {
            string message = "";
            var conn = _baseInterface.GetConnection();
            var transaction = conn.BeginTransaction();
            try
            {
                int result = 0;
                foreach (var d in dtos)
                {
                    var entity = d.ToEntity();
                    var dto = await _roleRepository.GetMenuAccessByIdAsync(d.MenuAccessPermissionId);
                    if (dto is null)
                        result = _roleRepository.InsertMenuAccess(entity, transaction, conn);
                    else
                    {
                        entity.MenuAccessPermissionId = dto.MenuAccessPermissionId;
                        result = _roleRepository.UpdateMenuAccess(entity, transaction, conn);
                    }
                }
                message = _messageClass.ShowSuccessMessage(result);
                transaction.Commit();
            }
            catch (SqlException ex)
            {
                message = _messageClass.ShowErrorMessage(string.Format("{0} ~ {1}", ex.Number.ToString(), ex.Message));
                transaction.Rollback();
            }
            return message;
        }

    }
}
