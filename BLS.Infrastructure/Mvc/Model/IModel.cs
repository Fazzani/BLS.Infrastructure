using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BLS.Infrastructure.Mvc.Model
{
    public interface IModel<T> where T : class
    {
        T ToDTO();
    }
}