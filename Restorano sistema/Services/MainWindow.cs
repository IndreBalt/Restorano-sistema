using Restorano_sistema.Models;
using Restorano_sistema.Repositories;
using Restorano_sistema.Repositories.Interfaces;
using Restorano_sistema.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restorano_sistema.Services
{
    public class MainWindow
    {
        private readonly ITableRepository _tableRepository;
        private readonly IMeniuItemsRpository<DrinkItem> _drinkItemsRpository;
        private readonly IMeniuItemsRpository<FoodItem> _foodItemsRpository;
        private readonly ITablesServices _tablesServices;
        private readonly IReceiptServices _receiptServices;
        private readonly IMeniuItemsServices _meniuItemsServices;
        public MainWindow(ITableRepository tableRepository, IMeniuItemsRpository<DrinkItem> drinkItemsRpository, IMeniuItemsRpository<FoodItem> foodItemsRpository, ITablesServices tablesServices, IReceiptServices receiptServices, IMeniuItemsServices meniuItemsServices)
        {
            _tableRepository = tableRepository;
            _drinkItemsRpository = drinkItemsRpository;
            _foodItemsRpository = foodItemsRpository;
            _tablesServices = tablesServices;
            _receiptServices = receiptServices;
            _meniuItemsServices = meniuItemsServices;
        }
        public void Run()
        {
            while (true)
            {
                var tables = _tableRepository.GetAll();
                _tablesServices.ShowTablesList();
                int id = _tablesServices.ChooseTable();
                bool isTrue = true;
                do
                {
                    Table table = _tablesServices.ShowTableInformation(id);
                    isTrue = TableOperationChooseMeniuHandle(table);
                }while (isTrue);            
            }
        }
        public bool TableOperationChooseMeniuSwitch(int chooseTableOpertaion, Table table) //staliuko meniu pasirnkimai ka su juo daryti
        {
            bool istrue = true;
            switch (chooseTableOpertaion)
            {
                case 1:
                    Console.WriteLine("Papildyti uzsakyma");
                    DrinksOrFoodMeniuChooseSwitch(table);
                    break;
                case 2:
                    Console.WriteLine("Spausdinti ceki");
                    _receiptServices.PrintReceiptChoose(table);
                    break;
                case 3:
                    Console.WriteLine("Atsaukti uzsakyma");
                    _receiptServices.CanseledReceiptAndOrder(table);
                    
                    break;
                case 4:
                    Console.WriteLine("Baigti");
                    istrue = false;
                    break;
            }
            return istrue;
        }
        public bool TableOperationChooseMeniuHandle(Table table) //parenka meniu pgal tai ar stalikas laisvas ar rezervuotas
        {
            bool isTrue = true;
            PrintTableOperationChooseMeniu(table);
            int.TryParse(Console.ReadLine(), out int chooseTableOpertaion);
            if (table.Order is not null)
            {
                if (chooseTableOpertaion >= 1 && chooseTableOpertaion <= 4)
                {
                    isTrue = TableOperationChooseMeniuSwitch(chooseTableOpertaion, table);
                }
                else
                {
                    Console.WriteLine("Netinkamas pasirinkimas bandykit dar karta");
                }
            }
            else if (table.Order is null)
            {
                if (chooseTableOpertaion >= 1 && chooseTableOpertaion <= 2)
                {
                    if (chooseTableOpertaion == 2)
                    {
                        chooseTableOpertaion = 4;
                    }
                    isTrue = TableOperationChooseMeniuSwitch(chooseTableOpertaion, table);
                }
                else
                {
                    Console.WriteLine("Netinkamas pasirinkimas bandykit dar karta");
                }
            }
            return isTrue;

        }
        public void PrintTableOperationChooseMeniu(Table table) // atsapausdina staliuko meniu pasirinkimus
        {
            Console.WriteLine("Pasirinkite ka norite daryti?");
            if (table.Order is not null)
            {
                Console.WriteLine("1. Papildyti uzsakyma");
                Console.WriteLine("2. Spausdinti ceki ir bagti rezervacija");
                Console.WriteLine("3. Atsaukti uzsakyma");
                Console.WriteLine("4. Iseiti");
            }
            else
            {
                Console.WriteLine("1. Pradeti rezervacija");
                Console.WriteLine("2. Iseiti");
            }
        }
        public void IfTableHasNoReservation(Table table)//padaro staliuko oredri ir data ne null
        {
            if (table.Order is null)
            {
                table.Order = new Order();
                table.ReservationDate = DateTime.Now;
            }
        }
     
        public List<MeniuItem> DrinksOrFoodMeniuChooseSwitch(Table table)// pasirrenkami gerimai ir maistas, grazinamas uzsakymo sarasas ir pridedamas prie staliuko orderio
        {
            List<MeniuItem> chosenMeniuItems = new List<MeniuItem>();
            bool isTrue = true;
            while (isTrue)
            {
                Console.Clear();
                _tablesServices.TablesMainInfoLine(table);
                Console.WriteLine("Pasirinkite patiekalus ar gėrimus: ");
                Console.WriteLine("1. Patiekalai");
                Console.WriteLine("2. Gerimai");
                Console.WriteLine("3. Iseiti");
                Console.WriteLine();

                int.TryParse(Console.ReadLine(), out int meniuChoose);
                if (meniuChoose < 1 || meniuChoose > 3)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Netinkamas pasirinkimas, bandykite dar karta");
                    Console.ResetColor();
                }
                else
                {
                    switch (meniuChoose)
                    {
                        case 1:
                            Console.Clear();
                            Console.WriteLine("Patiekalai");
                            List<MeniuItem> foodItems = _foodItemsRpository.GetFromFile().Cast<MeniuItem>().ToList();
                            List<MeniuItem> chosenFoodItems = _meniuItemsServices.ChosenMeniuItemsToOrder(foodItems);
                            IfTableHasNoReservation(table);
                            chosenMeniuItems.AddRange(chosenFoodItems);
                            break;
                        case 2:
                            Console.Clear();
                            Console.WriteLine("Gerimai");
                            List<MeniuItem> drinksItems = _drinkItemsRpository.GetFromFile().Cast<MeniuItem>().ToList();
                            List<MeniuItem> chosenDrinkItems = _meniuItemsServices.ChosenMeniuItemsToOrder(drinksItems);
                            IfTableHasNoReservation(table);
                            chosenMeniuItems.AddRange(chosenDrinkItems);//Prideda pasirinkt prie staliuko uzsakymo
                            break;
                        case 3:
                            Console.WriteLine("Iseiti");
                            isTrue = false;
                            break;
                    }
                }
            }
            table.Order.MeniuItems.AddRange(chosenMeniuItems);
            return chosenMeniuItems;
        }


    }

   
}
