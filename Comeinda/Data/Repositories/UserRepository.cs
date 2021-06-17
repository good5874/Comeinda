using Comeinda.Data.Repositories.Abstract;
using Comeinda.Data.Tables;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Comeinda.Data.Repositories
{
    public class UserRepository : BaseRepository<CastomUser>, IUserRepository<CastomUser>
    {
        public UserRepository(ComeindaDbContext context) : base(context)
        {
        }

        public async Task<CastomUser> GetUserByPhone(string phone)
        {
            return await context.Users.FirstOrDefaultAsync(x => x.PhoneNumber == phone);
        }
    }
}
