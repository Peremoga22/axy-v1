using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace axy.Models.Entities
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CurentData { get; set; }
        public bool IsIncome { get; set; }
    }
}
