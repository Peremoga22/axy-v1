using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace axy.Models.Entities
{
    public class ReceiptDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public decimal Sum { get; set; }
    }
}
