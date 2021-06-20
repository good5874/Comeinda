using System;
using System.ComponentModel.DataAnnotations;

namespace Comeinda.Data.Tables
{
    [Display(Name = "Наборы билетов")]
    public class TicketSetsTable
    {
        [Required]
        [Display(Name = "Id набора билетов")]
        public Guid Id { get; set; }
        [Required]
        [Display(Name = "Тип вместимости")]
        public string RoomCapacityType { get; set; }
        [Required]
        [Display(Name = "Общая вместимостимость")]
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
        [Display(Name = "Стоимость стоячего билета")]
        public double? CostOfStandingTickets { get; set; }
        [Display(Name = "Стоимость сидячего билета")]
        public double? CostOfSeatingTickets { get; set; }

        [Display(Name = "Количество стоячих мест")]
        public int? NumberOfStandingPlace { get; set; }

        [Display(Name = "Количество сидячих мест")]
        public int? NumberOfSeatingPlace { get => Rows * NumberOfPlacesInRow; }
        [Display(Name = "Количество рядов")]
        public int? Rows { get; set; }
        [Display(Name = "Количество мест в ряду рядов")]
        public int? NumberOfPlacesInRow { get; set; }

        [Required]
        [Display(Name = "Id мероприятия")]
        public Guid EventId { get; set; }
        public EventTable Event { get; set; }
    }
}
