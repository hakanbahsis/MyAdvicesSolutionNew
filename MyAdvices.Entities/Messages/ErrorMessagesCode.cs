﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAdvices.Entities.Messages
{
   public enum ErrorMessagesCode
    {
        UsernameAlreadyExists=101,
        EmailAlreadyExists=102,
        UserIsNotActive=151,
        UsernameOrPassWrong=152,
        CheckYourEmail=153,
        UserAllreadyActive=154,
        ActivateIdDoesNotExist=155,
        UserNotFound=156,
        ProfileCouldNotUpdated = 157,
        UserCouldNotRemove = 158,
        UserCouldNotFind = 159,
        UserCouldNotInserted = 160,
        UserCouldNotUpdated = 161
    }
}
