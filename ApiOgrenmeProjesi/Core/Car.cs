using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiOgrenmeProjesi.Core
{
    public class Car
    {
        [Key]
        public int CarId { get; set; }
        public string RegNo { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Availability { get; set; }
        public string Price { get; set; }
    }
}
