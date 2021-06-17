using System;
using System.ComponentModel.DataAnnotations;

namespace Comeinda.Data.Tables
{
    public class TicketSetsTable
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string RoomCapacityType { get; set; }
        [Required]
        public int TotalRoomCapacity
        {
            get
            {
                if (NumberOfStandingPlace.HasValue && NumberOfSeatingPlace.HasValue)
                {
                    return NumberOfStandingPlace.Value + NumberOfSeatingPlace.Value;
                }
                else if (NumberOfStandingPlace.HasValue)
                {
                    return NumberOfStandingPlace.Value;
                }
                else if (NumberOfSeatingPlace.HasValue)
                {
                    return NumberOfSeatingPlace.Value;
                }
                else
                {
                    return 0;
                }
            }
        }
        public double? CostOfStandingTickets { get; set; }
        public double? CostOfSeatingTickets { get; set; }

        public int? NumberOfStandingPlace { get; set; }

        public int? NumberOfSeatingPlace { get => Rows * NumberOfPlacesInRow; }
        public int? Rows { get; set; }
        public int? NumberOfPlacesInRow { get; set; }

        [Required]
        public Guid EventId { get; set; }
        public EventTable Event { get; set; }
    }
}
