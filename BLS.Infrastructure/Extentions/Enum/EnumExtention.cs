﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BLS.Infrastructure.Extentions.Enums
{
  public static class EnumExtention
  {
    public static string GetEnumDescription(this Enum value)
    {
      FieldInfo fi = value.GetType().GetField(value.ToString());

      DescriptionAttribute[] attributes =
          (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

      return attributes != null && attributes.Length > 0 ? attributes[0].Description : value.ToString();
    }
  }
}
