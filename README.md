# Common Data Format Tools

This project provides tooling to support implementation of the NIST 1500 series common data formats. It is written in .NET Core, and works on Windows, Linux and Mac OS X.

Features:

- XSD Schema Validation
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
CdfTools validate --schema 'NIST_V0_cast_vote_records.json' --input 'expected_cvr_cdf.json'
```

> CdfTools automatically determines whether to invoke XSD or JSON Schema validation based on the file extension of the input. A XML file ending in `.xml` or a JSON file ending in `.json` is required.