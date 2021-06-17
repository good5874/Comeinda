using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Comeinda.Data.Tables
{
    public class CategoryEventTable
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public IEnumerable<EventTable> Events { get; set; }
    }
}
