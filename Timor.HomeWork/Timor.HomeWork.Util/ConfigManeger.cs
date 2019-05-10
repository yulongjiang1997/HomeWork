using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timor.HomeWork.Util
{
    public class ConfigManeger
    {
        private static string IDbServiceConfig{get;set;}
        static ConfigManeger()
        {
            IDbServiceConfig = ConfigurationManager.AppSettings["IDbService"];
        }
        public static List<string> GetIDbServiceConfig()
        {
            return IDbServiceConfig.Split(',').ToList();
        }
    }
}
