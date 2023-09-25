using DotnetAppWith.Hangfire.Example.Data;
using DotnetAppWith.Hangfire.Example.Models;

namespace DotnetAppWith.Hangfire.Example.Repository.Actors
{
    public class ActorRepository : GenericRepository<Actor>, IActorRepository
    {
        public ActorRepository(AppDbContext databaseContext) : base(databaseContext)
        {
        }
    }
}
