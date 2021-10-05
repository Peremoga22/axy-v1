using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace axy.Models
{
    public class ModelVueHome
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DescriptionCategory { get; set; }
        public decimal Sum { get; set; }
        public DateTime CurrentDate { get; set; }  
     
        public bool IsIncome { get; set; }
    }
}
