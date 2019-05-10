using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Timor.HomeWork.Util
{
    public static class PrintfModelInfo
    {
        /// <summary>
        /// 打印当前类全部的属性名称和值
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="t">类型实例化</param>
        public static void PrintNameAndValueByProperties<T>(this T t) where T : class
        {
            t.PrintProperties(i =>
            {
                Console.WriteLine($"{i.Name}----{i.GetValue(t)}");
            });
            //Type type = typeof(T);
            //foreach (var item in type.GetProperties())
            //{
            //    Console.WriteLine($"{item.Name}----{item.GetValue(t)}");
            //}
        }

        /// <summary>
        /// 打印当前类全部的属性备注和值
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="t">类型实例化</param>
        public static void PrintRemarkAndValueByProperties<T>(this T t) where T : class
        {
            t.PrintProperties(i =>
            {
                var remark = CheckAttribute.GetPropertyRemark(i,t);
                Console.WriteLine($"{(string.IsNullOrEmpty(remark) ? i.Name + "无备注信息" : remark)}----{i.GetValue(t)}");
            });
            //Type type = typeof(T);
            //foreach (var item in type.GetProperties())
            //{
            //    var remark = ExecuteAttribute.GetFiledRemark(item);
            //    Console.WriteLine($"{(string.IsNullOrEmpty(remark) ? item.Name + "无备注信息" : remark)}----{item.GetValue(t)}");
            //}
        }

        private static void PrintProperties<T>(this T t, Action<PropertyInfo> action) where T : class
        {
            Type type = typeof(T);
            foreach (var item in type.GetProperties())
            {
                action.Invoke(item);
            }
        }

        /// <summary>
        /// 打印指定的属性备注信息和值
        /// </summary>
        /// <typeparam name="T">当前类</typeparam>
        /// <param name="property">属性</param>
        /// <param name="t">类实体</param>
        public static void PrintRemarkAndValueByProperty<T>(this T t, string propertyName) where T : class
        {
            //Type type = typeof(T);
            //PropertyInfo property = type.GetProperty(propertyName);
            t.PrintByPropertyName(propertyName, i =>
            {
                var remark = CheckAttribute.GetPropertyRemark(i,t);
                Console.WriteLine($"{(string.IsNullOrEmpty(remark) ? i.Name + "无备注信息" : remark)}----{i.GetValue(t)}");
            });

        }

        /// <summary>
        /// 打印指定的属性名称和值
        /// </summary>
        /// <typeparam name="T">当前类</typeparam>
        /// <param name="property">属性</param>
        /// <param name="t">类实体</param>
        public static void PrintNameAndValueByProperty<T>(this T t, string propertyName) where T : class
        {
            //Type type = typeof(T);
            //PropertyInfo property = type.GetProperty(propertyName);
            t.PrintByPropertyName(propertyName, i =>
            {
                Console.WriteLine($"{i.Name}----{i.GetValue(t)}");
            });
            
        }

        private static void PrintByPropertyName<T>(this T t, string propertyName, Action<PropertyInfo> action) where T : class
        {
            Type type = typeof(T);
            PropertyInfo property = type.GetProperty(propertyName);
            action.Invoke(property);
        }
    }
}
