using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BLS.Infrastructure.Exceptions
{
    public class BaseException : Exception, ISerializable
    {
         public BaseException()
         : base()
      { 
         // Add implementation (if required)
      }

      public BaseException(string message)
         : base(message)
      { 
         // Add implemenation (if required)
      }

      public BaseException(string message, System.Exception inner)
         : base(message, inner)
      { 
         // Add implementation
      }

      protected BaseException(SerializationInfo info, StreamingContext context)
         : base(info, context)
      { 
         //Add implemenation
      }
    }
}
