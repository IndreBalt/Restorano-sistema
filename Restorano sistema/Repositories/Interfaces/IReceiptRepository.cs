namespace Restorano_sistema.Repositories.Interfaces
{
    public interface IReceiptRepository
    {
        void ReceiptAddToFile(List<string> receiptStrings);
        void ReceiptGetFromFile();
    }
}