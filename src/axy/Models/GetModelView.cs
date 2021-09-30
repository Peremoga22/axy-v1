

using DataAccessLayer.EF.Models;
using DataAccessLayer.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace axy.Models
{
    public class GetModelView
    {
        public IEnumerable<CategoryDto> GetCategories { get; set; }
        public IEnumerable<ReceiptDto> GetReceipt { get; set; }
        public IEnumerable<ExpenditureDto> GetExpenditure { get; set; }

        public int ReceiptId { get; set; }
        public string Name { get; set; }
    }
}
