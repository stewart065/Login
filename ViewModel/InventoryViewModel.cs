using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace login.ViewModel
{
    public class InventoryViewModel
    {
        public int Id { get; set; }
        public int Userid { get; set; }
        public string UserName { get; set; }
        public string Typemed { get; set; } = null!;
        public string Mendname { get; set; } = null!;
        public int Price { get; set; }
        public string Medis { get; set; } = null!;
        public int Stck { get; set; }
        public string Statusmed { get; set; } = null!;

        public string Medicinetyp { get; set; } = null!;
    }
}