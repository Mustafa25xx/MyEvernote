using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.Common.Helpers
{
    public class ConfigHelper // mail için 1 .adım
    {
        public static T  Get<T>(string key)
        {
         return (T)Convert.ChangeType(ConfigurationManager.AppSettings[key],typeof(T));
        
        }

    }
}
