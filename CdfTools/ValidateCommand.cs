using System;
using System.ComponentModel.DataAnnotations;
using CdfValidator;
using McMaster.Extensions.CommandLineUtils;

//[HelpOption("--hlp")]
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

    // You can use this pattern when the parent command may have options or methods you want to
    // use from sub-commands.
    // This will automatically be set before OnExecute is invoked
    private Program Parent { get; set; }

    public void OnExecute()
    {
        try
        {
            // TODO: sanitize uris/filenames
            System.Console.WriteLine("Invoking XSD Validation");
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
        catch (System.IO.FileNotFoundException ex)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(String.Format("Cannot find file {0}", ex.FileName));
        }
        catch (System.Xml.Schema.XmlSchemaException ex)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("Problem processing the XML Schema Definition");
            if(Parent.Verbose){
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
