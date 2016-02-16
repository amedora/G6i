using System;
using System.Xml.Linq;
using G6i;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestG6i
{
    [TestClass]
    public class TestConstantValues
    {
        [TestMethod]
        public void ConvertProperly()
        {
            var cv = new ConstantValues("LFShockDeflAtGarage", 7.62);
            var xl = cv.ToXElement();
            Assert.AreEqual("in", xl.Attribute("Unit").Value);
            Assert.AreEqual("7.620000", xl.Attribute("Value").Value);
            Assert.AreEqual("2", xl.Attribute("DPS").Value);

            var cv2 = new ConstantValues("LFCornerWeight", 888);
            var xl2 = cv2.ToXElement();
            Assert.AreEqual("lbs", xl2.Attribute("Unit").Value);
            Assert.AreEqual("888.000000", xl2.Attribute("Value").Value);
            Assert.AreEqual("0", xl2.Attribute("DPS").Value);
        }
    }
}
