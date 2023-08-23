using LearningServerRebuild.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using System.Security.Claims;

namespace ProductC.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ProjectContext _context;

        public ProductController(ProjectContext context)
        {
            _context = context;
        }

        
        [HttpGet]
        [Authorize]
        public async Task<ActionResult> GetProduct()
        {
            var userIDClaim = User.FindFirstValue("UserID");

            if (!Guid.TryParse(userIDClaim, out Guid userID))
            {
                var errorMessage = "Invalid user ID claim";
                var errorResponse = new { message = errorMessage };
                return BadRequest(errorResponse);
            }

            var user = await _context.User.FirstOrDefaultAsync(u => u.id == userID);

            if (user == null)
            {
                var errorMessage = "User not found";
                var errorResponse = new { message = errorMessage };
                return NotFound(errorResponse);
            }

            var products = await _context.Product.Where(u => u.Product_user == user.id).ToListAsync();

            return Ok(products);

        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> CreateProduct(Product productAdd)
        {
            var userIDClaim = User.FindFirstValue("UserID");

            if (!Guid.TryParse(userIDClaim, out Guid userID))
            {
                var errorMessage = "Invalid user ID claim";
                var errorResponse = new { message = errorMessage };
                return BadRequest(errorResponse);
            }

            var user = await _context.User.FirstOrDefaultAsync(u => u.id == userID);

            if (user == null)
            {
                var errorMessage = "User not found";
                var errorResponse = new { message = errorMessage };
                return NotFound(errorResponse);
            }

            if(productAdd.price <= 0)
            {
                var errorMessage = "Insira um preço válido";
                var errorResponse = new { message = errorMessage };
                return BadRequest(errorResponse);
            }

            if(_context.Product.Any(u => u.name == productAdd.name && u.Product_user == user.id))
            {
                var errorMessage = "Produto já cadastrado!";
                var errorResponse = new { message = errorMessage };
                return Conflict(errorResponse);
            }

            var product = new Products
            {
                name = productAdd.name,
                price = productAdd.price.Value, 
                Product_user = user.id,
                createdAt = DateTime.UtcNow,
                updatedAt = DateTime.UtcNow
            };
            
            _context.Product.Add(product);
            await _context.SaveChangesAsync();

            return Created("product", product);

        }

        [HttpGet("one/{name}")]
        [Authorize]
        public async Task<ActionResult> GetProductName(string name)
        {
            var userIDClaim = User.FindFirstValue("UserID");

            if (!Guid.TryParse(userIDClaim, out Guid userID))
            {
                var errorMessage = "Invalid user ID claim";
                var errorResponse = new { message = errorMessage };
                return BadRequest(errorResponse);
            }

            var user = await _context.User.FirstOrDefaultAsync(u => u.id == userID);

            if (user == null)
            {
                var errorMessage = "User not found";
                var errorResponse = new { message = errorMessage };
                return NotFound(errorResponse);
            }

            var product = await _context.Product.FirstOrDefaultAsync(u => u.name == name && u.Product_user == user.id);

            if (product == null)
            {
                var errorMessage = "Product not found";
                var errorResponse = new { message = errorMessage };
                return NotFound(errorResponse);
            }

            return Ok(product);
        }

        [HttpDelete("{name}")]
        [Authorize]
        public async Task<ActionResult> DeleteProduct(string name)
        {
            var userIDClaim = User.FindFirstValue("UserID");

            if (!Guid.TryParse(userIDClaim, out Guid userID))
            {
                var errorMessage = "Invalid user ID claim";
                var errorResponse = new { message = errorMessage };
                return BadRequest(errorResponse);
            }

            var user = await _context.User.FirstOrDefaultAsync(u => u.id == userID);

            if (user == null)
            {
                var errorMessage = "User not found";
                var errorResponse = new { message = errorMessage };
                return NotFound(errorResponse);
            }

            var product = await _context.Product.FirstOrDefaultAsync(u => u.name == name && u.Product_user == user.id);

            if (product == null)
            {
                var errorMessage = "Product not found";
                var errorResponse = new { message = errorMessage };
                return NotFound(errorResponse);
            }

            _context.Product.Remove(product);
            await _context.SaveChangesAsync();

            var sucessResponse = new { message = "Product successfully removed" };

            return Ok(sucessResponse);
        }

        [HttpGet("list")]
        [Authorize]
        public async Task<IActionResult> GetProductsQuery([FromQuery] Product queryParameters)
        {
            var products = _context.Product.AsQueryable();

            if (!string.IsNullOrEmpty(queryParameters.name))
            {
                products = products.Where(u => u.name.Contains(queryParameters.name));
            } 
            else
            {
                var errorMessage = "Enter a valid name";
                var errorResponse = new { message = errorMessage };
                return BadRequest(errorResponse);
            }

            if (queryParameters.price.HasValue)
            {
                products = products.Where(u => u.price >= queryParameters.price.Value);
            }
            else
            {
                var errorMessage = "Enter a valid price";
                var errorResponse = new { message = errorMessage };
                return BadRequest(errorResponse);
            }

            var result = products.ToList();
            return Ok(result);
        }


    }
}
