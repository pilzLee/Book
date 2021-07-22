using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookShop.Models.ViewModels
{
    [Table("Book")]
    public class Book
    {
        [Key]
        public int Id { get; set; }
        public string ISBN { get; set; }
        public string Title { get; set; }
        public int Edition { get; set; }
        public decimal Price { get; set; }
        public int AvailableQuantity { get; set; }
        public string Description { get; set; }
        public Publisher Publisher { get; set; }
        public IList<Order> Orders { get; set; }
        public IList<Genre> Genres { get; set; }
        public IList<Author> Authors { get; set; }
    }
}
