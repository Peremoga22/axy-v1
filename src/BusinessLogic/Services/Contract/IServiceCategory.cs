using BusinessLogic.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public interface IServiceCategory
    {
        public Task<List<CategoryDto>> GetCategory();
    }
}
