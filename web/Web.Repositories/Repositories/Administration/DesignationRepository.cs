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
    public interface IDesignationRepository
    {
        Task<IEnumerable<DesignationDto>> GetAllDesignationAsync();
        Task<DesignationDto> GetDesignationByIdAsync(int id);
        int Insert(Designation entity);
        int Update(Designation entity);
        int Delete(int id);
    }

    public class DesignationRepository:IDesignationRepository
    {
        private IDapperManager _dapperManager;
        private IBaseRepo<Designation> _designationRepo;

        public DesignationRepository(IDapperManager dapperManager,
           IBaseRepo<Designation> designationRepo)
        {
            _dapperManager = dapperManager;
            _designationRepo = designationRepo;
        }

        public async Task<IEnumerable<DesignationDto>> GetAllDesignationAsync()
        {
            return (await _dapperManager.QueryAsync<DesignationDto>("SELECT * FROM Designation"));
        }

        public async Task<DesignationDto> GetDesignationByIdAsync(int id)
        {
            return (await _dapperManager.QuerySingleAsync<DesignationDto>("SELECT * FROM Designation WHERE DesignationId=@id", new { id }));
        }

        public int Insert(Designation entity)
        {
            entity.CreatedDate = DateTime.Now;
            return _designationRepo.Insert(entity);
        }

        public int Update(Designation entity)
        {
            return _designationRepo.Update(entity);
        }

        public int Delete(int id)
        {
            return _designationRepo.Delete(id);
        }
    }
}
