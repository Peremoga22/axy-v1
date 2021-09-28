using DataAccessLayer.Adapters.Helpers;
using DataAccessLayer.EF.Models;
using DataAccessLayer.Entities;

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public static class PriceAdapter
    {
        public static IEnumerable<PriceDto> GetPrice()
        {
            var result = new List<PriceDto>();

            var sql = string.Format(@"EXEC [sp_GetPriceList]");

            var sqlResult = DataBaseHelper.GetSqlResult(sql);

            if (sqlResult.Rows.Count > 0)
            {
                foreach (DataRow item in sqlResult.Rows)
                {
                    result.Add(new PriceDto
                    {
                        Id = DataBaseHelper.GetIntegerValueFromRowByName(item, "Id"),
                        CurrentDate = DataBaseHelper.GetDateTimeValueFromRowByName(item, "CurentDate"),
                        Cost = DataBaseHelper.GetDecimalValueFromRowByName(item, "Cost"),
                        Income = DataBaseHelper.GetDecimalValueFromRowByName(item, "Income"),
                        IsIncome = DataBaseHelper.GetBoolValueFromRowByName(item, "IsIncome")
                    });
                }
            }
           
            return result;
        }
    }
}
