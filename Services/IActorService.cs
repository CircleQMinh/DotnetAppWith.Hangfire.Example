using DotnetAppWith.Hangfire.Example.Common;
using DotnetAppWith.Hangfire.Example.DTOs.Actors;
using DotnetAppWith.Hangfire.Example.Models;
using System.Linq.Expressions;

namespace DotnetAppWith.Hangfire.Example.Services
{
    public interface IActorService
    {
        Task<Actor> GetById(int id, List<string> includes);
        Task<List<Actor>> GetAll(Expression<Func<Actor, bool>> expression = null, Func<IQueryable<Actor>, IOrderedQueryable<Actor>> orderBy = null, List<string> includes = null, PaginationFilter paginationFilter = null);
        Task<int> GetCount(Expression<Func<Actor, bool>> expression = null);
        Task<bool> Insert(CreateActorDTO dto);
        Task<bool> Update(UpdateActorDTO dto, int id);
        Task<bool> Delete(int id);
    }
}
