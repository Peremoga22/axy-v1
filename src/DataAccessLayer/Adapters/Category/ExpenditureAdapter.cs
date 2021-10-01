using DataAccessLayer.Adapters.Helpers;
using DataAccessLayer.Entities;

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Adapters.Category
{
    public static class ExpenditureAdapter
    {

        public static IEnumerable<ExpenditureDto> GetExpenditure()
        {
            var result = new List<ExpenditureDto>();

            string sql = null;
            sql = string.Format(@"exec [sp_GetExpenditure]");
            var sqlResult = DataBaseHelper.GetSqlResult(sql);

            if (sqlResult.Rows.Count > 0)
            {
                foreach (DataRow item in sqlResult.Rows)
                {
                    result.Add(new ExpenditureDto
                    {
                        Id = DataBaseHelper.GetIntegerValueFromRowByName(item, "ExpenditureId"),
                        Name = DataBaseHelper.GetValueFromRowByName(item, "Name"),
                        Sum = DataBaseHelper.GetDecimalValueFromRowByName(item, "Sum")
                    });
                }
            }

            return result;
        }

        public static ExpenditureDto GetExpenditureDtoId(int contactId)
        {
            ExpenditureDto result = new ExpenditureDto();

            var sql = string.Format(@"EXEC [sp_GetExpenditureDetailID] {0}",
               DataBaseHelper.RawSafeSqlString(contactId));
            var sqlResult = DataBaseHelper.GetSqlResult(sql);

            if (sqlResult.Rows.Count > 0)
            {
                result = new ExpenditureDto
                {
                    Id = DataBaseHelper.GetIntegerValueFromRowByName(sqlResult.Rows[0], "ExpenditureId"),
                    Name = DataBaseHelper.GetValueFromRowByName(sqlResult.Rows[0], "Name"),
                    Sum = DataBaseHelper.GetDecimalValueFromRowByName(sqlResult.Rows[0], "Sum")
                };
            }

            return result;
        }

        public static void SaveExpenditure(ExpenditureDto model)
        {          
            var sql = string.Format(@"EXEC [sp_SaveExpenditure] {0}, {1},{2}",
            DataBaseHelper.RawSafeSqlString(model.Id),
            DataBaseHelper.SafeSqlString(model.Name),
            DataBaseHelper.RawSafeSglDecimal(model.Sum));           
            var sqlResult = DataBaseHelper.RunSql(sql);
        }

        public static void DeleteExpenditure(int id)
        {
            if (id > 0)
            {
                string sql = string.Format(@"exec sp_DeleteExpenditure {0}",
                DataBaseHelper.RawSafeSqlString(id));
                DataBaseHelper.RunSql(sql);
            }
        }
    }
}
