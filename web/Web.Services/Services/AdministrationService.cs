using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Entity.Dto;
using Web.Repositories.Repositories.Administration;

namespace Web.Services.Services
{
    public interface IAdministrationService
    {
        Task<IEnumerable<RoleDto>> GetActiveRoleAsync();
        Task<IEnumerable<DesignationDto>> GetActiveDesignationAsync();
        Task<IEnumerable<DepartmentDto>> GetActiveDepartmentAsync();
        Task<IEnumerable<GenderDto>> GetActiveGenderAsync();
        Task<IEnumerable<UserStatusDto>> GetActiveUserStatusAsync();
    }
    public class AdministrationService:IAdministrationService
    {
        IAdministrationRepository _administrationRepository;
        public AdministrationService(IAdministrationRepository administrationRepository)
        {
            _administrationRepository = administrationRepository;

        }
        public async Task<IEnumerable<RoleDto>> GetActiveRoleAsync()
        {
            return (await _administrationRepository.GetActiveRoleAsync());
        }

        public async Task<IEnumerable<DesignationDto>> GetActiveDesignationAsync()
        {
            return (await _administrationRepository.GetActiveDesignationAsync());
        }

        public async Task<IEnumerable<DepartmentDto>> GetActiveDepartmentAsync()
        {
            return (await _administrationRepository.GetActiveDepartmentAsync());
        }

        public async Task<IEnumerable<GenderDto>> GetActiveGenderAsync()
        {
            return (await _administrationRepository.GetActiveGenderAsync());
        }

        public async Task<IEnumerable<UserStatusDto>> GetActiveUserStatusAsync()
        {
            return (await _administrationRepository.GetActiveUserStatusAsync());
        }
    }
}
