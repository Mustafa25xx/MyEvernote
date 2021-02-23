using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyEvernote.BusinessLayer;
using MyEvernote.Entities;
using MyEvernote.Filter;
using MyEvernote.Models;

namespace MyEvernote.Controllers
{
    [Auth]
    public class CategoriesController : Controller
    {
        private CategoryManager manager = new CategoryManager();
        // GET: Categories
        public ActionResult Index()
        {
           
            return View(CacheHelper.GetCategoriesFromCache());
        }

        // GET: Categories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = manager.Find(x => x.Id == id.Value);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: Categories/Create
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category category)
        {
            ModelState.Remove("CreatedOn");
            ModelState.Remove("ModifiedOn");
            ModelState.Remove("ModifiedUsername");
            if (ModelState.IsValid)
            {
                int db = manager.Insert(category);
                CacheHelper.RemoveCategoriesFromCache();
                if (db > 0)
                {
                    return RedirectToAction("Index");
                }
                else return View(category);
            }

            return View(category);
        }

        // GET: Categories/Edit/5
        public ActionResult Edit(int? id)
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category cat)
        {

            ModelState.Remove("CreatedOn");
            ModelState.Remove("ModifiedOn");
            ModelState.Remove("ModifiedUsername");

            if (ModelState.IsValid)
            {
                Category category = new Category();
                category = manager.Find(x => x.Id == cat.Id);
                category.Title = cat.Title;
                category.Description = cat.Description;
                manager.Update(category);
                CacheHelper.RemoveCategoriesFromCache();

                return RedirectToAction("Index");
            }

            return View(cat);
        }

        // GET: Categories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Category category = manager.Find(x => x.Id == id.Value);

            if (category == null)
            {
                return HttpNotFound();
            }

            return View(category);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = manager.Find(x => x.Id == id);
            manager.Delete(category);

            CacheHelper.RemoveCategoriesFromCache();


            return RedirectToAction("Index");
        }


    }
}
