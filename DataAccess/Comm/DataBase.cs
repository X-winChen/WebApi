using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel.Design;
using API.Models.Home;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System.IO;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Reflection;

namespace API.DataAccess.Comm
{
    public class DataBase
    {
        //public static string ConnString { get; set; }
        //public DataBase(IConfiguration configuration)
        //{
        //    ConnString = configuration.GetSection("ConnectionStrings")["ConnString"];
        //}//获取连接串

        //string conStr = AppConfigurtaionServices.Configuration.GetSection("Appsettings:ConnectionString").Value;

        private static readonly string ConnString = "Data Source= ; Initial Catalog=MyBlog;User ID=sa;Password=123456;";

        /// <summary>
        /// 生成日志文件
        /// </summary>
        /// <param name="log"></param>
        private static void WriterLog(string log)
        {
            string fileName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + "Log.log";//拼接文件名

            string filePath = $@".\DataLog\" + DateTime.Now.ToString("yyyy") + "\\" + DateTime.Now.ToString("MM") + "\\" + DateTime.Now.ToString("dd") + "\\" + fileName + "";//拼接文件地址

            FileInfo fileInfo = new FileInfo(filePath);//如文件地址不存在 则创建
            var di = fileInfo.Directory;
            if (!di.Exists)
            {
                di.Create();
            }
            using (FileStream fileStream = new FileStream(filePath, FileMode.Append))
            {
                StreamWriter streamWriter = new StreamWriter(fileStream);

                streamWriter.WriteLine(DateTime.Now.ToLongTimeString() + "-------" + log);

                streamWriter.Close();

                fileStream.Close();
            }

        }


        /// <summary>
        /// 增删改方法
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parm"></param>
        /// <returns></returns>
        public static int Execute(string sql, params SqlParameter[] parm)
        {
            using (SqlConnection con = new SqlConnection(ConnString))
            {
                using (SqlCommand com = new SqlCommand(sql, con))
                {
                    try
                    {
                        if (parm != null)
                        {
                            com.Parameters.AddRange(parm);
                        }
                        con.Open();
                        return com.ExecuteNonQuery();

                    }
                    catch (Exception ex)
                    {
                        WriterLog("在执行----   " + sql + "   ----时出现错误,错误信息：" + ex.Message.ToString());
                        throw new Exception(ex.Message);
                    }
                    finally
                    {
                        con.Close();
                    }
                }
            }
        }

        /// <summary>
        /// 查询 返回一个DataTable
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parm"></param>
        /// <returns></returns>
        public static DataTable GetDataTable(string sql, params SqlParameter[] parm)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(sql, ConnString))
                {
                    if (parm != null)
                    {
                        adapter.SelectCommand.Parameters.AddRange(parm);
                    }
                    adapter.Fill(dt);
                }
            }
            catch(Exception ex)
            {
                WriterLog("在执行----   " + sql + "   ----时出现错误,错误信息：" + ex.Message.ToString());
                throw new Exception(ex.Message);
            }
            return dt;
        }

        /// <summary>
        /// 查询 返回一个集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parm"></param>
        /// <returns></returns>
        public static ObservableCollection<T> Query<T>(string sql, params SqlParameter[] parm)where T:class,new()
        {
            DataTable dt=GetDataTable(sql, parm);

            //T model = new T();

            return TableToModel<T>(dt);
        }




        /// <summary>
        /// 返回泛型集合的通用方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static ObservableCollection<T> TableToModel<T>(DataTable dataTable) where T : class, new()
        {
            Type type = typeof(T); //获取类型

            ObservableCollection<T> ts = new ObservableCollection<T>();//泛型对象集合

            //string RowName = string.Empty;//临时变量 

            foreach (DataRow item in dataTable.Rows)
            {
                T model = new T(); //定义一个model 

                PropertyInfo[] infos = type.GetProperties(); //获取model的公共属性

                foreach (PropertyInfo info in infos)
                {
                    if (dataTable.Columns.Contains(info.Name))//DataTable 是否包含此列
                    {
                        object value = item[info.Name];//取值

                        if (value!=DBNull.Value)
                        {
                            info.SetValue(model, value,null);
                        }
                    }
                }

                ts.Add(model);
            }

            return ts;

        }


    }
}
