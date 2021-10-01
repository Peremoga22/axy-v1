using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class CategoryDto
    {     
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CurrentDate { get; set; }
        public bool IsIncome { get; set; }
        public decimal Cost { get; set; }
        public decimal Income { get; set; }
        public int? ExpenditureId { get; set; }
        public int? ReceiptId { get; set; }
    }
}
