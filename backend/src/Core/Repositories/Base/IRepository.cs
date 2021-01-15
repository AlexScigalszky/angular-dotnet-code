using Core.Models;
using Core.Specifications.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.Repositories.Base
{
    public interface IRepository<T> // where T : Entity (COMMENTED JUST FOR A WHILE)
    {
        Task<IReadOnlyList<T>> GetAllAsync();
        IQueryable<T> GetQueryable(ISpecification<T> spec);
        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate);
        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null,
                                        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                        string includeString = null,
                                        bool disableTracking = true);
        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null,
                                        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                        List<Expression<Func<T, object>>> includes = null,
                                        bool disableTracking = true);
        Task<IReadOnlyList<T>> GetAsync(ISpecification<T> spec);
        Task<PageableList<T>> GetPageableListAsync(Expression<Func<T, bool>> predicate);
        Task<PageableList<T>> GetPageableListAsync(Expression<Func<T, bool>> predicate = null,
                                                    Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                                    string includeString = null,
                                                    bool disableTracking = true);
        Task<PageableList<T>> GetPageableListAsync(Expression<Func<T, bool>> predicate = null,
                                                    Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                                    List<Expression<Func<T, object>>> includes = null,
                                                    bool disableTracking = true);
        Task<PageableList<T>> GetPageableListAsync(ISpecification<T> spec);
        Task<T> GetByIdAsync(long id);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task UpdateAsync(IEnumerable<T> entities);
        Task DeleteAsync(T entity);
        Task<int> CountAsync(ISpecification<T> spec);
    }
}
