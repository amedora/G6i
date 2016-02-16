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
            xl.Attribute("Unit").Value.Is("in");
            xl.Attribute("Value").Value.Is("7.620000");
            xl.Attribute("DPS").Value.Is("2");

            var cv2 = new ConstantValues("LFCornerWeight", 888);
            var xl2 = cv2.ToXElement();
            xl2.Attribute("Unit").Value.Is("lbs");
            xl2.Attribute("Value").Value.Is("888.000000");
            xl2.Attribute("DPS").Value.Is("0");
        }
    }
}
