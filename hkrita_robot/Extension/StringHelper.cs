using hkrita_robot.Maths;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hkrita_robot.Extension
{
    public class StringHelper
    {
        public delegate T Supplier<T>();
        public delegate GenerateUniqueNameIterator Request();


        public static String InputString()
        {
            Console.WriteLine("Enter string input: ");
            return Console.ReadLine();
        }

        public static Boolean IsNullOrEmtpy(String value)
        {
            return value == null;
        }

        public static string ToNonNull(String value)
        {
            return value == null ? "" : value;
        }

        public static string ListToString<T>(T[] param)
        {
            if (param == null) return "";
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < param.Length; i++)
            {
                if (i != 0) builder.Append(", ");
                String current = param[i] == null ? "null" :
                    param[i].GetType().IsInstanceOfType(new Object[i]) ? ListToString(param: param[i] as object[]) : param[i].ToString();
                builder.Append(current);
            }
            return null;
        }

        public interface GenerateUniqueNameIterator
        {
            String Next();
        }
        public static string MySubString(string input, int startIndex, int endIndex)
        {
            return input.Substring(startIndex, endIndex - startIndex);
        }


        public static string GenerateUniqueName(string given, Request requester)
        {
            string prettyName = given;
            for (int k = 1; ; k++)
            {
                bool has = false;
                GenerateUniqueNameIterator itor = requester.Invoke();

                String next = itor.Next();
                do
                {
                    if (next == null) break;
                    if (prettyName.Equals(next))
                    {
                        has = true;
                        break;
                    }
                    next = itor.Next();
                }
                while (true);
                if (!has) return prettyName;
                prettyName = Format("{0}_{1}", given, k);
            }
        }

        public object ExtractString(string text)
        {
            return null;
        }

        public static string Format(String formatMsg, params Object[] param)
        {
            StringBuilder builder = new StringBuilder();
            int offset = 0;
            while (offset < formatMsg.Length)
            {
                int start = formatMsg.IndexOf('{', offset);
                if (start < 0) break;
                int end = formatMsg.IndexOf('}', start + 1);
                if (end < 0) break;
                int next = formatMsg.IndexOf('{', start + 1);
                if (next >= 0 && next < end)
                {
                    builder.Append(formatMsg.Substring(offset, next));
                    offset = next;
                    continue;
                }
                string arg = MySubString(formatMsg, start + 1, end);
                string value = GetArgIndex(arg, param);
                if (value == null)
                {
                    builder.Append(MySubString(formatMsg, offset, end + 1));
                }
                else
                {
                    builder.Append(MySubString(formatMsg, offset, start));
                    builder.Append(value);
                }
                offset = end + 1;
            }
            if (offset < formatMsg.Length)
            {
                string last = formatMsg.Substring(offset);
                builder.Append(last);
            }
            return builder.ToString();
        }

        public static String[] FormatPoseString(String input)
        {
            int offset = 0;

     
            int startIndex = input.IndexOf('p') + 1;
            input.Substring(startIndex);

            int brackStart = input.IndexOf('[') + 1;
            input.Substring(brackStart);

            int brackEnd = input.IndexOf(']');
            input = MySubString(input, brackStart, brackEnd);

            return input.Split(','); // string array containing coordinate strings
        }



        private static string GetArgIndex(string arg, params Object[] param)
        {
            int argNumber;
            try
            {
                argNumber = Convert.ToInt32(arg);
            }
            catch (FormatException e)
            {
                return null;
            }
            return argNumber < 0 || argNumber >= param.Length ? null : ObjectToString(param[argNumber]);
        }

        private static string ObjectToString(Object obj)
        {
            obj.GetType();
            if (obj == null) return "<null>";
            if (obj.GetType().IsArray) return ListToString((Object[])obj);
            
            return obj.ToString();
        }


    }
}
