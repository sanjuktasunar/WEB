using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Database;
using Web.Database.BaseRepo;
using Web.Entity.Dto;
using Web.Repositories.Interface;

namespace Web.Repositories.Repositories
{
    public class DataRepository:IDataRepository
    {
        private IDapperManager _dapperManager;
        public DataRepository(IDapperManager dapperManager)
        {
            _dapperManager = dapperManager;
        }

        public async Task<IEnumerable<CountryDto>> GetOutsideCountryAsync()
        {
            return (await _dapperManager.QueryAsync<CountryDto>("SELECT * FROM Country WHERE Status=1 AND IsOutsideNepal=1"));
        }

        public async Task<IEnumerable<ProvinceDto>> GetActiveProvinceAsync()
        {
            return (await _dapperManager.QueryAsync<ProvinceDto>("SELECT * FROM Province WHERE Status=1"));
        }

        public async Task<IEnumerable<DistrictDto>> GetDistrictByProvinceIdAsync(int provinceId)
        {
            return (await _dapperManager.QueryAsync<DistrictDto>("SELECT * FROM District WHERE Status=1 AND ProvinceId=@provinceId",new { provinceId=provinceId }));
        }


        public async Task<IEnumerable<MunicipalityTypeDto>> GetActiveMunicipalityTypeAsync()
        {
            return (await _dapperManager.QueryAsync<MunicipalityTypeDto>("SELECT * FROM MunicipalityType WHERE Status=1"));
        }

        public async Task<IEnumerable<MemberFieldDto>> GetActiveMemberFieldAsync()
        {
            return (await _dapperManager.QueryAsync<MemberFieldDto>("SELECT * FROM MemberField WHERE Status=1"));
        }

        public async Task<IEnumerable<OccupationDto>> GetActiveOccupationAsync()
        {
            return (await _dapperManager.QueryAsync<OccupationDto>("SELECT * FROM Occupation WHERE Status=1"));
        }

        public async Task<IEnumerable<AccountHeadDto>> GetActiveAccountHeadAsync()
        {
            return (await _dapperManager.QueryAsync<AccountHeadDto>("SELECT * FROM AccountHead WHERE Status=1"));
        }
    }
}
