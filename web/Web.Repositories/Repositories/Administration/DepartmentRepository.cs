using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Database;
using Web.Database.BaseRepo;
using Web.Entity.Dto;
using Web.Entity.Entity;

namespace Web.Repositories.Repositories.Administration
{
    public interface IDepartmentRepository
    {
        Task<IEnumerable<DepartmentDto>> GetAllDepartmentAsync();
        Task<DepartmentDto> GetDepartmentByIdAsync(int id);
        int Insert(Department entity);
        int Update(Department entity);
        int Delete(int id);
    }

    public class DepartmentRepository:IDepartmentRepository
    {
        private IDapperManager _dapperManager;
        private IBaseRepo<Department> _departmentRepo;

        public DepartmentRepository(IDapperManager dapperManager,
            IBaseRepo<Department> departmentRepo)
        {
            _dapperManager = dapperManager;
            _departmentRepo = departmentRepo;
        }
        public async Task<IEnumerable<DepartmentDto>> GetAllDepartmentAsync()
        {
            return (await _dapperManager.QueryAsync<DepartmentDto>("SELECT * FROM Department"));
        }

        public async Task<DepartmentDto> GetDepartmentByIdAsync(int id)
        {
            return (await _dapperManager.QuerySingleAsync<DepartmentDto>("SELECT * FROM Department WHERE DepartmentId=@id", new { id }));
        }

        public int Insert(Department entity)
        {
            entity.CreatedDate = DateTime.Now;
            return _departmentRepo.Insert(entity);
        }

        public int Update(Department entity)
        {
            return _departmentRepo.Update(entity);
        }

        public int Delete(int id)
        {
            return _departmentRepo.Delete(id);
        }
    }
}
