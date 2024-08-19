using Restorano_sistema.Models;
using Restorano_sistema.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restorano_sistema.Repositories
{
    public class ReceiptRepository : IReceiptRepository
    {
        private readonly string _filePath;
        public ReceiptRepository(string filePath)
        {
            _filePath = filePath;
        }
        public void ReceiptGetFromFile()
        {
            File.ReadAllLines(_filePath);
        }
        public void ReceiptAddToFile(List<string> receiptStrings)
        {
            File.AppendAllLines(_filePath, receiptStrings);
        }
    }
}
