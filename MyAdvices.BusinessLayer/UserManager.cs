using MyAdvices.DataAccessLayer.EntityFramework;
using MyAdvices.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAdvices.BusinessLayer
{
   public class UserManager
    {
        private Repository<AdvicesUser> repo_user = new Repository<AdvicesUser>();
        public List<AdvicesUser> GetAllUser()
        {
            return repo_user.List();
        }
        public IQueryable<AdvicesUser> GetAllUserQueryable()
        {
            return repo_user.ListQuaryable();
        }
    }
}
