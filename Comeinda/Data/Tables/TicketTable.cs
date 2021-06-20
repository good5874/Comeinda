using Comeinda.Data.Tables.Enams;
using System;
using System.ComponentModel.DataAnnotations;

namespace Comeinda.Data.Tables
{
    [Display(Name = "Билеты")]
    public class TicketTable
    {
        [Required]
        [Display(Name = "Id билета")]
        public Guid Id { get; set; }
        [Display(Name = "Порядковый номер")]
        public int? Number { get; set; }
        [Display(Name = "Номер ряда")]
        public int? Row { get; set; }
        [Display(Name = "Номер места")]
        public int? PlaceNumber { get; set; }
        [Required]
        [Display(Name = "Стоимость")]
        public double Cost { get; set; }
        [Required]
        [Display(Name = "Статус билета")]
        public TicketStatusEnum TicketStatus { get; set; }
        public Guid? QRCodeId { get; set; }
        [Display(Name = "Штрих-код")]
        public string Barcode { get; set; }
        [Required]
        [Display(Name = "Id мероприятия")]
        public Guid EventId { get; set; }
        [Required]
        public EventTable Event { get; set; }
    }
}
