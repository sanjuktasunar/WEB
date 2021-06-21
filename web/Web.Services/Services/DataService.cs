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
        private readonly IAdministrationService _administrationService;
        public DataService(IDataRepository dataRepository,
            IAdministrationService administrationService)
        {
            _dataRepository = dataRepository;
            _administrationService = administrationService;
        }

        public async Task<IEnumerable<DropdownList>> GetOutsideCountryAsync()
        {
            return (await _dataRepository.GetOutsideCountryAsync()).Select(a=>a.ToCountryDropdown());
        }

        public async Task<IEnumerable<DropdownList>> GetActiveProvinceAsync()
        {
            return (await _dataRepository.GetActiveProvinceAsync()).Select(a=>a.ToProvinceDropdown());
        }

        public async Task<IEnumerable<DropdownList>> GetDistrictByProvinceIdAsync(int? id)
        {
            var obj = new List<DropdownList>();
            if(id!=null)
             obj= (await _dataRepository.GetDistrictByProvinceIdAsync(id.Value)).Select(a => a.ToDistrictDropdown()).ToList();
            return obj;
        }

        public async Task<IEnumerable<DropdownList>> GetActiveMunicipalityTypeAsync()
        {
            return (await _dataRepository.GetActiveMunicipalityTypeAsync()).Select(a=>a.ToMunicipalityTypeDropdown());
        }

        public async Task<IEnumerable<DropdownList>> GetActiveGenderAsync()
        {
            return (await _administrationService.GetActiveGenderAsync()).Select(a => a.ToGenderDropdown());
        }

        public async Task<IEnumerable<DropdownList>> GetActiveMemberFieldAsync()
        {
            return (await _dataRepository.GetActiveMemberFieldAsync()).Select(a => a.ToMemberFieldDropdown());
        }

        public async Task<IEnumerable<DropdownList>> GetActiveOccupationAsync()
        {
            return (await _dataRepository.GetActiveOccupationAsync()).Select(a => a.ToOccupationDropdown());
        }

        public async Task<IEnumerable<DropdownList>> GetActiveAccountHeadAsync()
        {
            return (await _dataRepository.GetActiveAccountHeadAsync()).Select(a => a.ToAccountHeadDto());
        }
    }
}
