using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employees
{
    public class FileEntry
    {
        public int EmpID { get; set; }

        public int ProjectID { get; set; }
        
        public DateTime DateFrom { get; set; }

        public DateTime DateTo { get; set; }


        public void AddEntry (string line)
        {
            string[] subs = line.Split(',');

            for (int i = 0; i < subs.Length; i++)
            {
                EmpID = Convert.ToInt32(subs[0]);
                ProjectID = Convert.ToInt32(subs[1]);
                DateFrom = Convert.ToDateTime(subs[2]);

                if (!(subs[3].Equals("NULL")))
                    DateTo = Convert.ToDateTime(subs[3]);
                else
                    DateTo = DateTime.Today;
            }
        }


        public void PrintEntry()
        {
            Console.WriteLine($"EmployeeID: {EmpID}");
            Console.WriteLine($"ProjectID: {ProjectID}");
            Console.WriteLine($"DateFrom: {DateFrom}");
            Console.WriteLine($"DateTo: {DateTo} \n");
        }
    }
}
