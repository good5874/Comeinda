using Comeinda.Data.Tables.Enams;
using System;
using System.ComponentModel.DataAnnotations;

namespace Comeinda.Models
{
    public class AdditionalEventDataViewModel
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public EventStatusEnum Status { get; set; }
        [Required]
        public string TitleSEO { get; set; }
        [Required]
        public string DescriptionSEO { get; set; }
        [Required]
        public string KeywordsSEO { get; set; }
        [Required]
        public string LinkURL { get; set; }
    }
}
