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
    public class NoteController : Controller
    {
        private NoteManager manager = new NoteManager();
        private CategoryManager CategoryManager = new CategoryManager();
        private UserManager UserManager = new UserManager();
        // GET: Note
        public ActionResult Index()
        {
            ////var notes = db.Notes.Include(n => n.Categories);
            //List<Note> note = manager.List();
            //return View(note);

            EverNoteUser everNoteUser = Session["login"] as EverNoteUser;

            var notes = manager.ListQueryable().Include("Categories").Include("Owner").Where(
                x => x.Owner.Id == everNoteUser.Id).OrderByDescending(
                x => x.ModifiedOn);
            return View(notes.ToList());
        }

        // GET: Note/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Note note = manager.Find(x=>x.Id==id.Value);
            if (note == null)
            {
                return HttpNotFound();
            }
            return View(note);
        }

        // GET: Note/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(CategoryManager.List(), "Id", "Title");
            return View();
        }
        
        // POST: Note/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Note note)
        {
            ModelState.Remove("CreatedOn");
            ModelState.Remove("ModifiedOn");
            ModelState.Remove("ModifiedUsername");
            if (ModelState.IsValid)
            {
                EverNoteUser everNoteUser = Session["login"] as EverNoteUser;

                note.Owner = everNoteUser;
                manager.Insert(note);
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(CacheHelper.GetCategoriesFromCache(), "Id", "Title", note.CategoryId);
            return View(note);
        }

        // GET: Note/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Note note = manager.Find(x=>x.Id==id.Value);
            if (note == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(CacheHelper.GetCategoriesFromCache(), "Id", "Title", note.CategoryId);
            return View(note);
        }

        // POST: Note/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Note note)
        {
            ModelState.Remove("CreatedOn");
            ModelState.Remove("ModifiedOn");
            ModelState.Remove("ModifiedUsername");
            Note note1 = manager.Find(x => x.Id == note.Id);
            if (ModelState.IsValid)
            {
                note1.IsDraft = note.IsDraft;
                note1.CategoryId = note.CategoryId;
                note1.Text = note.Text;
                note1.Tittle = note.Tittle;

                manager.Update(note1);


                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(CacheHelper.GetCategoriesFromCache(), "Id", "Title", note.CategoryId);
            return View(note);
        }

        // GET: Note/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Note note = manager.Find(x=>x.Id==id.Value);
            if (note == null)
            {
                return HttpNotFound();
            }
            return View(note);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Note note)
        {
            Note note1 = manager.Find(x=>x.Id==note.Id);
            manager.Delete(note1);
            //db.SaveChanges();
            return RedirectToAction("Index");
        }

   
    }
}
