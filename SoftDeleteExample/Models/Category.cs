using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SoftDeleteExample.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }

        #region Relations
        public virtual ICollection<Book> Books { get; set; }
        #endregion
    }
}
