using CorporateCompany.Areas.Admin.Models;

namespace CorporateCompany.ViewModels;

public class ProjectsListViewModel
{
    public ProjectsListViewModel()
    {
        Photos = new List<Photo>();
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<Photo> Photos { get; set; }
    public bool IsFinished { get; set; }

    public string AboutUsSummary { get; set; }
    public string FacebookUrl { get; set; }
    public string TwitterUrl { get; set; }
    public string InstagramUrl { get; set; }
    public string LinkedInUrl { get; set; }
}