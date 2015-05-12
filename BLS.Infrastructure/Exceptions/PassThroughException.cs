using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLS.Infrastructure.Exceptions
{
    /// <summary>
    /// This is used to replace DataAccessException and DataAccessCustomException types with this type at Business Logic level. 
    /// Otherwise, Business Logic will also handle the exception as Data access is being called from Business Logic.
    /// </summary>
    public class PassThroughException: BaseException
    {
    }
}
