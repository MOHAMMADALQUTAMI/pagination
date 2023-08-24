using System.Security.AccessControl;
using Microsoft.AspNetCore.Mvc;
using PcPartsManager.Models;
using Microsoft.EntityFrameworkCore;
using PcPartsManager.ViewModels;
using PcPartsManager.ViewModels.SubCategory;
using System.Linq;

namespace PcPartsManager.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class SubCategoryController : ControllerBase
{
    private readonly ApplicationContext _context;
    public SubCategoryController(ApplicationContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IEnumerable<SubCategoryVM>> Get()
    {
        var subCategories = await _context.SubCategories.Include(sc => sc.Category).ToListAsync();

        return subCategories.Select(sc => new SubCategoryVM
        {
            Id = sc.Id,
            Name = sc.Name,
            Category = new CategoryVM
            {
                Id = sc.Category.Id,
                Name = sc.Category.Name
            }
        }).ToList();
    }

    [HttpPost]
    public async Task<IActionResult> Post(CreateSubCategoryVM vm)
    {
        var subCategory = new SubCategory
        {
            Name = vm.Name,
            CategoryId = vm.CategoryId
        };
        _context.SubCategories.Add(subCategory);
        await _context.SaveChangesAsync();
        return Ok();
    }
}
