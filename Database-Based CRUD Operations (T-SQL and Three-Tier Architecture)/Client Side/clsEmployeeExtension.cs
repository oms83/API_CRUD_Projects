using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client_Side
{
    public static class clsEmployeeExtension
    {
        public static void Print(this List<clsEmployee> source)
        {
            source.ForEach(e => Console.WriteLine(e));
        }
    }
}
