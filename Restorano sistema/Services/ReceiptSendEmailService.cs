using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restorano_sistema.Services.Interfaces;

namespace Restorano_sistema.Services
{
    public class ReceiptSendEmailService : IReceiptSendEmailService
    {
        public void SendEmail()
        {
            Console.WriteLine("Receipt sended");
        }
    }
}
