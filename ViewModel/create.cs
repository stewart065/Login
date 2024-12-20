using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace login.ViewModel
{
    public class csreateni{

        public int Id { get; set; }
        public string Typemed { get; set; } = null!;
        public string Mendname { get; set; } = null!;
        public int Price { get; set; }
        public string Medis { get; set; } = null!;
            
    }
}