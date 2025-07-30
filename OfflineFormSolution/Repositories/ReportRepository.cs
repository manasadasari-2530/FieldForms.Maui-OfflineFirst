using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfflineFormApp.Repositories
{
    public static class ReportRepository
    {
        static string dbPath = Path.Combine(FileSystem.AppDataDirectory, "app_data.db");

        public static List<FormSubmission> GetAllReports()
        {
            using var db = new SQLiteConnection(dbPath);
            db.CreateTable<FormSubmission>();
            return db.Table<FormSubmission>().OrderByDescending(r => r.SubmittedAt).ToList();
        }
    }
}