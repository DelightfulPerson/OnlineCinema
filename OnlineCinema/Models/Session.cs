using System;
using System.Collections.Generic;

#nullable disable

namespace OnlineCinema.Models
{
    public partial class Session
    {
        public Session()
        {
            Tickets = new HashSet<Ticket>();
        }

        public int SessionId { get; set; }
        public int HallId { get; set; }
        public int FilmId { get; set; }
        public DateTime TimeStart { get; set; }

        public virtual Film Film { get; set; }
        public virtual Hall Hall { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
