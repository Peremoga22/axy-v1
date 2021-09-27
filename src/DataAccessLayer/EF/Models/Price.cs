using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EF.Models
{
    public class Price
    {
        public int Id { get; set; }    
        public DateTime CurentData { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public decimal Cost { get; set; }
        public decimal Income { get; set; }
        public bool IsIncome { get; set; }
    }
}
