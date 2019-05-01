using System;
using System.Xml;
namespace CdfTools
{
    public class XmlValidationHarness
    {
        public static void schematron(string inputFile, string schematronFile)
        {

            System.Console.WriteLine("Invoking Schematron Validation");
            // run schematron logic
            var docToValidate = new XmlDocument();
            docToValidate.Load(inputFile);
            var schematronRulesDoc = new XmlDocument();
            schematronRulesDoc.Load(schematronFile);
            var svrl = SchematronValidator.Validate(schematronRulesDoc, docToValidate);
            var nsmgr = new XmlNamespaceManager(svrl.NameTable);
            nsmgr.AddNamespace("svrl", "http://purl.oclc.org/dsdl/svrl");
            var errors = svrl.SelectNodes("/svrl:schematron-output/svrl:failed-assert", nsmgr);
            if (errors.Count > 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                foreach (XmlNode error in errors)
                {
                    Console.Write(error.InnerText);
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Schematron validation passed");
            }
            Console.ResetColor();
            //TODO: remove short circuit
            return;
        }


        public static void xsd(string inputFile, string schemaFile)
        {
            try
            {
                // TODO: sanitize uris/filenames
                System.Console.WriteLine("Invoking XSD Validation");
                var validator = new XsdValidator();
                var errorList = validator.Validate(schemaFile, inputFile);
                if (errorList.Count > 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    foreach (var error in errorList)
                    {
                        Console.WriteLine(error);
                    }
                    Console.WriteLine("XML Instance has one or more errors");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("XML Instance is Valid");
                }
                Console.ResetColor();
            }
            catch (System.Xml.Schema.XmlSchemaException)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Problem processing the XML Schema Definition");
                // if (Parent.Verbose)
                // {
                //    Console.WriteLine(ex.Message);
                // }
            }
        }
    }
}