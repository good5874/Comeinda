using Comeinda.Data.Repositories.Abstract;
using Comeinda.Data.Tables;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Comeinda.Data.Repositories
{
    public class TicketRepository : BaseRepository<TicketTable>, ITicketRepository
    {
        public TicketRepository(ComeindaDbContext context) : base(context)
        {
        }

        public async Task AddRangeAsync(IEnumerable<TicketTable> items)
        {
            dbSet.AddRange(items);
            await context.SaveChangesAsync();
        }
    }
}
