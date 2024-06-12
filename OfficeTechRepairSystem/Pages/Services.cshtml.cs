using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OfficeTechRepairSystem.Data;
using OfficeTechRepairSystem.Data.Models;
using System.Security.Claims;

namespace OfficeTechRepairSystem.Pages
{
    public class ServicesModel(
         IDbContextFactory<ApplicationDbContext> contextFactory,
         IHttpContextAccessor httpContextAccessor,
         IWebHostEnvironment webHostEnvironment) : PageModel
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
            using var context = contextFactory.CreateDbContext();

            Categories = await context.Categories.ToListAsync();
            Services = await context.Services.Include(u => u.Image).ToListAsync();

            foreach (var item in Services)
            {
                if (item.ImageId is not null)
                {
                    await OnGetDownloadFileAsync(item.ImageId);
                }
            }
        }

        public async Task OnGetDownloadFileAsync(int? requestId)
        {
            using var context = contextFactory.CreateDbContext();
            var request = await context.Images.FindAsync(requestId);

            if (request == null || request.FileData == null)
            {
                return;
            }

            var uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "lib/img");

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var filePath = Path.Combine(uploadsFolder, request.FileName);

            if (!System.IO.File.Exists(filePath))
            {
                await System.IO.File.WriteAllBytesAsync(filePath, request.FileData);
            }
        }

        public async Task OnPostDeleteService(int serviceId)
        {
            using var context = contextFactory.CreateDbContext();

            var service = await context.Services.Where(u => u.Id == serviceId).FirstOrDefaultAsync();

            if (service is not null)
            {
                context.Services.Remove(service); 

                await context.SaveChangesAsync();
            }

            await OnGetAsync();
        }

        public async Task OnPostGetServicesByCategory(int categoryId)
        {
            using var context = contextFactory.CreateDbContext();

            Services = await context.Services.Where(u => u.CategoryId == categoryId).Include(u => u.Image).ToListAsync();

            foreach (var item in Services)
            {
                if (item.ImageId is not null)
                {
                    await OnGetDownloadFileAsync(item.ImageId);
                }
            }

            Categories = await context.Categories.ToListAsync(); 
        }

        public async Task OnPostGetServicesBySearch(string query)
        {
            using var context = contextFactory.CreateDbContext();

            var exactMatches = await context.Services.Where(s => s.Title.StartsWith(query)).Include(u => u.Image).ToListAsync();

            if (exactMatches.Any())
            {
                Services = exactMatches;
            }
            else
            {
                // Поиск похожих результатов
                var similarMatches = context.Services.Where(s => s.Title.Contains(query)).Include(u => u.Image).ToList();

                Services = similarMatches;
            }

            foreach (var item in Services)
            {
                if (item.ImageId is not null)
                {
                    await OnGetDownloadFileAsync(item.ImageId);
                }
            }

            Categories = await context.Categories.ToListAsync();
        }

        public async Task OnPostUpsertService()
        {
            using var context = contextFactory.CreateDbContext();

            var file = httpContextAccessor.HttpContext.Request.Form.Files.FirstOrDefault();

            var request = new Image();

            if (file != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    request.FileData = memoryStream.ToArray();
                    request.FileName = file.FileName;
                    request.FileContentType = file.ContentType;
                    request.Name = file.Name;
                }

                try
                {
                    await context.Images.AddAsync(request);
                    await context.SaveChangesAsync();

                    Service.ImageId = await context.Images.OrderBy(item => item.Id).Select(u => u.Id).LastOrDefaultAsync();
                }
                catch (Exception ex)
                {
                    // Добавление ошибки в ModelState
                    ModelState.AddModelError(string.Empty, "Произошла ошибка при сохранении запроса: " + ex.Message);
                }

            }

            await context.Services.AddAsync(Service);

            await context.SaveChangesAsync();       

            await OnGetAsync();
        }
    }
}
