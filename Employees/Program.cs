using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Employees
{
    class Program
    {
        static void Main(string[] args)
        {
            List<FileEntry> employees = new List<FileEntry>();

            string dir = Directory.GetCurrentDirectory();

            string[] lines = System.IO.File.ReadAllLines($@"{dir}\data.txt");
            
            foreach (var line in lines)
            {
                FileEntry entry = new FileEntry();
                entry.AddEntry(line);

                employees.Add(entry);
            }

            foreach (var item in employees)
            {
                Console.WriteLine($"EmployeeID: {item.EmpID}");
                Console.WriteLine($"ProjectID: {item.ProjectID}");
                Console.WriteLine($"DateFrom: {item.DateFrom}");
                Console.WriteLine($"DateTo: {item.DateTo} \n");
            }

            CompareEmployees(employees);
        }

        static void CompareEmployees(List<FileEntry> employees)
        {
            for (int i = 0; i < employees.Count - 1; i++)
            {
                for (int j = i + 1; j < employees.Count; j++)
                {
                    if (employees[i].ProjectID == employees[j].ProjectID)
                    {
                        Team team = new Team();
                        team.firstEmpID = employees[i].EmpID;
                        team.secondEmpID = employees[j].EmpID;

                        if (employees[i].DateTo.CompareTo(employees[j].DateFrom) > 0 && employees[i].DateFrom.CompareTo(employees[j].DateTo) < 0)
                        {
                            Console.WriteLine("\nPeriods match!");

                            if (employees[i].DateTo.CompareTo(employees[j].DateTo) > 0)
                            {
                                if (employees[j].DateFrom.CompareTo(employees[i].DateFrom) > 0)
                                {
                                    team.timeWorkedTogether = employees[j].DateTo.Subtract(employees[j].DateFrom);
                                }
                                else
                                {
                                    team.timeWorkedTogether = employees[j].DateTo.Subtract(employees[i].DateFrom);
                                }
                            }
                            else
                            {
                                if (employees[j].DateFrom.CompareTo(employees[i].DateFrom) > 0)
                                {
                                    team.timeWorkedTogether = employees[i].DateTo.Subtract(employees[j].DateFrom);
                                }
                                else
                                {
                                    team.timeWorkedTogether = employees[i].DateTo.Subtract(employees[i].DateFrom);
                                }
                            }

                            Console.WriteLine($"\nPeriod worked together: {team.timeWorkedTogether}");
                        }
                    }
                }
            }
        }
    }
}
