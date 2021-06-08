using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Entity.Dto;
using Web.Entity.Model;
using Web.Repositories.Interface;
using Web.Services.Interface;
using Web.Services.Mapping;

namespace Web.Services.Services
{
    public class DataService:IDataService
    {
        private readonly IDataRepository _dataRepository;
        public DataService(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }

        public async Task<IEnumerable<DropdownList>> GetOutsideCountryAsync()
        {
            return (await _dataRepository.GetOutsideCountryAsync()).Select(a=>a.ToCountryDropdown());
        }

        public async Task<IEnumerable<DropdownList>> GetActiveProvinceAsync()
        {
            return (await _dataRepository.GetActiveProvinceAsync()).Select(a=>a.ToProvinceDropdown());
        }

        public async Task<IEnumerable<DropdownList>> GetDistrictByProvinceIdAsync(int id)
        {
            return (await _dataRepository.GetDistrictByProvinceIdAsync(id)).Select(a=>a.ToDistrictDropdown());
        }

        public async Task<IEnumerable<DropdownList>> GetActiveMunicipalityTypeAsync()
        {
            return (await _dataRepository.GetActiveMunicipalityTypeAsync()).Select(a=>a.ToMunicipalityTypeDropdown());
        }
    }
}
