using Restorano_sistema.Models;
using Restorano_sistema.Repositories;
using Restorano_sistema.Repositories.Interfaces;
using Restorano_sistema.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restorano_sistema.Services
{
    public class MeniuItemsServices : IMeniuItemsServices
    {
        public List<MeniuItem> ShowMeniuItems(List<MeniuItem> meniuItems) //rodo gerimus ir valgius
        {
            for (int i = 0; i < meniuItems.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {meniuItems[i].Name}, {meniuItems[i].Price}eur");
            }
            return meniuItems;
        }
        public List<MeniuItem> MeniuItemsForAddingToOrder(List<MeniuItem> meniuItems)//grazina meniuItems kuriuos reiks pridet i table.order
        {
            List<MeniuItem> meniuItemsToOrder = new List<MeniuItem>();
            Console.WriteLine("Pasirinkite meniu punktus");
            bool isTrue = true;
            while (isTrue)
            {
                string orderString = Console.ReadLine().ToLower();
                if (orderString != "x")
                {
                    int.TryParse(orderString, out int orderInt);
                    if (orderInt < 1 || orderInt > meniuItems.Count)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("netinkama ivestis");
                        Console.ResetColor();
                    }
                    else if (orderInt >= 1 && orderInt <= meniuItems.Count)
                    {
                        MeniuItem item = meniuItems.Find(x => meniuItems.IndexOf(x) == orderInt - 1);
                        meniuItemsToOrder.Add(item);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write($"{item.Name}");
                        Console.ResetColor();
                        Console.WriteLine();
                    }
                    Console.WriteLine("Norint iseiti spauskite X arba teskite darba");
                }
                else
                {
                    isTrue = false;
                }
            }
            return meniuItemsToOrder;
        }
        public List<MeniuItem> ChosenMeniuItemsToOrder(List<MeniuItem> meniuItems)
        {
            ShowMeniuItems(meniuItems);
            List<MeniuItem> chosenMeniuItems = new List<MeniuItem>();
            chosenMeniuItems = MeniuItemsForAddingToOrder(meniuItems);
            return chosenMeniuItems;
        }
    }
}
