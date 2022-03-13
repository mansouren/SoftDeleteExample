using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SoftDeleteExample.Models
{
    public class BookAuthor
    {
        [Key]
        public int Id { get; set; }
        public int BookId { get; set; }
        public int AuthorId { get; set; }

        #region Relations
        public Book Book { get; set; }
        public Author Author { get; set; }
        #endregion
    }
}
