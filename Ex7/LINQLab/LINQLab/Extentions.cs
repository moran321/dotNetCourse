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
            var properties = from p in obj.GetType().GetProperties()
                             from q in other_obj.GetType().GetProperties()
                             where p.Name == q.Name && p.CanRead && q.CanWrite &&
                                 p.PropertyType == q.PropertyType
                             select new
                             {
                                 SrcProperty = p,
                                 DstProperty = q
                             };
            foreach (var property in properties)
                property.DstProperty.SetValue(other_obj, property.SrcProperty.GetValue(obj, null), null);

        }
    }
    /******************************************/

}
