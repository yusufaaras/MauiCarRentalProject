using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Entity
{
    public class Return
    {
        public int Id { get; set; }
        public string CarReg { get; set; }
        public string Name { get; set; }
        public DateTime RentalDate { get; set; }
        public DateTime ReturnDate { get; set; }

        public int Delay { get; set; }
        public decimal Fine { get; set; }
        public decimal TotalFees { get; set; }
    }
}
