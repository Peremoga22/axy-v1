using BusinessLogic.Entities;

using DataAccessLayer.EF.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Map
{
    public static class MapPrice
    {
        public static Price Map(this PriceDto val)
        {
            var res = new Price();            
            res.CurentData = val.CurrentDate;
            res.Cost = val.Cost;
            res.Income = val.Income;
            res.IsIncome = val.IsIncome;

            return res;
        }
    }
}
