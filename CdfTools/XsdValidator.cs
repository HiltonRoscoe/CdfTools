using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System;
using System.Collections.Generic;

internal class XsdValidator
{
    public bool HasErrors { get; set; }
    public List<string> Validate(string schema, string input)
    {
        var errorList = new List<string>();
        XmlSchemaSet xss = new XmlSchemaSet();
        xss.XmlResolver = new XmlUrlResolver();
        using (XmlReader xmlReader = XmlReader.Create(schema))
        {
            var schemaO = XmlSchema.Read(xmlReader, null);
            xss.Add(schemaO);

        }
        // compile the schema
        xss.Compile();

        var readerSettings = new XmlReaderSettings();
        readerSettings.Schemas = xss;
        readerSettings.ValidationFlags |= XmlSchemaValidationFlags.ReportValidationWarnings;
        readerSettings.ValidationType = ValidationType.Schema;
        readerSettings.ValidationEventHandler += new ValidationEventHandler((object sender, ValidationEventArgs validationEvent) =>
            {
                    // Console.WriteLine(validationEvent.Message);
                    XmlSeverityType type = XmlSeverityType.Warning;
                if (Enum.TryParse<XmlSeverityType>("Error", out type))
                {
                    if (type == XmlSeverityType.Error)
                    {
                        errorList.Add(validationEvent.Message);
                    }
                }
            });

        XmlReader instanceReader = XmlReader.Create(input, readerSettings);

        while (instanceReader.Read())
        {

        }
        return errorList;
    }
}
