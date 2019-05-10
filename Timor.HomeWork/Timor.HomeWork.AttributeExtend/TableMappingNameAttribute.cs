using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timor.HomeWork.AttributeExtend
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
    public class DbMappingNameAttribute : Attribute
    {
        private string Name { get; set; }
        public DbMappingNameAttribute(string name)
        {
            Name = name;
        }
        public string GetName()
        {
            return Name;
        }
    }
}
