using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Database;
using Web.Database.BaseRepo;
using Web.Entity.Dto.UserSite;
using Web.Entity.Entity;

namespace Web.Repositories.Repositories.Customer
{
    public interface ICustomerQueryRepository
    {
        int Insert(CustomerQuery entity);
        Task<IEnumerable<CustomerQueryDto>> GetAllQueryAsync();
        Task<CustomerQueryDto> GetQueryByIdAsync(int id);
    }
    public class CustomerQueryRepository: ICustomerQueryRepository
    {
        private IDapperManager _dapperManager;
        private IBaseRepo<CustomerQuery> _customerRepo;

        public CustomerQueryRepository(IDapperManager dapperManager,
            IBaseRepo<CustomerQuery> customerRepo)
        {
            _dapperManager = dapperManager;
            _customerRepo = customerRepo;
        }

        public int Insert(CustomerQuery entity)
        {
            entity.CreatedDate = DateTime.Now;
            return _customerRepo.Insert(entity);
        }

        public async Task<IEnumerable<CustomerQueryDto>> GetAllQueryAsync()
        {
            return (await _dapperManager.QueryAsync<CustomerQueryDto>("SELECT * FROM CustomerQuery"));
        }

        public async Task<CustomerQueryDto> GetQueryByIdAsync(int id)
        {
            return (await _dapperManager.QuerySingleAsync<CustomerQueryDto>("SELECT * FROM CustomerQuery WHERE Id=@id",new { id }));
        }
    }
}
