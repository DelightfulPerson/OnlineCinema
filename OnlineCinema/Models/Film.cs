using System;
using System.Collections.Generic;

#nullable disable

namespace OnlineCinema.Models
{
    public partial class Film
    {
        public Film()
        {
            GenresAndFilms = new HashSet<GenresAndFilm>();
            Sessions = new HashSet<Session>();
        }

        public int FilmId { get; set; }
        public string Name { get; set; }
        public byte AgeRating { get; set; }
        public decimal Price { get; set; }
        public TimeSpan Duration { get; set; }

        public virtual ICollection<GenresAndFilm> GenresAndFilms { get; set; }
        public virtual ICollection<Session> Sessions { get; set; }
    }
}
