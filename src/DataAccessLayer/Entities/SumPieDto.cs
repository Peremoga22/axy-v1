using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class SumPieDto
    {
        public int CategoryId { get; set; }
        public string NameCategory { get; set; }

        public decimal SumReceipt { get; set; }
        public decimal SumExpenditure { get; set; }
        public string SumPercentage => (this.SumExpenditure * 100) / this.SumReceipt + "%";
    }
}
