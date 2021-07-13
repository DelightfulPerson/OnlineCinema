using System;
using System.Collections.Generic;

#nullable disable

namespace OnlineCinema.Models
{
    public partial class GenresAndFilm
    {
        public int FilmsId { get; set; }
        public int GenresId { get; set; }

        public virtual Film Films { get; set; }
        public virtual Genre Genres { get; set; }
    }
}
