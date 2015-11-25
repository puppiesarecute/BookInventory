using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookInventory.Models;
using BookInventory.DAL;
using BookInventory.Models.ViewModels;

namespace BookInventory.Controllers
{
    public class BooksController : Controller
    {
        private UnitOfWork uow = new UnitOfWork();

        // GET: Books
        public ActionResult Index()
        {
            //var books = unitOfWork.BookRepository.Get
            //return View(db.Books.ToList());
            return View();
        }

        //// GET: Books/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Book book = db.Books.Find(id);
        //    if (book == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(book);
        //}

        //// GET: Books/Create
        [HttpGet]
        public ActionResult Create()
        {
            BookDataViewModel bvm = new BookDataViewModel
            {
                Book = new Book(),
                LocationCodes = uow.LocationCodeRepository.Get()
            };
            return View(bvm);
        }

        //// POST: Books/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Isbn10,Isbn13,Title,Subtitle,Publisher,PublishedDate,Description,CostPrice,SalesPrice,Quantity")] BookDataViewModel bdModel)
        public ActionResult Create(BookDataViewModel bdModel, IEnumerable<int> itemCode)
        {
            if (ModelState.IsValid)
            {
                uow.BookRepository.Insert(bdModel.Book);
                uow.BookRepository.SaveChanges();
                return RedirectToAction("Index");
            }

            BookDataViewModel test = new BookDataViewModel
            {
                Book = new Book(),
                AuthorsText = String.Empty,
                CategoriesText = String.Empty,
                LocationCodes = uow.LocationCodeRepository.Get()
            };
            return View(test);
        }

        //// GET: Books/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Book book = db.Books.Find(id);
        //    if (book == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(book);
        //}

        //// POST: Books/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "BookId,Isbn10,Isbn13,Title,Subtitle,Publisher,PublishedDate,Description,CostPrice,SalesPrice,Quantity")] Book book)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(book).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(book);
        //}

        //// GET: Books/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Book book = db.Books.Find(id);
        //    if (book == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(book);
        //}

        //// POST: Books/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Book book = db.Books.Find(id);
        //    db.Books.Remove(book);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

    }
}
