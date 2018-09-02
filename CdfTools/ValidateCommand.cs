using System;
using System.ComponentModel.DataAnnotations;
using McMaster.Extensions.CommandLineUtils;
namespace CdfTools
{
    //[HelpOption("--hlp")]
    [Command(ExtendedHelpText = "Validates Common Data Format instances.")]
    class ValidateCommand
    {
        [LegalFilePath]
        [Option(Template = "-i|--input", Description = "Input instance file")]
        [Required]
        public string InputFile { get; }

        [LegalFilePath]
        [Option(Template = "-s|--schema", Description = "Input schema file")]
        [Required]
        public string SchemaFile { get; }

        [LegalFilePath]
        [Option(Template = "-t|--schematron", Description = "Input schematron file")]
        [Required]
        public string SchematronFile { get; }
        // You can use this pattern when the parent command may have options or methods you want to
        // use from sub-commands.
        // This will automatically be set before OnExecute is invoked
        private Program Parent { get; set; }

        public void OnExecute()
        {
            try
            {
                if (!string.IsNullOrEmpty(this.SchematronFile))
                {
                    // run schematron logic
                }
                // TODO: sanitize uris/filenames
                System.Console.WriteLine("Invoking XSD Validation");
                var validator = new XsdValidator();
                var errorList = validator.Validate(this.SchemaFile, this.InputFile);
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
            catch (System.IO.FileNotFoundException ex)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine(String.Format("Cannot find file {0}", ex.FileName));
            }
            catch (System.Xml.Schema.XmlSchemaException ex)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Problem processing the XML Schema Definition");
                if (Parent.Verbose)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            finally
            {
                Console.ResetColor();
            }
        }
        public bool HasErrors { get; set; }

        private class HelpOptionAttribute : Attribute
        {
        }
    }
}