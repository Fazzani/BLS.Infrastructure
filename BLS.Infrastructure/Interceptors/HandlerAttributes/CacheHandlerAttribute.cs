using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BLS.Infrastructure.Interceptors.HandlerAttributes
{
    /// <summary>
    /// Cache function's result by key
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class CacheHandlerAttribute : HandlerAttribute
    {
        private readonly string _key;

        public CacheHandlerAttribute(string key)
        {
            this._key = key;
        }

        public override ICallHandler CreateHandler(IUnityContainer container)
        {
            return new CachingCallHandler() { Key = _key };
        }
    }

    public class CachingCallHandler : ICallHandler
    {
        readonly ObjectCache Cache = MemoryCache.Default;
        readonly System.Web.Caching.Cache _cacheWeb;

        public CachingCallHandler()
        {
            _cacheWeb = HttpContext.Current.Cache;
        }

        public bool IsWebContext { get { return HttpContext.Current != null; } }

        public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
        {
            return IsWebContext ? HandleWebCache(input, getNext) : HandleDefaultCache(input, getNext);
        }

        /// <summary>
        /// Http.Context.Cache Provider
        /// </summary>
        /// <param name="input"></param>
        /// <param name="getNext"></param>
        /// <returns></returns>
        private IMethodReturn HandleWebCache(IMethodInvocation input, GetNextHandlerDelegate getNext)
        {
            //Before invoking the method on the original target
            var item = _cacheWeb.Get(Key);
            if (item != null)
                return input.CreateMethodReturn(item);
            IMethodReturn result = getNext()(input, getNext);
            _cacheWeb[Key] = result.ReturnValue;
            return result;
        }

        /// <summary>
        /// Memory cache Provider
        /// </summary>
        /// <param name="input"></param>
        /// <param name="getNext"></param>
        /// <returns></returns>
        private IMethodReturn HandleDefaultCache(IMethodInvocation input, GetNextHandlerDelegate getNext)
        {
            //Before invoking the method on the original target
            CacheItemPolicy policy = new CacheItemPolicy();
            policy.Priority = CacheItemPriority.NotRemovable;
            var item = Cache.Get(Key);
            if (item != null)
                return input.CreateMethodReturn(item);
            IMethodReturn result = getNext()(input, getNext);
            Cache.Add(Key, result.ReturnValue, policy);
            return result;
        }

        public string Key
        {
            get;
            set;
        }

        public int Order
        {
            get;
            set;
        }
    }

}
