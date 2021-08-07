using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookShop.Models.ViewModels
{
    public class BookVM
    {
        public Book Book { get; set; }

        public IEnumerable<int> SelectedAuthors { get; set; }
        public IEnumerable<SelectListItem> Authors { get; set; }

        public IEnumerable<int> SelectedGenres { get; set; }
        public IEnumerable<SelectListItem> Genres { get; set; }

        public IEnumerable<SelectListItem> Publishers { get; set; }
    }
}
