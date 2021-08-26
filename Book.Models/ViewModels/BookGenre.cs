using System.ComponentModel.DataAnnotations.Schema;

namespace BookShop.Models.ViewModels
{
    [Table("BookGenre")]
    public class BookGenre
    {
        public int BookId { get; set; }
        public virtual Book Book { get; set; }
        public int GenreId { get; set; }
        public virtual Genre Genre { get; set; }

        public BookGenre()
        {

        }

        public BookGenre(int bookId, int genreId)
        {
            BookId = bookId;
            GenreId = genreId;
        }
    }
}
