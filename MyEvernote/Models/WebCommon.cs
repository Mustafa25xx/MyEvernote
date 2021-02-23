using MyEvernote.Common;
using MyEvernote.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyEvernote.Models
{
    public class WebCommon : Icommon
    {
        public string GetUsername()
        {
            if (HttpContext.Current.Session["login"] != null)
            {


                EverNoteUser everNoteUser = HttpContext.Current.Session["login"] as EverNoteUser;
                return everNoteUser.Username;

            }
            return "system";
        }
    }
}