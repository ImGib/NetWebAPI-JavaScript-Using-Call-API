using AppServer.DTO.CategoryDTO;
using AppServer.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly MySaleDBContext _context;
        private readonly IMapper _mapper;
        public CategoriesController(MySaleDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryResponse>>> GetCategories()
        {
            if (_context.Categories == null)
            {
                return NotFound();
            }
            List<Category> list = await _context.Categories.Include(p => p.Products).ToListAsync();
            return _mapper.Map<List<CategoryResponse>>(list);
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryResponse>> GetCategory(int id)
        {
            if (_context.Categories == null)
            {
                return NotFound();
            }

            Category? category = await _context.Categories.Include(p => p.Products).FirstOrDefaultAsync(p => p.CategoryId == id);

            if (category == null)
            {
                return NotFound();
            }

            return _mapper.Map<CategoryResponse>(category);
        }

        // PUT: api/Categories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(int id, CategoryRequest data)
        {
            Category category = _mapper.Map<Category>(data);
            category.CategoryId = id;

            _context.Entry(category).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Categories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CategoryResponse>> PostCategory(CategoryRequest data)
        {
            if (_context.Categories == null)
            {
                return Problem("Entity set 'MySaleDBContext.Categories'  is null.");
            }

            Category category = _mapper.Map<Category>(data);

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategory", new { id = category.CategoryId }, category);
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            if (_context.Categories == null)
            {
                return NotFound();
            }
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CategoryExists(int id)
        {
            return (_context.Categories?.Any(e => e.CategoryId == id)).GetValueOrDefault();
        }
    }
}
