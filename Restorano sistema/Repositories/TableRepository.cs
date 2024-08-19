using Restorano_sistema.Models;
using Restorano_sistema.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restorano_sistema.Repositories
{
    public class TableRepository : ITableRepository
    {
        private readonly List<Table> _tableList;
        public TableRepository(List<Table> tableList)
        {
            _tableList = tableList;
        }
        public List<Table> GetAll()
        {
            return _tableList;
        }
    }
}
