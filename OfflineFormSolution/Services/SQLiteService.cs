using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfflineFormApp.Services
{
    public static class SQLiteService
    {
        private static SQLiteAsyncConnection _database;

        private static async Task InitAsync()
        {
            if (_database != null)
                return;

            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "form_submissions.db");
            _database = new SQLiteAsyncConnection(dbPath);

            await _database.CreateTableAsync<FormSubmission>();
        }

        public static async Task SaveFormAsync(string formName, string jsonData)
        {
            await InitAsync();

            var form = new FormSubmission
            {
                FormName = formName,
                JsonData = jsonData,
                IsSynced = false,
                SubmittedAt = DateTime.Now
            };

            await _database.InsertAsync(form);

        }

        public static async Task<List<FormSubmission>> GetPendingFormsAsync()
        {
            await InitAsync();
            return await _database.Table<FormSubmission>()
                                 .Where(f => !f.IsSynced)
                                 .ToListAsync();
        }
        public static async Task MarkFormAsSyncedAsync(FormSubmission form)
        {
            await InitAsync();
            form.IsSynced = true;
            await _database.UpdateAsync(form);
        }

        public static async Task DeleteFormAsync(FormSubmission form)
        {
            await InitAsync();
            await _database.DeleteAsync(form);
        }
        public static async Task<List<FormSubmission>> GetAllFormsAsync()
        {
            await InitAsync();
            return await _database.Table<FormSubmission>().ToListAsync();
        }
    }
}