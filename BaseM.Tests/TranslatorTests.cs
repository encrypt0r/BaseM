using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BaseM.Tests
{
    [TestClass]
    public class TranslatorTests
    {
        [TestMethod]
        public void DecimalToDecimalTest1()
        {
            string result = Translator.Translate("100", NumericSystem.Decimal, NumericSystem.Decimal);
            Assert.AreEqual(result, "100");
        }
    }
}
