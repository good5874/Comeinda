using Comeinda.Data.Repositories.Abstract;
using Comeinda.Data.Tables;
using Comeinda.Data.Tables.Enams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Comeinda.Data.Extension
{
    public static class TableExtension
    {
        public static async Task AddTickets(this TicketSetsTable ticketSets, ITicketRepository ticketRepository)
        {
            if (ticketSets.TotalRoomCapacity > 0)
            {
                List<TicketTable> tickets = new List<TicketTable>();

                if (ticketSets.NumberOfSeatingPlace != null &&
                    ticketSets.CostOfSeatingTickets != null &&
                    ticketSets.Rows != null &&
                    ticketSets.NumberOfPlacesInRow != null)
                {
                    for (int i = 1; i <= ticketSets.Rows; i++)
                    {
                        for (int j = 1; j <= ticketSets.NumberOfPlacesInRow; j++)
                        {
                            tickets.Add(
                                new TicketTable()
                                {
                                    Id = Guid.NewGuid(),
                                    Row = i,
                                    PlaceNumber = j,
                                    Cost = ticketSets.CostOfSeatingTickets.Value,
                                    TicketStatus = TicketStatusEnum.OnSale,
                                    Barcode = ticketSets.EventId.ToString() + ticketSets.Id + i + j,
                                    EventId = ticketSets.EventId,
                                });
                        }
                    }
                }

                if (ticketSets.NumberOfStandingPlace != null &&
                    ticketSets.CostOfStandingTickets != null)
                {

                    for (int i = 1; i <= ticketSets.NumberOfStandingPlace; i++)
                    {
                        tickets.Add(
                            new TicketTable() {
                                Id = Guid.NewGuid(),
                                Number = i,
                                Cost = ticketSets.CostOfStandingTickets.Value,
                                TicketStatus = TicketStatusEnum.OnSale,
                                Barcode = ticketSets.EventId.ToString() + ticketSets.Id + i,
                                EventId = ticketSets.EventId,
                            });
                    }
                }

                await ticketRepository.AddRangeAsync(tickets);
            }
        }
    }
}
