using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProximitySearch.Services;
using ProximitySearch.Services.Exceptions;

namespace ProximitySearch.Tests
{
    [TestClass]
    public class ProximitySearchTests
    {
        [TestMethod]
        public void Process_WithNonIntegerRangeValue_ShouldThrowInvalidInputException()
        {
            var processCommands = new ProcessCommandsService();
            Assert.ThrowsException<InputValidationException>(() =>
                processCommands.Process("the", "canal", "range", $"{Environment.CurrentDirectory}\\input.txt"));
        }

        [TestMethod]
        public void Process_WithRangeLessThen2_ShouldThrowInvalidInputException()
        {
            var processCommands = new ProcessCommandsService();
            Assert.ThrowsException<InputValidationException>(() =>
                processCommands.Process("the", "canal", "1", $"{Environment.CurrentDirectory}\\input.txt"));
        }

        [TestMethod]
        public void Process_WithInvalidFilePath_ShouldThrowInvalidInputException()
        {
            var processCommands = new ProcessCommandsService();
            Assert.ThrowsException<InputValidationException>(() =>
                processCommands.Process("the", "canal", "range", $"{Environment.CurrentDirectory}\\NonExistingFile.txt"));
        }

        [TestMethod]
        public void Process_WithInvalidKeyWords_ShouldReturnZero()
        {
            var processCommands = new ProcessCommandsService();
            var result = processCommands.Process("invalid1", "invalid2", "6", $"{Environment.CurrentDirectory}\\input.txt");
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void Process_WithUpperCaseInput_ShouldReturnOutput()
        {
            var processCommands = new ProcessCommandsService();
            var result = processCommands.Process("The", "CANAL", "6", $"{Environment.CurrentDirectory}\\input.txt");
            Assert.AreEqual(11, result);
        }

        [TestMethod]
        public void Process_WithValidInput_ShouldReturnOutput()
        {
            var processCommands = new ProcessCommandsService();
            var result = processCommands.Process("the", "canal", "6", $"{Environment.CurrentDirectory}\\input.txt");
            Assert.AreEqual(11, result);
        }
    }
}
