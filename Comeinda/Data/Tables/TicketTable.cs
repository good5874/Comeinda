using Comeinda.Data.Tables.Enams;
using System;
using System.ComponentModel.DataAnnotations;

namespace Comeinda.Data.Tables
{
    public class TicketTable
    {
        [Required]
        public Guid Id { get; set; }
        public int? Number { get; set; }
        public int? Row { get; set; }
        public int? PlaceNumber { get; set; }
        [Required]
        public double Cost { get; set; }
        [Required]
        public TicketStatusEnum TicketStatus { get; set; }
        public Guid? QRCodeId { get; set; }
        public string Barcode { get; set; }
        [Required]
        public Guid EventId { get; set; }
        [Required]
        public EventTable Event { get; set; }
    }
}
