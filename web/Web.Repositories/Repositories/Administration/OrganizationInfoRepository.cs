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
    public interface IOrganizationInfoRepository
    {
        Task<OrganizationInfoDto> GetOrganizationInfoAsync();
        int Insert(OrganizationInfo entity);
        int Update(OrganizationInfo entity);
    }

    public class OrganizationInfoRepository:IOrganizationInfoRepository
    {
        private IDapperManager _dapperManager;
        private IBaseRepo<OrganizationInfo> _organizationInfoRepo;

        public OrganizationInfoRepository(IDapperManager dapperManager, IBaseRepo<OrganizationInfo> organizationInfoRepo)
        {
            _dapperManager = dapperManager;
            _organizationInfoRepo = organizationInfoRepo;
        }

        public async Task<OrganizationInfoDto> GetOrganizationInfoAsync()
        {
            var obj = await _dapperManager.QuerySingleAsync<OrganizationInfoDto>("SELECT * FROM OrganizationInfo");
            return obj;
        }

        public int Insert(OrganizationInfo entity)
        {
            return _organizationInfoRepo.Insert(entity);
        }

        public int Update(OrganizationInfo entity)
        {
            return _organizationInfoRepo.Update(entity);
        }
    }
}
