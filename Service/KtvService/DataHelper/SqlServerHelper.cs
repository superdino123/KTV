using Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataHelper
{
    public abstract class SqlServerHelper
    {
        /// <summary>
        /// 在KTVDB库执行Sql
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        public static DataTable GetDataFromKtvdb(string strSql)
        {
            using (var connection = ConnectionHelper.GetSqlConnection())
            {
                var ds = new DataSet();
                try
                {
                    connection.Open();
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = strSql;
                    cmd.CommandType = CommandType.Text;

                    using (var dataAdapter = new SqlDataAdapter(cmd))
                    {
                        dataAdapter.Fill(ds);
                    }

                    return ds.Tables.Count > 0 ? ds.Tables[0] : null;
                }
                catch (Exception ex)
                {
                    LogHelper.LogError("执行sql出错", ex);
                    return null;
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        /// <summary>
        /// 增删改操作使用此方法（需要一个存在的连接）
        /// </summary> 
        /// <param name="commandType">命令类型（sql或者存储过程）</param>
        /// <param name="commandText">sql语句或者存储过程名称</param>
        /// <param name="commandParameters">命令所需参数数组</param>
        /// <returns>返回受影响的行数</returns>
        public static int ExecuteNonQuery(CommandType cmdType, string cmdText, int timeOut, SqlParameter[] commandParameters)
        {
            using (var connection = ConnectionHelper.GetSqlConnection())
            {
                // 创建一个OracleCommand
                SqlCommand cmd = new SqlCommand();
                //调用静态方法PrepareCommand完成赋值操作
                PrepareCommand(cmd, connection, null, cmdType, cmdText, timeOut, commandParameters);
                //执行命令返回
                int val = 0;
                try
                {
                    val = cmd.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    throw;
                }
                //清空参数
                cmd.Parameters.Clear();
                return val;
            }  
        }

        /// <summary>
        /// 增删改操作使用此方法（需要一个存在的事务参数）
        /// </summary>
        /// <param name="trans">一个存在的事务</param>
        /// <param name="commandType">命令类型（sql或者存储过程）</param>
        /// <param name="commandText">sql语句或者存储过程名称</param>
        /// <param name="commandParameters">命令所需参数数组</param>
        /// <returns>返回受影响的行数</returns>
        public static int ExecuteNonQuery(SqlTransaction trans, CommandType cmdType, string cmdText, int timeOut, params SqlParameter[] commandParameters)
        {
            // 创建一个SqlCommand
            SqlCommand cmd = new SqlCommand();
            //调用静态方法PrepareCommand完成赋值操作
            PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, timeOut, commandParameters);
            //执行命令返回
            int val = 0;
            try
            {
                val = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw;
            }
            //清空参数
            cmd.Parameters.Clear();
            return val;
        }

        /// <summary>
        /// 一个静态的预处理函数
        /// </summary>
        /// <param name="cmd">存在的SqlCommand对象</param>
        /// <param name="conn">存在的SqlConnection对象</param>
        /// <param name="trans">存在的SqlTransaction对象</param>
        /// <param name="cmdType">命令类型（sql或者存在过程）</param>
        /// <param name="cmdText">sql语句或者存储过程名称</param>
        /// <param name="timeOut">超时时间</param>
        /// <param name="commandParameters">Parameters for the command</param>
        private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, CommandType cmdType, string cmdText, int timeOut, SqlParameter[] commandParameters)
        {
            //如果连接未打开，先打开连接
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
            }
            catch (Exception)
            {
                throw;
            }

            //未要执行的命令设置参数
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            cmd.CommandType = cmdType;
            cmd.CommandTimeout = timeOut;

            //如果传入了事务，需要将命令绑定到指定的事务上去
            if (trans != null)
                cmd.Transaction = trans;

            //将传入的参数信息赋值给命令参数
            if (commandParameters != null)
            {
                cmd.Parameters.AddRange(commandParameters);
            }
        }
    }
}
