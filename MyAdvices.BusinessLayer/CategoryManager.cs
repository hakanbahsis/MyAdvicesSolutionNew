using MyAdvices.BusinessLayer.Abstract;
using MyAdvices.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAdvices.BusinessLayer
{
    public class CategoryManager : ManagerBase<Category>
    {
        //public override int Delete(Category category)
        //{
        //    NoteManager noteManager = new NoteManager();
        //    LikedManager likedManager = new LikedManager();
        //    CommentManager commentManager = new CommentManager();

        //    //Kategori ile ilişkili notların silinmesi - notların likeları ve commentları da silinmeli
        //    foreach (Note note in category.Notes.ToList())
        //    {
        //        //Note ile ilişkili likeların silinmesi
        //        foreach (Liked like in note.Likes.ToList())
        //        {
        //            likedManager.Delete(like);
        //        }

        //        //Note ile ilişkili Commentlerin silinmesi
        //        foreach (Comment comment in note.Comments.ToList())
        //        {
        //            commentManager.Delete(comment);
        //        }

        //        noteManager.Delete(note);
        //    }

        //    return base.Delete(category);
        //}


    }
}
