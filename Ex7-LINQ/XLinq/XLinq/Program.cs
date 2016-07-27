/*Moran Ankori*/
/*Lab 4.2*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace XLinq
{
    class Program
    {
        static void Main(string[] args)
        {
            //1)
            var types = from c in typeof(string).Assembly.GetExportedTypes()
                        where c.IsClass
                        let properties = c.GetProperties(BindingFlags.Public)
                        select new XElement("Type",
                            new XAttribute("FullName", c.FullName),
                            new XElement("Properties",
                                from property in properties
                                select new XElement("Property",
                                    new XAttribute("Name", property.Name),
                                     new XAttribute("Type", property?.PropertyType?.FullName ?? "NullType"))),
                            new XElement("Methods",
                                from method in c.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                                where !method.IsSpecialName
                                select new XElement("Method",
                                    new XAttribute("Name", method.Name),
                                    new XAttribute("ReturnType", method.ReturnType.FullName ?? "T"),
                                    new XElement("Parameters",
                                        from prm in method.GetParameters()
                                        select new XElement("Parameter",
                                            new XAttribute("Name", prm.Name),
                                            new XAttribute("Type", prm.ParameterType))))));

            XElement xml = new XElement("Types", types);

            //test:
          //  Console.WriteLine(xml);


            //3) 
            //a.
            var type_no_prop = from type in types
                          where type.Element("Properties").Descendants().Count() == 0
                          let name = (string)type.Attribute("FullName")
                          orderby name
                          select name;

            //test
            Console.WriteLine($"Types with no properties: {type_no_prop.Count()}");
            foreach (var type in type_no_prop)
                Console.WriteLine(type);

            //3) b.
            Console.WriteLine("Total methods: {0}", 
                types.Sum(t => t.Descendants("Method").Count()));

            //3) c.
            Console.WriteLine("Total properties: {0}", 
                types.Sum(e => e.Descendants("Property").Count()));

            var mostCommonParams = from p in types.Descendants("Parameter")
                             group p
                             by (string)p.Attribute("Type")
                                      into g
                             orderby g.Count() descending
                             select new
                             {
                                 Name = g.Key,
                                 Count = g.Count()
                             };
            Console.WriteLine($"Most common parameter type: {mostCommonParams.First().Name} ");

            //3) d.
            var sortedTypes = from t in types
                                  let methods = t.Descendants("Method").Count()
                                  orderby methods descending
                                  select new
                                  {
                                      Name = (string)t.Attribute("FullName"),
                                      Methods = methods,
                                      Properties = t.Descendants("Property").Count()
                                  };

            foreach (var i in sortedTypes)
                Console.WriteLine(i);

            //3) e.
            var groupedByMethods = from t in types
                                 let methods = t.Descendants("Method").Count()
                                 orderby (string)t.Attribute("FullName")
                                 group new
                                 {
                                     Methods = methods,
                                     Properties = t.Descendants("Property").Count(),
                                     Name = (string)t.Attribute("FullName")
                                 } by methods into g
                                 orderby g.Key descending
                                 select g;
            //test
            foreach (var t in groupedByMethods)
            {
                Console.WriteLine($"Methods number: {t.Key}");
                foreach (var type in t)
                    Console.WriteLine($"{type.Name} {type.Properties} Properties");
            }


            Console.Read();
        }
    }
}
