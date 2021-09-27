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
        public decimal Money { get; set; }
        public DateTime CurentData { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public List<Cost> Costs { get; set; }
        public List<Income> Incomes { get; set; }
    }
}
