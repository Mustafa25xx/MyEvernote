using MyEvernote.DataAccessLayer.EntityFramework;
using MyEvernote.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.BusinessLayer
{
    public class Test
    {
        private Repository<EverNoteUser> repouser = new Repository<EverNoteUser>();
        private Repository<Category> repo = new Repository<Category>();
        private Repository<Note> repo_note = new Repository<Note>();
        private Repository<Comment> repo_comment = new Repository<Comment>();




        public Test()
        {
            //DatabaseContext db = new DatabaseContext();
            //db.Database.CreateIfNotExists(); fake data yoksa db oluşturur
            //db.Categories.ToList();// fake datayı basmak için kullanırız.

            //List<Category> categories = repo.List();
            //List<Category> categories_filter = repo.List(x => x.Id > 5);
        }
        public void InsertTest()
        {
            int result = repouser.Insert(new EverNoteUser()
            {

                Name = FakeData.NameData.GetFirstName(),
                Email = FakeData.NetworkData.GetEmail(),
                Surname = FakeData.NameData.GetSurname(),
                ActivateGuid = Guid.NewGuid(),
                IsActive = true,
                Admin = false,
                Username = "kara_murat",
                Password = "123",
                CreatedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                ModifiedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                ModifiedUserName = "aabb"




            });

        }
        public void UpdateTest()
        {
            EverNoteUser user = repouser.Find(x => x.Username == "kübra");
            if (user != null)
            {

                user.Username = "hatice";
                int result = repouser.Save();
            }



        }
        public void Commend()
        {
            EverNoteUser user = repouser.Find(x => x.Id == 1);
            Note note = repo_note.Find(x => x.Id == 3);


            Comment commend = new Comment()
            {
                Text = "Test aşamasındayız",
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now,
                ModifiedUserName = "kübra",
                Note = note,
                Owner= user,




            };
            repo_comment.Insert(commend);
        }
        public void CommendDelete()
        {
            Comment comment = repo_comment.Find(x => x.Id == 211);
            repo_comment.Delete(comment);
            
        }

    }
}
