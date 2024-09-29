using Server_Side.Model;
using System;
using System.Collections.Generic;

namespace Server_Side.Data_Simulation
{
    public class EmployeesDataSimulation
    {
        public static readonly List<clsEmployee> EmployeesList = new List<clsEmployee>()
        {
            new clsEmployee(1, "Ömer", "MEMES", 35, 55000.50m, new DateTime(2015, 5, 10), null),
            new clsEmployee(2, "Ayşe", "Yılmaz", 28, 62000.00m, new DateTime(2017, 9, 23), null),
            new clsEmployee(3, "Mehmet", "Demir", 42, 75000.25m, new DateTime(2010, 3, 18), null),
            new clsEmployee(4, "Elif", "Kaya", 30, 58000.75m, new DateTime(2019, 1, 10), new DateTime(2022, 11, 9)),
            new clsEmployee(5, "Ali", "Şahin", 48, 90000.00m, new DateTime(2008, 7, 21), null),
            new clsEmployee(6, "Fatma", "Çelik", 32, 66000.80m, new DateTime(2016, 2, 28), null),
            new clsEmployee(7, "Ahmet", "Öztürk", 29, 64000.50m, new DateTime(2018, 10, 12), null),
            new clsEmployee(8, "Zeynep", "Arslan", 26, 58000.40m, new DateTime(2020, 5, 6), null),
            new clsEmployee(9, "Mustafa", "Yıldırım", 36, 70000.90m, new DateTime(2014, 4, 15), null),
            new clsEmployee(10, "Hüseyin", "Aydın", 40, 80000.60m, new DateTime(2011, 8, 22), new DateTime(2017, 11, 8)),
            new clsEmployee(11, "Serkan", "Polat", 34, 69000.25m, new DateTime(2016, 6, 18), null),
            new clsEmployee(12, "Merve", "Koç", 27, 59000.50m, new DateTime(2019, 3, 29), null),
            new clsEmployee(13, "Emre", "Aslan", 39, 74000.10m, new DateTime(2012, 11, 8), new DateTime(2015, 10, 8)),
            new clsEmployee(14, "Gamze", "Doğan", 31, 63000.45m, new DateTime(2017, 7, 14), null),
            new clsEmployee(15, "Halil", "Güneş", 45, 85000.00m, new DateTime(2009, 12, 19), new DateTime(2012, 04, 13)),
            new clsEmployee(16, "Esra", "Eren", 33, 68000.30m, new DateTime(2015, 2, 7), null),
            new clsEmployee(17, "Furkan", "Çetin", 37, 71000.80m, new DateTime(2013, 9, 10), null),
            new clsEmployee(18, "Gül", "Koçak", 25, 56000.70m, new DateTime(2021, 1, 25), null),
            new clsEmployee(19, "Yunus", "Kaplan", 41, 79000.60m, new DateTime(2010, 4, 30), null),
            new clsEmployee(20, "Bahar", "Gündüz", 29, 62000.90m, new DateTime(2018, 11, 5), null),
        };
    }
}
