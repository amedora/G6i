using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Xml.XPath;
using G6i;

namespace TestG6i
{
    [TestClass]
    public class TestSetupParser
    {
        [TestMethod]
        public void LoadProperly()
        {
            var setup = SetupParser.Load(@"../../sample/charlotte_quadoval.htm");

            var value = setup.GetValue(Components.LFCornerWeight); // /html/body/u[40]
            value.Is(862);

            value = setup.GetValue(Components.LFShockDeflAtGarage);
            value.Is(7.22);

            value = setup.GetValue(Components.LFShockDeflAvailable);
            value.Is(11.00);

            value = setup.GetValue(Components.LFSpringRate);
            value.Is(425);
        }

        [TestMethod]
        public void ListProperly()
        {
            var setup = SetupParser.Load(@"../../sample/charlotte_quadoval.htm");
            var list = setup.ListConstantValues();

            list.Count.Is(18);
        }
    }
}
