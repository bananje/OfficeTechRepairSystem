using System.ComponentModel.DataAnnotations;

namespace OfficeTechRepairSystem.Data.Models
{
    public class Request
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Cообщение обязательное поле")]
        [StringLength(1001, ErrorMessage = "Сообщение не может превышать 1000 символов")]
        public string Message { get; set; }

        [Required(ErrorMessage = "Cообщение обязательное поле")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Cообщение обязательное поле")]
        [Phone(ErrorMessage = "Введён неккоректный телефон")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Cообщение обязательное поле")]
        [EmailAddress(ErrorMessage = "Введён неккоректный Email")]
        public string Email { get; set; }
    }
}
