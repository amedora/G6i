using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using G6i;
using System.Collections.Generic;

namespace TestG6i
{
    [TestClass]
    public class TestMotecFile
    {
        [TestMethod]
        public void ReturnConstantValuesNodeProperly()
        {
            var motec = MotecFile.Load(Path.GetFullPath(@"../../sample/sample-alreadyset.ldx"));
            XElement element = motec.AsDynamic().ConstantValuesRoot();
            element.Name.Is("Details");
        }

        [TestMethod]
        [Ignore]
        public void ReplaceConstantValue()
        {
            var motec = MotecFile.Load(Path.GetFullPath(@"../../sample/sample-alreadyset.ldx"));

            motec.AddConstantValues(new List<ConstantValues> {
                new ConstantValues("LFShockDeflAtGarage", 1.23),
            });

            motec.Save(withBackup: true);
        }
    }
}
