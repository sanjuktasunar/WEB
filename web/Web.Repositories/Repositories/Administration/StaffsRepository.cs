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
using Web.Repositories.Interface;

namespace Web.Repositories.Repositories.Administration
{
    public interface IStaffsRepository
    {
        Task<IEnumerable<StaffsDto>> GetStaffListAsync();
        Task<StaffsDto> GetStaffByIdAsync(int id);
        int Insert(Staffs entity,IDbTransaction transaction, SqlConnection con);
        int Update(Staffs entity,IDbTransaction transaction, SqlConnection con);
        int Delete(int id, IDbTransaction transaction);
    }

    public class StaffsRepository:IStaffsRepository
    {
        private IDapperManager _dapperManager;
        BaseRepo<Staffs> _staffsRepo = new BaseRepo<Staffs>();
        public StaffsRepository(IDapperManager dapperManager)
        {
            _dapperManager = dapperManager;
        }
        
        public async Task<IEnumerable<StaffsDto>> GetStaffListAsync()
        {
            var dto =await _dapperManager.QueryAsync<StaffsDto>("SELECT * FROM StaffsView");
            return dto;
        }

        public async Task<StaffsDto> GetStaffByIdAsync(int id)
        {
            var dto = (await _dapperManager.QueryAsync<StaffsDto>("SELECT * FROM StaffsView WHERE StaffId=@id", new { id }));

            return dto.FirstOrDefault();
        }

        public int Insert(Staffs entity,IDbTransaction transaction,SqlConnection con)
        {
            return (_staffsRepo.Insert(entity,transaction,con));
        }

        public int Update(Staffs entity,IDbTransaction transaction, SqlConnection con)
        {
            return (_staffsRepo.Update(entity,transaction,con));
        }

        public int Delete(int id, IDbTransaction transaction)
        {
            return (_staffsRepo.Delete(id,transaction));
        }
    }
}
