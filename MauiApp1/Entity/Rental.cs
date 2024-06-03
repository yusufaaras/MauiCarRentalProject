using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Entity
{
    public class Rental
    {
        public int RentId { get; set; }
        public string CarReg { get; set; }
        public string CustName { get; set; }
        public int CustId { get; set; }// Diğer özellikler

        public DateTime ReturnDate { get; set; }
        public DateTime Date { get; set; }

        public string FormattedDate => Date.ToString("yyyy-MM-dd");
        public decimal RentFees { get; set; }
    }
}
