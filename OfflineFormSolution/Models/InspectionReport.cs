using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfflineFormSolution.Models
{
    public class InspectionReport
    {
        [SQLite.PrimaryKey, SQLite.AutoIncrement]
        public int Id { get; set; }

        public string FormName { get; set; }
        public string JsonData { get; set; }
        public bool IsSynced { get; set; }
    }

}
