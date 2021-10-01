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
    public static class ReceiptAdapter
    {
        public static IEnumerable<ReceiptDto> GetReceipt()
        {
            var result = new List<ReceiptDto>();

            string sql = null;
            sql = string.Format(@"exec [sp_GetReceipt]");
            var sqlResult = DataBaseHelper.GetSqlResult(sql);

            if (sqlResult.Rows.Count > 0)
            {
                foreach (DataRow item in sqlResult.Rows)
                {
                    result.Add(new ReceiptDto
                    {
                        Id = DataBaseHelper.GetIntegerValueFromRowByName(item, "ReceiptId"),
                        Name = DataBaseHelper.GetValueFromRowByName(item, "Name"),
                        Sum = DataBaseHelper.GetDecimalValueFromRowByName(item, "Sum")                       
                    });
                }
            }

            return result;
        }

        public static void SaveReceipt(ReceiptDto model)
        {
            var sql = string.Format(@"EXEC [sp_SaveReceipt] {0}, {1},{2}",
            DataBaseHelper.RawSafeSqlString(model.Id),
            DataBaseHelper.RawSafeSqlString(model.Name),
            DataBaseHelper.SafeSqlString(model.Sum));      
            var sqlResult = DataBaseHelper.RunSql(sql);
        }

        public static ReceiptDto GetReceiptDtoId(int contactId)
        {
            ReceiptDto result = new ReceiptDto();

            var sql = string.Format(@"EXEC [sp_GetReceiptDetailID] {0}",
               DataBaseHelper.RawSafeSqlString(contactId));
            var sqlResult = DataBaseHelper.GetSqlResult(sql);

            if (sqlResult.Rows.Count > 0)
            {
                result = new ReceiptDto
                {
                    Id = DataBaseHelper.GetIntegerValueFromRowByName(sqlResult.Rows[0], "ReceiptId"),
                    Name = DataBaseHelper.GetValueFromRowByName(sqlResult.Rows[0], "Name"),
                    Sum = DataBaseHelper.GetDecimalValueFromRowByName(sqlResult.Rows[0], "Sum")                  
                };
            }

            return result;
        }

        public static void DeleteReceipt(int id)
        {
            if (id > 0)
            {
                string sql = string.Format(@"exec sp_DeleteReceipt {0}",
                DataBaseHelper.RawSafeSqlString(id));
                DataBaseHelper.RunSql(sql);
            }
        }
    }
}
