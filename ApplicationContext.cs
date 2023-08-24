using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PcPartsManager.Models;

namespace PcPartsManager;

public class ApplicationContext : DbContext
{
    public DbSet<Part> Parts { get; set; }
    public DbSet<SubCategory> SubCategories { get; set; }
    public DbSet<Category> Categories { get; set; }

    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }
}

