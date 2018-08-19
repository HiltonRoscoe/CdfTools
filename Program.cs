using System;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Xsl;
using McMaster.Extensions.CommandLineUtils;
namespace CdfValidator
{
    [HelpOption("--my-help")]
    [Command(ExtendedHelpText = "Validates Common Data Format instances.", 
    Name = "cdftools",
    FullName = "Common Data Format Tools" )]
    class Program
    {
        public static int Main(string[] args) => CommandLineApplication.Execute<Program>(args);

        [Option(Template = "-i|--input", Description = "Input instance file")]
        [Required]
        public string Input { get; }

        [Option(Template = "-s|--schema", Description = "Input schema file")]
        [Required]
        public string Schema { get; }

        [Option(Template = "-v|--verbose")]
        public bool Verbose { get; }

        public void OnExecute()
        {
            System.Console.WriteLine("Common Data Format Validation Tools");
            ValidateSchema();
        }

        void ValidateSchema()
        {
            try
            {
                XmlSchemaSet xss = new XmlSchemaSet();
                xss.XmlResolver = new XmlUrlResolver();
                using (XmlReader xmlReader = XmlReader.Create(this.Schema))
                {
                    xss.Add(XmlSchema.Read(xmlReader, null));
                }
                xss.Compile();
                XmlReader rd = XmlReader.Create(this.Input);
                XDocument doc = XDocument.Load(rd);
                doc.Validate(xss, ValidationEventHandler);
                if (this.HasErrors)
                {
                    Console.WriteLine("XML Instance has one or more errors", Color.Red);
                }
                else
                {
                    Console.WriteLine("XML Instance is Valid", Color.Green);
                }
            }
            catch (System.IO.FileNotFoundException ex)
            {
                System.Console.WriteLine(String.Format("Cannot find input file {0}", ex.FileName));
            }
            catch (System.Xml.Schema.XmlSchemaException ex)
            {
                System.Console.WriteLine("Problem processing the XML Schema Definition");
            }
        }
        public bool HasErrors { get; set; }
        void ValidationEventHandler(object sender, ValidationEventArgs e)
        {
            XmlSeverityType type = XmlSeverityType.Warning;
            if (Enum.TryParse<XmlSeverityType>("Error", out type))
            {
                this.HasErrors = true;
                if (type == XmlSeverityType.Error) System.Console.WriteLine(e.Message);
            }
        }
    }
}
