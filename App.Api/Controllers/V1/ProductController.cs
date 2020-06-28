using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using App.Api.Contract.V1.Map;
using App.Api.Contract.V1.Request;
using App.Api.Contract.V1.Response;
using App.Api.Contract.V1;
using App.Api.Contract.V1.Interface;
using App.Persistence.Model;

namespace App.Api.Controllers.V1
{

    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly IEntityService<Product, ProductCreateRequest, ProductUpdateRequest> _productService;
        private readonly ILogger<ProductController> _logger;

        public ProductController(
            IEntityService<Product, ProductCreateRequest, ProductUpdateRequest> productService,
            ILogger<ProductController> logger)
        {
            this._productService = productService;
            this._logger = logger;
        }

        [HttpPost(ApiRoutes.Product.Create)]
        public IActionResult Create([FromBody] ProductCreateRequest product)
        {

            Product productModel = null;
            ProductResponse productResponse = null;

            productModel = this._productService.Create(product);
            productResponse = ProductMapper.ModelToResponse(productModel);

            return Ok(productResponse);

        }

        [HttpGet(ApiRoutes.Product.GetById)]
        public IActionResult GetById(int id)
        {

            Product productModel = null;
            ProductResponse productResponse = null;

            productModel = this._productService.Get(id);

            if (productModel == null)
            {
                return NotFound($"Id { id } Not Found!");
            }

            productResponse = ProductMapper.ModelToResponse(productModel);

            string baseUrl = $"{ HttpContext.Request.Scheme }://{ HttpContext.Request.Host.ToUriComponent() }";
            string locationUri = baseUrl + "/" + ApiRoutes.Product.GetById.Replace("{id}", productModel.ProductId.ToString());

            return Created(locationUri, productResponse);

        }

        [HttpGet(ApiRoutes.Product.GetList)]
        public IActionResult GetList([FromQuery] string filter, [FromQuery] int? pageIndex, [FromQuery] int? pageSize)
        {

            IEnumerable<Product> productModels = null;
            IList<ProductResponse> productesListResponse = null;

            productModels = this._productService.GetList(filter, pageIndex, pageSize);

            if (productModels.Count() <= 0)
            {
                return NotFound($"Items not found for requested page");
            }

            productesListResponse = new Collection<ProductResponse>();

            foreach (Product productModel in productModels)
            {
                productesListResponse.Add(ProductMapper.ModelToResponse(productModel));
            }

            return Ok(productesListResponse);

        }

        [HttpPut(ApiRoutes.Product.Update)]
        public IActionResult UpdateAsync([FromBody] ProductUpdateRequest product, [FromRoute] int id)
        {

            Product productModel = null;
            ProductResponse productResponse = null;

            productModel = this._productService.Update(product, id);
            productResponse = ProductMapper.ModelToResponse(productModel);

            return Ok(productResponse);

        }

        [HttpDelete(ApiRoutes.Product.Delete)]
        public IActionResult RemoveAsync([FromRoute] int id)
        {

            Product productModel = null;
            ProductResponse productResponse = null;

            productModel = this._productService.Remove(id);
            productResponse = ProductMapper.ModelToResponse(productModel);

            return Ok(productResponse);

        }

    }

}