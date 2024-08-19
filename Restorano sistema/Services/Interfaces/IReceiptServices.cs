using Restorano_sistema.Models;

namespace Restorano_sistema.Services.Interfaces
{
    public interface IReceiptServices
    {
        Table CanseledReceiptAndOrder(Table table);
        List<string> PrintReceiptChoose(Table table);
        void PrintReceiptForClient(Table table);
        List<string> ReceiptForCanceledORder(Table table);
        Receipt ReceiptMainInfo(Table table);
        List<string> ReceiptStringForClient(Table table);
        List<string> ReceiptStringForRestourant(Table table);
        Table ResetTableInfo(Table table);
        void SwitcReceiptFormat(Table table);
    }
}