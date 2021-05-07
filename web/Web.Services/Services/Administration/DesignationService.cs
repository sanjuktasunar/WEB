using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Entity.Dto;
using Web.Repositories.Repositories.Administration;
using Web.Repositories.Utitlities;
using Web.Services.Mapping;

namespace Web.Services.Services.Administration
{
    public interface IDesignationService
    {
        Task<IEnumerable<DesignationDto>> GetAllDesignation();
        Task<DesignationDto> GetDesignationById(int id);
        string Insert(DesignationDto dto);
        string Update(DesignationDto dto);
        string Delete(int id);
    }

    public class DesignationService:IDesignationService
    {
        private IDesignationRepository _designationRepository;
        private IMessageClass _messageClass;
        public DesignationService(IDesignationRepository designationRepository,
            IMessageClass messageClass)
        {
            _designationRepository = designationRepository;
            _messageClass = messageClass;
        }

        public async Task<IEnumerable<DesignationDto>> GetAllDesignation()
        {
            return (await _designationRepository.GetAllDesignationAsync());
        }

        public async Task<DesignationDto> GetDesignationById(int id)
        {
            return (await _designationRepository.GetDesignationByIdAsync(id));
        }

        public string Insert(DesignationDto dto)
        {
            string message = "";
            try
            {
                int result = _designationRepository.Insert(dto.ToEntity());
                message = _messageClass.ShowSuccessMessage(result);
            }
            catch (SqlException ex)
            {
                message = _messageClass.ShowErrorMessage(string.Format("{0} ~ {1}", ex.Number.ToString(), ex.Message));
            }
            return message;
        }

        public string Update(DesignationDto dto)
        {
            string message = "";
            try
            {
                int result = _designationRepository.Update(dto.ToEntity());
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
                int result = _designationRepository.Delete(id);
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
