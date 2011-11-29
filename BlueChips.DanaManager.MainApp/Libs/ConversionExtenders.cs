using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace BlueChips.DanaManager.MainApp.Libs
{
    public static class ConversionExtenders
    {
        #region Loose type converions
       
        /// <summary>
        /// tries a to-int32 conversion returning default value on failure
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static Int32? AsInt(this String str, Int32? defaultValue = null)
        {
            return str.AsInt32(defaultValue);
        }
        /// <summary>
        /// tries a to-int16 conversion returning default value on failure
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static Int16? AsInt16(this String str, Int16? defaultValue = null)
        {
            if (String.IsNullOrEmpty(str))
                return defaultValue;
            else
                return Tools.TryThis(() => str.ConvertToInt16(), defaultValue);
        }
        /// <summary>
        /// tries a to-int32 conversion returning default value on failure
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static Int32? AsInt32(this String str, Int32? defaultValue = null)
        {
            if (String.IsNullOrEmpty(str))
                return defaultValue;
            else
                return Tools.TryThis(() => str.ConvertToInt32(), defaultValue);
        }
        /// <summary>
        /// tries a to-int64 conversion returning default value on failure
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static Int64? AsInt64(this String str, Int64? defaultValue = null)
        {
            if (String.IsNullOrEmpty(str))
                return defaultValue;
            else
                return Tools.TryThis(() => str.ConvertToInt64(), defaultValue);
        }
        /// <summary>
        /// tries a to-Char conversion returning default value on failure
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static Char? AsChar(this String str, Char? defaultValue = null)
        {
            if (String.IsNullOrEmpty(str)) return defaultValue;
            return str[0];
        }
        /// <summary>
        /// tries a to-datetime conversion returning default value on failure
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static DateTime? AsDateTime(this String str, DateTime? defaultValue = null)
        {
            if (String.IsNullOrEmpty(str)) return defaultValue;
            return Tools.TryThis(() => str.ConvertToDateTime(), defaultValue);
        }
        /// <summary>
        /// tries a to-datetime conversion returning default value on failure
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static DateTime? AsDateTime(this String str, String format, DateTime? defaultValue = null)
        {
            if (String.IsNullOrEmpty(str)) return defaultValue;
            return Tools.TryThis(() => str.ConvertToDateTime(format), defaultValue);
        }

        /// <summary>
        /// tries a to-int32 conversion returning default value on failure
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static Int32? AsInt(this Object value, Int32? defaultValue = null)
        {
            if (value == null)
                return defaultValue;
            else
                return Tools.TryThis(() => value.ConvertToInt32(), defaultValue);
        }


        /// <summary>
        /// tries a to-datetime conversion returning default value on failure
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static DateTime? AsDateTime(this object value, DateTime? defaultValue = null)
        {
            if (value == null) return defaultValue;
            return Tools.TryThis(() => value.ConvertToDateTime(), defaultValue);
        }
        /// <summary>
        /// tries a to-datetime conversion returning default value on failure
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static DateTime? AsDateTime(this object value, String format, DateTime? defaultValue = null)
        {
            if (value == null) return defaultValue;
            return Tools.TryThis(() => value.ConvertToDateTime(format), defaultValue);
        }
        #endregion

        #region Boolean parsing and conversion
        public static readonly String[] FalseStringValues = { "", "0", "false", "n", "no", "f" };

        static public bool ParseBool(this String val)
        {
            if (val == null) return false;
            val = val.Trim().ToLower();

            if (Array.IndexOf(FalseStringValues, val) >= 0) {
                return false;
            }

            return true;
        }

        static public bool ParseBool(this object val)
        {
            if (val == null) return false;
            if (val is bool) return (bool)val;
            return ParseBool(val.ToString());
        }
        #endregion

        #region Strict type conversions
        /// <summary>
        /// converts a struct to its nullable version
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Nullable<T> ToNullable<T>(this T value)
            where T : struct
        {
            return new Nullable<T>(value);
        }

        /// <summary>
        /// forcibly converts a String to any type, raising exceptions on failure
        /// </summary>
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object ConvertTo(this String value, Type type)
        {
            if (value.IsNullOrEmpty()) {
                if (type.IsClass)
                    return null;
                else
                    throw new NotSupportedException(type + " does not support null");
            }

            // If type is NULLABLE, then get the underlying type. eg if "Nullable<Int32>" then this will return just "Int32"
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>)) {
                type = type.GetGenericArguments()[0];
            }

            if (type == typeof(String)) {
                return value;
            } else if (type == typeof(Int16)) {
                return value.ConvertToInt16();
            } else if (type == typeof(Int32)) {
                return value.ConvertToInt32();
            } else if (type == typeof(Int64)) {
                return value.ConvertToInt64();
            } else if (type == typeof(Double)) {
                return value.ConvertToDouble();
            } else if (type == typeof(Boolean)) {
                return value.ConvertToBoolean();
            } else if (type == typeof(Guid)) {
                return value.ConvertToGuid();
            } else if (type == typeof(DateTime)) {
                return value.ConvertToDateTime();
            } else
                throw new NotSupportedException("Conversion to " + type + " not (yet) supported");
        }

        /// <summary>
        /// forcibly converts a String to any type, raising exceptions on failure
        /// </summary>
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object ConvertTo(this Object value, Type type)
        {
            if (value == null) {
                if (type.IsClass)
                    return null;
                else
                    throw new NotSupportedException(type + " does not support null");
            }

            // If type is NULLABLE, then get the underlying type. eg if "Nullable<Int32>" then this will return just "Int32"
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>)) {
                type = type.GetGenericArguments()[0];
            }

            if (type == typeof(String)) {
                return value.ToString();
            } else if (type == typeof(Int16)) {
                return value.ConvertToInt16();
            } else if (type == typeof(Int32)) {
                return value.ConvertToInt32();
            } else if (type == typeof(Int64)) {
                return value.ConvertToInt64();
            } else if (type == typeof(Double)) {
                return value.ConvertToDouble();
            } else if (type == typeof(Boolean)) {
                return value.ConvertToBoolean();
            } else if (type == typeof(Guid)) {
                return value.ConvertToGuid();
            } else if (type == typeof(DateTime)) {
                return value.ConvertToDateTime();
            } else
                throw new NotSupportedException("Conversion to " + type + " not (yet) supported");
        }

        /// <summary>
        /// object-to-type conversion support function, with fallback to string parse
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="o"></param>
        /// <param name="parse"></param>
        /// <returns></returns>
        private static T ConvertTo<T>(this Object o, Func<String, T> parse)
        {
            if (o is T)
                return (T)o;
            else
                return parse(o.ToString());

        }

        #region Guid
        /// <summary>
        /// convert String to Guid
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static Guid ConvertToGuid(this String s)
        {

			return Guid.Parse(s);
        }
        /// <summary>
        /// convert Object to Guid
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static Guid ConvertToGuid(this Object o)
        {
            return ConvertTo<Guid>(o, x => ConvertToGuid(x));
        }
        #endregion

        #region Integers
        /// <summary>
        /// convert String to Int64
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static Int64 ConvertToInt64(this String s)
        {
            return Int64.Parse(s);
        }

        /// <summary>
        /// convert Object to Int64
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static Int64 ConvertToInt64(this Object o)
        {
            return ConvertTo<Int64>(o, x => ConvertToInt64(x));
        }

        /// <summary>
        /// convert String to Int32
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static Int32 ConvertToInt32(this String s)
        {
            return Int32.Parse(s);
        }

        /// <summary>
        /// convert Object to Int32
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static Int32 ConvertToInt32(this Object o)
        {
            return ConvertTo<Int32>(o, x => ConvertToInt32(x));
        }

        /// <summary>
        /// convert String to Int16
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static Int16 ConvertToInt16(this String s)
        {
            return Int16.Parse(s);
        }

        /// <summary>
        /// convert Object to Int16
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static Int16 ConvertToInt16(this Object o)
        {
            return ConvertTo<Int16>(o, x => ConvertToInt16(x));
        }
        #endregion

        #region Double
        /// <summary>
        /// convert String to Double
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static Double ConvertToDouble(this String s)
        {
            return Double.Parse(s);
        }

        /// <summary>
        /// convert Object to Double
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static Double ConvertToDouble(this Object o)
        {
            return ConvertTo<Double>(o, x => ConvertToDouble(x));
        }
        #endregion

        #region Boolean
        /// <summary>
        /// convert String to Boolean
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static Boolean ConvertToBoolean(this String s)
        {
            return ConversionExtenders.ParseBool(s);
        }

        /// <summary>
        /// convert Object to Boolean
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static Boolean ConvertToBoolean(this Object o)
        {
            return ConversionExtenders.ParseBool(o);
        }
        #endregion

        #region DateTime
        /// <summary>
        /// convert String to DateTime
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static DateTime ConvertToDateTime(this String s)
        {
            return DateTime.Parse(s, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// convert String to DateTime from custom format
        /// </summary>
        /// <param name="s"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static DateTime ConvertToDateTime(this String s, String format)
        {
            return DateTime.ParseExact(s, format, CultureInfo.CurrentCulture);
        }
        /// <summary>
        /// convert Object to DateTime
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static DateTime ConvertToDateTime(this Object o)
        {
            return ConvertTo<DateTime>(o, x => ConvertToDateTime(x));
        }
        /// <summary>
        /// convert Object to DateTime from custom format
        /// </summary>
        /// <param name="s"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static DateTime ConvertToDateTime(this Object o, String format)
        {
            return ConvertTo<DateTime>(o, x => ConvertToDateTime(x, format));
        }
        #endregion

        #endregion
    }
}
