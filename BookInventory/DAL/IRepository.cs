//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Linq.Expressions;

//namespace BookInventory.Repositories
//{
//    public interface IRepository<T> : IDisposable where T : class
//    {
//        IQueryable<T> GetAll();
//        IQueryable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties);
//        void InsertOrUpdate(T t);
//        T GetByID(int id);
//        void Delete(int id);
//        void Save();
//    }
//}
