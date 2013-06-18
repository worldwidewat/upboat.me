using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace UpboatMe.Utilities
{
    public static class StringExtensionMethods
    {
        public static string Sanitize(this string text)
        {
            if (text == null)
            {
                return string.Empty;
            }

            return text.Replace('-', ' ');
        }

        public static List<string> GetLines(this string text, int margin)
        {
            int start = 0, end;
            var lines = new List<string>();

            while ((end = start + margin) < text.Length)
            {
                while (text[end] != ' ' && end > start)
                {
                    end -= 1;
                }

                if (end == start)
                {
                    end = start + margin;
                }

                lines.Add(text.Substring(start, end - start));
                start = end + 1;
            }

            if (start < text.Length)
            {
                lines.Add(text.Substring(start));
            }

            return lines;
        }

        public static string AlphaOnly(this string text)
        {
            var builder = new StringBuilder();

            foreach (char c in text)
            {
                if (Char.IsLetter(c))
                {
                    builder.Append(c);
                }
            }

            return builder.ToString();
        }
    }
}