using ApiOgrenmeProjesi.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiOgrenmeProjesi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly DataContext _dataContext;
        public ProductController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]  //Listeleme
        public async Task<ActionResult<List<ProductEntity>>> Get()
        {
            return Ok(await _dataContext.productEntities.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductEntity>> Get(int id)    //id li elemanı bulmak için
        {
            var product =await _dataContext.productEntities.FindAsync(id);
            if (product == null)
            {
                return BadRequest("Ürün id Bulunamadı");
            }
            return Ok(product);
        }
        [HttpPost]  //Ekleme İşlemi  
        public async Task<ActionResult<List<ProductEntity>>> AddProduct(ProductEntity product)
        {
            _dataContext.productEntities.Add(product);
            await _dataContext.SaveChangesAsync();
            return Ok(await _dataContext.productEntities.ToListAsync());
        }
        [HttpPut]  //Düzenleme İşlemi
        public async Task<ActionResult<List<ProductEntity>>> UpdateProduc(ProductEntity request)
        {
            var product=await _dataContext.productEntities.FindAsync(request.Id);
            if (product == null)
                return BadRequest("Değiştirilecek ürün bulunamadı");
            product.Name = request.Name;
            product.Price = request.Price;
            await _dataContext.SaveChangesAsync();
            return Ok(await _dataContext.productEntities.ToListAsync()); 
        }
        [HttpDelete("{id}")]  //silme işlemi
        public async Task<ActionResult<List<ProductEntity>>> DeleteProduct(int id)
        {
            var product = await _dataContext.productEntities.FindAsync(id);
            if (product == null)
                return NotFound("Silinecek ürün bulunamadı");
            _dataContext.productEntities.Remove(product);
            await _dataContext.SaveChangesAsync();
            return Ok(await _dataContext.productEntities.ToListAsync()); 
        }
    }
}
