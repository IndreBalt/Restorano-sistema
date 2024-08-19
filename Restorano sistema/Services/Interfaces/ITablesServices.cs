using Restorano_sistema.Models;

namespace Restorano_sistema.Services.Interfaces
{
    public interface ITablesServices
    {
        int ChooseTable();
        Table ShowTableInformation(int id);
        void ShowTablesList();
        void TablesMainInfoLine(Table table);
    }
}