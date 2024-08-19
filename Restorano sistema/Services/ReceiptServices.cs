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
    public class ReceiptServices : IReceiptServices
    {
        private readonly IReceiptRepository _receiptRepository;
        public ReceiptServices(IReceiptRepository receiptRepository)
        {
            _receiptRepository = receiptRepository;
        }
        public Receipt ReceiptMainInfo(Table table) // sukurimas Receipt objektas
        {
            Receipt receipt = new Receipt();
            receipt.TableNumber = table.Id;
            receipt.MeniuItems = table.Order.MeniuItems;
            receipt.PrintDate = DateTime.Now;
            return receipt;
        }
        public List<string> ReceiptStringForClient(Table table) //suformuojamas tekstas kliento cekiui
        {
            List<string> clientreceiptstring = new List<string>();
            List<string> restourantReceipt = ReceiptStringForRestourant(table);
            string restourantTitle = "------RESTORANAS------";
            string restourantAddress = "Restoranog. 2, Kavinenai";
            clientreceiptstring.Add(restourantTitle);
            clientreceiptstring.Add(restourantAddress);
            clientreceiptstring.AddRange(restourantReceipt);
            return clientreceiptstring;
        }
        public List<string> ReceiptStringForRestourant(Table table) //suformuojamas tekstas restorano cekiui 
        {
            Receipt receipt = new Receipt();
            receipt = ReceiptMainInfo(table);
            List<string> receiptStringList = new List<string>();
            string starsLine = "************************************";
            string line = "-----------------";
            string tableNumber = $"Staliukas nr.{receipt.TableNumber}";
            receiptStringList.Add(starsLine);
            receiptStringList.Add(tableNumber);
            receiptStringList.Add(line);
            foreach (var item in receipt.MeniuItems)
            {
                receiptStringList.Add($"{item.Name}, {item.Price} eur");
            }
            receiptStringList.Add(line);
            receiptStringList.Add($"Suma: {receipt.GetAmount()} eur");
            string getTaxes = String.Format("{0:0.00}", receipt.GetTaxes());
            receiptStringList.Add($"Mokesciai: {getTaxes} eur");
            receiptStringList.Add($"Cekio data: {receipt.PrintDate}");
            return receiptStringList;
        }
        public void PrintReceiptForClient(Table table) // cekio spausdinimas klientui
        {
            List<string> clientReceiptStrings = ReceiptStringForClient(table);
            foreach (var clientReceiptString in clientReceiptStrings)
            {
                Console.WriteLine(clientReceiptString);
            } 
        }
        public List<string> PrintReceiptChoose(Table table)//ar spausdinti ceki
        {
            Console.WriteLine();
            Console.WriteLine("Ar norite spausdinti ceki ir baigti rezervacija? (y/n)");
            string printReceipt = Console.ReadLine().ToLower();
            List<string> receiptForResourant = ReceiptStringForRestourant(table);
            if (printReceipt != "y" && printReceipt != "n")
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Netinkamas pasirinkimas, bandykit dar kartą");
                Console.ResetColor();
            }
            else if (printReceipt == "y")
            {
                _receiptRepository.ReceiptAddToFile(receiptForResourant);// restorano cekis ikeliamas i faila
                Console.WriteLine("Ar Klientas pageidauja cekio?");
                string answer = Console.ReadLine();
                if (answer == "y")
                {
                    SwitcReceiptFormat(table);
                }
                ResetTableInfo(table);
            }
            return receiptForResourant;
        }
        public void SwitcReceiptFormat(Table table)//pasirenkama ar ceki siusti pastu ar sapusdinti
        {
            Console.WriteLine("Ceki spausdinti ar siusti e-pastu?");
            Console.WriteLine("1. E-pastu");
            Console.WriteLine("2. Spausdint");
            int.TryParse(Console.ReadLine(), out int chooseOfReceiptFormat);
            switch (chooseOfReceiptFormat)
            {
                case 1:
                    Console.WriteLine("pastu");
                    IReceiptSendEmailService receiptSendEmailService = new ReceiptSendEmailService();
                    receiptSendEmailService.SendEmail();//cekis isciunciamas
                    Console.ReadKey();
                    break;
                case 2:
                    Console.WriteLine();
                    Console.Clear();
                    PrintReceiptForClient(table);//cekis atspausdinamas klientui
                    Console.ReadKey();
                    break;
            }
        }
        public Table ResetTableInfo(Table table)//isvaloma staliuko info
        {
            table.Order = default;
            table.ReservationDate = default;
            return table;
        }
        public List<string> ReceiptForCanceledORder(Table table)//cekis atsauktam uzsakymui
        {
            List<string> receiptForResourant = ReceiptStringForRestourant(table);
            string canceled = "CANCELED!!!";
            receiptForResourant.Add(canceled);
            _receiptRepository.ReceiptAddToFile(receiptForResourant);// restorano cekis ikeliamas i faila
            return receiptForResourant;
        }
        public Table CanseledReceiptAndOrder(Table table)
        {
            ReceiptForCanceledORder(table);
            ResetTableInfo(table);
            Console.WriteLine("Rezervacija atsaukta");
            Console.ReadKey();
            return table;
        }


    }
}
