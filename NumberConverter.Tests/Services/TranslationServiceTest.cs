using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NumberConverter.Service.Abstract;
using NumberConverter.Service.Concrete;
using NumberConverter.Service.Extensions;
using NumberConverter.Service.Helpers;
using NumberConverter.ViewModel.Models;

namespace NumberConverter.Tests.Services
{
    [TestClass]
    public class TranslationServiceTest
    {
        private Mock<ITranslationLogic> _translationLogicMock;
        private ITranslationService _translationService;

        private void Prepare(decimal initialValue)
        {
            _translationLogicMock = new Mock<ITranslationLogic>();
            TranslationModel translationModel = new TranslationModel() { Value = initialValue };
            _translationLogicMock.Setup(x => x.TranslationModel).Returns(translationModel);
            _translationLogicMock.Setup(x => x.ProcessDecimal(It.IsAny<decimal>())).Callback((decimal value) =>
            {
                if (value < 0)
                    return;

                _translationLogicMock.Object.TranslationModel.Value = value;

                var twoPartDecimalArray = _translationLogicMock.Object.TranslationModel.Value.ToString().Split(char.Parse(NumberFormatInfo.CurrentInfo.NumberDecimalSeparator));

                _translationLogicMock.Object.TranslationModel.IntegerPart = Convert.ToInt64(twoPartDecimalArray?.First());
                _translationLogicMock.Object.TranslationModel.DecimalPart = twoPartDecimalArray?.Length > 1
                    ? Convert.ToInt32(_translationLogicMock.Object.TranslationModel.Value.ToString().Split(char.Parse(NumberFormatInfo.CurrentInfo.NumberDecimalSeparator))?.Last())
                    : -1;
                _translationLogicMock.Object.TranslationModel.IsDecimalPartEqualZero = _translationLogicMock.Object.TranslationModel.DecimalPart <= 0;
                _translationLogicMock.Object.TranslationModel.IntegerPartSplitted = _translationLogicMock.Object.TranslationModel.IntegerPart.ToString().SplitToParts();

                if (_translationLogicMock.Object.TranslationModel.PartsTranslations == null)
                    _translationLogicMock.Object.TranslationModel.PartsTranslations = new List<string>();

                for (int i = 0; i < _translationLogicMock.Object.TranslationModel.IntegerPartSplitted.Length; i++)
                {
                    _translationLogicMock.Object.TranslationModel.PartsTranslations.Add(TranslationLogic.TranslatePartOfNumber(_translationLogicMock.Object.TranslationModel.IntegerPartSplitted[i], _translationLogicMock.Object.TranslationModel.IntegerPartSplitted.Length - i - 1));
                }
            });

            _translationService = new TranslationService(_translationLogicMock.Object);
        }

        [TestMethod]
        public void TranslateTest_NegativeNumberIn()
        {
            decimal value = -1.23M;
            Prepare(value);

            var translationResult = _translationService.Translate(value);

            Assert.AreEqual(string.Empty, translationResult);
        }

        [TestMethod]
        public void TranslateTest_PositiveNumberIn()
        {
            decimal value = 10000001.2M;
            Prepare(value);
            
            string expectedTranslation = "Ten Million One and 2 / 10";

            var translationResult = _translationService.Translate(value);

            Assert.AreEqual(expectedTranslation, translationResult);
        }
    }
}
