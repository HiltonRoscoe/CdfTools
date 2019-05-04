using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Schema;

namespace CdfTools
{
    class JsonSchemaValidator
    {
        public static List<String> jsonSchema(string inputPath, string schemaPath)
        {            
            using (StreamReader schemaFile = File.OpenText(schemaPath))
            {
                using (StreamReader inputFile = File.OpenText(inputPath))
                {
                    using (JsonTextReader schemaReader = new JsonTextReader(schemaFile))
                    {
                        using (JsonTextReader inputReader = new JsonTextReader(inputFile))
                        {
                            JSchema schema = JSchema.Load(schemaReader);
                            using (JSchemaValidatingReader validatingReader = new JSchemaValidatingReader(inputReader))
                            {
                                validatingReader.Schema = schema;

                                List<string> errorList = new List<string>();
                                validatingReader.ValidationEventHandler += (o, a) =>
                                {
                                    errorList.Add(a.Message);             
                                };

                                JsonSerializer serializer = new JsonSerializer();

                                // json will be validated without loading the entire document into memory
                                while (validatingReader.Read())
                                {
                                }

                                return errorList;
                            }
                        }
                    }
                }
            }
        }
    }
}