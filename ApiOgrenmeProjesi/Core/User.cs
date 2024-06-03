using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiOgrenmeProjesi.Core
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string Uname { get; set; }
        public string Upass { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}
