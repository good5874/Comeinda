using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Comeinda.Data.Tables
{
    [Display(Name = "Категории мероприятий")]
    public class CategoryEventTable
    {
        [Required]
        [Display(Name = "Id категории")]
        public Guid Id { get; set; }
        [Required]
        [Display(Name = "Название категории")]
        public string Name { get; set; }
        public IEnumerable<EventTable> Events { get; set; }
    }
}
