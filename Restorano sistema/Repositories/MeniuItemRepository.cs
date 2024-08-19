using Restorano_sistema.Models;
using Restorano_sistema.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restorano_sistema.Repositories
{
    public class MeniuItemRepository<T> : IMeniuItemsRpository<T> where T : MeniuItem, new()
    {
        private readonly string _filePath;
        public MeniuItemRepository(string filePath)
        {
            _filePath = filePath;
        }
        public string AddToFile(T meniuItem)
        {            
            string meniuItemString = $"{meniuItem.Name}; {meniuItem.Price}";
            File.AppendAllText(this._filePath, meniuItemString + Environment.NewLine);
            return meniuItemString;
        }
        public List<T> GetFromFile()
        {
            List<T> meniuItemsList = new List<T>();
            List<string> MeniuItemsStrings = new List<string>();
            MeniuItemsStrings = File.ReadAllLines(_filePath).ToList();
            foreach(string line in MeniuItemsStrings)
            {
                T item = new T();
                string[] separatedLine = line.Split(';');
                item.Name = separatedLine[0].Trim();
                item.Price = decimal.Parse(separatedLine[1].Trim());
                meniuItemsList.Add(item);
            }
            return meniuItemsList;
        }
    }
}
