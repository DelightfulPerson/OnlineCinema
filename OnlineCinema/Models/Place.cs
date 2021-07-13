using System;
using System.Collections.Generic;

#nullable disable

namespace OnlineCinema.Models
{
    public partial class Place
    {
        public Place()
        {
            Tickets = new HashSet<Ticket>();
        }

        public int PlaceId { get; set; }
        public short Row { get; set; }
        public short Place1 { get; set; }
        public int HallId { get; set; }
        public short? Offset { get; set; }

        public virtual Hall Hall { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
