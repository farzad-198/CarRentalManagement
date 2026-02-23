using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalManagement
{
    internal class Rental
    {
        public int RentalID { get; set; }
        public int CarID { get; set; } 
        public int CustomerID { get; set; } 
        public DateTime RentDate { get; set; } 
        public DateTime ReturnDate { get; set; } 
        public decimal TotalCost { get; set; } 
        public string Status { get; set; } 
    }
}
