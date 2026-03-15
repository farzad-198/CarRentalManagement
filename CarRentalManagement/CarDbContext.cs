using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalManagement
{
    internal class CarDbContext : DbContext
    {
        public CarDbContext()
            : base("name=CarConnectionstring")
            { }
        public DbSet<Car> Cars { get; set; }    
    }
}
