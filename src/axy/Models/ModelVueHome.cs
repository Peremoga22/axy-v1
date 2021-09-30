using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace axy.Models
{
    public class ModelVueHome
    {
        public int Id { get; set; }
       
        public string Description { get; set; }
        public decimal Sum { get; set; }
        public DateTime CurrentData { get; set; }  
     
        public bool IsIncome { get; set; }
    }
}
