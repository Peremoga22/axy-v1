using DataAccessLayer.Adapters.Helpers;
using DataAccessLayer.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Adapters.Category
{
    public static class ReceiptAdapter
    {
        public static void SaveReceipt(ReceiptDto model)
        {
            var sql = string.Format(@"EXEC [sp_SaveReceipt] {0}, {1}",
            DataBaseHelper.SafeSqlString(model.Name),
            DataBaseHelper.SafeSqlString(model.Sum));      
            var sqlResult = DataBaseHelper.GetSqlResult(sql);
        }
    }
}
