using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OfficeTechRepairSystem.Data;
using OfficeTechRepairSystem.Data.Models;

namespace OfficeTechRepairSystem.Pages
{
    public class СontactsModel(
        IDbContextFactory<ApplicationDbContext> contextFactory,
        EmailSender emailSender) : PageModel
    {
        public Request Request { get; set; }

        public void OnGet()
        {

        }

        public async Task OnPostSendRequest(Request Request)
        {
            using var context = contextFactory.CreateDbContext();
            
            if (ModelState.IsValid)
            {
                if (Request is not null)
                {
                    try
                    {
                        await context.Requests.AddAsync(Request);

                        await context.SaveChangesAsync();

                        await emailSender.SendUserEmail(Request.Email);

                        await emailSender.SendAdminEmail(Request.Email, Request.Phone, Request.Message, Request.UserName);
                    }
                    catch (Exception ex)
                    {
                        //ModelState.AddModelError(string.Empty, "Произошла ошибка при сохранении запроса: " + ex.Message);
                    }
                }
            }
            else
            {
                //ModelState.AddModelError(string.Empty, "Запрос не может быть пустым.");
            }
        }
    }
}
