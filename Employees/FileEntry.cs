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
        
        ///yyyy-mm-dd
        public DateTime DateFrom { get; set; }

        //yyyy-mm-dd
        public DateTime DateTo { get; set; }

        public void AddEntry (string line)
        {
            string[] subs = line.Split(',');

            foreach (var element in subs)
            {
                EmpID = Convert.ToInt32(subs[0]);
                ProjectID = Convert.ToInt32(subs[1]);
                DateFrom = Convert.ToDateTime(subs[2]);
                DateTo = Convert.ToDateTime(subs[3]);
            }
        }
    }

    
}
