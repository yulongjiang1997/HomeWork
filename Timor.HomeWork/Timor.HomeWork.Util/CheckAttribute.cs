using Timor.HomeWork.AttributeExtend;
using Timor.HomeWork.AttributeExtend.AttributeModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Timor.HomeWork.Util
{
    public static class CheckAttribute
    {
        /// <summary>
        /// 获得属性备注
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetPropertyRemark<T>(this PropertyInfo type, T t)
        {
            //获取缓存中当前类的带有特性的属性集合
            var attributeNameList = ReflectionCache<T>.GetPropertyAttributeList();
            //查找当前集合中第一个符合当前类型的项
            var checkType = attributeNameList.FirstOrDefault(i => i.Key == type);
            //判断当前项是否有数据 且包含备注特性
            if (checkType.Value != null && checkType.Value.Contains(typeof(PropertyRemarkAttribute)))
            {
                var remarkAttribute = (PropertyRemarkAttribute)type.GetCustomAttribute(typeof(PropertyRemarkAttribute));
                return remarkAttribute.GetRemark();
            }
            return "";
        }

        /// <summary>
        /// 检查属性值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="IsAll">是否检查全部属性</param>
        /// <returns></returns>
        public static CheckValueReturnModel CheckValue<T>(this T t, bool IsAll = false)
        {
            var result = new CheckValueReturnModel() { Success = true, Message = "" };
            var tResult = CheckValue(t);
            foreach (var item in tResult)
            {
                if (!IsAll)
                {
                    return item;
                }
                result.Message += item.Message;
                result.Success = false;
            }
            return result;
        }

        /// <summary>
        /// 检查数据并返回第一个不满足要求的错误信息，已合并到上面方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static CheckValueReturnModel CheckValueReutrnError<T>(this T t)
        {
            var result = new CheckValueReturnModel() { Success = true, Message = "" };
            var tResult = CheckValue(t);
            foreach (var item in tResult)
            {
                return item;
            }
            return result;
        }

        /// <summary>
        /// 常规循环查询效验全部数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        private static IEnumerable<CheckValueReturnModel> CheckValue<T>(T t)
        {
            Type type = typeof(T);
            var result = new List<CheckValueReturnModel>();
            //获得包含特性的属性列表
            var propertyAttributeList = ReflectionCache<T>.GetPropertyAttributeList();
            //遍历出每个属性和对应的特性列表
            foreach (var property in propertyAttributeList)
            {
                //遍历出每个特性
                foreach (var item in property.Value)
                {
                    //判断特性父类是否BaseAttribute
                    if (item.BaseType == typeof(BaseAttribute))
                    {
                        //获得属性值
                        object value = property.Key.GetValue(t);
                        //获得特性实例
                        var attribute = (BaseAttribute)property.Key.GetCustomAttribute(item);
                        //调用特性方法检测
                        var tResult = attribute.CheckValue(value);
                        if (!tResult.Success)
                        {
                            tResult.Message = property.Key.Name + ":" + tResult.Message + "\r\n";
                            tResult.Success = false;
                            result.Add(tResult);
                        }
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 获得表名称映射
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string GetTableMappingName(this Type t)
        {
            return GetMappingName(t);
        }

        /// <summary>
        /// 获得属性名称映射
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public static string GetPropertyMappingName(this PropertyInfo property)
        {
            return GetMappingName(property);
        }

        /// <summary>
        /// 获得所有过滤掉主键属性
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public static IEnumerable<PropertyInfo> GetNoKeyProperties(this Type t)
        {
            return t.GetProperties().Where(i => !i.IsDefined(typeof(TimorKeyAttribute)));
        }

        /// <summary>
        /// 获得映射名称
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        private static string GetMappingName<T>(T t) where T : MemberInfo
        {
            if (t.IsDefined(typeof(DbMappingNameAttribute)))
            {
                var attribute = (DbMappingNameAttribute)t.GetCustomAttribute(typeof(DbMappingNameAttribute));
                return attribute.GetName();
            }
            else
            {
                return t.Name;
            }
        }
    }
}
