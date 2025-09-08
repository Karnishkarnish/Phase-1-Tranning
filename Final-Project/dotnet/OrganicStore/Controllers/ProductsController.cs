using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrganicStore.Dtos;
using OrganicStore.Model;
using OrganicStore.Service;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<ActionResult<ServiceResponse<IEnumerable<ProductDto>>>> GetProducts()
    {
        var products = await _productService.GetAllProductsAsync();

        var dtoResponse = new ServiceResponse<IEnumerable<ProductDto>>
        {
            Data = products.Data.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                StockQuantity = p.StockQuantity,
                Category = p.Category,
                ImageUrl = p.ImageUrl,
                IsAvailable = p.IsAvailable,
                CreatedAt = p.CreatedAt,
                StoreId = p.StoreId,
                Store = new StoreDto
                {
                    Id = p.Store.Id,
                    Name = p.Store.Name
                }
            }).ToList(),
            Success = products.Success,
            Message = products.Message
        };

        return Ok(dtoResponse);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ServiceResponse<Product>>> GetProduct(int id)
    {
        var response = await _productService.GetProductByIdAsync(id);
        if (!response.Success) return NotFound(response);
        return Ok(response);
    }

    [HttpGet("category/{category}")]
    public async Task<ActionResult<ServiceResponse<IEnumerable<Product>>>> GetProductsByCategory(string category)
    {
        var response = await _productService.GetProductsByCategoryAsync(category);
        return Ok(response);
    }

    [HttpGet("search/{searchTerm}")]
    public async Task<ActionResult<ServiceResponse<IEnumerable<Product>>>> SearchProducts(string searchTerm)
    {
        var response = await _productService.SearchProductsAsync(searchTerm);
        return Ok(response);
    }

    [HttpGet("store/{storeId}")]
    public async Task<ActionResult<ServiceResponse<IEnumerable<Product>>>> GetProductsByStore(int storeId)
    {
        var response = await _productService.GetProductsByStoreAsync(storeId);
        return Ok(response);
    }

    [Authorize(Roles = "Store,Admin")]
    [HttpPost]
    public async Task<ActionResult<ServiceResponse<Product>>> CreateProduct(Product product)
    {
        var response = await _productService.CreateProductAsync(product);
        return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, response);
    }

    [Authorize(Roles = "Store,Admin")]
    [HttpPut("{id}")]
    public async Task<ActionResult<ServiceResponse<Product>>> UpdateProduct(int id, UpdateProductDto dto)
    {
        if (id != dto.Id) return BadRequest("ID mismatch");

        var response = await _productService.UpdateProductAsync(dto);
        if (!response.Success) return NotFound(response);

        return Ok(response);
    }

    [Authorize(Roles = "Store,Admin")]
    [HttpDelete("{id}")]
    public async Task<ActionResult<ServiceResponse<bool>>> DeleteProduct(int id)
    {
        var response = await _productService.DeleteProductAsync(id);
        if (!response.Success) return NotFound(response);

        return Ok(response);
    }
}
