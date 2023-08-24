using System.Security.AccessControl;
using Microsoft.AspNetCore.Mvc;
using PcPartsManager.Models;
using Microsoft.EntityFrameworkCore;
using PcPartsManager.ViewModels;
using PcPartsManager.ViewModels.Category;
using System.Linq;

namespace PcPartsManager.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class CategoryController : ControllerBase
{
    private readonly ApplicationContext _context;
    public CategoryController(ApplicationContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IEnumerable<CategoryVM>> Get()//query string
    {
        
        
        var categories = await _context.Categories.ToListAsync();
        return categories.Select(p => new CategoryVM
        {
            Id = p.Id,
            Name = p.Name
        }).ToList();
        
    }
     [HttpGet("{id}")]
    public async Task <CategoryVM>GetbyId(int id)
    {
        var category = await _context.Categories.SingleOrDefaultAsync(categories =>categories.Id==id);
        return  new CategoryVM
        {
            Id = category.Id,
            Name = category.Name
        };
    }

    [HttpPost]
    public async Task<IActionResult> Post(CreateCategoryVM vm)
    {
        var category = new Category
        {
            Name = vm.Name
        };
        _context.Categories.Add(category);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task Delete(int id)
    {
        var category = await _context.Categories.FindAsync(id);
        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
    }
    [HttpPut("{id}")]
     public async Task<IActionResult> Edit(int id ,CreateCategoryVM vm)
    {
       var category = await _context.Categories.FindAsync(id);
       category.Name =vm.Name;
        await _context.SaveChangesAsync();
        return Ok();
    }
    [HttpGet]
    public async Task<Pagination<CategoryVM>> GetPagination(int pageIndex, int pageSize)//query string
    {
        var query =_context.Categories;
        var length =query.Count();
        var categories = await query.Skip(pageIndex*pageSize).Take(pageSize).ToListAsync();
        var result= categories.Select(p => new CategoryVM
        {
            Id = p.Id,
            Name = p.Name
        }).ToList();
        return new Pagination<CategoryVM>
        {
            Length = length,
            Data = result
        };
    }
}
