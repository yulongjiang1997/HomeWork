using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Timor.HomeWork.AttributeExtend;

namespace Timor.HomeWork.Util
{
    public static class ReflectionCache<T>
    {
        /// <summary>
        /// 类上面的特性集合
        /// </summary>
        private static List<Type> ClassAttributeNameList { get; set; }

        /// <summary>
        /// 属性上面的特性集合
        /// </summary>
        private static Dictionary<PropertyInfo, List<Type>> PropertyAttributeNameList { get; set; }

        /// <summary>
        /// 当前类的所有属性集合
        /// </summary>
        private static List<PropertyInfo> PropertyList { get; set; }
        static ReflectionCache()
        {
            ClassAttributeNameList = new List<Type>();//实例化对象
            PropertyAttributeNameList = new Dictionary<PropertyInfo, List<Type>>();//实例化对象
            PropertyList = new List<PropertyInfo>();//实例化对象
            Type type = typeof(T);//得当当前泛型类型
            if (type.GetCustomAttributes().Count() > 0)
            {
                //反射获取当前类上面标记的特性名称添加到临时集合
                ClassAttributeNameList = type.GetCustomAttributes().Select(i => i.GetType()).ToList();
            }
            //获得当前类所有属性遍历
            foreach (PropertyInfo item in type.GetProperties())
            {
                PropertyList.Add(item);//吧当前属性名称添加进属性名称集合
                var attributes = item.GetCustomAttributes();//获得当前属性上标记的所有特性
                if (attributes.Count() > 0)//判断是否有特性
                {
                    PropertyAttributeNameList.Add(item, attributes.Select(i => i.GetType()).ToList());
                }
            }
        }

        public static List<Type> GetClassAttributeList()
        {
            return ClassAttributeNameList;
        }

        public static Dictionary<PropertyInfo, List<Type>> GetPropertyAttributeList()
        {
            return PropertyAttributeNameList;
        }

        public static List<PropertyInfo> GetPropertyList()
        {
            return PropertyList;
        }
    }
}
