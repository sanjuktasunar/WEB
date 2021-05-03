using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Database;
using Web.Entity.Dto;

namespace Web.Repositories.Repositories.Administration
{
    public interface IAdministrationRepository
    {
        Task<IEnumerable<RoleDto>> GetActiveRoleAsync();
        Task<IEnumerable<DesignationDto>> GetActiveDesignationAsync();
        Task<IEnumerable<DepartmentDto>> GetActiveDepartmentAsync();
        Task<IEnumerable<GenderDto>> GetActiveGenderAsync();
        Task<IEnumerable<UserStatusDto>> GetActiveUserStatusAsync();
    }
    public class AdministrationRepository:IAdministrationRepository
    {
        IDapperManager _dapperManager;
        public AdministrationRepository(IDapperManager dapperManager)
        {
            _dapperManager = dapperManager;
        }
        public async Task<IEnumerable<RoleDto>> GetActiveRoleAsync()
        {
            var roles = await _dapperManager.QueryAsync<RoleDto>("SELECT * FROM Role WHERE Status=1");
            return roles;
        }

        public async Task<IEnumerable<DesignationDto>> GetActiveDesignationAsync()
        {
            var designations = await _dapperManager.QueryAsync<DesignationDto>("SELECT * FROM Designation WHERE Status=1");
            return designations;
        }

        public async Task<IEnumerable<DepartmentDto>> GetActiveDepartmentAsync()
        {
            var department = await _dapperManager.QueryAsync<DepartmentDto>("SELECT * FROM Department WHERE Status=1");
            return department;
        }

        public async Task<IEnumerable<GenderDto>> GetActiveGenderAsync()
        {
            var department = await _dapperManager.QueryAsync<GenderDto>("SELECT * FROM Gender WHERE Status=1");
            return department;
        }

        public async Task<IEnumerable<UserStatusDto>> GetActiveUserStatusAsync()
        {
            var status = await _dapperManager.QueryAsync<UserStatusDto>("SELECT * FROM UserStatus WHERE Status=1");
            return status;
        }
    }
}
