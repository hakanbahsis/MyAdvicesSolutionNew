using MyAdvices.DataAccessLayer.EntityFramework;
using MyAdvices.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAdvices.BusinessLayer
{
   public class Test
    {
        private Repository<AdvicesUser> repo_user = new Repository<AdvicesUser>();
        private Repository<Category> repo_category = new Repository<Category>();
        private Repository<Comment> repo_comment = new Repository<Comment>();
        private Repository<Note> repo_note = new Repository<Note>();
        public Test()
        {
            
            List<Category> categories= repo_category.List();
            //List<Category> categories_filtered = repo_category.List(x => x.Id > 5);
        }

        public void InserTest()
        {
           
            int result = repo_user.Insert(new AdvicesUser()
            {
                Name = "aa",
                Surname = "bb",
                Email = "hakanbahsis@outlook.com",
                ActivateGuid = Guid.NewGuid(),
                IsActive = true,
                IsAdmin = true,
                Username = "aabb",
                Password = "111",
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now.AddMinutes(5),
                ModifiedUsername = "aabb"
            });
        }

        public void UpdateTest()
        {
            AdvicesUser user = repo_user.Find(x => x.Username == "aabb");
            if (user!=null)
            {
                user.Username = "xxx";
               int result= repo_user.Update(user);
            }
        }

        public void DeleteTest()
        {
            AdvicesUser user = repo_user.Find(x => x.Username == "xxx");
            if (user!=null)
            {
               int result= repo_user.Delete(user);
            }
        }

        public void CommentTest()
        {
            AdvicesUser user = repo_user.Find(x => x.Id == 1);
            Note note = repo_note.Find(x => x.Id == 3);

            Comment comment = new Comment()
            {
                Text = "Bu bir test'dir",
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now,
                ModifiedUsername = "hakanbahss",
                Note = note,
                Owner = user
            };
            repo_comment.Insert(comment);
        }
        
    }
}
