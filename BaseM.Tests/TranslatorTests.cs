using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BaseM.Tests
{
    [TestClass]
    public class TranslatorTests
    {
        [TestMethod]
        [TestCategory("Translator")]
        public void DecimalToDecimalSimple()
        {
            string result = Translator.Translate("100", NumericSystem.Decimal, NumericSystem.Decimal);
            Assert.AreEqual(result, "100");
        }

        [TestMethod]
        [TestCategory("Translator")]
        public void DecimalToDecimalBigNumber()
        {
            string bigNum = "1000000000000000000000000000000000";
            string result = Translator.Translate(bigNum, NumericSystem.Decimal, NumericSystem.Decimal);
            Assert.AreEqual(result, Translator.UNKNOWN);
        }
    }
}
