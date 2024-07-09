using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using hexagonXg.Application.Models;
using hexagonXg.Application.Services;

namespace hexagonXg.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryService _categoryService;

        public CategoryController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: api/category
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return Ok(categories);
        }

        // GET: api/category/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound(new { error = "Category not found" });
            }
            return Ok(category);
        }

        // POST: api/category
        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory(Category category)
        {
            await _categoryService.AddCategoryAsync(category);
            return CreatedAtAction(nameof(GetCategory), new { id = category.CategoryId }, category);
        }

        // DELETE: api/category/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound(new { error = "Category not found" });
            }

            await _categoryService.DeleteCategoryAsync(id);
            return Ok(new { deleted = category });
        }

        // PUT: api/category/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(int id, Category category)
        {
            if (id != category.CategoryId)
            {
                return BadRequest();
            }

            try
            {
                await _categoryService.UpdateCategoryAsync(category);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_categoryService.CategoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok(new { data = category });
        }
    }
}
