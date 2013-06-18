using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UpboatMe.Utilities
{
    public static class StringExtensionMethods
    {
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
    }
}