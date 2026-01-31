using Microsoft.AspNetCore.Mvc;
using dotnetapp.Data;
using dotnetapp.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnetapp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _db;

        public ProductsController(AppDbContext db)
        {
            _db = db;
        }

        // Endpoint 1: GET /api/products - list all products
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _db.Products.AsNoTracking().ToListAsync();
            return Ok(products);
        }

        // Endpoint 2: GET /api/products/{id} - get product by id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _db.Products.FindAsync(id);
            if (product == null) return NotFound();
            return Ok(product);
        }

        // Endpoint 3: POST /api/products - create a new product
        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            _db.Products.Add(product);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }
    }
}