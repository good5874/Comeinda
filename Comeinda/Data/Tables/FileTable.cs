using System;
using System.ComponentModel.DataAnnotations;

namespace Comeinda.Data.Tables
{
    public class FileTable
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Path { get; set; }
        [Required]
        public long SizeBytes { get; set; }
        [Required]
        public string ParentLink { get; set; }
        [Required]
        public Guid EventId { get; set; }
        [Required]
        public EventTable Event { get; set; }
    }
}
