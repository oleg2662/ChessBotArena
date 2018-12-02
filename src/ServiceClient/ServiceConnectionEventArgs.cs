using System;
using System.Collections.Generic;
using System.Text;

namespace BoardGame.ServiceClient
{
    public class ServiceConnectionEventArgs
    {
        protected ServiceConnectionEventArgs(bool isError = false)
        {
            IsError = isError;
        }

        public bool IsError { get; }

        public string Message { get; set; }

        public Exception Exception { get; set; }

        public static ServiceConnectionEventArgs Ok() => new ServiceConnectionEventArgs(false);

        public static ServiceConnectionEventArgs Ok(string message) => new ServiceConnectionEventArgs(false)
        {
            Message = message
        };

        public static ServiceConnectionEventArgs Error(string message) => new ServiceConnectionEventArgs(true)
        {
            Message = message
        };

        public static ServiceConnectionEventArgs Error(Exception exception) => new ServiceConnectionEventArgs(true)
        {
            Message = exception.Message,
            Exception = exception
        };

        public static ServiceConnectionEventArgs Error(string message, Exception exception) => new ServiceConnectionEventArgs(true)
        {
            Message = message,
            Exception = exception
        };
    }
}
