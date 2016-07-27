using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQLab
{
    public static class Extentions
    {
        public static bool IsAccessible(this Process p)
        {
            try {
                if (p.Handle != IntPtr.Zero)
                    return true;
                else
                    return false;
            }catch (System.ComponentModel.Win32Exception e)
            {
                return false;
            }
            

        }

        // 2) (*)
        public static void CopyTo(this object obj, object other_obj)
        {
            //get the readable properties and write to writable properties
            var properties = from p in obj.GetType().GetProperties()
                             from p2 in other_obj.GetType().GetProperties()
                             where p.CanRead && p2.CanWrite &&
                                   p.PropertyType == p2.PropertyType &&
                                   p.Name == p2.Name
                             select new
                             {
                                 SrcProperty = p,
                                 DstProperty = p2
                             };
            //copy operation
            foreach (var property in properties)
                property.DstProperty.SetValue(other_obj, property.SrcProperty.GetValue(obj, null), null);

        }
    }
    /******************************************/

}
