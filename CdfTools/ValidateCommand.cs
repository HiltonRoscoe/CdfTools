using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using McMaster.Extensions.CommandLineUtils;
namespace CdfTools
{
    // [HelpOption("--hlp")]
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
        //    [Required]
        public string SchematronFile { get; }
        // You can use this pattern when the parent command may have options or methods you want to
        // use from sub-commands.
        // This will automatically be set before OnExecute is invoked
        private Program Parent { get; set; }

        public void OnExecute()
        {
            try
            {
                // check which validator to invoke
                if (this.InputFile.EndsWith(".xml"))
                {
                    if (!string.IsNullOrEmpty(this.SchematronFile))
                    {
                        throw new NotImplementedException("Schematron support is not available at this time");
                        // XmlValidationHarness.schematron(this.InputFile, this.SchematronFile);
                    }
                    XmlValidationHarness.xsd(this.InputFile, this.SchemaFile);
                }
                else if (this.InputFile.EndsWith(".json"))
                {
                    // note that schematron flag is ignored in this case
                    JsonSchemaValidator.jsonSchema(this.InputFile, this.SchemaFile);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine(String.Format("Unknown file type!"));
                }
            }
            catch (FileNotFoundException ex)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine(String.Format("Cannot find file {0}", ex.FileName));
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