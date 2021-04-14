using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Entity.Entity;
using Web.Repositories.Repositories.Administration;

namespace Web.Services.Services
{
    public interface IMenusService
    {
        Task<List<Menus>> GetMenusAsync();
    }
    public class MenusService : IMenusService
    {
        private IMenusRepository _menusRepository;

        public MenusService(IMenusRepository menusRepository)
        {
            _menusRepository = menusRepository;
        }

        public async Task<List<Menus>> GetMenusAsync()
        {
            var menus = await _menusRepository.GetMenusAsync();
            return menus;
        }
    }
}
