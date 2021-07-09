using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Database;
using Web.Database.BaseRepo;
using Web.Entity.Dto;
using Web.Entity.Entity;

namespace Web.Repositories.Repositories.AddressRepos
{
    public interface ICountryRepository
    {
        Task<IEnumerable<CountryDto>> GetCountryList();
        Task<CountryDto> GetCountryById(int id);
        int Insert(Country entity);
        int Update(Country entity);
        int Delete(int id);
    }

    public class CountryRepository : ICountryRepository
    {
        private IDapperManager _dapperManager;
        private IBaseRepo<Country> _countryRepo;
        public CountryRepository(IDapperManager dapperManager,
            IBaseRepo<Country> countryRepo)
        {
            _dapperManager = dapperManager;
            _countryRepo = countryRepo;
        }
        public async Task<IEnumerable<CountryDto>> GetCountryList()
        {
            var country = await _dapperManager.QueryAsync<CountryDto>("SELECT * FROM Country");
            return country;
        }

        public async Task<CountryDto> GetCountryById(int id)
        {
            var country = await _dapperManager.QuerySingleAsync<CountryDto>("SELECT * FROM Country WHERE Id=@id", new { id });
            return country;
        }
        public int Insert(Country entity)
        {
            return _countryRepo.Insert(entity);
        }

        public int Update(Country entity)
        {
            return _countryRepo.Update(entity);
        }

        public int Delete(int id)
        {
            return _countryRepo.Delete(id);
        }
    }
}
