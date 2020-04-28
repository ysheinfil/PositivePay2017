using System;
using BankFileLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BankFileTestProject
{
    [TestClass]
    public class UtilitiesTest  
    {
        [TestMethod]
        public void InvalidInput_DateToStringTest()
        {
            string dateToTest = "abcdef";
            string dateForFile = Utilities.YYYMMDDString(dateToTest);
            Assert.AreEqual(string.Empty, dateForFile);
        }

        [TestMethod]
        public void InvalidDate_DateToStringTest()
        {
            string dateToTest = "10/32/1998";
            string dateForFile = Utilities.YYYMMDDString(dateToTest);
            Assert.AreEqual(string.Empty, dateForFile);
        }

        [TestMethod]
        public void Invalid_Feb29_DateToStringTest()
        {
            string dateToTest = "29-Feb-2001";
            string dateForFile = Utilities.YYYMMDDString(dateToTest);
            string expected = string.Empty;
            Assert.AreEqual(expected, dateForFile);
        }

        [TestMethod]
        public void Valid_DDMMMYYYY_DateToStringTest()
        {
            string dateToTest = "21-Sep-2010";
            string dateForFile = Utilities.YYYMMDDString(dateToTest);
            string expected = "20100921";
            Assert.AreEqual(expected, dateForFile);
        }

        [TestMethod]
        public void Valid_MMDDYYYY_DateToStringTest()
        {
            string dateToTest = "09/21/2010";
            string dateForFile = Utilities.YYYMMDDString(dateToTest);
            string expected = "20100921";
            Assert.AreEqual(expected, dateForFile);
        }
        [TestMethod]
        public void SingleDigit_MMDDYYYY_DateToStringTest()
        {
            string dateToTest = "09/01/2010";
            string dateForFile = Utilities.YYYMMDDString(dateToTest);
            string expected = "20100901";
            Assert.AreEqual(expected, dateForFile);
        }

        [TestMethod]
        public void SingleDigit_AmericanDateTest()
        {
            string dateToTest = "2001-01-09";
            string expected = "01/09/2001";
            string actual = Utilities.AmericanDate(dateToTest);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Valid_Feb29_DateToStringTest()
        {
            string dateToTest = "29-Feb-2004";
            string dateForFile = Utilities.YYYMMDDString(dateToTest);
            string expected = "20040229";
            Assert.AreEqual(expected, dateForFile);
        }

        [TestMethod]
        public void MMDDYYStringTest()
        {
            string dateToTest = "2012-03-01";
            string expected = "030112";
            string actual = Utilities.MMDDYYString(dateToTest);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ThrownException_IntegerToString()
        {
            int tooLarge = 321987;
            string actual = Utilities.IntegerToString(tooLarge, 4);
            Assert.Fail("Expected ArgumentOutOfRangeException");
        }

        [TestMethod]
        public void Valid_IntegerToString()
        {
            int number = 45677;
            string actual = Utilities.IntegerToString(number, 8);
            string expected = "00045677";
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void ValidExact_IntegerToString()
        {
            int number = 45677;
            string actual = Utilities.IntegerToString(number, 5);
            string expected = "45677";
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ThrownException_LeftJustifiedField()
        {
            int tooLarge = 321987;
            string actual = Utilities.LeftJustifiedField(tooLarge.ToString(), 4);
            Assert.Fail("Expected ArgumentOutOfRangeException");
        }

        [TestMethod]
        public void Valid_LeftJustifiedField()
        {
            string myName = "Jerry";
            string expected = myName + "     ";
            string actual = Utilities.LeftJustifiedField(myName, 10);
            Assert.AreEqual(expected, actual);

        }
        [TestMethod]
        public void ValidExact_LeftJustifiedField()
        {
            string myName = "Harrold";
            
            string actual = Utilities.LeftJustifiedField(myName, myName.Length);
            Assert.AreEqual(myName, actual);
        }

        [TestMethod]
        public void Valid_LeftFillZeros()
        {
            string accountNumber = "8765438";
            string expected = "000" + accountNumber;
            string actual = Utilities.LeftFillZeros(accountNumber, 10);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DecimalToStringTest()
        {
            decimal testNumber = 98765.543M;
            string expected = "0009876554";
            string actual = Utilities.DecimalToString(testNumber, 10);
            Assert.AreEqual(expected, actual);

        }
    }
}
