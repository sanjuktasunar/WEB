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
    public interface IFiscalYearRepository
    {
        Task<IEnumerable<FiscalYearDto>> GetAllFiscalYearAsync();
        Task<IEnumerable<FiscalYearDto>> GetAllActiveFiscalYearAsync();
        Task<FiscalYearDto> GetFiscalYearByIdAsync(int id);
        Task<FiscalYearDto> GetCurrentFiscalYearAsync();
        void MakeIsCurrentFalse(SqlConnection con, IDbTransaction transaction);
        void MakeIsCurrentTrueById(int id, SqlConnection con, IDbTransaction transaction);
        int Insert(FiscalYear entity, IDbTransaction transaction, SqlConnection con);
        int Update(FiscalYear entity, IDbTransaction transaction, SqlConnection con);
        int Delete(SqlConnection con, IDbTransaction transaction, int id);
    }
    public class FiscalYearRepository:IFiscalYearRepository
    {
        private IDapperManager _dapperManager;
        private IBaseRepo<FiscalYear> _fiscalYearRepo;

        public FiscalYearRepository(IDapperManager dapperManager,
            IBaseRepo<FiscalYear> fiscalYearRepo)
        {
            _dapperManager = dapperManager;
            _fiscalYearRepo = fiscalYearRepo;
        }

        public async Task<IEnumerable<FiscalYearDto>> GetAllFiscalYearAsync()
        {
            return (await _dapperManager.QueryAsync<FiscalYearDto>("SELECT * FROM FiscalYear"));
        }

        public async Task<IEnumerable<FiscalYearDto>> GetAllActiveFiscalYearAsync()
        {
            return (await _dapperManager.QueryAsync<FiscalYearDto>("SELECT * FROM FiscalYear WHERE Status=1 ORDER BY IsCurrent DESC"));
        }

        public async Task<FiscalYearDto> GetFiscalYearByIdAsync(int id)
        {
            return (await _dapperManager.QuerySingleAsync<FiscalYearDto>("SELECT * FROM FiscalYear WHERE FiscalYearId=@id", new { id }));
        }

        public async Task<FiscalYearDto> GetCurrentFiscalYearAsync()
        {
            return (await _dapperManager.QuerySingleAsync<FiscalYearDto>("SELECT * FROM FiscalYear WHERE IsCurrent=1 AND Status=1"));
        }

        public void MakeIsCurrentFalse(SqlConnection con, IDbTransaction transaction)
        {
            _dapperManager.ExecuteScalar<FiscalYear>(con, "UPDATE FiscalYear SET IsCurrent=0  WHERE IsCurrent=1",null, transaction);
        }

        public void MakeIsCurrentTrueById(int id,SqlConnection con, IDbTransaction transaction)
        {
            _dapperManager.ExecuteScalar<FiscalYear>(con, "UPDATE FiscalYear SET IsCurrent=1  WHERE FiscalYearId=@id",new { id }, transaction);
        }

        public int Insert(FiscalYear entity, IDbTransaction transaction, SqlConnection con)
        {
            entity.CreatedBy = Convert.ToInt32(HttpContext.Current.Session["UserId"]);
            entity.CreatedDate = DateTime.Now;
            return (_fiscalYearRepo.Insert(entity, transaction, con));
        }

        public int Update(FiscalYear entity, IDbTransaction transaction, SqlConnection con)
        {
            entity.UpdatedBy = Convert.ToInt32(HttpContext.Current.Session["UserId"]);
            entity.UpdatedDate = DateTime.Now;
            return (_fiscalYearRepo.Update(entity, transaction, con));
        }

        public int Delete(SqlConnection con, IDbTransaction transaction, int id)
        {
            return (_fiscalYearRepo.Delete(id, transaction, con));
        }
    }
}
