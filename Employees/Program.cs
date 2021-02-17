using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace Employees
{
    class Program
    {
        static void Main(string[] args)
        {
            List<FileEntry> employees = new List<FileEntry>();

            try
            {
                string dir = Directory.GetCurrentDirectory();

                string[] lines = System.IO.File.ReadAllLines($@"{dir}\data.txt");

                foreach (string line in lines)
                {
                    FileEntry entry = new FileEntry();
                    string trimmedLine = Regex.Replace(line, @" ", "");
                    entry.AddEntry(trimmedLine);
                    employees.Add(entry);
                }

                Console.WriteLine("\nList of employees: \n");
                foreach (FileEntry item in employees)
                {
                    item.PrintEntry();
                }

                CompareEmployees(employees);
            }

            catch (Exception e)
            {
                Console.WriteLine($"Error { e.Message}");
            }

            Console.Read();
        }

        static void CompareEmployees(List<FileEntry> employees)
        {
            List<Team> teams = new List<Team>();

            for (int i = 0; i < employees.Count - 1; i++)
            {
                for (int j = i + 1; j < employees.Count; j++)
                {
                    if (employees[i].ProjectID == employees[j].ProjectID)
                    {
                        if (employees[i].DateTo.CompareTo(employees[j].DateFrom) > 0 && employees[i].DateFrom.CompareTo(employees[j].DateTo) < 0)
                        {
                            //Periods match!
                            Team team = new Team();
                            team.firstEmpID = employees[i].EmpID;
                            team.secondEmpID = employees[j].EmpID;
                            team.projectID = employees[i].ProjectID;

                            if (employees[i].DateTo.CompareTo(employees[j].DateTo) > 0)
                            {
                                if (employees[j].DateFrom.CompareTo(employees[i].DateFrom) > 0)
                                    team.timeWorkedTogether = employees[j].DateTo.Subtract(employees[j].DateFrom);

                                else
                                    team.timeWorkedTogether = employees[j].DateTo.Subtract(employees[i].DateFrom);
                            }
                            else
                            {
                                if (employees[j].DateFrom.CompareTo(employees[i].DateFrom) > 0)
                                    team.timeWorkedTogether = employees[i].DateTo.Subtract(employees[j].DateFrom);

                                else
                                    team.timeWorkedTogether = employees[i].DateTo.Subtract(employees[i].DateFrom);
                            }

                            teams.Add(team);
                        }
                    }
                }
            }

            if (teams != null)
            {
                Team bestTeam = new Team();

                for (int i = 0; i < teams.Count - 1; i++)
                {
                    for (int j = i + 1; j < teams.Count; j++)
                    {
                        if (teams[i].timeWorkedTogether > teams[j].timeWorkedTogether)
                            bestTeam = teams[i];
                        else
                            bestTeam = teams[j];
                    }
                }

                bestTeam.PrintTeam();
            }
        }
    }
}
