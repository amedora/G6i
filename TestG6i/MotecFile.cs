using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using G6i;

namespace TestG6i
{
    [TestClass]
    public class TestMotecFile
    {
        [TestMethod]
        public void ReturnConstantValuesNodeProperly()
        {
            var motec = MotecFile.Load(Path.GetFullPath(@"../../sample/sample-already.ldx");
        }
    }
}
