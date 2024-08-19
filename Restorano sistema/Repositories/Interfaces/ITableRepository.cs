using Restorano_sistema.Models;

namespace Restorano_sistema.Repositories.Interfaces
{
    public interface ITableRepository
    {
        List<Table> GetAll();
    }
}