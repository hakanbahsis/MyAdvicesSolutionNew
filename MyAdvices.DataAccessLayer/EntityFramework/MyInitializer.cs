using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using MyAdvices.Entities;

namespace MyAdvices.DataAccessLayer.EntityFramework
{
    public class MyInitializer:CreateDatabaseIfNotExists<DatabaseContext>
    {
        protected override void Seed(DatabaseContext context)
        {
            //Adding admin user
            AdvicesUser admin = new AdvicesUser()
            {
                Name = "Hakan",
                Surname = "Bahşiş",
                Email = "hakanbahsis@outlook.com",
                ActivateGuid = Guid.NewGuid(),
                IsActive = true,
                IsAdmin = true,
                Username = "hakanbahsis",
                ProfileImageFilename="user.png",
                Password = "123456",
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now.AddMinutes(5),
                ModifiedUsername = "hakanbahsis"
            };

            //adding standart user
            AdvicesUser standarUser = new AdvicesUser()
            {
                Name = "Baran",
                Surname = "Bahşiş",
                Email = "hakanbahsis@outlook.com",
                ActivateGuid = Guid.NewGuid(),
                IsActive = true,
                IsAdmin = false,
                Username = "baranbahsis",
                Password = "654321",
                ProfileImageFilename = "user.png",
                CreatedOn = DateTime.Now.AddHours(1),
                ModifiedOn = DateTime.Now.AddMinutes(65),
                ModifiedUsername = "hakanbahsis"
            };

            context.AdvicesUsers.Add(admin);
            context.AdvicesUsers.Add(standarUser);

            for (int i = 0; i < 8; i++)
            {
                AdvicesUser user = new AdvicesUser()
                {
                    Name = FakeData.NameData.GetFirstName(),
                    Surname = FakeData.NameData.GetSurname(),
                    Email = FakeData.NetworkData.GetEmail(),
                    ActivateGuid = Guid.NewGuid(),
                    ProfileImageFilename = "user.png",
                    IsActive = true,
                    IsAdmin = false,
                    Username =$"user{i}",
                    Password = "123",
                    CreatedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1),DateTime.Now),
                    ModifiedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                    ModifiedUsername = $"user{i}"
                };
                context.AdvicesUsers.Add(user);
            }

            context.SaveChanges();

            //user list for using...
            List<AdvicesUser> userlist = context.AdvicesUsers.ToList();

            //Adding fake categories
            for (int i = 0; i < 6; i++)
            {
                Category cat = new Category()
                {
                    Title="Gezi",
                    Description=FakeData.PlaceData.GetAddress(),
                    CreatedOn=DateTime.Now,
                    ModifiedOn=DateTime.Now,
                    ModifiedUsername="hakanbahsis"
                };
                context.Categories.Add(cat);

                //Adding fake notes
                for (int k = 0; k < FakeData.NumberData.GetNumber(5,9); k++)
                {
                    AdvicesUser owner = userlist[FakeData.NumberData.GetNumber(0, userlist.Count - 1)];

                    Note note = new Note()
                    {
                        Title = FakeData.TextData.GetAlphabetical(FakeData.NumberData.GetNumber(5, 25)),
                        Text = FakeData.TextData.GetSentences(FakeData.NumberData.GetNumber(1, 3)),
                        Category=cat,
                        IsDraft=false,
                        LikeCount=FakeData.NumberData.GetNumber(1,9),
                        Owner=owner,
                        NoteImageFilename = "NoteImages/images.png",
                        CreatedOn =FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1),DateTime.Now),
                        ModifiedOn=FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1),DateTime.Now),
                        ModifiedUsername=owner.Username

                    };
                    cat.Notes.Add(note);

                    //Adding fake Comments

                    for (int j = 0; j < FakeData.NumberData.GetNumber(3,5); j++)
                    {
                        AdvicesUser comment_owner = userlist[FakeData.NumberData.GetNumber(0, userlist.Count - 1)];
                        Comment comment = new Comment()
                        {
                            Text = FakeData.TextData.GetSentence(),
                            Owner = comment_owner,
                            CreatedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                            ModifiedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                            ModifiedUsername = comment_owner.Username
                        };
                        note.Comments.Add(comment);
                    }
                    //adding fake likes
                    
                    for (int m = 0; m < note.LikeCount; m++)
                    {
                        Liked liked = new Liked()
                        {
                            LikedUser = userlist[m]
                        };

                    note.Likes.Add(liked);
                    }
                }
            }
            context.SaveChanges();
        }
    }
}
