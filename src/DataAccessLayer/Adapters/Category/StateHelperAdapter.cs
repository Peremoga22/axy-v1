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
    public static  class StateHelperAdapter
    {

        public static StateSelectHelperDto GetState()
        {
            var result = new StateSelectHelperDto();

            string sql = null;
            sql = string.Format(@"exec [sp_GetStateSelectHelper]");
            var sqlResult = DataBaseHelper.GetSqlResult(sql);

            if (sqlResult.Rows.Count > 0)
            {               
                foreach (DataRow item in sqlResult.Rows)
                {
                    result.Id = DataBaseHelper.GetIntegerValueFromRowByName(item, "Id");
                    result.IsState = DataBaseHelper.GetBoolValueFromRowByName(item, "IsState");
                }
            }
                      
            return result;
        }
        public static void Save(StateSelectHelperDto model)
        {
            if (model.Id > 0)
            {
                var sql = string.Format(@"EXEC [sp_StateSelectHelper] {0}, {1}",
                DataBaseHelper.RawSafeSqlString(model.Id),
                DataBaseHelper.SafeSqlString(model.IsState));              
                var dataResult = DataBaseHelper.RunSql(sql);
            }
            else
            {
                var sql = string.Format(@"EXEC [sp_StateSelectHelper] {0}, {1}",
                DataBaseHelper.RawSafeSqlString(model.Id),
                DataBaseHelper.SafeSqlString(model.IsState));               
                DataBaseHelper.RunSql(sql);
            }
        }
    }
}
