using MyEvernote.BusinessLayer.Abstract;
using MyEvernote.Common;
using MyEvernote.Common.Helpers;
using MyEvernote.Core.DataAccess;
using MyEvernote.Entities;
using MyEvernote.Entities.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.BusinessLayer
{
    public class UserManager :ManagerBase<EverNoteUser>
    {

        public BusinessLayerResult<EverNoteUser> RegisterUser(RegisterViewModel data)
        {
            BusinessLayerResult<EverNoteUser> layerResult = new BusinessLayerResult<EverNoteUser>();

            EverNoteUser everNoteUser = Find(x => x.Username == data.Username || x.Email == data.Email);
            if (everNoteUser != null)
            {
                if (everNoteUser.Username == data.Username)
                {
                    layerResult.Errors.Add("Kullanıcı Adı Kayıtlı");

                }

                if (everNoteUser.Email == data.Email)
                {
                    layerResult.Errors.Add("Email Adı Kayıtlı");

                }

            }
            else
            {
                int dbresult = base.Insert(new EverNoteUser
                {
                    Username = data.Username,
                    Password = data.Password,
                    Email = data.Email,
                    ActivateGuid = Guid.NewGuid(),
                    photo = "photouser1.jpg",
                    IsActive = true,
                    Admin = false,


                }); ;
                //if (dbresult > 1)
                // 
                //    layerResult.result = repos_user.Find(x => x.Username == data.Username && x.Email == data.Email);
                //    string siteUri = ConfigHelper.Get<string>("SiteRootUrl");
                //    MailHelper.SendMail($"Hesabınızı Aktifleştirmek için < a href='{layerResult.result.ActivateGuid}'> tıklayınız.</a>", layerResult.result.Email,"Hesap Aktifleştirme");

                //    //layerResult.result.ActivateGuid
                //}
                

            }

            return layerResult;
        }
        public BusinessLayerResult<EverNoteUser> LoginUser(LoginViewModel data)
        {


            BusinessLayerResult<EverNoteUser> businessLayerResult = new BusinessLayerResult<EverNoteUser>();
            businessLayerResult.result = Find(x => x.Username == data.Username && x.Password == data.Password);
            if (businessLayerResult.result != null)
            {

                if (businessLayerResult.result.IsActive == false)
                {
                    businessLayerResult.Errors.Add("Kullanıcı Aktif edilmemiştir. ");


                }


            }
            else
            {
                businessLayerResult.Errors.Add("Kullanıcı Adı ve ya şifre yanlış ");

            }

            return businessLayerResult;


        }
        //  

        public EverNoteUser GetUserBySession()
        {
            string userstorage = App.Common.GetUsername();
            EverNoteUser everNoteUser = Find(x => x.Username == userstorage);
            return everNoteUser;

        }

        public EverNoteUser GetUserBySession(int  id)
        {
            //string userstorage = App.Common.GetUsername();
            EverNoteUser everNoteUser =Find(x => x.Id == id);
            return everNoteUser;

        }

        public BusinessLayerResult<EverNoteUser> Update(EverNoteUser everNoteUser)
        {
            //string userstorage = App.Common.GetUsername();


            BusinessLayerResult<EverNoteUser> res = new BusinessLayerResult<EverNoteUser>();
            EverNoteUser everNoteUser1 = Find(x => x.Id != everNoteUser.Id && (x.Username == everNoteUser.Username || x.Email == everNoteUser.Email));
            res.result = everNoteUser;
            if (everNoteUser1 != null )
            {
                if (everNoteUser1.Username == everNoteUser.Username)
                {

                    res.Errors.Add("Bu kullanıcı adı mevcut. Farklı bir kullanıcı adı deneyiniz..");
                }
                if (everNoteUser1.Email == everNoteUser.Email)
                {

                    res.Errors.Add("Bu e mail mevcut. Farklı bir e mail  deneyiniz..");
                }
                return res;
            }

            res.result = Find(x => x.Id == everNoteUser.Id);
            res.result.Username = everNoteUser.Username;
            res.result.Surname = everNoteUser.Surname;
            res.result.Password = everNoteUser.Password;
            res.result.Name = everNoteUser.Name;
            res.result.Email = everNoteUser.Email;

            if (everNoteUser.photo != null)
            {
                res.result.photo = everNoteUser.photo;
            }



            int db = base.Update(res.result);
            return res;

        }

        public int DeleteProfile(EverNoteUser everNoteUser)
        {

            EverNoteUser everNoteUser1 = Find(x => x.Id == everNoteUser.Id);
            int deleteitem = base.Delete(everNoteUser1);
            return deleteitem;
        }
        public new BusinessLayerResult<EverNoteUser> Insert(EverNoteUser data)
        {
            BusinessLayerResult<EverNoteUser> layerResult = new BusinessLayerResult<EverNoteUser>();

            EverNoteUser everNoteUser = Find(x => x.Username == data.Username || x.Email == data.Email);
            if (everNoteUser != null)
            {
                if (everNoteUser.Username == data.Username)
                {
                    layerResult.Errors.Add("Kullanıcı Adı Kayıtlı");

                }

                if (everNoteUser.Email == data.Email)
                {
                    layerResult.Errors.Add("Email Adı Kayıtlı");

                }

            }
            else
            {
                data.ActivateGuid = Guid.NewGuid();
                data.photo = "photouser1.jpg";
              base. Insert(data);
               



            }
            return layerResult;
        }

        public new BusinessLayerResult<EverNoteUser> UpdateUser(EverNoteUser everNoteUser)
        {
            //string userstorage = App.Common.GetUsername();


            BusinessLayerResult<EverNoteUser> res = new BusinessLayerResult<EverNoteUser>();
            EverNoteUser everNoteUser1 = Find(x => x.Id != everNoteUser.Id && (x.Username == everNoteUser.Username || x.Email == everNoteUser.Email));
            res.result = everNoteUser;
            if (everNoteUser1 != null)
            {
                if (everNoteUser1.Username == everNoteUser.Username)
                {

                    res.Errors.Add("Bu kullanıcı adı mevcut. Farklı bir kullanıcı adı deneyiniz..");
                }
                if (everNoteUser1.Email == everNoteUser.Email)
                {

                    res.Errors.Add("Bu e mail mevcut. Farklı bir e mail  deneyiniz..");
                }
                return res;
            }

            res.result = Find(x => x.Id == everNoteUser.Id);
            res.result.Username = everNoteUser.Username;
            res.result.Surname = everNoteUser.Surname;
            res.result.Password = everNoteUser.Password;
            res.result.Name = everNoteUser.Name;
            res.result.Email = everNoteUser.Email;


            int db = base.Update(res.result);
            return res;

        }
    }
}
