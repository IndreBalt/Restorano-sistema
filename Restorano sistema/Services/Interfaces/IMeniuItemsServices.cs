using Restorano_sistema.Models;

namespace Restorano_sistema.Services.Interfaces
{
    public interface IMeniuItemsServices
    {
        List<MeniuItem> ChosenMeniuItemsToOrder(List<MeniuItem> meniuItems);
        List<MeniuItem> MeniuItemsForAddingToOrder(List<MeniuItem> meniuItems);
        List<MeniuItem> ShowMeniuItems(List<MeniuItem> meniuItems);
    }
}