﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using DataAccessLayer.Entities;

namespace axy.Models.Entities
{
    public class RecieprsExpenditure
    {
        public IEnumerable<ReceiptDto> GetReceipts { get; set; }
        public IEnumerable<ExpenditureDto> GetExpenditures { get; set; }
    }
}
