using System;
using System.Collections.Generic;

#nullable disable

namespace OnlineCinema.Models
{
    public partial class AddedValue
    {
        public int AddValueId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
