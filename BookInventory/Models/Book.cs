
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookInventory.Models
{
    public class Book
    {
        public int BookId { get; set; }

        [Required(ErrorMessage = "Please enter an Isbn number")]
        public string Isbn10 { get; set; }
        public string Isbn13 { get; set; }
        [Required(ErrorMessage = "Please enter the book title")]
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Publisher { get; set; }
        public string PublishedDate { get; set; }
        public string Description { get; set; }
        public double CostPrice { get; set; }
        public double SalesPrice { get; set; }
        public int Quantity { get; set; }
        public string CoverThumbnailPath { get; set; }

        public virtual ICollection<LocationCode> LocationCodes { get; set; }
        public virtual ICollection<Author> Authors { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
    }
}