﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyAdvices.Entities.ValueObject

{
    public class LoginViewModel
    {
        [DisplayName("Kullanıcı adı") ,Required(ErrorMessage ="{0} alanı boş geçilemez."),StringLength(25,ErrorMessage ="{0} max. {1} karakter olmalı.")]
        public string Username { get; set; }

        [DisplayName("Parola"), Required(ErrorMessage = "{0} alanı boş geçilemez."),DataType(DataType.Password), StringLength(25, ErrorMessage = "{0} max. {1} karakter olmalı.")]
        public string Password { get; set; }
    }
}