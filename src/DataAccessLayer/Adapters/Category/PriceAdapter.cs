using DataAccessLayer.Adapters.Helpers;
using DataAccessLayer.EF.Models;

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
        public static IEnumerable<Price> GetPrice()
        {
            var result = new List<Price>();

            var sql = string.Format(@"EXEC [sp_GetPriceList]");

            var sqlResult = DataBaseHelper.GetSqlResult(sql);

            if (sqlResult.Rows.Count > 0)
            {
                foreach (DataRow item in sqlResult.Rows)
                {
                    result.Add(new Price
                    {
                        Id = DataBaseHelper.GetIntegerValueFromRowByName(item, "Id"),
                        CurentData = DataBaseHelper.GetDateTimeValueFromRowByName(item, "CurentDate"),
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
