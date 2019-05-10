using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timor.HomeWork.AttributeExtend.AttributeModels;

namespace Timor.HomeWork.AttributeExtend
{
    [AttributeUsage(AttributeTargets.Property)]
    public class CheckValueLengthAttribute : BaseAttribute
    {
        private int MinLength { get; set; }
        private int MaxLength { get; set; }
        public CheckValueLengthAttribute(int min, int max)
        {
            MinLength = min;
            MaxLength = max;
        }
        public override CheckValueReturnModel CheckValue(object oValue)
        {
            CheckValueReturnModel resul = new CheckValueReturnModel() { Success = true };
            if(oValue ==null||string.IsNullOrEmpty(oValue.ToString()))
            {
                resul.Message = "数据为空，验证长度失败";
                resul.Success = false;
            }
            else if (oValue.ToString().Length < MinLength)
            {
                resul.Message = "数据长度小于设定最小值";
                resul.Success = false;
            }
            else if (oValue.ToString().Length > MaxLength)
            {
                resul.Message = "数据长度超过设定最大值";
                resul.Success = false;
            }
            return resul;
        }
    }
}
