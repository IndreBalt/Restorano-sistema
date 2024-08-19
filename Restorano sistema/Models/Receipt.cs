using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restorano_sistema.Models
{
    public class Receipt : Order
    {
        public DateTime PrintDate { get; set; }
        public int? TableNumber { get; set; }
        public decimal GetTaxes()
        {            
            decimal taxes = GetAmount() * 0.21m;
            return taxes;
        }
    }
}
