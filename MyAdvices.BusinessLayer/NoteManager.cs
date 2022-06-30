using MyAdvices.BusinessLayer.Abstract;
using MyAdvices.DataAccessLayer.EntityFramework;
using MyAdvices.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAdvices.BusinessLayer
{
    public class NoteManager:ManagerBase<Note>
    {
       
        private Repository<Note> repo_note = new Repository<Note>();
        public List<Note> GetAllNote()
        {
            return repo_note.List();
        }

      
    }
}
