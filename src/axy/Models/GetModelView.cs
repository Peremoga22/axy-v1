using BusinessLogic.Entities;

using DataAccessLayer.EF.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace axy.Models
{
    public class GetModelView
    {
        public IEnumerable<Category> GetCategories { get; set; }
        public IEnumerable<Price> GetPrices { get; set; }
    }
}
