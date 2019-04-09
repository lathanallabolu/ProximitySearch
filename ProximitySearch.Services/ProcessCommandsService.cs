using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ProximitySearch.Services.Exceptions;

namespace ProximitySearch.Services
{
    public class ProcessCommandsService
    {
        private List<int> _keyWord1PositionList;
        private Dictionary<int, int> _keyWord2IndexToOccurenceDictionary;
        private int _keyWordsOccurenceWithinRange;

        public int Process(string keyWord1, string keyWord2, string rangeString, string textFilePath)
        {
            ValidationService.ValidateInputArguments(rangeString, textFilePath);
            var wordsArray = GetWordsFromInputFile(textFilePath);
            var range = int.Parse(rangeString);
            PopulateKeysPositions(wordsArray, keyWord1, keyWord2);
            if (OneOrNoneOfTheKeyPresentInInput()) return _keyWordsOccurenceWithinRange;
            FindKeyWordsOccurenceInGivenRange(range, wordsArray.Length);
            return _keyWordsOccurenceWithinRange;
        }

        private static string[] GetWordsFromInputFile(string textFilePath)
        {
            var lines = File.ReadLines(textFilePath).ToList();
            var stringBuilder = new StringBuilder();
            for (var linesIndex = 0; linesIndex < lines.Count; linesIndex++)
            {
                stringBuilder.Append(lines[linesIndex].TrimEnd());
                if (linesIndex != lines.Count - 1)
                {
                    stringBuilder.Append(' ');
                }
            }
            var textFileContent = stringBuilder.ToString();
            ValidationService.ValidateTextInFile(textFileContent);
            return textFileContent.Split(' ');
        }

        private void PopulateKeysPositions(string[] wordsArray, string keyWord1, string keyWord2)
        {
            _keyWord1PositionList = new List<int>();
            _keyWord2IndexToOccurenceDictionary = new Dictionary<int, int>() { { -1, 0 } };
            var keyWord2Occurence = 0;
            for (var index = 0; index < wordsArray.Length; index++)
            {
                if (string.Equals(wordsArray[index], keyWord1, StringComparison.InvariantCultureIgnoreCase) )
                {
                    _keyWord1PositionList.Add(index);
                }
                else if (string.Equals(wordsArray[index], keyWord2, StringComparison.InvariantCultureIgnoreCase) )
                {
                    keyWord2Occurence++;
                }
                _keyWord2IndexToOccurenceDictionary.Add(index, keyWord2Occurence);
            }
        }

        private void FindKeyWordsOccurenceInGivenRange(int range, int numberOfWordsInInputFile)
        {
            foreach (var keyWord1Position in _keyWord1PositionList)
            { 
                var rangeEnd = keyWord1Position + range - 1; // To find keyword2 occurence after keyword1 position within the range
                var rangeStart = keyWord1Position - range; // To find keyword2 occurence before keyword1 position within the range
                rangeEnd = IsRangeEndBeyondArrayLength(rangeEnd, numberOfWordsInInputFile) ? numberOfWordsInInputFile - 1 : rangeEnd; 
                rangeStart = IsRangeStartBelowZero(rangeStart) ? -1 : rangeStart;
                _keyWordsOccurenceWithinRange += _keyWord2IndexToOccurenceDictionary[rangeEnd] - _keyWord2IndexToOccurenceDictionary[rangeStart];
            }
        }

        private static bool IsRangeStartBelowZero(int rangeStart)
        {
            return rangeStart < 0;
        }

        private static bool IsRangeEndBeyondArrayLength(int rangeEnd, int numberOfWordsInInputFile)
        {
            return rangeEnd >= numberOfWordsInInputFile;
        }

        private bool OneOrNoneOfTheKeyPresentInInput()
        {
            return _keyWord1PositionList.Count <= 0 || _keyWord2IndexToOccurenceDictionary.Last().Value <= 0;
        }
    }
}
