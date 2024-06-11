using Microsoft.AspNetCore.Mvc.RazorPages;
using OfficeTechRepairSystem.Data.Models;
using OfficeTechRepairSystem.Data.Repositories;

namespace OfficeTechRepairSystem.Pages
{
    public class СontactsModel(
        IRepository<Request> requestRepositor) : PageModel
    {
        public Request Request { get; set; }

        public void OnGet()
        {
        }

        public async Task OnPostSendRequest()
        {
            if (Request is not null)
            {
                await requestRepositor.AddAsync(Request);
            }
        }
    }
}
