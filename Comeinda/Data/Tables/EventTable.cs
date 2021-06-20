using Comeinda.Data.Tables.Enams;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Comeinda.Data.Tables
{
    [Display(Name = "Мероприятия")]
    public class EventTable
    {
        [Required]
        [Display(Name = "Id мероприятия")]
        public Guid Id { get; set; }
        [Required]
        [Display(Name = "Название мероприятия")]
        public string Name { get; set; }
        [Display(Name = "Описание")]
        public string Description { get; set; }
        [Required]
        [Display(Name = "Статус")]
        public EventStatusEnum Status { get; set; } = EventStatusEnum.New;
        [Required]
        [Display(Name = "Возрастное ограничение")]
        public int AgeLimit { get; set; }
        [Required]
        [Display(Name = "Id категории")]
        public Guid CategoryId { get; set; }
        public CategoryEventTable Category { get; set; }
        [Required]
        [Display(Name = "Жанр")]
        public string Genre { get; set; }
        [Display(Name = "Id картинки для афиши")]
        public Guid? PosterId { get; set; }
        public IEnumerable<FileTable> Files { get; set; }
        [Display(Name = "Title SEO")]
        public string TitleSEO { get; set; }
        [Display(Name = "Desctiption SEO")]
        public string DescriptionSEO { get; set; }
        [Display(Name = "Keywords SEO")]
        public string KeywordsSEO { get; set; }
        [Display(Name = "Ссылка на страницу мероприятия")]
        public string LinkURL { get; set; }
        [Display(Name = "Дата окончания мероприятия")]
        public DateTime RegistrationClosingDate { get; set; }
        [Display(Name = "Дата открытия дверей")]
        public DateTime DoorOpeningDate { get; set; }
        [Display(Name = "Дата начала мероприятия")]
        public DateTime EventStartDate { get; set; }
        [Display(Name = "Дата окончания мероприятия")]
        public DateTime FinishingEventDate { get; set; }
        [Required]
        [Display(Name = "Адрес")]
        public string Address { get; set; }
        [Required]
        [Display(Name = "Место")]
        public string Place { get; set; }
        public IEnumerable<TicketSetsTable> TicketSets { get; set; }
        public IEnumerable<TicketTable> Tickets { get; set; }
        [Display(Name = "Id пользователя")]
        public string UserId { get; set; }
        public CastomUser User { get; set; }
    }
}
