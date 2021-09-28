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
        public static IEnumerable<Category> GetCategory()
        {
            var result = new List<Category>();

            string sql = null;
            sql = string.Format(@"exec [sp_GetCategory]");
            var sqlResult = DataBaseHelper.GetSqlResult(sql);

            if (sqlResult.Rows.Count > 0)
            {
                foreach (DataRow item in sqlResult.Rows)
                {
                    result.Add(new Category
                    {
                        Id = DataBaseHelper.GetIntegerValueFromRowByName(item, "Id"),
                        Name = DataBaseHelper.GetValueFromRowByName(item, "Name"),
                        Description = DataBaseHelper.GetValueFromRowByName(item, "Cost")                       
                    });
                }
            }

            return result;
        }

        public static void SaveCategory(CategoryDto model)
        {                       
             var sql = string.Format(@"EXEC [sp_SaveCategory] {0}, {1},{2}",
             DataBaseHelper.RawSafeSqlString(model.Id),
             DataBaseHelper.RawSafeSqlString(model.Name),
             DataBaseHelper.RawSafeSqlString(model.Description));
           
            var sqlResult = DataBaseHelper.GetSqlResult(sql);                                        
        }
    }
}
