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
    public interface IAccountHeadService
    {
        Task<IEnumerable<AccountHeadDto>> GetAllAccountHead();
        Task<IEnumerable<AccountHeadDto>> GetAllActiveAccountHead();
        Task<AccountHeadDto> GetAccountHeadById(int id);
        string Insert(AccountHeadDto dto);
        Task<string> Update(AccountHeadDto dto);
        string Delete(int id);
    }

    public class AccountHeadService:IAccountHeadService
    {
        private IAccountHeadRepository _accountHeadRepository;
        private IMessageClass _messageClass;

        public AccountHeadService(IAccountHeadRepository accountHeadRepository,
            IMessageClass messageClass)
        {
            _accountHeadRepository = accountHeadRepository;
            _messageClass = messageClass;
        }

        public async Task<IEnumerable<AccountHeadDto>> GetAllAccountHead()
        {
            return (await _accountHeadRepository.GetAllAccountHeadAsync());
        }

        public async Task<IEnumerable<AccountHeadDto>> GetAllActiveAccountHead()
        {
            return (await _accountHeadRepository.GetAllActiveAccountHeadAsync());
        }

        public async Task<AccountHeadDto> GetAccountHeadById(int id)
        {
            return (await _accountHeadRepository.GetAllAccountHeadByIdAsync(id));
        }

        public string Insert(AccountHeadDto dto)
        {
            string message = "";
            try
            {
                int result = _accountHeadRepository.Insert(dto.ToEntity());
                message = _messageClass.ShowSuccessMessage(result);
            }
            catch (SqlException ex)
            {
                message = _messageClass.ShowErrorMessage(string.Format("{0} ~ {1}", ex.Number.ToString(), ex.Message));
            }
            return message;
        }

        public async Task<string> Update(AccountHeadDto dto)
        {
            string message = "";
            try
            {
                var accountHead = await GetAccountHeadById(dto.AccountHeadId);
                if (accountHead is null)
                    return null;

                int result = _accountHeadRepository.Update(dto.ToEntity());
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
                int result = _accountHeadRepository.Delete(id);
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
