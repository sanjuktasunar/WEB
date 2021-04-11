using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Data;
using Web.Entity.Entity;

namespace Web.Repositories.Repositories.Administration
{
    public interface IMenusRepository
    {
        Task<List<Menus>> GetMenusAsync();
    }
    public class MenusRepository:IMenusRepository
    {
        private IDapperManager _dapperManager;
        public MenusRepository(IDapperManager dapperManager)
        {
            _dapperManager = dapperManager;
        }

        public async Task<List<Menus>> GetMenusAsync()
        {
            var result = await _dapperManager.QueryAsync<Menus>("select * from Menus");
            return result.ToList();
        }
    }
}
