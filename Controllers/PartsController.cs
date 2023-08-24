using System.Security.AccessControl;
using Microsoft.AspNetCore.Mvc;
using PcPartsManager.Models;
using Microsoft.EntityFrameworkCore;
using PcPartsManager.ViewModels;
using PcPartsManager.ViewModels.Parts;
using System.Linq;

namespace PcPartsManager.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class PartsController : ControllerBase
{
    private readonly ApplicationContext _context;
    public PartsController(ApplicationContext context)
    {
        _context = context;
    }
    [HttpGet]
    public async Task<IEnumerable<PartVM>> GetPartsByCategory(int categoryId)
    {
        var parts = await _context.Parts
                    .Include(p => p.SubCategory)
                    .ThenInclude(s => s.Category)
                    .Where(p => p.SubCategory.CategoryId == categoryId)
                    .ToListAsync();

        // https://stackoverflow.com/questions/5010110/entityframework-join-using-join-method-and-lambdas
        var test = _context.Parts // source
                    .Join(_context.SubCategories, // target
                        p => p.SubCategoryId, // FK
                        s => s.Id,  // PK
                        (p, s) => new { p, s }) // result
                    .Join(_context.Categories, // target
                        p => p.s.CategoryId, // FK
                        c => c.Id, // PK
                        (p, c) => new { p, c }) // result
                    .Where(p => p.c.Id == categoryId);

        var result = await test.ToListAsync();



        return parts.Select(p => new PartVM
        {
            Id = p.Id,
            Name = p.Name,
            SubCategory = new SubCategoryVM
            {
                Id = p.SubCategory.Id,
                Name = p.SubCategory.Name
            }
        }).ToList();
    }

    [HttpGet]
    public async Task<IEnumerable<PartVM>> GetPartsBySubCategory(int subCategoryId)
    {
        var parts = await _context.Parts
        .Include(p => p.SubCategory)
        .Where(p => p.SubCategoryId == subCategoryId)
        .ToListAsync();


        return parts.Select(p => new PartVM
        {
            Id = p.Id,
            Name = p.Name,
            SubCategory = new SubCategoryVM
            {
                Id = p.SubCategory.Id,
                Name = p.SubCategory.Name
            }
        }).ToList();
    }

    [HttpGet]
    public async Task<IEnumerable<PartVM>> Get()
    {
        var parts = await _context.Parts
        .Include(p => p.SubCategory)
        .ToListAsync();

        return parts.Select(p => new PartVM
        {
            Id = p.Id,
            Name = p.Name,
            SubCategory = new SubCategoryVM
            {
                Id = p.SubCategory.Id,
                Name = p.SubCategory.Name
            }
        }).ToList();
    }

    [HttpPost]
    public async Task<IActionResult> Post(CreatePartVM vm)
    {
        var part = new Part
        {
            Name = vm.Name,
            SubCategoryId = vm.SubCategoryId
        };
        _context.Parts.Add(part);
        await _context.SaveChangesAsync();
        return Ok();
    }
}
