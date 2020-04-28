using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using BankFileLibrary;
using log4net;
using Test.Helpers;

namespace BankFileTestProject
{
    [TestClass]
    public class FacilityFactoryTest
    {
        [TestMethod]
        public void ParseFileNameTest()
        {
            string fullFileName = string.Empty, bankName = string.Empty, acctNo = string.Empty, facName = string.Empty;
            fullFileName = "Facname_Fac4code-bank-456789_14-Nov-2018_1234.csv";
            FacilityFactory.ParseFileName(fullFileName, ref bankName, ref acctNo, ref facName);
            Assert.AreEqual("bank", bankName);
            Assert.AreEqual("456789", acctNo);
            Assert.AreEqual("Facname", facName);
        }

        [TestMethod]
        public void ParseOldFileNameTest()
        {
            string fullFileName = string.Empty, bankName = string.Empty, acctNo = string.Empty, facName = string.Empty;
            fullFileName = "Facname_bank-456789_14-Nov-2018_1234.csv";
            FacilityFactory.ParseFileName(fullFileName, ref bankName, ref acctNo, ref facName);
            Assert.AreEqual("bank", bankName);
            Assert.AreEqual("456789", acctNo);
            Assert.AreEqual("Facname", facName);
        }

        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", 
            @"c:\testdata\input\positivepaytestdata.csv", "positivepaytestdata#csv", 
            DataAccessMethod.Sequential )]
        public void ProcessFileTest()
        {
            IFacility Facility;
            FacilityFactory.Log = LogManager.GetLogger("TestingLog");

            string inputFile = TestContext.DataRow["inputFileName"].ToString();
            string outputFileName = TestContext.DataRow["outputFileName"].ToString();

            Facility = FacilityFactory.ProcessFile(inputFile);
            string actualOutputFile = Facility.OutputFileName;

            FileAssert.AreEqual(outputFileName, actualOutputFile);

        }
        
        private TestContext _testContext;
        public TestContext TestContext
        {  get
            {
                return _testContext;
            }
            set
            {
                _testContext = value;
            }
        }
    }
}
