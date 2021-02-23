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

namespace MyEvernote.Controllers
{
    [Auth]
    public class UserController : Controller
    {
        private UserManager manager = new UserManager();

        // GET: User
        public ActionResult Index()
        {
            return View(manager.List());
        }

        // GET: User/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EverNoteUser everNoteUser = manager.Find(x => x.Id == id.Value);
            if (everNoteUser == null)
            {
                return HttpNotFound();
            }
            return View(everNoteUser);
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EverNoteUser everNoteUser)
        {
            ModelState.Remove("CreatedOn");
            ModelState.Remove("ModifiedOn");
            ModelState.Remove("ModifiedUsername");

            BusinessLayerResult<EverNoteUser> user = new BusinessLayerResult<EverNoteUser>();
            if (ModelState.IsValid)
            {
                user = manager.Insert(everNoteUser);

                if (user.Errors.Count > 0)
                {
                    user.Errors.ForEach(x => ModelState.AddModelError("", x));
                    return View(everNoteUser);

                }
                return RedirectToAction("Index");
            }

            return View(everNoteUser);
        }

        // GET: User/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EverNoteUser everNoteUser = manager.Find(z => z.Id == id.Value);
            if (everNoteUser == null)
            {
                return HttpNotFound();
            }
            return View(everNoteUser);
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EverNoteUser everNoteUser)
        {
            BusinessLayerResult<EverNoteUser> user = new BusinessLayerResult<EverNoteUser>();

            ModelState.Remove("CreatedOn");
            ModelState.Remove("ModifiedOn");
            ModelState.Remove("ModifiedUsername");
            if (ModelState.IsValid)
            {
                user = manager.UpdateUser(everNoteUser);
                if (user.Errors.Count > 0)
                {
                    user.Errors.ForEach(x => ModelState.AddModelError("", x));
                    return View(everNoteUser);

                }
                return RedirectToAction("Index");
            }
            return View(everNoteUser);
        }

        // GET: User/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EverNoteUser everNoteUser = manager.Find(x => x.Id == id);
            if (everNoteUser == null)
            {
                return HttpNotFound();
            }
            return View(everNoteUser);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EverNoteUser everNoteUser = manager.Find(x => x.Id == id);
            manager.Delete(everNoteUser);
            return RedirectToAction("Index");
        }


    }
}
