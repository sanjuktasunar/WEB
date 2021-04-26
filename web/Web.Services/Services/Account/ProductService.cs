using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Entity.Dto;
using Web.Repositories.Interface;
using Web.Repositories.Repositories.Account;
using Web.Repositories.Utitlities;
using Web.Services.Mapping;

namespace Web.Services.Services.Account
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllProduct();
        Task<IEnumerable<ProductDto>> GetAllActiveParentProduct();
        Task<IEnumerable<ProductDto>> GetAllActiveChildProduct();
        Task<ProductDto> GetProductById(int id);
        string Insert(ProductDto dto);
        Task<string> Update(ProductDto dto);
        Task<ProductDto> DropDownList(ProductDto dto);
        Task<IEnumerable<ProductPriceDto>> GetProductPriceByProductId(int productId);
        Task<string> InsertProductPrice(ProductPriceDto dto);
        string Delete(int id);
        string DeletePrice(int id);
    }

    public class ProductService:IProductService
    {
        private IProductRepository _productRepository;
        private IMessageClass _messageClass;
        IBaseInterface _baseInterface;
        public ProductService(
            IMessageClass messageClass, 
            IProductRepository productRepository,
            IBaseInterface baseInterface)
        {
            _productRepository = productRepository;
            _messageClass = messageClass;
            _baseInterface = baseInterface;
        }

        public async Task<IEnumerable<ProductDto>> GetAllProduct()
        {
            return (await _productRepository.GetAllProductAsync());
        }

        public async Task<IEnumerable<ProductDto>> GetAllActiveParentProduct()
        {
            return (await _productRepository.GetAllActiveParentProductAsync());
        }

        public async Task<IEnumerable<ProductDto>> GetAllActiveChildProduct()
        {
            return (await _productRepository.GetAllActiveChildProductAsync());
        }

        public async Task<ProductDto> GetProductById(int id)
        {
            return (await _productRepository.GetProductByIdAsync(id));
        }

        public string Insert(ProductDto dto)
        {
            string message = "";
            try
            {
                int result = _productRepository.Insert(dto.ToEntity());
                message = _messageClass.ShowSuccessMessage(result);
            }
            catch (SqlException ex)
            {
                message = _messageClass.ShowErrorMessage(string.Format("{0} ~ {1}", ex.Number.ToString(), ex.Message));
            }
            return message;
        }

        public async Task<string> Update(ProductDto dto)
        {
            string message = "";
            try
            {
                var product = await GetProductById(dto.ProductId);
                if (product is null)
                    return null;

                dto.CreatedBy = product.CreatedBy;
                dto.CreatedDate = product.CreatedDate;
                int result = _productRepository.Update(dto.ToEntity());
                message = _messageClass.ShowSuccessMessage(product.ProductId);
            }
            catch (SqlException ex)
            {
                message = _messageClass.ShowErrorMessage(string.Format("{0} ~ {1}", ex.Number.ToString(), ex.Message));
            }
            return message;
        }

        public async Task<ProductDto> DropDownList(ProductDto dto)
        {
            if (dto is null)
                dto = new ProductDto();
            dto.GetActiveParentProduct = await GetAllActiveParentProduct();
            return dto;
        }

        public async Task<IEnumerable<ProductPriceDto>> GetProductPriceByProductId(int productId)
        {
            return (await _productRepository.GetProductPriceByProductIdAsync(productId));
        }

        public async Task<string> InsertProductPrice(ProductPriceDto dto)
        {
            var con = _baseInterface.GetConnection();
            var transaction = con.BeginTransaction();
            string message = "";
            try
            {
                var product = await GetProductById(dto.ProductId);
                if (product is null)
                    return null;

                _productRepository.UpdatePriceByUnitIdProductId(dto.UnitId, dto.ProductId, con, transaction);

                int result = _productRepository.InsertProductPrice(dto.ToEntity());
                message = _messageClass.ShowSuccessMessage(product.ProductId);
                transaction.Commit();
            }
            catch (SqlException ex)
            {
                message = _messageClass.ShowErrorMessage(string.Format("{0} ~ {1}", ex.Number.ToString(), ex.Message));
                transaction.Rollback();
            }
            return message;
        }

        public string Delete(int id)
        {
            string message = "";
            try
            {
                int result = _productRepository.Delete(id);
                message = _messageClass.ShowDeleteMessage(result);
            }
            catch (SqlException ex)
            {
                message = _messageClass.ShowErrorMessage(string.Format("{0} ~ {1}", ex.Number.ToString(), ex.Message));
            }
            return message;
        }

        public string DeletePrice(int id)
        {
            string message = "";
            try
            {
                int result = _productRepository.DeletePrice(id);
                message = _messageClass.ShowDeleteMessage(result);
            }
            catch (SqlException ex)
            {
                message = _messageClass.ShowErrorMessage(string.Format("{0} ~ {1}", ex.Number.ToString(), ex.Message));
            }
            return message;
        }
    }
}
