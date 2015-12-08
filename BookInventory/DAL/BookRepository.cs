//using BookInventory.Models;
//using System;
//using System.Collections.Generic;
//using System.Data.Entity;
//using System.Linq;
//using System.Web;
//using System.Linq.Expressions;

//namespace BookInventory.Repositories
//{
//    public class BookRepository : IRepository<Book>, IDisposable
//    {
//        private ApplicationDbContext dbContext;
//        private bool disposed = false; 

//        public BookRepository(ApplicationDbContext context)
//        {
//            this.dbContext = context;
//        }

//        public void Delete(int id)
//        {
//            var book = dbContext.Books.Find(id);
//            dbContext.Books.Remove(book);
//        }

//        protected virtual void Dispose(bool disposing)
//        {
//            if (!this.disposed)
//            {
//                if (disposing)
//                {
//                    dbContext.Dispose();
//                }
//            }
//            this.disposed = true;
//        }

//        public void Dispose()
//        {
//            Dispose(true);
//            GC.SuppressFinalize(this);
//        }

//        public Book GetByID(int id)
//        {
//            return dbContext.Books.Find(id);
//        }

//        public void Save()
//        {
//            dbContext.SaveChanges();
//        }

//        IQueryable<Book> IRepository<Book>.GetAll()
//        {
//            return dbContext.Books;
//        }

//        public IQueryable<Book> AllIncluding(params Expression<Func<Book, object>>[] includeProperties)
//        {
//            IQueryable<Book> query = dbContext.Books;
//            foreach (var includeProperty in includeProperties)
//            {
//                query = query.Include(includeProperty);
//            }
//            return query;
//        }

//        public void InsertOrUpdate(Book t)
//        {
//            if (t.BookId == 0) //new
//            {
//                dbContext.Books.Add(t);
//            }
//            else //edit
//            {
//                dbContext.Entry(t).State = EntityState.Modified;
//            }
//        }
//    }
//}