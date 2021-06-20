using System;
using System.ComponentModel.DataAnnotations;

namespace Comeinda.Models.Files
{
    [Display(Name = "Файлы мероприятия")]
    public class EventFilesViewModel
    {
        [Display(Name = "Id мероприятия")]
        public Guid EventId { get; set; }
        [Display(Name = "Название мероприятия")]
        public string NameEvent { get; set; }
        [Display(Name = "Количество файлов")]
        public int Files { get; set; }
    }
}
