using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiOgrenmeProjesi.Core
{
    public class Rental
    {
        [Key]
        public int RentId { get; set; }
        public string CarReg { get; set; }
        public int CustId { get; set; }
        public string CustName { get; set; }
        public DateTime Date { get; set; }
        public DateTime ReturnDate { get; set; }
        public int RentFees { get; set; }
    }
}
