﻿using System.Text.RegularExpressions;

namespace HamedStack.Mustache.Core
{
    internal static class RegexHelper
    {
        public const string Key = @"[_\w][_\w\d]*";
        public const string String = @"'.*?'";
        public const string Number = @"[-+]?\d*\.?\d+";
        public const string CompoundKey = "@?" + Key + @"(?:\." + Key + ")*";
        public const string Argument = @"(?:(?<arg_key>" + CompoundKey + @")|(?<arg_string>" + String + @")|(?<arg_number>" + Number + @"))";

        public static bool IsValidIdentifier(string name)
        {
            if (name == null)
            {
                return false;
            }
            Regex regex = new Regex("^" + Key + "$");
            return regex.IsMatch(name);
        }

        public static bool IsString(string value)
        {
            if (value == null)
            {
                return false;
            }
            Regex regex = new Regex("^" + String + "$");
            return regex.IsMatch(value);
        }

        public static bool IsNumber(string value)
        {
            if (value == null)
            {
                return false;
            }
            Regex regex = new Regex("^" + Number + "$");
            return regex.IsMatch(value);
        }
    }
}
