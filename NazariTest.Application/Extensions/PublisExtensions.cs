using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NazariTest.Application.Extensions
{
    public static partial  class PublisExtensions
    {
        public static string ToDescription(this Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }
        public static Guid ToGuid(this object obj)
        {
            if (obj == null)
            {
                return Guid.Empty;
            }

            try
            {
                return new Guid(obj.ToString());
            }
            catch
            {
                return Guid.Empty;
            }
        }

        public static string GregorianToShamsi(this DateTime date, bool onlyDate = false)
        {
            PersianCalendar pc = new PersianCalendar();
            if (onlyDate)
                return $"{pc.GetYear(date)}/{pc.GetMonth(date)}/{pc.GetDayOfMonth(date)}";
            else
                return $"{pc.GetYear(date)}/{pc.GetMonth(date)}/{pc.GetDayOfMonth(date)}  {date.Hour}:{date.Minute}";
        }
        public static DateTime ShamsiToGregorian(this string date)
        {
            PersianCalendar persianCalendar = new PersianCalendar();

            var dateArray = date.Split('/');
            int Y = Convert.ToInt16(dateArray[0]);
            int M = Convert.ToInt16(dateArray[1]);
            int D = Convert.ToInt16(dateArray[2]);
            var result = persianCalendar.ToDateTime(Y, M, D, 0, 0, 1, 1);
            return result;

        }

    }
}
