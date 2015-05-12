using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLS.Infrastructure.Exceptions
{
    /// <summary>
    /// This is used by the Exception Handling block to replace the original exception with a user friendly message such as 
    /// “An unknown error has occurred at User Interface while processing your request. Please contact Technical Help Desk with this identifier XXXXXXXXXXXXXX”
    /// </summary>
    public class UserInterfaceException: BaseException
    {
    }
}
