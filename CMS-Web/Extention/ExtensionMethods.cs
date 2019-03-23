using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

public static class ExtensionMethods
{
    public static SelectList ToSelectList<TEnum>(this TEnum obj)
     where TEnum : struct, IComparable, IFormattable, IConvertible
    {

        return new SelectList(Enum.GetValues(typeof(TEnum)).OfType<Enum>()
            .Select(x =>
                new SelectListItem
                {
                    Text = Enum.GetName(typeof(TEnum), x),
                    Value = (Convert.ToInt32(x)).ToString()
                }), "Value", "Text");
    }
    public static string GetDescription(this Enum GenericEnum, string enumKey)
    {
        Type genericEnumType = GenericEnum.GetType();
        MemberInfo[] memberInfo = genericEnumType.GetMember(enumKey);
        if ((memberInfo != null && memberInfo.Length > 0))
        {
            var _Attribs = memberInfo[0].GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);
            if ((_Attribs != null && _Attribs.Count() > 0))
            {
                return ((System.ComponentModel.DescriptionAttribute)_Attribs.ElementAt(0)).Description;
            }
        }
        return enumKey;
    }
}