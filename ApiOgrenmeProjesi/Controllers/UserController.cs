using ApiOgrenmeProjesi.Core;
using Microsoft.AspNetCore.Mvc;

namespace ApiOgrenmeProjesi.Controllers
{
    public class UserController : Controller
    {
        private readonly DataContext _dataContext;
        public UserController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]  //Listeleme
        public async Task<ActionResult<List<User>>> Get()
        {
            return Ok(await _dataContext.userEntities.ToListAsync());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Get(int id)    //id li elemanı bulmak için
        {
            var user = await _dataContext.userEntities.FindAsync(id);
            if (user == null)
            {
                return BadRequest("Kullanıcı Bulunamadı");
            }
            return Ok(user);
        }
        [HttpPost]  //Ekleme İşlemi  
        public async Task<ActionResult<List<User>>> AddProduct(User user)
        {
            _dataContext.userEntities.Add(user);
            await _dataContext.SaveChangesAsync();
            return Ok(await _dataContext.productEntities.ToListAsync());
        }
        [HttpPut]  //Düzenleme İşlemi
        public async Task<ActionResult<List<User>>> UpdateUser(User request)
        {
            var product = await _dataContext.productEntities.FindAsync(request.UserId);
            if (product == null)
                return BadRequest("Değiştirilecek Kullanıcı bulunamadı");
            product.Name = request.Uname;
            product.Price = request.Upass;
            await _dataContext.SaveChangesAsync();
            return Ok(await _dataContext.productEntities.ToListAsync());
        }
        [HttpDelete("{id}")]  //silme işlemi
        public async Task<ActionResult<List<User>>> DeleteUser(int id)
        {
            var user = await _dataContext.productEntities.FindAsync(id);
            if (user == null)
                return NotFound("Silinecek ürün bulunamadı");
            _dataContext.productEntities.Remove(user);
            await _dataContext.SaveChangesAsync();
            return Ok(await _dataContext.userEntities.ToListAsync());
        }

    }
}
