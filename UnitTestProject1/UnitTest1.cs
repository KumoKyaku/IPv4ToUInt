using ConvertAnIPv4Address;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        [DataRow("172.168.5.1")]
        [DataRow("172 .168.5.1")]
        [DataRow("172 . 168.5.1")]
        [DataRow("172 . 168.5 .1")]
        [DataRow("172 . 168 .5. 1")]
        public void TestValidInputs(string address)
        {
            Assert.AreEqual(2896692481, ConvertUtility.IPv4ToUInt(address));
        }

        [TestMethod]
        [DataRow("17 2.168.5.1")]
        [DataRow(" 172.168.5.1")]
        [DataRow("172.168.5.1 ")]
        [DataRow("17 2 . 168.5 .1")]
        [DataRow("256.168.5.1")]
        [DataRow("172.168.5.1.1")]
        [ExpectedException(typeof(FormatException))]
        public void TestInvalidInputs(string address)
        {
            ConvertUtility.IPv4ToUInt(address);
        }
    }
}
