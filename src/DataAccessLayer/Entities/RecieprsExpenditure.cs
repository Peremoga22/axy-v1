using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class RecieprsExpenditure
    {
        public IEnumerable<ReceiptDto> GetReceipts { get; set; }
        public IEnumerable<ExpenditureDto> GetExpenditures { get; set; }
    }
}
