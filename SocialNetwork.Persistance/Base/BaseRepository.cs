

using Microsoft.EntityFrameworkCore;
using SocialNetwork.Domain.Repositories;
using SocialNetwork.Domain.Result;
using SocialNetwork.Persistance.Context;
using System.Linq.Expressions;

namespace SocialNetwork.Persistance.Base
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly SocialNetworkContext _socialNetworkContext;
        private DbSet<TEntity> entities;

        public BaseRepository(SocialNetworkContext socialNetworkContext)
        {
            _socialNetworkContext = socialNetworkContext;
            this.entities = socialNetworkContext.Set<TEntity>();
        }

        public virtual async Task<OperationResult> Remove(TEntity entity)
        {
            OperationResult result = new OperationResult();

            try
            {
                entities.Remove(entity);
                await _socialNetworkContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                result.Success = false;
                result.Message = "Hubo un error eliminando la entidad.";
            }
            return result;
        }

        public virtual async Task<bool> Exists(Expression<Func<TEntity, bool>> filter)
        {
            return await this.entities.AnyAsync(filter);
        }

        public virtual async Task<OperationResult> GetAll()
        {
            OperationResult result  = new OperationResult();

            try
            {
                var datos = await this.entities.ToListAsync();
                result.Data = datos;
            }
            catch (Exception)
            {
                result.Success = false;
                result.Message = "Hubo un error obteniendo las entidades.";
            }
            return result;
        }

        public virtual async Task<OperationResult> GetById(int id)
        {
            OperationResult result = new OperationResult();

            try
            {
                var entity = await this.entities.FindAsync(id);
                result.Data = entity;
            }
            catch (Exception)
            {
                result.Success = false;
                result.Message = "hubo un error obteniendo la entidad.";
            }
            return result;
        }

        public virtual async Task<OperationResult> Save(TEntity entity)
        {
            OperationResult result = new OperationResult();

            try
            {
                entities.Add(entity);
                await _socialNetworkContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                result.Success = false;
                result.Message = "Hubo un error guardando la entidad.";
            }
            return result;
        }

        public virtual async Task<OperationResult> Update(TEntity entity)
        {
            OperationResult result = new OperationResult();

            try
            {
                entities.Update(entity);
                await _socialNetworkContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                result.Success = false;
                result.Message = "Hubo un error actualizando la entidad.";
            }
            return result;
        }
    }
}
