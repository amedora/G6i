using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using System.Xml.XPath;

namespace G6i
{
    public class MotecFile
    {
        public XDocument XDoc { get; private set; }
        public string Path { get; private set; }
        private MotecFile() { }

        public static MotecFile Load(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException("File not found.", path);
            }

            if (!FileIsValid(path))
            {
                throw new ArgumentException("Invalid Motec file.");
            }
            return new MotecFile { XDoc = XDocument.Load(path, LoadOptions.None), Path = path };
        }

        public void Save(bool withBackup = false)
        {
            if (withBackup)
            {
                File.Copy(this.Path, this.Path + ".backup");
            }
            this.XDoc.Save(this.Path, SaveOptions.None);
        }

        public void AddConstantValues(List<ConstantValues> constList)
        {
            if (!(this.XDoc is XDocument))
            {
                throw new ApplicationException("Motec file has not loaded.");
            }

            var targetElement = "XXX";
            var targetNode = this.XDoc.Descendants(targetElement).First();

            foreach(var c in constList)
            {
                targetNode.Add(c.ToXElement());
            }
        }

        private XElement ConstantValuesRoot()
        {
            return this.XDoc.XPathSelectElement("/LDXFile/Layers/Details");
        }

        private static bool FileIsValid(string path)
        {
            return true;
        }
    }

    public class ConstantValues
    {
        public string Name { get; set; }
        public double Value { get; set; }
        public string Unit { get; set; }
        public string DPS { get; set; }

        public ConstantValues(string name, double value)
        {
            this.Name = name;
            this.Value = value;
            this.Unit = this.GetUnit();
            this.DPS = this.GetDPS();
        }

        public XElement ToXElement()
        {
            var top = new XElement("Numeric");
            top.Add(new XAttribute("Id", this.Name));
            top.Add(new XAttribute("Value", this.Value.ToString("F6")));
            top.Add(new XAttribute("Unit", this.GetUnit()));
            top.Add(new XAttribute("DPS", this.GetDPS()));

            return top;
        }

        private string GetUnit()
        {
            if (Regex.IsMatch(this.Name, @"\w\wShockDefl.*"))
            {
                return "in";
            }
            else if (Regex.IsMatch(this.Name, @"\w\wCornerWeight"))
            {
                return "lb";
            }
            else
            {
                return null;
            }
        }

        private string GetDPS()
        {
            if (Regex.IsMatch(this.Name, @"\w\wShockDefl.*"))
            {
                return "2";
            }
            else if (Regex.IsMatch(this.Name, @"\w\wCornerWeight"))
            {
                return "0";
            }
            else
            {
                return null;
            }
        }
    }
}
