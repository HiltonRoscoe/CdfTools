using System.IO;
using System.Xml;
using System.Xml.Xsl;
namespace CdfTools
{
    public class SchematronValidator
    {
        public static XmlDocument Validate(XmlDocument schematronRules, XmlDocument input)
        {
            var basePath = "PUT BASE PATH HERE";
            var schematronXslt = new XslCompiledTransform();
            XsltArgumentList argsList = new XsltArgumentList();
            // allow use of xslt within sch ruleset.
            argsList.AddParam("allow-foreign", "", "true");
            // load the iso schematron transform
            schematronXslt.Load(basePath + "/xslt/iso_svrl_for_xslt1.xsl", new XsltSettings(true, true), new XmlUrlResolver());
            // Transform input document to XmlDocument for additional processing  
            XmlDocument schematronRuleDoc = new XmlDocument();
            using (XmlWriter writer = schematronRuleDoc.CreateNavigator().AppendChild())
            {
                // generate validator for Schematron rules
                schematronXslt.Transform(schematronRules, argsList, writer);
            }
            var ruleXslt = new XslCompiledTransform();
            ruleXslt.Load(schematronRuleDoc);
            XmlDocument svrlDoc = new XmlDocument();
            using (XmlWriter writer = svrlDoc.CreateNavigator().AppendChild())
            {
                ruleXslt.Transform(input, writer);
            }
            return svrlDoc;
        }
    }
}
