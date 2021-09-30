﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EF.Models
{
    public class Receipt
    {
        [Key]
        public int ReceiptId { get; set; }
        public string Name { get; set; }

        public decimal Sum { get; set; }
            
        
    }
}
