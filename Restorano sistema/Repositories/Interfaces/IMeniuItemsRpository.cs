using Restorano_sistema.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restorano_sistema.Repositories.Interfaces
{
    public interface IMeniuItemsRpository<T> where T : MeniuItem
    {        
        public string AddToFile(T meniuItem);
        public List<T> GetFromFile();
    }
}
