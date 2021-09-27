using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EF.Models
{
    public class Income
    {
        public int Id { get; set; }
        public decimal IncomeSum { get; set; }
        public DateTime CurrentDate { get; set; }

        public int PriceId { get; set; }
        public Price Price { get; set; }
    }
}
