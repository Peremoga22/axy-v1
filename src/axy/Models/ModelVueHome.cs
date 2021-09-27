using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace axy.Models
{
    public class ModelVueHome
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CurrentData { get; set; }  
        public decimal Price { get; set; }    
        public bool IsIncome { get; set; }
    }
}
