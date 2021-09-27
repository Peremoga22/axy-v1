using BusinessLogic.Entities;

using DataAccessLayer.EF.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Map
{
    public static class MapCategory
    {
        public static Category Map(this CategoryDto val)
        {           
            var res = new Category();           
            res.Name = val.Name;
            res.Description = val.Description;
                     
            return res;
        }
    }
}
