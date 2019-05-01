/*
    This file is part of the CdfTools project.
    Copyright (c) 2018-2019 Hilton Roscoe LLC
    Authors: John Dziurlaj
    This program is free software; you can redistribute it and/or modify
    it under the terms of the GNU Affero General Public License version 3
    as published by the Free Software Foundation with the addition of the
    following permission added to Section 15 as permitted in Section 7(a):
    FOR ANY PART OF THE COVERED WORK IN WHICH THE COPYRIGHT IS OWNED BY
    HILTON ROSCOE LLC. HILTON ROSCOE LLC DISCLAIMS THE WARRANTY OF NON 
    INFRINGEMENT OF THIRD PARTY RIGHTS
    This program is distributed in the hope that it will be useful, but
    WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY
    or FITNESS FOR A PARTICULAR PURPOSE.
    See the GNU Affero General Public License for more details.
    You should have received a copy of the GNU Affero General Public License
    along with this program; if not, see http://www.gnu.org/licenses or write to
    the Free Software Foundation, Inc., 51 Franklin Street, Fifth Floor,
    Boston, MA, 02110-1301 USA, or download the license from the following URL:
    http://itextpdf.com/terms-of-use/
    The interactive user interfaces in modified source and object code versions
    of this program must display Appropriate Legal Notices, as required under
    Section 5 of the GNU Affero General Public License.    
 */
 using McMaster.Extensions.CommandLineUtils;
namespace CdfTools
{
    [HelpOption("--hlp")]
    [Command(ExtendedHelpText = "Tools for working with Common Data Formats",
    Name = "cdftools",
    FullName = "Common Data Format Tools")]
    [Subcommand("validate", typeof(ValidateCommand))]
    class Program
    {
        [Option(Template = "-v|--verbose")]
        public bool Verbose { get; }
        public static int Main(string[] args) => CommandLineApplication.Execute<Program>(args);
        public int OnExecute(CommandLineApplication app)
        {
            // this shows help even if the --help option isn't specified
            app.ShowHelp();
            return 1;
        }
    }
}
