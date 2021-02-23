using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using MyEvernote.Entities;

namespace MyEvernote.DataAccessLayer.EntityFramework
{
    public class MyInitializer : CreateDatabaseIfNotExists<DatabaseContext>
    {
        protected override void Seed(DatabaseContext context)
        {
            EverNoteUser admin = new EverNoteUser()
            {
                photo = "pngtree - user - vector - avatar - png - image_1541962",


                Name = "Mustafa",
                Email = "mkara@gmail.com",
                Surname = "Kara",
                ActivateGuid = Guid.NewGuid(),
                IsActive = true,
                Admin = true,
                Username = "mkara",
                Password = "Erzurum",
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now.AddMinutes(5),
                ModifiedUserName = "mkara"


            };
            EverNoteUser standartUser = new EverNoteUser()
            {
                photo = "pngtree - user - vector - avatar - png - image_1541962",


                Name = "kübra",
                Email = "kübra@gmail.com",
                Surname = "Kara",
                ActivateGuid = Guid.NewGuid(),
                IsActive = true,
                Admin = false,
                Username = "kübra",
                Password = "Erzurum",
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now.AddMinutes(5),
                ModifiedUserName = "mkara"


            };

            context.EverNoteUsers.Add(admin);
            context.EverNoteUsers.Add(standartUser);

            for (int i = 0; i < 8; i++)
            {
                EverNoteUser user = new EverNoteUser()
                {
                    photo = "pngtree - user - vector - avatar - png - image_1541962",
                    Name = FakeData.NameData.GetFirstName(),
                    Email = FakeData.NetworkData.GetEmail(),
                    Surname = FakeData.NameData.GetSurname(),
                    ActivateGuid = Guid.NewGuid(),
                    IsActive = true,
                    Admin = false,
                    Username = $"user{i}",
                    Password = "123",
                    CreatedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                    ModifiedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                    ModifiedUserName = $"user{i}"


                };
                context.EverNoteUsers.Add(user);


            }

            context.SaveChanges();

            for (int i = 0; i < 10; i++)
            {
                Category cat = new Category()
                {
                    Title = FakeData.PlaceData.GetStreetName(),
                    Description = FakeData.PlaceData.GetAddress(),
                    CreatedOn = DateTime.Now,
                    ModifiedOn = DateTime.Now,
                    ModifiedUserName = "mkara"
                };
                context.Categories.Add(cat);
                for (int k = 0; k < FakeData.NumberData.GetNumber(5, 9); k++)
                {
                    Note note = new Note()
                    {

                        Tittle = FakeData.TextData.GetAlphabetical(FakeData.NumberData.GetNumber(5, 25)),
                        Text = FakeData.TextData.GetSentences(FakeData.NumberData.GetNumber(1, 3)),
                        //Categories=cat,
                        IsDraft = false,
                        LikeCount = FakeData.NumberData.GetNumber(1, 9),
                        Owner = (k % 2 == 0) ? admin : standartUser,
                        CreatedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                        ModifiedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),

                        ModifiedUserName = (k % 2 == 0) ? admin.Username : standartUser.Username



                    };
                    cat.Notes.Add(note);
                    for (int j = 0; j < FakeData.NumberData.GetNumber(3, 5); j++)
                    {
                        Comment comment = new Comment()
                        {

                            Text = FakeData.TextData.GetSentence(),
                            CreatedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                            ModifiedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),

                            ModifiedUserName = (j % 2 == 0) ? admin.Username : standartUser.Username,

                        };
                        note.Comments.Add(comment);

                    }
                    List<EverNoteUser> userList = context.EverNoteUsers.ToList();
                    for (int l = 0; l < FakeData.NumberData.GetNumber(1, 9); l++)
                    {
                        Liked liked = new Liked()
                        {

                            LikedUser = userList[l]
                        };

                        note.Likes.Add(liked);

                    }

                }

            }
            context.SaveChanges();
        }

    }
}
