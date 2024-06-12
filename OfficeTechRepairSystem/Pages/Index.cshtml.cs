using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using OfficeTechRepairSystem.Data;
using OfficeTechRepairSystem.Data.Models;

namespace OfficeTechRepairSystem.Pages
{
    public class IndexModel : PageModel
    {

        private readonly ILogger<IndexModel> _logger;
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;

        public IndexModel(ILogger<IndexModel> logger,
                          IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            _logger = logger;
            _contextFactory = contextFactory;
        }

        public IEnumerable<Service> Services { get; set; }

        public async Task OnGet()
        {
            using var context = _contextFactory.CreateDbContext();

            Services = await context.Services.ToListAsync();
        }
    }
}
