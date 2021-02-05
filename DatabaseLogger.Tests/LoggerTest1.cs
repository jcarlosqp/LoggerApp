using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DatabaseLogger;
using LogPluginContract;

namespace DatabaseLogger.Tests
{
    [TestClass]
    public class LoggerTest1
    {
        [TestMethod]
        public void Logger_ValidateRows()
        {
            // Arrange
            DBLogger logger = new DBLogger();

            //Act
            int beforeCount = Service.LogService.CountLogRows();
            logger.Logger("Rows unit testing", MessageTypeEnum.Success);
            int afterCount = Service.LogService.CountLogRows();

            // Assert
            Assert.AreEqual<int>(beforeCount + 1, afterCount);
        }
    }
}
