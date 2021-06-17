using Comeinda.Data.Repositories.Abstract;
using Comeinda.Data.Tables;

namespace Comeinda.Data.Repositories
{
    public class CategoryRepository : BaseRepository<CategoryEventTable>, ICategoryRepository
    {
        public CategoryRepository(ComeindaDbContext context) : base(context)
        {
        }
    }
}
