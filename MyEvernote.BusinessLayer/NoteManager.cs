using MyEvernote.BusinessLayer.Abstract;
using MyEvernote.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.BusinessLayer
{
   public class NoteManager:ManagerBase<Note>
    {
        private Repository<Note> repos_not = new Repository<Note>();

        public List<Note> GetNotes()
        {

          return  repos_not.List();
        }
    }
}
