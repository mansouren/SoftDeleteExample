using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SoftDeleteExample.Models
{
    public class Author
    {
        [Key]
        public int Id { get; set; }
        public string FullName { get; set; }

        #region Relations
        public virtual ICollection<BookAuthor> BookAuthors { get; set; }
        #endregion
    }
}
