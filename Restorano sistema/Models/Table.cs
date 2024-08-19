using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restorano_sistema.Models
{
    public class Table
    {
        public int? Id { get; set; }
        public int Seats { get; set; }
        public DateTime ReservationDate { get; set; }     
        public Order Order { get; set; }
    }
}
