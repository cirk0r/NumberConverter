using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NumberConverter.Service.Helpers;

namespace NumberConverter.Tests.Services
{
    [TestClass]
    public class TranslationLogicTest
    {
        [TestMethod]
        public void ProcessDecimalTest_NegativeNumberIn()
        {
            decimal value = -1.1M;

            var translationLogic = new TranslationLogic();

            translationLogic.ProcessDecimal(value);

            Assert.IsNull(translationLogic.TranslationModel.IntegerPartSplitted);
            Assert.IsNull(translationLogic.TranslationModel.PartsTranslations);
            Assert.AreEqual(0, translationLogic.TranslationModel.DecimalPart);
            Assert.AreEqual(0, translationLogic.TranslationModel.IntegerPart);
        }

        [TestMethod]
        public void ProcessDecimalTest_PositiveNumberIn()
        {
            decimal value = 12345.123M;
            var expectedSplittedIntegerPart = new string[] {"12", "345"};
            var expectedPartsTranslations = new List<string>();
            expectedPartsTranslations.Add("twelve thousand");
            expectedPartsTranslations.Add("three hundred fourty five");

            var translationLogic = new TranslationLogic();

            translationLogic.ProcessDecimal(value);

            Assert.IsNotNull(translationLogic.TranslationModel.IntegerPartSplitted);
            Assert.AreEqual(expectedSplittedIntegerPart.Length, translationLogic.TranslationModel.IntegerPartSplitted.Length);
            for (int i = 0; i < expectedSplittedIntegerPart.Length; i++)
            {
                Assert.AreEqual(expectedSplittedIntegerPart[i], translationLogic.TranslationModel.IntegerPartSplitted[i]);    
            }
            
            Assert.IsNotNull(translationLogic.TranslationModel.PartsTranslations);
            Assert.AreEqual(expectedPartsTranslations.Count, translationLogic.TranslationModel.PartsTranslations.Count);

            for (int i = 0; i < expectedPartsTranslations.Count; i++)
            {
                Assert.AreEqual(expectedPartsTranslations[i], translationLogic.TranslationModel.PartsTranslations[i]);
            }

            Assert.AreEqual(12345, translationLogic.TranslationModel.IntegerPart);
            Assert.AreEqual(123, translationLogic.TranslationModel.DecimalPart);
        }
    }
}
