using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MVC2020.Excal.Common
{
    /// <summary>
    /// 项目开发中，经常会获取到DataTable对象，如何把它转化成一个List对象呢？前几天就碰到这个问题，网上搜索整理了一个万能类，用了泛型和反射的知识。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DataTableToList<T> where T : new()  // 此处一定要加上new()
    {
        public static List<T> ConvertToModel<T>(DataTable dt) where T : new()
        {

            // 定义集合
            List<T> list = new List<T>();
            // 获得此模型的类型
            Type type = typeof(T);
            //定义一个临时变量
            string tempName = string.Empty;
            //遍历DataTable中所有的数据行 
            foreach (DataRow dr in dt.Rows)
            {
                T t = new T();
                // 获得此模型的公共属性
                PropertyInfo[] propertys = t.GetType().GetProperties();
                //遍历该对象的所有属性
                foreach (PropertyInfo pi in propertys)
                {
                    tempName = pi.Name;//将属性名称赋值给临时变量  
                    //检查DataTable是否包含此列（列名==对象的属性名）    
                    if (dt.Columns.Contains(tempName))
                    {
                        // 判断此属性是否有Setter  
                        if (!pi.CanWrite) continue;//该属性不可写，直接跳出  
                        //取值  
                        object value = dr[tempName];
                        //如果非空，则赋给对象的属性  
                        if (value != DBNull.Value)
                            pi.SetValue(t, value, null);
                    }
                }
                //对象添加到泛型集合中
                list.Add(t);
            }
            dt.Clear();
            dt.Dispose();
            return list;
        }
    }
}
