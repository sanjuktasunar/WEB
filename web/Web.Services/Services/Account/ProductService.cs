using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
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
        Task<string> UpdatePrice(int productPriceId);
        Task<IEnumerable<ProductImageDto>> GetProductImageByProductId(int productId);
        Task<string> InsertImage(ProductImageDto dto);
        Task<string> DeleteImage(int id);
        Task<string> UpdateImage(int id);
    }

    public class ProductService:IProductService
    {
        private IProductRepository _productRepository;
        private IMessageClass _messageClass;
        IBaseInterface _baseInterface;
        private IImageService _imageService;
        public ProductService(
            IMessageClass messageClass, 
            IProductRepository productRepository,
            IBaseInterface baseInterface,
            IImageService imageService)
        {
            _productRepository = productRepository;
            _messageClass = messageClass;
            _baseInterface = baseInterface;
            _imageService = imageService;
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

        public async Task<string> UpdatePrice(int productPriceId)
        {
            string message = "";
            var con = _baseInterface.GetConnection();
            var transaction = con.BeginTransaction();
            try
            {
                var productPrice = await _productRepository.GetProductPriceById(productPriceId);
                if (productPrice is null)
                    return null;

                _productRepository.UpdatePriceByUnitIdProductId(productPrice.UnitId,productPrice.ProductId,con,transaction);
                _productRepository.UpdatePrice(productPriceId, con, transaction);

                message = _messageClass.ShowSuccessMessage(productPrice.ProductId);
                transaction.Commit();
            }
            catch (SqlException ex)
            {
                message = _messageClass.ShowErrorMessage(string.Format("{0} ~ {1}", ex.Number.ToString(), ex.Message));
                transaction.Rollback();
            }
            return message;
        }

        public async Task<IEnumerable<ProductImageDto>> GetProductImageByProductId(int productId)
        {
            return (await _productRepository.GetProductImageByProductIdAsync(productId));
        }

        public async Task<string> InsertImage(ProductImageDto dto)
        {
            string message = "";
            var con = _baseInterface.GetConnection();
            var transaction = con.BeginTransaction();
            try
            {
                int result = 0;
                var product = await _productRepository.GetProductByIdAsync(dto.ProductId);
                if (product == null || dto.Image is null)
                    return "Product is not available or image cannot be empty";

                var productImages = await GetProductImageByProductId(dto.ProductId);
                if(productImages.Count()>0)
                {
                    if(dto.IsPrimary == true)
                    {
                        _productRepository.ProductPrimaryImage(con, transaction, dto.ProductId);
                    }
                }
                else
                {
                    dto.IsPrimary = true;
                }

                var entity = dto.ToEntity();
                entity.Photo = _imageService.ConvertToByte(dto.Image);
                result = _productRepository.InsertProductImage(entity, transaction, con);
                message = _messageClass.ShowSuccessMessage(result);
                transaction.Commit();
            }
            catch (SqlException ex)
            {
                message = _messageClass.ShowErrorMessage(string.Format("{0} ~ {1}", ex.Number.ToString(), ex.Message));
                transaction.Rollback();
            }
            return message;
        }

        public async Task<string> DeleteImage(int id)
        {
            string message = "";
            var productImage = await _productRepository.GetProductImageById(id);
            if (productImage is null)
                return "Image is not available";

            var imageList = (await GetProductImageByProductId(productImage.ProductId)).Where(a => a.ImageId != productImage.ImageId);
            var con = _baseInterface.GetConnection();
            var transaction = con.BeginTransaction();
            try
            {
                int result = _productRepository.DeleteProductImage(con,transaction,id);
                if(imageList.Count()>0 && productImage.IsPrimary==true)
                {
                    var entity = imageList.OrderByDescending(a => a.ImageId).FirstOrDefault().ToEntity();
                    entity.IsPrimary = true;
                    _productRepository.UpdateProductImage(entity, transaction, con);
                }
                message = _messageClass.ShowDeleteMessage(result);
                transaction.Commit();
            }
            catch (SqlException ex)
            {
                message = _messageClass.ShowErrorMessage(string.Format("{0} ~ {1}", ex.Number.ToString(), ex.Message));
                transaction.Rollback();
            }
            return message;
        }

        public async Task<string> UpdateImage(int id)
        {
            string message = "";
            var productImage = await _productRepository.GetProductImageById(id);
            if (productImage is null)
                return "Image is not available";
            var con = _baseInterface.GetConnection();
            var transaction = con.BeginTransaction();
            _productRepository.ProductPrimaryImage(con, transaction, productImage.ProductId);
            productImage.IsPrimary = true;
            try
            {
                int result = _productRepository.UpdateProductImage(productImage.ToEntity(),transaction,con);
                message = _messageClass.ShowSuccessMessage(result);
                transaction.Commit();
            }
            catch (SqlException ex)
            {
                message = _messageClass.ShowErrorMessage(string.Format("{0} ~ {1}", ex.Number.ToString(), ex.Message));
                transaction.Rollback();
            }
            return message;
        }
    }
}
