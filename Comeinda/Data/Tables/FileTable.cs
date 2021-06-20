using System;
using System.ComponentModel.DataAnnotations;

namespace Comeinda.Data.Tables
{
    [Display(Name = "Файлы мероприятия")]
    public class FileTable
    {
        [Required]
        [Display(Name = "Id файла")]
        public Guid Id { get; set; }
        [Display(Name = "Имя файла")]
        public string Name { get; set; }
        [Display(Name = "Путь к файлу")]
        public string Path { get; set; }
        [Display(Name = "Размер файла в байтах")]
        public long SizeBytes { get; set; }
        [Required]
        [Display(Name = "Id мероприятия")]
        public Guid EventId { get; set; }
        public EventTable Event { get; set; }
    }
}
