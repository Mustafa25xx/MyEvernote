using MyEvernote.BusinessLayer;
using MyEvernote.Entities;
using MyEvernote.Entities.ValueObject;
using MyEvernote.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MyEvernote.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home

        public ActionResult Select(int? id)
        {
            if (id == null)
            {

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryManager cat = new CategoryManager();
            Category ct = cat.Find(x => x.Id == id.Value);
            if (cat == null)
            {

                return RedirectToAction("Index", "Home");
            }
            TempData["mm"] = ct.Notes;
            return RedirectToAction("Index", "Home");

        }
        public ActionResult Index()
        {
            //Test test = new MyEvernote.BusinessLayer.Test();
            //test.InsertTest();
            //test.UpdateTest();
            //test.Commend();
            //test.CommendDelete();
            if (TempData["mm"] != null)
            {
                return View(TempData["mm"] as List<Note>);
            }
            NoteManager noteManager = new NoteManager();

            return View(noteManager.GetNotes());
        }
        public ActionResult Login()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                UserManager user = new UserManager();
                BusinessLayerResult<EverNoteUser> businessLayerResult = new BusinessLayerResult<EverNoteUser>();
                businessLayerResult = user.LoginUser(loginViewModel);
                if (businessLayerResult.Errors.Count > 0)
                {
                    businessLayerResult.Errors.ForEach(x => ModelState.AddModelError("", x));
                    return View(loginViewModel);

                }
                Session["login"] = businessLayerResult.result;
                return RedirectToAction("Index", "Home");
            }
            return View(loginViewModel);


        }

        public ActionResult Register()
        {


            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel register)
        {
            if (ModelState.IsValid)
            {

                UserManager user = new UserManager();
                BusinessLayerResult<EverNoteUser> businessLayerResult = user.RegisterUser(register);
                #region modelvalidate
                //if (register.Username == "aa")
                //{

                //    ModelState.AddModelError("", "Kullanıcı Adı Mevcut.Lütfen Farklı Kullanıcı İsmi Deneyiniz.");

                //}
                //if (register.Email == "aa")
                //{

                //    ModelState.AddModelError("", "Mail adresi Mevcut.Lütfen Farklı Bir Mail Adresi Deneyiniz.");

                //}

                //foreach( var item in ModelState)
                //{
                //    if (item.Value.Errors.Count > 0) return View(register);

                //}
                #endregion

                if (businessLayerResult.Errors.Count > 0)
                {
                    businessLayerResult.Errors.ForEach(x => ModelState.AddModelError("", x));
                    return View(register);

                }

                return RedirectToAction("RegisterOk");

            }

            return View(register);
        }

        public ActionResult RegisterOk()
        {
            return View();
        }

        public ActionResult Exit()
        {

            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        //public ActionResult UserActivate(Guid id)
        //{
        //    UserManager evernoteuse = new UserManager();
        //    BusinessLayerResult<EverNoteUser> result = evernoteuse.ActivateUser(id);
        //    if (result.Errors.Count > 0)
        //    {

        //        TempData["Errors"] = result;
        //        return RedirectToAction("UserActivateCancel");
        //    }
        //    return View();
        //}

        public ActionResult UserActivateOk(Guid activateno)
        {
            return View();

        }

        [Auth]

        public ActionResult ShowProfile()
        {

            EverNoteUser everNoteUser = Session["login"] as EverNoteUser;
            UserManager userManager = new UserManager();
            
            //UserManager userManager = new UserManager();
            EverNoteUser everNoteUser1 = userManager.GetUserBySession(everNoteUser.Id);

            return View(everNoteUser1);
            //return View(everNoteUser);
        }
        //public ActionResult UserActivateCancel(Guid activateno)
        //{
        //    BusinessLayerResult<EverNoteUser> result = TempData["error"] as BusinessLayerResult<EverNoteUser>;


        //    if (TempData["error"] != null)
        //    {
        //        result.Errors.ForEach(x => ModelState.AddModelError("", x));
        //    }
        //    return View();

        //}
        [Auth]

        public ActionResult EditProfile()
        {

            EverNoteUser everNoteUser = Session["login"] as EverNoteUser;

            UserManager user = new UserManager();
            EverNoteUser everNoteUser1 = user.GetUserBySession(everNoteUser.Id);
            return View(everNoteUser1);


        }

        [Auth]

        [HttpPost]
        public ActionResult EditProfile(EverNoteUser everNoteUser,HttpPostedFileBase ProfileImage)
        {
            ModelState.Remove("ModifiedUserName");
            if (ModelState.IsValid)
            {

                if (ProfileImage != null &&
                        (ProfileImage.ContentType == "image/jpeg" ||
                        ProfileImage.ContentType == "image/jpg" ||
                        ProfileImage.ContentType == "image/png"))
                {
                    string filename = $"user_{everNoteUser.Id}.{ProfileImage.ContentType.Split('/')[1]}";

                    ProfileImage.SaveAs(Server.MapPath($"~/images/{filename}"));
                    everNoteUser.photo = filename;
                }

                UserManager userManager = new UserManager();
                BusinessLayerResult<EverNoteUser> everNoteUser1 = new BusinessLayerResult<EverNoteUser>();
                everNoteUser1 = userManager.Update(everNoteUser);

                if (everNoteUser1.Errors.Count > 0)
                {
                    everNoteUser1.Errors.ForEach(x => ModelState.AddModelError("", x));
                    return View(everNoteUser);

                }
                if (everNoteUser1.result != null)
                    Session["login"] = everNoteUser1.result;
                return RedirectToAction("ShowProfile");

            }

            return View(everNoteUser);

        }
        [Auth]

        public ActionResult Delete()
        {
           EverNoteUser everNoteUser= Session["login"] as EverNoteUser;
            UserManager userManager = new UserManager();
            int userdelete = userManager.DeleteProfile(everNoteUser);
            if (userdelete > 0)
            {
                Session.Clear();
                return RedirectToAction("Login");
            }

            return RedirectToAction("ProfileEdit");

        }
    }
}