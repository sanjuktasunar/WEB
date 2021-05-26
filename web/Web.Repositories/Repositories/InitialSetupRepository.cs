using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Web.Database;
using Web.Database.BaseRepo;
using Web.Entity.Dto;

namespace Web.Repositories.Repositories
{
    public class InitialSetupRepository
    {
        private BaseRepo<MenusDto> _menusRepo = new BaseRepo<MenusDto>();
        private BaseRepo<MenuAccessPermissionDto> _menuAccessPermissionDto = new BaseRepo<MenuAccessPermissionDto>();
        private BaseRepo<VersionInfoDto> _versionInfoRepo = new BaseRepo<VersionInfoDto>();
        private BaseRepo<OrganizationInfoDto> _organizationInfoRepo = new BaseRepo<OrganizationInfoDto>();

        public IEnumerable<VersionInfoDto> GetVersionInfo()
        {
            var dto = _versionInfoRepo.Query<VersionInfoDto>("SELECT * FROM VersionInfo ORDER BY Version DESC");
            return dto;
        }

        public IEnumerable<MenuAccessPermissionDto> GetMenuAccessPermissionByUserId()
        {
            int UserId = Convert.ToInt32(HttpContext.Current.Session["UserId"]);
            int LangId = Convert.ToInt32(HttpContext.Current.Session["LangId"]);
            var MenusDtos = _menusRepo.Query<MenuAccessPermissionDto>("SELECT * FROM Menus WHERE ParentMenuId IS NULL AND Status=1");

            var menuList = new List<MenuAccessPermissionDto>();
            foreach (var menuDto in MenusDtos)
            {
                var childMenuDtos = _menusRepo.StoredProcedure<MenuAccessPermissionDto>("Proc_GetMenuAccessPermssionByUserId", new { UserId = UserId, ParentMenuId = menuDto.MenuId, LangId = LangId });
                if (childMenuDtos.Count() > 0)
                {
                    menuDto.GetChildMenus = childMenuDtos;
                    if (LangId == 1)
                    {
                        menuDto.MenuName = menuDto.MenuNameEnglish;
                        foreach (var x in menuDto.GetChildMenus)
                            x.MenuName = x.MenuNameEnglish;
                    }
                    else
                    {
                        menuDto.MenuName = menuDto.MenuNameNepali;
                        foreach (var x in menuDto.GetChildMenus)
                            x.MenuName = x.MenuNameNepali;
                    }
                    menuList.Add(menuDto);
                }
            }
            return menuList;
        }

        public IEnumerable<MenuAccessPermissionDto> GetPermissionForStaffByMenuAsync(int StaffId, string CheckMenuName)
        {
            var result = _menuAccessPermissionDto.Query<MenuAccessPermissionDto>("SELECT * FROM MenuAccessPermissionView WHERE StaffId=@StaffId AND CheckMenuName=@CheckMenuName",
                new { StaffId, CheckMenuName });
            return result;
        }

        public OrganizationInfoDto GetOrganizationInfo()
        {
            var dto = _organizationInfoRepo.Query<OrganizationInfoDto>("SELECT * FROM OrganizationInfo").FirstOrDefault();
            return dto;
        }
    }
}
