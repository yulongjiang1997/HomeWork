using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timor.HomeWork.AttributeExtend;

namespace Timor.HomeWork.Model
{
    public class Company : BaseModel
    {
        [PropertyRemark("姓名")]
        public string Name { get; set; }
        public DateTime CreateTime { get; set; }
        public int CreatorId { get; set; }
        public int? LastModifierId { get; set; }
        public DateTime? LastModifyTime { get; set; }
    }
}
