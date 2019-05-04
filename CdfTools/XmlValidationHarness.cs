using System.Collections.Generic;
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
           /* if (errors.Count > 0)
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
            Console.ResetColor();*/
            //TODO: remove short circuit
            return;
        }


        public static List<string> xsd(string inputFile, string schemaFile)
        {
            try
            {
                // TODO: sanitize uris/filenames
                var validator = new XsdValidator();
                return validator.Validate(schemaFile, inputFile);
             
            }
            catch (System.Xml.Schema.XmlSchemaException ex)
            {
                throw new System.Exception("Problem processing the XML Schema Definition", ex);                        
            }
        }
    }
}