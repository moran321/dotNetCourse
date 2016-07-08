/*Moran Ankori*/
/* Lab 1.2 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
namespace AttribDemo
{
    public class AssemblyAnalyzer
    {

        public bool AnalayzeAssembly(Assembly assemblyObject)
        {
            Type[] types = assemblyObject.GetTypes();
            bool IsAllApproved = true;
            foreach (Type type in types)
            {
                Attribute attribute = type.GetCustomAttribute(typeof(CodeReviewAttribute));
                if (attribute != null)
                {
                    Console.WriteLine(attribute);
                    CodeReviewAttribute reviewAttribute = attribute as CodeReviewAttribute;
                    if (reviewAttribute!=null)
                    {
                        if (!reviewAttribute.IsApproved)
                            IsAllApproved = false;
                    }
                }


            }
            return IsAllApproved;

        }
    }
}
