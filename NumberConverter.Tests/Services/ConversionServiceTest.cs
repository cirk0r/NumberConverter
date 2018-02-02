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
using NumberConverter.Service.Logic;
using NumberConverter.ViewModel.Models;

namespace NumberConverter.Tests.Services
{
    [TestClass]
    public class ConversionServiceTest
    {
        private Mock<IConversionLogic> _conversionLogicMock;
        private IConversionService _conversionService;

        private void Prepare(decimal initialValue)
        {
            _conversionLogicMock = new Mock<IConversionLogic>();
            ConversionModel conversionModel = new ConversionModel() { Value = initialValue };
            _conversionLogicMock.Setup(x => x.ConversionModel).Returns(conversionModel);
            _conversionLogicMock.Setup(x => x.PrepareDecimalForConversion(It.IsAny<decimal>())).Callback((decimal value) =>
            {
                if (value < 0)
                    return;

                _conversionLogicMock.Object.ConversionModel.Value = value;

                var twoPartDecimalArray = _conversionLogicMock.Object.ConversionModel.Value.ToString().Split(char.Parse(NumberFormatInfo.CurrentInfo.NumberDecimalSeparator));

                _conversionLogicMock.Object.ConversionModel.IntegerPart = Convert.ToInt64(twoPartDecimalArray?.First());
                _conversionLogicMock.Object.ConversionModel.DecimalPart = twoPartDecimalArray?.Length > 1
                    ? Convert.ToInt32(_conversionLogicMock.Object.ConversionModel.Value.ToString().Split(char.Parse(NumberFormatInfo.CurrentInfo.NumberDecimalSeparator))?.Last())
                    : -1;
                _conversionLogicMock.Object.ConversionModel.IsDecimalPartEqualZero = _conversionLogicMock.Object.ConversionModel.DecimalPart <= 0;
                _conversionLogicMock.Object.ConversionModel.IntegerPartSplitted = _conversionLogicMock.Object.ConversionModel.IntegerPart.ToString().Split(StringExtensions.DefaultChunkSize);

                if (_conversionLogicMock.Object.ConversionModel.PartsConversions == null)
                    _conversionLogicMock.Object.ConversionModel.PartsConversions = new List<string>();

                for (int i = 0; i < _conversionLogicMock.Object.ConversionModel.IntegerPartSplitted.Length; i++)
                {
                    _conversionLogicMock.Object.ConversionModel.PartsConversions.Add(ConversionLogic.ConvertPartOfNumber(_conversionLogicMock.Object.ConversionModel.IntegerPartSplitted[i], _conversionLogicMock.Object.ConversionModel.IntegerPartSplitted.Length - i - 1));
                }

                _conversionLogicMock.Object.ConversionModel.PartsConversions.RemoveAll(string.IsNullOrWhiteSpace);
            });

            _conversionService = new ConversionService(_conversionLogicMock.Object);
        }

        [TestMethod]
        public void ConvertTest_NegativeNumberIn()
        {
            decimal value = -1.23M;
            Prepare(value);

            var conversionResult = _conversionService.Convert(value);

            Assert.AreEqual(string.Empty, conversionResult);
        }

        [TestMethod]
        public void ConvertTest_PositiveNumberIn()
        {
            decimal value = 10000001.2M;
            Prepare(value);
            
            string expectedConversion = "Ten Million One and 2 / 10";

            var conversionResult = _conversionService.Convert(value);

            Assert.AreEqual(expectedConversion, conversionResult);
        }
    }
}
