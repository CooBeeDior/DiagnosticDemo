using System;
using System.Collections.Generic;
using System.Text;

namespace SpiderCore
{
    public class NotFoundServiceException : Exception
    {
        public string ServiceName { get; }
        public NotFoundServiceException(string serviceName)
        { 
            this.ServiceName = serviceName; 
        }




        public override string ToString()
        {
            return $"{ this.ServiceName}服务未注册";
        }



    }
}
