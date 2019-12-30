using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Data;
using Ahp.Core.Common;
using Ahp.Core.Models;

namespace Ahp.Core.Common
{

    public static partial class ExceptionExtensions
    {

        /// <summary>
        /// Returns a list of all the exception messages from the top-level
        /// exception down through all the inner exceptions. Useful for making
        /// logs and error pages easier to read when dealing with exceptions.
        /// Usage: Exception.Messages()
        /// </summary>
        public static IEnumerable<string> Messages(this Exception ex)
        {
            // return an empty sequence if the provided exception is null
            if (ex == null) { yield break; }
            // first return THIS exception's message at the beginning of the list
            yield return ex.Message;
            // then get all the lower-level exception messages recursively (if any)
            IEnumerable<Exception> innerExceptions = Enumerable.Empty<Exception>();

            if (ex is AggregateException && (ex as AggregateException).InnerExceptions.Any())
            {
                innerExceptions = (ex as AggregateException).InnerExceptions;
            }
            else if (ex.InnerException != null)
            {
                innerExceptions = new Exception[] { ex.InnerException };
            }

            foreach (var innerEx in innerExceptions)
            {
                foreach (string msg in innerEx.Messages())
                {
                    yield return msg;
                }
            }
        }
    }

    public static partial class DynamicExtension
    {
        private static CultureInfo cultureID = new CultureInfo("id-ID");
        private static CultureInfo cultureUS = new CultureInfo("en-US");

        public static string ReplaceUnderscore(this string value)
        {
            return value.Replace("_", " ");
        }

        public static string ReplaceKendoFilter(this string value)
        {
            return value.Substring(value.LastIndexOf("~'") + 2).Replace("'", "");
        }

        public static string ToAlreadyUseMesssageBy(this string value, string by)
        {
            return string.Format(BaseConstants.MESSAGE_DATA_IS_ALREADY_USE, value, by);
        }

        public static string ToGenerateAutoNumber(this string value)
        {

            return string.Format(value + "{0}", DateTime.Now.ToString("ddMMyy"));
        }


        public static decimal ToWelfarePerHour(this decimal value)
        {
            decimal val = 0;
            if (value >= 173)
            {
                val = value / 173;
            }

            return val;
        }

        public static string ReplaceUnderScoreToSpace(this string value)
        {
            return value.Replace("_", " ");
        }

        public static string ReplaceUnderScoreToSpace(this BaseEnums.EnumStatus value)
        {
            return value.ToString().Replace("_", " ");
        }

        public static string ToCurrencyIndonesia(this double? value)
        {
            var n = value.HasValue ? value.Value : 0;

            return n.ToString("N0", cultureID);
        }

        public static string ToCurrencyUnitedStates(this double? value)
        {
            var n = value.HasValue ? value.Value : 0;

            return n.ToString("N2", cultureID); //cultureUS);
        }

        public static string ToCurrencyIndonesia(this double value)
        {
            return value.ToString("N0", cultureID);
        }

        public static string ToCurrencyUnitedStates(this double value)
        {
            return value.ToString("N2", cultureID); //cultureUS);
        }


        public static string ToRoman(this int value)
        {
            Dictionary<string, int> RomanNumbers = new Dictionary<string, int>();
            RomanNumbers.Add("M", 1000);
            RomanNumbers.Add("CM", 900);
            RomanNumbers.Add("D", 500);
            RomanNumbers.Add("CD", 400);
            RomanNumbers.Add("C", 100);
            RomanNumbers.Add("XC", 90);
            RomanNumbers.Add("L", 50);
            RomanNumbers.Add("XL", 40);
            RomanNumbers.Add("X", 10);
            RomanNumbers.Add("IX", 9);
            RomanNumbers.Add("V", 5);
            RomanNumbers.Add("IV", 4);
            RomanNumbers.Add("I", 1);

            string result = "";

            foreach (var pair in RomanNumbers)
            {
                while (value >= pair.Value)
                {
                    value -= pair.Value;
                    result += pair.Key;
                }
            }
            return result;
        }

        public static int ToInteger(this string value)
        {
            Dictionary<string, int> RomanNumbers = new Dictionary<string, int>();
            RomanNumbers.Add("M", 1000);
            RomanNumbers.Add("CM", 900);
            RomanNumbers.Add("D", 500);
            RomanNumbers.Add("CD", 400);
            RomanNumbers.Add("C", 100);
            RomanNumbers.Add("XC", 90);
            RomanNumbers.Add("L", 50);
            RomanNumbers.Add("XL", 40);
            RomanNumbers.Add("X", 10);
            RomanNumbers.Add("IX", 9);
            RomanNumbers.Add("V", 5);
            RomanNumbers.Add("IV", 4);
            RomanNumbers.Add("I", 1);

            int result = 0;

            foreach (var pair in RomanNumbers)
            {
                while (value.IndexOf(pair.Key.ToString()) == 0)
                {
                    result += int.Parse(pair.Value.ToString());
                    value = value.Substring(pair.Key.ToString().Length);
                }
            }

            return result;
        }
    }
}