using Comeinda.Data.Tables;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Comeinda.Data.Repositories.Abstract
{
    public interface ITicketRepository : IBaseRepository<TicketTable>
    {
        public Task AddRangeAsync(IEnumerable<TicketTable> items);
    }
}
