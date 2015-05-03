using System;
using System.ComponentModel;

namespace Sample.Core.Common
{
    public static class EnumUtils
    {
        public static TEnum ParseFromString<TEnum>(string value)
        {
            return (TEnum)Enum.Parse(typeof(TEnum), value);
        }

        public static string Description<TEnum>(this TEnum enumObject)
        {
            var type = enumObject.GetType();
            var memInfo = type.GetMember(enumObject.ToString());

            if (memInfo.Length <= 0) 
                return enumObject.ToString();

            var attributes = (DescriptionAttribute[])memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

            return attributes.Length > 0 ? attributes[0].Description : enumObject.ToString();
        }

    }
}