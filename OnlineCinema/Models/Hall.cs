using System;
using System.Collections.Generic;

#nullable disable

namespace OnlineCinema.Models
{
    public partial class Hall
    {
        public Hall()
        {
            Places = new HashSet<Place>();
            Sessions = new HashSet<Session>();
        }

        public int HallId { get; set; }
        public string Name { get; set; }
        public bool _3dStatus { get; set; }
        public bool ImaxStatus { get; set; }

        public virtual ICollection<Place> Places { get; set; }
        public virtual ICollection<Session> Sessions { get; set; }
    }
}
