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
    public interface IOrganizationInfoService
    {
        Task<OrganizationInfoDto> GetOrganizationInfo();
        string Insert(OrganizationInfoDto dto);
        string Update(OrganizationInfoDto dto);
    }

    public class OrganizationInfoService:IOrganizationInfoService
    {
        private readonly OrganizationInfoRepository _organizationInfoRepository;
        private readonly IMessageClass _messageClass;
        public OrganizationInfoService(OrganizationInfoRepository organizationInfoRepository, IMessageClass messageClass)
        {
            _organizationInfoRepository = organizationInfoRepository;
            _messageClass = messageClass;
        }

        public async Task<OrganizationInfoDto> GetOrganizationInfo()
        {
            var obj = await _organizationInfoRepository.GetOrganizationInfoAsync();
            return obj;
        }

        public string Insert(OrganizationInfoDto dto)
        {
            string message = "";
            try
            {
                int result = _organizationInfoRepository.Insert(dto.ToEntity());
                message = _messageClass.ShowSuccessMessage(result);
            }
            catch (SqlException ex)
            {
                message = _messageClass.ShowErrorMessage(string.Format("{0} ~ {1}", ex.Number.ToString(), ex.Message));
            }
            return message;
        }

        public string Update(OrganizationInfoDto dto)
        {
            string message = "";
            try
            {
                int result = _organizationInfoRepository.Update(dto.ToEntity());
                message = _messageClass.ShowSuccessMessage(result);
            }
            catch (SqlException ex)
            {
                message = _messageClass.ShowErrorMessage(string.Format("{0} ~ {1}", ex.Number.ToString(), ex.Message));
            }
            return message;
        }
    }
}
