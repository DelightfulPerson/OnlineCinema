using System;
using System.Collections.Generic;

#nullable disable

namespace OnlineCinema.Models
{
    public partial class Ticket
    {
        public int TicketId { get; set; }
        public decimal Price { get; set; }
        public int PlacesId { get; set; }
        public int SesssionsId { get; set; }
        public int UserId { get; set; }

        public virtual Place Places { get; set; }
        public virtual Session Sesssions { get; set; }
        public virtual User User { get; set; }
    }
}
