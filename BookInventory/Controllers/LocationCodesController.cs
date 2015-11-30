using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookInventory.Models;

namespace BookInventory.Controllers
{
    public class LocationCodesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: LocationCodes
        public ActionResult Index()
        {
            return View(db.LocationCodes.ToList());
        }

        // GET: LocationCodes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LocationCode locationCode = db.LocationCodes.Find(id);
            if (locationCode == null)
            {
                return HttpNotFound();
            }
            return View(locationCode);
        }

        // GET: LocationCodes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LocationCodes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Code,Description")] LocationCode locationCode)
        {
            if (ModelState.IsValid)
            {
                db.LocationCodes.Add(locationCode);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(locationCode);
        }

        // GET: LocationCodes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LocationCode locationCode = db.LocationCodes.Find(id);
            if (locationCode == null)
            {
                return HttpNotFound();
            }
            return View(locationCode);
        }

        // POST: LocationCodes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Code,Description")] LocationCode locationCode)
        {
            if (ModelState.IsValid)
            {
                db.Entry(locationCode).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(locationCode);
        }

        // GET: LocationCodes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LocationCode locationCode = db.LocationCodes.Find(id);
            if (locationCode == null)
            {
                return HttpNotFound();
            }
            return View(locationCode);
        }

        // POST: LocationCodes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LocationCode locationCode = db.LocationCodes.Find(id);
            db.LocationCodes.Remove(locationCode);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
