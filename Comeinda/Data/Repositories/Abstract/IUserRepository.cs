using System.Threading.Tasks;

namespace Comeinda.Data.Repositories.Abstract
{
    public interface IUserRepository<TUser> : IBaseRepository<TUser> where TUser: class
    {
        public Task<TUser> GetUserByPhone(string phone);
    }
}
