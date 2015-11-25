using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookInventory.Models
{
    public class LocationCode
    {
        [Key]
        public int Id { get; set; }

        public string Code { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}