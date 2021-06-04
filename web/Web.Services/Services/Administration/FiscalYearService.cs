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
    public interface IFiscalYearService
    {
        Task<IEnumerable<FiscalYearDto>> GetAllFiscalYearAsync();
        Task<IEnumerable<FiscalYearDto>> GetAllActiveFiscalYearAsync();
        Task<FiscalYearDto> GetCurrentFiscalYearAsync();
        Task<FiscalYearDto> GetFiscalYearByIdAsync(int id);
        Task<string> InsertAsync(FiscalYearDto dto);
        Task<string> UpdateAsync(FiscalYearDto dto);
        Task<string> DeleteAsync(int id);
    }

    public class FiscalYearService:IFiscalYearService
    {
        private readonly IFiscalYearRepository _fiscalYearRepository;
        private readonly IMessageClass _messageClass;
        private readonly IBaseInterface _baseInterface;
        private readonly IDateService _dateService;

        public FiscalYearService(IFiscalYearRepository fiscalYearRepository,
           IMessageClass messageClass,
           IBaseInterface baseInterface,
           IDateService dateService)
        {
            _fiscalYearRepository = fiscalYearRepository;
            _messageClass = messageClass;
            _baseInterface = baseInterface;
            _dateService = dateService;
        }

        public async Task<IEnumerable<FiscalYearDto>> GetAllFiscalYearAsync()
        {
            return (await _fiscalYearRepository.GetAllFiscalYearAsync());
        }

        public async Task<IEnumerable<FiscalYearDto>> GetAllActiveFiscalYearAsync()
        {
            return (await _fiscalYearRepository.GetAllActiveFiscalYearAsync());
        }

        public async Task<FiscalYearDto> GetCurrentFiscalYearAsync()
        {
            return (await _fiscalYearRepository.GetCurrentFiscalYearAsync());
        }
        public async Task<FiscalYearDto> GetFiscalYearByIdAsync(int id)
        {
            return (await _fiscalYearRepository.GetFiscalYearByIdAsync(id));
        }

        public async Task<string> InsertAsync(FiscalYearDto dto)
        {
            var currentFiscalYear = await GetCurrentFiscalYearAsync();
           var con = _baseInterface.GetConnection();
            var transaction = con.BeginTransaction();
            string message = "";
            try
            {
                if (dto.IsCurrent)
                    _fiscalYearRepository.MakeIsCurrentFalse(con, transaction);
                else
                {
                    if (currentFiscalYear is null)
                        dto.IsCurrent = true;
                }

                dto.StartDateAD = Convert.ToDateTime(_dateService.ConvertToEnglishDate(dto.StartDateBS));
                dto.EndDateAD = Convert.ToDateTime(_dateService.ConvertToEnglishDate(dto.EndDateBS));
                int result = _fiscalYearRepository.Insert(dto.ToEntity(),transaction,con);
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

        public async Task<string> UpdateAsync(FiscalYearDto dto)
        {
            var currentFiscalYear = await GetCurrentFiscalYearAsync();
            var con = _baseInterface.GetConnection();
            var transaction = con.BeginTransaction();
            string message = "";
            try
            {
                var fiscalYear = await GetFiscalYearByIdAsync(dto.FiscalYearId);
                if (fiscalYear is null)
                    return null;

                if(dto.IsCurrent)
                    _fiscalYearRepository.MakeIsCurrentFalse(con, transaction);
                else
                {
                    if (currentFiscalYear is null)
                        dto.IsCurrent = true;
                }
                dto.StartDateAD = Convert.ToDateTime(_dateService.ConvertToEnglishDate(dto.StartDateBS));
                dto.EndDateAD = Convert.ToDateTime(_dateService.ConvertToEnglishDate(dto.EndDateBS));
                dto.CreatedBy = fiscalYear.CreatedBy;
                dto.CreatedDate = fiscalYear.CreatedDate;
                int result = _fiscalYearRepository.Update(dto.ToEntity(),transaction,con);
                message = _messageClass.ShowSuccessMessage(fiscalYear.FiscalYearId);
                transaction.Commit();
            }
            catch (SqlException ex)
            {
                message = _messageClass.ShowErrorMessage(string.Format("{0} ~ {1}", ex.Number.ToString(), ex.Message));
                transaction.Rollback();
            }
            return message;
        }

        public async Task<string> DeleteAsync(int id)
        {
            var con = _baseInterface.GetConnection();
            var transaction = con.BeginTransaction();
            var activeFiscalYear = (await GetAllActiveFiscalYearAsync()).Where(a=>a.FiscalYearId!=id);
            string message = "";
            try
            {
                var fiscalYear = await GetFiscalYearByIdAsync(id);
                if (fiscalYear is null)
                    return null;

                int result = _fiscalYearRepository.Delete(con,transaction,id);
                if (fiscalYear.IsCurrent)
                {
                    if(activeFiscalYear.Count()>0)
                    {
                        int? fiscalYearId = activeFiscalYear.OrderByDescending(a=>a.StartDateAD).FirstOrDefault().FiscalYearId;
                        if (fiscalYearId > 0)
                            _fiscalYearRepository.MakeIsCurrentTrueById(Convert.ToInt32(fiscalYearId), con, transaction);
                    }
                }

                message = _messageClass.ShowDeleteMessage(result);
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
