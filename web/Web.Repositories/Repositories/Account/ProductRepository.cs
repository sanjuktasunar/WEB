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
using PagedList;
using PagedList.Mvc;

namespace Web.Repositories.Repositories.Account
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductDto>> GetAllProductAsync();
        Task<IEnumerable<ProductDto>> GetAllActiveParentProductAsync();
        Task<IEnumerable<ProductDto>> GetAllActiveChildProductAsync();
        Task<ProductDto> GetProductByIdAsync(int id);
        int Insert(Product entity);
        int Update(Product entity);
        int InsertProductPrice(ProductPrice entity);
        Task<IEnumerable<ProductPriceDto>> GetProductPriceByProductIdAsync(int productId);
        void UpdatePriceByUnitIdProductId(int unitId, int productId, SqlConnection con, IDbTransaction transaction);
        int Delete(int id);
        int DeletePrice(int id);
        void UpdatePrice(int productPriceId, SqlConnection con, IDbTransaction transaction);
        Task<ProductPriceDto> GetProductPriceById(int id);
        Task<IEnumerable<ProductImageDto>> GetProductImageByProductIdAsync(int ProductId);
        int InsertProductImage(ProductImage entity, IDbTransaction transaction, SqlConnection con);
        Task<ProductImageDto> GetProductImageById(int id);
        int DeleteProductImage(SqlConnection con, IDbTransaction transaction, int id);
        void ProductPrimaryImage(SqlConnection con, IDbTransaction transaction, int ProductId);
        int UpdateProductImage(ProductImage entity, IDbTransaction transaction, SqlConnection con);
        Task<IEnumerable<ProductDto>> GetChildProductByParentProductIdAsync(int parentProductId);
        Task<IEnumerable<ProductDto>> GetDisplayProductAsync();
        Task<IPagedList<ProductDto>> GetDisplayProductPaginationAsync(int pageNumber, int pageSize, string query,int? parentProductId);
    }
    public class ProductRepository:IProductRepository
    {
        private IDapperManager _dapperManager;
        private IBaseRepo<Product> _productRepo;
        private IBaseRepo<ProductPrice> _productPriceRepo;
        private IBaseRepo<ProductImage> _productImageRepo;
        public ProductRepository(IDapperManager dapperManager,
            IBaseRepo<Product> productRepo,
            IBaseRepo<ProductPrice> productPriceRepo,
            IBaseRepo<ProductImage> productImageRepo
            )
        {
            _dapperManager = dapperManager;
            _productRepo = productRepo;
            _productPriceRepo = productPriceRepo;
            _productImageRepo = productImageRepo;
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductAsync()
        {
            return (await _dapperManager.QueryAsync<ProductDto>("SELECT * FROM ProductView"));
        }

        public async Task<IEnumerable<ProductDto>> GetAllActiveParentProductAsync()
        {
            return (await _dapperManager.QueryAsync<ProductDto>("SELECT * FROM ProductView " +
                "WHERE ParentProductId IS NULL AND Status=1"));
        }

        public async Task<IEnumerable<ProductDto>> GetAllActiveChildProductAsync()
        {
            return (await _dapperManager.QueryAsync<ProductDto>("SELECT * FROM ProductView " +
                "WHERE ParentProductId IS NOT NULL AND Status=1"));
        }

        public async Task<IEnumerable<ProductDto>> GetChildProductByParentProductIdAsync(int parentProductId)
        {
            return (await _dapperManager.QueryAsync<ProductDto>("SELECT * FROM ProductView " +
                "WHERE ParentProductId=@id AND Status=1",new { id=parentProductId }));
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            return (await _dapperManager.QuerySingleAsync<ProductDto>("SELECT * FROM ProductView " +
                "WHERE ProductId=@id",new { id }));
        }

        public int Insert(Product entity)
        {
            entity.CreatedBy = Convert.ToInt32(HttpContext.Current.Session["UserId"]);
            entity.CreatedDate = DateTime.Now;
            return (_productRepo.Insert(entity));
        }
        public int Update(Product entity)
        {
            entity.UpdatedBy = Convert.ToInt32(HttpContext.Current.Session["UserId"]);
            entity.UpdatedDate = DateTime.Now;
            return (_productRepo.Update(entity));
        }

        public int InsertProductPrice(ProductPrice entity)
        {
            entity.CreatedBy=Convert.ToInt32(HttpContext.Current.Session["UserId"]);
            entity.UpdatedDate = DateTime.Now;
            entity.Status = true;
            return (_productPriceRepo.Insert(entity));
        }

        public async Task<IEnumerable<ProductPriceDto>> GetProductPriceByProductIdAsync(int productId)
        {
            return (await _dapperManager.QueryAsync<ProductPriceDto>("SELECT * FROM ProductPriceView " +
                "WHERE ProductId=@productId", new { productId }));
        }

        public async Task<IEnumerable<ProductPriceDto>> GetProductPriceByUnitIdAsync(int unitId)
        {
            return (await _dapperManager.QueryAsync<ProductPriceDto>("SELECT * FROM ProductPriceView " +
                "WHERE UnitId=@unitId", new { unitId }));
        }
        public void UpdatePriceByUnitIdProductId(int unitId,int productId, SqlConnection con,IDbTransaction transaction)
        {
            int UpdatedBy = Convert.ToInt32(HttpContext.Current.Session["UserId"]);
            var result = _dapperManager.ExecuteScalar<ProductPrice>(con,"UPDATE ProductPrice SET Status=0,UpdatedBy=@UpdatedBy WHERE UnitId=@unitId AND ProductId=@productId", new { UpdatedBy,unitId, productId },transaction);
        }
        public int Delete(int id)
        {
            return _productRepo.Delete(id);
        }

        public int DeletePrice(int id)
        {
            return _productPriceRepo.Delete(id);
        }

        public void UpdatePrice(int productPriceId,SqlConnection con, IDbTransaction transaction)
        {
            int UpdatedBy = Convert.ToInt32(HttpContext.Current.Session["UserId"]);
            _dapperManager.ExecuteScalar<ProductPrice>(con, "UPDATE ProductPrice SET Status=1,UpdatedBy=@UpdatedBy WHERE ProductPriceId=@productPriceId", new { UpdatedBy, productPriceId }, transaction);
        }

        public async Task<ProductPriceDto> GetProductPriceById(int id)
        {
            return (await _dapperManager.QuerySingleAsync<ProductPriceDto>("SELECT * FROM ProductPriceView " +
                "WHERE ProductPriceId=@id", new { id }));
        }

        public async Task<IEnumerable<ProductImageDto>> GetProductImageByProductIdAsync(int ProductId)
        {
            return (await _dapperManager.QueryAsync<ProductImageDto>("SELECT * FROM ProductImage WHERE ProductId=@ProductId",new { ProductId }));
        }

        public int InsertProductImage(ProductImage entity, IDbTransaction transaction, SqlConnection con)
        {
            return (_productImageRepo.Insert(entity, transaction, con));
        }

        public int UpdateProductImage(ProductImage entity, IDbTransaction transaction, SqlConnection con)
        {
            return (_productImageRepo.Update(entity, transaction, con));
        }

        public int DeleteProductImage(SqlConnection con,IDbTransaction transaction,int id)
        {
            return (_productImageRepo.Delete(id,transaction,con));
        }

        public void ProductPrimaryImage(SqlConnection con,IDbTransaction transaction,int ProductId)
        {
            _dapperManager.ExecuteScalar<ProductImage>(con,"UPDATE ProductImage SET IsPrimary=0 WHERE ProductId=@ProductId AND IsPrimary=1",new { ProductId },transaction);
        }

        public async Task<ProductImageDto> GetProductImageById(int id)
        {
            return (await _dapperManager.QuerySingleAsync<ProductImageDto>("SELECT * FROM ProductImage " +
               "WHERE ImageId=@id", new { id }));
        }

        public async Task<IEnumerable<ProductDto>> GetDisplayProductAsync()
        {
            return (await _dapperManager.QueryAsync<ProductDto>("SELECT * FROM DisplayProductView"));
        }

        public async Task<IPagedList<ProductDto>> GetDisplayProductPaginationAsync(int pageNumber, int pageSize,string query,int? parentProductId)
        {
            if (pageNumber <= 0)
                pageNumber = 1;
            var result= await _dapperManager.StoredProcedureAsync<ProductDto>("[dbo].[Sp_SearchProductForDisplay]",new { query=query, parentProductId= parentProductId });
            var data= result.ToPagedList(pageNumber, pageSize);
            return data;
        }
    }
}
