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
    public class CheckPhoneAttribute : BaseAttribute
    {
        public override CheckValueReturnModel CheckValue(object oValue)
        {
            if (!Regex.IsMatch(oValue.ToString(), @"^1(3|4|5|7|8|9)\d{9}$"))
            {
                return new CheckValueReturnModel() { Message = "手机号码格式错误", Success = false };
            }
            return new CheckValueReturnModel() { Message = "", Success = true };
        }
    }
}
