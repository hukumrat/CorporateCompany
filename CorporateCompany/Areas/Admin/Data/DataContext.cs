using System.Security.Principal;
using CorporateCompany.Areas.Admin.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CorporateCompany.Areas.Admin.Data;

public class DataContext : IdentityDbContext<ApplicationUser>
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {

    }
    public DbSet<Article> Articles { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<Content> Contents { get; set; }
    public DbSet<Education> Educations { get; set; }
    public DbSet<Employment> Employments { get; set; }
    public DbSet<Photo> Photos { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Reference> References { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<Slide> Slides { get; set; }
    public DbSet<Static> Statics { get; set; }
}