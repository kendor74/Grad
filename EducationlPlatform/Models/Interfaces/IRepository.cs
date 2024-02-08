﻿namespace EducationlPlatform.Models.Interfaces
{
    public interface IRepository<TEntity>
    {
        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity> FindById(int id);
        Task<TEntity> Create(TEntity entity);
        Task<bool> Delete(int id);
        Task<TEntity> Update(int id, TEntity entity);

        Task<bool> IsExist(int id);
        Task<int> Count();
    }
}
