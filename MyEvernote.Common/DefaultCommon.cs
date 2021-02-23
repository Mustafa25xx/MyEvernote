using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.Common
{
    public class DefaultCommon : Icommon
    {
        public string GetUsername()
        {
            return  "system";
        }
    }
}
