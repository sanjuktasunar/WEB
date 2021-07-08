using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Entity.Dto;
using Web.Repositories.Interface;
using Web.Repositories.Repositories.AddressRepos;
using Web.Repositories.Utitlities;
using Web.Services.Mapping;

namespace Web.Services.Services.AddressService
{
    public interface ICountryService
    {
        Task<IEnumerable<CountryDto>> GetCountryListAsync();
        Task<CountryDto> GetCountryByIdAsyc(int id);
        string Insert(CountryDto dto);
        Task<string> Update(CountryDto dto);
        Task<string> Delete(int id);
    }

    public class CountryService : ICountryService
    {
        private ICountryRepository _countryRepository;
        private IMessageClass _messageClass;
        private IBaseInterface _baseInterface;

        public CountryService(ICountryRepository countryRepository,
            IMessageClass messageClass,
            IBaseInterface baseInterface)
        {
            _countryRepository = countryRepository;
            _messageClass = messageClass;
            _baseInterface = baseInterface;
        }

        public async Task<IEnumerable<CountryDto>> GetCountryListAsync()
        {
            var obj = await _countryRepository.GetCountryList();
            return obj;
        }

        public async Task<CountryDto> GetCountryByIdAsyc(int id)
        {
            var obj = await _countryRepository.GetCountryById(id);
            return obj;
        }

        public string Insert(CountryDto dto)
        {
            string message = "";
            try
            {
                dto.Status = true;
                dto.IsOutsideNepal = true;
                int result = _countryRepository.Insert(dto.ToEntity());
                message = _messageClass.ShowSuccessMessage(result);
            }
            catch (SqlException ex)
            {
                message = _messageClass.ShowErrorMessage(string.Format("{0} ~ {1}", ex.Number.ToString(), ex.Message));
            }
            return message;
        }

        public async Task<string> Update(CountryDto dto)
        {
            string message = "";
            try
            {
                var country = await GetCountryByIdAsyc(dto.Id);
                if (country == null)
                    return "Sorry Country cannot be found + -1";

                //dto.Status = country.Status;
                dto.IsOutsideNepal = dto.IsOutsideNepal;
                int result = _countryRepository.Update(dto.ToEntity());
                message = _messageClass.ShowSuccessMessage(result);
            }
            catch (SqlException ex)
            {
                message = _messageClass.ShowErrorMessage(string.Format("{0} ~ {1}", ex.Number.ToString(), ex.Message));
            }
            return message;
        }

        public async Task<string> Delete(int id)
        {
            string message = "";
            try
            {
                var country = await GetCountryByIdAsyc(id);
                if (country == null)
                    return "Sorry Country cannot be found + -1";

                int result = _countryRepository.Delete(id);
                message = _messageClass.ShowSuccessMessage(result);
            }
            catch (SqlException ex)
            {
                message = _messageClass.ShowErrorMessage(string.Format("{0} ~ {1}", ex.Number.ToString(), ex.Message));
            }
            return message;
        }
    }
}
