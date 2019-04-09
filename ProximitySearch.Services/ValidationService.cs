using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ProximitySearch.Services.Exceptions;

namespace ProximitySearch.Services
{
    public static class ValidationService
    {
        public static void ValidateInputArguments(string range, string textFileName)
        {
            if (!int.TryParse(range, out var rangeNumber))
            {
                throw new InputValidationException("Range should be an Integer number");
            }

            if (rangeNumber < 2)
            {
                throw new InputValidationException("Minimum range should be 2");
            }

            if (!File.Exists(textFileName))
            {
                throw new InputValidationException("File does not exists, please provide valid path or file name as: input.txt");
            }
        }
        public static void ValidateNumberOfArgumentsProvided(string[] inputArguments)
        {
            if (inputArguments.Length != 4)
            {
                throw new InputValidationException("Number of commands should be exactly four");
            }
        }

        public static void ValidateTextInFile(string fileText)
        {
            const string pattern = @"^[a-zA-Z0-9_]+( [a-zA-Z0-9_]+)*$";
            var stringMatch = Regex.Match(fileText, pattern, RegexOptions.IgnoreCase);
            if (!stringMatch.Success) throw new InputValidationException("File should only contain text with one space between words");
        }
    }
}
