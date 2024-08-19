using Restorano_sistema.Models;
using Restorano_sistema.Repositories.Interfaces;
using Restorano_sistema.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restorano_sistema.Services
{
    public class TablesServices : ITablesServices
    {
        private readonly ITableRepository _tableRepository;
        private readonly IMeniuItemsServices _meniuItemsServices;

        public TablesServices(ITableRepository tableRepository, IMeniuItemsServices meniuItemsServices)
        {
            _tableRepository = tableRepository;
            _meniuItemsServices = meniuItemsServices;
        }
        public void ShowTablesList()//parodo staliuku sarasa
        {
            Console.Clear();
            var tables = _tableRepository.GetAll();
            tables.OrderBy(x => x.Id);
            Console.WriteLine("Tables list: ");
            foreach (var table in tables)
            {
                if (table.ReservationDate != default)
                {
                    Console.WriteLine($"{table.Id}. Seats: {table.Seats}, reserved");
                }
                else
                {
                    Console.WriteLine($"{table.Id}. Seats: {table.Seats}");
                }
            }
        }
        public int ChooseTable()//staliuko pasirinikimas, grazina staliuko numeri is saraso
        {
            Console.WriteLine("Choose table:");
            int choose = 0;
            var tables = _tableRepository.GetAll();
            while (choose < 1 || choose > tables.Count())
            {
                int.TryParse(Console.ReadLine(), out choose);
                if (choose < 1 || choose > tables.Count())
                {
                    Console.WriteLine("Tokio staliuko nera, bandykit dar karą");
                }
            }
            return choose;
        }
        public Table ShowTableInformation(int id)//rodo staliuko info (jei yra uzsakymas, rodo patiekalus ir suma)
        {
            var tables = _tableRepository.GetAll();
            Console.Clear();
            var table = tables.Find(x => x.Id.Equals(id));
            TablesMainInfoLine(table);
            if (table.Order is not null && table.Order.MeniuItems.Count > 0)
            {
                Console.WriteLine("Uzsakymas: ");
                _meniuItemsServices.ShowMeniuItems(table.Order.MeniuItems);
                Console.WriteLine($"Uzsakymo suma: {table.Order.GetAmount()}eur");
                Console.WriteLine();
            }
            return table;
        }
        public void TablesMainInfoLine(Table table) //staliuko antraste su info
        {
            Console.WriteLine();
            Console.WriteLine($"Table no.{table.Id}, Seats: {table.Seats} {(table.ReservationDate != default ? $"Reservation date: {table.ReservationDate} " : ' ')}");
            Console.WriteLine();
        }




    }
}
