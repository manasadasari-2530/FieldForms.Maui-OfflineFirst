using OfflineFormApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class LocalDbService
{
    public static async Task SaveReportAsync(string formName, string jsonData)
    {
        var fileName = $"{formName}_{DateTime.Now:yyyyMMdd_HHmmss}.json";
        var path = Path.Combine(FileSystem.AppDataDirectory, fileName);
        await File.WriteAllTextAsync(path, jsonData);

        // Save to SQLite
        await SQLiteService.SaveFormAsync(formName, jsonData);
    }
}
