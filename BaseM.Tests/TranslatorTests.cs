using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BaseM.Tests
{
    [TestClass]
    public class TranslatorTests
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        [TestCategory("Translator")]
        public void DecimalToDecimalSimple()
        {
            string result = Translator.Translate("100", NumericSystem.Decimal, NumericSystem.Decimal);
            Assert.AreEqual("100", result);
        }

        [TestMethod]
        [TestCategory("Translator")]
        public void DecimalToDecimalBigNumber()
        {
            string bigNum = "1000000000000000000000000000000000";
            string result = Translator.Translate(bigNum, NumericSystem.Decimal, NumericSystem.Decimal);
            Assert.AreEqual(Translator.UNKNOWN, result);
        }

        [TestMethod]
        [TestCategory("Translator")]
        public void DecimalToBinarySimple()
        {
            string number = "25";
            string output = "11001";
            string result = Translator.Translate(number, NumericSystem.Decimal, NumericSystem.Binary);
            Assert.AreEqual(output, result);
        }

        [TestMethod]
        [TestCategory("Translator")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML",
            "|DataDirectory|\\NumberConversionTests.xml", "convert",
            DataAccessMethod.Sequential)]
        public void VigorousTest()
        {
            var number = TestContext.DataRow["number"].ToString();
            var expectedResult = TestContext.DataRow["result"].ToString();
            var from = HelperMethods.StringToNumericSystem(TestContext.DataRow["from"].ToString());
            var to = HelperMethods.StringToNumericSystem(TestContext.DataRow["to"].ToString());

            var result = Translator.Translate(number, from, to);
            Assert.AreEqual(expectedResult, result);
        }

        
    }
}
