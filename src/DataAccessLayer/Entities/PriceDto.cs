using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class PriceDto
    {
        public int Id { get; set; }
        public DateTime CurrentDate { get; set; }
        public bool IsIncome { get; set; }
        public decimal Cost { get; set; }
        public decimal Income { get; set; }
    }
}
