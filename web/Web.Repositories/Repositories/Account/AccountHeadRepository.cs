using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Database;
using Web.Database.BaseRepo;
using Web.Entity.Dto;
using Web.Entity.Entity;

namespace Web.Repositories.Repositories.Account
{
    public interface IAccountHeadRepository
    {
        Task<IEnumerable<AccountHeadDto>> GetAllAccountHeadAsync();
        Task<IEnumerable<AccountHeadDto>> GetAllActiveAccountHeadAsync();
        Task<AccountHeadDto> GetAllAccountHeadByIdAsync(int id);
        int Insert(AccountHead entity);
        int Update(AccountHead entity);
        int Delete(int id);
    }

    public class AccountHeadRepository:IAccountHeadRepository
    {
        private IDapperManager _dapperManager;
        private IBaseRepo<AccountHead> _accountHeadRepo;
        public AccountHeadRepository(IDapperManager dapperManager, IBaseRepo<AccountHead> accountHeadRepo)
        {
            _dapperManager = dapperManager;
            _accountHeadRepo = accountHeadRepo;

        }
        public async Task<IEnumerable<AccountHeadDto>> GetAllAccountHeadAsync()
        {
            return (await _dapperManager.QueryAsync<AccountHeadDto>("SELECT * FROM AccountHead"));
        }
        public async Task<IEnumerable<AccountHeadDto>> GetAllActiveAccountHeadAsync()
        {
            return (await _dapperManager.QueryAsync<AccountHeadDto>("SELECT * FROM AccountHead WHERE Status=1"));
        }
        public async Task<AccountHeadDto> GetAllAccountHeadByIdAsync(int id)
        {
            return (await _dapperManager.QuerySingleAsync<AccountHeadDto>("SELECT * FROM AccountHead WHERE AccountHeadId=@id",new { id }));
        }

        public int Insert(AccountHead entity)
        {
            return _accountHeadRepo.Insert(entity);
        }

        public int Update(AccountHead entity)
        {
            return _accountHeadRepo.Update(entity);
        }

        public int Delete(int id)
        {
            return _accountHeadRepo.Delete(id);
        }
    }
}
