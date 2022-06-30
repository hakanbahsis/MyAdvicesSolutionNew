﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAdvices.Entities
{
    [Table("AdviceUser")]
    public class AdvicesUser : MyEntityBase
    {
        [DisplayName("Ad"),
            StringLength(25,ErrorMessage ="{0} alanı maximum {1} karakter olmalıdır.")]
        public string Name { get; set; }

        [DisplayName("Soyad"), 
            StringLength(25,ErrorMessage = "{0} alanı maximum {1} karakter olmalıdır.")]
        public string Surname { get; set; }

        [DisplayName("Kullanıcı Adı"), 
            Required(ErrorMessage ="{0} alanı gereklidir."),
            StringLength(25,ErrorMessage = "{0} alanı maximum {1} karakter olmalıdır.")]
        public string Username { get; set; }

        [DisplayName("E-Posta"), 
            Required(ErrorMessage = "{0} alanı gereklidir."), 
            StringLength(30, ErrorMessage = "{0} alanı maximum {1} karakter olmalıdır.")]
        public string Email { get; set; }

        [DisplayName("Parola"), 
            Required(ErrorMessage = "{0} alanı gereklidir."), 
            StringLength(25, ErrorMessage = "{0} alanı maximum {1} karakter olmalıdır.")]
        public string Password { get; set; }

        [StringLength(30),ScaffoldColumn(false)] //  images/user_12.jpg
        public string ProfileImageFilename { get; set; }
        [DisplayName("Is Active")]
        public bool IsActive { get; set; }
        [DisplayName("Is Admin")]
        public bool IsAdmin { get; set; }

        [Required,ScaffoldColumn(false)]
        public Guid ActivateGuid { get; set; }
        

        public virtual List<Note> Notes { get; set; }
        public virtual List<Comment> Comments { get; set; }
        public virtual List<Liked> Likes { get; set; }
    }

}
