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
                // Get the current directory, navigate and read from file.
                string dir = Directory.GetCurrentDirectory();
                string[] lines = System.IO.File.ReadAllLines($@"{dir}\data.txt");

                // Populate list of employees with FileEntry objects for each employee.
                foreach (string line in lines)
                {
                    FileEntry entry = new FileEntry();
                    string trimmedLine = Regex.Replace(line, @" ", "");
                    entry.AddEntry(trimmedLine);

                    employees.Add(entry);
                }

                // Print out the list of employees.
                Console.WriteLine("\nList of employee-project entries: \n");
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

            // Prevent program from auto-closing.
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
                            // Periods match, a team from a pair of employees is being created.
                            Team team = new Team();
                            team.firstEmpID = employees[i].EmpID;
                            team.secondEmpID = employees[j].EmpID;
                            team.projectID = employees[i].ProjectID.ToString();

                            // Calculate the time during which the employees have worked together.
                            if (employees[i].DateTo.CompareTo(employees[j].DateTo) > 0)
                            {
                                if (employees[j].DateFrom.CompareTo(employees[i].DateFrom) > 0)
                                    team.timeWorkedTogether = employees[j].DateTo.Subtract(employees[j].DateFrom) + TimeSpan.FromDays(1);

                                else
                                    team.timeWorkedTogether = employees[j].DateTo.Subtract(employees[i].DateFrom) + TimeSpan.FromDays(1);
                            }
                            else
                            {
                                if (employees[j].DateFrom.CompareTo(employees[i].DateFrom) > 0)
                                    team.timeWorkedTogether = employees[i].DateTo.Subtract(employees[j].DateFrom) + TimeSpan.FromDays(1);

                                else
                                    team.timeWorkedTogether = employees[i].DateTo.Subtract(employees[i].DateFrom) + TimeSpan.FromDays(1);
                            }

                            // The current team is added to a list of teams.
                            teams.Add(team);
                        }
                    }
                }
            }

            if (teams.Count == 1)
                teams[0].PrintTeam();

            else if (teams.Count > 1)
            {
                Team bestTeam = new Team();

                // Check if there are teams, which were working on more than one mutual project and sum their worktime.
                for (int i = 0; i < teams.Count - 1; i++)
                {
                    for (int j = i + 1; j < teams.Count; j++)
                    {
                        if (teams[i].firstEmpID == teams[j].firstEmpID)
                            if (teams[i].secondEmpID == teams[j].secondEmpID)
                            {
                                // Add the time worked from the other mutual project and it's ID into the projects string.
                                teams[i].timeWorkedTogether += teams[j].timeWorkedTogether;
                                teams[i].projectID += $", {teams[j].projectID}";

                                // Delete the unnecessary team entry for fewer cycles in the future.
                                teams.RemoveAt(j);
                            }
                    }
                }

                // Compare teams' timeWorkedTogether and print out the data for the team with highest result.
                for (int i = 0; i < teams.Count - 1; i++)
                {
                    for (int j = i + 1; j < teams.Count; j++)
                    {
                        if (teams[i].timeWorkedTogether > bestTeam.timeWorkedTogether)
                        {
                            if (teams[i].timeWorkedTogether > teams[j].timeWorkedTogether)
                                bestTeam = teams[i];
                            else
                                bestTeam = teams[j];
                        }
                    }
                }
                bestTeam.PrintTeam();
            }
            else
                Console.WriteLine("\nNo teams were formed from the list of employees.");
        }
    }
}
