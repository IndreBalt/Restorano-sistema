using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restorano_sistema.Models
{
    public class Order
    {
        public List<MeniuItem>? MeniuItems {  get; set; }
        public Order() 
        {
            MeniuItems = new List<MeniuItem>();
        }
        public decimal GetAmount()
        {
            decimal amount = MeniuItems.Sum(x => x.Price);
            return amount;
        }
    }
}
