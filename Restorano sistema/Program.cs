using Restorano_sistema.Models;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Text.Json;
using Restorano_sistema.Repositories;
using Restorano_sistema.Services;
using Restorano_sistema.Repositories.Interfaces;
using System.Diagnostics;
using Restorano_sistema.Services.Interfaces;


namespace Restorano_sistema
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IMeniuItemsRpository<FoodItem> foodItemRepository = new MeniuItemRepository<FoodItem>("C:\\Code\\CodeAcademy\\Restorano sistema\\Restorano sistema\\Files\\Dishes.csv");
            IMeniuItemsRpository<DrinkItem> drinksItemRepository = new MeniuItemRepository<DrinkItem>("C:\\Code\\CodeAcademy\\Restorano sistema\\Restorano sistema\\Files\\Drinks.csv");
            IReceiptRepository receiptRepository = new ReceiptRepository("C:\\Code\\CodeAcademy\\Restorano sistema\\Restorano sistema\\Files\\RestourantReceipts.csv");
            IMeniuItemsServices meniuItemsServices = new MeniuItemsServices();
            IReceiptServices receiptServices = new ReceiptServices(receiptRepository);

            //Staliukai
            Order order = new Order();
            order.MeniuItems = new List<MeniuItem>()
                {
                    new FoodItem { Name = "Pica", Price = 12},
                    new DrinkItem {  Name = "sultys", Price = 4}
                };
            List<Table> tables = new List<Table>()
                {
                    new Table { Id = 1, Seats = 5 },
                    new Table { Id = 2, Seats = 2 },
                    new Table { Id = 3, Seats = 7, ReservationDate = DateTime.Now,
                    Order = order

                    },
                };
            ITableRepository tableRepo = new TableRepository(tables);
            ITablesServices tableServ = new TablesServices(tableRepo, meniuItemsServices);
            MainWindow pagrindinis = new(tableRepo, drinksItemRepository, foodItemRepository, tableServ, receiptServices, meniuItemsServices);
            pagrindinis.Run();


        }

    }
}
