using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timor.HomeWork.AttributeExtend.AttributeModels;

namespace Timor.HomeWork.AttributeExtend
{
    [AttributeUsage(AttributeTargets.Property)]
    public class MinAndMaxValueAttribute : BaseAttribute
    {
        private long Min { get; set; }
        private long Max { get; set; }
        public MinAndMaxValueAttribute(long min, long max)
        {
            Min = min;
            Max = max;
        }
        public override CheckValueReturnModel CheckValue(object oValue)
        {
            CheckValueReturnModel resul = new CheckValueReturnModel() { Success = true };
            if (Convert.ToInt64(oValue) < Min)
            {
                resul.Message = "数据小于设定最小值";
                resul.Success = false;
            }
            else if (Convert.ToInt64(oValue) > Max)
            {
                resul.Message = "数据超过设定最大值";
                resul.Success = false;
            }
            return resul;
        }
    }
}
