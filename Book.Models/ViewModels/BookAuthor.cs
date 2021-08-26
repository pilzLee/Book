using System.ComponentModel.DataAnnotations.Schema;

namespace BookShop.Models.ViewModels
{
    [Table("BookAuthor")]
    public class BookAuthor
    {
        public int BookId { get; set; }
        public virtual Book Book { get; set; }
        public int AuthorId { get; set; }
        public virtual Author Author { get; set; }

        public BookAuthor()
        {

        }

        public BookAuthor(int bookId, int authorId)
        {
            BookId = bookId;
            AuthorId = authorId;
        }
    }
}
