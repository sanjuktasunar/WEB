using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Entity.Dto.UserSite;
using Web.Repositories.Repositories.Customer;
using Web.Repositories.Utitlities;
using Web.Services.Mapping;

namespace Web.Services.Services.Customer
{
    public interface ICustomerQueryService
    {
        string Insert(CustomerQueryDto dto);
    }

    public class CustomerQueryService:ICustomerQueryService
    {
        private ICustomerQueryRepository _customerQueryRepository;
        MessageClass _message = new MessageClass();
        public CustomerQueryService(ICustomerQueryRepository customerQueryRepository)
        {
            _customerQueryRepository = customerQueryRepository;
        }

        public string Insert(CustomerQueryDto dto)
        {
            string message = "";
            try
            {
                var entity = dto.ToEntity();
                int result = _customerQueryRepository.Insert(entity);
                message = _message.ShowCustomerMessage(result);
            }
            catch (SqlException ex)
            {
                message = _message.ShowErrorMessage(string.Format("{0} ~ {1}", ex.Number.ToString(), ex.Message));
            }

            return message;
        }
    }
}
