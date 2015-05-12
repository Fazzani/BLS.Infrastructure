using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BLS.Infrastructure.Exceptions
{
    /// <summary>
    /// This is used by the Exception handling Block to replace the original exception with a User Friendly message such as 
    /// “An unknown error has occurred at database level while processing your request. Please contact Technical Help Desk with this identifier XXXXXXXXXXXXXX”
    /// </summary>
    public class DataAccessException : BaseException
   {
      public DataAccessException()
         : base()
      { 
         // Add implementation (if required)
      }

      public DataAccessException(string message)
         : base(message)
      { 
         // Add implemenation (if required)
      }

      public DataAccessException(string message, System.Exception inner)
         : base(message, inner)
      { 
         // Add implementation
      }

      protected DataAccessException(SerializationInfo info, StreamingContext context)
         : base(info, context)
      { 
         //Add implemenation
      }

   }
}
