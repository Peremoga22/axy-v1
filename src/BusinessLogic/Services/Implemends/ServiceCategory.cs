using BusinessLogic.Entities;

using DataAccessLayer;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.Implemends
{
    public class ServiceCategory : IServiceCategory
    {
        public async Task<List<CategoryDto>> GetCategory()
        {
                      
            var category =  CategoryAdapter.GetCategory();
                    
          
           

            return (List<CategoryDto>)category;
        }
    }
}
