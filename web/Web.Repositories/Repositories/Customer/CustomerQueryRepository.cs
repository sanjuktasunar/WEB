using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Database;
using Web.Database.BaseRepo;
using Web.Entity.Entity;

namespace Web.Repositories.Repositories.Customer
{
    public interface ICustomerQueryRepository
    {
        int Insert(CustomerQuery entity);
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
    }
}
