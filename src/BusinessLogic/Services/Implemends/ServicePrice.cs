using BusinessLogic.Entities;
using BusinessLogic.Services.Contract;

using DataAccessLayer;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.Implemends
{
    public class ServicePrice : IServicePrice
    {
        public async Task<List<PriceDto>> GetPrice()
        {
            var price = PriceAdapter.GetPrice();

            return (List<PriceDto>)price;
        }
    }
}
