using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalManagement.Classes
{
    internal class Payment
    {
        public int PaymentID { get; set; }
        public string CustomerName { get; set; }
        public int Amount { get; set; }
        public string PaymentMethod { get; set; }
    }
}
