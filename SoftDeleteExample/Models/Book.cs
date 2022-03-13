using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SoftDeleteExample.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        public int CatgoryId { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }

        #region Relations
        public virtual ICollection<BookAuthor> BookAuthors { get; set; }

        [ForeignKey(nameof(CatgoryId))]
        public virtual Category Category { get; set; }
        public virtual BookCD BookCD { get; set; }
        #endregion

    }
}
