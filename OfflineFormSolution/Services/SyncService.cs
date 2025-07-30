using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace OfflineFormApp.Services
{
    // ✅ This class encapsulates your sync logic
    public static class SyncService
    {
        private static HttpClient _client = new HttpClient();
        private static string SyncEndpoint = "https://yourapi.com/api/sync/sendbatch";

        public static async Task SyncPendingFormsAsync()
        {
            var pending = await SQLiteService.GetPendingFormsAsync();
            if (!pending.Any()) return;

            var batch = pending.Select(f => new
            {
                Id = f.Id,
                FormName = f.FormName,
                JsonData = f.JsonData,
                SubmittedAt = f.SubmittedAt
            }).ToList();
            var json = JsonSerializer.Serialize(batch);

            try
            {
                var response = await _client.PostAsync(
                    SyncEndpoint,
                    new StringContent(json, System.Text.Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    foreach (var form in pending)
                        await SQLiteService.MarkFormAsSyncedAsync(form);
                }
            }
            catch (Exception ex)
            {
                // ✅ Optional: Log or handle the error
                System.Diagnostics.Debug.WriteLine($"Sync error: {ex.Message}");
            }
        }
    }
}
