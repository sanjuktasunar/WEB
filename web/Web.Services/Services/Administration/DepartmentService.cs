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
    public interface IDepartmentService
    {
        Task<IEnumerable<DepartmentDto>> GetAllDepartment();
        Task<DepartmentDto> GetDepartmentById(int id);
        string Insert(DepartmentDto dto);
        string Update(DepartmentDto dto);
        string Delete(int id);
    }

    public class DepartmentService: IDepartmentService
    {
        private IDepartmentRepository _departmentRepository;
        private IMessageClass _messageClass;
        public DepartmentService(IDepartmentRepository departmentRepository,
            IMessageClass messageClass)
        {
            _departmentRepository = departmentRepository;
            _messageClass = messageClass;
        }

        public async Task<IEnumerable<DepartmentDto>> GetAllDepartment()
        {
            return (await _departmentRepository.GetAllDepartmentAsync());
        }

        public async Task<DepartmentDto> GetDepartmentById(int id)
        {
            return (await _departmentRepository.GetDepartmentByIdAsync(id));
        }

        public string Insert(DepartmentDto dto)
        {
            string message = "";
            try
            {
                int result = _departmentRepository.Insert(dto.ToEntity());
                message = _messageClass.ShowSuccessMessage(result);
            }
            catch (SqlException ex)
            {
                message = _messageClass.ShowErrorMessage(string.Format("{0} ~ {1}", ex.Number.ToString(), ex.Message));
            }
            return message;
        }

        public string Update(DepartmentDto dto)
        {
            string message = "";
            try
            {
                int result = _departmentRepository.Update(dto.ToEntity());
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
                int result = _departmentRepository.Delete(id);
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
