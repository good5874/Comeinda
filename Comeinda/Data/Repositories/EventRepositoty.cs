using Comeinda.Data.Repositories.Abstract;
using Comeinda.Data.Tables;

namespace Comeinda.Data.Repositories
{
    public class EventRepositoty : BaseRepository<EventTable>, IEventRepository
    {
        public EventRepositoty(ComeindaDbContext context) : base(context)
        {

        }
    }
}
