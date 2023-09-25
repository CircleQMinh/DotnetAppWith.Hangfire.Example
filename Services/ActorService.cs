using DotnetAppWith.Hangfire.Example.Common;
using DotnetAppWith.Hangfire.Example.DTOs.Actors;
using DotnetAppWith.Hangfire.Example.Models;
using DotnetAppWith.Hangfire.Example.Repository.Actors;
using DotnetAppWith.Hangfire.Example.Repository.Actors;
using System.Linq.Expressions;

namespace DotnetAppWith.Hangfire.Example.Services
{
    public class ActorService : IActorService
    {
        private readonly IActorRepository _repository;
        public ActorService(IActorRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Actor>> GetAll(Expression<Func<Actor, bool>> expression = null, Func<IQueryable<Actor>, IOrderedQueryable<Actor>> orderBy = null, List<string> includes = null, PaginationFilter paginationFilter = null)
        {
            var result = await _repository.GetAll(expression, orderBy, includes, paginationFilter);
            return result.ToList();
        }

        public async Task<Actor> GetById(int id, List<string> includes)
        {
            var result = await _repository.Get(q => q.Id == id, includes);
            return result;
        }

        public Task<int> GetCount(Expression<Func<Actor, bool>> expression = null)
        {
            var result = _repository.GetCount(expression);
            return result;
        }

        public async Task<bool> Insert(CreateActorDTO dto)
        {
            var newActor = new Actor()
            {
                Name = dto.Name,
            };
            await _repository.Insert(newActor);
            return await _repository.SaveChangesAsync();
        }

        public async Task<bool> Update(UpdateActorDTO dto, int id)
        {
            var actor = await _repository.Get(q => q.Id == id);
            if (actor == null)
            {
                return false;
            }
            _repository.Update(actor);
            actor.Name = dto.Name;
            return await _repository.SaveChangesAsync();
        }

        public async Task<bool> Delete(int id)
        {
            var actor = await _repository.Get(q => q.Id == id);
            if (actor == null)
            {
                return false;
            }
            await _repository.Delete(id);
            return await _repository.SaveChangesAsync();
        }
    }
}
