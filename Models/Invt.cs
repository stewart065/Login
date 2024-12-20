using System;
using System.Collections.Generic;

namespace login.Models
{
    public partial class Invt
    {
        public int Id { get; set; }
        public int Userid { get; set; }
        public string Typemed { get; set; } = null!;
        public string Medicinetype { get; set; } = null!;
        public string Mendname { get; set; } = null!;
        public int Price { get; set; }
        public string Medis { get; set; } = null!;
        public string Statusmed { get; set; } = null!;
        public int Stock { get; set; }
    }
}
