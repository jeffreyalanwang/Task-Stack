using System;
using System.Collections.Generic;
using System.Text;

namespace Task_Stack.State
{
    public static class State_Serialization
    {
        public static string SerializeDictionary(Dictionary<string, string> dict)
        {
            StringBuilder inner = new StringBuilder();
            foreach (KeyValuePair<string, string> kvp in dict)
            {
                inner.Append( SanitizeString(kvp.Key) );
                inner.Append( ':' );
                inner.Append( SanitizeString(kvp.Value) );
                inner.Append( ',' );
            }
            return  "{" + inner.ToString() + "}";
        }
        public static string SerializeList(List<string> list)
        {
            StringBuilder inner = new StringBuilder();
            foreach (string str in list)
            {
                inner.Append( SanitizeString(str) );
                inner.Append( ',' );
            }
            return "[" + inner.ToString() + "]";
        }
        public static string SerializeBool(bool value)
        {
            return value ? "true" : "false";
        }
        public static Dictionary<string, string> UnserializeDictionary(string str)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();

            str = str.Trim();
            if ( !str.StartsWith("{") || !str.EndsWith("}") )
            {
                string reason = "String is not enclosesd in curly braces {}.";
                throw new SyntaxException($"Cannot deserialize value to a Dictionary. {reason} Value: {str}");
            }
            else
            {
                str = str.Substring(1, str.Length - 2);
            }

            str = str.Trim();
            bool expectingMore = true; // false when comma does not indicate a next value
            while (str.Length > 0)
            {
                string keyStr, valStr;

                if (!expectingMore)
                {
                    string reason = "Missing comma ,.";
                    throw new SyntaxException($"Cannot deserialize value to a Dictionary. {reason} Value: {str}");
                }

                (keyStr, str) = UnsanitizeFirstString(str);

                str = str.TrimStart();
                if ( str.StartsWith(":") )
                {
                    str = str.Substring(1);
                }
                else
                {
                    string reason = "Missing colon :.";
                    throw new SyntaxException($"Cannot deserialize value to a Dictionary. {reason} Value: {str}");
                }

                str = str.TrimStart();
                (valStr, str) = UnsanitizeFirstString(str);
                
                str = str.TrimStart();
                if (str.StartsWith(","))
                {
                    str = str.Substring(1);
                    str = str.TrimStart();
                }
                else
                {
                    expectingMore = false;
                }

                dict.Add( keyStr, valStr );
            }

            return dict;
        }
        public static List<string> UnserializeList(string str)
        {
            List<string> list = new List<string>();

            str = str.Trim();
            if (!str.StartsWith("[") || !str.EndsWith("]"))
            {
                string reason = "String is not enclosesd in square brackets [].";
                throw new SyntaxException($"Cannot deserialize value to a List. {reason} Value: {str}");
            }
            else
            {
                str = str.Substring(1, str.Length - 2);
            }

            str = str.TrimStart();
            bool expectingMore = true;
            while (str.Length > 0)
            {
                string valStr;

                if (!expectingMore)
                {
                    string reason = "Missing comma ,.";
                    throw new SyntaxException($"Cannot deserialize value to a List. {reason} Value: {str}");
                }

                str = str.TrimStart();
                (valStr, str) = UnsanitizeFirstString(str);

                str = str.TrimStart();
                if (str.StartsWith(","))
                {
                    str = str.Substring(1);
                    str = str.TrimStart();
                }
                else
                {
                    expectingMore = false;
                }

                list.Add(valStr);
            }

            return list;
        }
        public static bool UnserializeBool(string str)
        {
            switch (str) {
                case "true": return true;
                case "false": return false;
                default: throw new SyntaxException($"Cannot deserialize value to a bool: {str}");
            }
        }
        private static string SanitizeString(string str)
        {
            str = str.Replace("\\", "\\\\");
            str = str.Replace("\"", "\\\"");
            return '\"' + str + '\"';
        }
        private static (string firstRawString, string remainingString) UnsanitizeFirstString(string str)
        {
            string original = str;
            str = str.TrimStart();
            if (!str.StartsWith("\"")) throw new SyntaxException("Not a sanitized string (no quote at beginning \"): " + original);
            else str = str.Substring(1);

            // Is char at index i the closing quote?
            int closingQuoteIndex = -1;
            for (int i = 0; i < str.Length; i++)
            {
                // * must be a quote '\"'
                if (str[i] != '"') continue;
                // * if preceded by a backslash, '\\', must be preceded by an even number of backslashes (e.g. "\\\\")
                if (i > 0)
                {
                    int backslashCount = 0;
                    for (int j = i - 1; j >= 0; j--)
                    {
                        if (str[j] != '\\') break;
                        else backslashCount++;
                    }
                    if (backslashCount % 2 == 1) continue; // odd number of preceding backslashes, so this quote was escaped
                }
                // if no continue keyword: we found the closing quote
                closingQuoteIndex = i;
                break;
            }

            if (closingQuoteIndex < 0) throw new SyntaxException("Could not unsanitize string (no closing quote \"): " + original);

            // Unsanitize escape sequences
            string firstRawString = str.Substring(0, closingQuoteIndex);
            firstRawString = firstRawString.Replace("\\\\", "\\");
            firstRawString = firstRawString.Replace("\\\"", "\"");

            string remainingString = str.Substring(1 + closingQuoteIndex);
            return (firstRawString: firstRawString, remainingString: firstRawString);
        }

        [Serializable]
        public class SyntaxException : Exception
        {
            public SyntaxException()
            { }

            public SyntaxException(string message)
                : base(message)
            { }

            public SyntaxException(string message, Exception innerException)
                : base(message, innerException)
            { }
        }
    }
}
