using System;
using System.Linq;

namespace Extensions
{
    public static class EnumHelper
    {
        public static int GetMaxEnumValue<T>() where T : Enum
        {
            return Enum.GetValues(typeof(T)).Cast<int>().Max();
        }
    }
}