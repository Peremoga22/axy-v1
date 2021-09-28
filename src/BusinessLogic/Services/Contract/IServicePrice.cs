using BusinessLogic.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.Contract
{
    public  interface IServicePrice
    {
        public Task<List<PriceDto>> GetPrice();
    }
}
