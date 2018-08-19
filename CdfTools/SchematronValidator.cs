using System.Xml;
using System.Xml.Xsl;

class SchematronValidator
{
        static void Schematron()
        {
            {
                var xsl = new System.Xml.Xsl.XslCompiledTransform();

                xsl.Load("xslt/iso_svrl_for_xslt1.xsl", new XsltSettings(true, true), new XmlUrlResolver());
                xsl.Transform("xml/ERR.sch", "xml/output.xsl");
            }
            {
                var xsl = new System.Xml.Xsl.XslCompiledTransform();
                xsl.Load("xml/output.xsl");
                xsl.Transform("input.xml", "result.xml");
            }
        }
}