using BookInventory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookInventory.DAL
{
    public class UnitOfWork : IDisposable
    {
        private ApplicationDbContext context = new ApplicationDbContext();
        private GenericRepository<Book> bookRepository;
        private GenericRepository<Author> authorRepository;
        private GenericRepository<LocationCode> locationCodeRepository;
        // Add more repos

        public GenericRepository<Book> BookRepository
        {
            get
            {

                if (this.bookRepository == null)
                {
                    this.bookRepository = new GenericRepository<Book>(context);
                }
                return bookRepository;
            }
        }

        public GenericRepository<Author> AuthorRepository
        {
            get
            {

                if (this.authorRepository == null)
                {
                    this.authorRepository = new GenericRepository<Author>(context);
                }
                return authorRepository;
            }
        }

        public GenericRepository<LocationCode> LocationCodeRepository
        {
            get
            {
                if (this.locationCodeRepository == null)
                {
                    this.locationCodeRepository = new GenericRepository<LocationCode>(context);
                }
                return locationCodeRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}