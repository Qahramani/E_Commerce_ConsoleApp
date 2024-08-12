using ORM_Mini_Project.Models.Common;
using System.Linq.Expressions;

namespace ORM_Mini_Project.Repositories.Interfaces.Generic;

public interface IRepository<T> where T : BaseEntity
{
    Task CreateAsync(T entity);
    void Delete(T entity);
    void Update(T entity);
    Task<T> GetSingleAsync(Expression<Func<T,bool>> predicate, params string[] includes);
    Task<List<T>> GetAllAsync(params string[] includes);
    Task<List<T>> GetFilterAsync(Expression<Func<T, bool>> expression, params string[] includes);
    Task<int> SaveAllChangesAsync();
    Task<bool> IsExistAsync(Expression<Func<T,bool>> predicate);

}
