using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProximitySearch.Services;
using ProximitySearch.Services.Exceptions;

namespace ProximitySearch
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Please enter input as KeyWord1 KeyWord2 Range and input file path");
                var input = Console.ReadLine();
                var inputArray = input.Split(' ');
                ValidationService.ValidateNumberOfArgumentsProvided(inputArray);
                var processCommandService = new ProcessCommandsService();
                var keyWordsOccurenceWithinRange = processCommandService.Process(inputArray[0], inputArray[1], inputArray[2], inputArray[3]);
                Console.WriteLine(keyWordsOccurenceWithinRange);
                Main(new string[0]);
            }
            catch (InputValidationException ex)
            {
                Console.WriteLine(ex.Message);
                Main(new string[0]);
            }
            catch (Exception ex)
            {
                Console.WriteLine("unknown error occured");
                Main(new string[0]);
            }
        }
    }
}
