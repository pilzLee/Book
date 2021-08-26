using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookShop.Models.ViewModels
{
    [Table("Publisher")]
    public class Publisher
    {
        [Key]
        public int Id { get; set; }
        public string PublisherName { get; set; }
        //public virtual IList<Book> Books { get; set; }
    }
}
