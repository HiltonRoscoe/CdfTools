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
    [Command(ExtendedHelpText = "Tools for working with Common Data Formats",
    Name = "cdftools",
    FullName = "Common Data Format Tools")]

    [Subcommand("validate", typeof(ValidateCommand))]
    class Program
    {
        public static int Main(string[] args) => CommandLineApplication.Execute<Program>(args);
        public int OnExecute(CommandLineApplication app)
        {
            // this shows help even if the --help option isn't specified
            app.ShowHelp();
            return 1;
        }

    }
    [HelpOption("--my-help")]
    [Command(ExtendedHelpText = "Validates Common Data Format instances.")]
    class ValidateCommand
    {

        [LegalFilePath]
        [Option(Template = "-i|--input", Description = "Input instance file")]
        [Required]
        public string Input { get; }

        [LegalFilePath]
        [Option(Template = "-s|--schema", Description = "Input schema file")]
        [Required]
        public string Schema { get; }

        [Option(Template = "-v|--verbose")]
        public bool Verbose { get; }

        public void OnExecute()
        {
            var validator = new XsdValidator();
            var errorList = validator.Validate(this.Schema, this.Input);
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

        public bool HasErrors { get; set; }
    }
}
