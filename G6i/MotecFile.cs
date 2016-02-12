using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace G6i
{
    class MotecFile
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

        private static bool FileIsValid(string path)
        {
            return true;
        }
    }

    public class ConstantValues
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public string Unit { get; set; }

        public XElement ToXElement()
        {
            var top = new XElement(this.Name);
            top.Add(new XElement("Value", this.Value));
            top.Add(new XElement("Unit", this.Unit));

            return top;
        }
    }
}
