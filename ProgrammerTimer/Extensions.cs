using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ExtesionsMethods
{
    public static class MyExtensions
    {    
        /// <summary>
         /// Returns int. If exception occured returns null
         /// </summary>
        public static int? ToInt(this String str)
        {
            try
            {
                return Int32.Parse(str);
            }
            catch
            {
                return null;
            }
        }
    }
}
