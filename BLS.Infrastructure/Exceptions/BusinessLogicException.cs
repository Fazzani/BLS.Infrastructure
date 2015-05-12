using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BLS.Infrastructure.Exceptions
{
    /// <summary>
    /// This is used by the Exception Handling block to replace the original exception with a user friendly message such as 
    /// “An unknown error has occurred in Business Logic Layer while processing your request. Please contact Technical Help Desk with this identifier XXXXXXXXXXXXXX”
    /// </summary>
    public class BusinessLogicException : BaseException
    {
         public BusinessLogicException()
         : base()
      { 
         // Add implementation (if required)
      }

      public BusinessLogicException(string message)
         : base(message)
      { 
         // Add implemenation (if required)
      }

      public BusinessLogicException(string message, System.Exception inner)
         : base(message, inner)
      { 
         // Add implementation
      }

      protected BusinessLogicException(SerializationInfo info, StreamingContext context)
         : base(info, context)
      { 
         //Add implemenation
      }
    }
}
