using Comeinda.Data.Repositories.Abstract;
using Comeinda.Data.Tables;

namespace Comeinda.Data.Repositories
{
    public class TicketSetsRepository : BaseRepository<TicketSetsTable>, ITicketSetsRepository
    {
        public TicketSetsRepository(ComeindaDbContext context) : base(context)
        {
        }
    }
}
