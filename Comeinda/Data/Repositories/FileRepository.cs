using Comeinda.Data.Repositories.Abstract;
using Comeinda.Data.Tables;

namespace Comeinda.Data.Repositories
{
    public class FileRepository : BaseRepository<FileTable>, IFileRepository
    {
        public FileRepository(ComeindaDbContext context) : base(context)
        {
        }
    }
}
