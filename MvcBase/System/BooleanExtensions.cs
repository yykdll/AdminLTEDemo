using System.ComponentModel;

namespace System
{
    public static class BooleanExtensions
    {
        static BooleanExtensions()
        {
            LicenseManager.Validate(typeof(BooleanExtensions));
        }

        public static bool CheckRoles(this object value1, object value2)
        {
            return (value1.ToString().ToInt() & value2.ToString().ToInt()) == value2.ToString().ToInt();
        }

        public static string ToChinese(this bool? value, string trueString, string falseString)
        {
            if (value.HasValue && value.Value)
                return trueString;
            return falseString;
        }

        public static string ToShiFou(this bool? value)
        {
            return value.ToChinese("是", "否");
        }

        public static string ToYouWu(this bool? value)
        {
            return value.ToChinese("有", "无");
        }

        public static string ToZhenJia(this bool? value)
        {
            return value.ToChinese("真", "假");
        }
    }
}