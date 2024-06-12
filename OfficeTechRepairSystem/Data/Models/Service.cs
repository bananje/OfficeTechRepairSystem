using System.ComponentModel.DataAnnotations;

namespace OfficeTechRepairSystem.Data.Models
{
    public class Service
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Наименование обязательное поле")]
        [StringLength(100, ErrorMessage = "Наименование не может превышать 100 символов")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Короткое описание обязательное поле")]
        [StringLength(255, ErrorMessage = "Короткое описани  не может превышать 255 символов")]
        public string ShortDesc { get; set; }

        [Required(ErrorMessage = "Описание обязательное поле")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Цена обязательное поле")]
        public decimal Price { get; set; }

        public bool? IsPopular { get; set; }

        public string? ImageUrl { get; set; }

        [Required(ErrorMessage = "Категория обязательное поле")]
        public int CategoryId { get; set; }

        public int? ImageId { get; set; }

        public Category Category { get; set; }

        public Image Image { get; set; }
    }
}
