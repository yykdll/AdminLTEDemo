using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace System
{
    public static class EnumExtensions
    {

        public static string GetEnumDesc(this Type e, string value)
        {
            try
            {
                if (string.IsNullOrEmpty(value))
                    throw new Exception("");
                FieldInfo field = e.GetField(value, BindingFlags.IgnoreCase | BindingFlags.Static | BindingFlags.Public);
                if (field == (FieldInfo)null)
                    return "";
                DescriptionAttribute[] customAttributes = (DescriptionAttribute[])field.GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (customAttributes.Length > 0)
                    return customAttributes[0].Description;
                return e.ToString();
            }
            catch
            {
                return "";
            }
        }

        public static string GetEnumDesc(this Type e, int? value)
        {
            try
            {
                FieldInfo[] fields = e.GetFields();
                int index = 1;
                for (int length = fields.Length; index < length; ++index)
                {
                    int num = (int)Enum.Parse(e, fields[index].Name);
                    int? nullable = value;
                    if ((num != nullable.GetValueOrDefault() ? 0 : (nullable.HasValue ? 1 : 0)) != 0)
                    {
                        DescriptionAttribute[] customAttributes = (DescriptionAttribute[])fields[index].GetCustomAttributes(typeof(DescriptionAttribute), false);
                        if (customAttributes.Length > 0)
                            return customAttributes[0].Description;
                    }
                }
                return "";
            }
            catch
            {
                return "";
            }
        }

        public static IEnumerable<SelectListItem> ListItem(this Type e)
        {
            List<SelectListItem> selectListItemList = new List<SelectListItem>();
            FieldInfo[] fields = e.GetFields();
            int index = 1;
            for (int length = fields.Length; index < length; ++index)
            {
                string str1 = "";
                DescriptionAttribute[] customAttributes = (DescriptionAttribute[])fields[index].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (customAttributes.Length > 0)
                    str1 = customAttributes[0].Description;
                string str2 = ((int)Enum.Parse(e, fields[index].Name)).ToString();
                selectListItemList.Add(new SelectListItem()
                {
                    Text = str1,
                    Value = str2
                });
            }
            return (IEnumerable<SelectListItem>)selectListItemList;
        }

        public static IEnumerable<SelectListItem> ListItemByName(this Type e)
        {
            List<SelectListItem> selectListItemList = new List<SelectListItem>();
            FieldInfo[] fields = e.GetFields();
            int index = 1;
            for (int length = fields.Length; index < length; ++index)
            {
                string str1 = "";
                DescriptionAttribute[] customAttributes = (DescriptionAttribute[])fields[index].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (customAttributes.Length > 0)
                    str1 = customAttributes[0].Description;
                string str2 = Enum.Parse(e, fields[index].Name).ToString();
                selectListItemList.Add(new SelectListItem()
                {
                    Text = str1,
                    Value = str2
                });
            }
            return (IEnumerable<SelectListItem>)selectListItemList;
        }

        public static string GetEnumDesc(this Enum e)
        {
            try
            {
                DescriptionAttribute[] customAttributes = (DescriptionAttribute[])e.GetType().GetField(e.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (customAttributes.Length > 0)
                    return customAttributes[0].Description;
                return e.ToString();
            }
            catch
            {
                return "";
            }
        }

        public static MvcHtmlString GetEnumJson(this HtmlHelper Html, Type enumType)
        {
            return MvcHtmlString.Create("({" + string.Join(",", enumType.GetEnumListItem().Select<EnumExtensions.EnumItem, string>((Func<EnumExtensions.EnumItem, string>)(enumItem_0 => string.Format("{0}:\\\"{1}\\\"", (object)enumItem_0.IntValue, (object)enumItem_0.Description)))) + "})");
        }

        public static MvcHtmlString GetEnumNameJson(this HtmlHelper Html, Type enumType)
        {
            return MvcHtmlString.Create("({" + string.Join(",", enumType.GetEnumListItem().Select<EnumExtensions.EnumItem, string>((Func<EnumExtensions.EnumItem, string>)(enumItem_0 => string.Format("\\\"{0}\\\":\\\"{1}\\\"", (object)enumItem_0.Name, (object)enumItem_0.Description)))) + "})");
        }

        public static MvcHtmlString GetDictionaryJson<T, TKey, TValue>(this IEnumerable<T> list, Func<T, TKey> funcTkey, Func<T, TValue> func_3)
        {
            string empty = string.Empty;
            Dictionary<TKey, TValue> jsonObject = new Dictionary<TKey, TValue>();
            foreach (T obj in list)
                jsonObject.Add(funcTkey(obj), func_3(obj));
            return MvcHtmlString.Create(jsonObject.ToJson());
        }

        public static List<EnumExtensions.EnumItem> GetEnumListItem(this Type enumType)
        {
            List<EnumExtensions.EnumItem> enumItemList = new List<EnumExtensions.EnumItem>();
            foreach (FieldInfo fieldInfo in ((IEnumerable<FieldInfo>)enumType.GetFields()).Where<FieldInfo>((Func<FieldInfo, bool>)(fieldInfo_0 => fieldInfo_0.Name != "value__")).ToArray<FieldInfo>())
            {
                EnumExtensions.EnumItem enumItem = new EnumExtensions.EnumItem();
                enumItem.Name = fieldInfo.Name;
                DescriptionAttribute[] customAttributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (customAttributes != null && customAttributes.Length > 0)
                    enumItem.Description = customAttributes[0].Description;
                enumItem.IntValue = (int)Enum.Parse(enumType, fieldInfo.Name);
                enumItemList.Add(enumItem);
            }
            return enumItemList;
        }

        public static List<EnumExtensions.EnumItem<T>> GetEnumListItem<T>(this Type enumType) where T : Attribute
        {
            List<EnumExtensions.EnumItem<T>> enumItemList = new List<EnumExtensions.EnumItem<T>>();
            foreach (FieldInfo fieldInfo in ((IEnumerable<FieldInfo>)enumType.GetFields()).Where<FieldInfo>((Func<FieldInfo, bool>)(fieldInfo_0 => fieldInfo_0.Name != "value__")).ToArray<FieldInfo>())
            {
                EnumExtensions.EnumItem<T> enumItem = new EnumExtensions.EnumItem<T>();
                enumItem.Name = fieldInfo.Name;
                DescriptionAttribute[] customAttributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (customAttributes != null && customAttributes.Length > 0)
                    enumItem.Description = customAttributes[0].Description;
                enumItem.Attribute = (T[])fieldInfo.GetCustomAttributes(typeof(T), false);
                enumItem.IntValue = (int)Enum.Parse(enumType, fieldInfo.Name);
                enumItemList.Add(enumItem);
            }
            return enumItemList;
        }

        public static EnumExtensions.EnumItem<T> GetEnumItem<T>(this Enum e) where T : Attribute
        {
            EnumExtensions.EnumItem<T> enumItem = new EnumExtensions.EnumItem<T>();
            FieldInfo field = e.GetType().GetField(e.ToString());
            enumItem.IntValue = (int)Enum.Parse(e.GetType(), field.Name);
            enumItem.Name = field.Name;
            enumItem.Attribute = (T[])field.GetCustomAttributes(typeof(T), false);
            return enumItem;
        }

        public static int GetEnumFileInfo(this Enum e, out string EnumName, out string FieldName)
        {
            Type type = e.GetType();
            FieldInfo field = type.GetField(e.ToString());
            EnumName = type.FullName;
            FieldName = field.Name;
            return (int)Enum.Parse(type, FieldName);
        }

        public class EnumItem
        {
            public string Name { get; set; }

            public string Description { get; set; }

            public int IntValue { get; set; }
        }

        public class EnumItem<T> : EnumExtensions.EnumItem where T : Attribute
        {
            public T[] Attribute { get; set; }
        }
    }
}
