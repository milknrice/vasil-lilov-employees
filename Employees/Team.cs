using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employees
{
    public class Team
    {
        public int firstEmpID { get; set; }

        public int secondEmpID { get; set; }

        public int projectID { get; set; }

        public TimeSpan timeWorkedTogether { get; set; }

        public void PrintTeam()
        {
            Console.WriteLine("\tThe team which has worked together for the longest time is: \n");
            Console.WriteLine($"First employee ID: {firstEmpID}");
            Console.WriteLine($"Second employee ID: {secondEmpID}");
            Console.WriteLine($"Time worked together: {timeWorkedTogether.Days} days");
            Console.WriteLine($"On project with ID: {projectID}");
        }


    }
}
