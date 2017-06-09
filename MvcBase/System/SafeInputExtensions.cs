using System.ComponentModel;

namespace System
{
    public static class SafeInputExtensions
    {

        public static short ToShort(this object str)
        {
            short result = 0;
            if (str != null)
                short.TryParse(str.ToString(), out result);
            return result;
        }

        public static int ToInt(this object str)
        {
            int result = 0;
            if (str != null)
                int.TryParse(str.ToString(), out result);
            return result;
        }

        public static double ToDouble(this object str)
        {
            double result = 0.0;
            if (str != null)
                double.TryParse(str.ToString(), out result);
            return result;
        }

        public static Decimal ToDecimal(this object str)
        {
            Decimal result = new Decimal(0);
            if (str != null)
                Decimal.TryParse(str.ToString(), out result);
            return result;
        }

        public static string ToNone(this object str, string extStr)
        {
            if (str != null && !string.IsNullOrEmpty(str.ToString()))
                return str.ToString() + " " + extStr;
            return "";
        }

        public static bool ToBool(this object str)
        {
            return str != null && str.ToString().Equals("true", StringComparison.CurrentCultureIgnoreCase);
        }

        public static Guid ToGuid(this object str)
        {
            Guid result = Guid.NewGuid();
            if (str != null)
                Guid.TryParse(str.ToString(), out result);
            return result;
        }

        public static DateTime ToDateTime(this object str)
        {
            DateTime result = DateTime.Now;
            if (str != null)
                DateTime.TryParse(str.ToString(), out result);
            return result;
        }

        public static string ToDateTimeRandom()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmss") + new Random().Next(9999).ToString();
        }

        public static string ToDateString()
        {
            return DateTime.Now.ToString("yyyyMMdd");
        }

        public static string ToBr(this string str)
        {
            if (!string.IsNullOrEmpty(str))
                return str.Replace("\r\n", "<br>");
            return str;
        }
    }
}