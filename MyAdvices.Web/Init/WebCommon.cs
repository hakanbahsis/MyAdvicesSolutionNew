using MyAdvices.Common;
using MyAdvices.Entities;
using MyAdvices.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyAdvices.Web.Init
{
    public class WebCommon : ICommon
    {
        public string GetCurrentUsername()
        {
            AdvicesUser user = CurrentSession.User;
            if (user !=null)
            {
                return user.Username;
            }
            else
            
            return "system";
        }
    }
}