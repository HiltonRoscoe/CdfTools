using McMaster.Extensions.CommandLineUtils;
namespace CdfValidator
{
    [HelpOption("--hlp")]
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
}
