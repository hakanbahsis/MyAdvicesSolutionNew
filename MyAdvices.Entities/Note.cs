using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAdvices.Entities
{
    [Table("Notes")]
    public class Note : MyEntityBase
    {
        [DisplayName("Not Başlığı"),Required,StringLength(60)]
        public string Title { get; set; }

        [DisplayName("İçerik"),Required,StringLength(1000)]
        public string Text { get; set; }
        [DisplayName("Taslak")]
        public bool IsDraft { get; set; } //Taslak mı
        [DisplayName("Beğenilme")]
        public int LikeCount { get; set; }

        [StringLength(30)] //  
        public string NoteImageFilename { get; set; }
        public int CategoryId { get; set; }

        public virtual AdvicesUser Owner { get; set; }
        public virtual List<Comment> Comments { get; set; }
        public virtual Category Category { get; set; }

        public virtual List<Liked> Likes { get; set; }

        public Note()
        {
            Comments = new List<Comment>();
            Likes = new List<Liked>();
        }

    }

}
