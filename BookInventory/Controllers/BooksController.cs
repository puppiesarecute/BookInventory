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
using BookInventory.Helpers;
using AutoMapper;

namespace BookInventory.Controllers
{
    [Authorize]
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
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DbSet<Book> bookDbSet = uow.BookRepository.dbSet;
            Book book = bookDbSet.Include(x => x.Authors).Include(y => y.Categories).Include(z => z.LocationCodes).FirstOrDefault(b => b.BookId == id);
            if (book == null)
            {
                return HttpNotFound();
            }
            BookDataViewModel details = new BookDataViewModel
            {
                Book = book,
                AuthorsText = ListExtension.ConvertToString<Author>(book.Authors),
                CategoriesText = ListExtension.ConvertToString<Category>(book.Categories),
                LocationCodes = book.LocationCodes
            };
            return View(details);
        }

        //// GET: Books/Create
        [HttpGet]
        [Authorize(Users = "admin@administrators.com")]
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
        [Authorize(Users = "admin@administrators.com")]
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
                    if (itemCode != null)
                    {
                        bdModel.Book.LocationCodes = uow.LocationCodeRepository.Get(x => itemCode.Contains(x.Id)).ToList();
                    }                    

                    // save to db
                    uow.BookRepository.Insert(bdModel.Book);                    
                }
                else //this Isbn already exist in db, do nothing
                {
                    // detech changes and save the chnaged fields to existing
                    //existingIsbn.Title = bdModel.Book.Title;
                    //existingIsbn.Subtitle = bdModel.Book.Subtitle;
                    //existingIsbn.Publisher = bdModel.Book.Publisher;
                    //existingIsbn.PublishedDate = bdModel.Book.PublishedDate;
                    //existingIsbn.CostPrice = bdModel.Book.CostPrice;
                    //existingIsbn.SalesPrice = bdModel.Book.SalesPrice;
                    //existingIsbn.Quantity = bdModel.Book.Quantity;

                    //string existingAuthors = ListExtension.ConvertToString<Author>(existingIsbn.Authors);

                    
                    //uow.BookRepository.Update(existingIsbn);
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
        [HttpGet]
        [Authorize(Users = "admin@administrators.com")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //TODO create the include method in genericRepo
            DbSet<Book> bookDbSet = uow.BookRepository.dbSet;
            Book book = bookDbSet.Include(x => x.Authors).Include(y => y.Categories).Include(z => z.LocationCodes).FirstOrDefault(b => b.BookId == id);
            if (book == null)
            {
                return HttpNotFound();
            }
            BookDataViewModel editBook = new BookDataViewModel
            {
                Book = book,
                LocationCodes = uow.LocationCodeRepository.Get(),
                AuthorsText = ListExtension.ConvertToString<Author>(book.Authors),
                CategoriesText = ListExtension.ConvertToString<Category>(book.Categories)
            };
            return View(editBook);
        }

        //// POST: Books/Edit/5
        [HttpPost]
        [Authorize(Users = "admin@administrators.com")]
        public ActionResult Edit(BookDataViewModel data, IEnumerable<int> itemCode)
        {
            if (ModelState.IsValid)
            {
                //TODO process data to save new authors, categories and itemcode
                uow.BookRepository.Update(data.Book);
                return RedirectToAction("Index");
            }

            data.LocationCodes = uow.LocationCodeRepository.Get();
            return View(data);
        }
    }
}
