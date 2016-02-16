using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.XPath;
using Sgml;

namespace G6i
{
    public class SetupParser
    {
        public string Path { get; private set; }
        public XDocument HTML { get; private set; }

        private Regex _regValue = new Regex(@"[0-9.]+");

        private SetupParser() { }

        public static SetupParser Load(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException("File not found.", path);
            }

            SetupParser parser;
            using (var sr = new StreamReader(path))
            {
                var str = sr.ReadToEnd().Replace("<br>", "");
                parser = new SetupParser
                {
                    HTML = ParseHtml(new StringReader(str)),
                    Path = path,
                };
            }

            return parser;
        }

        public double GetValue(Components component)
        {
            var value = this.HTML.XPathSelectElement(component.XPath()).Value;

            return Double.Parse(_regValue.Match(value).Value);
        }

        static XDocument ParseHtml(TextReader reader)
        {
            using (var sgmlReader = new SgmlReader { DocType = "HTML", CaseFolding = CaseFolding.ToLower })
            {
                sgmlReader.InputStream = reader;
                return XDocument.Load(sgmlReader);
            }
        }
    }

    public enum Components
    {
        LFShockDeflAtGarage,
        LRShockDeflAtGarage,
        RFShockDeflAtGarage,
        RRShockDeflAtGarage,
        LFShockDeflAvailable,
        LRShockDeflAvailable,
        RFShockDeflAvailable,
        RRShockDeflAvailable,
        LFCornerWeight,
        LRCornerWeight,
        RFCornerWeight,
        RRCornerWeight,
        LFSpringRate,
        LRSpringRate,
        RFSpringRate,
        RRSpringRate,
        LFShockSpringRate,
        RFShockSpringRate,
    }

    public static class ComponentsExtension
    {
        public static string Name(this Components component)
        {
			string[] names =
			{
				"LFShockDeflAtGarage",
                "LRShockDeflAtGarage",
                "RFShockDeflAtGarage",
                "RRShockDeflAtGarage",
                "LFShockDeflAvailable",
                "LRShockDeflAvailable",
                "RFShockDeflAvailable",
                "RRShockDeflAvailable",
                "LFCornerWeight",
                "LRCornerWeight",
                "RFCornerWeight",
                "RRCornerWeight",
                "LFSpringRate",
                "LRSpringRate",
                "RFSpringRate",
                "RRSpringRate",
                "LFShockSpringRate",
                "RFShockSpringRate",
			};

            return names[(int)component];
        }

        public static string XPath(this Components component)
        {
            string[] xpaths =
            {
                "//html/body/u[42]",        // LFShockDeflAtGarage
                "//html/body/u[57]",        //"LRShockDeflAtGarage",
                "//html/body/u[75]",        //"RFShockDeflAtGarage",
                "//html/body/u[90]",        //"RRShockDeflAtGarage",
                "//html/body/u[43]",        //"LFShockDeflAvailable",
                "//html/body/u[58]",        //"LRShockDeflAvailable",
                "//html/body/u[76]",        //"RFShockDeflAvailable",
                "//html/body/u[91]",        //"RRShockDeflAvailable",
                "//html/body/u[40]",
                "//html/body/u[55]",        //"LRCornerWeight",
                "//html/body/u[73]",        //"RFCornerWeight",
                "//html/body/u[88]",        //"RRCornerWeight",
                "//html/body/u[47]",        //"LFSpringRate",
                "//html/body/u[62]",        //"LRSpringRate",
                "//html/body/u[80]",        //"RFSpringRate",
                "//html/body/u[95]",        //"RRSpringRate",
                "//html/body/u[48]",        //"LFShockSpringRate",
                "//html/body/u[81]",        //"RFShockSpringRate",
            };

            return xpaths[(int)component];
        }
    }
}
