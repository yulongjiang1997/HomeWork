using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Timor.HomeWork.Factory
{
    public static class CreateObject
    {
        public static T GetObject<T>(List<string> configString)
        {
            Assembly assembly = Assembly.Load(configString[0]);
            var types = assembly.GetTypes();
            Type type = assembly.GetType(configString[1]);
            return (T)Activator.CreateInstance(type);
        }
    }
}
