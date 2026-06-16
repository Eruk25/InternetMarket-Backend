using InternetMarket.ProductService.API.DTOs.Requests;
using InternetMarket.ProductService.Application.Abstractions.Repositories;
using InternetMarket.ProductService.Application.Products;
using InternetMarket.ProductService.Application.Products.Create;
using InternetMarket.ProductService.Application.Products.Delete;
using InternetMarket.ProductService.Application.Products.Get;
using InternetMarket.ProductService.Application.Products.Get.GetById;
using InternetMarket.ProductService.Application.Products.Get.GetByIds;
using InternetMarket.ProductService.Application.Products.Import;
using InternetMarket.ProductService.Application.Products.Update;
using InternetMarket.ProductService.Application.Products.Update.CancelReservation;
using InternetMarket.ProductService.Application.Products.Update.ConfirmShipment;
using InternetMarket.ProductService.Application.Products.Update.Reserve;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InternetMarket.ProductService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProviderRepository _providerRepository;

        public ProductsController(IMediator mediator, ICategoryRepository categoryRepository, IProviderRepository providerRepository)
        {
            _mediator = mediator;
            _categoryRepository = categoryRepository;
            _providerRepository = providerRepository;
        }

        [HttpGet]
        [Route("all")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllAsync()
        {
            var products = await _mediator.Send(new GetAllProductsQuery());
            return Ok(products);
        }

        [HttpGet]
        [Route("paged")]
        public async Task<ActionResult<PagedResult<ProductDto>>> GetPagedAsync([FromQuery] int page = 1, [FromQuery] int pageSize = 20)
        {
            var result = await _mediator.Send(new GetProductsPagedQuery(page, pageSize));
            return Ok(result);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<ProductDto>> GetByIdAsync(Guid id)
        {
            var product = await _mediator.Send(new GetProductByIdQuery(id));
            return Ok(product);
        }

        [HttpPost]
        [Route("by-ids")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetByIdsAsync([FromBody] GetByIdsRequest request)
        {
            var products = await _mediator.Send(new GetProductsByIdsQuery(request.Ids));
            return Ok(products);
        }

        [HttpPost]
        [Route("reserve")]
        public async Task<ActionResult> ReserveAsync([FromBody] ReserveRequest request)
        {
            await _mediator.Send(new ReserveProductCommand(request.ItemsToReserve));
            return NoContent();
        }

        [HttpPost]
        [Route("cancel-reservation")]
        public async Task<ActionResult> CancelReservationAsync([FromBody] ReserveRequest request)
        {
            await _mediator.Send(new CancelReservationCommand(request.ItemsToReserve));
            return NoContent();
        }

        [HttpPost]
        [Route("confirm-reservation")]
        public async Task<ActionResult> ConfirmShipmentAsync([FromBody] ReserveRequest request)
        {
            await _mediator.Send(new ConfirmShipmentCommand(request.ItemsToReserve));
            return NoContent();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ProductDto>> CreateAsync([FromBody] CreateProductRequest request)
        {
            var command = new CreateProductCommand(
                request.ProductName,
                request.Description,
                request.Price,
                request.Quantity,
                request.Weight,
                request.Length,
                request.Width,
                request.Height,
                request.CategoryId,
                request.ProviderId,
                request.ImageUrl);

            var product = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = product.Id }, product);
        }

        [HttpPut]
        [Route("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ProductDto>> UpdateAsync(Guid id, [FromBody] UpdateProductRequest request)
        {
            var command = new UpdateProductCommand(
                id,
                request.ProductName,
                request.Description,
                request.Price,
                request.Quantity,
                request.Weight,
                request.Length,
                request.Width,
                request.Height,
                request.CategoryId,
                request.ProviderId,
                request.ImageUrl);

            var product = await _mediator.Send(command);
            return Ok(product);
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            await _mediator.Send(new DeleteProductCommand(id));
            return NoContent();
        }

        [HttpGet]
        [Route("categories")]
        public async Task<ActionResult> GetCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();
            return Ok(categories.Select(c => new { c.Id, CategoryName = c.CategoryName.Value }));
        }

        [HttpGet]
        [Route("providers")]
        public async Task<ActionResult> GetProvidersAsync()
        {
            var providers = await _providerRepository.GetAllAsync();
            return Ok(providers.Select(p => new { p.Id, Name = p.Name.Value }));
        }

        [HttpPost]
        [Route("import")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<int>> ImportFromExcelAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("File is required");

            var products = new List<ExcelProductRow>();

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                stream.Position = 0;

                using var workbook = new ClosedXML.Excel.XLWorkbook(stream);
                var worksheet = workbook.Worksheet(1);
                var rows = worksheet.RowsUsed().Skip(1);

                foreach (var row in rows)
                {
                    var productName = row.Cell(1).GetString().Trim();
                    if (string.IsNullOrWhiteSpace(productName))
                        continue;

                    var imageUrlCell = row.Cell(11);
                    var imageUrl = imageUrlCell.IsEmpty() ? null : imageUrlCell.GetString().Trim();

                    products.Add(new ExcelProductRow(
                        productName,
                        row.Cell(2).GetString().Trim(),
                        (decimal)row.Cell(3).GetDouble(),
                        (int)row.Cell(4).GetDouble(),
                        (int)row.Cell(5).GetDouble(),
                        (int)row.Cell(6).GetDouble(),
                        (int)row.Cell(7).GetDouble(),
                        (int)row.Cell(8).GetDouble(),
                        row.Cell(9).GetString().Trim(),
                        row.Cell(10).GetString().Trim(),
                        imageUrl
                    ));
                }
            }

            var count = await _mediator.Send(new ImportProductsFromExcelCommand(products));
            return Ok(count);
        }
        [HttpPost]
        [Route("upload-image")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<string>> UploadImageAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("File is required");

            var uploadsDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "products");
            Directory.CreateDirectory(uploadsDir);

            var ext = Path.GetExtension(file.FileName);
            var fileName = $"{Guid.NewGuid()}{ext}";
            var filePath = Path.Combine(uploadsDir, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var url = $"/images/products/{fileName}";
            return Ok(url);
        }
    }
}
