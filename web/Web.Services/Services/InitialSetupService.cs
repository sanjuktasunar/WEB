using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Web.Entity.Dto;
using Web.Repositories.Repositories;
using HttpContext = System.Web.HttpContext;

namespace Web.Services.Services
{
    public class InitialSetupService
    {
        private InitialSetupRepository _initialSetupRepository=new InitialSetupRepository();

        public string GetCurrentVersionInfo()
        {
            var versionData = _initialSetupRepository.GetVersionInfo().FirstOrDefault().Version.ToString();
            string version = "";
            for (int i = 0; i < versionData.Length; i++)
            {
                if (i == 0)
                    version = "V" + versionData[i];
                else
                    version = version + "." + versionData[i];
            }
            return version;
        }

        public IEnumerable<MenuAccessPermissionDto> GetMenuAccessPermissionForLoginUser()
        {
            return (_initialSetupRepository.GetMenuAccessPermissionByUserId());
        }

        public MenuAccessPermissionDto GetMenuPermissionForLoginUser(string checkMenuName)
        {
            int UserId = Convert.ToInt32(HttpContext.Current.Session["UserId"]);
            var menus = _initialSetupRepository.GetPermissionForStaffByMenuAsync(UserId, checkMenuName.Trim());
            if (menus.Count() == 0)
                return new MenuAccessPermissionDto { CheckMenuName=checkMenuName };
            var menu = menus.FirstOrDefault();
            if (menu.AdminAccess)
                menu.ReadAccess = menu.WriteAccess = menu.ModifyAccess = menu.DeleteAccess = true;
            return menu;
        }
        public OrganizationInfoDto GetOrganizationInfo()
        {
            return _initialSetupRepository.GetOrganizationInfo();
        }

        //public string ConvertToAD(string)
    }
}
