

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
    }
}
