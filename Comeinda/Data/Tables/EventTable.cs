using Comeinda.Data.Tables.Enams;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Comeinda.Data.Tables
{
    public class EventTable
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public EventStatusEnum Status { get; set; } = EventStatusEnum.New;
        [Required]
        public int AgeLimit { get; set; }
        [Required]
        public Guid CategoryId { get; set; }
        public CategoryEventTable Category { get; set; }
        [Required]
        public string Genre { get; set; }
        public IEnumerable<FileTable> Files { get; set; }
        public string TitleSEO { get; set; }
        public string DescriptionSEO { get; set; }
        public string KeywordsSEO { get; set; }
        public string LinkURL { get; set; }
        public DateTime RegistrationClosingDate { get; set; }
        public DateTime DoorOpeningDate { get; set; }
        public DateTime EventStartDate { get; set; }
        public DateTime FinishingEventDate { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Place { get; set; }
        public IEnumerable<TicketSetsTable> TicketSets { get; set; }
        public IEnumerable<TicketTable> Tickets { get; set; }
        public string UserId { get; set; }
        public CastomUser User { get; set; }
    }
}
