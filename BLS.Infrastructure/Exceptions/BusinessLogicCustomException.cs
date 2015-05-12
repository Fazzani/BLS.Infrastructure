using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BLS.Infrastructure.Exceptions
{
    /// <summary>
    /// This is used by the Exception Handling block to send raised Custom Error message towards UI
    /// </summary>
    public class BusinessLogicCustomException : BaseException
    {
        public BusinessLogicCustomException()
            : base()
        {
            // Add implementation (if required)
        }

        public BusinessLogicCustomException(string message)
            : base(message)
        {
            // Add implemenation (if required)
        }

        public BusinessLogicCustomException(string message, System.Exception inner)
            : base(message, inner)
        {
            // Add implementation
        }

        protected BusinessLogicCustomException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            //Add implemenation
        }
    }

}
