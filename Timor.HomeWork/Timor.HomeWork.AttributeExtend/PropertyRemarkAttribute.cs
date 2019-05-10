using Timor.HomeWork.AttributeExtend.AttributeModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timor.HomeWork.AttributeExtend
{
    [AttributeUsage(AttributeTargets.Property)]
    public class PropertyRemarkAttribute : Attribute
    {
        private string Remark { get; set; }
        public PropertyRemarkAttribute(string remark)
        {
            Remark = remark;
        }

        public string GetRemark()
        {
            return Remark;
        }
    }
}
