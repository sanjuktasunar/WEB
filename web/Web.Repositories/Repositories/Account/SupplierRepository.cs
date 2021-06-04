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
    public interface ISupplierRepository
    {
        Task<IEnumerable<SupplierDto>> GetAllSupplierAsync();
        Task<IEnumerable<SupplierDto>> GetAllActiveSupplierAsync();
        Task<SupplierDto> GetSupplierByIdAsync(int id);
        int Insert(Supplier entity);
        int Update(Supplier entity);
        int Delete(int id);
    }

    public class SupplierRepository:ISupplierRepository
    {
        private readonly IDapperManager _dapperManager;
        private readonly IBaseRepo<Supplier> _supplierRepo;
        public SupplierRepository(IDapperManager dapperManager,
                IBaseRepo<Supplier> supplierRepo)
        {
            _dapperManager = dapperManager;
            _supplierRepo = supplierRepo;
        }
        public async Task<IEnumerable<SupplierDto>> GetAllSupplierAsync()
        {
            return (await _dapperManager.QueryAsync<SupplierDto>("SELECT * FROM Supplier"));
        }
        public async Task<IEnumerable<SupplierDto>> GetAllActiveSupplierAsync()
        {
            return (await _dapperManager.QueryAsync<SupplierDto>("SELECT * FROM AccountHead WHERE Supplier=1"));
        }

        public async Task<SupplierDto> GetSupplierByIdAsync(int id)
        {
            return (await _dapperManager.QuerySingleAsync<SupplierDto>("SELECT * FROM Supplier WHERE SupplierId=@id", new { id }));
        }
        public int Insert(Supplier entity)
        {
            entity.CreatedBy = Convert.ToInt32(HttpContext.Current.Session["UserId"]);
            entity.CreatedDate = DateTime.Now;
            return _supplierRepo.Insert(entity);
        }

        public int Update(Supplier entity)
        {
            entity.UpdatedBy = Convert.ToInt32(HttpContext.Current.Session["UserId"]);
            entity.UpdatedDate = DateTime.Now;
            return _supplierRepo.Update(entity);
        }

        public int Delete(int id)
        {
            return _supplierRepo.Delete(id);
        }
    }
}
