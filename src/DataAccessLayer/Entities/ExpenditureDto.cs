﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class ExpenditureDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Sum { get; set; }
        public int CategoryId { get; set; }
    }
}
