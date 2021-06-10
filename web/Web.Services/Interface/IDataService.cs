using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Entity.Dto;
using Web.Entity.Model;

namespace Web.Services.Interface
{
    public interface IDataService
    {
        Task<IEnumerable<DropdownList>> GetOutsideCountryAsync();
        Task<IEnumerable<DropdownList>> GetActiveProvinceAsync();
        Task<IEnumerable<DropdownList>> GetDistrictByProvinceIdAsync(int? id);
        Task<IEnumerable<DropdownList>> GetActiveMunicipalityTypeAsync();
        Task<IEnumerable<DropdownList>> GetActiveGenderAsync();
        Task<IEnumerable<DropdownList>> GetActiveMemberFieldAsync();
        Task<IEnumerable<DropdownList>> GetActiveOccupationAsync();
    }
}
