using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiOgrenmeProjesi.Core
{
    public class Return
    {
        [Key]
        public int Id { get; set; }
        public string CarReg { get; set; }
        public string Name { get; set; }
        public DateTime RentalDate { get; set; }
        public int Delay { get; set; }
        public int Fine { get; set; }
        public int TotalFees { get; set; }
    }
}
