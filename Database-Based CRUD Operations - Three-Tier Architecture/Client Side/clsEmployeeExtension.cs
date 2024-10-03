using System;

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
