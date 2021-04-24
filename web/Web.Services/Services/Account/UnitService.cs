using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Entity.Dto;
using Web.Repositories.Repositories.Account;
using Web.Repositories.Utitlities;
using Web.Services.Mapping;

namespace Web.Services.Services.Account
{
    public interface IUnitService
    {
        Task<IEnumerable<UnitDto>> GetAllUnitAsync();
        Task<IEnumerable<UnitDto>> GetActiveUnitAsync();
        Task<UnitDto> GetUnitByIdAsync(int id);
        string Insert(UnitDto dto);
        Task<string> Update(UnitDto dto);
        string Delete(int id);
    }
    public class UnitService:IUnitService
    {
        private IUnitRepository _unitRepository;
        IMessageClass _messageClass;
        public UnitService(
            IMessageClass messageClass, 
            IUnitRepository unitRepository)
        {
            _unitRepository = unitRepository;
            _messageClass = messageClass;
        }

        public async Task<IEnumerable<UnitDto>> GetAllUnitAsync()
        {
            return (await _unitRepository.GetAllUnitAsync());
        }

        public async Task<IEnumerable<UnitDto>> GetActiveUnitAsync()
        {
            return (await _unitRepository.GetActiveUnitAsync());
        }

        public async Task<UnitDto> GetUnitByIdAsync(int id)
        {
            return (await _unitRepository.GetUnitByIdAsync(id));
        }

        public string Insert(UnitDto dto)
        {
            string message = "";
            try
            {
               int result= _unitRepository.Insert(dto.ToEntity());
               message = _messageClass.ShowSuccessMessage(result);
            }
            catch (SqlException ex)
            {
                message = _messageClass.ShowErrorMessage(string.Format("{0} ~ {1}", ex.Number.ToString(), ex.Message));
            }
            return message;
        }

        public async Task<string> Update(UnitDto dto)
        {
            string message = "";
            try
            {
                var unit = await GetUnitByIdAsync(dto.UnitId);
                if (unit is null)
                    return null;

                dto.CreatedBy = unit.CreatedBy;
                dto.CreatedDate = unit.CreatedDate;
                int result = _unitRepository.Update(dto.ToEntity());
                message = _messageClass.ShowSuccessMessage(result);
            }
            catch (SqlException ex)
            {
                message = _messageClass.ShowErrorMessage(string.Format("{0} ~ {1}", ex.Number.ToString(), ex.Message));
            }
            return message;
        }

        public string Delete(int id)
        {
            string message = "";
            try
            {
                int result = _unitRepository.Delete(id);
                message = _messageClass.ShowDeleteMessage(result);
            }
            catch (SqlException ex)
            {
                message = _messageClass.ShowErrorMessage(string.Format("{0} ~ {1}", ex.Number.ToString(), ex.Message));
            }
            return message;
        }
    }
}
