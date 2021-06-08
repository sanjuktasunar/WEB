using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Entity.Dto;

namespace Web.Repositories.Interface
{
    public interface IDataRepository
    {
        Task<IEnumerable<CountryDto>> GetOutsideCountryAsync();
        Task<IEnumerable<ProvinceDto>> GetActiveProvinceAsync();
        Task<IEnumerable<DistrictDto>> GetDistrictByProvinceIdAsync(int provinceId);
        Task<IEnumerable<MunicipalityTypeDto>> GetActiveMunicipalityTypeAsync();
    }
}
