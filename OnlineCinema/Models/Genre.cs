using System;
using System.Collections.Generic;

#nullable disable

namespace OnlineCinema.Models
{
    public partial class Genre
    {
        public Genre()
        {
            GenresAndFilms = new HashSet<GenresAndFilm>();
        }

        public int GenreId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<GenresAndFilm> GenresAndFilms { get; set; }
    }
}
