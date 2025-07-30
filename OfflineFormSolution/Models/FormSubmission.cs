using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfflineFormApp
{
    public class FormSubmission
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string FormName { get; set; }
        public string JsonData { get; set; }
        public bool IsSynced { get; set; } 
        public DateTime SubmittedAt { get; set; } = DateTime.Now;
    }
}