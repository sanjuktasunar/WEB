using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Web.Database;
using Web.Database.BaseRepo;
using Web.Entity.Dto;
using Web.Entity.Entity;

namespace Web.Repositories.Repositories.Administration
{
    public interface IMenusRepository
    {
        Task<IEnumerable<MenusDto>> GetMenusAsync();
        IEnumerable<MenuAccessPermissionDto> GetMenusByUserId(int? UserId);
        Task<MenusDto> GetMenusByIdAsync(int id);
        Task<IEnumerable<MenusDto>> GetParentMenusAsync();
        int Insert(Menus entity);
        int Update(Menus entity);
        int Delete(int id);
        //int Delete(int id, SqlConnection con, IDbTransaction transaction);
        Task MenuOrder(Menus entity, SqlConnection con, IDbTransaction transaction);
        Task MenuOrderOnDelete(int menuId, SqlConnection con, IDbTransaction transaction);
    }
    public class MenusRepository:IMenusRepository
    {
        private IDapperManager _dapperManager;
        BaseRepo<Menus> _menusRepo=new BaseRepo<Menus>();
        public MenusRepository(IDapperManager dapperManager)
        {
            _dapperManager = dapperManager;
        }
        public async Task<IEnumerable<MenusDto>> GetMenusAsync()
        {
            var result = await _dapperManager.QueryAsync<MenusDto>("SELECT * from MenuView");
            return result;
        }
        public IEnumerable<MenuAccessPermissionDto> GetMenusByUserId(int? UserId)
       {
            if (UserId == null)
                UserId = Convert.ToInt32(HttpContext.Current.Session["UserId"]);

            int LangId = Convert.ToInt32(HttpContext.Current.Session["LangId"]);
            var MenusDtos =  _menusRepo.Query<MenuAccessPermissionDto>("SELECT * FROM Menus WHERE ParentMenuId IS NULL AND Status=1");
           
            var menuList = new List<MenuAccessPermissionDto>();
            foreach(var menuDto in MenusDtos)
            {
                var childMenuDtos =  _menusRepo.StoredProcedure<MenuAccessPermissionDto>("Proc_GetMenuAccessPermssionByUserId", new { UserId=UserId,ParentMenuId=menuDto.MenuId,LangId=LangId });
                if(childMenuDtos.Count()>0)
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

        public async Task<MenusDto> GetMenusByIdAsync(int id)
        {
            var menus =await _dapperManager.QueryAsync<MenusDto>("SELECT * FROM Menus WHERE MenuId=@id",
                new { id });
            if (menus.Count() > 0)
                return menus.FirstOrDefault();
            return null;
        }
        public async Task<IEnumerable<MenusDto>> GetParentMenusAsync()
        {
            var menus = await _dapperManager.QueryAsync<MenusDto>("SELECT * FROM Menus WHERE ParentMenuId IS NULL");
            return menus;
        }

        public int Insert(Menus entity)
        {
            return (_menusRepo.Insert(entity));
        }

        public int Update(Menus entity)
        {
            return (_menusRepo.Update(entity));
        }

        public int Delete(int id)
        {
            return (_menusRepo.Delete(id));
        }

        public async Task MenuOrder(Menus entity,SqlConnection con,IDbTransaction transaction)
        {
            var menus =await _dapperManager.QueryAsyncTrans<Menus>("SELECT * FROM Menus WHERE ParentMenuId=@ParentMenuId", new { entity.ParentMenuId},transaction,con);
            if(menus.Select(a=>a.MenuOrder).Contains(entity.MenuOrder))
            {
                foreach(var m in menus.Where(a=>a.MenuOrder>=entity.MenuOrder && a.MenuId!=entity.MenuId).OrderBy(a=>a.MenuOrder))
                {
                    m.MenuOrder = m.MenuOrder + 1;
                    _menusRepo.Update(m, transaction, con);
                }
            }
        }

        public async Task MenuOrderOnDelete(int menuId, SqlConnection con, IDbTransaction transaction)
        {
            var entity =await GetMenusByIdAsync(menuId);
            var menus = await _dapperManager.QueryAsyncTrans<Menus>("SELECT * FROM Menus WHERE ParentMenuId=@ParentMenuId AND MenuOrder>@MenuOrder", new { entity.ParentMenuId, entity.MenuOrder }, transaction, con);
            foreach (var m in menus)
            {
                m.MenuOrder = m.MenuOrder - 1;
                _menusRepo.Update(m, transaction, con);
            }
        }
    }
}
