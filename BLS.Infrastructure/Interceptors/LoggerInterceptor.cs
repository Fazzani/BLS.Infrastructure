using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity.InterceptionExtension;
using Microsoft.Practices.ServiceLocation;
using log4net;
using System.Data.SqlClient;
using BLS.Infrastructure.Exceptions;

namespace BLS.Infrastructure.Interceptors
{
    public class LoggerInterceptor : IInterceptionBehavior
    {
        public IEnumerable<Type> GetRequiredInterfaces()
        {
            throw new NotImplementedException();
        }
        const string message = "An unknown error has occurred at database level while processing your request. Please contact Technical Help Desk with this identifier {0}";

        public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
            ILog log = ServiceLocator.Current.GetInstance<ILog>();
            IMethodReturn result = getNext()(input, getNext);
            if (result.Exception == null)
            {
                log.InfoFormat("The method {0} returned {1}", getNext.GetType(), result.ReturnValue);
                return result;
            }

            log.ErrorFormat("Exception : ", result.Exception.Message);

            if ((result.Exception is SqlException))
            {
                SqlException dbExp = (SqlException)result.Exception;
                log.ErrorFormat("SQL exception number : {0}", dbExp.Number);
                throw new DataAccessException(string.Format(message, "sql_" + dbExp.Number), result.Exception);
            }
            if (result.Exception is DataAccessCustomException)
            {
                throw result.Exception;
            }
            if (result.Exception is BusinessLogicCustomException)
            {
                throw result.Exception;
            }
            if (result.Exception is DataAccessException)
            {
                throw new BusinessLogicException(result.Exception.Message, result.Exception.InnerException);
            }
            else if (result.Exception is System.Exception)
            {
                throw result.Exception;
            }
            return result;
        }

        public bool WillExecute
        {
            get { return true; }
        }
    }
}
