﻿using System;
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
using BookInventory.Helpers;

namespace BookInventory.Controllers
{
    public class BooksController : Controller
    {
        private UnitOfWork uow = new UnitOfWork();

        // GET: Books
        public ActionResult Index()
        {
            var books = uow.BookRepository.Get();
            return View(books);
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
        public ActionResult Create(BookDataViewModel bdModel, IEnumerable<int> itemCode)
        {
            if (ModelState.IsValid)
            {
                // check if this ISbn no has been added, if yes do an update instead of insert
                var existingIsbn = uow.BookRepository.FirstOrDefault(x => x.Isbn10 == bdModel.Book.Isbn10);
                if (existingIsbn == null) //no existing Isbn, do insert
                {
                    // handle book's authors
                    bdModel.Book.Authors = new List<Author>();
                    if (bdModel.AuthorsText != null)
                    {
                        var authors = HelperClass.converStringToListAuthors(bdModel.AuthorsText);
                        foreach (var item in authors)
                        {
                            var existingAuthor = uow.AuthorRepository.FirstOrDefault(x => x.AuthorName == item.AuthorName);
                            if (existingAuthor != null)
                            {
                                bdModel.Book.Authors.Add(existingAuthor);
                            }
                            else
                            {
                                bdModel.Book.Authors.Add(item);
                            }
                        }
                    }

                    // handle book's categories
                    bdModel.Book.Categories = new List<Category>();
                    if (bdModel.CategoriesText != null)
                    {
                        var categories = HelperClass.converStringToListCategories(bdModel.CategoriesText);
                        foreach (var item in categories)
                        {
                            var existingCategory = uow.CategoryRepository.FirstOrDefault(x => x.CategoryName == item.CategoryName);
                            if (existingCategory != null)
                            {
                                bdModel.Book.Categories.Add(existingCategory);
                            }
                            else
                            {
                                bdModel.Book.Categories.Add(item);
                            }
                        }
                    }

                    // handle Location Codes
                    // TODO: handle when none checkbox is checked
                    bdModel.Book.LocationCodes = uow.LocationCodeRepository.Get(x => itemCode.Contains(x.Id)).ToList();

                    // save to db
                    uow.BookRepository.Insert(bdModel.Book);                    
                }
                else //this Isbn already exist in db
                {
                    existingIsbn.Quantity = bdModel.Book.Quantity;
                    uow.BookRepository.Update(existingIsbn);
                }

                uow.BookRepository.SaveChanges();
                return RedirectToAction("Index");
            }

            BookDataViewModel initial = new BookDataViewModel
            {
                Book = new Book(),
                LocationCodes = uow.LocationCodeRepository.Get()
            };
            return View(initial);
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
