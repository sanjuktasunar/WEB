using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Web.Database;
using Web.Database.BaseRepo;
using Web.Entity.Dto;
using Web.Entity.Entity;

namespace Web.Repositories.Repositories.Account
{
    public interface IUnitRepository
    {
        Task<IEnumerable<UnitDto>> GetAllUnitAsync();
        Task<IEnumerable<UnitDto>> GetActiveUnitAsync();
        Task<UnitDto> GetUnitByIdAsync(int id);
        int Insert(Unit entity);
        int Update(Unit entity);
        int Delete(int id);
    }
    public class UnitRepository:IUnitRepository
    {
        private IDapperManager _dapperManager;
        private IBaseRepo<Unit> _unitRepo;
        public UnitRepository(IDapperManager dapperManager,
            IBaseRepo<Unit> unitRepo)
        {
            _dapperManager = dapperManager;
            _unitRepo = unitRepo;
        }

        public async Task<IEnumerable<UnitDto>> GetAllUnitAsync()
        {
            return (await _dapperManager.QueryAsync<UnitDto>("SELECT * FROM Unit"));
        }

        public async Task<IEnumerable<UnitDto>> GetActiveUnitAsync()
        {
            return (await _dapperManager.QueryAsync<UnitDto>("SELECT * FROM Unit WHERE Status=1"));
        }

        public async Task<UnitDto> GetUnitByIdAsync(int id)
        {
            return (await _dapperManager.QuerySingleAsync<UnitDto>("SELECT * FROM Unit WHERE UnitId=@id",new { id }));
        }

        public int Insert(Unit entity)
        {
            entity.CreatedBy = Convert.ToInt32(HttpContext.Current.Session["UserId"]);
            entity.CreatedDate=DateTime.Now;
            return _unitRepo.Insert(entity);
        }

        public int Update(Unit entity)
        {
            entity.UpdatedBy = Convert.ToInt32(HttpContext.Current.Session["UserId"]);
            entity.UpdatedDate = DateTime.Now;
            return _unitRepo.Update(entity);
        }

        public int Delete(int id)
        {
            return _unitRepo.Delete(id);
        }
    }
}
