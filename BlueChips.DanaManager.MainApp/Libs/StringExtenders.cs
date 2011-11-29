using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace BlueChips.DanaManager.MainApp.Libs
{
    public static class StringExtenders
    {
    	/// <summary>
		/// returns number of occurences of a (sub)string inside current one
		/// </summary>
		/// <param name="o"></param>
		/// <param name="sub"></param>
		/// <returns></returns>
		public static Int32 SubstringCount(this String o, String sub)
		{
			Int32 rt = 0;
			Int32 start = 0, subLen = sub.Length;
			while ((start = o.IndexOf(sub, start)) > 0) {
				++rt;
				start += subLen;
			}
			return rt;
		}
        /// <summary>
        /// tells if a string is null or empty
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static Boolean IsNullOrEmpty(this String o)
        {
            return String.IsNullOrEmpty(o);
        }
		/// <summary>
		/// tells if a string is null or empty or just spaces
		/// </summary>
		/// <param name="o"></param>
		/// <returns></returns>
		public static Boolean IsNullOrWhiteSpace(this String o)
		{
			return String.IsNullOrWhiteSpace(o);
		}
		/// <summary>
		/// tells if a string is not (null or empty)
		/// </summary>
		/// <param name="o"></param>
		/// <returns></returns>
		public static Boolean HasValue(this String o)
		{
			return !String.IsNullOrEmpty(o);
		}
		/// <summary>
        /// tells if a string contains another string, with a case-insensitive test
        /// </summary>
        /// <param name="o">the string to look into</param>
        /// <param name="test">the string to search for</param>
        /// <returns></returns>
        public static Boolean IsInsensitiveLike(this String o, String test)
        {
            return o.ToUpper().Contains(test.ToUpper());
        }

        /// <summary>
        /// transform object to string, and null to empty string
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static String ToEmptyString(this Object o)
        {
            return (o == null ? "" : o.ToString());
        }

		/// <summary>
		/// transform null string to empty string
		/// </summary>
		/// <param name="o"></param>
		/// <returns></returns>
		public static String ToEmptyString(this String s)
		{
			return (s ?? "");
		}

		
		/// <summary>
        /// ensures an empty / white space string is converted to null
        /// </summary>
        /// <param name="o"></param>
        public static String ToDbString(this String o){
            if (String.IsNullOrWhiteSpace(o))
                return null;
            else
                return o;
        }

        /// <summary>
        /// returns the MD5 signature of a stringified object
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        static public string Md5(this Object data)
        {
            MD5 md5 = MD5CryptoServiceProvider.Create();
            byte[] dataMd5 = md5.ComputeHash(Encoding.Default.GetBytes(data.ToString()));
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < dataMd5.Length; i++)
                sb.AppendFormat("{0:x2}", dataMd5[i]);
            return sb.ToString();
        }

        /// <summary>
        /// Confronta 2 stringhe eliminando caratteri speciali \n
        /// e restituendo il coefficiente di matching
        /// </summary>
        public static double FuzzyCompare(this String str1, String str2)
        {
            str1 = str1.Trim().ToLowerInvariant();
            str2 = str2.Trim().ToLowerInvariant();

            if (String.Compare(str1, str2, true) == 0) return 1.0;

            char[] chars1 = str1.ToCharArray();
            char[] chars2 = str2.ToCharArray();

            int pos1 = 0;
            int pos2 = 0;
            int length1 = chars1.Length;
            int length2 = chars2.Length;
            int matchCount = 0;
            bool cont = true;

            while (true)
            {
                char c1 = '_', c2 = '/';

                length1++;
                do
                {
                    length1--;
                    if (pos1 == chars1.Length)
                    {
                        cont = false;
                        break;
                    }
                    c1 = chars1[pos1++];
                } while (!((c1 >= 'a' && c1 <= 'z') || (c1 >= '0' && c1 <= '9')));

                length2++;
                do
                {
                    length2--;
                    if (pos2 == chars2.Length)
                    {
                        cont = false;
                        break;
                    }
                    c2 = chars2[pos2++];
                } while (!((c2 >= 'a' && c2 <= 'z') || (c2 >= '0' && c2 <= '9')));

                if (!cont) break;

                if (c1 == c2)
                {
                    matchCount++;
                }
                else
                {
                    break;
                }
            }

            int maxLength = Math.Max(length1, length2);
            return matchCount / (double)maxLength;
        }

    }
}
