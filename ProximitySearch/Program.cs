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
                ValidationService.ValidateNumberOfArgumentsProvided(args);
                var processCommandService = new ProcessCommandsService();
                var keyWordsOccurenceWithinRange = processCommandService.Process(args[0], args[1], args[2], args[3]);
                Console.WriteLine(keyWordsOccurenceWithinRange);
            }
            catch (InputValidationException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception)
            {
                Console.WriteLine("unknown error occured");
            }
        }
    }
}
