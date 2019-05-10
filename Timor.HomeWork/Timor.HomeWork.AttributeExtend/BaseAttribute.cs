using Timor.HomeWork.AttributeExtend.AttributeModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Timor.HomeWork.AttributeExtend
{
    public abstract class BaseAttribute : Attribute
    {
        public abstract CheckValueReturnModel CheckValue(object oValue);
    }
}
