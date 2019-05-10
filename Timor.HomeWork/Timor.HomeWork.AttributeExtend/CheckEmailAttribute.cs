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
    public class CheckEmailAttribute : BaseAttribute
    {
        public override CheckValueReturnModel CheckValue(object oValue)
        {
            if (oValue == null || !Regex.IsMatch(oValue.ToString(), @"^[A-Za-z\d]+([-_.][A-Za-z\d]+)*@([A-Za-z\d]+[-.])+[A-Za-z\d]{2,4}$"))
            {
                return new CheckValueReturnModel() { Message = "邮箱格式错误", Success = false };
            }
            return new CheckValueReturnModel() { Message = "", Success = true };
        }
    }
}
