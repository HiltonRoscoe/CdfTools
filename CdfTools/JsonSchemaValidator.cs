using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Schema;

namespace CdfTools
{
    class JsonSchemaValidator
    {
        public static void jsonSchema(string inputPath, string schemaPath)
        {
            System.Console.WriteLine("Invoking JSON Schema Validation");
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

                                IList<string> errorList = new List<string>();
                                validatingReader.ValidationEventHandler += (o, a) =>
                                {
                                    errorList.Add(a.Message);
                                    // send out immediately
                                    // Console.WriteLine(a.Message);
                                };

                                JsonSerializer serializer = new JsonSerializer();

                                // json will be validated without loading the entire document into memory
                                while (validatingReader.Read())
                                {
                                }

                                if (errorList.Count > 0)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    foreach (var error in errorList)
                                    {
                                        Console.WriteLine(error);
                                    }
                                    Console.WriteLine("JSON Instance has one or more errors");
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine("JSON Instance is Valid");
                                }
                                Console.ResetColor();
                            }
                        }
                    }
                }
            }
        }
    }
}