using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SoftDeleteExample.Models
{
    public class BookCD
    {
        [Key]
        [ForeignKey(nameof(BookId))]
        public int BookId { get; set; }

        public string Name { get; set; }

        #region Relations
        public virtual Book Book { get; set; }
        #endregion
    }
}
