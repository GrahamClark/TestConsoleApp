using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace TestConsoleApp.FieldReplacement
{
    public class Runner : IRunner
    {
        public void RunProgram()
        {
            const string template = @"
                <head>
                    <style type=""text/css"">
                        body { 
                            font-weight: bold;
                        }
                    </style>
                </head>
                <body>Hello {{FirstName}} {{LastName}} you are {{Age}}</body>";
            Console.WriteLine(template.ReplaceFields(new { FirstName = "Bob", LastName = "Slob", Age = 23 }));
        }
    }

    public static class Extensions
    {
        // match all text between double curly braces ({{ }})
        private static readonly Regex _fieldReplacementPattern = new Regex(@"(\{\{)([^\}]+)(\}\})", RegexOptions.Compiled);

        public static string ReplaceFields(this string template, object replacementFields)
        {
            if (replacementFields == null)
            {
                throw new ArgumentNullException("replacementFields");
            }

            Type templateType = replacementFields.GetType();
            var cache = new Dictionary<string, string>();
            return _fieldReplacementPattern.Replace(template, match =>
            {
                string key = match.Groups[2].Value;
                string value;
                if (!cache.TryGetValue(key, out value))
                {
                    var property = templateType.GetProperty(key);
                    if (property == null)
                    {
                        // no matching property found in the replacement fields, keep the template unchanged
                        return match.Value;
                    }

                    value = Convert.ToString(property.GetValue(replacementFields, null));
                    cache.Add(key, value);
                }

                return value;
            });
        }
    }
}
