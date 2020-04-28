using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System;
using System.Collections.Generic;

namespace Test.Helpers
{
    public static class FileAssert
    {
        static string GetFileHash(string filename)
        {
            Assert.IsTrue(File.Exists(filename));

            using (var hash = new SHA1Managed())
            {
                var clearBytes = File.ReadAllBytes(filename);
                var hashedBytes = hash.ComputeHash(clearBytes);
                return ConvertBytesToHex(hashedBytes);
            }
        }

        static string ConvertBytesToHex(byte[] hashedBytes)
        {
            var sb = new StringBuilder();

            for (var i = 0; i < hashedBytes.Length; i++)
            {
                sb.Append(hashedBytes[i].ToString("x"));
            }
            return sb.ToString();
        }

        public static void AreEqual(string expected, string actual)
        {
            //HashMethod(expected, actual);
            DeepMethod(expected, actual);
        }

        private static void DeepMethod(string expected, string actual)
        {
            StringBuilder sbExpected = new StringBuilder();
            StringBuilder sbActual = new StringBuilder();

            using (StreamReader sr = new StreamReader(expected))
            {
                while (!sr.EndOfStream)
                {
                    sbExpected.AppendLine(sr.ReadLine().TrimEnd());
                }
            }

            using (StreamReader sr = new StreamReader(actual))
            {
                while (!sr.EndOfStream)
                {
                    sbActual.AppendLine(sr.ReadLine().TrimEnd());
                }
            }

            var expectedFileAsStrings = sbExpected.ToString().Split('\n');
            var actualFileAsStrings = sbActual.ToString().Split('\n');

            CollectionAssert.AreEqual(expectedFileAsStrings, actualFileAsStrings, actual + " did not match " + expected);
        }

        private static void HashMethod(string expected, string actual)
        {
            string ehash = GetFileHash(expected);
            string ahash = GetFileHash(actual);

            Assert.AreEqual(ehash, ahash);
        }
    }
}
