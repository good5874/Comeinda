using Comeinda.Data.Tables.Enams;
using System;
using System.ComponentModel.DataAnnotations;

namespace Comeinda.Models
{
    [Display(Name = "Дополнительные данные мероприятия")]
    public class AdditionalEventDataViewModel
    {
        [Required]
        [Display(Name = "Id мероприятия")]
        public Guid Id { get; set; }
        [Required]
        [Display(Name = "Название мероприятия")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Статус мероприятия")]
        public EventStatusEnum Status { get; set; }
        [Required]
        [Display(Name = "Title SEO")]
        public string TitleSEO { get; set; }
        [Required]
        [Display(Name = "Description SEO")]
        public string DescriptionSEO { get; set; }
        [Required]
        [Display(Name = "Keywords SEO")]
        public string KeywordsSEO { get; set; }
        [Required]
        [Display(Name = "Ссылка на мероприятие")]
        public string LinkURL { get; set; }
    }
}
