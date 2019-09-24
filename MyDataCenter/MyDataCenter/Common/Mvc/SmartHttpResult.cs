using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyDataCenter.Common.Mvc
{
    public class SmartHttpResult
    {
        public bool status { get; set; }
        public string msg { get; set; }

        public void Set(bool status)
        {
            this.status = status;
        }
        public void Set(bool status, string msg)
        {
            this.status = status;
            this.msg = msg;
        }
    }

    public class SmartHttpResult<T> : SmartHttpResult where T : class
    {
        public T data { get; set; }

        public void Set(bool status, string msg, T data)
        {
            //this.status = status;
            //this.msg = msg;
            Set(status,msg);
            this.data = data;
        }

        public void Set(bool status, T data)
        {
            Set(status);
            this.data = data;
        }
    }
}
