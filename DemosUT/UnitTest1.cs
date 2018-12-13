using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DennisDemos.Utils;


namespace DemosUT
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestCodeConvertor()
        {
            string customerInput = "string.Format(\"Current time is: {0}\", DateTime.Now)";
            var inputAfterConvert = CodeConvertor.Convertor(customerInput);
            //Assert.IsTrue();
        }
    }
}
