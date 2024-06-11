using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OfficeTechRepairSystem.Data;
using OfficeTechRepairSystem.Data.Models;
using OfficeTechRepairSystem.Data.Repositories;

namespace OfficeTechRepairSystem.Pages
{
    public class ServicesModel(
         IRepository<Service> serviceRepository,
         IRepository<Category> categoryRepository,
         ApplicationDbContext context) : PageModel
    {

        public bool IsEditMode = false;

        public int CategoryId { get; set; }

        public string Query { get; set; }

        public int ServiceId { get; set; }

        public IEnumerable<Category> Categories { get; set; }

        public IEnumerable<Service> Services { get; set; }

        [BindProperty]
        public Service Service { get; set; }

        public async Task OnGetAsync()
        {
            Categories = await categoryRepository.GetAllAsync();
            Services = await serviceRepository.GetAllAsync();
        }

        public async Task OnPostDeleteService(int serviceId)
        {
            var service = await serviceRepository.GetByIdAsync(serviceId);

            if (service is not null)
            {
                serviceRepository.Remove(service);
            }

            await OnGetAsync();
        }

        public async Task OnPostGetServicesByCategory(int categoryId)
        {
            Services = new List<Service>();

            Services = await serviceRepository.FindAsync(u => u.CategoryId == categoryId);
            Categories = await categoryRepository.GetAllAsync();
        }

        public async Task OnPostGetServicesBySearch(string query)
        {
            var exactMatches = context.Services.Where(s => s.Title.StartsWith(query)).ToList();

            if (exactMatches.Any())
            {
                Services = exactMatches;
            }
            else
            {
                // Поиск похожих результатов
                var similarMatches = context.Services.Where(s => s.Title.Contains(query)).ToList();
                
                Services = similarMatches;
            }

            Categories = await categoryRepository.GetAllAsync();
        }

        public async Task<IActionResult> OnPostChangeModeAsync(int categoryId)
        {
            IsEditMode = true;
            // Ваш код для обработки данных
            return new JsonResult(new { success = true });
        }

        public async Task OnPostUpsertService()
        {
            if (!IsEditMode)
            {
                await serviceRepository.AddAsync(Service);
            }
            else
            {
                await serviceRepository.UpdateAsync(Service);
            }

            await OnGetAsync();
        }
    }
}
