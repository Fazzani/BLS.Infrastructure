using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BLS.Infrastructure.Mvc.Model
{
    public class BaseModel
    {
        public BaseModel()
        {
            CurrentUser = new CurrentUserModel();
            Messages = new List<UIMessage>();
        }
        public CurrentUserModel CurrentUser { get; set; }
        public object Result { get; set; }
        public List<UIMessage> Messages { get; set; }
    }

    public class CurrentUserModel
    {
        public string DisplayName { get; set; }
        public string Email { get; set; }
    }

    public class UIMessage
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public MessageTypeEnum MessageType { get; set; }
    }

    public enum MessageTypeEnum
    {
        Info = 0,
        Warning = 1,
        Error = 2
    }
}