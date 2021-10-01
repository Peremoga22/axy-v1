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
    public static class CategoryAdapter
    {
        public static IEnumerable<CategoryDto> GetCategory()
        {
            var result = new List<CategoryDto>();

            string sql = null;
            //sql = string.Format(@"exec [sp_GetCategory]");
            //var sqlResult = DataBaseHelper.GetSqlResult(sql);

            //if (sqlResult.Rows.Count > 0)
            //{
            //    foreach (DataRow item in sqlResult.Rows)
            //    {
            //        result.Add(new CategoryDto
            //        {
            //            Id = DataBaseHelper.GetIntegerValueFromRowByName(item, "Id"),
            //            Name = DataBaseHelper.GetValueFromRowByName(item, "Name"),
            //            Description = DataBaseHelper.GetValueFromRowByName(item, "Cost"),
            //            CurrentDate = DataBaseHelper.GetValueFromRowByName(item, "CurentDate"),
            //            Cost = DataBaseHelper.GetDecimalValueFromRowByName(item, "Cost"),
            //            Income = DataBaseHelper.GetDecimalValueFromRowByName(item, "Income"),
            //            IsIncome = DataBaseHelper.GetBoolValueFromRowByName(item, "IsIncome")
            //        });
            //    }
            //}

            return result;
        }

        public static void SaveCategory(CategoryDto model)
        {
             model.Id = 0;
            if(model.ReceiptId == 0)
            {
                model.ReceiptId = null;
            }
            
            if(model.ExpenditureId == 0)
            {
                model.ExpenditureId = null;
            }

             var sql = string.Format(@"EXEC [sp_SaveCategory] {0}, {1}, {2}, {3},{4},{5},{6}",
             DataBaseHelper.RawSafeSqlString(model.Id),
             DataBaseHelper.SafeSqlString(model.Name),
             DataBaseHelper.SafeSqlString(model.Description),            
             DataBaseHelper.SafeSqlString(model.CurrentDate.ToString()),            
             DataBaseHelper.RawSafeSqlString(model.IsIncome),
             DataBaseHelper.RawSafeSqlString(model.ExpenditureId),
             DataBaseHelper.RawSafeSqlString(model.ReceiptId));           
      
            var dataResult = DataBaseHelper.RunSql(sql);
           
        }
    }
}
