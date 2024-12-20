using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace login.ViewModel
{
  
    public class UserInventoryViewModel
{
    public string UserId { get; set; }
    public string UserName { get; set; }
    public List<InventoryViewModel> Items { get; set; }
}


        public class ItemsofuserViewModal
{
    public int Id { get; set; }
    public string Typemed { get; set; }
    public string Mendname { get; set; }
    public decimal Price { get; set; }
    public string Medis { get; set; }
    public int Stock { get; set; }
    public string Statusmed { get; set; }
    public string Medicinetyp { get; set; }
}
    
}