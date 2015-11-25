using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookInventory.Models.ViewModels
{
    public class BookDataViewModel
    {
        public Book Book { get; set; }
        public string AuthorsText { get; set; }
        public string CategoriesText { get; set; }
        public IEnumerable<LocationCode> LocationCodes { get; set; }
    }
}