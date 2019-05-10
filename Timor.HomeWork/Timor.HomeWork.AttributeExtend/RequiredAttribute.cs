using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Timor.HomeWork.AttributeExtend.AttributeModels;

namespace Timor.HomeWork.AttributeExtend
{
    [AttributeUsage(AttributeTargets.Property)]
    public class RequiredAttribute : BaseAttribute
    {
        public override CheckValueReturnModel CheckValue(object oValue)
        {
            if (oValue == null || string.IsNullOrEmpty(oValue.ToString()))
            {
                return new CheckValueReturnModel() { Message = "数据为空", Success = false };
            }
            return new CheckValueReturnModel() { Message = "", Success = true };
        }
    }
}
