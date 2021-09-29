using DataAccessLayer.Adapters.ExtensionModels;

using Microsoft.Data.SqlClient;

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Adapters.Helpers
{
    public class DataBaseHelper
    {
        internal static string DBConnection
        {
            get { return ConnectionString.Value; }
        }

        internal static DataTable GetSqlResult(string sql, bool isLowTimeout = false, int timeout = 0)
        {
            var connection = DBConnection;

            using (SqlConnection sc = new SqlConnection(connection))
            {             

                try
                {
                    sc.Open();
                    using (SqlCommand com = new SqlCommand(sql, sc))
                    {
                        if (isLowTimeout)
                        {
                            com.CommandTimeout = 3;
                        }

                        if (timeout > 0)
                        {
                            com.CommandTimeout = timeout;
                        }

                        DataSet ds = new DataSet();
                        SqlDataAdapter sda = new SqlDataAdapter(com);
                        sda.Fill(ds);
                        return ds.Tables[0];
                    }
                }
                catch (Exception ex)
                {
                   
                    if (isLowTimeout)
                    {
                        return new DataTable();
                    }

                    throw ex;
                }
            }
        }

        internal static DataTableCollection GetSqlResultCollection(string sql, bool isLowTimeout = false, int timeout = 0)
        {
            var connection = DBConnection;

            using (SqlConnection sc = new SqlConnection(connection))
            {               
                try
                {
                    sc.Open();
                    using (SqlCommand com = new SqlCommand(sql, sc))
                    {
                        if (isLowTimeout)
                        {
                            com.CommandTimeout = 3;
                        }

                        if (timeout > 0)
                        {
                            com.CommandTimeout = timeout;
                        }
                        DataSet ds = new DataSet();
                        SqlDataAdapter sda = new SqlDataAdapter(com);
                        sda.Fill(ds);
                        sc.Close();
                        return ds.Tables;
                    }
                }
                catch (Exception ex)
                {                    
                    throw ex;
                }
            }
        }

        internal static int RunSql(string sql, int timeout = 0)
        {
            using (SqlConnection sc = new SqlConnection(DBConnection))
            {               
                try
                {
                    sc.Open();
                    using (SqlCommand com = new SqlCommand(sql, sc))
                    {
                        if (timeout > 0)
                        {
                            com.CommandTimeout = timeout;
                        }

                        return com.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {                    
                    throw ex;
                }
            }
        }

        internal static string GetConfigValue(string name)
        {
            var result = string.Empty;

            return result;
        }

        internal static bool StringToBool(string data)
        {
            if (!string.IsNullOrEmpty(data) && (
                data.ToLower().Equals("y") || data.ToLower().Equals("1")))
            {
                return true;
            }

            return false;
        }

        internal static string SafeSqlString(object s, bool nullable = false)
        {
            if (s == null ||
                (nullable && string.IsNullOrEmpty(s.ToString())))
            {
                return "null";
            }

            var result = s.ToString().Replace("'", "''");

            result = "'" + result + "'";

            return result;
        }

        internal static string RawSafeSqlString(object s)
        {
            if (s == null || string.IsNullOrEmpty(s.ToString()))
            {
                return "null";
            }

            var result = s.ToString().Replace("'", "''");

            return result;
        }

        internal static string RawSafeSglDecimal(object s)
        {
            var res = s.ToString().Replace(",", ".");
            return res;
        }

        #region RowsDataRegion
        internal static string GetFirstRowValueFromDataTableByName(DataTable dataTable, string name)
        {
            try
            {
                return GetValueFromRowByName(dataTable.Rows[0], name);
            }
            catch (Exception) { }

            return string.Empty;
        }

        internal static string GetValueFromRowByName(DataRow row, string name)
        {
            try
            {
                return string.IsNullOrEmpty(row[name]?.ToString()) ? string.Empty : row[name].ToString();
            }
            catch (Exception) { }

            return string.Empty;
        }

        internal static int GetIntegerValueFromRowByName(DataRow row, string name)
        {
            var result = 0;
            var value = GetValueFromRowByName(row, name);

            try
            {
                result = int.Parse(value);
            }
            catch (Exception) { }

            return result;
        }

        internal static decimal GetDecimalValueFromRowByName(DataRow row, string name)
        {
            decimal result = 0;
            var value = GetValueFromRowByName(row, name);

            try
            {
                result = decimal.Parse(value);
            }
            catch (Exception) { }

            return result;
        }

        internal static bool GetBoolValueFromRowByName(DataRow row, string name)
        {
            var result = false;
            var value = GetValueFromRowByName(row, name);

            try
            {
                result = bool.Parse(value);
            }
            catch (Exception) { }

            return result;
        }

        internal static bool? GetBoolNullableValueFromRowByName(DataRow row, string name)
        {
            bool? result = null;
            string value = GetValueFromRowByName(row, name);

            try
            {
                result = bool.Parse(value);
            }
            catch (Exception) { }

            return result;
        }

        internal static DateTime GetDateTimeValueFromRowByName(DataRow row, string name)
        {
            DateTime result = new DateTime();
            var value = GetValueFromRowByName(row, name);

            try
            {
                result = DateTime.Parse(value);
            }
            catch (Exception) { }

            return result;
        }

        internal static DateTime? GetDateTimeNullableValueFromRowByName(DataRow row, string name)
        {
            DateTime? result = null;
            string value = GetValueFromRowByName(row, name);

            if (DateTime.TryParse(value, out DateTime _rst))
            {
                result = _rst;
            }

            return result;
        }

        internal static int ExecuteNonQuery(string querySql, SqlParameter[] parameters)
        {
            try
            {
                using (SqlConnection sc = new SqlConnection(DBConnection))
                {
                    sc.Open();

                    using (SqlCommand command = new SqlCommand(querySql, sc))
                    {
                        command.Parameters.Clear();

                        if (parameters != null && parameters.Any())
                        {
                            command.Parameters.AddRange(parameters);
                        }

                        return command.ExecuteNonQuery();

                    }

                }
            }
            catch (OverflowException ex)
            {               
                throw ex;
            }
            catch (Exception ex)
            {                
                throw ex;
            }
        }
        #endregion
    }
}
 