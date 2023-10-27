using Helper;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Reflection;
using System.Text;
namespace FMS.Helpers
{
    public class OracleHelper
    {
        private string strConnectionString = "";

        public OracleHelper()
        {

            strConnectionString = new AppConfigManager().getConnectionString;
        }
        public T ExecuteScalar<T>(string query)
        {
            OracleConnection cnn = new OracleConnection(strConnectionString);
            OracleCommand cmd = new OracleCommand(query, cnn);
            T retval = Activator.CreateInstance<T>();
            try
            {
                if ((query.StartsWith("SELECT") | query.StartsWith("select")))
                    cmd.CommandType = CommandType.Text;
                else
                    cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 3600;
                cnn.Open();
                retval = (T)cmd.ExecuteScalar();

            }
            catch (Exception ex)
            {
                retval = Activator.CreateInstance<T>();
            }
            finally
            {
                if ((cnn.State == ConnectionState.Open))
                    cnn.Close();
                cmd.Dispose();
                cnn.Dispose();
            }
            return retval;
        }

        public int ExecuteNonQuery(string query)
        {
            OracleConnection cnn = new OracleConnection(strConnectionString);
            OracleCommand cmd = new OracleCommand(query, cnn);
            if ((query.StartsWith("INSERT") | query.StartsWith("insert") | query.StartsWith("UPDATE") | query.StartsWith("update") | query.StartsWith("DELETE") | query.StartsWith("delete") | query.StartsWith("exec")))
                cmd.CommandType = CommandType.Text;
            else
                cmd.CommandType = CommandType.StoredProcedure;
            int retval;
            try
            {
                cnn.Open();
                cmd.CommandTimeout = 120;
                retval = cmd.ExecuteNonQuery();
            }
            catch (Exception exp)
            {
                throw exp;
            }
            // ExceptionHelper.ExceptionHelper exphelper = new ExceptionHelper.ExceptionHelper();
            // exphelper.PublishInDatabase(exp);
            // exphelper.PublishInEventLog(exp);
            // exphelper.PublishInEmail(exp);
            finally
            {
                if ((cnn.State == ConnectionState.Open))
                    cnn.Close();
                cnn.Dispose();
                OracleConnection.ClearPool(cnn);
                cmd.Dispose();
            }
            return retval;
        }

        public int ExecuteNonQuery(string query, OracleParameter[] parameters_value)
        {
            OracleConnection cnn = new OracleConnection(strConnectionString);
            OracleCommand cmd = new OracleCommand(query, cnn);
            int retVal = -1;
            try
            {
                if ((query.StartsWith("INSERT") | query.StartsWith("insert") | query.StartsWith("UPDATE") | query.StartsWith("update") | query.StartsWith("DELETE") | query.StartsWith("delete")))
                    cmd.CommandType = CommandType.Text;
                else
                    cmd.CommandType = CommandType.StoredProcedure;
                int i;
                for (i = 0; i <= parameters_value.Length - 1; i++)
                    cmd.Parameters.Add(parameters_value[i]);
                cnn.Open();
                cmd.BindByName = true;
                retVal = cmd.ExecuteNonQuery();
                cnn.Close();
                cnn.Dispose();
            }
            catch (Exception ex)
            {

            }

            finally
            {
                if ((cnn.State == ConnectionState.Open))
                    cnn.Close();
                cmd.Dispose();

                cmd.Parameters.Clear();
                OracleConnection.ClearPool(cnn);
                cnn.Dispose();
            }
            return retVal;
        }
        public DataSet ExecuteDataSet(string query)
        {
            OracleConnection cnn = new OracleConnection(strConnectionString);
            var cmd = new OracleCommand(query, cnn);
            DataSet ds = new DataSet();
            OracleDataAdapter da = new OracleDataAdapter();
            try
            {
                if ((query.ToUpper().StartsWith("SELECT") | query.ToLower().StartsWith("select")))
                    cmd.CommandType = CommandType.Text;
                else
                    cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 3600;
                da.SelectCommand = cmd;
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                throw ex;


            }
            finally
            {
                if ((cnn.State == ConnectionState.Open))
                    cnn.Close();
                cmd.Dispose();
                cnn.Dispose();
                OracleConnection.ClearPool(cnn);

                da.Dispose();
            }
            return ds;
        }

        public DataSet ExecuteDataSet(string query, OracleParameter[] parameters_value)
        {
            OracleConnection cnn = new OracleConnection(strConnectionString);
            var cmd = new OracleCommand(query, cnn);
            OracleDataAdapter da = new OracleDataAdapter();
            DataSet ds = new DataSet();
            try
            {
                if ((query.StartsWith("SELECT") | query.StartsWith("select")))
                    cmd.CommandType = CommandType.Text;
                else
                    cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 120;
                int i;
                for (i = 0; i <= parameters_value.Length - 1; i++)
                    cmd.Parameters.Add(parameters_value[i]);
                cmd.BindByName = true;
                cmd.CommandTimeout = 5;
                da.SelectCommand = cmd;
                da.Fill(ds);
            }
            catch (Exception ex)
            {
            }
            finally
            {
                if ((cnn.State == ConnectionState.Open))
                    cnn.Close();
                cmd.Parameters.Clear();
                cmd.Dispose();

                cnn.Dispose();
                OracleConnection.ClearPool(cnn);
                da.Dispose();

            }
            return ds;
        }
      
        public List<T> GetRecords<T>(string query)
        {
            List<T> obj = new List<T>();

            DataSet ds = ExecuteDataSet(query);

            if (ds != null)
            {
                if (ds.Tables != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        obj = ConvertDataTable<T>(ds.Tables[0]);

                    }
                    else
                    {
                        obj = new List<T>();
                    }
                }
                else
                {
                    obj = new List<T>();
                }
            }
            else
            {
                obj = new List<T>();
            }

            return obj;
        }

        public List<T> GetRecords<T>(string query, OracleParameter[] parameters_value)
        {
            List<T> obj = new List<T>();

            DataSet ds = ExecuteDataSet(query, parameters_value);

            if (ds != null)
            {
                if (ds.Tables != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        obj = ConvertDataTable<T>(ds.Tables[0]);

                    }
                    else
                    {
                        obj = new List<T>();
                    }
                }
                else
                {
                    obj = new List<T>();
                }
            }
            else
            {
                obj = new List<T>();
            }

            return obj;
        }
       
        public void dispose()
        {
            this.dispose();
        }
        public List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }
        public T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name.ToLower() == column.ColumnName.ToLower())
                    {
                        pro.SetValue(obj, (dr[column.ColumnName] == DBNull.Value) ? null : dr[column.ColumnName], null);
                    }
                    else
                        continue;
                }
            }
            return obj;
        }
    }
}
