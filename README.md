# Common Data Format Tools (.NET version)

This project provides tooling to support implementation of the NIST 1500 series common data formats. Producers or consumers of a NIST 1500 file can validate that their files conform to specification schemas. It is written in .NET Core, and works on Windows, Linux and Mac OS X.

Features:

- XML Schema (XSD) Validation
- JSON Schema Validation
- Large File Support

CDfTools is a command line interface tool. Basic familiarity with the command line is required.

## Running CdfTools

```sh
$ CdfTools

Common Data Format Tools

Usage: cdftools [options] [command]

Options:
  --hlp         Show help information
  -v|--verbose  Verbose

Commands:
  validate

Run 'cdftools [command] --hlp' for more information about a command.
```

## Validating Files

CdfTools can validate JSON and XML instances against JSON and XML schema files. The syntax is of the form:

```sh
CdfTools validate --schema '{input schema}' --input '{input schema instance}'
```

> CdfTools automatically determines whether to invoke XSD or JSON Schema validation based on the file extension of the input. An XML file ending in `.xml` or a JSON file ending in `.json` is required.

> Schema files are not included with `CdfTools`. Please visit the [NIST Voting Repository](https://github.com/usnistgov/voting) and download the required `joson` or `xml` file.