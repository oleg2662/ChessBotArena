using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace BoardGame.ServiceClient
{
    public class ServiceConnectionEventArgs
    {
        protected ServiceConnectionEventArgs(bool isError = false, [CallerMemberName] string callerMemberName = "")
        {
            IsError = isError;
            CallerMemberName = callerMemberName;
        }

        public bool IsError { get; }

        public string Message { get; set; }

        public Exception Exception { get; set; }

        public string CallerMemberName { get; set; }

        public static ServiceConnectionEventArgs Ok(string message = "", [CallerMemberName] string callerMemberName = "") => new ServiceConnectionEventArgs(false, callerMemberName)
        {
            Message = message
        };

        public static ServiceConnectionEventArgs Error(Exception exception = null, [CallerMemberName] string callerMemberName = "") => new ServiceConnectionEventArgs(true, callerMemberName)
        {
            Exception = exception
        };

        public static ServiceConnectionEventArgs Error(string message = "", Exception exception = null, [CallerMemberName] string callerMemberName = "") => new ServiceConnectionEventArgs(true, callerMemberName)
        {
            Message = message,
            Exception = exception
        };
    }
}
