using Microsoft.Maui.Networking;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OfflineFormSolution.Models;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

public static class FormSchemaService
{
    public static async Task<FormSchema> GetFormSchemaAsync(string name)
    {
        var assembly = Assembly.GetExecutingAssembly();
        var resourceName = "OfflineFormApp.Resources.form-schema.json";

        using Stream stream = assembly.GetManifestResourceStream(resourceName);
        if (stream == null)
            throw new FileNotFoundException("form-schema.json not found in embedded resources.");

        using StreamReader reader = new StreamReader(stream);
        var json = await reader.ReadToEndAsync();

        return JsonConvert.DeserializeObject<FormSchema>(json);
    }

    public static async Task<List<string>> FetchPickerItemsAsync(string url)
    {
        // Generate a cache file name unique per URL
        // This ensures cache for multiple pickers or endpoints doesn't conflict.
        string urlHash = Convert.ToHexString(MD5.HashData(Encoding.UTF8.GetBytes(url)));
        var cachePath = Path.Combine(FileSystem.AppDataDirectory, $"picker_{urlHash}.json");

        // Try to load from cache first (OFFLINE FIRST)
        if (File.Exists(cachePath))
        {
            try
            {
                var cached = await File.ReadAllTextAsync(cachePath);
                var json = JObject.Parse(cached);
                var items = json["Results"]?
                    .Select(r => r["Mfr_Name"]?.ToString())
                    .Where(s => !string.IsNullOrEmpty(s))
                    .ToList();

                if (items != null && items.Count > 0)
                    return items;
            }
            catch { /* Continue to try web if cache is corrupt! */ }
        }

        // Try to download if online and cache is missing or empty
        if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
        {
            try
            {
                using var client = new HttpClient();
                var response = await client.GetStringAsync(url);

                await File.WriteAllTextAsync(cachePath, response); // cache for offline use

                var json = JObject.Parse(response);
                var items = json["Results"]?
                    .Select(r => r["Mfr_Name"]?.ToString())
                    .Where(s => !string.IsNullOrEmpty(s))
                    .ToList();

                return items ?? new();
            }
            catch
            {
                // fail over to empty, client is offline or service is unreachable
            }
        }

        // Still here? No data available: show fallback
        return new List<string>();
    }
}
