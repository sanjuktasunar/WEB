using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Database;
using Web.Database.BaseRepo;
using Web.Entity.Dto;
using Web.Entity.Entity;

namespace Web.Repositories.Repositories.Administration
{
    public interface IRoleRepository
    {
        Task<IEnumerable<RoleDto>> GetAllRolesAsync();
        Task<RoleDto> GetRoleByIdAsync(int id);
        int Insert(Role entity);
        int Update(Role entity);
        int Delete(int id);
        Task<IEnumerable<MenuAccessPermissionDto>> GetMenuAccessPermissionsByRoleIdAsync(int id);
        Task<MenuAccessPermissionDto> GetMenuAccessByIdAsync(int id);
        int InsertMenuAccess(MenuAccessPermission entity, IDbTransaction transaction, SqlConnection con);
        int UpdateMenuAccess(MenuAccessPermission entity, IDbTransaction transaction, SqlConnection con);
    }

    public class RoleRepository: IRoleRepository
    {
        private IDapperManager _dapperManager;
        private IBaseRepo<Role> _roleRepo;
        BaseRepo<MenuAccessPermission> _menuAccessRepo = new BaseRepo<MenuAccessPermission>();

        public RoleRepository(IDapperManager dapperManager,
            IBaseRepo<Role> roleRepo)
        {
            _dapperManager = dapperManager;
            _roleRepo = roleRepo;
        }

        public async Task<IEnumerable<RoleDto>> GetAllRolesAsync()
        {
            return (await _dapperManager.QueryAsync<RoleDto>("SELECT * FROM Role"));
        }

        public async Task<RoleDto> GetRoleByIdAsync(int id)
        {
            return (await _dapperManager.QuerySingleAsync<RoleDto>("SELECT * FROM Role WHERE RoleId=@id", new { id }));
        }

        public int Insert(Role entity)
        {
            entity.CreatedDate = DateTime.Now;
            return _roleRepo.Insert(entity);
        }

        public int Update(Role entity)
        {
            return _roleRepo.Update(entity);
        }

        public int Delete(int id)
        {
            return _roleRepo.Delete(id);
        }

        public async Task<IEnumerable<MenuAccessPermissionDto>> GetMenuAccessPermissionsByRoleIdAsync(int id)
        {
            var dto = await _dapperManager.StoredProcedureAsync<MenuAccessPermissionDto>("[dbo].[MenuAccessPermissionForRole]", new { RoleId = id });
            return dto;
        }

        public async Task<MenuAccessPermissionDto> GetMenuAccessByIdAsync(int id)
        {
            return (await _dapperManager.QuerySingleAsync<MenuAccessPermissionDto>("SELECT * FROM MenuAccessPermission WHERE MenuAccessPermissionId=@id", new { id }));
        }

        public int InsertMenuAccess(MenuAccessPermission entity, IDbTransaction transaction, SqlConnection con)
        {
            return (_menuAccessRepo.Insert(entity, transaction, con));
        }

        public int UpdateMenuAccess(MenuAccessPermission entity, IDbTransaction transaction, SqlConnection con)
        {
            return (_menuAccessRepo.Update(entity, transaction, con));
        }
    }
}
