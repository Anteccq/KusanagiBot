using System;
using System.Collections.Generic;
using System.Text;

namespace KusanagiBot
{
    public static class StringExtensions
    {
        public static string[] BisectByBlank(this string str) =>
            str.Split(new char[] {' ', '　'}, 2, StringSplitOptions.RemoveEmptyEntries);
    }
}
