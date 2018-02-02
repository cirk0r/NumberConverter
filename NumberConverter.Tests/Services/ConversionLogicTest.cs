using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NumberConverter.Service.Helpers;

namespace NumberConverter.Tests.Services
{
    [TestClass]
    public class ConversionLogicTest
    {
        [TestMethod]
        public void ProcessDecimalTest_NegativeNumberIn()
        {
            decimal value = -1.1M;

            var conversionLogic = new ConversionLogic();

            conversionLogic.ProcessDecimal(value);

            Assert.IsNull(conversionLogic.ConversionModel.IntegerPartSplitted);
            Assert.IsNull(conversionLogic.ConversionModel.PartsConversions);
            Assert.AreEqual(0, conversionLogic.ConversionModel.DecimalPart);
            Assert.AreEqual(0, conversionLogic.ConversionModel.IntegerPart);
        }

        [TestMethod]
        public void ProcessDecimalTest_PositiveNumberIn()
        {
            decimal value = 12345.123M;
            var expectedSplittedIntegerPart = new string[] {"12", "345"};
            var expectedPartsConversions = new List<string>();
            expectedPartsConversions.Add("twelve thousand");
            expectedPartsConversions.Add("three hundred fourty five");

            var conversionLogic = new ConversionLogic();

            conversionLogic.ProcessDecimal(value);

            Assert.IsNotNull(conversionLogic.ConversionModel.IntegerPartSplitted);
            Assert.AreEqual(expectedSplittedIntegerPart.Length, conversionLogic.ConversionModel.IntegerPartSplitted.Length);
            for (int i = 0; i < expectedSplittedIntegerPart.Length; i++)
            {
                Assert.AreEqual(expectedSplittedIntegerPart[i], conversionLogic.ConversionModel.IntegerPartSplitted[i]);    
            }
            
            Assert.IsNotNull(conversionLogic.ConversionModel.PartsConversions);
            Assert.AreEqual(expectedPartsConversions.Count, conversionLogic.ConversionModel.PartsConversions.Count);

            for (int i = 0; i < expectedPartsConversions.Count; i++)
            {
                Assert.AreEqual(expectedPartsConversions[i], conversionLogic.ConversionModel.PartsConversions[i]);
            }

            Assert.AreEqual(12345, conversionLogic.ConversionModel.IntegerPart);
            Assert.AreEqual(123, conversionLogic.ConversionModel.DecimalPart);
        }
    }
}
