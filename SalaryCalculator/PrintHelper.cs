using System;
using System.Collections.Generic;

namespace SalaryCalculator
{
    public static class PrintHelper
    {
        private static int _tableWidth => Console.WindowWidth;

        public static void PrintLine()
        {
            Console.WriteLine(new string('-', _tableWidth));
        }

        public static void PrintRow(params string[] columns)
        {
            int width = (_tableWidth - columns.Length) / columns.Length - 1;
            string row = "|";

            foreach (string column in columns)
            {
                row += AlignCentre(column, width) + "|";
            }

            Console.WriteLine(row);
        }

        public static void PrintTable<T>(
             IEnumerable<T> values,
             string[] columnHeaders,
             params Func<T, object>[] valueSelectors)
        {
            PrintLine();
            PrintRow(columnHeaders);
            PrintLine();
            foreach (var value in values)
            {
                List<string> selectorsResult = new List<string>();
                foreach (var selector in valueSelectors)
                {
                    selectorsResult.Add(selector(value).ToString());
                }
                PrintRow(selectorsResult.ToArray());
            }
            PrintLine();
            Console.WriteLine();
        }

        public static void PrintTitle(string title)
        {
            PrintLine();
            PrintRow(title);
            PrintLine();
        }

        private static string AlignCentre(string text, int width)
        {
            text = text.Length > width ? text.Substring(0, width - 3) + "..." : text;

            if (string.IsNullOrEmpty(text))
                return new string(' ', width);
            else
                return text.PadRight(width - (width - text.Length) / 2).PadLeft(width);
        }
    }
}
