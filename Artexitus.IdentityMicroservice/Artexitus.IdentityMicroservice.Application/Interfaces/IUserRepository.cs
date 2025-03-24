using Artexitus.IdentityMicroservice.Domain.Entities;

namespace Artexitus.IdentityMicroservice.Application.Interfaces
{
    public interface IUserRepository : IRepository<User>, IPaginatableRepository<User>, 
        ISearchableRepository<User>
    {
    }
}
