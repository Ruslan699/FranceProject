using System;
using System.Collections.Generic;

namespace DaddyAgencies.Common.Util
{
    public static class ExceptionExtensions
    {
        public static IEnumerable<string> ExceptionMessages(this Exception ex)
        {
            //Collect all inner exceptions
            while (true)
            {
                yield return ex.Message;
                if (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                    continue;
                }
                break;
            }
        }
    }
}
