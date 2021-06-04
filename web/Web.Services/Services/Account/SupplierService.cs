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
    public interface ISupplierService
    {
        Task<IEnumerable<SupplierDto>> GetAllSupplierAsync();
        Task<IEnumerable<SupplierDto>> GetAllActiveSupplierAsync();
        Task<SupplierDto> GetSupplierByIdAsync(int id);
        string Insert(SupplierDto dto);
        Task<string> Update(SupplierDto dto);
        string Delete(int id);
    }
    public class SupplierService:ISupplierService
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly IMessageClass _messageClass;
        public SupplierService(
            IMessageClass messageClass,
            ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
            _messageClass = messageClass;
        }

        public async Task<IEnumerable<SupplierDto>> GetAllSupplierAsync()
        {
            return (await _supplierRepository.GetAllSupplierAsync());
        }

        public async Task<IEnumerable<SupplierDto>> GetAllActiveSupplierAsync()
        {
            return (await _supplierRepository.GetAllActiveSupplierAsync());
        }

        public async Task<SupplierDto> GetSupplierByIdAsync(int id)
        {
            return (await _supplierRepository.GetSupplierByIdAsync(id));
        }

        public string Insert(SupplierDto dto)
        {
            string message = "";
            try
            {
                int result = _supplierRepository.Insert(dto.ToEntity());
                message = _messageClass.ShowSuccessMessage(result);
            }
            catch (SqlException ex)
            {
                message = _messageClass.ShowErrorMessage(string.Format("{0} ~ {1}", ex.Number.ToString(), ex.Message));
            }
            return message;
        }

        public async Task<string> Update(SupplierDto dto)
        {
            string message = "";
            try
            {
                var unit = await GetSupplierByIdAsync(dto.SupplierId);
                if (unit is null)
                    return null;

                dto.CreatedBy = unit.CreatedBy;
                dto.CreatedDate = unit.CreatedDate;
                int result = _supplierRepository.Update(dto.ToEntity());
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
                int result = _supplierRepository.Delete(id);
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
